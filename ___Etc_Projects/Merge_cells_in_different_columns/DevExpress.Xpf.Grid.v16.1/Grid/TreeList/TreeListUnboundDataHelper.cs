// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListUnboundDataHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Xpf.GridData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListUnboundDataHelper : TreeListDataHelperBase
  {
    public readonly int MaxNodeId = 1073741823;

    protected internal int NodeCounter { get; set; }

    public override bool RequiresReloadDataOnEndUpdate
    {
      get
      {
        return !string.IsNullOrEmpty(this.View.CheckBoxFieldName);
      }
      internal set
      {
      }
    }

    public override Type ItemType
    {
      get
      {
        return (Type) null;
      }
    }

    public override bool AllowEdit
    {
      get
      {
        return true;
      }
    }

    public override bool AllowRemove
    {
      get
      {
        return true;
      }
    }

    public override bool IsReady
    {
      get
      {
        return this.IsLoaded;
      }
    }

    public override bool IsLoaded
    {
      get
      {
        return this.DataProvider.Nodes.Count > 0;
      }
    }

    public override bool IsUnboundMode
    {
      get
      {
        return true;
      }
    }

    public TreeListUnboundDataHelper(TreeListDataProvider provider)
      : base(provider)
    {
    }

    public override void PopulateColumns()
    {
      foreach (IColumnInfo column in (IEnumerable<IColumnInfo>) this.DataProvider.View.GetColumns())
      {
        if (column.UnboundType == UnboundColumnType.Bound && !string.IsNullOrEmpty(column.FieldName))
          this.PopulateColumn((PropertyDescriptor) new UnitypeDataPropertyDescriptor(column.FieldName, column.ReadOnly));
      }
      this.PopulateUnboundColumns();
      this.InitAllNodesIsChecked();
    }

    public override void LoadData()
    {
      this.InitAllNodesIsChecked();
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

    public override void DeleteNode(TreeListNode node, bool deleteChildren, bool modifySource)
    {
      if (!deleteChildren)
      {
        List<TreeListNode> list = node.Nodes.ToList<TreeListNode>();
        this.DataProvider.LockRecursiveNodesUpdate();
        int rootParentIndex = this.GetRootParentIndex(node);
        foreach (TreeListNode treeListNode in list)
        {
          ++rootParentIndex;
          this.DataProvider.Nodes.Insert(rootParentIndex, treeListNode);
        }
        node.Nodes.Clear();
        this.DataProvider.UnlockRecursiveNodesUpdate();
      }
      this.RemoveTreeListNode(node);
    }

    private void InitAllNodesIsChecked()
    {
      this.DataProvider.InitNodesIsChecked();
    }
  }
}
