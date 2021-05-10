// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.SelectionStrategyRow
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Editors.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid.Native
{
  public class SelectionStrategyRow : SelectionStrategyRowBase
  {
    private int oldFocusedRowHandle = int.MinValue;

    protected bool IsRightMouseButtonPressed
    {
      get
      {
        return Mouse.RightButton == MouseButtonState.Pressed;
      }
    }

    protected SelectionActionBase SelectionAction
    {
      get
      {
        return this.RootView.GlobalSelectionAction;
      }
      set
      {
        this.RootView.SetSelectionAction(value);
      }
    }

    protected GridViewBase GridView
    {
      get
      {
        return (GridViewBase) this.view;
      }
    }

    protected GridViewBase RootView
    {
      get
      {
        return (GridViewBase) this.view.RootView;
      }
    }

    protected GridControl Grid
    {
      get
      {
        return (GridControl) this.DataControl;
      }
    }

    protected int GroupRowCount
    {
      get
      {
        return this.DataControl.DataProviderBase.DataController.GroupRowCount;
      }
    }

    protected bool IsAllItemsSelected
    {
      get
      {
        if (this.GetAllItemsSelected().HasValue)
          return this.GetAllItemsSelected().Value;
        return false;
      }
    }

    protected bool IsAllItemsUnselected
    {
      get
      {
        return this.SelectedRowCount == 0;
      }
    }

    protected bool IsOneDataRowSelected
    {
      get
      {
        if (this.SelectedRowCount == 1)
          return !this.Grid.IsGroupRowHandle(this.GetSelectedRows()[0]);
        return false;
      }
    }

    public SelectionStrategyRow(GridViewBase view)
      : base((DataViewBase) view)
    {
    }

    public override void SelectRow(int rowHandle)
    {
      this.SelectRowCore(rowHandle);
    }

    public override void UnselectRow(int rowHandle)
    {
      this.UnselectRowCore(rowHandle);
    }

    public override void ClearSelection()
    {
      if (this.view.DataProviderBase == null)
        return;
      this.view.DataProviderBase.Selection.Clear();
    }

    public override bool IsRowSelected(int rowHandle)
    {
      return this.view.DataProviderBase.Selection.GetSelected(rowHandle);
    }

    public override SelectionState GetRowSelectionState(int rowHandle)
    {
      if (!this.IsRowSelected(rowHandle))
        return SelectionState.None;
      return rowHandle == this.view.FocusedRowHandle && this.view.IsFocusedView ? SelectionState.Focused : SelectionState.Selected;
    }

    protected override SelectionState GetCellSelectionStateCore(int rowHandle, bool isFocused, bool isSelected)
    {
      if (!this.IsRowSelected(rowHandle))
        return SelectionState.None;
      return base.GetCellSelectionStateCore(rowHandle, isFocused, isSelected);
    }

    public override int[] GetSelectedRows()
    {
      return this.view.DataProviderBase.Selection.GetSelectedRows();
    }

    public override void OnAssignedToGrid()
    {
      base.OnAssignedToGrid();
      this.GridView.SetSelectionAnchor();
    }

    protected override void OnAssignedToGridCore()
    {
      if (this.DataControl.DataProviderBase.Selection.Count != 0)
      {
        this.UpdateSelection();
      }
      else
      {
        if (!this.view.IsRootView)
          return;
        this.SetFocusedRowSelected();
      }
    }

    protected void UpdateSelection()
    {
      this.GridView.OnSelectionChanged(new SelectionChangedEventArgs(CollectionChangeAction.Refresh, int.MinValue));
    }

    public override void SetFocusedRowSelected()
    {
      this.DataControl.UpdateCurrentItem();
      ISelectionController selection = this.view.DataProviderBase.Selection;
      selection.BeginSelection();
      try
      {
        selection.Clear();
        selection.SetSelected(this.view.FocusedRowHandle, true);
        selection.SetActuallyChanged();
      }
      finally
      {
        selection.EndSelection();
      }
    }

    public override void BeginSelection()
    {
      this.view.DataProviderBase.Selection.BeginSelection();
    }

    public override void EndSelection()
    {
      if (!this.view.DataProviderBase.Selection.IsSelectionLocked)
        return;
      this.view.DataProviderBase.Selection.EndSelection();
    }

    public override void OnFocusedRowHandleChanged(int oldRowHandle)
    {
      if (this.CanExecuteSelectionActionOnFocusedRowHandleChanged())
        this.ExecuteSelectionAction();
      else
        this.GridView.SetSelectionAnchor();
    }

    protected virtual bool CanExecuteSelectionActionOnFocusedRowHandleChanged()
    {
      if (this.SelectionAction != null)
        return this.SelectionAction.CanFocusChangeDeleteAction;
      return false;
    }

    protected virtual void ExecuteSelectionAction()
    {
      this.GridView.ExecuteSelectionAction();
    }

    public override void SelectRowForce()
    {
      if (!this.IsExtendedMode)
        return;
      this.SelectionAction = (SelectionActionBase) new OnlyThisSelectionAction(this.GridView);
    }

    public override sealed void OnInvertSelection()
    {
      this.InvertSelectionForRow(this.view.FocusedRowHandle);
    }

    protected void InvertSelectionForRow(int rowHandle)
    {
      this.SetIsRowSelectedInternal(rowHandle, !this.IsRowSelected(rowHandle));
    }

    protected virtual void SetIsRowSelectedInternal(int rowHandle, bool isSelected)
    {
      this.view.DataProviderBase.Selection.SetSelected(rowHandle, isSelected);
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
      if (this.IsRowSelected(this.view.FocusedRowHandle) && !this.view.ShowFocusedRectangle)
        return false;
      if (this.RootView.NavigationStyle == GridViewNavigationStyle.Cell)
        this.view.SetFocusedRectangleOnCell();
      else
        this.view.SetFocusedRectangleOnRow();
      return true;
    }

    public override void CopyToClipboard()
    {
      int[] numArray = this.GetSelectedRows();
      if (numArray.Length == 0 && !this.view.IsInvalidFocusedRowHandle)
        numArray = new int[1]
        {
          this.view.FocusedRowHandle
        };
      this.GridView.DataControl.CopyRowsToClipboard((IEnumerable<int>) numArray);
    }

    public override void CopyMasterDetailToClipboard()
    {
      this.Grid.CopyAllSelectedItemsToClipboard();
    }

    protected virtual bool ShouldProcessMouseClick(IDataViewHitInfo hitInfo)
    {
      if (hitInfo.RowHandle == int.MinValue)
        return false;
      if (this.IsMultipleMode)
        return !this.RootView.IsExpandButton(hitInfo);
      return true;
    }

    public override void OnBeforeMouseLeftButtonDown(DependencyObject originalSource)
    {
      IDataViewHitInfo hitInfo = this.view.RootView.CalcHitInfoCore(originalSource);
      this.SelectionAction = (SelectionActionBase) null;
      if (!this.ShouldProcessMouseClick(hitInfo) || this.IsMultipleMode)
        return;
      int handleByTreeElement = this.GridView.GetRowHandleByTreeElement(originalSource);
      if (this.CanInvertSelection(hitInfo))
        return;
      this.SelectRowOnLMouseDown(handleByTreeElement, originalSource);
    }

    public override void OnAfterMouseLeftButtonDown(IDataViewHitInfo hitInfo)
    {
      if (this.HasValidationError || !this.ShouldProcessMouseClick(hitInfo) || this.IsRightMouseButtonPressed && (!this.IsMultipleMode || this.IsRowSelected(hitInfo.RowHandle)))
        return;
      int rowHandle = hitInfo.RowHandle;
      if (!this.CanInvertSelection(hitInfo))
        return;
      this.InvertSelectionOnClick(hitInfo);
    }

    protected virtual void InvertSelectionOnClick(IDataViewHitInfo hitInfo)
    {
      this.view.EditorSetInactiveAfterClick = true;
      if (this.CanInvertSelectionOnLMouseDown(hitInfo))
        this.InvertSelectionForRow(hitInfo.RowHandle);
      else
        this.SelectionAction = (SelectionActionBase) new InvertSelectionOnMouseUpAction(this.GridView);
    }

    protected virtual bool CanInvertSelectionOnLMouseDown(IDataViewHitInfo hitInfo)
    {
      if (this.view.IsRowSelected(hitInfo.RowHandle) && !this.IsControlPressed)
        return this.IsShiftPressed;
      return true;
    }

    private void SelectRowOnLMouseDown(int rowHandle, DependencyObject originalSource)
    {
      bool flag = this.RootView.ViewBehavior.IsRowIndicator(originalSource) && !this.IsRightMouseButtonPressed;
      if (this.CanSelectRowOnLMouseDown(rowHandle, flag))
        this.SelectRowOnLMouseDown(rowHandle);
      else
        this.CreateMouseSelectionActions(rowHandle, flag);
    }

    public override void CreateMouseSelectionActions(int rowHandle, bool isIndicatorPressed)
    {
      if (ModifierKeysHelper.IsShiftPressed(Keyboard.Modifiers))
      {
        this.view.EditorSetInactiveAfterClick = true;
        this.SelectionAction = (SelectionActionBase) new AddRowsToSelectionAction(this.GridView);
      }
      else if (ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers))
        this.SelectionAction = (SelectionActionBase) new AddOneRowToSelectionAction(this.GridView);
      else if (rowHandle == -2147483647)
      {
        this.view.EditorSetInactiveAfterClick = false;
        this.SelectionAction = (SelectionActionBase) new OnlyThisSelectionAction(this.GridView);
      }
      else if (rowHandle == -999997)
        this.view.EditorSetInactiveAfterClick = false;
      else if (this.view.IsRowSelected(rowHandle) && !isIndicatorPressed)
      {
        if (this.GridView.GetSelectedRowHandlesCore().Length > 1)
          this.SelectionAction = (SelectionActionBase) new OnlyThisSelectionMouseUpAction(this.GridView);
        else
          this.view.EditorSetInactiveAfterClick = false;
      }
      else
      {
        this.SelectionAction = (SelectionActionBase) new OnlyThisSelectionAction(this.GridView);
        if (this.GridView.GetSelectedRowHandlesCore().Length <= 1)
          return;
        this.view.EditorSetInactiveAfterClick = true;
      }
    }

    private bool CanSelectRowOnLMouseDown(int rowHandle, bool isIndicatorPressed)
    {
      if (this.IsControlPressed || rowHandle != this.view.FocusedRowHandle || !this.view.IsFocusedView)
        return false;
      if (this.view.IsRowSelected(rowHandle))
        return isIndicatorPressed;
      return true;
    }

    private void SelectRowOnLMouseDown(int rowHandle)
    {
      this.BeginSelection();
      try
      {
        this.ClearMasterDetailSelection();
        this.GridView.SetSelectionAnchor();
        if (rowHandle == -999997 || rowHandle == -2147483647)
          return;
        this.SelectRow(rowHandle);
      }
      finally
      {
        this.EndSelection();
      }
    }

    public override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
      if (this.view.FocusedRowHandle != this.GridView.GetRowHandleByMouseEventArgs((MouseEventArgs) e))
      {
        this.SelectionAction = (SelectionActionBase) null;
      }
      else
      {
        if (this.SelectionAction == null || this.SelectionAction.CanFocusChangeDeleteAction)
          return;
        this.ExecuteSelectionAction();
      }
    }

    public override void OnBeforeProcessKeyDown(KeyEventArgs e)
    {
      this.oldFocusedRowHandle = this.view.FocusedRowHandle;
      SelectionStrategyRow.GetKeyboardSelectionAction(this.GridView, e).Do<SelectionActionBase>((Action<SelectionActionBase>) (action => this.SelectionAction = action));
    }

    internal static SelectionActionBase GetKeyboardSelectionAction(GridViewBase view, KeyEventArgs e)
    {
      if (SelectionStrategyRow.IsNavigationKey(e))
      {
        if (ModifierKeysHelper.IsShiftPressed(ModifierKeysHelper.GetKeyboardModifiers(e)))
          return (SelectionActionBase) new AddRowsToSelectionAction(view);
        if (!ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)))
          return (SelectionActionBase) new OnlyThisSelectionAction(view);
      }
      return (SelectionActionBase) null;
    }

    public override void OnAfterProcessKeyDown(KeyEventArgs e)
    {
      if (e.Key == Key.Next || e.Key == Key.Prior)
        return;
      this.OnNavigationComplete(e.Key == Key.Tab);
    }

    protected static bool IsNavigationKey(KeyEventArgs e)
    {
      if (e.Key != Key.Up && e.Key != Key.Down && (e.Key != Key.Prior && e.Key != Key.Next) && (e.Key != Key.Right && e.Key != Key.Left && (e.Key != Key.Tab && e.Key != Key.Home)))
        return e.Key == Key.End;
      return true;
    }

    public override void OnNavigationComplete(bool IsTabPressed)
    {
      if (this.oldFocusedRowHandle != this.view.FocusedRowHandle)
        return;
      this.SelectionAction = (SelectionActionBase) null;
    }

    public override void OnNavigationCanceled()
    {
      this.SelectionAction = (SelectionActionBase) null;
    }

    protected override void SelectAllRows()
    {
      this.DoSelectionAction((Action) (() =>
      {
        this.SelectDataRows();
        this.SelectGroupRows();
      }));
    }

    protected void SelectGroupRows()
    {
      GroupRowInfoCollection groupInfo = this.DataControl.DataProviderBase.DataController.GroupInfo;
      for (int index = 0; index < this.GroupRowCount; ++index)
        this.SelectRowCore(groupInfo[index].Handle);
    }
  }
}
