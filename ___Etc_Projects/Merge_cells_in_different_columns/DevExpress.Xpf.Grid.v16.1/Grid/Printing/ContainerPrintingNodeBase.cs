// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.ContainerPrintingNodeBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using DevExpress.XtraPrinting.DataNodes;
using System;
using System.Windows;

namespace DevExpress.Xpf.Grid.Printing
{
  public abstract class ContainerPrintingNodeBase : PrintingNodeBase
  {
    protected readonly Size pageSize;
    protected readonly DataNodeContainer nodeContainer;

    protected override bool IsDetailContainerCore
    {
      get
      {
        return this.GetIsDetailContainerCore();
      }
    }

    public ContainerPrintingNodeBase(DataNodeContainer nodeContainer, DataTreeBuilder treeBuilder, IDataNode parent, int index, Size pageSize)
      : base(treeBuilder, parent, index)
    {
      this.pageSize = pageSize;
      this.nodeContainer = nodeContainer;
    }

    protected abstract bool GetIsDetailContainerCore();

    protected override IDataNode GetChildCore(int index)
    {
      RowNode rowNode = this.nodeContainer.Items[index];
      return DataTreeBuilder.CreateRowElement<IDataNode>(!this.IsDetailContainerCore, (Func<IDataNode>) (() => this.CreateGroupChildNode(rowNode, index)), (Func<IDataNode>) (() => this.CreateChildNode((NodeContainer) this.nodeContainer, rowNode, (IDataNode) this, index)));
    }

    protected abstract IDataNode CreateGroupChildNode(RowNode rowNode, int index);

    protected abstract IDataNode CreateChildNode(NodeContainer container, RowNode rowNode, IDataNode parentNode, int index);

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
