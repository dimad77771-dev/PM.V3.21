// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GridGroupPrintingNode
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting.DataNodes;
using System.Windows;

namespace DevExpress.Xpf.Grid.Printing
{
  public class GridGroupPrintingNode : GridContainerPrintingNodeBase, IVisualGroupNode, IGroupNode, IDataNode
  {
    protected readonly GroupNode groupNode;
    protected readonly NodeContainer parentContainer;

    protected internal GridPrintingDataTreeBuilder GridTreeBuilder
    {
      get
      {
        return this.TreeBuilder as GridPrintingDataTreeBuilder;
      }
    }

    GroupUnion IGroupNode.Union
    {
      get
      {
        return GroupUnion.WithFirstDetail;
      }
    }

    bool IGroupNode.RepeatHeaderEveryPage
    {
      get
      {
        return true;
      }
    }

    public GridGroupPrintingNode(NodeContainer parentContainer, GroupNode groupNode, GridPrintingDataTreeBuilder treeBuilder, IDataNode parent, int index, Size pageSize)
      : base(groupNode.NodesContainer, (PrintingDataTreeBuilderBase) treeBuilder, parent, index, pageSize)
    {
      this.groupNode = groupNode;
      this.parentContainer = parentContainer;
    }

    RowViewInfo IVisualGroupNode.GetFooter(bool allowContentReuse)
    {
      return (RowViewInfo) null;
    }

    RowViewInfo IVisualGroupNode.GetHeader(bool allowContentReuse)
    {
      this.GridTreeBuilder.reusingGroupRowData.AssignFromInternal((RowsContainer) null, this.parentContainer, (RowNode) this.groupNode, true);
      return this.CreateRowElement((RowData) this.GridTreeBuilder.reusingGroupRowData, this.GridTreeBuilder.PrintGroupRowTemplate);
    }

    protected override bool CanGetChildCore(int index)
    {
      if (0 <= index && index < this.nodeContainer.Items.Count)
      {
        DataRowNode dataRowNode = this.nodeContainer.Items[index] as DataRowNode;
        if (dataRowNode == null || dataRowNode.RowHandle.Value != -2147483647)
          return true;
      }
      return false;
    }
  }
}
