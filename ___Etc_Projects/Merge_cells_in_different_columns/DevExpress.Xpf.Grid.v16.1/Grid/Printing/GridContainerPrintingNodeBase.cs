// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GridContainerPrintingNodeBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using DevExpress.XtraPrinting.DataNodes;
using System.Windows;

namespace DevExpress.Xpf.Grid.Printing
{
  public abstract class GridContainerPrintingNodeBase : ContainerPrintingNodeBase
  {
    protected internal PrintingDataTreeBuilder TreeBuilder
    {
      get
      {
        return base.TreeBuilder as PrintingDataTreeBuilder;
      }
    }

    public GridContainerPrintingNodeBase(DataNodeContainer nodeContainer, PrintingDataTreeBuilderBase treeBuilder, IDataNode parent, int index, Size pageSize)
      : base(nodeContainer, (DataTreeBuilder) treeBuilder, parent, index, pageSize)
    {
    }

    protected override bool GetIsDetailContainerCore()
    {
      if (!this.nodeContainer.PrintInfo.IsGroupRowsContainer && !(this.TreeBuilder.View.DataControl.DetailDescriptorCore is DataControlDetailDescriptor))
        return !(this.TreeBuilder.View.DataControl.DetailDescriptorCore is TabViewDetailDescriptor);
      return false;
    }

    protected override IDataNode CreateGroupChildNode(RowNode rowNode, int index)
    {
      if (!(rowNode is GroupNode))
        return this.TreeBuilder.CreateMasterDetailPrintingNode((NodeContainer) this.nodeContainer, rowNode, (IDataNode) this, index, this.pageSize);
      return this.TreeBuilder.CreateGroupPrintingNode((NodeContainer) this.nodeContainer, rowNode, (IDataNode) this, index, this.pageSize);
    }

    protected override IDataNode CreateChildNode(NodeContainer container, RowNode rowNode, IDataNode parentNode, int index)
    {
      return this.TreeBuilder.CreateDetailPrintingNode((NodeContainer) this.nodeContainer, rowNode, (IDataNode) this, index);
    }
  }
}
