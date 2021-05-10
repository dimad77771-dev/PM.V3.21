// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.SelectionStrategyCellRectangle
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Editors.Helpers;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid.Native
{
  public class SelectionStrategyCellRectangle : SelectionStrategyCell
  {
    private ITableViewHitInfo hitInfo;

    public SelectionStrategyCellRectangle(TableView view)
      : base(view)
    {
    }

    public override void OnBeforeMouseLeftButtonDown(DependencyObject originalSource)
    {
      base.OnBeforeMouseLeftButtonDown(originalSource);
      this.hitInfo = (ITableViewHitInfo) this.TableView.CalcHitInfo(originalSource);
      Tuple<int, ColumnBase> actualRowAndColumn = SelectionRectangleTableViewHelper.GetActualRowAndColumn(this.hitInfo, this.view);
      this.SetOldRowAndColumn(actualRowAndColumn.Item1, actualRowAndColumn.Item2);
    }

    protected override void OnAfterMouseLeftButtonDownCore(IDataViewHitInfo hitInfo)
    {
      Tuple<int, ColumnBase> actualRowAndColumn = SelectionRectangleTableViewHelper.GetActualRowAndColumn((ITableViewHitInfo) hitInfo, this.view);
      bool flag = false;
      if (actualRowAndColumn.Item1 == this.view.FocusedRowHandle && actualRowAndColumn.Item2 == this.CurrentColumn)
      {
        flag = Mouse.RightButton == MouseButtonState.Pressed;
        if (!flag)
        {
          base.OnAfterMouseLeftButtonDownCore(hitInfo);
          hitInfo = (IDataViewHitInfo) null;
          return;
        }
      }
      if (this.view.IsEditing)
        return;
      ITableViewHitInfo tableViewHitInfo = (ITableViewHitInfo) hitInfo;
      if (!this.IsValidRowHandle(actualRowAndColumn.Item1) || this.oldRowHandle == actualRowAndColumn.Item1 && this.oldColumn == actualRowAndColumn.Item2 && (actualRowAndColumn.Item2 != null && !this.IsElementFocused((IDataViewHitInfo) tableViewHitInfo)) && (!this.IsCellSelected(actualRowAndColumn.Item1, actualRowAndColumn.Item2) && ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers) && !tableViewHitInfo.IsRowIndicator))
        return;
      if (Mouse.RightButton == MouseButtonState.Pressed && !flag && (this.oldRowHandle == actualRowAndColumn.Item1 || ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers)))
      {
        this.UpdateSelectionBoundsForceCore(tableViewHitInfo, actualRowAndColumn.Item1, actualRowAndColumn.Item2);
      }
      else
      {
        if (!ModifierKeysHelper.IsShiftPressed(Keyboard.Modifiers))
          this.UpdateSelectionBoundsForceCore(tableViewHitInfo, actualRowAndColumn.Item1, actualRowAndColumn.Item2);
        if (actualRowAndColumn.Item2 != null && this.IsCellSelected(actualRowAndColumn.Item1, actualRowAndColumn.Item2) && Mouse.RightButton == MouseButtonState.Pressed)
          return;
        this.BeginSelection();
        if (!ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers) && !ModifierKeysHelper.IsShiftPressed(Keyboard.Modifiers))
          this.ClearSelection();
        this.EndSelection();
      }
    }

    protected void UpdateSelectionBoundsForceCore(ITableViewHitInfo tableViewHitInfo, int rowHandle, ColumnBase column)
    {
      this.selectionAnchor = !this.IsValidRowHandle(rowHandle) || column == null ? (CellBase) null : this.CreateCell(rowHandle, column);
      this.oldSelectionRectangle = Rectangle.Empty;
      this.oldRowHandle = rowHandle;
      this.oldColumn = column;
    }

    public override void UpdateSelectionRect(int rowHandle, ColumnBase column)
    {
      if (this.hitInfo == null)
      {
        base.UpdateSelectionRect(rowHandle, column);
      }
      else
      {
        if (this.selectionAnchor == null)
        {
          Tuple<int, ColumnBase> actualRowAndColumn = SelectionRectangleTableViewHelper.GetActualRowAndColumn(this.hitInfo, this.view);
          this.selectionAnchor = this.CreateCell(actualRowAndColumn.Item1, actualRowAndColumn.Item2);
          this.oldSelectionRectangle = Rectangle.Empty;
        }
        if (this.selectionAnchor.ColumnCore == null)
          return;
        this.SelectAnchorRange(this.selectionAnchor.RowHandleCore, this.selectionAnchor.ColumnCore.VisibleIndex, rowHandle, column.VisibleIndex);
      }
    }
  }
}
