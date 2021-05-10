// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListHierarchicalDataHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.GridData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListHierarchicalDataHelper : TreeListBoundDataHelper
  {
    public readonly int MaxNodeId = 1073741823;
    private Dictionary<object, TreeListNodeCollection> notificationDictionary;
    private Dictionary<TreeListNodeCollection, object> mirrorNotificationDictionary;

    protected internal int NodeCounter { get; set; }

    protected internal Dictionary<object, TreeListNodeCollection> NotificationDictionary
    {
      get
      {
        if (this.notificationDictionary == null)
          this.notificationDictionary = new Dictionary<object, TreeListNodeCollection>();
        return this.notificationDictionary;
      }
    }

    protected internal Dictionary<TreeListNodeCollection, object> MirrorNotificationDictionary
    {
      get
      {
        if (this.mirrorNotificationDictionary == null)
          this.mirrorNotificationDictionary = new Dictionary<TreeListNodeCollection, object>();
        return this.mirrorNotificationDictionary;
      }
    }

    protected bool CreateAllNodes
    {
      get
      {
        return !this.View.EnableDynamicLoading;
      }
    }

    public TreeListHierarchicalDataHelper(TreeListDataProvider provider, object dataSource)
      : base(provider, dataSource)
    {
    }

    public override void DeleteNode(TreeListNode node, bool deleteChildren, bool modifySource)
    {
      if (node == null)
        return;
      if (modifySource)
      {
        TreeListNode parentNode = node.ParentNode;
        IList list = parentNode != null ? parentNode.ItemsSource : this.ListSource;
        if (list != null)
          list.Remove(node.Content);
      }
      this.RemoveTreeListNode(node);
    }

    private void ClearNotifications(List<TreeListNode> nodes)
    {
      foreach (TreeListNode node in nodes)
      {
        this.ClearNotifications(node.Nodes);
        this.ClearNodeNotification(node);
      }
    }

    private void ClearNotifications(TreeListNodeCollection nodes)
    {
      this.ClearNotifications(nodes, true);
    }

    private void ClearNotifications(TreeListNode node, bool recursive)
    {
      this.ClearNodeNotification(node);
      if (!recursive)
        return;
      this.ClearNotifications(node.Nodes, recursive);
    }

    private void ClearNotifications(TreeListNodeCollection nodes, bool recursive)
    {
      if (recursive)
      {
        foreach (TreeListNode node in (IEnumerable<TreeListNode>) new TreeListNodeIterator(nodes))
          this.ClearNodeNotification(node);
      }
      else
      {
        foreach (TreeListNode node in (Collection<TreeListNode>) nodes)
          this.ClearNodeNotification(node);
      }
    }

    private void UpdateNodeChildren(TreeListNodeCollection nodes, object s, ListChangedEventArgs e)
    {
      IBindingList list = s as IBindingList;
      if (list == null)
        return;
      Dispatcher dispatcher = this.View.Dispatcher;
      if (dispatcher == null || dispatcher.CheckAccess())
        this.UpdateNodeChildrenCore(nodes, e, list);
      else
        dispatcher.BeginInvoke((Delegate) (() => this.UpdateNodeChildrenCore(nodes, e, list)));
      nodes.Owner.IsExpandButtonVisible = DefaultBoolean.Default;
    }

    private void UpdateNodeChildrenCore(TreeListNodeCollection nodes, ListChangedEventArgs e, IBindingList list)
    {
      if (e.ListChangedType == ListChangedType.Reset)
        this.OnListReset(nodes, list);
      else
        this.OnListChanged(nodes, list, e);
    }

    private void OnListChanged(TreeListNodeCollection nodes, IBindingList list, ListChangedEventArgs e)
    {
      switch (e.ListChangedType)
      {
        case ListChangedType.ItemAdded:
          this.OnItemAdded(nodes, list, e.NewIndex);
          break;
        case ListChangedType.ItemDeleted:
          this.OnItemDeleted(nodes, list, e);
          break;
        case ListChangedType.ItemMoved:
          this.OnItemMoved(nodes, e);
          break;
        case ListChangedType.ItemChanged:
          this.OnItemChanged(nodes, list, e);
          break;
      }
    }

    protected virtual void OnItemChanged(TreeListNodeCollection nodes, IBindingList list, ListChangedEventArgs e)
    {
      PropertyDescriptor propertyDescriptor = e.PropertyDescriptor;
      if (propertyDescriptor == null)
      {
        if (e.OldIndex != e.NewIndex)
          this.OnListReset(nodes, list);
      }
      else if (this.View != null && propertyDescriptor.Name == this.View.CheckBoxFieldName && this.DataProvider.IsRecursiveCheckInProgress)
        return;
      IList<TreeListNode> treeListNodeList = (IList<TreeListNode>) nodes.Where<TreeListNode>((Func<TreeListNode, bool>) (x => object.Equals(list[e.NewIndex], x.Content))).ToList<TreeListNode>();
      if (treeListNodeList.Count == 0)
      {
        this.OnListReset(nodes, list);
        treeListNodeList = this.DataProvider.FindNodesByValue(list[e.NewIndex]);
      }
      foreach (TreeListNode node in (IEnumerable<TreeListNode>) treeListNodeList)
      {
        if (node != this.DataProvider.CurrentlyCheckingNode)
          node.IsChecked = this.DataProvider.GetObjectIsChecked(node);
        if (!this.IsLoading)
          node.ForceUpdateExpandState();
        this.UpdateChildren(node, e);
        this.DataProvider.OnNodeCollectionChanged(node, NodeChangeType.Content, true, propertyDescriptor != null ? propertyDescriptor.Name : (string) null);
      }
    }

    private void UpdateChildren(TreeListNode node, ListChangedEventArgs e)
    {
      if (!this.View.AllowChildNodeSourceUpdates || !node.ChildrenWereEverFetched)
        return;
      if (this.View.TreeDerivationMode == TreeDerivationMode.ChildNodesSelector && this.View.ChildNodesSelector == null && (e.PropertyDescriptor != null && e.PropertyDescriptor.Name == this.View.ChildNodesPath))
      {
        this.ReloadChildNodes(node, (IEnumerable) null);
      }
      else
      {
        IEnumerable children = this.GetChildren(node);
        if (object.ReferenceEquals((object) children, (object) node.ItemsSource))
          return;
        this.ReloadChildNodes(node, children);
      }
    }

    public override void ReloadChildNodes(TreeListNode node, IEnumerable children = null)
    {
      if (node == null)
        return;
      this.ClearNotifications(node, true);
      this.DataProvider.SaveFocusState();
      this.DataProvider.BeginUpdateCore();
      try
      {
        node.Nodes.Clear();
        this.CreateChildren(node, children);
        if (node.IsExpanded)
        {
          if (!this.View.FetchSublevelChildrenOnExpand)
          {
            if (!this.CreateAllNodes)
              goto label_8;
          }
          this.FillChildrenSubNodes(node, this.CreateAllNodes);
        }
      }
      finally
      {
        this.DataProvider.CancelUpdate();
        this.DataProvider.RestoreFocusState();
        this.UpdateNodes(node);
      }
label_8:
      this.DataProvider.OnNodeCollectionChanged(node, NodeChangeType.Expand, true, (string) null);
    }

    protected virtual void OnItemAdded(TreeListNodeCollection nodes, IBindingList list, int index)
    {
      TreeListNode node = this.DataProvider.CreateNode(list[index]);
      node.DataProvider = this.DataProvider;
      node.InitIsChecked();
      this.DataProvider.SaveFocusState();
      this.DataProvider.BeginUpdateCore();
      try
      {
        nodes.Insert(index, node);
        this.AddNode(nodes, node);
      }
      finally
      {
        this.DataProvider.CancelUpdate();
        this.DataProvider.RestoreFocusState();
      }
      this.DataProvider.OnNodeCollectionChanged(node, NodeChangeType.Add, false, (string) null);
    }

    protected virtual void OnItemMoved(TreeListNodeCollection nodes, ListChangedEventArgs e)
    {
      this.DataProvider.BeginUpdateCore();
      this.DataProvider.SaveNodesState(false);
      TreeListNode node = nodes[e.OldIndex];
      try
      {
        nodes.Remove(node);
        nodes.Insert(e.NewIndex, node);
      }
      finally
      {
        this.DataProvider.RestoreNodesState(false);
        this.DataProvider.CancelUpdate();
      }
      this.DataProvider.OnNodeCollectionChanged(node, NodeChangeType.Add, false, (string) null);
    }

    protected internal override void UpdateNodeId(TreeListNode node)
    {
      node.Id = this.NodeCounter++;
    }

    protected override void CalcNodeIds()
    {
      this.NodeCounter = 0;
      foreach (TreeListNode treeListNode in (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.DataProvider.Nodes))
        treeListNode.Id = this.NodeCounter++;
    }

    protected internal override void RecalcNodeIdsIfNeeded()
    {
      if (this.NodeCounter < this.MaxNodeId)
        return;
      this.CalcNodeIds();
    }

    protected virtual void OnItemDeleted(TreeListNodeCollection nodes, IBindingList list, ListChangedEventArgs e)
    {
      this.DataProvider.SaveFocusState();
      this.DataProvider.BeginUpdateCore();
      List<TreeListNode> oldNodes;
      try
      {
        oldNodes = this.GetOldNodes(nodes, list, e);
        this.ClearNotifications(oldNodes);
        this.RemoveOldNodes(nodes, (IEnumerable) oldNodes);
      }
      finally
      {
        this.DataProvider.CancelUpdate();
        this.DataProvider.RestoreFocusState();
      }
      this.DataProvider.OnNodeCollectionChanged(oldNodes.Count > 0 ? oldNodes[0] : (TreeListNode) null, NodeChangeType.Remove, false, (string) null);
    }

    protected virtual void OnListReset(TreeListNodeCollection nodes, IBindingList list)
    {
      this.DataProvider.BeginUpdate();
      try
      {
        this.ClearNotifications(nodes, true);
        nodes.Clear();
        this.AddNewNodes(nodes, (IEnumerable) list);
      }
      finally
      {
        this.DataProvider.EndUpdate();
      }
    }

    private List<TreeListNode> GetOldNodes(TreeListNodeCollection treeListNodeCollection, IBindingList list, ListChangedEventArgs e)
    {
      List<TreeListNode> treeListNodeList = new List<TreeListNode>();
      ArrayList arrayList = new ArrayList((ICollection) list);
      foreach (TreeListNode treeListNode in (Collection<TreeListNode>) treeListNodeCollection)
      {
        if (arrayList.Count > 0 && arrayList.Contains(treeListNode.Content))
          arrayList.Remove(treeListNode.Content);
        else
          treeListNodeList.Add(treeListNode);
      }
      return treeListNodeList;
    }

    private void AddNewNodes(TreeListNodeCollection nodes, IEnumerable list)
    {
      foreach (object obj in list)
        this.AddNewNode(nodes, obj);
    }

    private void AddNewNode(TreeListNodeCollection nodes, object item)
    {
      TreeListNode node = this.DataProvider.CreateNode(item);
      nodes.Add(node);
      this.AddNode(nodes, node);
    }

    protected virtual void AddNode(TreeListNodeCollection nodes, TreeListNode node)
    {
      if (this.View.FetchSublevelChildrenOnExpand || this.CreateAllNodes)
      {
        this.AddNotification(node.Nodes, this.GetChildren(node));
        this.FillSubNodes(node);
        if (!this.CreateAllNodes)
          return;
        this.FillChildrenSubNodes(node, this.CreateAllNodes);
      }
      else
        node.IsExpandButtonVisible = DefaultBoolean.True;
    }

    public override void Dispose()
    {
      this.ResetNotifications();
      base.Dispose();
      this.DataProvider.Nodes.Clear();
    }

    private void RemoveOldNodes(TreeListNodeCollection nodes, IEnumerable oldNodes)
    {
      foreach (TreeListNode oldNode in oldNodes)
        nodes.Remove(oldNode);
    }

    protected virtual IEnumerable GetChildren(TreeListNode node)
    {
      if (node == null)
        return (IEnumerable) null;
      object content = node.Content;
      if (content == null)
        return (IEnumerable) null;
      if (this.View.ChildNodesSelector != null)
      {
        IEnumerable enumerable = this.View.ChildNodesSelector.SelectChildren(content);
        if (enumerable != null)
          return enumerable;
      }
      if (string.IsNullOrEmpty(this.View.ChildNodesPath))
        return (IEnumerable) null;
      PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(content).Find(this.View.ChildNodesPath, false);
      if (propertyDescriptor == null)
        return (IEnumerable) null;
      return propertyDescriptor.GetValue(content) as IEnumerable;
    }

    public override void LoadData()
    {
      this.ResetNotifications();
      this.DataProvider.Nodes.Clear();
      if (this.ListSource == null)
        return;
      this.AddNodesFromItems();
      if (!this.View.FetchSublevelChildrenOnExpand && !this.CreateAllNodes)
        return;
      this.FillChildrenSubNodes(this.DataProvider.RootNode, this.CreateAllNodes);
    }

    public void ResetNotifications()
    {
      foreach (KeyValuePair<object, TreeListNodeCollection> notification in this.NotificationDictionary)
      {
        IBindingList bindingList = notification.Key as IBindingList;
        if (bindingList != null)
          bindingList.ListChanged -= new ListChangedEventHandler(this.OnBindingListChanged);
      }
      this.NotificationDictionary.Clear();
      this.MirrorNotificationDictionary.Clear();
    }

    private void AddNodesFromItems()
    {
      this.AddNodesFromItems(this.DataProvider.RootNode, (IEnumerable) this.ListSource);
      this.AddNotification(this.DataProvider.Nodes, (IEnumerable) this.ListSource);
    }

    private void AddNodesFromItems(TreeListNode treeListNode, IEnumerable children)
    {
      this.BeginLoad();
      this.DataProvider.BeginUpdateCore();
      try
      {
        TreeListNodeCollection nodes = treeListNode.Nodes;
        foreach (object child in children)
        {
          TreeListNode node = this.DataProvider.CreateNode(child);
          node.DataProvider = this.DataProvider;
          node.InitIsChecked();
          node.IsExpandButtonVisible = DefaultBoolean.True;
          node.Template = this.GetItemTemplate(treeListNode);
          nodes.Add(node);
          if (node.Template == null)
            this.AsignImplicitDataTemplate(node);
        }
      }
      finally
      {
        this.DataProvider.CancelUpdate();
        this.UpdateNodes(treeListNode);
        this.EndLoad();
        if (!this.IsLoading && (this.View.ExpandStateBinding != null || !string.IsNullOrEmpty(this.View.ExpandStateFieldName)))
          this.DataProvider.UpdateNodesExpandState(treeListNode.Nodes, false);
      }
    }

    protected virtual DataTemplate GetItemTemplate(TreeListNode treeListNode)
    {
      return (DataTemplate) null;
    }

    protected virtual void AsignImplicitDataTemplate(TreeListNode treeListNode)
    {
    }

    protected override PropertyDescriptor GetActualComplexPropertyDescriptor(TreeListDataProvider provider, object obj, string name)
    {
      if (provider.DataSource is ITypedList)
        return (PropertyDescriptor) new TreeListComplexPropertyDescriptor(provider, obj, name);
      return (PropertyDescriptor) new UnitypeComplexPropertyDescriptor(provider, obj, name);
    }

    protected override PropertyDescriptor GetActualPropertyDescriptor(PropertyDescriptor descriptor)
    {
      if (this.DataProvider.DataSource is ITypedList)
        return descriptor;
      return (PropertyDescriptor) new UnitypeDataPropertyDescriptor(descriptor, this.View.AutoDetectColumnTypeInHierarchicalMode);
    }

    private bool CreateChildren(TreeListNode node, IEnumerable children = null)
    {
      if (children == null)
      {
        children = this.TryGetChildrenFromNoteDictionary(node);
        if (children == null)
          children = this.GetChildren(node);
        if (children == null)
          return false;
      }
      this.AddNotification(node.Nodes, children);
      this.AddNodesFromItems(node, children);
      node.ItemsSource = children as IList;
      IEnumerator enumerator = children.GetEnumerator();
      try
      {
        if (enumerator.MoveNext())
        {
          object current = enumerator.Current;
          return true;
        }
      }
      finally
      {
        IDisposable disposable = enumerator as IDisposable;
        if (disposable != null)
          disposable.Dispose();
      }
      return false;
    }

    private IEnumerable TryGetChildrenFromNoteDictionary(TreeListNode node)
    {
      KeyValuePair<object, TreeListNodeCollection> keyValuePair = new KeyValuePair<object, TreeListNodeCollection>((object) null, (TreeListNodeCollection) null);
      if (this.MirrorNotificationDictionary.ContainsKey(node.Nodes))
        return this.MirrorNotificationDictionary[node.Nodes] as IEnumerable;
      return (IEnumerable) null;
    }

    private void AddNotification(TreeListNodeCollection nodes, IEnumerable list)
    {
      if (list == null || this.NotificationDictionary.ContainsKey((object) list) || this.MirrorNotificationDictionary.ContainsKey(nodes))
        return;
      IBindingList bindingList = this.ExtractListSource((object) list) as IBindingList;
      if (bindingList == null)
        return;
      this.NotificationDictionary.Add((object) bindingList, nodes);
      this.MirrorNotificationDictionary.Add(nodes, (object) bindingList);
      bindingList.ListChanged += new ListChangedEventHandler(this.OnBindingListChanged);
    }

    private void OnBindingListChanged(object sender, ListChangedEventArgs e)
    {
      IBindingList bindingList = sender as IBindingList;
      if (!this.NotificationDictionary.ContainsKey((object) bindingList))
        return;
      this.UpdateNodeChildren(this.NotificationDictionary[(object) bindingList], sender, e);
    }

    private void ClearNodeNotification(TreeListNode node)
    {
      object key = (object) null;
      if (!this.MirrorNotificationDictionary.TryGetValue(node.Nodes, out key))
        return;
      (key as IBindingList).ListChanged -= new ListChangedEventHandler(this.OnBindingListChanged);
      this.MirrorNotificationDictionary.Remove(this.NotificationDictionary[key]);
      this.NotificationDictionary.Remove(key);
    }

    protected override void PatchColumnCollection(PropertyDescriptorCollection properties)
    {
      List<IColumnInfo> columnInfoList = new List<IColumnInfo>();
      foreach (IColumnInfo column in (IEnumerable<IColumnInfo>) this.View.GetColumns())
      {
        bool flag = false;
        if (properties != null)
        {
          foreach (MemberDescriptor property in properties)
          {
            if (property.Name == column.FieldName)
            {
              flag = true;
              break;
            }
          }
        }
        if (!flag)
          columnInfoList.Add(column);
      }
      foreach (IColumnInfo columnInfo in columnInfoList)
      {
        if (columnInfo.UnboundType == UnboundColumnType.Bound && !string.IsNullOrEmpty(columnInfo.FieldName) && !columnInfo.FieldName.Contains("."))
          this.PopulateColumn((PropertyDescriptor) new UnitypeDataPropertyDescriptor(columnInfo.FieldName, columnInfo.ReadOnly));
      }
      if (string.IsNullOrEmpty(this.View.CheckBoxFieldName))
        return;
      bool flag1 = false;
      foreach (MemberDescriptor property in properties)
      {
        if (property.Name == this.View.CheckBoxFieldName)
        {
          flag1 = true;
          break;
        }
      }
      if (flag1)
        return;
      this.PopulateColumn((PropertyDescriptor) new UnitypeDataPropertyDescriptor(this.View.CheckBoxFieldName, false));
    }

    public override void NodeExpandingCollapsing(TreeListNode treeListNode)
    {
      if (this.CreateAllNodes)
        return;
      this.DataProvider.SaveFocusState();
      if (this.View.DataControl != null)
        this.View.DataControl.CurrentItemChangedLocker.Lock();
      try
      {
        if (treeListNode.Nodes.Count == 0)
          this.FillSubNodes(treeListNode);
        if (!this.View.FetchSublevelChildrenOnExpand)
          return;
        this.FillChildrenSubNodes(treeListNode, false);
      }
      finally
      {
        this.RecalcNodeIdsIfNeeded();
        if (this.View.DataControl != null)
          this.View.DataControl.CurrentItemChangedLocker.Unlock();
        this.DataProvider.RestoreFocusState();
      }
    }

    private void UpdateNodes(TreeListNode parentNode)
    {
      this.DataProvider.DoSortNodes(parentNode);
      this.DataProvider.DoFilterNodes(parentNode);
      this.DataProvider.SynchronizeSummary();
    }

    private void FillChildrenSubNodes(TreeListNode treeListNode, bool createAllNodes = false)
    {
      foreach (TreeListNode node in (Collection<TreeListNode>) treeListNode.Nodes)
      {
        if (node.Nodes.Count == 0)
        {
          this.FillSubNodes(node);
          if (createAllNodes)
            this.FillChildrenSubNodes(node, createAllNodes);
        }
      }
    }

    private void FillSubNodes(TreeListNode treeListNode)
    {
      if (treeListNode.ChildrenWereEverFetched)
        return;
      this.CreateChildren(treeListNode, (IEnumerable) null);
      treeListNode.isExpandButtonVisible = DefaultBoolean.Default;
      treeListNode.ChildrenWereEverFetched = true;
    }

    protected internal object GetRowData(TreeListNode node)
    {
      if (this.DataProvider.IsValidVisibleIndex(node.VisibleIndex))
      {
        RowData rowData = this.View.GetRowData(node.RowHandle);
        if (rowData != null)
        {
          rowData.UpdateData();
          return (object) rowData;
        }
      }
      return (object) this.CreateFakeRowData(node);
    }

    protected RowData CreateFakeRowData(TreeListNode node)
    {
      RowData rowData = (RowData) new TreeListRowData((DataTreeBuilder) this.View.VisualDataTreeBuilder);
      rowData.AssignFrom(node.RowHandle);
      return rowData;
    }
  }
}
