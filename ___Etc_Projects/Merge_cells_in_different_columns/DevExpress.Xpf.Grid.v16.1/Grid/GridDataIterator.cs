// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridDataIterator
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Data;
using DevExpress.Xpf.Grid.Native;
using System;

namespace DevExpress.Xpf.Grid
{
  public class GridDataIterator : DataIteratorBase
  {
    private GridViewBase View
    {
      get
      {
        return (GridViewBase) this.viewBase;
      }
    }

    private GridControl Grid
    {
      get
      {
        return this.View.Grid;
      }
    }

    public GridDataIterator(GridViewBase view)
      : base((DataViewBase) view)
    {
    }

    protected internal override bool GetHasTop(DataNodeContainer nodeContainer)
    {
      if (!base.GetHasTop(nodeContainer))
        return this.GetHasTopCore(nodeContainer);
      return true;
    }

    protected virtual bool GetHasTopCore(DataNodeContainer nodeContainer)
    {
      return this.GetActualRowLevel(nodeContainer.StartScrollIndex - 1) < nodeContainer.GroupLevel;
    }

    protected internal override bool GetHasBottomCore(DataNodeContainer nodeContainer, int lastVisibleIndex)
    {
      return this.GetActualRowLevel(lastVisibleIndex + 1) < nodeContainer.GroupLevel;
    }

    private int GetActualRowLevel(int scrollIndex)
    {
      object indexByScrollIndex = this.Grid.DataProviderBase.GetVisibleIndexByScrollIndex(scrollIndex);
      if (!(indexByScrollIndex is GroupSummaryRowKey))
        return this.Grid.GetRowLevelByVisibleIndex((int) indexByScrollIndex);
      return ((GroupSummaryRowKey) indexByScrollIndex).Level;
    }

    private GroupNode GetGroupNode(int rowHandle)
    {
      DataRowNode dataRowNode = (DataRowNode) null;
      if (this.Grid.View.Nodes.TryGetValue(rowHandle, out dataRowNode))
        return dataRowNode as GroupNode;
      return (GroupNode) null;
    }

    protected internal override RowNode GetRowNodeForCurrentLevel(DataNodeContainer nodeContainer, int index, int startVisibleIndex, ref bool shouldBreak)
    {
      object indexByScrollIndex = this.Grid.DataProviderBase.GetVisibleIndexByScrollIndex(index);
      GroupSummaryRowKey groupSummaryRowKey = indexByScrollIndex as GroupSummaryRowKey;
      if (groupSummaryRowKey != null)
      {
        if (nodeContainer.IsGroupRowsContainer && nodeContainer.GroupLevel == groupSummaryRowKey.Level && (nodeContainer.treeBuilder.View.AllowFixedGroupsCore ? (nodeContainer.StartScrollIndex != index ? 1 : 0) : 1) != 0)
        {
          RowNode nodeForCurrentNode = this.GetSummaryNodeForCurrentNode(nodeContainer, groupSummaryRowKey.RowHandle, index);
          if (nodeForCurrentNode != null)
          {
            GroupNode groupNode = this.GetGroupNode(groupSummaryRowKey.RowHandle.Value);
            if (groupNode != null)
              groupNode.summaryNode = nodeForCurrentNode;
            return nodeForCurrentNode;
          }
        }
        else if (groupSummaryRowKey.Level < nodeContainer.GroupLevel)
          shouldBreak = true;
        else if (nodeContainer.StartScrollIndex == index)
          return (RowNode) this.GetGroupRowNode(nodeContainer.treeBuilder, nodeContainer.StartScrollIndex + (nodeContainer.treeBuilder.View.AllowFixedGroupsCore ? 1 : 0), nodeContainer.treeBuilder.View.AllowFixedGroupsCore, DataIteratorBase.CreateValuesContainer(nodeContainer.treeBuilder, nodeContainer.parentVisibleIndex), index == nodeContainer.StartScrollIndex ? nodeContainer.DetailStartScrollIndex : 0);
        return (RowNode) null;
      }
      DataControllerValuesContainer valuesContainer = DataIteratorBase.CreateValuesContainer(nodeContainer.treeBuilder, (int) indexByScrollIndex);
      if (valuesContainer.RowHandle.Value == int.MinValue)
        return (RowNode) null;
      if (valuesContainer.RowHandle.Value < 0 && !nodeContainer.IsGroupRowsContainer && valuesContainer.RowHandle.Value != -2147483647)
      {
        shouldBreak = true;
        return (RowNode) null;
      }
      int actualRowLevel = this.View.DataControl.DataProviderBase.GetActualRowLevel(valuesContainer.RowHandle.Value, valuesContainer.Level);
      if (actualRowLevel > nodeContainer.GroupLevel && index == nodeContainer.StartScrollIndex)
        return (RowNode) this.GetGroupRowNode(nodeContainer.treeBuilder, nodeContainer.StartScrollIndex + (this.View.AllowFixedGroupsCore ? 1 : 0), this.View.AllowFixedGroupsCore, DataIteratorBase.CreateValuesContainer(nodeContainer.treeBuilder, nodeContainer.parentVisibleIndex), startVisibleIndex);
      if (nodeContainer.GroupLevel == actualRowLevel)
        return this.CreateRowNode(nodeContainer, valuesContainer, startVisibleIndex, index);
      if (actualRowLevel < nodeContainer.GroupLevel)
        shouldBreak = true;
      return (RowNode) null;
    }

