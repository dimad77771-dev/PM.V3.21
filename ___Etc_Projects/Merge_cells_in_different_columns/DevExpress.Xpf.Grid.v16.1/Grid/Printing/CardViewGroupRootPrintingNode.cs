// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.CardViewGroupRootPrintingNode
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting.DataNodes;
using System.Windows;

namespace DevExpress.Xpf.Grid.Printing
{
  public class CardViewGroupRootPrintingNode : CardViewContainerPrintingNodeBase, IVisualGroupNodeFixedFooter, IVisualGroupNode, IGroupNode, IDataNode
  {
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
        return false;
      }
    }

    public CardViewGroupRootPrintingNode(CardViewPrintingDataTreeBuilder treeBuilder, IDataNode parent, Size pageSize)
      : base((DataNodeContainer) treeBuilder.RootNodeContainer, treeBuilder, parent, -1, pageSize)
    {
    }

    RowViewInfo IVisualGroupNode.GetFooter(bool allowContentReuse)
    {
      if (this.TreeBuilder.View.PrintTotalSummary && this.TreeBuilder.View.ShowTotalSummary && this.TreeBuilder.PrintFooterTemplate != null)
        return new RowViewInfo(this.TreeBuilder.PrintFooterTemplate, (object) this.TreeBuilder.HeadersData);
      return (RowViewInfo) null;
    }

    RowViewInfo IVisualGroupNode.GetHeader(bool allowContentReuse)
    {
      if (this.TreeBuilder.View.PrintTotalSummary && this.TreeBuilder.View.ShowTotalSummary && this.TreeBuilder.PrintHeaderTemplate != null)
        return new RowViewInfo(this.TreeBuilder.PrintHeaderTemplate, (object) this.TreeBuilder.HeadersData);
      return (RowViewInfo) null;
    }

    public RowViewInfo GetFixedFooter(bool allowContentReuse)
    {
      if (this.TreeBuilder.View.PrintFixedTotalSummary && this.TreeBuilder.View.ShowFixedTotalSummary && this.TreeBuilder.PrintFixedFooterTemplate != null)
        return new RowViewInfo(this.TreeBuilder.PrintFixedFooterTemplate, (object) this.TreeBuilder.HeadersData);
      return (RowViewInfo) null;
    }
  }
}
