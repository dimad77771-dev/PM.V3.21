// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GridRootPrintingNode
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Utils;
using DevExpress.Xpf.Grid.Native;
using DevExpress.XtraPrinting.DataNodes;
using System;
using System.Windows;

namespace DevExpress.Xpf.Grid.Printing
{
  public class GridRootPrintingNode : GridContainerPrintingNodeBase, IRootDataNode, IDataNode, IDisposable
  {
    protected override bool IsDetailContainerCore
    {
      get
      {
        return false;
      }
    }

    public GridRootPrintingNode(PrintingDataTreeBuilderBase treeBuilder, Size pageSize)
      : base((DataNodeContainer) null, treeBuilder, (IDataNode) null, -1, pageSize)
    {
    }

    protected override bool CanGetChildCore(int index)
    {
      return index < 1;
    }

    protected override IDataNode GetChildCore(int index)
    {
      return (IDataNode) new GridGroupRootPrintingNode((PrintingDataTreeBuilderBase) this.TreeBuilder, (IDataNode) this, this.pageSize);
    }

    private int GetMasterRowsCount()
    {
      VirtualItemsEnumerator virtualItemsEnumerator = new VirtualItemsEnumerator((NodeContainer) this.TreeBuilder.RootNodeContainer);
      int num = 0;
      while (virtualItemsEnumerator.MoveNext())
        ++num;
      return num;
    }

    private int GetExpandedDetailsRowsCount()
    {
      int num = 0;
      GridPrintingDataTreeBuilder printingDataTreeBuilder = this.TreeBuilder as GridPrintingDataTreeBuilder;
      if (printingDataTreeBuilder == null || !printingDataTreeBuilder.View.AllowPrintDetails.ToBoolean(true))
        return num;
      return this.GetExpandedDetailsRowsCountCore(printingDataTreeBuilder.View.Grid);
    }

    private int GetExpandedDetailsRowsCountCore(GridControl grid)
    {
      int num = 0;
      for (int visibleIndex = 0; visibleIndex < grid.VisibleRowCount; ++visibleIndex)
      {
        int handleByVisibleIndex = grid.GetRowHandleByVisibleIndex(visibleIndex);
        if (grid.IsMasterRowExpanded(handleByVisibleIndex, (DetailDescriptorBase) null))
        {
          DataControlDetailDescriptor detailDescriptor = MasterDetailPrintHelper.GetActiveDetailDescriptor((GridPrintingDataTreeBuilder) this.TreeBuilder, handleByVisibleIndex, grid, false);
          if (detailDescriptor != null)
          {
            GridControl grid1 = grid.MasterDetailProvider.FindDetailDataControl(handleByVisibleIndex, detailDescriptor) as GridControl;
            if (grid1 != null && grid1.View is TableView)
            {
              num += grid1.VisibleRowCount;
              if (((TableView) grid1.View).AllowPrintDetails.ToBoolean(true))
                num += this.GetExpandedDetailsRowsCountCore(grid1);
            }
          }
        }
      }
      return num;
    }

    private bool IsPrintAllDetails()
    {
      GridPrintingDataTreeBuilder printingDataTreeBuilder = this.TreeBuilder as GridPrintingDataTreeBuilder;
      if (printingDataTreeBuilder == null)
        return false;
      return printingDataTreeBuilder.View.PrintAllDetails.ToBoolean(false);
    }

    int IRootDataNode.GetTotalDetailCount()
    {
      if (this.IsPrintAllDetails())
        return -1;
      return this.GetMasterRowsCount() + this.GetExpandedDetailsRowsCount();
    }

    void IDisposable.Dispose()
    {
      this.TreeBuilder.OnRootNodeDispose();
    }
  }
}
