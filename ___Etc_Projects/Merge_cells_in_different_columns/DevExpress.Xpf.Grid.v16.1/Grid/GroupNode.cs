// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupNode
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Grid.Hierarchy;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections.Generic;

namespace DevExpress.Xpf.Grid
{
  public class GroupNode : DataRowNode
  {
    internal RowNode summaryNode;

    public GridControl Grid
    {
      get
      {
        return (GridControl) this.DataControl;
      }
    }

    internal GridViewBase GridView
    {
      get
      {
        return (GridViewBase) this.Grid.View;
      }
    }

    public DataNodeContainer NodesContainer
    {
      get
      {
        return (DataNodeContainer) base.NodesContainer;
      }
    }

    protected override bool IsDataExpanded
    {
      get
      {
        return this.Grid.DataProviderBase.IsGroupRowExpanded(this.RowHandle.Value);
      }
    }

    internal override bool CanGenerateItems
    {
      get
      {
        return base.CanGenerateItems;
      }
      set
      {
        base.CanGenerateItems = value;
        if (this.NodesContainer == null)
          return;
        foreach (RowNode rowNode in (IEnumerable<RowNode>) this.NodesContainer.Items)
        {
          if (rowNode is GroupNode)
            rowNode.CanGenerateItems = value;
        }
      }
    }

    protected override bool CanGenerateItemsCore
    {
      get
      {
        return true;
      }
    }

    internal override bool IsFixedNode
    {
      get
      {
        if (!this.IsRowVisible || !this.IsExpanded || this.NodesContainer.Items.Count == 0)
          return false;
        IRootItemsContainer rootItemsContainer = (IRootItemsContainer) this.GridView.MasterRootRowsContainer;
        if (this.NodesContainer.Items[0].GetRowData() == rootItemsContainer.ScrollItem && rootItemsContainer.ScrollItemOffset != 0.0)
          return true;
        return this.ControllerValues.VisibleIndex + 1 < ((DataRowNode) this.NodesContainer.Items[0]).ControllerValues.VisibleIndex;
      }
    }

    public override int CurrentLevelItemCount
    {
      get
      {
        int currentLevelItemCount = this.NodesContainer.CurrentLevelItemCount;
        if (this.IsRowVisible)
          ++currentLevelItemCount;
        return currentLevelItemCount;
      }
    }

    public GroupNode(DataTreeBuilder treeBuilder, DataControllerValuesContainer controllerValues)
      : base(treeBuilder, controllerValues)
    {
      this.NodesContainer = (NodeContainer) new DataNodeContainer(treeBuilder, this.Level + 1, (DataRowNode) this);
      this.NodesContainer.OnDataChangedCore();
    }

    internal override LinkedList<FreeRowDataInfo> GetFreeRowDataQueue(SynchronizationQueues synchronizationQueues)
    {
      return synchronizationQueues.FreeGroupRowDataQueue;
    }

    internal override RowDataBase CreateRowData()
    {
      return (RowDataBase) this.GridView.CreateGroupRowDataCore(this.treeBuilder);
    }

    protected override void ValidateControllerValues()
    {
      if (this.RowHandle.Value >= 0)
        throw new ArgumentException("Internal error: RowHandle should be negative");
    }

    internal override RowNode GetNodeToScroll()
    {
      if (!this.GridView.AllowFixedGroupsCore && this.IsRowVisible)
        return (RowNode) this;
      RowNode nodeToScroll = base.GetNodeToScroll();
      if (nodeToScroll == this && this.GridView.ShowGroupSummaryFooter && this.summaryNode != null)
        return this.summaryNode;
      return nodeToScroll;
    }

    public override int SkipChildNodes(int index)
    {
      if (this.Grid.DataProviderBase.IsAsyncServerMode || !this.Grid.DataController.AllowPartialGrouping || this.Grid.DataProviderBase.GetVisibleIndexByScrollIndex(index) is GroupSummaryRowKey)
        return index;
      int footerCount;
      index = this.Grid.GetRowVisibleIndexByHandle(this.RowHandle.Value) + this.GetChildRowCount(out footerCount);
      if (this.Grid.DataProviderBase.ShowGroupSummaryFooter)
        index += this.Grid.DataProviderBase.GetSummaryRowCountBeforeRow(this.RowHandle.Value);
      return index + footerCount;
    }

    private int GetChildRowCount(out int footerCount)
    {
      footerCount = 0;
      GroupRowInfoCollection groupInfo1 = this.Grid.DataController.GroupInfo;
      GroupRowInfo groupRowInfoByHandle = groupInfo1.GetGroupRowInfoByHandle(this.RowHandle.Value);
      int num = 0;
      int index = groupRowInfoByHandle.Index;
      GroupRowInfo groupInfo2 = (GroupRowInfo) null;
      while (index != groupInfo1.Count)
      {
        GroupRowInfo groupInfo3 = groupInfo1[index];
        if (groupInfo3 == groupRowInfoByHandle || (int) groupInfo3.Level > (int) groupRowInfoByHandle.Level)
        {
          groupInfo2 = groupInfo3;
          if (groupInfo3 != groupRowInfoByHandle && this.Grid.DataProviderBase.IsGroupVisible(groupInfo3))
            ++num;
          if ((int) groupInfo3.Level == groupInfo1.LevelCount - 1 && groupInfo3.Expanded)
            num += groupInfo3.ChildControllerRowCount;
          if (groupInfo3.Expanded)
            ++index;
          else
            index = GridDataProvider.GetLevelNextIndex(index, groupInfo1);
        }
        else
          break;
      }
      if (this.Grid.DataProviderBase.ShowGroupSummaryFooter && groupInfo2 != groupRowInfoByHandle)
      {
        while (true)
        {
          if (this.Grid.DataProviderBase.IsGroupVisible(groupInfo2) && this.Grid.DataProviderBase.OnShowingGroupFooter(groupInfo2.Handle, (int) groupInfo2.Level))
            ++footerCount;
          if (groupInfo2.ParentGroup != groupRowInfoByHandle)
            groupInfo2 = groupInfo2.ParentGroup;
          else
            break;
        }
      }
      return num;
    }
  }
}
