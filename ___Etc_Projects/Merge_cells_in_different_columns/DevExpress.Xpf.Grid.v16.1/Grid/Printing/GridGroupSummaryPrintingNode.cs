// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GridGroupSummaryPrintingNode
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting.DataNodes;
using System.Windows;

namespace DevExpress.Xpf.Grid.Printing
{
  public class GridGroupSummaryPrintingNode : GridGroupPrintingNode, IVisualGroupNode, IGroupNode, IDataNode
  {
    private GroupSummaryRowNode SummaryRowNode
    {
      get
      {
        return this.groupNode as GroupSummaryRowNode;
      }
    }

    public GridGroupSummaryPrintingNode(NodeContainer parentContainer, GroupNode groupNode, GridPrintingDataTreeBuilder treeBuilder, IDataNode parent, int index, Size pageSize)
      : base(parentContainer, groupNode, treeBuilder, parent, index, pageSize)
    {
    }

    RowViewInfo IVisualGroupNode.GetFooter(bool allowContentReuse)
    {
      GroupSummaryRowData groupSummaryRowData = (GroupSummaryRowData) this.SummaryRowNode.CreateRowData();
      groupSummaryRowData.AssignFromInternal((RowsContainer) null, this.parentContainer, (RowNode) this.groupNode, true);
      return this.CreateRowElement((RowData) groupSummaryRowData, this.GridTreeBuilder.PrintGroupFooterTemplate);
    }

    RowViewInfo IVisualGroupNode.GetHeader(bool allowContentReuse)
    {
      return (RowViewInfo) null;
    }

    protected override bool CanGetChildCore(int index)
    {
      return false;
    }
  }
}
