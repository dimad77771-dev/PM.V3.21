// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.CardViewContainerPrintingNodeBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using DevExpress.XtraPrinting.DataNodes;
using System.Windows;

namespace DevExpress.Xpf.Grid.Printing
{
  public abstract class CardViewContainerPrintingNodeBase : ContainerPrintingNodeBase
  {
    protected internal CardViewPrintingDataTreeBuilder TreeBuilder
    {
      get
      {
        return base.TreeBuilder as CardViewPrintingDataTreeBuilder;
      }
    }

    public CardViewContainerPrintingNodeBase(DataNodeContainer nodeContainer, CardViewPrintingDataTreeBuilder treeBuilder, IDataNode parent, int index, Size pageSize)
      : base(nodeContainer, (DataTreeBuilder) treeBuilder, parent, index, pageSize)
    {
    }

    protected override bool GetIsDetailContainerCore()
    {
      return !this.nodeContainer.PrintInfo.IsGroupRowsContainer;
    }

    protected override IDataNode CreateGroupChildNode(RowNode rowNode, int index)
    {
      return this.TreeBuilder.CreateGroupPrintingNode((NodeContainer) this.nodeContainer, rowNode, (IDataNode) this, index, this.pageSize);
    }

    protected override IDataNode CreateChildNode(NodeContainer container, RowNode rowNode, IDataNode parentNode, int index)
    {
      return this.TreeBuilder.CreateDetailPrintingNode((NodeContainer) this.nodeContainer, rowNode, (IDataNode) this, index);
    }
  }
}
