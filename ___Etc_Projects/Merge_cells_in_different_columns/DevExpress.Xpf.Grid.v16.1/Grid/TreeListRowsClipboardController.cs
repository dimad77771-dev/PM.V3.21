// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListRowsClipboardController
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Grid.TreeList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevExpress.Xpf.Grid
{
  public class TreeListRowsClipboardController : RowsClipboardController
  {
    protected TreeListView View
    {
      get
      {
        return base.View as TreeListView;
      }
    }

    public TreeListRowsClipboardController(TreeListView view)
      : base((DataViewBase) view)
    {
    }

    public void CopyCellsToClipboard(IEnumerable<TreeListCell> cells)
    {
      this.CopyToClipboard((Func<CopyingToClipboardEventArgsBase>) (() => this.CreateCellsCopyingToClipboardEventArgs(cells)), (IClipboardDataProvider) new TreeListRowsClipboardController.TreeListCellsClipboardDataProvider(cells, this));
    }

    protected override CopyingToClipboardEventArgsBase CreateRowsCopyingToClipboardEventArgs(IEnumerable<int> rows)
    {
      return (CopyingToClipboardEventArgsBase) new TreeListCopyingToClipboardEventArgs(this.View, rows, true);
    }

    protected virtual CopyingToClipboardEventArgsBase CreateCellsCopyingToClipboardEventArgs(IEnumerable<TreeListCell> cells)
    {
      return (CopyingToClipboardEventArgsBase) new TreeListCopyingToClipboardEventArgs(this.View, cells, true);
    }

    public class TreeListCellsClipboardDataProvider : IClipboardDataProvider
    {
      protected TreeListRowsClipboardController ClipboardController { get; private set; }

      protected TreeListView View
      {
        get
        {
          return this.ClipboardController.View;
        }
      }

      protected IEnumerable<TreeListCell> Cells { get; private set; }

      public TreeListCellsClipboardDataProvider(IEnumerable<TreeListCell> cells, TreeListRowsClipboardController clipboardController)
      {
        this.ClipboardController = clipboardController;
        this.Cells = cells;
      }

      public object GetObjectFromClipboard()
      {
        List<int> intList = new List<int>();
        foreach (TreeListCell cell in this.Cells)
        {
          if (!intList.Contains(cell.RowHandle))
            intList.Add(cell.RowHandle);
        }
        return this.ClipboardController.GetSelectedData((IEnumerable<int>) intList);
      }

      public string GetTextFromClipboard()
      {
        if (this.Cells.Count<TreeListCell>() == 0)
          return string.Empty;
        StringBuilder sb = new StringBuilder();
        List<TreeListCell> cells = this.PrepareCells(this.Cells);
        List<ColumnBase> columns = this.PrepareColumns(cells);
        if (this.View.ActualClipboardCopyWithHeaders)
        {
          this.AppendHeadersText(sb, columns);
          this.AppendNewLine(sb);
        }
        this.AppendCellsText(cells, sb, columns);
        return sb.ToString();
      }

      protected List<TreeListCell> PrepareCells(IEnumerable<TreeListCell> cells)
      {
        return this.Cells.OrderBy<TreeListCell, CellBase>((Func<TreeListCell, CellBase>) (cell => (CellBase) cell), (IComparer<CellBase>) new CellComparer((DataViewBase) this.View)).ToList<TreeListCell>();
      }

      protected List<ColumnBase> PrepareColumns(List<TreeListCell> cells)
      {
        List<ColumnBase> columnBaseList = new List<ColumnBase>();
        foreach (TreeListCell cell in cells)
        {
          if (!columnBaseList.Contains(cell.Column))
            columnBaseList.Add(cell.Column);
        }
        columnBaseList.Sort((Comparison<ColumnBase>) ((column1, column2) => Comparer<int>.Default.Compare(column1.VisibleIndex, column2.VisibleIndex)));
        return columnBaseList;
      }

      protected virtual void AppendHeadersText(StringBuilder sb, List<ColumnBase> columns)
      {
        if (columns.Count == 0)
          return;
        foreach (ColumnBase column in columns)
          sb.Append(this.View.GetTextForClipboard(int.MinValue, this.View.VisibleColumns.IndexOf(column)) + "\t");
        sb.Remove(sb.Length - 1, 1);
      }

      protected virtual void AppendCellsText(List<TreeListCell> cells, StringBuilder sb, List<ColumnBase> columns)
      {
        int rowHandle = cells[0].RowHandle;
        int num = 0;
        foreach (TreeListCell cell in cells)
        {
          if (rowHandle != cell.RowHandle)
          {
            sb.Append(Environment.NewLine);
            num = 0;
          }
          for (int index = 0; index < columns.IndexOf(cell.Column) - num; ++index)
            sb.Append("\t");
          rowHandle = cell.RowHandle;
          num = columns.IndexOf(cell.Column);
          sb.Append(this.View.GetTextForClipboard(cell.RowHandle, this.View.VisibleColumns.IndexOf(cell.Column)));
        }
      }

      protected void AppendNewLine(StringBuilder sb)
      {
        sb.Append(Environment.NewLine);
      }
    }
  }
}
