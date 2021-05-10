// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.ClipboardController
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevExpress.Xpf.Grid.Native
{
  public class ClipboardController : RowsClipboardController
  {
    protected GridViewBase View
    {
      get
      {
        return base.View as GridViewBase;
      }
    }

    public ClipboardController(GridViewBase view)
      : base((DataViewBase) view)
    {
    }

    public void CopyCellsToClipboard(IEnumerable<GridCell> cells, bool isSelectеdCells)
    {
      this.CopyToClipboard((Func<CopyingToClipboardEventArgsBase>) (() => (CopyingToClipboardEventArgsBase) this.CreateCellsCopyingToClipboardEventArgs(cells)), this.GetDataProvider(cells, isSelectеdCells));
    }

    private IClipboardDataProvider GetDataProvider(IEnumerable<GridCell> cells, bool isSelectеdCells)
    {
      if (isSelectеdCells)
        return (IClipboardDataProvider) new ClipboardController.SelectedCellsClipboardDataProvider(cells, this);
      return (IClipboardDataProvider) new ClipboardController.CellsClipboardDataProvider(cells, this);
    }

    internal string GetTextInRows(IEnumerable<GridCell> gridCells)
    {
      if (gridCells.Count<GridCell>() == 0)
        return string.Empty;
      StringBuilder sb = new StringBuilder();
      List<GridColumn> columns = new List<GridColumn>();
      int minGroupLevel = 100;
      int dataLevel = 0;
      this.GetOffsetsAndVisibleColumns(gridCells, columns, out minGroupLevel, out dataLevel);
      if (this.View.ActualClipboardCopyWithHeaders)
        this.AppendColumnHeadersText(sb, columns, dataLevel);
      this.AppendCellValues(gridCells, sb, columns, minGroupLevel, dataLevel);
      return sb.ToString();
    }

    private void AppendCellValues(IEnumerable<GridCell> gridCells, StringBuilder sb, List<GridColumn> columns, int minGroupLevel, int dataLevel)
    {
      int num = 0;
      int rowHandle = gridCells.ElementAt<GridCell>(0).RowHandle;
      bool flag = true;
      foreach (GridCell gridCell in gridCells)
      {
        if (rowHandle != gridCell.RowHandle)
        {
          sb.Append(Environment.NewLine);
          num = 0;
          flag = true;
        }
        if (!this.View.Grid.IsGroupRowHandle(gridCell.RowHandle))
        {
          if (rowHandle != gridCell.RowHandle)
            this.AppendRowIndent(sb, dataLevel);
          for (int index = 0; index < columns.IndexOf(gridCell.Column) - num; ++index)
            sb.Append("\t");
          num = columns.IndexOf(gridCell.Column);
          sb.Append(this.View.GetTextForClipboard(gridCell.RowHandle, this.View.VisibleColumns.IndexOf(gridCell.Column)));
        }
        else if (flag)
        {
          this.AppendRowIndent(sb, this.View.Grid.GetRowLevelByRowHandle(gridCell.RowHandle) - minGroupLevel);
          sb.Append(this.View.GetGroupRowDisplayText(gridCell.RowHandle));
          flag = false;
        }
        rowHandle = gridCell.RowHandle;
      }
    }

    private void AppendColumnHeadersText(StringBuilder sb, List<GridColumn> columns, int dataLevel)
    {
      if (columns.Count == 0)
        return;
      this.AppendRowIndent(sb, dataLevel);
      foreach (GridColumn column in columns)
        sb.Append(this.View.GetTextForClipboard(int.MinValue, this.View.VisibleColumns.IndexOf(column)) + "\t");
      sb.Remove(sb.Length - 1, 1);
      sb.Append(Environment.NewLine);
    }

    private void GetOffsetsAndVisibleColumns(IEnumerable<GridCell> gridCells, List<GridColumn> columns, out int minGroupLevel, out int dataLevel)
    {
      minGroupLevel = 100;
      dataLevel = 0;
      foreach (GridCell gridCell in gridCells)
      {
        if (this.View.Grid.IsGroupRowHandle(gridCell.RowHandle))
        {
          int levelByRowHandle = this.View.Grid.GetRowLevelByRowHandle(gridCell.RowHandle);
          minGroupLevel = Math.Min(levelByRowHandle, minGroupLevel);
          dataLevel = Math.Max(dataLevel, levelByRowHandle);
        }
        else if (!columns.Contains(gridCell.Column))
          columns.Add(gridCell.Column);
      }
      columns.Sort((Comparison<GridColumn>) ((column1, column2) => Comparer<int>.Default.Compare(column1.VisibleIndex, column2.VisibleIndex)));
      if (minGroupLevel == 100)
      {
        minGroupLevel = -1;
        dataLevel = 0;
      }
      else
        dataLevel = dataLevel - minGroupLevel + 1;
    }

    private void AppendRowIndent(StringBuilder sb, int count)
    {
      for (int index = 0; index < count; ++index)
        sb.Append('\t');
    }

    protected override bool CanAddRowToSelectedData(DataControlBase dataControl, int rowHandle)
    {
      GridViewBase gridViewBase = (GridViewBase) dataControl.DataView;
      return !gridViewBase.IsGroupRow(gridViewBase.DataProviderBase.GetRowVisibleIndexByHandle(rowHandle), 1);
    }

    protected override int GetCountCopyRows(IEnumerable<KeyValuePair<DataControlBase, int>> rows)
    {
      if (this.View.DataProviderBase.IsServerMode && this.View.ClipboardCopyMaxRowCountInServerMode != -1)
        return Math.Min(this.View.ClipboardCopyMaxRowCountInServerMode, rows.Count<KeyValuePair<DataControlBase, int>>());
      return base.GetCountCopyRows(rows);
    }

    protected virtual CopyingToClipboardEventArgs CreateCellsCopyingToClipboardEventArgs(IEnumerable<GridCell> cells)
    {
      return new CopyingToClipboardEventArgs((DataViewBase) this.View, cells, true);
    }

    protected override CopyingToClipboardEventArgsBase CreateRowsCopyingToClipboardEventArgs(IEnumerable<int> rows)
    {
      return (CopyingToClipboardEventArgsBase) new CopyingToClipboardEventArgs((DataViewBase) this.View, rows, true);
    }

    public abstract class ClipboardDataProviderBase : IClipboardDataProvider
    {
      protected IEnumerable<GridCell> Cells { get; private set; }

      protected ClipboardController Owner { get; private set; }

      public ClipboardDataProviderBase(IEnumerable<GridCell> cells, ClipboardController owner)
      {
        this.Cells = cells;
        this.Owner = owner;
      }

      public abstract object GetObjectFromClipboard();

      public abstract string GetTextFromClipboard();
    }

    public class SelectedCellsClipboardDataProvider : ClipboardController.ClipboardDataProviderBase
    {
      public SelectedCellsClipboardDataProvider(IEnumerable<GridCell> cells, ClipboardController owner)
        : base(cells, owner)
      {
      }

      public override object GetObjectFromClipboard()
      {
        List<int> intList = new List<int>();
        int num = int.MinValue;
        foreach (GridCell cell in this.Cells)
        {
          if (num != cell.RowHandle)
          {
            num = cell.RowHandle;
            intList.Add(num);
          }
        }
        return this.Owner.GetSelectedData((IEnumerable<int>) intList);
      }

      public override string GetTextFromClipboard()
      {
        return this.Owner.GetTextInRows(this.Cells);
      }
    }

    public class CellsClipboardDataProvider : ClipboardController.ClipboardDataProviderBase
    {
      public CellsClipboardDataProvider(IEnumerable<GridCell> cells, ClipboardController owner)
        : base(cells, owner)
      {
      }

      public override object GetObjectFromClipboard()
      {
        List<int> intList = new List<int>();
        foreach (GridCell cell in this.Cells)
        {
          if (!intList.Contains(cell.RowHandle))
            intList.Add(cell.RowHandle);
        }
        return this.Owner.GetSelectedData((IEnumerable<int>) intList);
      }

      public override string GetTextFromClipboard()
      {
        return this.Owner.GetTextInRows((IEnumerable<GridCell>) this.Cells.OrderBy<GridCell, CellBase>((Func<GridCell, CellBase>) (cell => (CellBase) cell), (IComparer<CellBase>) new CellComparer((DataViewBase) this.Owner.View)).ToList<GridCell>());
      }
    }
  }
}
