// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.ClipboardSelectionProvider`2
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.XtraExport.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Grid.Printing
{
  public abstract class ClipboardSelectionProvider<TCol, TRow> where TCol : ColumnWrapper where TRow : IRowBase
  {
    protected DataViewBase View { get; private set; }

    protected DataControlBase DataControl
    {
      get
      {
        return this.View.DataControl;
      }
    }

    public ClipboardSelectionProvider(DataViewBase view)
    {
      this.View = view;
    }

    public string GetRowCellDisplayText(int rowHandle, string columnName)
    {
      if (this.View.IsMultiRowSelection && this.View.IsRowSelected(rowHandle) || this.IsCellSelected(rowHandle, this.DataControl.ColumnsCore[columnName]))
        return this.DataControl.GetCellDisplayText(rowHandle, columnName);
      return string.Empty;
    }

    public IEnumerable<ColumnWrapper> GetSelectedColumns()
    {
      return (IEnumerable<ColumnWrapper>) this.GetSelectedColumnsList();
    }

    public int GetSelectedCellsCount()
    {
      return this.GetSelectedCellsCountCore((IGroupRow<TRow>) null);
    }

    private Dictionary<ColumnBase, int> GetSelectedCells(int rowHandle)
    {
      return this.View.DataProviderBase.Selection.GetSelectedObject(rowHandle) as Dictionary<ColumnBase, int>;
    }

    public bool IsCellSelected(int rowHandle, ColumnBase column)
    {
      if (!this.View.IsMultiCellSelection)
        return this.View.IsRowSelected(rowHandle);
      Dictionary<ColumnBase, int> selectedCells = this.GetSelectedCells(rowHandle);
      return selectedCells != null && selectedCells.ContainsKey(column);
    }

    public virtual int GetSelectedCellsCountCore(IGroupRow<TRow> groupRow)
    {
      int num = 0;
      IEnumerable<TRow> rows = groupRow != null ? groupRow.GetAllRows() : this.GetSelectedRows();
      int count = this.GetSelectedColumnsList().Count;
      foreach (TRow row in rows)
      {
        if ((object) row is IGroupRow<TRow>)
          num += this.GetSelectedCellsCountCore((object) row as IGroupRow<TRow>);
        else
          num += count;
      }
      return num;
    }

    private IList<TCol> GetColumnsMultiCell()
    {
      int[] selectedRowHandles = this.View.DataControl.GetSelectedRowHandles();
      IList<TCol> columns = this.GetColumns((IEnumerable) this.DataControl.ColumnsCore, false);
      SortedList<int, TCol> sortedList = new SortedList<int, TCol>();
      HashSet<BaseColumn> baseColumnSet = new HashSet<BaseColumn>();
      for (int index = 0; index < selectedRowHandles.Length; ++index)
      {
        if (this.GetSelectedCells(selectedRowHandles[index]) != null)
        {
          foreach (ColumnBase key in this.GetSelectedCells(selectedRowHandles[index]).Keys)
          {
            if (this.IsColumnVisible(key) && !baseColumnSet.Contains((BaseColumn) key))
            {
              sortedList.Add(key.VisibleIndex, DataViewExportHelperBase<TCol, TRow>.CreateColumn(key, key.VisibleIndex));
              baseColumnSet.Add((BaseColumn) key);
            }
          }
          if (sortedList.Count == columns.Count)
            return sortedList.Values;
        }
      }
      if (sortedList.Values.Count != 0 || selectedRowHandles.Length == 0)
        return sortedList.Values;
      if (((IEnumerable<int>) selectedRowHandles).Max() >= 0)
        return (IList<TCol>) new List<TCol>();
      foreach (TCol column in (IEnumerable<TCol>) this.GetColumns((IEnumerable) this.DataControl.ColumnsCore, true))
      {
        if (this.IsColumnVisible(column.Column) && !baseColumnSet.Contains((BaseColumn) column.Column) && (column.Column.GroupIndexCore != -1 && !sortedList.ContainsKey(column.Column.VisibleIndex)))
        {
          sortedList.Add(column.Column.VisibleIndex, column);
          baseColumnSet.Add((BaseColumn) column.Column);
        }
      }
      return sortedList.Values;
    }

    protected virtual bool IsColumnVisible(ColumnBase column)
    {
      if (column.ParentBand != null && !column.ParentBand.Visible)
        return false;
      return column.Visible;
    }

    public IList<TCol> GetSelectedColumnsList()
    {
      if (this.View.IsMultiCellSelection)
        return this.GetColumnsMultiCell();
      return this.GetColumns((IEnumerable) this.View.VisibleColumnsCore, false);
    }

    public abstract IEnumerable<TRow> GetSelectedRows();

    public abstract IList<TCol> GetColumns(IEnumerable collection, bool isGroupColumn = false);
  }
}
