using System;
using System.Linq;
using System.ComponentModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.DevAV.Common.Utils;
using DevExpress.DevAV.Common.DataModel;

namespace DevExpress.DevAV.Common.ViewModel {
    /// <summary>
    /// The base class for POCO view models that operate the collection of documents.
    /// </summary>
    /// <typeparam name="TModule">A navigation list entry type.</typeparam>
    /// <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    public abstract class DocumentsViewModel<TModule, TUnitOfWork>
        where TModule : ModuleDescription<TModule>
        where TUnitOfWork : IUnitOfWork {

        protected readonly IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory;

        /// <summary>
        /// Initializes a new instance of the DocumentsViewModel class.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected DocumentsViewModel(IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory) {
            this.unitOfWorkFactory = unitOfWorkFactory;
            Modules = CreateModules().ToArray();
            foreach(var module in Modules)
                Messenger.Default.Register<NavigateMessage<TModule>>(this, module, x => Show(x.Token));
        }

        /// <summary>
        /// Navigation list that represents a collection of module descriptions.
        /// </summary>
        public TModule[] Modules { get; private set; }

        /// <summary>
        /// A currently selected navigation list entry. This property is writable. When this property is assigned a new value, it triggers the navigating to the corresponding document.
        /// Since DocumentsViewModel is a POCO view model, this property will raise INotifyPropertyChanged.PropertyEvent when modified so it can be used as a binding source in views.
        /// </summary>
  public virtual TModule SelectedModule { get; set; }

        /// <summary>
        /// A navigation list entry that corresponds to the currently active document. If the active document does not have the corresponding entry in the navigation list, the property value is null. This property is read-only.
        /// Since DocumentsViewModel is a POCO view model, this property will raise INotifyPropertyChanged.PropertyEvent when modified so it can be used as a binding source in views.
        /// </summary>
        public virtual TModule ActiveModule { get; protected set; }

        /// <summary>
        /// Saves changes in all opened documents.
        /// Since DocumentsViewModel is a POCO view model, an instance of this class will also expose the SaveAllCommand property that can be used as a binding source in views.
        /// </summary>
        public void SaveAll() {
            Messenger.Default.Send(new SaveAllMessage());
        }

        /// <summary>
        /// Used to close all opened documents and allows you to save unsaved results and to cancel closing.
        /// Since DocumentsViewModel is a POCO view model, an instance of this class will also expose the OnClosingCommand property that can be used as a binding source in views.
        /// </summary>
        /// <param name="cancelEventArgs">An argument of the System.ComponentModel.CancelEventArgs type which is used to cancel closing if needed.</param>
        public void OnClosing(CancelEventArgs cancelEventArgs) {
            Messenger.Default.Send(new CloseAllMessage(cancelEventArgs));
        }

  /// <summary>
  /// Contains a current state of the navigation pane.
  /// </summary>
        /// Since DocumentsViewModel is a POCO view model, this property will raise INotifyPropertyChanged.PropertyEvent when modified so it can be used as a binding source in views.
  public virtual NavigationPaneVisibility NavigationPaneVisibility { get; set; }

  /// <summary>
  /// Navigates to a document.
        /// Since DocumentsViewModel is a POCO view model, an instance of this class will also expose the ShowCommand property that can be used as a binding source in views.
  /// </summary>
        /// <param name="module">A navigation list entry specifying a document what to be opened.</param>
        public void Show(TModule module) {
            if(module == null || DocumentManagerService == null)
                return;
            IDocument document = DocumentManagerService.FindDocumentByIdOrCreate(module, x => CreateDocument(module));
            document.Show();
        }

  /// <summary>
  /// Creates and shows a document which view is bound to PeekCollectionViewModel. The document is created and shown using a document manager service named "WorkspaceDocumentManagerService".
        /// Since DocumentsViewModel is a POCO view model, an instance of this class will also expose the PinPeekCollectionViewCommand property that can be used as a binding source in views.
  /// </summary>
        /// <param name="module">A navigation list entry that is used as a PeekCollectionViewModel factory.</param>
        public void PinPeekCollectionView(TModule module) {
            if(WorkspaceDocumentManagerService == null)
                return;
            IDocument document = WorkspaceDocumentManagerService.FindDocumentByIdOrCreate(module, x => CreatePinnedPeekCollectionDocument(module));
            document.Show();
        }

  /// <summary>
  /// Finalizes the DocumentsViewModel initialization and opens the default document.
        /// Since DocumentsViewModel is a POCO view model, an instance of this class will also expose the OnLoadedCommand property that can be used as a binding source in views.
  /// </summary>
        public virtual void OnLoaded() {
   IsLoaded = true;
            DocumentManagerService.ActiveDocumentChanged += OnActiveDocumentChanged;
            Show(DefaultModule);
        }

