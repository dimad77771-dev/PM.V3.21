// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.ItemsGenerationSimpleStrategy
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Grid.Printing
{
  public class ItemsGenerationSimpleStrategy : ItemsGenerationStrategyBase
  {
    private Dictionary<ColumnBase, string> TotalSummaries { get; set; }

    private string FixedLeftSummaryText { get; set; }

    private string FixedRightSummaryText { get; set; }

    public override bool RequireFullExpand
    {
      get
      {
        return false;
      }
    }

    public ItemsGenerationSimpleStrategy(DataViewBase view)
      : base(view)
    {
      this.StoreTotalSummary();
    }

    public override string GetTotalSummaryText(ColumnBase column)
    {
      if (!this.TotalSummaries.ContainsKey(column))
        return string.Empty;
      return this.TotalSummaries[column] ?? string.Empty;
    }

    public override string GetFixedTotalSummaryLeftText()
    {
      return this.FixedLeftSummaryText;
    }

    public override string GetFixedTotalSummaryRightText()
    {
      return this.FixedRightSummaryText;
    }

    private void StoreTotalSummary()
    {
      this.TotalSummaries = this.View.DataControl.ColumnsCore.Cast<ColumnBase>().ToDictionary<ColumnBase, ColumnBase, string>((Func<ColumnBase, ColumnBase>) (c => c), (Func<ColumnBase, string>) (c => c.TotalSummaryText));
      this.FixedLeftSummaryText = this.View.DataControl.viewCore.GetFixedSummariesLeftString();
      this.FixedRightSummaryText = this.View.DataControl.viewCore.GetFixedSummariesRightString();
    }

    public override object GetRowValue(RowData rowData)
    {
      return this.DataProvider.GetRowByListIndex(rowData.DataRowNode.PrintInfo.ListIndex);
    }

    public override object GetCellValue(RowData rowData, string fieldName)
    {
      if (TableView.IsCheckBoxSelectorColumn(fieldName))
        return (object) rowData.DataRowNode.PrintInfo.IsSelected;
      return ((GridDataProvider) this.DataProvider).GetRowValueByListIndex(rowData.DataRowNode.PrintInfo.ListIndex, fieldName);
    }
  }
}
