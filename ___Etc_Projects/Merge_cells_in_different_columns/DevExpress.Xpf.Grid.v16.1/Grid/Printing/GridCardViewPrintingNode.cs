// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GridCardViewPrintingNode
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting.DataNodes;
using System;

namespace DevExpress.Xpf.Grid.Printing
{
  public class GridCardViewPrintingNode : CardViewPrintingNodeBase, IVisualDetailNode, IDataNode
  {
    private readonly RowNode rowNode;
    private readonly NodeContainer parentContainer;

    protected override bool IsDetailContainerCore
    {
      get
      {
        return false;
      }
    }

    public GridCardViewPrintingNode(NodeContainer parentContainer, RowNode rowNode, CardViewPrintingDataTreeBuilder treeBuilder, IDataNode parent, int index)
      : base(treeBuilder, parent, index)
    {
      this.rowNode = rowNode;
      this.parentContainer = parentContainer;
    }

    RowViewInfo IVisualDetailNode.GetDetail(bool allowContentReuse)
    {
      this.TreeBuilder.ReusingRowData.AssignFromInternal((RowsContainer) null, this.parentContainer, this.rowNode, false);
      this.TreeBuilder.ReusingRowData.UpdateCellDataEditorsDisplayText();
      return this.CreateRowElement(this.TreeBuilder.ReusingRowData, this.TreeBuilder.PrintRowTemplate);
    }

    protected override IDataNode GetChildCore(int index)
    {
      throw new NotSupportedException();
    }

    protected override bool CanGetChildCore(int index)
    {
      return false;
    }
  }
}
