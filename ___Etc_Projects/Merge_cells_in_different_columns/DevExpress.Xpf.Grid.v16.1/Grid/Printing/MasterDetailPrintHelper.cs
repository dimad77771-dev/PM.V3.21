// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.MasterDetailPrintHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Utils;
using DevExpress.Xpf.Grid.Native;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DevExpress.Xpf.Grid.Printing
{
  public class MasterDetailPrintHelper
  {
    public static bool IsMasterRowExpanded(GridPrintingDataTreeBuilder treeBuilder, int rowHandle)
    {
      return treeBuilder.View.Grid.IsMasterRowExpanded(treeBuilder.GetOriginalRowHandle(rowHandle), (DetailDescriptorBase) null);
    }

    public static bool IsDetailContainsRows(GridPrintingDataTreeBuilder treeBuilder, int masterRowHandle, DataControlBase detailDataControl)
    {
      if (!detailDataControl.DataView.PrintSelectedRowsOnly)
        return detailDataControl.VisibleRowCount > 0;
      PrintSelectedRowsInfo printSelectedRowsInfo = (PrintSelectedRowsInfo) null;
      return PrintSelectedRowsHelper.GetSelectedRows(detailDataControl.DataProviderBase, detailDataControl.viewCore, out printSelectedRowsInfo, (IList) null).Count > 0;
    }

    public static RowDetailInfoBase GetRowDetailInfoForPrinting(GridPrintingDataTreeBuilder treeBuilder, int rowHandle)
    {
      return ((MasterDetailProvider) treeBuilder.View.Grid.MasterDetailProvider).GetRowDetailInfoForPrinting(treeBuilder.GetOriginalRowHandle(rowHandle));
    }

    public static DataControlBase FindDetailDataControl(GridPrintingDataTreeBuilder treeBuilder, int rowHandle, DataControlDetailDescriptor descriptor)
    {
      return treeBuilder.View.Grid.MasterDetailProvider.FindDetailDataControl(treeBuilder.GetOriginalRowHandle(rowHandle), descriptor);
    }

    public static DataControlDetailDescriptor GetActiveDetailDescriptor(GridPrintingDataTreeBuilder treeBuilder, int rowHandle, GridControl grid = null, bool useOriginalRowHandle = true)
    {
      if (grid == null)
        grid = treeBuilder.View.Grid;
      if (grid.DetailDescriptor is DataControlDetailDescriptor)
        return grid.DetailDescriptor as DataControlDetailDescriptor;
      if (grid.DetailDescriptor is TabViewDetailDescriptor)
        return MasterDetailPrintHelper.GetActiveDetailDescriptor(grid, grid.DetailDescriptor as TabViewDetailDescriptor, useOriginalRowHandle ? treeBuilder.GetOriginalRowHandle(rowHandle) : rowHandle);
      return (DataControlDetailDescriptor) null;
    }

    private static DataControlDetailDescriptor GetActiveDetailDescriptor(GridControl grid, TabViewDetailDescriptor tabDescriptor, int rowHandle)
    {
      DetailDescriptorBase detailDescriptor = ((MasterDetailProvider) grid.MasterDetailProvider).GetRowDetailInfo(rowHandle).FindVisibleDetailDescriptor();
      if (detailDescriptor is DataControlDetailDescriptor)
        return detailDescriptor as DataControlDetailDescriptor;
      if (detailDescriptor is TabViewDetailDescriptor)
        return MasterDetailPrintHelper.GetActiveDetailDescriptor(grid, detailDescriptor as TabViewDetailDescriptor, rowHandle);
      return (DataControlDetailDescriptor) null;
    }

    public static List<DataControlDetailDescriptor> GetAllDetailDescriptors(DataControlBase grid)
    {
      GridControl gridControl = grid as GridControl;
      List<DataControlDetailDescriptor> result = new List<DataControlDetailDescriptor>();
      if (gridControl.DetailDescriptor is DataControlDetailDescriptor)
      {
        result.Add(gridControl.DetailDescriptor as DataControlDetailDescriptor);
        return result;
      }
      if (gridControl.DetailDescriptor is TabViewDetailDescriptor)
        MasterDetailPrintHelper.GetAllDetailDescriptorsFromTab((TabViewDetailDescriptor) gridControl.DetailDescriptor, result);
      return result;
    }

    private static void GetAllDetailDescriptorsFromTab(TabViewDetailDescriptor tabDescriptor, List<DataControlDetailDescriptor> result)
    {
      foreach (DetailDescriptorBase detailDescriptor in (Collection<DetailDescriptorBase>) tabDescriptor.DetailDescriptors)
      {
        if (detailDescriptor is DataControlDetailDescriptor)
          result.Add(detailDescriptor as DataControlDetailDescriptor);
        else if (detailDescriptor is TabViewDetailDescriptor)
          MasterDetailPrintHelper.GetAllDetailDescriptorsFromTab((TabViewDetailDescriptor) detailDescriptor, result);
      }
    }

    public static MasterDetailPrintInfo GetInheritedPrintInfo(TableView view, GridPrintingDataTreeBuilder TreeBuilder, PrintDetailType printDetailType)
    {
      DefaultBoolean allowPrintDetails = view.AllowPrintDetails == DefaultBoolean.Default ? TreeBuilder.MasterDetailPrintInfo.AllowPrintDetails : view.AllowPrintDetails;
      DefaultBoolean allowPrintEmptyDetails = view.AllowPrintEmptyDetails == DefaultBoolean.Default ? TreeBuilder.MasterDetailPrintInfo.AllowPrintEmptyDetails : view.AllowPrintEmptyDetails;
      DefaultBoolean printAllDetails = view.PrintAllDetails == DefaultBoolean.Default ? TreeBuilder.MasterDetailPrintInfo.PrintAllDetails : view.PrintAllDetails;
      int detailGroupLevel = TreeBuilder.MasterDetailPrintInfo.DetailGroupLevel + TreeBuilder.ReusingRowData.Level;
      return new MasterDetailPrintInfo(allowPrintDetails, allowPrintEmptyDetails, printAllDetails, TreeBuilder.MasterDetailPrintInfo.RootPrintingDataTreeBuilder, printDetailType, detailGroupLevel);
    }
  }
}
