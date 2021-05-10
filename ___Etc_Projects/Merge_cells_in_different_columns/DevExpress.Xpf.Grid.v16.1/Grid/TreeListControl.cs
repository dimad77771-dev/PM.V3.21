// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI.Native.ViewGenerator;
using DevExpress.Utils.Design.DataAccess;
using DevExpress.Utils.Serializing;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Grid.Automation;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Grid.TreeList;
using DevExpress.Xpf.Utils;
using DevExpress.Xpf.Utils.About;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>The TreeList control.
  /// 
  /// </para>
  ///             </summary>
  [LicenseProvider(typeof (DX_WPF_LicenseProvider))]
  [DataAccessMetadata("All", EnableInMemoryCollectionViewBinding = false, SupportedProcessingModes = "Simple")]
  [DXToolboxBrowsable]
  public class TreeListControl : GridDataControlBase
  {
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ViewProperty;
    private static readonly DependencyPropertyKey BandsLayoutPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty BandsLayoutProperty;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent CopyingToClipboardEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent SelectionChangedEvent;

    /// <summary>
    ///                 <para>Gets or sets the view. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListView" /> object that is the view used to display data.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListControlView")]
    [XtraSerializableProperty(XtraSerializationVisibility.Content)]
    [Category("View")]
    public TreeListView View
    {
      get
      {
        return (TreeListView) this.GetValue(TreeListControl.ViewProperty);
      }
      set
      {
        this.SetValue(TreeListControl.ViewProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public TreeListControlBandsLayout BandsLayout
    {
      get
      {
        return (TreeListControlBandsLayout) this.GetValue(TreeListControl.BandsLayoutProperty);
      }
      internal set
      {
        this.SetValue(TreeListControl.BandsLayoutPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Provides access to the collection of sorted columns.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListSortInfoCollection" /> collection that contains information on the sorted and grouping columns.
    /// </value>
    [XtraSerializableProperty(true, false, false)]
    [XtraResetProperty]
    [Category("Data")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [GridUIProperty]
    public TreeListSortInfoCollection SortInfo
    {
      get
      {
        return (TreeListSortInfoCollection) this.SortInfoCore;
      }
    }

    /// <summary>
    ///                 <para>Provides access to a collection of total summary items.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListSummaryItemCollection" /> object that contains total summary items.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListControlTotalSummary")]
    [GridUIProperty]
    [XtraResetProperty]
    [XtraSerializableProperty(true, false, false)]
    [Category("Data")]
    public TreeListSummaryItemCollection TotalSummary
    {
      get
      {
        return (TreeListSummaryItemCollection) this.TotalSummaryCore;
      }
    }

    /// <summary>
    ///                 <para>Provides access to the column collection.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListColumnCollection" /> object that is a collection of columns within the control.
    /// 
    /// </value>
    [GridStoreAlwaysProperty]
    [DevExpressXpfGridLocalizedDescription("TreeListControlColumns")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    [XtraSerializableProperty(true, true, true)]
    public TreeListColumnCollection Columns
    {
      get
      {
        return (TreeListColumnCollection) this.ColumnsCore;
      }
    }

    protected internal override IDesignTimeAdornerBase EmptyDesignTimeAdorner
    {
      get
      {
        return (IDesignTimeAdornerBase) EmptyDesignTimeAdornerBase.Instance;
      }
    }

    internal override DataViewBase DataView
    {
      get
      {
        return this.viewCore;
      }
      set
      {
        this.View = (TreeListView) value;
      }
    }

    internal override DetailDescriptorBase DetailDescriptorCore
    {
      get
      {
        return (DetailDescriptorBase) null;
      }
    }

    protected internal override Type ColumnType
    {
      get
      {
        return typeof (TreeListColumn);
      }
    }

    protected internal override Type BandType
    {
      get
      {
        return typeof (TreeListControlBand);
      }
    }

    protected internal override BandsLayoutBase BandsLayoutCore
    {
      get
      {
        return (BandsLayoutBase) this.BandsLayout;
      }
      set
      {
        this.BandsLayout = (TreeListControlBandsLayout) value;
      }
    }

    /// <summary>
    ///                 <para>Provides access to the treelist's band collection.
    /// </para>
    ///             </summary>
    /// <value>An observable collection of bands within the treelist.</value>
    [XtraSerializableProperty(true, true, true)]
    [GridStoreAlwaysProperty]
    [DevExpressXpfGridLocalizedDescription("TreeListControlBands")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    public ObservableCollectionCore<TreeListControlBand> Bands
    {
      get
      {
        return (ObservableCollectionCore<TreeListControlBand>) this.BandsCore;
      }
    }

    /// <summary>
    ///                 <para>Occurs when data is copied to the clipboard, allowing you to manually copy required data.
    /// </para>
    ///             </summary>
    [Category("Options Copy")]
    public event TreeListCopyingToClipboardEventHandler CopyingToClipboard
    {
      add
      {
        this.AddHandler(TreeListControl.CopyingToClipboardEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListControl.CopyingToClipboardEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after treelist's selection has been changed.
    /// </para>
    ///             </summary>
    [Category("Options Selection")]
    public event TreeListSelectionChangedEventHandler SelectionChanged
    {
      add
      {
        this.AddHandler(TreeListControl.SelectionChangedEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListControl.SelectionChangedEvent, (Delegate) value);
      }
    }

    static TreeListControl()
    {
      DevExpress.Xpf.About.CheckLicenseShowNagScreen(typeof (TreeListControl));
      Type type = typeof (TreeListControl);
      TreeListControl.ViewProperty = DependencyPropertyManager.Register("View", typeof (TreeListView), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(DataControlBase.OnViewChanged), new CoerceValueCallback(DataControlBase.OnCoerceView)));
      TreeListControl.BandsLayoutPropertyKey = DependencyPropertyManager.RegisterReadOnly("BandsLayout", typeof (TreeListControlBandsLayout), type, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((DataControlBase) d).OnBandsLayoutChanged(e.OldValue as BandsLayoutBase, e.NewValue as BandsLayoutBase))));
      TreeListControl.BandsLayoutProperty = TreeListControl.BandsLayoutPropertyKey.DependencyProperty;
      TreeListControl.CopyingToClipboardEvent = EventManager.RegisterRoutedEvent("CopyingToClipboard", RoutingStrategy.Direct, typeof (TreeListCopyingToClipboardEventHandler), type);
      TreeListControl.SelectionChangedEvent = EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Direct, typeof (TreeListSelectionChangedEventHandler), type);
      CommandManager.RegisterClassCommandBinding(type, new CommandBinding((ICommand) ApplicationCommands.SelectAll, (ExecutedRoutedEventHandler) ((d, e) => ((DataControlBase) d).SelectAllMasterDetail()), (CanExecuteRoutedEventHandler) ((d, e) => ((TreeListControl) d).CanSelectAll(d, e))));
      CommandManager.RegisterClassCommandBinding(type, new CommandBinding((ICommand) ApplicationCommands.Copy, (ExecutedRoutedEventHandler) ((d, e) => ((TreeListControl) d).Copy()), (CanExecuteRoutedEventHandler) ((d, e) => ((TreeListControl) d).CanCopy(d, e))));
      CommandManager.RegisterClassCommandBinding(type, new CommandBinding((ICommand) ApplicationCommands.Paste, (ExecutedRoutedEventHandler) ((d, e) => ((TreeListControl) d).Paste()), (CanExecuteRoutedEventHandler) ((d, e) => ((TreeListControl) d).CanPaste(d, e))));
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListControl class.
    /// </para>
    ///             </summary>
    public TreeListControl()
      : this((IDataControlOriginationElement) null)
    {
    }

    internal TreeListControl(IDataControlOriginationElement dataControlOriginationElement)
      : base(dataControlOriginationElement)
    {
      ControlExtensions.SetDefaultStyleKey(this, typeof (TreeListControl));
      DXSerializer.SetSerializationIDDefault((DependencyObject) this, this.GetType().Name);
    }

    internal static TreeListView CreateDefaultTreeListView()
    {
      return new TreeListView();
    }

    protected override EditorsGeneratorBase GetGenerateEditorsWrapper(GenerateBandWrapper bandWrapper)
    {
      return (EditorsGeneratorBase) new DefaultColumnWrapperGenerator(bandWrapper);
    }

    internal override void ValidateDataProvider(DataViewBase newValue)
    {
      if (this.View == null)
        return;
      this.fDataProvider = this.View.DataProviderBase;
    }

    internal override IColumnCollection CreateColumns()
    {
      return (IColumnCollection) new TreeListColumnCollection(this);
    }

    protected internal override BandBase CreateBand()
    {
      return (BandBase) new TreeListControlBand();
    }

    internal override SortInfoCollectionBase CreateSortInfo()
    {
      return (SortInfoCollectionBase) new TreeListSortInfoCollection();
    }

    internal override ISummaryItemOwner CreateSummariesCollection(SummaryItemCollectionType collectionType)
    {
      return (ISummaryItemOwner) new TreeListSummaryItemCollection((DataControlBase) this);
    }

    protected internal override DataViewBase CreateDefaultView()
    {
      return (DataViewBase) TreeListControl.CreateDefaultTreeListView();
    }

    protected override DataProviderBase CreateDataProvider()
    {
      return (DataProviderBase) new EmptyDataProvider();
    }

    internal override SummaryItemBase CreateSummaryItem()
    {
      return (SummaryItemBase) new TreeListSummaryItem();
    }

    /// <summary>
    ///                 <para>Returns the value of the specified data cell.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the handle of the node that contains the cell.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListColumn" /> object that is the column that owns the specified cell.
    /// 
    ///           </param>
    /// <returns>An object that specifies the cell's value.</returns>
    public object GetCellValue(int rowHandle, TreeListColumn column)
    {
      return this.GetCellValueCore(rowHandle, (ColumnBase) column);
    }

    /// <summary>
    ///                 <para>Returns the text displayed within the specified cell.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the row's handle.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListColumn" /> object that represents the column displayed within the grid.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:System.String" /> value that specifies the text displayed within the specified cell.
    /// </returns>
    public string GetCellDisplayText(int rowHandle, TreeListColumn column)
    {
      return this.GetCellDisplayTextCore(rowHandle, (ColumnBase) column);
    }

    protected internal override IList<SummaryItemBase> GetGroupSummaries()
    {
      throw new NotImplementedException();
    }

    internal override object GetGroupSummaryValue(int rowHandle, int summaryItemIndex)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///                 <para>Selects multiple nodes, while preserving the current selection (if any).
    /// </para>
    ///             </summary>
    /// <param name="startNode">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node at which the selection starts.
    /// 
    ///           </param>
    /// <param name="endNode">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node at which the selection ends.
    /// 
    ///           </param>
    public void SelectRange(TreeListNode startNode, TreeListNode endNode)
    {
      this.View.Do<TreeListView>((Action<TreeListView>) (view => view.SelectRangeCore(startNode, endNode)));
    }

    /// <summary>
    ///                 <para>Selects the specified node.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node to select.
    /// 
    ///           </param>
    public void SelectItem(TreeListNode node)
    {
      this.View.Do<TreeListView>((Action<TreeListView>) (view => view.SelectNodeCore(node)));
    }

    /// <summary>
    ///                 <para>Unselects the specified node.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node to unselect.
    /// 
    ///           </param>
    public void UnselectItem(TreeListNode node)
    {
      this.View.Do<TreeListView>((Action<TreeListView>) (view => view.UnselectNodeCore(node)));
    }

    /// <summary>
    ///                 <para>Returns selected nodes.
    /// </para>
    ///             </summary>
    /// <returns>An array of nodes currently selected within a View.</returns>
    public TreeListNode[] GetSelectedNodes()
    {
      int[] selectedRowHandles = this.GetSelectedRowHandles();
      TreeListNode[] treeListNodeArray = new TreeListNode[selectedRowHandles.Length];
      if (this.View == null)
        return treeListNodeArray;
      for (int index = 0; index < selectedRowHandles.Length; ++index)
        treeListNodeArray[index] = this.View.GetNodeByRowHandle(selectedRowHandles[index]);
      return treeListNodeArray;
    }

    protected void CanSelectAll(object sender, CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = this.View.IsMultiSelection;
    }

    public void CopyRowsToClipboard(IEnumerable<TreeListNode> nodes)
    {
      this.View.Do<TreeListView>((Action<TreeListView>) (view => view.CopyRowsToClipboardCore(nodes)));
    }

    /// <summary>
    ///                 <para>Copies the values displayed within the specified range of nodes, to the clipboard.
    /// </para>
    ///             </summary>
    /// <param name="startNode">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the first node in the range.
    /// 
    ///           </param>
    /// <param name="endNode">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the last node in the range.
    /// 
    ///           </param>
    public void CopyRangeToClipboard(TreeListNode startNode, TreeListNode endNode)
    {
      this.View.Do<TreeListView>((Action<TreeListView>) (view => view.CopyRangeToClipboardCore(startNode, endNode)));
    }

    protected internal override bool RaiseCopyingToClipboard(CopyingToClipboardEventArgsBase e)
    {
      e.RoutedEvent = TreeListControl.CopyingToClipboardEvent;
      this.RaiseEvent((RoutedEventArgs) e);
      return e.Handled;
    }

    protected void Copy()
    {
      this.CopyToClipboard();
    }

    protected void Paste()
    {
      if (this.RaisePastingFromClipboard())
        return;
      this.View.RaisePastingFromClipboard();
    }

    protected void CanCopy(object sender, CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = this.View.CanCopyRows();
    }

    protected void CanPaste(object sender, CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = true;
    }

    protected internal override bool GetAddNewColumns()
    {
      return DataControlSerializationOptions.GetAddNewColumns((DependencyObject) this);
    }

    protected internal override bool GetRemoveOldColumns()
    {
      return DataControlSerializationOptions.GetRemoveOldColumns((DependencyObject) this);
    }

    protected override string GetSerializationAppName()
    {
      return typeof (TreeListControl).Name;
    }

    protected override PeerCacheBase CreatePeerCache()
    {
      return (PeerCacheBase) new PeerCache();
    }

    protected override AutomationPeer OnCreateAutomationPeer()
    {
      if (this.AutomationPeer == null)
        this.AutomationPeer = (DataControlAutomationPeer) new TreeListControlAutomationPeer(this);
      return (AutomationPeer) this.AutomationPeer;
    }

    protected internal override void RaiseSelectionChanged(GridSelectionChangedEventArgs e)
    {
      e.RoutedEvent = TreeListControl.SelectionChangedEvent;
      this.RaiseEvent((RoutedEventArgs) e);
    }

    protected override IBandsCollection CreateBands()
    {
      return (IBandsCollection) new BandCollection<TreeListControlBand>();
    }

    protected override BandsLayoutBase CreateBandsLayout()
    {
      return (BandsLayoutBase) new TreeListControlBandsLayout();
    }

    protected override object GetItemsSource()
    {
      if (DesignerProperties.GetIsInDesignMode((DependencyObject) this))
        return GridControlHelper.GetDesignTimeSource((DataControlBase) this);
      return base.GetItemsSource();
    }
  }
}
