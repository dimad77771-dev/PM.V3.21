// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListDataHelperBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using System;
using System.Collections;
using System.ComponentModel;

namespace DevExpress.Xpf.Grid.TreeList
{
  public abstract class TreeListDataHelperBase : IDisposable
  {
    private int loading;

    public abstract Type ItemType { get; }

    public virtual bool IsReady
    {
      get
      {
        return true;
      }
    }

    public virtual bool IsUnboundMode
    {
      get
      {
        return false;
      }
    }

    protected TreeListDataProvider DataProvider { get; private set; }

    protected TreeListView View
    {
      get
      {
        return this.DataProvider.View;
      }
    }

    protected DataColumnInfoCollection Columns
    {
      get
      {
        return this.DataProvider.Columns;
      }
    }

    public virtual bool RequiresReloadDataOnEndUpdate
    {
      get
      {
        return false;
      }
      internal set
      {
      }
    }

    public abstract bool AllowEdit { get; }

    public abstract bool AllowRemove { get; }

    public abstract bool IsLoaded { get; }

    public bool IsLoading
    {
      get
      {
        return this.loading > 0;
      }
    }

    protected internal virtual bool SupportNotifications
    {
      get
      {
        return false;
      }
    }

    public TreeListDataHelperBase(TreeListDataProvider provider)
    {
      this.DataProvider = provider;
    }

    protected virtual bool CanPopulate(PropertyDescriptor descriptor)
    {
      return true;
    }

    protected virtual DataColumnInfo CreateDataColumn(PropertyDescriptor descriptor)
    {
      DataColumnInfo column = new DataColumnInfo(descriptor);
      column.Visible = this.IsColumnVisible(column);
      return column;
    }

    protected virtual bool IsColumnVisible(DataColumnInfo column)
    {
      return column.Browsable;
    }

    protected virtual void PopulateColumn(PropertyDescriptor descriptor)
    {
      if (!this.CanPopulate(descriptor))
        return;
      this.Columns.Add(this.CreateDataColumn(descriptor));
    }

    public virtual void NodeExpandingCollapsing(TreeListNode node)
    {
    }

    public virtual void Dispose()
    {
    }

    public object GetValue(TreeListNode node, string fieldName)
    {
      if (node == null)
        return (object) null;
      return this.GetValue(node, this.Columns[fieldName]);
    }

    public object GetValue(TreeListNode node, DataColumnInfo columnInfo)
    {
      if (node == null || columnInfo == null)
        return (object) null;
      return this.GetValue(node, columnInfo.PropertyDescriptor);
    }

    protected internal object GetValue(TreeListNode node, PropertyDescriptor descriptor)
    {
      object component = descriptor is TreeListUnboundPropertyDescriptor || descriptor is TreeListDisplayTextPropertyDescriptor || (descriptor is TreeListSearchDisplayTextPropertyDescriptor || descriptor is TreeListValuePropertyDescriptor) ? (object) node : node.Content;
      return descriptor.GetValue(component);
    }

    public void SetValue(TreeListNode node, string fieldName, object value)
    {
      this.SetValue(node, this.Columns[fieldName], value);
    }

    public void SetValue(TreeListNode node, DataColumnInfo columnInfo, object value)
    {
      if (node == null || columnInfo == null || columnInfo.ReadOnly)
        return;
      this.SetValue(node, columnInfo.PropertyDescriptor, columnInfo.ConvertValue(value, true));
    }

    protected void SetValue(TreeListNode node, PropertyDescriptor descriptor, object value)
    {
      object component = descriptor is TreeListUnboundPropertyDescriptor ? (object) node : node.Content;
      if (component == null)
        return;
      descriptor.SetValue(component, value);
    }

    protected void RemoveTreeListNode(TreeListNode node)
    {
      TreeListNode parentNode = node.ParentNode;
      if (parentNode == null)
        this.DataProvider.Nodes.Remove(node);
      else
        parentNode.Nodes.Remove(node);
    }

    protected int GetRootParentIndex(TreeListNode node)
    {
      TreeListNode treeListNode = node.ParentNode;
      if (treeListNode == null)
      {
        treeListNode = node;
      }
      else
      {
        while (treeListNode.ParentNode != null)
          treeListNode = treeListNode.ParentNode;
      }
      return this.DataProvider.Nodes.IndexOf(treeListNode);
    }

    protected internal virtual void UpdateNodeId(TreeListNode node)
    {
    }

    protected internal virtual void RecalcNodeIdsIfNeeded()
    {
    }

    protected virtual void CalcNodeIds()
    {
    }

    public virtual void DeleteNode(TreeListNode node, bool deleteChildren, bool modifySource)
    {
    }

    public abstract void PopulateColumns();

    public abstract void LoadData();

    protected virtual void BeginLoad()
    {
      ++this.loading;
    }

    protected virtual void EndLoad()
    {
      --this.loading;
    }

    protected virtual object GetValue(object item, string fieldName)
    {
      DataColumnInfo dataColumnInfo = this.Columns[fieldName];
      if (dataColumnInfo != null)
        return dataColumnInfo.PropertyDescriptor.GetValue(item);
      return (object) null;
    }

    protected virtual TreeListUnboundPropertyDescriptor CreateUnboundPropertyDescriptor(UnboundColumnInfo info)
    {
      return new TreeListUnboundPropertyDescriptor(this.DataProvider, info);
    }

    protected virtual void PopulateUnboundColumns()
    {
      UnboundColumnInfoCollection unboundColumns = this.DataProvider.GetUnboundColumns();
      if (unboundColumns == null)
        return;
      foreach (UnboundColumnInfo info in (CollectionBase) unboundColumns)
      {
        if (this.Columns[info.Name] == null)
          this.PopulateColumn((PropertyDescriptor) this.CreateUnboundPropertyDescriptor(info));
      }
    }

    public virtual object GetDataRowByListIndex(int listSourceRowIndex)
    {
      return (object) null;
    }

    public virtual object GetCellValueByListIndex(int listSourceRowIndex, string fieldName)
    {
      return (object) null;
    }

    public virtual int GetListIndexByDataRow(object row)
    {
      return -1;
    }

    public virtual void ReloadChildNodes(TreeListNode node, IEnumerable chidnren = null)
    {
    }
  }
}
