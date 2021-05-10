// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupSummaryLayoutCalculator
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;

namespace DevExpress.Xpf.Grid
{
  internal static class GroupSummaryLayoutCalculator
  {
    internal static bool IsFirstColumn(ColumnBase column, RowData rowData)
    {
      if (column.ParentBand != null)
      {
        if (column.ParentBand.VisibleIndex == 0 && column.BandRow != null)
          return column.BandRow.Columns[0] == column;
        return false;
      }
      IList<ColumnBase> ownedColumns = GroupSummaryLayoutCalculator.GetOwnedColumns(column, rowData);
      if (ownedColumns != null && ownedColumns.Count > 0)
        return ownedColumns[0] == column;
      return false;
    }

    private static IList<ColumnBase> GetOwnedColumns(ColumnBase column, RowData rowData)
    {
      if (rowData == null || rowData.VisualDataTreeBuilder == null)
        return (IList<ColumnBase>) null;
      IList<ColumnBase> fixedLeftColumns = rowData.VisualDataTreeBuilder.GetFixedLeftColumns();
      if (fixedLeftColumns != null && fixedLeftColumns.Count > 0)
        return fixedLeftColumns;
      return rowData.VisualDataTreeBuilder.GetFixedNoneColumns();
    }

    internal static bool IsFirstVisibleColumn(ColumnBase column, RowData rowData)
    {
      if (column == null || column.View == null || (rowData == null || rowData.VisualDataTreeBuilder == null))
        return false;
      ITableView tableView = column.View as ITableView;
      IList<ColumnBase> fixedLeftColumns = rowData.VisualDataTreeBuilder.GetFixedLeftColumns();
      if (fixedLeftColumns != null && fixedLeftColumns.Count > 0)
        return column == fixedLeftColumns[0];
      if (tableView == null || tableView.ViewportVisibleColumns == null || tableView.ViewportVisibleColumns.Count == 0)
        return false;
      return column == tableView.ViewportVisibleColumns[0];
    }
  }
}