    private RowNode CreateRowNode(DataNodeContainer nodeContainer, DataControllerValuesContainer info, int startVisibleIndex, int globalIndex)
    {
      return DataTreeBuilder.CreateRowElement<RowNode>(nodeContainer.IsGroupRowsContainer && this.View.DataControl.IsGroupRowHandleCore(info.RowHandle.Value), (Func<RowNode>) (() => (RowNode) this.GetGroupRowNode(nodeContainer.treeBuilder, globalIndex + 1, true, info, startVisibleIndex)), (Func<RowNode>) (() => (RowNode) this.GetRowNode(nodeContainer.treeBuilder, startVisibleIndex, info)));
    }

    protected internal override bool IsGroupRowsContainer(DataNodeContainer nodeContainer)
    {
      return nodeContainer.GroupLevel < this.View.Grid.ActualGroupCount;
    }

    private GroupNode GetGroupRowNode(DataTreeBuilder treeBuilder, int startVisibleIndex, bool isGroupRowVisible, DataControllerValuesContainer controllerValues, int detailStartVisibleIndex)
    {
      GroupNode groupNode = (GroupNode) treeBuilder.GetRowNode((DataTreeBuilder.CreateRowNodeDelegate) (values => (DataRowNode) new GroupNode(treeBuilder, values)), controllerValues);
      groupNode.summaryNode = (RowNode) null;
      groupNode.NodesContainer.DetailStartScrollIndex = detailStartVisibleIndex;
      groupNode.UpdateExpandInfo(startVisibleIndex, isGroupRowVisible);
      return groupNode;
    }

    protected internal override RowNode GetSummaryNodeForCurrentNode(DataNodeContainer nodeContainer, RowHandle rowHandle, int index)
    {
      if (this.View.ShowGroupSummaryFooter)
        return (RowNode) nodeContainer.treeBuilder.GetGroupSummaryRowNode(rowHandle.Value) ?? this.CreateGroupSummaryNode(nodeContainer, rowHandle);
      return (RowNode) null;
    }

    protected virtual RowNode CreateGroupSummaryNode(DataNodeContainer nodeContainer, RowHandle rowHandle)
    {
      GroupSummaryRowNode groupSummaryRowNode = new GroupSummaryRowNode(nodeContainer.treeBuilder, DataIteratorBase.CreateValuesContainer(nodeContainer.treeBuilder, rowHandle));
      nodeContainer.treeBuilder.AddGroupSummaryRowNode(rowHandle.Value, (DataRowNode) groupSummaryRowNode);
      return (RowNode) groupSummaryRowNode;
    }

    protected internal override int GetRowParentIndex(DataNodeContainer nodeContainer, int visibleIndex, int level)
    {
      object indexByScrollIndex = this.Grid.DataProviderBase.GetVisibleIndexByScrollIndex(visibleIndex);
      if (indexByScrollIndex == null)
        return base.GetRowParentIndex(nodeContainer, visibleIndex, level);
      GroupSummaryRowKey groupSummaryRowKey = indexByScrollIndex as GroupSummaryRowKey;
      if (groupSummaryRowKey != null)
        return base.GetRowParentIndex(nodeContainer, nodeContainer.treeBuilder.View.DataProviderBase.GetRowVisibleIndexByHandle(groupSummaryRowKey.RowHandle.Value), level);
      return base.GetRowParentIndex(nodeContainer, (int) indexByScrollIndex, level);
    }
  }
}
