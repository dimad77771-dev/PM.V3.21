// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.CardViewGroupPrintingNode
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting.DataNodes;
using System.Windows;

namespace DevExpress.Xpf.Grid.Printing
{
  public class CardViewGroupPrintingNode : CardViewContainerPrintingNodeBase, IVisualGroupNode, IGroupNode, IDataNode
  {
    protected readonly GroupNode groupNode;
    protected readonly NodeContainer parentContainer;

    public bool RepeatHeaderEveryPage
    {
      get
      {
        return true;
      }
    }

    public GroupUnion Union
    {
      get
      {
        return GroupUnion.WithFirstDetail;
      }
    }

    public CardViewGroupPrintingNode(NodeContainer parentContainer, GroupNode groupNode, CardViewPrintingDataTreeBuilder treeBuilder, IDataNode parent, int index, Size pageSize)
      : base(groupNode.NodesContainer, treeBuilder, parent, index, pageSize)
    {
      this.groupNode = groupNode;
      this.parentContainer = parentContainer;
    }

    public RowViewInfo GetFooter(bool allowContentReuse)
    {
      return (RowViewInfo) null;
    }

    public RowViewInfo GetHeader(bool allowContentReuse)
    {
      this.TreeBuilder.reusingGroupRowData.AssignFromInternal((RowsContainer) null, this.parentContainer, (RowNode) this.groupNode, true);
      return this.CreateRowElement((RowData) this.TreeBuilder.reusingGroupRowData, this.TreeBuilder.PrintGroupRowTemplate);
    }
  }
}
