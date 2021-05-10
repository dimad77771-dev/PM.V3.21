// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.PrintingDataTreeBuilder
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.XtraPrinting.DataNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DevExpress.Xpf.Grid.Printing
{
  public abstract class PrintingDataTreeBuilder : PrintingDataTreeBuilderBase
  {
    private MasterDetailPrintInfo masterDetailPrintInfo;

    public ITableView PrintView
    {
      get
      {
        return this.View as ITableView;
      }
    }

    protected BandsLayoutBase BandsLayout { get; private set; }

    protected PrintRowInfoBase basePrintRowInfo { get; set; }

    public override bool SupportsMasterDetail
    {
      get
      {
        return false;
      }
    }

    protected internal MasterDetailPrintInfo MasterDetailPrintInfo
    {
      get
      {
        if (this.masterDetailPrintInfo == null)
          this.masterDetailPrintInfo = this.GetDefaultMasterDetailPrintInfo();
        return this.masterDetailPrintInfo;
      }
      protected set
      {
        this.masterDetailPrintInfo = value;
      }
    }

    public PrintingDataTreeBuilder(DataViewBase view, double totalHeaderWidth, BandsLayoutBase bandsLayout, MasterDetailPrintInfo masterDetailPrintInfo = null)
      : base(view, totalHeaderWidth)
    {
      this.BandsLayout = bandsLayout;
      this.MasterDetailPrintInfo = masterDetailPrintInfo;
      this.basePrintRowInfo = this.GetHeaderFooterPrintInfo();
      this.SetHeadersPrintRowInfo(this.HeadersData, this.basePrintRowInfo);
      this.UpdateBandData();
      this.HeadersData.UpdateCellData();
      this.UpdateIsTotalSummaryLeftBorderVisible();
    }

    protected virtual MasterDetailPrintInfo GetDefaultMasterDetailPrintInfo()
    {
      return (MasterDetailPrintInfo) null;
    }

    protected abstract double GetHeaderFooterLeftIndent();

    protected abstract void SetHeadersPrintRowInfo(HeadersData headersData, PrintRowInfoBase printRowInfo);

    private void UpdateIsTotalSummaryLeftBorderVisible()
    {
      if (this.BandsLayout != null)
        return;
      List<KeyValuePair<ColumnBase, GridColumnData>> list = this.HeadersData.CellDataCache.ToList<KeyValuePair<ColumnBase, GridColumnData>>();
      for (int index = 0; index < list.Count; ++index)
        this.UpdateIsLeftTotalSummaryBorderVisible(list[index].Key, (ColumnsRowDataBase) this.HeadersData, GridPrintingHelper.GetPrintCellInfo((DependencyObject) list[index].Value));
    }

    private void UpdateBandData()
    {
      if (this.BandsLayout == null)
        return;
      this.SetCloneBandPrintInfo(this.View.DataControl.BandsLayoutCore.PrintableBands, this.BandsLayout.VisibleBands);
    }

    private void SetCloneBandPrintInfo(IEnumerable<BandBase> sourceBands, List<BandBase> cloneBands)
    {
      List<BandBase> list = sourceBands.ToList<BandBase>();
      for (int index = 0; index < list.Count; ++index)
        this.SetCloneBandPrintInfo(list[index], cloneBands[index]);
    }

    private void SetCloneBandPrintInfo(BandBase source, BandBase clone)
    {
      PrintCellInfo printCellInfo = new PrintCellInfo(false, (string) null, (Style) null, GridPrintingHelper.GetPrintColumnWidth((DependencyObject) clone), source.HeaderCaption, source.ActualPrintBandHeaderStyle, (Style) null, this.basePrintRowInfo.IsPrintColumnHeadersVisible, 0, source.HorizontalHeaderContentAlignment, clone.HasTopElement, !clone.HasRightSibling);
      GridPrintingHelper.SetPrintCellInfo((DependencyObject) clone, printCellInfo);
      PrintBandInfo printBandInfo = new PrintBandInfo(source);
      GridPrintingHelper.SetPrintBandInfo((DependencyObject) clone, printBandInfo);
      this.SetCloneBandPrintInfo(source.PrintableBands, clone.VisibleBands);
    }

    protected virtual PrintRowInfoBase GetHeaderFooterPrintInfo()
    {
      return this.CreateBasePrintRowInfo();
    }

    protected abstract PrintRowInfoBase CreateBasePrintRowInfo();

    protected virtual int GetActualRowLevel(ColumnsRowDataBase rowData)
    {
      return rowData.Level;
    }

    internal override void UpdateColumnData(ColumnsRowDataBase rowData, GridColumnData cellData, ColumnBase column)
    {
      base.UpdateColumnData(rowData, cellData, column);
      PrintCellInfo basePrintCellInfo = this.GetBasePrintCellInfo(cellData, column);
      PrintCellInfo printCellInfo;
      if (this.IsFirstColumn(cellData.Column))
      {
        printCellInfo = GridPrintingHelper.GetPrintCellInfo((DependencyObject) cellData);
        double num1 = basePrintCellInfo.PrintColumnWidth - 20.0 * (double) this.GetActualRowLevel(rowData);
        if (printCellInfo == null)
        {
          int num2 = basePrintCellInfo.IsLast ? 1 : 0;
          string totalSummaryText = basePrintCellInfo.TotalSummaryText;
          Style totalSummaryStyle = basePrintCellInfo.PrintTotalSummaryStyle;
          double printColumnWidth = num1;
          object headerCaption = basePrintCellInfo.HeaderCaption;
          Style columnHeaderStyle = basePrintCellInfo.PrintColumnHeaderStyle;
          Style printCellStyle = basePrintCellInfo.PrintCellStyle;
          int num3 = basePrintCellInfo.IsColumnHeaderVisible ? 1 : 0;
          HorizontalAlignment contentAlignment = basePrintCellInfo.HorizontalHeaderContentAlignment;
          int detailLevel = this.GetDetailLevel();
          int num4 = (int) contentAlignment;
          int num5 = basePrintCellInfo.HasTopElement ? 1 : 0;
          int num6 = basePrintCellInfo.IsRight ? 1 : 0;
          printCellInfo = new PrintCellInfo(num2 != 0, totalSummaryText, totalSummaryStyle, printColumnWidth, headerCaption, columnHeaderStyle, printCellStyle, num3 != 0, detailLevel, (HorizontalAlignment) num4, num5 != 0, num6 != 0);
        }
        else
          printCellInfo.PrintColumnWidth = num1;
      }
      else
        printCellInfo = basePrintCellInfo;
      GridPrintingHelper.SetPrintCellInfo((DependencyObject) cellData, printCellInfo);
      if (GridPrintingHelper.GetPrintFixedFooterTextLeft((DependencyObject) this.HeadersData) == null)
        GridPrintingHelper.SetPrintFixedFooterTextLeft((DependencyObject) this.HeadersData, this.GetFixedLeftTotalSummaryText());
      if (GridPrintingHelper.GetPrintFixedFooterTextRight((DependencyObject) this.HeadersData) != null)
        return;
      GridPrintingHelper.SetPrintFixedFooterTextRight((DependencyObject) this.HeadersData, this.GetFixedRightTotalSummaryText());
    }

    protected virtual void UpdateTotalSummary(ColumnsRowDataBase rowData)
    {
      foreach (GridColumnData gridColumnData in (IEnumerable<GridColumnData>) this.HeadersData.CellData)
      {
        PrintCellInfo printCellInfo = GridPrintingHelper.GetPrintCellInfo((DependencyObject) gridColumnData);
        printCellInfo.TotalSummaryText = this.GetTotalSummaryText(gridColumnData.Column);
        this.UpdateIsLeftTotalSummaryBorderVisible(gridColumnData.Column, (ColumnsRowDataBase) this.HeadersData, printCellInfo);
      }
      GridPrintingHelper.SetPrintFixedFooterTextLeft((DependencyObject) this.HeadersData, this.GetFixedLeftTotalSummaryText());
      GridPrintingHelper.SetPrintFixedFooterTextRight((DependencyObject) this.HeadersData, this.GetFixedRightTotalSummaryText());
    }

    protected virtual string GetFixedLeftTotalSummaryText()
    {
      return this.View.GetFixedSummariesLeftString();
    }

    protected virtual string GetFixedRightTotalSummaryText()
    {
      return this.View.GetFixedSummariesRightString();
    }

    private bool IsFirstColumn(ColumnBase column)
    {
      if (this.BandsLayout != null)
        return !GridPrintingHelper.GetPrintHasLeftSibling((DependencyObject) column);
      return column == this.GetVisibleColumns().First<ColumnBase>();
    }

    private bool IsLastColumn(ColumnBase column)
    {
      return column == this.GetVisibleColumns().Last<ColumnBase>();
    }

    private bool IsRightColumn(ColumnBase column)
    {
      if (this.BandsLayout != null)
        return !GridPrintingHelper.GetPrintHasRightSibling((DependencyObject) column);
      return this.IsLastColumn(column);
    }

    private void UpdateIsLeftTotalSummaryBorderVisible(ColumnBase column, ColumnsRowDataBase rowData, PrintCellInfo printCellInfo)
    {
      if (column.IsFirst || !string.IsNullOrWhiteSpace(this.GetTotalSummaryText(column)))
      {
        printCellInfo.IsTotalSummaryLeftBorderVisible = true;
      }
      else
      {
        int prevVisibleIndex = column.VisibleIndex - 1;
        GridColumnData gridColumnData = rowData.CellDataCache.Values.FirstOrDefault<GridColumnData>((Func<GridColumnData, bool>) (cd => cd.VisibleIndex == prevVisibleIndex));
        if (gridColumnData == null || gridColumnData.Column == null)
          printCellInfo.IsTotalSummaryLeftBorderVisible = true;
        else
          printCellInfo.IsTotalSummaryLeftBorderVisible = !string.IsNullOrWhiteSpace(this.GetTotalSummaryText(gridColumnData.Column));
      }
    }

    protected virtual string GetTotalSummaryText(ColumnBase column)
    {
      return column.TotalSummaryText;
    }

    protected PrintCellInfo GetBasePrintCellInfo(ColumnBase column)
    {
      return GridPrintingHelper.GetPrintCellInfo((DependencyObject) this.HeadersData.GetCellDataByColumn(column, false)) ?? new PrintCellInfo(this.IsLastColumn(column), " ", column.ActualPrintTotalSummaryStyle, GridPrintingHelper.GetPrintColumnWidth((DependencyObject) column), column.HeaderCaption, column.ActualPrintColumnHeaderStyle, column.ActualPrintCellStyle, this.basePrintRowInfo != null && this.basePrintRowInfo.IsPrintColumnHeadersVisible, this.GetDetailLevel(), column.HorizontalHeaderContentAlignment, (this.basePrintRowInfo != null ? (this.basePrintRowInfo.IsPrintBandHeadersVisible ? 1 : 0) : 0) != 0 || column.HasTopElement, this.IsRightColumn(column));
    }

    private PrintCellInfo GetBasePrintCellInfo(GridColumnData cellData, ColumnBase column)
    {
      return this.GetBasePrintCellInfo(column);
    }

    internal override ColumnBase GetGroupColumnByNode(DataRowNode node)
    {
      return (ColumnBase) null;
    }

    internal override object GetGroupValueByNode(DataRowNode node)
    {
      return (object) null;
    }

    internal override IList<SummaryItemBase> GetGroupSummaries()
    {
      return (IList<SummaryItemBase>) null;
    }

    internal override bool TryGetGroupSummaryValue(RowData rowData, SummaryItemBase item, out object value)
    {
      value = (object) null;
      return false;
    }

    internal override string GetGroupRowDisplayTextByNode(DataRowNode node)
    {
      return (string) null;
    }

    internal override string GetGroupRowHeaderCaptionByNode(DataRowNode node)
    {
      return (string) null;
    }

    internal override GroupTextHighlightingProperties GetGroupHighlightingPropertiesByNode(DataRowNode node)
    {
      return (GroupTextHighlightingProperties) null;
    }

    protected bool IsPrintFooters()
    {
      if (!this.IsPrintFooter())
        return this.IsPrintFixedFooter();
      return true;
    }

    protected bool IsPrintFooter()
    {
      if (this.IsPrintTotalSummary() && this.View.ShowTotalSummary)
        return this.PrintFooterTemplate != null;
      return false;
    }

    protected bool IsPrintFixedFooter()
    {
      if (this.IsPrintFixedTotalSummary() && this.View.ShowFixedTotalSummary)
        return this.PrintFixedFooterTemplate != null;
      return false;
    }

    protected abstract bool IsPrintTotalSummary();

    protected abstract bool IsPrintFixedTotalSummary();

    protected virtual int GetDetailLevel()
    {
      return 0;
    }

    public abstract IDataNode CreateMasterDetailPrintingNode(NodeContainer container, RowNode rowNode, IDataNode parentNode, int index, Size pageSize);

    protected override IList<ColumnBase> GetPrintableColumns()
    {
      if (this.BandsLayout != null)
        return (IList<ColumnBase>) this.BandsLayout.GetVisibleColumns();
      return base.GetPrintableColumns();
    }
  }
}