        void OnActiveDocumentChanged(object sender, ActiveDocumentChangedEventArgs e) {
            ActiveModule = e.NewDocument == null ? null : e.NewDocument.Id as TModule;
        }

        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>("FrameDocumentManagerService"); } }

        protected IDocumentManagerService WorkspaceDocumentManagerService { get { return this.GetService<IDocumentManagerService>("WorkspaceDocumentManagerService"); } }

        protected virtual TModule DefaultModule { get { return Modules.First(); } }

  protected bool IsLoaded { get; private set; }

        protected virtual void OnSelectedModuleChanged(TModule oldModule) {
   if(IsLoaded)
    Show(SelectedModule);
  }

        protected virtual void OnActiveModuleChanged(TModule oldModule) {
   SelectedModule = ActiveModule;
  }

        IDocument CreateDocument(TModule module) {
            var document = DocumentManagerService.CreateDocument(module.DocumentType, null, this);
            document.Title = GetModuleTitle(module);
            document.DestroyOnClose = false;
            return document;
        }

        protected virtual string GetModuleTitle(TModule module) {
            return module.ModuleTitle;
        }

        IDocument CreatePinnedPeekCollectionDocument(TModule module) {
            var document = WorkspaceDocumentManagerService.CreateDocument("PeekCollectionView", module.CreatePeekCollectionViewModel());
            document.Title = module.ModuleTitle;
            return document;
        }

        protected Func<TModule, object> GetPeekCollectionViewModelFactory<TEntity, TPrimaryKey>(Func<TUnitOfWork, IRepository<TEntity, TPrimaryKey>> getRepositoryFunc) where TEntity : class {
            return module => PeekCollectionViewModel<TModule, TEntity, TPrimaryKey, TUnitOfWork>.Create(module, unitOfWorkFactory, getRepositoryFunc).SetParentViewModel(this);
        }

        protected abstract TModule[] CreateModules();

        protected TUnitOfWork CreateUnitOfWork() {
            return unitOfWorkFactory.CreateUnitOfWork();
        }
    }

 /// <summary>
    /// A base class representing a navigation list entry.
    /// </summary>
    /// <typeparam name="TModule">A navigation list entry type.</typeparam>
    public abstract partial class ModuleDescription<TModule> where TModule : ModuleDescription<TModule> {

  readonly Func<TModule, object> peekCollectionViewModelFactory;
        object peekCollectionViewModel;

        /// <summary>
        /// Initializes a new instance of the ModuleDescription class.
        /// </summary>
        /// <param name="title">A navigation list entry display text.</param>
        /// <param name="documentType">A string value that specifies the view type of corresponding document.</param>
        /// <param name="group">A navigation list entry group name.</param>
        /// <param name="peekCollectionViewModelFactory">An optional parameter that provides a function used to create a PeekCollectionViewModel that provides quick navigation between collection views.</param>
        public ModuleDescription(string title, string documentType, string group, Func<TModule, object> peekCollectionViewModelFactory = null)
		{
            ModuleTitle = title;
            ModuleGroup = group;
            DocumentType = documentType;
            this.peekCollectionViewModelFactory = peekCollectionViewModelFactory;
        }

        /// <summary>
        /// The navigation list entry display text.
        /// </summary>
        public string ModuleTitle
		{
			get;
			private set;
		}

        /// <summary>
        /// The navigation list entry group name.
        /// </summary>
        public string ModuleGroup { get; private set; }

        /// <summary>
        /// Contains the corresponding document view type.
        /// </summary>
        public string DocumentType { get; private set; }

        /// <summary>
        /// A primary instance of corresponding PeekCollectionViewModel used to quick navigation between collection views.
        /// </summary>
        public object PeekCollectionViewModel {
            get {
                if(peekCollectionViewModelFactory == null)
                    return null;
                if(peekCollectionViewModel == null)
                    peekCollectionViewModel = CreatePeekCollectionViewModel();
                return peekCollectionViewModel;
            }
        }

        /// <summary>
        /// Creates and returns a new instance of the corresponding PeekCollectionViewModel that provides quick navigation between collection views.
        /// </summary>
        public object CreatePeekCollectionViewModel() {
            return peekCollectionViewModelFactory((TModule)this);
        }
    }

    /// <summary>
    /// Represents a navigation pane state.
    /// </summary>
 public enum NavigationPaneVisibility {

        /// <summary>
        /// Navigation pane is visible and minimized.
        /// </summary>
     Minimized,

        /// <summary>
        /// Navigation pane is visible and not minimized.
        /// </summary>
        Normal,

        /// <summary>
        /// Navigation pane is invisible.
        /// </summary>
        Off
    }
}
