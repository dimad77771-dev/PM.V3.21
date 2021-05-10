// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListSelfReferenceDataHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListSelfReferenceDataHelper : TreeListBoundDataHelper
  {
    private bool requiresReloadDataOnEndUpdateCore = true;
    private IBindingList bindingList;

    protected override IBindingList BindingList
    {
      get
      {
        return this.bindingList;
      }
      set
      {
        if (this.BindingList != null)
          this.BindingList.ListChanged -= new ListChangedEventHandler(this.OnBindingListChanged);
        this.bindingList = value;
        if (this.BindingList == null)
          return;
        this.BindingList.ListChanged += new ListChangedEventHandler(this.OnBindingListChanged);
      }
    }

    public override bool RequiresReloadDataOnEndUpdate
    {
      get
      {
        return this.requiresReloadDataOnEndUpdateCore;
      }
      internal set
      {
        if (this.requiresReloadDataOnEndUpdateCore == value)
          return;
        this.requiresReloadDataOnEndUpdateCore = value;
      }
    }

    public TreeListSelfReferenceDataHelper(TreeListDataProvider provider, object dataSource)
      : base(provider, dataSource)
    {
    }

    public override void Dispose()
    {
      base.Dispose();
      this.BindingList = (IBindingList) null;
    }

    protected override bool CanPopulate(PropertyDescriptor descriptor)
    {
      return true;
    }

    protected virtual bool IsDetailDescriptor(PropertyDescriptor descriptor)
    {
      if (typeof (IList).IsAssignableFrom(descriptor.PropertyType))
        return !typeof (Array).IsAssignableFrom(descriptor.PropertyType);
      return false;
    }

    protected object GetListItem(int index)
    {
      if (this.ListSource.Count > index)
        return this.ListSource[index];
      return (object) null;
    }

    protected virtual void OnBindingListChanged(object sender, ListChangedEventArgs e)
    {
      Dispatcher dispatcher = this.View.Dispatcher;
      if (dispatcher == null || dispatcher.CheckAccess())
        this.OnListChanged(e);
      else
        dispatcher.BeginInvoke((Delegate) (() => this.OnListChanged(e)));
    }

    private void OnListChanged(ListChangedEventArgs e)
    {
      this.DataProvider.TryRepopulateColumns();
      this.OnBindingListChanged(e);
    }

    private void OnBindingListChanged(ListChangedEventArgs e)
    {
      if (e.ListChangedType == ListChangedType.PropertyDescriptorAdded || e.ListChangedType == ListChangedType.PropertyDescriptorDeleted || e.ListChangedType == ListChangedType.PropertyDescriptorChanged)
      {
        this.DataProvider.OnDataSourceChanged();
      }
      else
      {
        if (this.DataProvider.IsUpdateLocked && this.RequiresReloadDataOnEndUpdate)
          return;
        switch (e.ListChangedType)
        {
          case ListChangedType.ItemAdded:
            this.OnItemAdded(e.NewIndex);
            break;
          case ListChangedType.ItemDeleted:
            this.OnItemDeleted(e.NewIndex);
            break;
          case ListChangedType.ItemChanged:
            this.OnItemChanged(e.NewIndex, e.PropertyDescriptor != null ? e.PropertyDescriptor.Name : string.Empty);
            break;
          default:
            this.OnReset();
            break;
        }
      }
    }

    protected virtual void OnItemChanged(int index, string memberName)
    {
      object listItem = this.GetListItem(index);
      if (listItem == null)
        return;
      TreeListNode nodeByValue = this.TryFindNodeByValue(listItem);
      if (nodeByValue == null)
        return;
      if (nodeByValue != this.DataProvider.CurrentlyCheckingNode)
        nodeByValue.IsChecked = this.DataProvider.GetObjectIsChecked(nodeByValue);
      if (!this.IsLoading)
        nodeByValue.ForceUpdateExpandState();
      if (this.CheckParentIDChanging(nodeByValue, memberName))
        return;
      this.DataProvider.OnNodeCollectionChanged(nodeByValue, NodeChangeType.Content, true, memberName);
    }

    private TreeListNode TryFindNodeByValue(object item)
    {
      TreeListNode nodeByValue = this.DataProvider.FindNodeByValue(item);
      if (nodeByValue == null)
      {
        this.OnReset();
        nodeByValue = this.DataProvider.FindNodeByValue(item);
      }
      return nodeByValue;
    }

    protected virtual bool CheckParentIDChanging(TreeListNode node, string memberName)
    {
      if (!this.IsValidColumnName(this.View.KeyFieldName) || !this.IsValidColumnName(this.View.ParentFieldName))
        return false;
      bool flag = string.IsNullOrEmpty(memberName) ? !object.Equals(this.GetValue(node, this.View.ParentFieldName), node.ParentNode == null ? this.View.RootValue : this.GetValue(node.ParentNode, this.View.KeyFieldName)) : memberName == this.View.ParentFieldName || memberName == this.View.KeyFieldName;
      if (flag)
        this.OnReset();
      return flag;
    }

    protected virtual void OnItemAdded(int index)
    {
      object listItem = this.GetListItem(index);
      if (listItem == null)
        return;
      TreeListNode node = this.DataProvider.CreateNode(listItem);
      node.DataProvider = this.DataProvider;
      node.InitIsChecked();
      object id = this.GetValue(node, this.View.KeyFieldName);
      if (this.DataProvider.FindNodeByValue(this.View.KeyFieldName, id) != null)
      {
        this.OnReset();
      }
      else
      {
        object objA = this.GetValue(node, this.View.ParentFieldName);
        this.DataProvider.SaveFocusState();
        this.DataProvider.BeginUpdateCore();
        try
        {
          if (objA == null || object.Equals(objA, this.View.RootValue))
          {
            this.DataProvider.Nodes.Add(node);
          }
          else
          {
            TreeListNode nodeByValue = this.DataProvider.FindNodeByValue(this.View.KeyFieldName, objA);
            if (nodeByValue != null)
              nodeByValue.Nodes.Add(node);
            else
              this.DataProvider.Nodes.Add(node);
            IEnumerable<TreeListNode> childNodesById = this.GetChildNodesById(id);
            if (childNodesById != null)
              this.MoveChildNodesToParent(node, childNodesById);
          }
          this.UpdateNodesIdsOnIndexInsert(node, index);
          if (!this.DataProvider.HasSortInfo)
            this.DataProvider.SortNodes(node.parentNodeCore, false);
        }
        finally
        {
          this.DataProvider.CancelUpdate();
          this.DataProvider.RestoreFocusState();
        }
        this.DataProvider.OnNodeCollectionChanged(node, NodeChangeType.Add, false, (string) null);
      }
    }

    private IEnumerable<TreeListNode> GetChildNodesById(object id)
    {
      return (IEnumerable<TreeListNode>) this.DataProvider.Nodes.Where<TreeListNode>((Func<TreeListNode, bool>) (node =>
      {
        object nodeValue = this.DataProvider.GetNodeValue(node, this.View.ParentFieldName);
        if (nodeValue != null && nodeValue.Equals(id))
          return !nodeValue.Equals(this.DataProvider.GetNodeValue(node, this.View.KeyFieldName));
        return false;
      })).ToList<TreeListNode>();
    }

    private void MoveChildNodesToParent(TreeListNode parentNode, IEnumerable<TreeListNode> children)
    {
      foreach (TreeListNode child in children)
      {
        int id = child.Id;
        this.DataProvider.Nodes.Remove(child);
        parentNode.Nodes.Add(child);
        child.Id = id;
      }
    }

    protected virtual void OnItemDeleted(int index)
    {
      this.DataProvider.SaveFocusState();
      try
      {
        TreeListNode nodeById = this.DataProvider.FindNodeById(index);
        if (nodeById == null)
          return;
        if (this.View.NeedWatchRowChanged())
          this.View.CurrentRowChanged(ListChangedType.ItemDeleted, nodeById.RowHandle, new int?(nodeById.RowHandle));
        this.DeleteNode(nodeById, false, false);
        this.UpdateNodesIdsOnIndexRemove(index);
      }
      finally
      {
        this.DataProvider.RestoreFocusState();
      }
    }

    protected virtual void OnReset()
    {
      this.requiresReloadDataOnEndUpdateCore = false;
      this.DataProvider.BeginUpdate();
      try
      {
        this.LoadData();
      }
      finally
      {
        this.DataProvider.EndUpdate();
        this.requiresReloadDataOnEndUpdateCore = true;
      }
    }

    public void DeleteNode(TreeListNode node)
    {
      this.DeleteNode(node, false, true);
    }

    public void DeleteNode(TreeListNode node, bool deleteChildren)
    {
      this.DeleteNode(node, deleteChildren, true);
    }

    public override void DeleteNode(TreeListNode node, bool deleteChildren, bool modifySource)
    {
      if (!this.IsReady)
        return;
      if (deleteChildren)
      {
        if (modifySource)
        {
          List<object> objectsToRemove = new List<object>();
          objectsToRemove.AddRange(new TreeListNodeIterator(node.Nodes).Select<TreeListNode, object>((Func<TreeListNode, object>) (n => n.Content)));
          objectsToRemove.Add(node.Content);
          this.RemoveObjectsFromSource(objectsToRemove);
        }
      }
      else
      {
        this.DataProvider.LockRecursiveNodesUpdate();
        try
        {
          if (modifySource)
            this.ListSource.Remove(node.Content);
          List<TreeListNode> list = node.Nodes.ToList<TreeListNode>();
          int rootParentIndex = this.GetRootParentIndex(node);
          foreach (TreeListNode treeListNode in list)
          {
            ++rootParentIndex;
            this.DataProvider.Nodes.Insert(rootParentIndex, treeListNode);
          }
          node.Nodes.Clear();
        }
        finally
        {
          this.DataProvider.UnlockRecursiveNodesUpdate();
        }
      }
      this.RemoveTreeListNode(node);
    }

    private void RemoveObjectsFromSource(List<object> objectsToRemove)
    {
      this.DataProvider.LockRecursiveNodesUpdate();
      try
      {
        foreach (object obj in objectsToRemove)
          this.ListSource.Remove(obj);
      }
      finally
      {
        this.DataProvider.UnlockRecursiveNodesUpdate();
      }
    }

    internal void SyncNodesIds(List<int> deletedIds)
    {
      deletedIds.Sort();
      foreach (TreeListNode treeListNode in (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.DataProvider.Nodes))
        treeListNode.Id -= this.GetMaxIndex(deletedIds, treeListNode.Id);
    }

    private int GetMaxIndex(List<int> deletedIds, int nodeId)
    {
      int num = 0;
      foreach (int deletedId in deletedIds)
      {
        if (nodeId > deletedId)
          ++num;
        else
          break;
      }
      return num;
    }

    private void UpdateNodesIdsOnIndexInsert(TreeListNode treeNode, int index)
    {
      foreach (TreeListNode treeListNode in (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.DataProvider.Nodes))
      {
        if (treeListNode.Id >= index)
          ++treeListNode.Id;
      }
      treeNode.Id = index;
    }

    private void UpdateNodesIdsOnIndexRemove(int index)
    {
      foreach (TreeListNode treeListNode in (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.DataProvider.Nodes))
      {
        if (treeListNode.Id > index)
          --treeListNode.Id;
      }
    }

    protected override bool IsColumnsVisibleCore(DataColumnInfo column)
    {
      if (base.IsColumnsVisibleCore(column) && column.Name != this.View.KeyFieldName)
        return column.Name != this.View.ParentFieldName;
      return false;
    }

    private bool IsValidColumnName(string fieldName)
    {
      return this.Columns[fieldName] != null;
    }

    public override object GetDataRowByListIndex(int listIndex)
    {
      if (!this.IsReady)
        return (object) null;
      if (listIndex < 0 || listIndex >= this.ListSource.Count)
        return (object) null;
      return this.ListSource[listIndex];
    }

    public override object GetCellValueByListIndex(int listSourceRowIndex, string fieldName)
    {
      if (!this.IsReady)
        return (object) null;
      if (listSourceRowIndex < 0 || listSourceRowIndex >= this.ListSource.Count)
        return (object) null;
      TreeListNode nodeByValue = this.DataProvider.FindNodeByValue(this.ListSource[listSourceRowIndex]);
      if (nodeByValue == null)
        return (object) null;
      return this.DataProvider.GetNodeValue(nodeByValue, fieldName);
    }

    public override int GetListIndexByDataRow(object row)
    {
      if (!this.IsReady || row == null || !this.ListSource.Contains(row))
        return -1;
      return this.ListSource.IndexOf(row);
    }

    public override void LoadData()
    {
      this.CheckServiceColumns();
      this.DataProvider.Nodes.Clear();
      this.DataProvider.LockRecursiveNodesUpdate();
      this.BeginLoad();
      try
      {
        this.DataProvider.LockRecursiveNodesUpdate();
        this.LoadDataCore();
      }
      finally
      {
        this.DataProvider.UnlockRecursiveNodesUpdate();
        this.EndLoad();
      }
    }

    protected override void EndLoad()
    {
      base.EndLoad();
      if (this.IsLoading || this.View.ExpandStateBinding == null && string.IsNullOrEmpty(this.View.ExpandStateFieldName))
        return;
      this.DataProvider.UpdateNodesExpandState(this.DataProvider.Nodes, false);
    }

    protected void CheckServiceColumns()
    {
      IColumnCollection columnsCore = this.View.ColumnsCore;
      if (columnsCore[this.View.KeyFieldName] != null && columnsCore[this.View.KeyFieldName].UnboundType != UnboundColumnType.Bound || columnsCore[this.View.ParentFieldName] != null && columnsCore[this.View.ParentFieldName].UnboundType != UnboundColumnType.Bound)
        throw new ArgumentException("A service column cannot be represented by an unbound column.");
    }

    protected virtual void LoadDataCore()
    {
      Dictionary<object, TreeListNode> dictionaryFromListSource = this.GetNodeDictionaryFromListSource();
      if (this.Columns[this.View.ParentFieldName] == null || this.Columns[this.View.KeyFieldName] == null || this.Columns[this.View.ParentFieldName] == this.Columns[this.View.KeyFieldName])
      {
        this.LoadLinearData(dictionaryFromListSource);
      }
      else
      {
        string parentFieldName = this.View.ParentFieldName;
        foreach (object key in dictionaryFromListSource.Keys)
        {
          TreeListNode node = dictionaryFromListSource[key];
          object index = (object) null;
          if (!string.IsNullOrEmpty(parentFieldName))
            index = this.GetValue(node, parentFieldName);
          if (index != null && dictionaryFromListSource.ContainsKey(index))
          {
            if (this.View.RootValue != null && object.Equals(index, this.View.RootValue))
              this.DataProvider.Nodes.Add(node);
            else if (dictionaryFromListSource[index] == node)
              this.DataProvider.Nodes.Add(node);
            else
              dictionaryFromListSource[index].Nodes.Add(node);
          }
          else if (this.View.RootValue == null || object.Equals(index, this.View.RootValue))
            this.DataProvider.Nodes.Add(node);
        }
      }
    }

    private Dictionary<object, TreeListNode> GetNodeDictionaryFromListSource()
    {
      Dictionary<object, TreeListNode> dictionary = new Dictionary<object, TreeListNode>();
      int num = 0;
      bool flag = false;
      string keyFieldName = this.View.KeyFieldName;
      if (!this.View.IsDesignTime && this.ListSource != null && this.ListSource.Count > 0)
        flag = this.Columns[keyFieldName] != null;
      for (int index = 0; index < this.ListSource.Count; ++index)
      {
        object obj = this.ListSource[index];
        object key;
        if (flag)
        {
          key = this.GetValue(obj, keyFieldName);
          if (key == null)
            throw new Exception("Missing primary key.");
          if (dictionary.ContainsKey(key))
            throw new Exception("Duplicated primary key.");
        }
        else
          key = (object) num++;
        TreeListNode node = this.DataProvider.CreateNode((object) null);
        node.Id = index;
        node.DataProvider = this.DataProvider;
        node.Content = obj;
        node.InitIsChecked();
        dictionary[key] = node;
      }
      return dictionary;
    }

    private void LoadLinearData(Dictionary<object, TreeListNode> tempMap)
    {
      foreach (TreeListNode treeListNode in tempMap.Values)
        this.DataProvider.Nodes.Add(treeListNode);
    }
  }
}
