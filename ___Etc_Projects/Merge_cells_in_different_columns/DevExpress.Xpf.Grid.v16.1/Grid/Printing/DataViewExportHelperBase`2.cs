// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.DataViewExportHelperBase`2
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Export.Xl;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraPrinting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace DevExpress.Xpf.Grid.Printing
{
  public abstract class DataViewExportHelperBase<TCol, TRow> : IGridView<TCol, TRow>, IGridViewBase<TCol, TRow, TCol, TRow> where TCol : ColumnWrapper where TRow : IRowBase
  {
    protected readonly List<TCol> visibleColumns;
    private readonly List<ISummaryItemEx> totalSummary;
    private readonly IEnumerable<IFormatRuleBase> formatRulesCore;
    private int totalBandRowCount;
    private int totalColumnRowCount;
    private List<ColumnBase> selectedColumns;

    public IEnumerable<IFormatRuleBase> FormatRules
    {
      get
      {
        return this.formatRulesCore;
      }
    }

    public IEnumerable<FormatConditionObject> FormatConditionsCollection
    {
      get
      {
        yield break;
      }
    }

    public abstract bool ShowGroupedColumns { get; }

    public IAdditionalSheetInfo AdditionalSheetInfo
    {
      get
      {
        return (IAdditionalSheetInfo) new AdditionalSheetInfoWrapper();
      }
    }

    public abstract IEnumerable<ISummaryItemEx> GridGroupSummaryItemCollection { get; }

    public abstract IEnumerable<ISummaryItemEx> GroupHeaderSummaryItems { get; }

    public IEnumerable<ISummaryItemEx> GridTotalSummaryItemCollection
    {
      get
      {
        return (IEnumerable<ISummaryItemEx>) this.totalSummary;
      }
    }

    public IEnumerable<ISummaryItemEx> FixedSummary
    {
      get
      {
        return this.GetFixedTotalSummary();
      }
    }

    public string FilterString
    {
      get
      {
        if (!object.ReferenceEquals((object) this.View.DataControl.DataProviderBase.FilterCriteria, (object) null))
          return this.View.DataControl.DataProviderBase.FilterCriteria.ToString().Replace("DxFts_", "");
        return (string) null;
      }
    }

    public virtual bool ShowFooter
    {
      get
      {
        if (!this.PrintTotalSummary)
          return this.PrintFixedTotalSummary;
        return true;
      }
    }

    public virtual bool ShowBandsPanel
    {
      get
      {
        return false;
      }
    }

    public virtual bool ShowGroupFooter
    {
      get
      {
        return true;
      }
    }

    public bool ReadOnly
    {
      get
      {
        return !this.View.AllowEditing;
      }
    }

    private bool PrintTotalSummary
    {
      get
      {
        return this.View.ShouldPrintTotalSummary;
      }
    }

    private bool PrintFixedTotalSummary
    {
      get
      {
        return this.View.ShouldPrintFixedTotalSummary;
      }
    }

    long IGridView<TCol, TRow>.RowCount
    {
      get
      {
        return this.RowCountCore;
      }
    }

    public bool ColumnAutoWidth
    {
      get
      {
        TableView tableView = this.View as TableView;
        if (tableView != null)
          return tableView.AutoWidth;
        return false;
      }
    }

    public string ViewCaption
    {
      get
      {
        return string.Empty;
      }
    }

    public IGridViewAppearance Appearance
    {
      get
      {
        return (IGridViewAppearance) new GridAppearanceWrapper();
      }
    }

    public IGridViewAppearancePrint AppearancePrint
    {
      get
      {
        return (IGridViewAppearancePrint) new GridAppearancePrintWrapper();
      }
    }

    public IGridOptionsBehavior OptionsBehavior
    {
      get
      {
        return (IGridOptionsBehavior) new GridOptionsBehaviorWrapper(this.View);
      }
    }

    public IGridOptionsView OptionsView
    {
      get
      {
        return (IGridOptionsView) new GridOptionsViewWrapper<TCol, TRow>(this);
      }
    }

    public int RowHeight
    {
      get
      {
        return -1;
      }
    }

    public int FixedRowsCount
    {
      get
      {
        return 0;
      }
    }

    public bool IsCancelPending
    {
      get
      {
        return false;
      }
    }

    public DataViewBase View { get; private set; }

    protected DataControlBase DataControl
    {
      get
      {
        return this.View.DataControl;
      }
    }

    protected DataProviderBase DataProvider
    {
      get
      {
        return this.View.DataProviderBase;
      }
    }

    protected abstract long RowCountCore { get; }

    protected abstract FormatConditionCollection FormatConditionsCore { get; }

    public DataViewExportHelperBase(DataViewBase view, ExportTarget target)
    {
      this.View = view;
      this.visibleColumns = this.GetPrintableColumns(view.PrintableColumns);
      this.totalSummary = this.GetFooterSummary().ToList<ISummaryItemEx>();
      this.formatRulesCore = this.GetFormatRules();
    }

    public abstract IEnumerable<TCol> GetGroupedColumns();

    public ISparklineInfo GetRowCellSparklineInfo(TRow row, TCol col)
    {
      return (ISparklineInfo) null;
    }

    public abstract bool GetAllowMerge(TCol col);

    public FormatSettings GetRowCellFormatting(TRow row, TCol col)
    {
      return (FormatSettings) null;
    }

    public string GetRowCellHyperlink(TRow row, TCol col)
    {
      return (string) null;
    }

    protected IEnumerable<ISummaryItemEx> GetPrintableSummary(ISummaryItemOwner items, Func<DevExpress.Xpf.Grid.SummaryItemBase, bool> canPrintSummaryItem, Func<DevExpress.Xpf.Grid.SummaryItemBase, ISummaryItemEx> createSummaryItemWrapper)
    {
      foreach (DevExpress.Xpf.Grid.SummaryItemBase summaryItemBase in (IEnumerable<DevExpress.Xpf.Grid.SummaryItemBase>) items)
      {
        if (summaryItemBase.Visible && canPrintSummaryItem(summaryItemBase))
          yield return createSummaryItemWrapper(summaryItemBase);
      }
    }

    protected string GetDisplayFormat(DevExpress.Xpf.Grid.SummaryItemBase item, DevExpress.Xpf.Grid.SummaryItemBase.ColumnSummaryType summaryType)
    {
      if (item.ActualShowInColumn == item.FieldName)
        return item.GetFooterDisplayFormatSameColumn(this.GetDisplayFormat(item.FieldName), summaryType);
      return item.GetFooterDisplayFormat(this.GetDisplayFormat(item.FieldName), summaryType);
    }

    protected string GetDisplayFormat(string fieldName)
    {
      ColumnBase columnBase = this.DataControl.ColumnsCore[fieldName];
      if (columnBase != null)
        return columnBase.DisplayFormat;
      return string.Empty;
    }

    protected string GetExportToColumn(string fieldName, string showInColumn)
    {
      if (!string.IsNullOrEmpty(showInColumn))
        return showInColumn;
      return fieldName;
    }

    protected string GetSummaryFieldName(DevExpress.Xpf.Grid.SummaryItemBase item, DevExpress.Xpf.Grid.SummaryItemBase.ColumnSummaryType summaryType)
    {
      if (!string.IsNullOrEmpty(item.FieldName) || item.SummaryType != SummaryItemType.Count)
        return item.FieldName;
      if (string.IsNullOrEmpty(item.ActualShowInColumn))
        return this.GetDefaultFieldName(item, summaryType);
      return item.ActualShowInColumn;
    }

    private string GetDefaultFieldName(DevExpress.Xpf.Grid.SummaryItemBase item, DevExpress.Xpf.Grid.SummaryItemBase.ColumnSummaryType summaryType)
    {
      if (summaryType == DevExpress.Xpf.Grid.SummaryItemBase.ColumnSummaryType.Total)
        return this.GetFixedTotalSummaryItemFieldName(item.Alignment);
      return this.GetLastVisibleColumnName();
    }

    private string GetLastVisibleColumnName()
    {
      return this.visibleColumns.Last<TCol>().Return<TCol, string>((Func<TCol, string>) (column => column.FieldName), (Func<string>) (() => string.Empty));
    }

    private IEnumerable<ISummaryItemEx> GetFooterSummary()
    {
      return (this.PrintTotalSummary ? this.GetTotalSummary() : Enumerable.Empty<ISummaryItemEx>()).Concat<ISummaryItemEx>(this.PrintFixedTotalSummary ? this.GetFixedTotalSummary() : Enumerable.Empty<ISummaryItemEx>());
    }

    private SummaryItemExportWrapper CreateTotalSummaryItemWrapper(DevExpress.Xpf.Grid.SummaryItemBase item, string fieldName, string showInColumn)
    {
      string displayFormat = this.GetDisplayFormat(item, DevExpress.Xpf.Grid.SummaryItemBase.ColumnSummaryType.Total);
      object totalSummaryValue = this.GetCustomTotalSummaryValue(item);
      return new SummaryItemExportWrapper(fieldName, showInColumn, item.SummaryType, displayFormat, totalSummaryValue, (Func<int, object>) (row => (object) null));
    }

    private IEnumerable<ISummaryItemEx> GetPrintableTotalSummary(Func<DevExpress.Xpf.Grid.SummaryItemBase, bool> canPrintSummaryItem, Func<DevExpress.Xpf.Grid.SummaryItemBase, ISummaryItemEx> createSummaryItemWrapper)
    {
      return this.GetPrintableSummary(this.DataControl.TotalSummaryCore, canPrintSummaryItem, createSummaryItemWrapper);
    }

    private IEnumerable<ISummaryItemEx> GetTotalSummary()
    {
      return this.GetPrintableTotalSummary(new Func<DevExpress.Xpf.Grid.SummaryItemBase, bool>(this.CanPrintTotalSummaryItem), new Func<DevExpress.Xpf.Grid.SummaryItemBase, ISummaryItemEx>(this.CreateTotalSummaryItemWrapper));
    }

    private bool CanPrintTotalSummaryItem(DevExpress.Xpf.Grid.SummaryItemBase item)
    {
      return item.Alignment == GridSummaryItemAlignment.Default;
    }

    private SummaryItemExportWrapper CreateTotalSummaryItemWrapper(DevExpress.Xpf.Grid.SummaryItemBase item)
    {
      return this.CreateTotalSummaryItemWrapper(item, item.FieldName, item.ActualShowInColumn);
    }

    private IEnumerable<ISummaryItemEx> GetFixedTotalSummary()
    {
      return this.GetPrintableTotalSummary(new Func<DevExpress.Xpf.Grid.SummaryItemBase, bool>(this.CanPrintFixedTotalSummaryItem), new Func<DevExpress.Xpf.Grid.SummaryItemBase, ISummaryItemEx>(this.CreateFixedTotalSummaryItemWrapper));
    }

    private bool CanPrintFixedTotalSummaryItem(DevExpress.Xpf.Grid.SummaryItemBase item)
    {
      if (string.IsNullOrEmpty(item.FieldName) && item.SummaryType != SummaryItemType.Count)
        return false;
      return item.Alignment != GridSummaryItemAlignment.Default;
    }

    private object GetCustomTotalSummaryValue(DevExpress.Xpf.Grid.SummaryItemBase item)
    {
      if (item.SummaryType == SummaryItemType.Custom)
        return this.DataControl.GetTotalSummaryValue(item);
      return (object) null;
    }

    private SummaryItemExportWrapper CreateFixedTotalSummaryItemWrapper(DevExpress.Xpf.Grid.SummaryItemBase item)
    {
      string summaryFieldName = this.GetSummaryFieldName(item, DevExpress.Xpf.Grid.SummaryItemBase.ColumnSummaryType.Total);
      return this.CreateTotalSummaryItemWrapper(item, summaryFieldName, this.GetExportToColumn(summaryFieldName, item.ActualShowInColumn));
    }

    private string GetFixedTotalSummaryItemFieldName(GridSummaryItemAlignment alignment)
    {
      if (this.visibleColumns.Count == 0)
        return string.Empty;
      switch (alignment)
      {
        case GridSummaryItemAlignment.Left:
          return this.visibleColumns.First<TCol>().FieldName;
        case GridSummaryItemAlignment.Right:
          return this.visibleColumns.Last<TCol>().FieldName;
        default:
          return string.Empty;
      }
    }

    public virtual IEnumerable<TCol> GetAllColumns()
    {
      return (IEnumerable<TCol>) this.visibleColumns;
    }

    protected virtual TCol CreateColumnCore(ColumnBase column, int logicalPosition)
    {
      return DataViewExportHelperBase<TCol, TRow>.CreateColumn(column, logicalPosition);
    }

    internal static TCol CreateColumn(ColumnBase column, int logicalPosition)
    {
      return (TCol) new ColumnWrapper(column, logicalPosition);
    }

    public TCol GetColumn(string fieldName)
    {
      return this.visibleColumns.FirstOrDefault<TCol>((Func<TCol, bool>) (column => column.FieldName == fieldName));
    }

    public virtual IEnumerable<TCol> GetSelectedColumns()
    {
      return (IEnumerable<TCol>) new TCol[0];
    }

    protected virtual List<TCol> GetPrintableColumns(IEnumerable<ColumnBase> processedColumns)
    {
      IEnumerable<ColumnBase> source = processedColumns.Cast<ColumnBase>();
      if (this.DataControl.BandsLayoutCore != null)
        source = this.GetPrintableColumns(this.DataControl.BandsLayoutCore.PrintableBands);
      return source.Select<ColumnBase, TCol>((Func<ColumnBase, int, TCol>) ((column, index) => this.CreateColumnCore(column, index))).ToList<TCol>();
    }

    private IEnumerable<ColumnBase> GetPrintableColumns(IEnumerable<BandBase> bands)
    {
      IEnumerable<ColumnBase> first = Enumerable.Empty<ColumnBase>();
      foreach (BandBase band in bands)
      {
        first = first.Concat<ColumnBase>(this.GetPrintableColumns(band.PrintableBands));
        first = first.Concat<ColumnBase>(band.ActualRows.SelectMany<BandRow, ColumnBase>((Func<BandRow, IEnumerable<ColumnBase>>) (row => (IEnumerable<ColumnBase>) row.Columns)).Where<ColumnBase>((Func<ColumnBase, bool>) (column => column.AllowPrinting)).Cast<ColumnBase>());
      }
      return first;
    }

    public abstract IEnumerable<TRow> GetAllRows();

    public object GetRowCellValue(TRow row, TCol col)
    {
      object rowCellValue = this.GetRowCellValue(row.LogicalPosition, col.LogicalPosition);
      if (col.ColEditType != ColumnEditTypes.Sparkline)
        return rowCellValue;
      return this.GetRowCellValueSparkline(col, rowCellValue);
    }

    public virtual object GetRowCellValue(int rowHandle, int visibleIndex)
    {
      if (visibleIndex < 0 || visibleIndex >= this.visibleColumns.Count)
        return (object) null;
      ColumnBase column = this.visibleColumns[visibleIndex].Column;
      return this.View.GetExportValue(rowHandle, column);
    }

    public virtual object GetRowCellValueSparkline(TCol col, object val)
    {
      try
      {
        SparklineEditSettings sparklineEditSettings = (SparklineEditSettings) col.Column.EditSettings;
        if (string.IsNullOrEmpty(sparklineEditSettings.PointValueMember))
          return val;
        IEnumerable enumerable = (IEnumerable) val;
        List<object> objectList = new List<object>();
        PropertyInfo propertyInfo = (PropertyInfo) null;
        foreach (object obj in enumerable)
        {
          if (propertyInfo == (PropertyInfo) null)
            propertyInfo = obj.GetType().GetProperty(sparklineEditSettings.PointValueMember);
          objectList.Add(propertyInfo.GetValue(obj, (object[]) null));
        }
        return (object) objectList;
      }
      catch
      {
        return val;
      }
    }

    public virtual int RaiseMergeEvent(int row1, int row2, TCol col)
    {
      return -1;
    }

    private IEnumerable<IFormatRuleBase> GetFormatRules()
    {
      foreach (FormatConditionBase formatCondition in (Collection<FormatConditionBase>) this.FormatConditionsCore)
      {
        TCol column = this.GetColumn(formatCondition.FieldName);
        if ((object) column != null)
        {
          FormatRuleExportWrapper rule = DataViewExportHelperBase<TCol, TRow>.GetFormatRule(formatCondition, column);
          if (rule.IsValid)
            yield return (IFormatRuleBase) rule;
        }
      }
    }

    public static FormatRuleExportWrapper GetFormatRule(FormatConditionBase formatCondition, TCol column)
    {
      return new FormatRuleExportWrapper(formatCondition, (ColumnWrapper) column);
    }

    protected internal virtual ClipboardBandLayoutInfo GetBandsInfo()
    {
      this.selectedColumns = this.GetSelectedColumns().Select<TCol, ColumnBase>((Func<TCol, ColumnBase>) (c => c.Column)).ToList<ColumnBase>();
      this.totalBandRowCount = this.CalcBandRowCount();
      this.totalColumnRowCount = this.CalcColumnRowCount();
      ClipboardBandLayoutInfo bandLayoutInfo = new ClipboardBandLayoutInfo(this.totalBandRowCount, this.totalColumnRowCount);
      foreach (BandBase visibleBand in this.View.DataControl.BandsLayoutCore.VisibleBands)
      {
        if (this.BandHasSelectedColumns(visibleBand))
          this.AddBandInfo(bandLayoutInfo, visibleBand);
      }
      bandLayoutInfo.NormalizeSpaces();
      return bandLayoutInfo;
    }

    protected virtual int AddBandInfo(ClipboardBandLayoutInfo bandLayoutInfo, BandBase band)
    {
      string @string = band.HeaderCaption.ToString();
      XlCellFormatting xlCellFormatting = new XlCellFormatting();
      xlCellFormatting.Alignment = ColumnWrapper.GetReportAlignment(band.HorizontalHeaderContentAlignment);
      XlCellFormatting formatting = xlCellFormatting;
      ClipboardBandCellInfo cell = new ClipboardBandCellInfo(0, 1, (object) @string, @string, formatting) { BandIndex = band.index };
      int level = band.Level;
      int bandPanelRowWidth1 = bandLayoutInfo.GetBandPanelRowWidth(level, true);
      int bandPanelRowWidth2 = bandLayoutInfo.GetBandPanelRowWidth(level, false);
      if (band.BandsCore.Count > 0)
      {
        foreach (BandBase visibleBand in band.VisibleBands)
        {
          if (this.BandHasSelectedColumns(visibleBand))
            cell.Width += this.AddBandInfo(bandLayoutInfo, visibleBand);
        }
      }
      else
      {
        cell.Width = this.AddBandColumnsInfo(bandLayoutInfo, band, level);
        cell.Height = this.totalBandRowCount - level;
      }
      if (bandPanelRowWidth2 < bandPanelRowWidth1)
        cell.SpaceBefore += bandPanelRowWidth1 - bandPanelRowWidth2;
      bandLayoutInfo.AddBandPanelCell(level, cell);
      return cell.Width;
    }

    protected virtual int AddBandColumnsInfo(ClipboardBandLayoutInfo bandLayoutInfo, BandBase band, int bandRowIndex)
    {
      int visibleColumnCount = this.GetBandMaxVisibleColumnCount(band);
      int bandPanelRowWidth = bandLayoutInfo.GetBandPanelRowWidth(bandRowIndex, true);
      int num1 = 0;
      int num2 = 0;
      ClipboardBandCellInfo clipboardBandCellInfo = (ClipboardBandCellInfo) null;
      for (int row = 0; row < band.ActualRows.Count; ++row)
      {
        foreach (ColumnBase column in band.ActualRows[row].Columns)
        {
          if (column.Visible && this.selectedColumns.Contains(column))
          {
            string @string = column.HeaderCaption.ToString();
            ColumnWrapper columnWrapper = new ColumnWrapper(column, column.VisibleIndex);
            XlCellFormatting xlCellFormatting = new XlCellFormatting();
            xlCellFormatting.Alignment = columnWrapper.AppearanceHeader.Alignment.Clone();
            XlCellFormatting formatting = xlCellFormatting;
            ClipboardBandCellInfo cell = new ClipboardBandCellInfo(1, 1, (object) @string, @string, formatting) { BandIndex = band.index, Row = row, Column = (object) columnWrapper };
            ++num1;
            int num3 = (int) column.ActualHeaderWidth;
            if (clipboardBandCellInfo == null || num2 < num3)
            {
              num2 = num3;
              clipboardBandCellInfo = cell;
            }
            int columnPanelRowWidth = bandLayoutInfo.GetColumnPanelRowWidth(row, -1);
            if (columnPanelRowWidth < bandPanelRowWidth)
              cell.SpaceBefore += bandPanelRowWidth - columnPanelRowWidth;
            bandLayoutInfo.AddColumnPanelCell(row, cell);
            if (row == band.ActualRows.Count - 1)
              cell.Height = this.totalColumnRowCount - cell.Row;
          }
        }
        if (num1 != visibleColumnCount && clipboardBandCellInfo != null)
          clipboardBandCellInfo.Width += visibleColumnCount - num1;
        num1 = num2 = 0;
        clipboardBandCellInfo = (ClipboardBandCellInfo) null;
      }
      return visibleColumnCount;
    }

    protected int GetBandMaxVisibleColumnCount(BandBase band)
    {
      int val1 = 1;
      foreach (BandRow actualRow in band.ActualRows)
        val1 = Math.Max(val1, actualRow.Columns.Where<ColumnBase>((Func<ColumnBase, bool>) (column =>
        {
          if (column.Visible)
            return this.selectedColumns.Contains(column);
          return false;
        })).Count<ColumnBase>());
      return val1;
    }

    protected virtual bool BandHasSelectedColumns(BandBase band)
    {
      if (!this.View.IsMultiCellSelection)
        return true;
      if (band.BandsCore.Count > 0)
      {
        bool flag = false;
        foreach (BandBase band1 in (IEnumerable) band.BandsCore)
          flag |= this.BandHasSelectedColumns(band1);
        return flag;
      }
      foreach (ColumnBase columnBase in (IEnumerable) band.ColumnsCore)
      {
        if (this.selectedColumns.Contains(columnBase))
          return true;
      }
      return false;
    }

    protected virtual int CalcBandRowCount()
    {
      return this.CalcBandRowCountCore(false);
    }

    protected virtual int CalcColumnRowCount()
    {
      return this.CalcColumnRowCountCore(false);
    }

    protected virtual int CalcBandRowCountCore(bool checkPrintable = false)
    {
      int val1 = 1;
      foreach (BandBase visibleBand in this.DataControl.BandsLayoutCore.VisibleBands)
      {
        if (!checkPrintable || visibleBand.AllowPrinting)
          val1 = Math.Max(val1, this.CalcBandRowCountCore(visibleBand, checkPrintable));
      }
      return val1;
    }

    protected int CalcBandRowCountCore(BandBase band, bool checkPrintable)
    {
      int num = 1;
      if (band.VisibleBands.Count == 0)
        return num;
      int val2 = 0;
      foreach (BandBase visibleBand in band.VisibleBands)
      {
        if (!checkPrintable || visibleBand.AllowPrinting)
          val2 = Math.Max(this.CalcBandRowCountCore(visibleBand, checkPrintable), val2);
      }
      return num + val2;
    }

    protected virtual int CalcColumnRowCountCore(bool checkPrintable = false)
    {
      int val1 = 1;
      foreach (ColumnBase columnBase in (IEnumerable<ColumnBase>) this.View.VisibleColumnsCore)
      {
        if (columnBase.ParentBand != null && columnBase.ParentBand.Visible && (!checkPrintable || columnBase.AllowPrinting))
          val1 = Math.Max(val1, this.CalcBandColumnRowCountCore(columnBase.ParentBand, checkPrintable));
      }
      return val1;
    }

    protected int CalcBandColumnRowCountCore(BandBase band, bool checkPrintable)
    {
      int num = 0;
      foreach (BandRow actualRow in band.ActualRows)
      {
        if (!checkPrintable || actualRow.Columns.Where<ColumnBase>((Func<ColumnBase, bool>) (c => c.AllowPrinting)).Count<ColumnBase>() > 0)
          ++num;
      }
      return num;
    }
  }
}
