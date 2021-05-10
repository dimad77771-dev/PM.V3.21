// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeBuilderPrintingHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public static class TreeBuilderPrintingHelper
  {
    public static IList<SummaryItemBase> CloneGroupSummaries(GridSummaryItemCollection groupSummary)
    {
      return TreeBuilderPrintingHelper.CopyList<SummaryItemBase>((IList<SummaryItemBase>) new SimpleBridgeList<SummaryItemBase, GridSummaryItem>((IList<GridSummaryItem>) groupSummary, (Func<GridSummaryItem, SummaryItemBase>) (item =>
      {
        return (SummaryItemBase) new GridSummaryItem() { DisplayFormat = item.DisplayFormat, FieldName = item.FieldName, ShowInColumn = item.ShowInColumn, SummaryType = item.SummaryType, Visible = item.Visible, ShowInGroupColumnFooter = item.ShowInGroupColumnFooter };
      }), (Func<SummaryItemBase, GridSummaryItem>) null));
    }

    private static IList<T> CopyList<T>(IList<T> source)
    {
      List<T> objList = new List<T>(source.Count);
      foreach (T obj in (IEnumerable<T>) source)
        objList.Add(obj);
      return (IList<T>) objList;
    }

    public static void UpdatePrintGroupRowInfo(PrintRowInfoBase rowInfo, GroupRowData groupRowData, GroupSummaryDisplayMode groupSummaryDisplayMode, GroupSummaryDisplayMode printGroupSummaryDisplayMode, string groupRowText, int detailLevel)
    {
      PrintGroupRowInfo printGroupRowInfo = new PrintGroupRowInfo();
      printGroupRowInfo.IsExpanded = groupRowData.IsRowExpanded;
      printGroupRowInfo.CaptionCell = TreeBuilderPrintingHelper.CreateGroupCaptionCellInfo(groupRowData, rowInfo, printGroupRowInfo, printGroupSummaryDisplayMode, detailLevel);
      printGroupRowInfo.GroupCells = TreeBuilderPrintingHelper.CreateGroupCellInfos(groupRowData, rowInfo, printGroupRowInfo, groupSummaryDisplayMode, printGroupSummaryDisplayMode, detailLevel);
      printGroupRowInfo.FirstColumnCell = TreeBuilderPrintingHelper.CreateFirstColumnGroupCellInfo(groupRowData, rowInfo, printGroupRowInfo, groupSummaryDisplayMode, printGroupSummaryDisplayMode, groupRowText, detailLevel);
      printGroupRowInfo.IsLast = ((DataRowNode) groupRowData.node).PrintInfo.IsLast;
      TreeBuilderPrintingHelper.ApplyNearEmptyInfos(printGroupRowInfo);
      TreeBuilderPrintingHelper.UpdateBorders(printGroupRowInfo);
      GridPrintingHelper.SetPrintGroupRowInfo((DependencyObject) groupRowData, printGroupRowInfo);
    }

    private static void UpdateBorders(PrintGroupRowInfo groupRowInfo)
    {
      groupRowInfo.CaptionCell.HasLeftBorder = true;
      groupRowInfo.CaptionCell.HasRightBorder = !TreeBuilderPrintingHelper.HasSummaryValue(groupRowInfo.FirstColumnCell) && groupRowInfo.GroupCells.Count == 0;
      groupRowInfo.FirstColumnCell.HasLeftBorder = false;
      groupRowInfo.FirstColumnCell.HasRightBorder = groupRowInfo.GroupCells.Count == 0;
      for (int index = 0; index < groupRowInfo.GroupCells.Count; ++index)
      {
        PrintGroupRowCellInfo cellInfo1 = groupRowInfo.GroupCells[index];
        if (index == 0)
        {
          cellInfo1.HasLeftBorder = TreeBuilderPrintingHelper.HasSummaryValue(cellInfo1) || TreeBuilderPrintingHelper.HasSummaryValue(groupRowInfo.FirstColumnCell);
        }
        else
        {
          PrintGroupRowCellInfo cellInfo2 = groupRowInfo.GroupCells[index - 1];
          cellInfo1.HasLeftBorder = TreeBuilderPrintingHelper.HasSummaryValue(cellInfo1) || TreeBuilderPrintingHelper.HasSummaryValue(cellInfo2);
        }
        if (index == groupRowInfo.GroupCells.Count - 1)
          cellInfo1.HasRightBorder = true;
      }
    }

    private static PrintGroupRowCellInfo CreateFirstColumnGroupCellInfo(GroupRowData rowData, PrintRowInfoBase rowInfo, PrintGroupRowInfo groupRowInfo, GroupSummaryDisplayMode groupSummaryDisplayMode, GroupSummaryDisplayMode printGroupSummaryDisplayMode, string groupRowText, int detailLevel)
    {
      PrintGroupRowCellInfo groupRowCellInfo = TreeBuilderPrintingHelper.CreatePrintGroupRowCellInfo(rowData, rowInfo, groupRowInfo, detailLevel);
      groupRowCellInfo.VisibleIndex = 1;
      groupRowCellInfo.Text = printGroupSummaryDisplayMode == GroupSummaryDisplayMode.Default ? groupRowText : TreeBuilderPrintingHelper.GetAlignedGroupSummaryText(rowData, rowData.CellData[0].Column, groupSummaryDisplayMode, printGroupSummaryDisplayMode);
      groupRowCellInfo.Position = groupRowInfo.GroupCells.Count == 0 ? PrintGroupCellPosition.Separator : PrintGroupCellPosition.None;
      return groupRowCellInfo;
    }

    private static string GetAlignedGroupSummaryText(GroupRowData rowData, ColumnBase column, GroupSummaryDisplayMode groupSummaryDisplayMode, GroupSummaryDisplayMode printGroupSummaryDisplayMode)
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < rowData.GroupSummaryData.Count; ++index)
      {
        if (rowData.GroupSummaryData[index].Column == column)
          stringList.Add(TreeBuilderPrintingHelper.GetGroupSummaryText(rowData, rowData.GroupSummaryData[index], groupSummaryDisplayMode, printGroupSummaryDisplayMode));
      }
      return string.Join(", ", (IEnumerable<string>) stringList);
    }

    public static string GetGroupRowText(GroupRowData rowData, GroupSummaryDisplayMode groupSummaryDisplayMode, GroupSummaryDisplayMode printGroupSummaryDisplayMode, string stringFormat, bool useFirstIndent)
    {
      GridGroupValueData groupValue = rowData.GroupValue;
      string str = "";
      if (rowData.GroupSummaryData.Count > 0 && useFirstIndent)
        str += " ";
      for (int index = 0; index < rowData.GroupSummaryData.Count; ++index)
      {
        str += string.Format(stringFormat, (object) TreeBuilderPrintingHelper.GetGroupSummaryText(rowData, rowData.GroupSummaryData[index], groupSummaryDisplayMode, printGroupSummaryDisplayMode));
        if (!rowData.GroupSummaryData[index].IsLast)
          str += ", ";
      }
      return str;
    }

    private static string GetGroupSummaryText(GroupRowData item, GridGroupSummaryData summaryData, GroupSummaryDisplayMode groupSummaryDisplayMode, GroupSummaryDisplayMode printGroupSummaryDisplayMode)
    {
      if (printGroupSummaryDisplayMode == groupSummaryDisplayMode)
        return summaryData.Text;
      object obj = (object) null;
      item.treeBuilder.TryGetGroupSummaryValue((RowData) item, (SummaryItemBase) summaryData.SummaryItem, out obj);
      TableView tableView = item.View as TableView;
      if (tableView == null)
        return (string) null;
      return item.GetGroupSummaryText(summaryData.SummaryItem, summaryData.Column, tableView, obj, true);
    }

    private static void ApplyNearEmptyInfos(PrintGroupRowInfo rowInfo)
    {
      List<PrintGroupRowCellInfo> source = new List<PrintGroupRowCellInfo>((IEnumerable<PrintGroupRowCellInfo>) rowInfo.GroupCells);
      source.Add(rowInfo.CaptionCell);
      source.Add(rowInfo.FirstColumnCell);
      foreach (PrintGroupRowCellInfo groupRowCellInfo1 in source)
      {
        int leftVisibleIndex = groupRowCellInfo1.VisibleIndex - 1;
        PrintGroupRowCellInfo groupRowCellInfo2 = source.FirstOrDefault<PrintGroupRowCellInfo>((Func<PrintGroupRowCellInfo, bool>) (i => i.VisibleIndex == leftVisibleIndex));
        if (groupRowCellInfo2 == null || string.IsNullOrEmpty(groupRowCellInfo2.Text))
          groupRowCellInfo1.IsLeftGroupSummaryValueEmpty = true;
        int rightVisibleIndex = groupRowCellInfo1.VisibleIndex + 1;
        PrintGroupRowCellInfo groupRowCellInfo3 = source.FirstOrDefault<PrintGroupRowCellInfo>((Func<PrintGroupRowCellInfo, bool>) (i => i.VisibleIndex == rightVisibleIndex));
        if (groupRowCellInfo3 == null || string.IsNullOrEmpty(groupRowCellInfo3.Text))
          groupRowCellInfo1.IsRightGroupSummaryValueEmpty = true;
      }
    }

    private static PrintGroupRowCellInfo CreateGroupCaptionCellInfo(GroupRowData rowData, PrintRowInfoBase rowInfo, PrintGroupRowInfo groupRowInfo, GroupSummaryDisplayMode printGroupSummaryDisplayMode, int detailLevel)
    {
      PrintGroupRowCellInfo groupRowCellInfo = TreeBuilderPrintingHelper.CreatePrintGroupRowCellInfo(rowData, rowInfo, groupRowInfo, detailLevel);
      groupRowCellInfo.Text = TreeBuilderPrintingHelper.GetGroupRowTextStart(rowData, (GridColumnData) rowData.GroupValue);
      groupRowCellInfo.Width = TreeBuilderPrintingHelper.GetFirstColumnWidth((RowData) rowData, rowInfo, printGroupSummaryDisplayMode);
      groupRowCellInfo.VisibleIndex = 0;
      groupRowCellInfo.Position = PrintGroupCellPosition.Default;
      return groupRowCellInfo;
    }

    private static bool HasSummaryValue(PrintGroupRowCellInfo cellInfo)
    {
      return !string.IsNullOrWhiteSpace(cellInfo.Text);
    }

    private static PrintGroupRowCellInfo CreatePrintGroupRowCellInfo(GroupRowData rowData, PrintRowInfoBase rowInfo, PrintGroupRowInfo groupRowInfo, int detailLevel)
    {
      PrintGroupRowCellInfo groupRowCellInfo = new PrintGroupRowCellInfo();
      groupRowCellInfo.DetailLevel = detailLevel;
      groupRowCellInfo.GroupLevel = rowData.GroupLevel;
      GridPrintingHelper.SetPrintGroupRowInfo((DependencyObject) groupRowCellInfo, groupRowInfo);
      groupRowCellInfo.PrintGroupRowStyle = rowInfo.PrintGroupRowStyle;
      return groupRowCellInfo;
    }

    private static double GetFirstColumnWidth(RowData rowData, PrintRowInfoBase rowInfo, GroupSummaryDisplayMode printGroupSummaryDisplayMode)
    {
      if (printGroupSummaryDisplayMode != GroupSummaryDisplayMode.Default)
        return GridPrintingHelper.GetPrintCellInfo((DependencyObject) rowData.CellData[0]).PrintColumnWidth;
      return rowInfo.TotalHeaderWidth;
    }

    private static string GetGroupRowTextStart(GroupRowData rowData, GridColumnData columnData)
    {
      return ((GroupRowNodePrintInfo) rowData.DataRowNode.PrintInfo).GroupDisplayText;
    }

    private static List<PrintGroupRowCellInfo> CreateGroupCellInfos(GroupRowData rowData, PrintRowInfoBase rowInfo, PrintGroupRowInfo groupRowInfo, GroupSummaryDisplayMode groupSummaryDisplayMode, GroupSummaryDisplayMode printGroupSummaryDisplayMode, int detailLevel)
    {
      if (printGroupSummaryDisplayMode == GroupSummaryDisplayMode.Default)
        return new List<PrintGroupRowCellInfo>();
      List<PrintGroupRowCellInfo> groupRowCellInfoList = new List<PrintGroupRowCellInfo>();
      for (int index = 1; index < rowData.CellData.Count; ++index)
      {
        PrintGroupRowCellInfo groupRowCellInfo = TreeBuilderPrintingHelper.CreatePrintGroupRowCellInfo(rowData, rowInfo, groupRowInfo, detailLevel);
        groupRowCellInfo.VisibleIndex = index + 1;
        groupRowCellInfo.Width = GridPrintingHelper.GetPrintCellInfo((DependencyObject) rowData.CellData[index]).PrintColumnWidth;
        groupRowCellInfo.Position = index == rowData.CellData.Count - 1 ? PrintGroupCellPosition.Last : PrintGroupCellPosition.Default;
        groupRowCellInfo.Text = TreeBuilderPrintingHelper.GetAlignedGroupSummaryText(rowData, rowData.CellData[index].Column, groupSummaryDisplayMode, printGroupSummaryDisplayMode);
        groupRowCellInfoList.Add(groupRowCellInfo);
      }
      return groupRowCellInfoList;
    }
  }
}
