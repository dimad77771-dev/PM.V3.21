// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GridViewClipboardHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Export.Xl;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraPrinting;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Grid.Printing
{
  public class GridViewClipboardHelper : GridViewExportHelperBase<ColumnWrapper, RowBaseWrapper>, IClipboardGridView<ColumnWrapper, RowBaseWrapper>, IGridView<ColumnWrapper, RowBaseWrapper>, IGridViewBase<ColumnWrapper, RowBaseWrapper, ColumnWrapper, RowBaseWrapper>
  {
    private GridViewClipboardSelectionProvider provider;

    public GridViewClipboardHelper(TableView view, ExportTarget target = ExportTarget.Xlsx)
      : base(view, target)
    {
      this.provider = new GridViewClipboardSelectionProvider(this);
    }

    protected override IEnumerable<ISummaryItemEx> GetGroupHeaderSummary()
    {
      return Enumerable.Empty<ISummaryItemEx>();
    }

    protected override IEnumerable<ISummaryItemEx> GetGroupFooterSummary()
    {
      return this.GetGroupHeaderSummaryCore().Concat<ISummaryItemEx>(this.PrintGroupFooters ? this.GetGroupFooterSummaryCore() : Enumerable.Empty<ISummaryItemEx>());
    }

    public override object GetRowCellValue(int rowHandle, int visibleIndex)
    {
      if (visibleIndex < 0 || visibleIndex >= this.View.VisibleColumns.Count)
        return (object) null;
      ColumnBase column = (ColumnBase) this.View.VisibleColumns[visibleIndex];
      if (this.View.IsMultiRowSelection && this.View.IsRowSelected(rowHandle) || this.provider.IsCellSelected(rowHandle, column))
        return this.View.GetExportValue(rowHandle, column) ?? (object) string.Empty;
      return (object) string.Empty;
    }

    public bool CanCopyToClipboard()
    {
      return this.View.ClipboardMode == ClipboardMode.Formatted;
    }

    public XlCellFormatting GetCellAppearance(RowBaseWrapper row, ColumnWrapper col)
    {
      if (col != null && row != null && this.provider.IsCellSelected(row.LogicalPosition, col.Column))
        return AppearanceHelper.GetCellAppearance(row.LogicalPosition, col.Column, (DataViewBase) this.View, this.FormatConditionsCore);
      XlCellFormatting xlCellFormatting = new XlCellFormatting();
      xlCellFormatting.Font = new XlFont();
      xlCellFormatting.Fill = (XlFill) null;
      xlCellFormatting.Alignment = new XlCellAlignment();
      return xlCellFormatting;
    }

    public string GetRowCellDisplayText(RowBaseWrapper row, string columnName)
    {
      int logicalPosition = row.LogicalPosition;
      if (this.View.IsMultiRowSelection && this.View.IsRowSelected(logicalPosition) || this.provider.IsCellSelected(logicalPosition, (ColumnBase) this.Grid.Columns[columnName]))
        return this.Grid.GetCellDisplayText(logicalPosition, columnName);
      return string.Empty;
    }

    public void ProgressBarCallBack(int progress)
    {
    }

    public bool GetAlignGroupSummaryInGroupRow()
    {
      return this.View.GroupSummaryDisplayMode == GroupSummaryDisplayMode.AlignByColumns;
    }

    public bool GetShowGroupedColumns()
    {
      return this.View.ShowGroupedColumns;
    }

    public IEnumerable<RowBaseWrapper> GetSelectedRows()
    {
      return this.provider.GetSelectedRows();
    }

    public override IEnumerable<ColumnWrapper> GetSelectedColumns()
    {
      return (IEnumerable<ColumnWrapper>) this.provider.GetSelectedColumnsList();
    }

    public int GetSelectedCellsCount()
    {
      return this.provider.GetSelectedCellsCountCore((IGroupRow<RowBaseWrapper>) null);
    }

    public bool UseHierarchyIndent(RowBaseWrapper row, ColumnWrapper col)
    {
      return false;
    }
  }
}
