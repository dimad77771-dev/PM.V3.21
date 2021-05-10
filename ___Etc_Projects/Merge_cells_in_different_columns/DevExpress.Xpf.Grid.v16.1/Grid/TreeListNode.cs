// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListNode
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Grid.TreeList;
using DevExpress.Xpf.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>A node displayed within a TreeListView.
  /// </para>
  ///             </summary>
  public class TreeListNode : INotifyPropertyChanged
  {
    internal DefaultBoolean isExpandButtonVisible = DefaultBoolean.Default;
    private readonly TreeListNodeCollection nodes;
    protected bool isExpandedCore;
    private Binding expandStateBindingCore;
    private object content;
    protected internal TreeListNode parentNodeCore;
    internal int rowHandle;
    internal int visibleIndex;
    private ImageSource image;
    private bool? isChecked;
    private bool isCheckBoxEnabled;
    internal bool isCheckStateInitialized;
    internal Locker CheckBoxUpdateLocker;
    private TreeListNode.ExpandStateBindingEvaluator expandStateBindingEvaluatorCore;
    private TreeListDataProvider dataProviderCore;
    private DataTemplate itemTemplateCore;
    private DataTemplate templateCore;

    protected internal virtual TreeListDataProvider DataProvider
    {
      get
      {
        return this.dataProviderCore;
      }
      set
      {
        if (this.DataProvider == value)
          return;
        if (this.DataProvider != null)
          this.ReleaseExpandStateBindingEvaluator();
        this.dataProviderCore = value;
        if (this.DataProvider == null)
          return;
        this.UpdateExpandStateBindingEvaluator();
      }
    }

    /// <summary>
    ///                 <para>Gets the collection of child nodes.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNodeCollection" /> object that is the collection of child nodes.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListNodeNodes")]
    public TreeListNodeCollection Nodes
    {
      get
      {
        return this.nodes;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the node's content.
    /// </para>
    ///             </summary>
    /// <value>An object that specifies the node's content.</value>
    [DevExpressXpfGridLocalizedDescription("TreeListNodeContent")]
    public object Content
    {
      get
      {
        return this.content;
      }
      set
      {
        if (object.ReferenceEquals(this.Content, value))
          return;
        this.content = value;
        this.UpdateExpandStateBindingEvaluator();
        this.NotifyDataProvider(NodeChangeType.Content);
        this.RaisePropertyChanged("Content");
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the expand button is displayed within the node or not.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Utils.DefaultBoolean" /> enumeration value that specifies the expand button's visibility within the node.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListNodeIsExpandButtonVisible")]
    public DefaultBoolean IsExpandButtonVisible
    {
      get
      {
        return this.isExpandButtonVisible;
      }
      set
      {
        if (this.IsExpandButtonVisible == value)
          return;
        this.isExpandButtonVisible = value;
        this.NotifyDataProvider(NodeChangeType.ExpandButtonVisibility);
        this.RaisePropertyChanged("IsExpandButtonVisible");
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the node's image.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Media.ImageSource" /> object that is the image displayed within the node.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListNodeImage")]
    public ImageSource Image
    {
      get
      {
        return this.image;
      }
      set
      {
        if (this.Image == value)
          return;
        this.image = value;
        this.NotifyDataProvider(NodeChangeType.Image);
        this.RaisePropertyChanged("Image");
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the node is checked.
    /// </para>
    ///             </summary>
    /// <value>A Boolean value that specifies whether or not the node is checked.</value>
    [DevExpressXpfGridLocalizedDescription("TreeListNodeIsChecked")]
    public bool? IsChecked
    {
      get
      {
        return this.isChecked;
      }
      set
      {
        if (!this.SetNodeChecked(this, value))
          return;
        if (this.DataProvider != null && this.DataProvider.IsRecursiveCheckingAllowed(this))
          this.DataProvider.DoRecursiveCheckAction((Action) (() =>
          {
            this.RecursiveCheckChildren(this);
            this.RecursiveCheckParents(this);
          }));
        this.NotifyDataProvider(NodeChangeType.CheckBox);
        this.RaisePropertyChanged("IsChecked");
      }
    }

    /// <summary>
    ///                 <para>Indicates whether or not the node's check box is enabled.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, if the check box is enabled; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListNodeIsCheckBoxEnabled")]
    public bool IsCheckBoxEnabled
    {
      get
      {
        return this.isCheckBoxEnabled;
      }
      set
      {
        if (this.isCheckBoxEnabled == value)
          return;
        this.isCheckBoxEnabled = value;
        this.NotifyDataProvider(NodeChangeType.IsCheckBoxEnabled);
        this.RaisePropertyChanged("IsCheckBoxEnabled");
      }
    }

    /// <summary>
    ///                 <para>Gets whether or not the node has a child node(s).
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the node has a child node(s); otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListNodeHasChildren")]
    public bool HasChildren
    {
      get
      {
        return this.nodes.Count > 0;
      }
    }

    /// <summary>
    ///                 <para>Gets whether or not the node has a child node(s) displayed within a View.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the node has a child node(s) displayed within a View; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListNodeHasVisibleChildren")]
    public bool HasVisibleChildren
    {
      get
      {
        return this.nodes.GetFirstVisible() != null;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether or not the node is expanded.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to expand the node; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListNodeIsExpanded")]
    public virtual bool IsExpanded
    {
      get
      {
        return this.isExpandedCore;
      }
      set
      {
        if (this.IsExpanded == value || !this.IsTogglable)
          return;
        this.Expand(value);
        this.RaisePropertyChanged("IsExpanded");
      }
    }

    protected internal bool IsExpandedSetInternally { get; internal set; }

    /// <summary>
    ///                 <para>Gets or sets the binding that determines whether the node is expanded.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Data.Binding" /> object specifying whether the node is expanded.
    /// </value>
    public virtual Binding ExpandStateBinding
    {
      get
      {
        return this.expandStateBindingCore;
      }
      set
      {
        if (this.ExpandStateBinding == value)
          return;
        this.expandStateBindingCore = value;
        this.UpdateExpandStateBindingEvaluator();
        this.ForceUpdateExpandState();
      }
    }

    /// <summary>
    ///                 <para>Gets the parent node.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the parent node. <b>null</b> (<b>Nothing</b> in Visual Basic) if the current node is at the root level.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListNodeParentNode")]
    public TreeListNode ParentNode
    {
      get
      {
        if (this.parentNodeCore is RootTreeListNode)
          return (TreeListNode) null;
        return this.parentNodeCore;
      }
      internal set
      {
        this.parentNodeCore = value;
      }
    }

    protected internal TreeListNode RootNode
    {
      get
      {
        TreeListNode treeListNode = this;
        while (treeListNode.ParentNode != null)
          treeListNode = treeListNode.ParentNode;
        return treeListNode;
      }
    }

    /// <summary>
    ///                 <para>Gets the node's visible parent.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node's visible parent.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListNodeVisibleParent")]
    public TreeListNode VisibleParent
    {
      get
      {
        TreeListNode parentNode = this.ParentNode;
        while (parentNode != null && !parentNode.IsVisible)
          parentNode = parentNode.ParentNode;
        return parentNode;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the data associated with the node.
    /// </para>
    ///             </summary>
    /// <value>An object that contains information associated with the current node.</value>
    [DevExpressXpfGridLocalizedDescription("TreeListNodeTag")]
    public object Tag { get; set; }

    /// <summary>
    ///                 <para>Gets the node's nesting level.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the node's nesting level.</value>
    [DevExpressXpfGridLocalizedDescription("TreeListNodeLevel")]
    public int Level
    {
      get
      {
        if (this.ParentNode == null)
          return 0;
        return this.ParentNode.Level + 1;
      }
    }

    /// <summary>
    ///                 <para>Gets the node's actual nesting level.
    /// </para>
    ///             </summary>
    /// <value>An integer value that is the node's actual nesting level.</value>
    public int ActualLevel
    {
      get
      {
        if (!this.IsVisible)
          return -1;
        int level = this.Level;
        for (TreeListNode parentNode = this.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
        {
          if (!parentNode.IsVisible)
            --level;
        }
        return level;
      }
    }

    /// <summary>
    ///                 <para>Gets whether or not the node is the first node within a collection of nodes.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the node is the first node within the collection; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListNodeIsFirst")]
    public bool IsFirst
    {
      get
      {
        if (this.parentNodeCore == null)
          return true;
        return this.parentNodeCore.Nodes[0] == this;
      }
    }

    internal bool IsFirstVisible
    {
      get
      {
        if (this.parentNodeCore == null)
          return true;
        if (this.VisibleParent == null && this.DataProvider != null)
          return this.DataProvider.RootNode.Nodes.GetFirstVisible() == this;
        return this.VisibleParent.Nodes.GetFirstVisible() == this;
      }
    }

    /// <summary>
    ///                 <para>Gets whether or not the node is the last node within a collection of nodes.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the node is the last node within the collection; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListNodeIsLast")]
    public bool IsLast
    {
      get
      {
        if (this.ParentNode == null)
          return true;
        TreeListNodeCollection nodes = this.parentNodeCore.Nodes;
        return nodes[nodes.Count - 1] == this;
      }
    }

    internal bool IsLastVisible
    {
      get
      {
        if (this.parentNodeCore == null)
          return true;
        if (this.VisibleParent == null && this.DataProvider != null)
          return this.DataProvider.RootNode.Nodes.GetLastVisible() == this;
        return this.VisibleParent.Nodes.GetLastVisible() == this;
      }
    }

    /// <summary>
    ///                 <para>Gets the row handle that identifies the node.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the row handle.</value>
    [DevExpressXpfGridLocalizedDescription("TreeListNodeRowHandle")]
    public int RowHandle
    {
      get
      {
        if (this.DataProvider != null)
          return this.DataProvider.GetRowHandleByNode(this);
        return this.rowHandle;
      }
    }

    /// <summary>
    ///                 <para>Gets whether the node is filtered or not.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> the node is filtered; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListNodeIsFiltered")]
    public bool IsFiltered
    {
      get
      {
        return !this.IsVisible;
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public IList ItemsSource { get; internal set; }

    protected internal bool IsVisible { get; internal set; }

    internal int Id { get; set; }

    internal DataTemplate ItemTemplate
    {
      get
      {
        return this.itemTemplateCore;
      }
      set
      {
        if (this.ItemTemplate == value)
          return;
        this.itemTemplateCore = value;
        this.RaisePropertyChanged("ItemTemplate");
      }
    }

    internal DataTemplate Template
    {
      get
      {
        return this.templateCore;
      }
      set
      {
        if (this.Template == value)
          return;
        this.templateCore = value;
        this.RaisePropertyChanged("Template");
      }
    }

    internal int VisibleIndex
    {
      get
      {
        if (this.DataProvider != null)
          return this.DataProvider.GetVisibleIndexByNode(this);
        return this.visibleIndex;
      }
    }

    protected internal bool IsTogglable
    {
      get
      {
        if (this.isExpandButtonVisible == DefaultBoolean.Default)
          return this.HasVisibleChildren;
        return true;
      }
    }

    internal bool ChildrenWereEverFetched { get; set; }

    /// <summary>
    ///                 <para>Occurs every time any of the <see cref="P:DevExpress.Xpf.Grid.TreeListNode.Content" />, <see cref="P:DevExpress.Xpf.Grid.TreeListNode.IsExpandButtonVisible" />, <see cref="P:DevExpress.Xpf.Grid.TreeListNode.Image" />, <see cref="P:DevExpress.Xpf.Grid.TreeListNode.IsChecked" />, <see cref="P:DevExpress.Xpf.Grid.TreeListNode.IsCheckBoxEnabled" />, and <see cref="P:DevExpress.Xpf.Grid.TreeListNode.IsExpanded" /> properties have changed their value.
    /// 
    /// </para>
    ///             </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListNode class.
    /// </para>
    ///             </summary>
    public TreeListNode()
    {
      this.nodes = this.CreateNodeCollection();
      this.IsVisible = true;
      this.Id = this.visibleIndex = -1;
      this.rowHandle = int.MinValue;
      this.isChecked = new bool?(false);
      this.isCheckBoxEnabled = true;
      this.isCheckStateInitialized = false;
      this.CheckBoxUpdateLocker = new Locker();
      this.ChildrenWereEverFetched = false;
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListNode class with the specified content.
    /// </para>
    ///             </summary>
    /// <param name="content">
    /// An object that specifies the node's content. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeListNode.Content" /> property.
    /// 
    ///           </param>
    public TreeListNode(object content)
      : this()
    {
      this.Content = content;
    }

    protected virtual TreeListNodeCollection CreateNodeCollection()
    {
      return new TreeListNodeCollection(this);
    }

    internal void ReleaseExpandStateBindingEvaluator()
    {
      if (this.expandStateBindingEvaluatorCore == null)
        return;
      this.expandStateBindingEvaluatorCore.Release();
      this.expandStateBindingEvaluatorCore = (TreeListNode.ExpandStateBindingEvaluator) null;
    }

    internal void UpdateExpandStateBindingEvaluator()
    {
      this.ReleaseExpandStateBindingEvaluator();
      if (this.Content == null || this.DataProvider == null || this.ExpandStateBinding == null && this.DataProvider.View.ExpandStateBinding == null && string.IsNullOrEmpty(this.DataProvider.View.ExpandStateFieldName) || this.DataProvider.View.IsDesignTime)
        return;
      this.expandStateBindingEvaluatorCore = new TreeListNode.ExpandStateBindingEvaluator(this, this.DataProvider.View.ExpandStateFieldName, this.ExpandStateBinding ?? this.DataProvider.View.ExpandStateBinding);
    }

    internal void Expand(bool isExpanded)
    {
      if (this.isExpandedCore == isExpanded)
        return;
      if (this.DataProvider != null)
      {
        if (!this.DataProvider.OnNodeExpandingOrCollapsing(this))
          return;
        this.isExpandedCore = isExpanded;
        if (this.expandStateBindingEvaluatorCore != null && this.expandStateBindingEvaluatorCore.IsTwoWayBinding)
          this.expandStateBindingEvaluatorCore.SetExpanded(this.isExpandedCore);
        if (this.DataProvider != null)
          this.DataProvider.OnNodeExpandedOrCollapsed(this);
        this.NotifyDataProvider(NodeChangeType.Expand);
      }
      else
        this.isExpandedCore = isExpanded;
    }

    /// <summary>
    ///                 <para>Expands all child nodes.
    /// </para>
    ///             </summary>
    public void ExpandAll()
    {
      this.ToggleExpandedAllChildren(true);
    }

    /// <summary>
    ///                 <para>Collapses all child nodes.
    /// </para>
    ///             </summary>
    public void CollapseAll()
    {
      this.ToggleExpandedAllChildren(false);
    }

    private void RecursiveCheckParents(TreeListNode node)
    {
      TreeListNode parentNode = node.ParentNode;
      if (parentNode == null)
        return;
      bool? checkStatus = parentNode.Nodes[0].IsChecked;
      foreach (TreeListNode node1 in (Collection<TreeListNode>) parentNode.Nodes)
      {
        bool? nullable1 = node1.isChecked;
        bool? nullable2 = checkStatus;
        if ((nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : (nullable1.HasValue != nullable2.HasValue ? 1 : 0)) != 0)
        {
          checkStatus = new bool?();
          break;
        }
      }
      if (!this.SetNodeChecked(parentNode, checkStatus))
        return;
      this.RecursiveCheckParents(parentNode);
    }

    private void RecursiveCheckChildren(TreeListNode node)
    {
      foreach (TreeListNode node1 in (IEnumerable<TreeListNode>) new TreeListNodeIterator(node))
        this.SetNodeChecked(node1, node.isChecked);
    }

    internal bool SetNodeChecked(TreeListNode node, bool? checkStatus)
    {
      bool? nullable1 = node.isChecked;
      bool? nullable2 = checkStatus;
      if ((nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 0 : (nullable1.HasValue == nullable2.HasValue ? 1 : 0)) != 0)
        return false;
      node.isChecked = checkStatus;
      TreeListDataProvider dataProvider = this.DataProvider;
      if (dataProvider != null && !dataProvider.View.IsDesignTime && !dataProvider.DataHelper.IsLoading)
      {
        dataProvider.SetObjectIsChecked(node, checkStatus);
        if (this.isCheckStateInitialized)
          dataProvider.OnNodeCheckStateChanged(node);
      }
      return true;
    }

    internal void InitIsChecked()
    {
      if (this.DataProvider == null)
        return;
      this.isCheckStateInitialized = false;
      this.IsChecked = this.DataProvider.GetObjectIsChecked(this);
      this.isCheckStateInitialized = true;
    }

    internal void UpdateNodeChecked(bool? checkStatus)
    {
      bool? isChecked = this.IsChecked;
      bool? nullable = checkStatus;
      if ((isChecked.GetValueOrDefault() != nullable.GetValueOrDefault() ? 0 : (isChecked.HasValue == nullable.HasValue ? 1 : 0)) != 0)
        this.isCheckStateInitialized = true;
      else if (this.DataProvider != null && this.DataProvider.IsRecursiveCheckingAllowed(this))
        this.IsChecked = checkStatus;
      else
        this.CheckBoxUpdateLocker.DoLockedAction((Action) (() => this.IsChecked = checkStatus));
    }

    private void ToggleExpandedAllChildren(bool expand)
    {
      if (this.DataProvider != null)
        this.DataProvider.ToggleExpandedAllChildNodes(this, expand);
      else
        this.ToggleExpandedAllChildrenCore(expand);
    }

    internal void ToggleExpandedAllChildrenCore(bool expand)
    {
      this.ProcessNodeAndDescendantsAction((Func<TreeListNode, bool>) (node =>
      {
        node.IsExpanded = expand;
        return true;
      }));
    }

    protected void NotifyDataProvider(NodeChangeType nodeChangeType)
    {
      if (this.DataProvider == null)
        return;
      this.DataProvider.OnNodeCollectionChanged(this, nodeChangeType, true, (string) null);
    }

    /// <summary>
    ///                 <para>Indicates whether the current node belongs to the specified branch node.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// The <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object.
    /// 
    ///           </param>
    /// <returns><b>true</b> if the specified branch node contains the current node; otherwise, <b>false</b>.
    /// </returns>
    [Browsable(false)]
    public bool IsDescendantOf(TreeListNode node)
    {
      for (TreeListNode parentNode = this.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
      {
        if (object.ReferenceEquals((object) parentNode, (object) node))
          return true;
      }
      return false;
    }

    internal void UpdateId()
    {
      if (this.DataProvider == null)
        return;
      this.DataProvider.UpdateNodeId(this);
    }

    internal void ProcessNodeAndDescendantsAction(Func<TreeListNode, bool> action)
    {
      TreeListNode.ProcessNodeAction(this, action);
    }

    internal static void ProcessNodeAction(TreeListNode node, Func<TreeListNode, bool> action)
    {
      if (!action(node))
        return;
      foreach (TreeListNode node1 in (Collection<TreeListNode>) node.Nodes)
        TreeListNode.ProcessNodeAction(node1, action);
    }

    private void RaisePropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    internal void ForceUpdateExpandState()
    {
      if (this.expandStateBindingEvaluatorCore == null || !this.IsTogglable)
        return;
      this.expandStateBindingEvaluatorCore.UpdateExpandState();
    }

    public class ExpandStateBindingEvaluator
    {
      protected TreeListNode Node { get; private set; }

      protected TreeListNode.ExpandStateBindingEvaluator.IExpandStateBindingStrategy EvaluatorStrategy { get; private set; }

      public bool IsTwoWayBinding
      {
        get
        {
          return this.EvaluatorStrategy.IsTwoWayBinding;
        }
      }

      public ExpandStateBindingEvaluator(TreeListNode node, string expandStateFieldName, Binding expandStateBinding)
      {
        this.Node = node;
        this.EvaluatorStrategy = this.CreateEvaluatorStrategy(expandStateFieldName, expandStateBinding);
      }

      protected virtual TreeListNode.ExpandStateBindingEvaluator.IExpandStateBindingStrategy CreateEvaluatorStrategy(string expandStateFieldName, Binding expandStateBinding)
      {
        if (expandStateBinding == null && !string.IsNullOrEmpty(expandStateFieldName))
          return (TreeListNode.ExpandStateBindingEvaluator.IExpandStateBindingStrategy) new TreeListNode.ExpandStateBindingEvaluator.DataFieldExpandStateBindingStrategy(this, expandStateFieldName, true);
        if (string.IsNullOrEmpty(expandStateFieldName) && this.IsSimpleBinding(expandStateBinding))
          return (TreeListNode.ExpandStateBindingEvaluator.IExpandStateBindingStrategy) new TreeListNode.ExpandStateBindingEvaluator.DataFieldExpandStateBindingStrategy(this, expandStateBinding.Path.Path, expandStateBinding.Mode == BindingMode.TwoWay);
        return (TreeListNode.ExpandStateBindingEvaluator.IExpandStateBindingStrategy) new TreeListNode.ExpandStateBindingEvaluator.BindingExpandStateBindingStrategy(this, BindingCloneHelper.Clone((BindingBase) expandStateBinding, this.Node.Content) as Binding);
      }

      protected virtual bool IsSimpleBinding(Binding expandStateBinding)
      {
        if (expandStateBinding.Path == null || !SimpleBindingProcessor.IsFieldValid(expandStateBinding.Path.Path) || expandStateBinding.Converter != null)
          return false;
        return SimpleBindingProcessor.ValidateBindingProperties(expandStateBinding);
      }

      public void SetExpanded(bool value)
      {
        this.EvaluatorStrategy.SetExpanded(value);
      }

      public void UpdateExpandState()
      {
        this.EvaluatorStrategy.UpdateExpandState();
      }

      public void Release()
      {
        this.EvaluatorStrategy.Release();
        this.Node = (TreeListNode) null;
      }

      public interface IExpandStateBindingStrategy
      {
        bool IsTwoWayBinding { get; }

        void UpdateExpandState();

        void SetExpanded(bool value);

        void Release();
      }

      public class DataFieldExpandStateBindingStrategy : TreeListNode.ExpandStateBindingEvaluator.IExpandStateBindingStrategy
      {
        private bool updating;
        private bool isTwoWayBinding;

        public bool IsTwoWayBinding
        {
          get
          {
            return this.isTwoWayBinding;
          }
        }

        protected TreeListNode.ExpandStateBindingEvaluator Evaluator { get; private set; }

        protected string FieldName { get; private set; }

        protected TreeListNode Node
        {
          get
          {
            return this.Evaluator.Node;
          }
        }

        protected TreeListDataProvider Provider
        {
          get
          {
            return this.Node.DataProvider;
          }
        }

        public DataFieldExpandStateBindingStrategy(TreeListNode.ExpandStateBindingEvaluator evaluator, string expandStateFieldName, bool userTwoWayBinding = true)
        {
          this.Evaluator = evaluator;
          this.FieldName = expandStateFieldName;
          if (this.Provider.Columns[this.FieldName] == null)
            return;
          this.isTwoWayBinding = !this.Provider.Columns[this.FieldName].ReadOnly && userTwoWayBinding;
        }

        public void Release()
        {
        }

        public void UpdateExpandState()
        {
          if (this.updating)
            return;
          this.updating = true;
          try
          {
            object obj = this.Provider.DataHelper.GetValue(this.Node, this.FieldName);
            if (obj == null || obj == DBNull.Value)
              return;
            this.Node.IsExpanded = (bool) Convert.ChangeType(obj, typeof (bool));
          }
          catch
          {
          }
          finally
          {
            this.updating = false;
          }
        }

        public void SetExpanded(bool value)
        {
          if (this.updating)
            return;
          this.Provider.DataHelper.SetValue(this.Node, this.FieldName, (object) value);
        }
      }

      public class BindingExpandStateBindingStrategy : DependencyObject, TreeListNode.ExpandStateBindingEvaluator.IExpandStateBindingStrategy
      {
        private static readonly DependencyProperty IsExpandedProperty = DependencyPropertyManager.Register("IsExpaned", typeof (bool), typeof (TreeListNode.ExpandStateBindingEvaluator.BindingExpandStateBindingStrategy), (PropertyMetadata) new FrameworkPropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((TreeListNode.ExpandStateBindingEvaluator.BindingExpandStateBindingStrategy) d).UpdateExpandState())));

        protected TreeListNode.ExpandStateBindingEvaluator Evaluator { get; private set; }

        protected TreeListNode Node
        {
          get
          {
            return this.Evaluator.Node;
          }
        }

        protected Binding Binding { get; private set; }

        public bool IsTwoWayBinding
        {
          get
          {
            return this.Binding.Mode == BindingMode.TwoWay;
          }
        }

        public bool IsExpanded
        {
          get
          {
            return (bool) this.GetValue(TreeListNode.ExpandStateBindingEvaluator.BindingExpandStateBindingStrategy.IsExpandedProperty);
          }
          set
          {
            this.SetValue(TreeListNode.ExpandStateBindingEvaluator.BindingExpandStateBindingStrategy.IsExpandedProperty, (object) value);
          }
        }

        public BindingExpandStateBindingStrategy(TreeListNode.ExpandStateBindingEvaluator evaluator, Binding expandStateBinding)
        {
          this.Evaluator = evaluator;
          this.Binding = expandStateBinding;
          BindingOperations.SetBinding((DependencyObject) this, TreeListNode.ExpandStateBindingEvaluator.BindingExpandStateBindingStrategy.IsExpandedProperty, (BindingBase) this.Binding);
        }

        public void Release()
        {
          BindingOperations.ClearBinding((DependencyObject) this, TreeListNode.ExpandStateBindingEvaluator.BindingExpandStateBindingStrategy.IsExpandedProperty);
          this.Binding = (Binding) null;
        }

        public void SetExpanded(bool value)
        {
          this.IsExpanded = value;
        }

        public void UpdateExpandState()
        {
          this.Node.IsExpanded = this.IsExpanded;
        }
      }
    }
  }
}
