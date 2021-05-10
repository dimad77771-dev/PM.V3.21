// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListSelectionStrategyRow
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListSelectionStrategyRow : SelectionStrategyRowBase
  {
    private int selectionAnchorRowHandle;
    private int oldFocusedRowHandle;
    private bool isSelectionLocked;
    private Action postponedSelectionAction;

    protected TreeListView TreeListView
    {
      get
      {
        return this.view as TreeListView;
      }
    }

    protected TreeListDataProvider DataProvider
    {
      get
      {
        return this.TreeListView.TreeListDataProvider;
      }
    }

    public TreeListSelectionStrategyRow(TreeListView view)
      : base((DataViewBase) view)
    {
    }

    public override bool IsRowSelected(int rowHandle)
    {
      return this.DataProvider.Selection.GetSelected(rowHandle);
    }

    public virtual bool IsRowSelected(TreeListNode node)
    {
      return this.DataProvider.TreeListSelection.GetSelected(node);
    }

    public override void SelectRow(int rowHandle)
    {
      if (!this.IsValidRowHandle(rowHandle))
        return;
      this.DataProvider.Selection.SetSelected(rowHandle, true);
    }

    public virtual void SelectRow(TreeListNode node)
    {
      if (node == null)
        return;
      this.DataProvider.TreeListSelection.SetSelected(node, true);
    }

    public override void UnselectRow(int rowHandle)
    {
      if (!this.IsValidRowHandle(rowHandle))
        return;
      this.DataProvider.Selection.SetSelected(rowHandle, false);
    }

    public virtual void UnselectRow(TreeListNode node)
    {
      if (node == null)
        return;
      this.DataProvider.TreeListSelection.SetSelected(node, false);
    }

    public override void ClearSelection()
    {
      this.DataProvider.Selection.Clear();
    }

    public override void BeginSelection()
    {
      this.DataProvider.Selection.BeginSelection();
    }

    public override void EndSelection()
    {
      this.DataProvider.Selection.EndSelection();
    }

    protected override void SelectAllRows()
    {
      this.view.DataProviderBase.Selection.SelectAll();
    }

    public override int[] GetSelectedRows()
    {
      return this.DataProvider.Selection.GetSelectedRows();
    }

    public virtual TreeListNode[] GetSelectedNodes()
    {
      return this.DataProvider.TreeListSelection.GetSelectedNodes();
    }

    protected virtual bool IsValidRowHandle(int rowHandle)
    {
      return this.DataProvider.IsValidRowHandle(rowHandle);
    }

    public override void OnInvertSelection()
    {
      if (!this.IsValidRowHandle(this.TreeListView.FocusedRowHandle))
        return;
      this.InvertRowSelection(this.TreeListView.FocusedRowHandle);
    }

    protected virtual void InvertRowSelection(int rowHandle)
    {
      if (this.IsRowSelected(rowHandle))
        this.UnselectRow(rowHandle);
      else
        this.SelectRow(rowHandle);
    }

    public override SelectionState GetRowSelectionState(int rowHandle)
    {
      if (rowHandle == this.view.FocusedRowHandle)
        return !this.IsRowSelected(rowHandle) ? SelectionState.None : SelectionState.Focused;
      return !this.IsRowSelected(rowHandle) ? SelectionState.None : SelectionState.Selected;
    }

    protected override SelectionState GetCellSelectionStateCore(int rowHandle, bool isFocused, bool isSelected)
    {
      if (!this.IsRowSelected(rowHandle))
        return SelectionState.None;
      return base.GetCellSelectionStateCore(rowHandle, isFocused, isSelected);
    }

    public override bool UpdateBorderForFocusedElementCore()
    {
      if (this.TreeListView.FocusedRowHandle == -999997)
      {
        if (this.TreeListView.CurrentCellEditor != null)
          this.TreeListView.SetFocusedRectangleOnCell();
        return true;
      }
      if (this.IsRowSelected(this.view.FocusedRowHandle) && !this.view.ShowFocusedRectangle)
        return false;
      if (this.TreeListView.NavigationStyle == GridViewNavigationStyle.Cell)
        this.TreeListView.SetFocusedRectangleOnCell();
      else
        this.TreeListView.SetFocusedRectangleOnRow();
      return true;
    }

    public override void SetFocusedRowSelected()
    {
      this.SetSingleRowSelectedCore(this.TreeListView.FocusedRowHandle);
    }

    private void SetSingleRowSelectedCore(int rowHandle)
    {
      if (!this.IsValidRowHandle(rowHandle))
        return;
      this.BeginSelection();
      try
      {
        this.ClearSelection();
        this.SelectRow(rowHandle);
        this.SetSelectionAnchorRowHandle(rowHandle);
      }
      finally
      {
        this.EndSelection();
      }
    }

    protected override void OnAssignedToGridCore()
    {
      base.OnAssignedToGridCore();
      this.SetFocusedRowSelected();
    }

    protected void SetSelectionAnchorRowHandle(int rowHandle)
    {
      this.selectionAnchorRowHandle = rowHandle;
    }

    public override void OnFocusedRowHandleChanged(int oldRowHandle)
    {
      if (this.isSelectionLocked)
        return;
      this.SetSelectionAnchorRowHandle(this.view.FocusedRowHandle);
      this.oldFocusedRowHandle = this.TreeListView.FocusedRowHandle;
    }

    public override void CopyToClipboard()
    {
      int[] numArray = this.GetSelectedRows();
      if (numArray.Length == 0 && !this.view.IsInvalidFocusedRowHandle)
        numArray = new int[1]
        {
          this.view.FocusedRowHandle
        };
      this.TreeListView.DataControl.CopyRowsToClipboard((IEnumerable<int>) numArray);
    }

    public override void OnBeforeMouseLeftButtonDown(DependencyObject originalSource)
    {
      base.OnBeforeMouseLeftButtonDown(originalSource);
      this.oldFocusedRowHandle = this.TreeListView.FocusedRowHandle;
      this.isSelectionLocked = true;
      this.view.EditorSetInactiveAfterClick = false;
    }

    public override void OnAfterMouseLeftButtonDown(IDataViewHitInfo hitInfo)
    {
      base.OnAfterMouseLeftButtonDown(hitInfo);
      this.OnAfterMouseLeftButtonDownCore(hitInfo);
      this.isSelectionLocked = false;
    }

    protected virtual void OnAfterMouseLeftButtonDownCore(IDataViewHitInfo hitInfo)
    {
      this.postponedSelectionAction = (Action) null;
      if (this.view.IsEditing)
        return;
      TreeListViewHitInfo treeListViewHitInfo = (TreeListViewHitInfo) hitInfo;
      if (treeListViewHitInfo.InNodeIndent || treeListViewHitInfo.InNodeExpandButton || (!this.IsValidRowHandle(hitInfo.RowHandle) || this.HasValidationError))
        return;
      if (Mouse.RightButton == MouseButtonState.Pressed && (this.IsRowSelected(hitInfo.RowHandle) || this.IsControlPressed || this.IsMultipleMode && this.IsRowSelected(hitInfo.RowHandle)))
        this.SetSelectionAnchorRowHandle(hitInfo.RowHandle);
      else if (this.IsMultipleMode)
      {
        this.SetSelectionAnchorRowHandle(hitInfo.RowHandle);
        this.TreeListView.EditorSetInactiveAfterClick = true;
        if (this.IsRowSelected(hitInfo.RowHandle) && !this.IsControlPressed && !this.IsShiftPressed)
          this.postponedSelectionAction = (Action) (() => this.InvertRowSelection(hitInfo.RowHandle));
        else
          this.InvertRowSelection(hitInfo.RowHandle);
      }
      else
      {
        if (!this.IsShiftPressed)
        {
          this.SetSelectionAnchorRowHandle(hitInfo.RowHandle);
          if (!this.IsControlPressed && this.IsRowSelected(hitInfo.RowHandle) && this.TreeListView.DataControl.GetSelectedRowHandles().Length > 1)
          {
            this.TreeListView.EditorSetInactiveAfterClick = true;
            this.postponedSelectionAction = (Action) (() => this.SetSingleRowSelectedCore(hitInfo.RowHandle));
            return;
          }
        }
        if (this.IsRowSelected(hitInfo.RowHandle) && !this.IsShiftPressed && !this.IsControlPressed)
          return;
        if (!this.IsShiftPressed && this.IsControlPressed)
        {
          this.InvertRowSelection(hitInfo.RowHandle);
          this.TreeListView.EditorSetInactiveAfterClick = true;
        }
        else
        {
          this.BeginSelection();
          try
          {
            if (!this.IsControlPressed)
              this.ClearSelection();
            if (!this.IsShiftPressed)
            {
              if (this.IsControlPressed)
                return;
              this.SelectRow(hitInfo.RowHandle);
            }
            else
            {
              this.SelectRange(this.TreeListView.FocusedRowHandle, this.selectionAnchorRowHandle);
              this.TreeListView.EditorSetInactiveAfterClick = true;
            }
          }
          finally
          {
            this.EndSelection();
          }
        }
      }
    }

    public override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
      if (this.postponedSelectionAction != null && this.view.FocusedRowHandle == this.view.GetRowHandleByMouseEventArgs((MouseEventArgs) e))
        this.postponedSelectionAction();
      this.postponedSelectionAction = (Action) null;
    }

    public override void OnBeforeProcessKeyDown(KeyEventArgs e)
    {
      base.OnBeforeProcessKeyDown(e);
      this.isSelectionLocked = true;
      this.oldFocusedRowHandle = this.TreeListView.FocusedRowHandle;
    }

    public override void OnAfterProcessKeyDown(KeyEventArgs e)
    {
      if (e.Key == Key.Next || e.Key == Key.Prior)
        return;
      this.OnNavigationComplete(e.Key == Key.Tab && this.view.NavigationStyle == GridViewNavigationStyle.Row);
    }

    public override void OnNavigationComplete(bool isTabPressed)
    {
      this.OnNavigationCompleteCore(isTabPressed);
      this.isSelectionLocked = false;
    }

    protected virtual void OnNavigationCompleteCore(bool isTabPressed)
    {
      if (this.oldFocusedRowHandle == this.TreeListView.FocusedRowHandle || !this.IsValidRowHandle(this.TreeListView.FocusedRowHandle) || this.IsMultipleMode)
        return;
      this.BeginSelection();
      try
      {
        if (!this.IsControlPressed && !isTabPressed)
          this.ClearSelection();
        if (!this.IsShiftPressed || isTabPressed)
        {
          if (!this.IsControlPressed)
            this.SelectRow(this.TreeListView.FocusedRowHandle);
          this.SetSelectionAnchorRowHandle(this.TreeListView.FocusedRowHandle);
        }
        else
          this.SelectRange(this.TreeListView.FocusedRowHandle, this.selectionAnchorRowHandle);
      }
      finally
      {
        this.EndSelection();
      }
    }

    protected override bool ShouldInvertSelectionOnSpace()
    {
      if (base.ShouldInvertSelectionOnSpace())
        return !this.TreeListView.ShowCheckboxes;
      return false;
    }

    protected override void SelectItemsCore(IList items)
    {
      this.BeginSelection();
      try
      {
        this.ClearSelection();
        if (items == null)
          return;
        foreach (TreeListNode node in (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.DataProvider.Nodes))
        {
          if (items.Contains(node.Content))
            this.SelectRow(node);
        }
      }
      finally
      {
        this.EndSelection();
      }
    }
  }
}
