// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListViewClipboardHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Export.Xl;
using DevExpress.Xpf.Grid.Printing;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraPrinting;
using System.Collections.Generic;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListViewClipboardHelper : TreeListViewExportHelperBase, IClipboardGridView<ColumnWrapper, TreeListNodeWrapper>, IGridView<ColumnWrapper, TreeListNodeWrapper>, IGridViewBase<ColumnWrapper, TreeListNodeWrapper, ColumnWrapper, TreeListNodeWrapper>
  {
    private TreeListViewClipboardSelectionProvider provider;

    public TreeListViewClipboardHelper(TreeListView view, ExportTarget target = ExportTarget.Xlsx)
      : base(view, target)
    {
      this.provider = new TreeListViewClipboardSelectionProvider(view, this.nodesProvider);
    }

    public override object GetRowCellValue(int rowHandle, int visibleIndex)
    {
      if (visibleIndex < 0 || visibleIndex >= this.View.VisibleColumns.Count)
        return (object) null;
      ColumnBase column = this.View.VisibleColumns[visibleIndex];
      if (this.View.IsMultiRowSelection && this.View.IsRowSelected(rowHandle) || this.provider.IsCellSelected(rowHandle, column))
        return this.View.GetExportValue(rowHandle, column) ?? (object) string.Empty;
      return (object) string.Empty;
    }

    protected internal void ResetNodesProvider()
    {
      this.nodesProvider.Reset();
    }

    public bool CanCopyToClipboard()
    {
      return this.View.ClipboardMode == ClipboardMode.Formatted;
    }

    public XlCellFormatting GetCellAppearance(TreeListNodeWrapper row, ColumnWrapper col)
    {
      if (col != null && row != null && this.provider.IsCellSelected(row.LogicalPosition, col.Column))
        return AppearanceHelper.GetCellAppearance(row.LogicalPosition, col.Column, (DataViewBase) this.View, this.FormatConditionsCore);
      XlCellFormatting xlCellFormatting = new XlCellFormatting();
      xlCellFormatting.Font = new XlFont();
      xlCellFormatting.Fill = (XlFill) null;
      xlCellFormatting.Alignment = new XlCellAlignment();
      return xlCellFormatting;
    }

    public string GetRowCellDisplayText(TreeListNodeWrapper row, string columnName)
    {
      int logicalPosition = row.LogicalPosition;
      if (this.View.IsMultiRowSelection && this.View.IsRowSelected(logicalPosition) || this.provider.IsCellSelected(logicalPosition, this.DataControl.ColumnsCore[columnName]))
        return this.DataControl.GetCellDisplayText(logicalPosition, columnName);
      return string.Empty;
    }

    public IEnumerable<TreeListNodeWrapper> GetSelectedRows()
    {
      return this.provider.GetSelectedRows();
    }

    public override IEnumerable<ColumnWrapper> GetSelectedColumns()
    {
      return (IEnumerable<ColumnWrapper>) this.provider.GetSelectedColumnsList();
    }

    public int GetSelectedCellsCount()
    {
      return this.provider.GetSelectedCellsCountCore((IGroupRow<TreeListNodeWrapper>) null);
    }

    public void ProgressBarCallBack(int progress)
    {
    }

    public bool GetAlignGroupSummaryInGroupRow()
    {
      return false;
    }

    public bool GetShowGroupedColumns()
    {
      return false;
    }

    public virtual bool UseHierarchyIndent(TreeListNodeWrapper row, ColumnWrapper col)
    {
      return col.VisibleIndex == 0;
    }
  }
}
