// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.SelectionStrategyCellBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid.Native
{
  public abstract class SelectionStrategyCellBase : MultiSelectionStrategyBase
  {
    private Locker updateSelectionBoundsLocker = new Locker();
    protected int oldRowHandle;
    protected ColumnBase oldColumn;
    protected CellBase selectionAnchor;
    protected Rectangle oldSelectionRectangle;

    protected DataViewBase DataView
    {
      get
      {
        return this.view;
      }
    }

    protected ColumnBase CurrentColumn
    {
      get
      {
        return this.view.DataControl.CurrentColumn;
      }
    }

    public SelectionStrategyCellBase(DataViewBase view)
      : base(view)
    {
    }

    public override bool IsRowSelected(int rowHandle)
    {
      if (!this.view.IsFocusedView)
        return false;
      return this.view.DataProviderBase.Selection.GetSelected(rowHandle);
    }

    public override SelectionState GetRowSelectionState(int rowHandle)
    {
      if (!this.view.IsFocusedView || !this.view.DataControl.IsGroupRowHandleCore(rowHandle))
        return SelectionState.None;
      if (rowHandle == this.view.FocusedRowHandle)
        return !this.IsRowSelected(rowHandle) ? SelectionState.None : SelectionState.Focused;
      return !this.IsRowSelected(rowHandle) ? SelectionState.None : SelectionState.Selected;
    }

    protected override SelectionState GetCellSelectionStateCore(int rowHandle, bool isFocused, bool isSelected)
    {
      if (isFocused)
        return this.view.IsEditing || !isSelected ? SelectionState.None : SelectionState.FocusedAndSelected;
      return !isSelected ? SelectionState.None : SelectionState.Selected;
    }

    public override int[] GetSelectedRows()
    {
      return this.view.DataProviderBase.Selection.GetSelectedRows();
    }

    public override void BeginSelection()
    {
      this.view.DataProviderBase.Selection.BeginSelection();
    }

    public override void EndSelection()
    {
      this.view.DataProviderBase.Selection.EndSelection();
    }

    public override void OnFocusedRowHandleChanged(int oldRowHandle)
    {
      this.UpdateSelectionBounds();
    }

    public override void OnFocusedColumnChanged()
    {
      this.UpdateSelectionBounds();
    }

    public override void SelectCell(int rowHandle, ColumnBase column)
    {
      this.SelectCellCore(rowHandle, column, false);
    }

    public override void SelectRow(int rowHandle)
    {
      if (this.view.DataControl.IsGroupRowHandleCore(rowHandle))
      {
        this.view.DataProviderBase.Selection.SetSelected(rowHandle, true);
      }
      else
      {
        if (this.view.VisibleColumnsCore.Count == 0)
          return;
        this.SetCellsSelection(rowHandle, this.view.VisibleColumnsCore[0], rowHandle, this.view.VisibleColumnsCore[this.view.VisibleColumnsCore.Count - 1], true);
      }
    }

    public override void UnselectRow(int rowHandle)
    {
      if (this.view.DataControl.IsGroupRowHandleCore(rowHandle))
      {
        this.view.DataProviderBase.Selection.SetSelected(rowHandle, false);
      }
      else
      {
        if (this.view.VisibleColumnsCore.Count == 0)
          return;
        this.SetCellsSelection(rowHandle, this.view.VisibleColumnsCore[0], rowHandle, this.view.VisibleColumnsCore[this.view.VisibleColumnsCore.Count - 1], false);
      }
    }

    internal override void SelectAllMasterDetail()
    {
      this.SelectAll();
    }

    public override void SelectAll()
    {
      if (this.view.DataProviderBase.VisibleCount == 0 || this.view.VisibleColumnsCore.Count == 0)
        return;
      this.BeginSelection();
      this.ClearSelection();
      for (int visibleIndex = 0; visibleIndex < this.view.DataControl.VisibleRowCount; ++visibleIndex)
        this.SelectRow(this.view.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex));
      this.EndSelection();
    }

    protected void SelectCellCore(int rowHandle, ColumnBase column, bool useSelectionCount)
    {
      if (column == null)
        return;
      Dictionary<ColumnBase, int> list = this.GetSelectedCells(rowHandle) ?? new Dictionary<ColumnBase, int>();
      if (list.ContainsKey(column))
      {
        if (!useSelectionCount)
          return;
        Dictionary<ColumnBase, int> dictionary;
        ColumnBase index;
        (dictionary = list)[index = column] = dictionary[index] + 1;
      }
      else
      {
        list[column] = 0;
        this.view.DataProviderBase.Selection.SetActuallyChanged();
        if (this.GetSelectedCells(rowHandle) == null)
          this.SetCellSelection(rowHandle, list);
        else
          this.RefreshSelectionForce(rowHandle);
      }
    }

    public override void UnselectCell(int rowHandle, ColumnBase column)
    {
      this.UnselectCellCore(rowHandle, column, false);
    }

    protected void UnselectCellCore(int rowHandle, ColumnBase column, bool useSelectionCount)
    {
      Dictionary<ColumnBase, int> selectedCells = this.GetSelectedCells(rowHandle);
      if (selectedCells == null || !selectedCells.ContainsKey(column))
        return;
      if (useSelectionCount && selectedCells[column] != 0)
      {
        Dictionary<ColumnBase, int> dictionary;
        ColumnBase index;
        (dictionary = selectedCells)[index = column] = dictionary[index] - 1;
      }
      else
      {
        selectedCells.Remove(column);
        this.view.DataProviderBase.Selection.SetActuallyChanged();
        if (selectedCells.Count == 0)
          this.view.DataProviderBase.Selection.SetSelected(rowHandle, false);
        else
          this.RefreshSelectionForce(rowHandle);
      }
    }

    private void RefreshSelectionForce(int rowHandle)
    {
      if (this.view.DataProviderBase.Selection.IsSelectionLocked)
        return;
      this.view.OnSelectionChanged(new SelectionChangedEventArgs(CollectionChangeAction.Refresh, rowHandle));
    }

    public override CellBase[] GetSelectedCells()
    {
      List<CellBase> source = new List<CellBase>();
      foreach (int selectedRow in this.view.DataProviderBase.Selection.GetSelectedRows())
      {
        Dictionary<ColumnBase, int> selectedCells = this.GetSelectedCells(selectedRow);
        if (selectedCells != null)
        {
          foreach (ColumnBase column in (IEnumerable<ColumnBase>) selectedCells.Keys.OrderBy<ColumnBase, int>((Func<ColumnBase, int>) (x => x.VisibleIndex)))
          {
            if (column.Visible)
              source.Add(this.CreateCell(selectedRow, column));
          }
        }
      }
      return source.ToArray<CellBase>();
    }

    private Dictionary<ColumnBase, int> GetSelectedCells(int rowHandle)
    {
      return this.view.DataProviderBase.Selection.GetSelectedObject(rowHandle) as Dictionary<ColumnBase, int>;
    }

    private void SetCellSelection(int rowHandle, Dictionary<ColumnBase, int> list)
    {
      this.view.DataProviderBase.Selection.SetSelected(rowHandle, true, (object) list);
    }

    public override bool IsCellSelected(int rowHandle, ColumnBase column)
    {
      if (!this.view.IsFocusedView)
        return false;
      Dictionary<ColumnBase, int> selectedCells = this.GetSelectedCells(rowHandle);
      return selectedCells != null && selectedCells.ContainsKey(column);
    }

    public override void ClearSelection()
    {
      if (this.view.DataProviderBase == null)
        return;
      this.view.DataProviderBase.Selection.Clear();
    }

    public override void SetCellsSelection(int startRowHandle, ColumnBase startColumn, int endRowHandle, ColumnBase endColumn, bool setSelected)
    {
      this.SetCellsSelectionCore(startRowHandle, startColumn.VisibleIndex, endRowHandle, endColumn.VisibleIndex, setSelected, false);
    }

    protected void SetCellsSelectionCore(int startRowHandle, int startColumnIndex, int endRowHandle, int endColumnIndex, bool setSelected, bool useSelectionCount)
    {
      this.BeginSelection();
      try
      {
        this.DataView.ViewBehavior.IterateCells(startRowHandle, startColumnIndex, endRowHandle, endColumnIndex, (Action<int, ColumnBase>) ((rowHandle, column) =>
        {
          if (setSelected)
            this.SelectCellCore(rowHandle, column, useSelectionCount);
          else
            this.UnselectCellCore(rowHandle, column, useSelectionCount);
        }));
      }
      finally
      {
        this.EndSelection();
      }
    }

    public override void OnBeforeMouseLeftButtonDown(DependencyObject originalSource)
    {
      this.updateSelectionBoundsLocker.Lock();
      this.oldRowHandle = this.view.FocusedRowHandle;
      this.oldColumn = this.CurrentColumn;
    }

    protected void SetOldRowAndColumn(int rowHandle, ColumnBase column)
    {
      this.oldRowHandle = rowHandle;
      this.oldColumn = column;
    }

    protected bool IsElementFocused(IDataViewHitInfo hitInfo)
    {
      if (this.view.FocusedRowHandle != hitInfo.RowHandle)
        return false;
      if (this.CurrentColumn != hitInfo.Column)
        return this.view.DataControl.IsGroupRowHandleCore(this.view.FocusedRowHandle);
      return true;
    }

    public override void OnAfterMouseLeftButtonDown(IDataViewHitInfo hitInfo)
    {
      this.OnAfterMouseLeftButtonDownCore(hitInfo);
      this.updateSelectionBoundsLocker.Unlock();
    }

    protected virtual void OnAfterMouseLeftButtonDownCore(IDataViewHitInfo hitInfo)
    {
      if (this.view.IsEditing)
        return;
      ITableViewHitInfo tableViewHitInfo = (ITableViewHitInfo) hitInfo;
      if (!this.IsValidRowHandle(hitInfo.RowHandle) || this.oldRowHandle == this.view.FocusedRowHandle && this.oldColumn == this.CurrentColumn && (!this.IsElementFocused((IDataViewHitInfo) tableViewHitInfo) && !this.IsCellSelected(this.view.FocusedRowHandle, this.CurrentColumn)) && (ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers) && !tableViewHitInfo.IsRowIndicator))
        return;
      if (Mouse.RightButton == MouseButtonState.Pressed && (this.oldRowHandle == this.view.FocusedRowHandle || ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers)))
      {
        this.UpdateSelectionBoundsForce();
      }
      else
      {
        if (!ModifierKeysHelper.IsShiftPressed(Keyboard.Modifiers))
          this.UpdateSelectionBoundsForce();
        if (this.CurrentColumn != null && this.IsCellSelected(this.view.FocusedRowHandle, this.CurrentColumn) && Mouse.RightButton == MouseButtonState.Pressed)
          return;
        this.BeginSelection();
        if (!ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers) && !ModifierKeysHelper.IsShiftPressed(Keyboard.Modifiers))
          this.ClearSelection();
        if (!ModifierKeysHelper.IsShiftPressed(Keyboard.Modifiers))
        {
          if (ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers))
            this.OnInvertSelectionCore(tableViewHitInfo.IsRowIndicator);
          else if (this.view.DataControl.IsGroupRowHandleCore(this.view.FocusedRowHandle) || tableViewHitInfo.IsRowIndicator)
            this.SelectRow(this.view.FocusedRowHandle);
          else
            this.SelectCell(this.view.FocusedRowHandle, this.CurrentColumn);
        }
        else
        {
          if (this.selectionAnchor == null)
            this.selectionAnchor = this.IsValidRowHandle(this.oldRowHandle) ? this.CreateCell(this.oldRowHandle, this.oldColumn) : this.CreateCell(this.view.FocusedRowHandle, this.CurrentColumn);
          if (!tableViewHitInfo.IsRowIndicator)
            this.SelectAnchorRange(this.view.FocusedRowHandle, this.CurrentColumn.VisibleIndex, this.selectionAnchor.RowHandleCore, this.selectionAnchor.ColumnCore.VisibleIndex);
          else
            this.SelectAnchorRange(this.view.FocusedRowHandle, 0, this.selectionAnchor.RowHandleCore, this.view.VisibleColumnsCore.Count);
        }
        this.EndSelection();
      }
    }

    private void UpdateSelectionBounds()
    {
      if (this.updateSelectionBoundsLocker.IsLocked)
        return;
      this.UpdateSelectionBoundsForce();
    }

    protected void UpdateSelectionBoundsForce()
    {
      this.selectionAnchor = !this.IsValidRowHandle(this.view.FocusedRowHandle) || this.CurrentColumn == null ? (CellBase) null : this.CreateCell(this.view.FocusedRowHandle, this.CurrentColumn);
      this.oldSelectionRectangle = Rectangle.Empty;
      this.oldRowHandle = this.view.FocusedRowHandle;
      this.oldColumn = this.CurrentColumn;
    }

    protected bool IsValidRowHandle(int rowHandle)
    {
      if (rowHandle != int.MinValue && rowHandle != -2147483647)
        return rowHandle != -999997;
      return false;
    }

    public override void UpdateSelectionRect(int rowHandle, ColumnBase column)
    {
      if (this.selectionAnchor == null || this.selectionAnchor.RowHandleCore != this.view.FocusedRowHandle || this.selectionAnchor.ColumnCore != this.CurrentColumn)
      {
        this.selectionAnchor = this.CreateCell(this.view.FocusedRowHandle, this.CurrentColumn);
        this.oldSelectionRectangle = Rectangle.Empty;
      }
      if (this.selectionAnchor.ColumnCore == null)
        return;
      this.SelectAnchorRange(this.selectionAnchor.RowHandleCore, this.selectionAnchor.ColumnCore.VisibleIndex, rowHandle, column.VisibleIndex);
    }

    public override void OnBeforeProcessKeyDown(KeyEventArgs e)
    {
      this.updateSelectionBoundsLocker.Lock();
      this.oldRowHandle = this.view.FocusedRowHandle;
      this.oldColumn = this.CurrentColumn;
    }

    public override void OnAfterProcessKeyDown(KeyEventArgs e)
    {
      if (e.Key == Key.Next || e.Key == Key.Prior)
        return;
      this.OnNavigationComplete(e.Key == Key.Tab);
    }

    public override void OnNavigationComplete(bool isTabPressed)
    {
      this.OnNavigationCompleteCore(isTabPressed);
      this.updateSelectionBoundsLocker.Unlock();
    }

    private void OnNavigationCompleteCore(bool isTabPressed)
    {
      if (this.oldRowHandle == this.view.FocusedRowHandle && this.oldColumn == this.CurrentColumn || !this.IsValidRowHandle(this.view.FocusedRowHandle))
        return;
      this.BeginSelection();
      if (!ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers) && (!ModifierKeysHelper.IsShiftPressed(Keyboard.Modifiers) || isTabPressed))
        this.ClearSelection();
      if (!ModifierKeysHelper.IsShiftPressed(Keyboard.Modifiers) || isTabPressed)
      {
        if (!ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers))
          this.SelectCell(this.view.FocusedRowHandle, this.CurrentColumn);
        this.UpdateSelectionBoundsForce();
      }
      else
      {
        if (this.selectionAnchor == null)
          this.selectionAnchor = this.IsValidRowHandle(this.oldRowHandle) ? this.CreateCell(this.oldRowHandle, this.oldColumn) : this.CreateCell(this.view.FocusedRowHandle, this.CurrentColumn);
        this.SelectAnchorRange(this.view.FocusedRowHandle, this.view.NavigationIndex, this.selectionAnchor.RowHandleCore, this.selectionAnchor.ColumnCore.VisibleIndex);
      }
      this.EndSelection();
    }

    protected void SelectAnchorRange(int rowHandle1, int colIndex1, int rowHandle2, int colIndex2)
    {
      if (rowHandle1 > rowHandle2)
      {
        int num = rowHandle1;
        rowHandle1 = rowHandle2;
        rowHandle2 = num;
      }
      if (colIndex1 > colIndex2)
      {
        int num = colIndex1;
        colIndex1 = colIndex2;
        colIndex2 = num;
      }
      Rectangle rect = new Rectangle(colIndex1, rowHandle1, colIndex2 - colIndex1 + 1, rowHandle2 - rowHandle1 + 1);
      if (!this.oldSelectionRectangle.IsEmpty && this.oldSelectionRectangle.IntersectsWith(rect))
        this.SetCellsSelectionCore(this.oldSelectionRectangle.Top, this.oldSelectionRectangle.Left, this.oldSelectionRectangle.Bottom - 1, this.oldSelectionRectangle.Right - 1, false, true);
      this.oldSelectionRectangle = rect;
      this.SetCellsSelectionCore(rowHandle1, colIndex1, rowHandle2, colIndex2, true, true);
    }

    public override void OnInvertSelection()
    {
      this.OnInvertSelectionCore(false);
    }

    protected void OnInvertSelectionCore(bool invertSelectionForEntireRow)
    {
      if (!this.IsValidRowHandle(this.view.FocusedRowHandle))
        return;
      if (this.view.DataControl.IsGroupRowHandleCore(this.view.FocusedRowHandle) || invertSelectionForEntireRow)
      {
        if (this.IsRowSelected(this.view.FocusedRowHandle))
          this.UnselectRow(this.view.FocusedRowHandle);
        else
          this.SelectRow(this.view.FocusedRowHandle);
      }
      else if (this.IsCellSelected(this.view.FocusedRowHandle, this.CurrentColumn))
        this.UnselectCell(this.view.FocusedRowHandle, this.CurrentColumn);
      else
        this.SelectCell(this.view.FocusedRowHandle, this.CurrentColumn);
    }

    public override bool UpdateBorderForFocusedElementCore()
    {
      if (this.view.DataControl.IsGroupRowHandleCore(this.view.FocusedRowHandle) && (!this.IsRowSelected(this.view.FocusedRowHandle) || this.view.ShowFocusedRectangle))
      {
        this.view.SetFocusedRectangleOnGroupRow();
        return true;
      }
      if (this.view.FocusedRowHandle == -2147483647 || this.view.FocusedRowHandle == -999997)
      {
        this.view.SetFocusedRectangleOnCell();
        return true;
      }
      if (this.CurrentColumn == null || this.IsCellSelected(this.view.FocusedRowHandle, this.CurrentColumn) && !this.view.ShowFocusedRectangle)
        return false;
      this.view.SetFocusedRectangleOnCell();
      return true;
    }

    protected abstract CellBase CreateCell(int rowHandle, ColumnBase column);

    protected override object GetSelection()
    {
      Dictionary<int, List<ColumnBase>> dictionary = new Dictionary<int, List<ColumnBase>>();
      foreach (int selectedRow in this.view.DataProviderBase.Selection.GetSelectedRows())
      {
        dictionary[selectedRow] = new List<ColumnBase>();
        Dictionary<ColumnBase, int> selectedCells = this.GetSelectedCells(selectedRow);
        if (selectedCells != null)
        {
          foreach (ColumnBase key in selectedCells.Keys)
            dictionary[selectedRow].Add(key);
        }
      }
      return (object) dictionary;
    }

    protected override void UpdateVisualState(object oldSelection)
    {
      Dictionary<int, List<ColumnBase>> selection = (Dictionary<int, List<ColumnBase>>) oldSelection;
      foreach (int key1 in selection.Keys)
      {
        int rowHandle = key1;
        List<ColumnBase> currentColumns = (List<ColumnBase>) null;
        this.view.UpdateRowDataByRowHandle(rowHandle, (UpdateRowDataDelegate) (rowData =>
        {
          Dictionary<ColumnBase, int> selectedCells = this.GetSelectedCells(rowHandle);
          if (currentColumns == null)
            rowData.UpdateIsSelected(selectedCells != null || this.IsRowSelected(rowHandle));
          foreach (ColumnBase key in selection[rowHandle])
          {
            if (rowData.CellDataCache.ContainsKey(key) && (currentColumns == null || !currentColumns.Contains(key)))
              ((GridCellData) rowData.CellDataCache[key]).UpdateIsSelected(rowHandle, selectedCells != null && selectedCells.Keys.Contains<ColumnBase>(key));
          }
        }));
      }
    }

    protected override bool ShouldInvertSelectionOnSpace()
    {
      return false;
    }
  }
}
