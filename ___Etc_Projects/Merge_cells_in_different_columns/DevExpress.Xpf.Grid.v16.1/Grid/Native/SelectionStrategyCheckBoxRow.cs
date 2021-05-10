// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.SelectionStrategyCheckBoxRow
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace DevExpress.Xpf.Grid.Native
{
  public class SelectionStrategyCheckBoxRow : SelectionStrategyRow
  {
    private Locker CheckBoxSelectionChangedLocker = new Locker();
    private Locker UpdateGroupRowsLocker = new Locker();

    private TableView View
    {
      get
      {
        return (TableView) this.view;
      }
    }

    protected override int TotalRowCount
    {
      get
      {
        return this.DataRowCount + this.GroupRowCount;
      }
    }

    public SelectionStrategyCheckBoxRow(TableView view)
      : base((GridViewBase) view)
    {
    }

    public override bool? GetAllItemsSelected(int rowHandle)
    {
      return this.AreAllDescendantsSelected(rowHandle);
    }

    private int GetRootGroupHandle(int groupRowHandle)
    {
      return this.DataProviderBase.DataController.GroupInfo.GetGroupRowInfoByHandle(groupRowHandle).RootGroup.Handle;
    }

    private void DoLockedSelectionAction(Action action)
    {
      this.UpdateGroupRowsLocker.DoLockedAction((Action) (() => this.DoSelectionAction((Action) (() => action()))));
    }

    private bool IsRowSelectedOrUnselected(SelectionChangedEventArgs e)
    {
      return e.Action != CollectionChangeAction.Refresh;
    }

    private bool IsDataRowSelectedOrUnselected(SelectionChangedEventArgs e)
    {
      if (!this.Grid.IsGroupRowHandle(e.ControllerRow))
        return this.IsRowSelectedOrUnselected(e);
      return false;
    }

    private bool IsDataRowCheckBox(IDataViewHitInfo hitInfo)
    {
      return hitInfo.Column == this.View.CheckBoxSelectorColumn;
    }

    private bool IsGroupRowPressed(IDataViewHitInfo hitInfo)
    {
      return this.Grid.IsGroupRowHandle(hitInfo.RowHandle);
    }

    protected override bool ShouldProcessMouseClick(IDataViewHitInfo hitInfo)
    {
      if (hitInfo.RowHandle == -999997)
        return false;
      if (this.View.RetainSelectionOnClickOutsideCheckBoxSelector)
        return this.IsDataRowCheckBox(hitInfo);
      if (this.IsGroupRowPressed(hitInfo) && !this.IsShiftPressed)
        return false;
      return base.ShouldProcessMouseClick(hitInfo);
    }

    protected override bool CanInvertSelection(IDataViewHitInfo hitInfo)
    {
      if (base.CanInvertSelection(hitInfo))
        return true;
      if (this.IsDataRowCheckBox(hitInfo))
        return !this.IsShiftPressed;
      return false;
    }

    protected override bool CanInvertSelectionOnLMouseDown(IDataViewHitInfo hitInfo)
    {
      if (!this.IsDataRowCheckBox(hitInfo))
        return base.CanInvertSelectionOnLMouseDown(hitInfo);
      if (!this.IsShiftPressed)
        return this.IsControlPressed;
      return true;
    }

    public override void OnBeforeMouseLeftButtonDown(DependencyObject originalSource)
    {
      this.View.EditorSetInactiveAfterClick = false;
      base.OnBeforeMouseLeftButtonDown(originalSource);
    }

    protected override void OnAssignedToGridCore()
    {
      this.UpdateSelection();
    }

    public override void SetFocusedRowSelected()
    {
      this.ClearSelectedItems();
    }

    public override void SelectRow(int rowHandle)
    {
      if (this.Grid.IsGroupRowHandle(rowHandle))
        return;
      this.SelectRowCore(rowHandle);
    }

    internal override void SelectRowRecursively(int rowHandle)
    {
      if (this.Grid.IsGroupRowHandle(rowHandle))
        this.SelectAllItems(rowHandle);
      else
        this.SelectRowCore(rowHandle);
    }

    private void SelectAllItems(int groupRowHandle)
    {
      this.DoLockedSelectionAction((Action) (() =>
      {
        this.IterateChildren(groupRowHandle, (Action<int>) (rowHandle => this.SelectRowRecursively(rowHandle)));
        this.UpdateGroupRow(groupRowHandle, new bool?(true));
        this.CheckRowSelection(this.GetRootGroupHandle(groupRowHandle));
      }));
    }

    public override void UnselectRow(int rowHandle)
    {
      if (this.Grid.IsGroupRowHandle(rowHandle))
        return;
      this.UnselectRowCore(rowHandle);
    }

    internal override void UnselectRowRecursively(int rowHandle)
    {
      if (this.Grid.IsGroupRowHandle(rowHandle))
        this.UnselectAllItems(rowHandle);
      else
        this.UnselectRowCore(rowHandle);
    }

    private void UnselectAllItems(int groupRowHandle)
    {
      this.DoLockedSelectionAction((Action) (() =>
      {
        this.IterateChildren(groupRowHandle, (Action<int>) (rowHandle => this.UnselectRowRecursively(rowHandle)));
        this.UpdateGroupRow(groupRowHandle, new bool?(false));
        this.CheckRowSelection(this.GetRootGroupHandle(groupRowHandle));
      }));
    }

    public override void SelectAll()
    {
      this.SelectAllRows();
    }

    protected override bool ShouldInvertSelectionOnSpace()
    {
      if (!base.ShouldInvertSelectionOnSpace() && this.DataControl.CurrentColumn != this.View.CheckBoxSelectorColumn)
        return this.Grid.IsGroupRowHandle(this.View.FocusedRowHandle);
      return true;
    }

    protected override void SetIsRowSelectedInternal(int rowHandle, bool isSelected)
    {
      if (isSelected)
        this.SelectRowRecursively(rowHandle);
      else
        this.UnselectRowRecursively(rowHandle);
    }

    protected override bool CanExecuteSelectionActionOnFocusedRowHandleChanged()
    {
      if (!base.CanExecuteSelectionActionOnFocusedRowHandleChanged())
        return false;
      if (this.Grid.IsGroupRowHandle(this.View.FocusedRowHandle))
        return !ModifierKeysHelper.NoModifiers(ModifierKeysHelper.GetKeyboardModifiers());
      return true;
    }

    public override void SelectRange(int startRowHandle, int endRowHandle)
    {
      HashSet<int> rootGroups = new HashSet<int>();
      this.DoLockedSelectionAction((Action) (() =>
      {
        this.SelectRangeCore(startRowHandle, endRowHandle, (Action<int>) (rowHandle =>
        {
          if (this.Grid.IsGroupRowHandle(rowHandle))
          {
            if (this.Grid.IsGroupRowExpanded(rowHandle))
              return;
            this.SelectRowRecursively(rowHandle);
          }
          else
            this.SelectRowCore(rowHandle);
          int parentRowHandle = this.Grid.GetParentRowHandle(rowHandle);
          if (!this.Grid.IsGroupRowHandle(parentRowHandle))
            return;
          rootGroups.Add(this.GetRootGroupHandle(parentRowHandle));
        }));
        this.UpdateGroupRowsSelection((IEnumerable<int>) rootGroups);
      }));
    }

    public override void SelectOnlyThisRange(int startRowHandle, int endRowHandle)
    {
      this.DoLockedSelectionAction((Action) (() =>
      {
        this.DataControl.UnselectAll();
        this.SetGroupRowsSelection(false);
        this.SelectRange(startRowHandle, endRowHandle);
      }));
    }

    public override bool? GetAllItemsSelected()
    {
      return SelectionStrategyBase.AreAllItemsSelected(this.SelectedRowCount, this.TotalRowCount);
    }

    private bool? AreAllChildrenSelected(int groupRowHandle)
    {
      if (this.IsAllItemsUnselected)
        return new bool?(false);
      if (this.IsAllItemsSelected)
        return new bool?(true);
      int childRowCount = this.Grid.GetChildRowCount(groupRowHandle);
      int selectedChildrenCount = 0;
      this.IterateChildren(groupRowHandle, (Action<int>) (rowHandle =>
      {
        if (!this.View.IsRowSelected(rowHandle))
          return;
        ++selectedChildrenCount;
      }));
      return SelectionStrategyBase.AreAllItemsSelected(selectedChildrenCount, childRowCount);
    }

    private void IterateChildren(int groupRowHandle, Action<int> action)
    {
      int childRowCount = this.Grid.GetChildRowCount(groupRowHandle);
      for (int childIndex = 0; childIndex < childRowCount; ++childIndex)
        action(this.Grid.GetChildRowHandle(groupRowHandle, childIndex));
    }

    private bool? HasOneSelectedChild(int groupRowHandle)
    {
      if (this.Grid.GetChildRowCount(groupRowHandle) == 1)
        return new bool?(true);
      return new bool?();
    }

    private bool? AreAllDescendantsSelected(int groupRowHandle)
    {
      if (this.IsAllItemsUnselected)
        return new bool?(false);
      if (this.IsAllItemsSelected)
        return new bool?(true);
      if (this.IsOneDataRowSelected)
        return this.AreAllDescendantsSelected(groupRowHandle, this.Grid.GetSelectedRowHandles()[0]);
      return SelectionStrategyBase.AreAllItemsSelected(this.GetSelectedDescendantsCount(groupRowHandle), this.GetDescendantsCount(groupRowHandle));
    }

    private bool? AreAllDescendantsSelected(int groupRowHandle, int selectedRowHandle)
    {
      int parentRowHandle = this.Grid.GetParentRowHandle(selectedRowHandle);
      if (parentRowHandle == groupRowHandle)
        return this.HasOneSelectedChild(groupRowHandle);
      do
      {
        parentRowHandle = this.Grid.GetParentRowHandle(parentRowHandle);
        if (!this.Grid.IsGroupRowHandle(parentRowHandle))
          return new bool?(false);
      }
      while (parentRowHandle != groupRowHandle);
      return this.AreAllDescendantsSelected(this.AreAllChildrenSelected(groupRowHandle), true);
    }

    private int GetDescendantsCount(int groupRowHandle)
    {
      return this.GetDescendantsCount(groupRowHandle, (Func<int, bool>) (dataRowHandle => true));
    }

    private int GetSelectedDescendantsCount(int groupRowHandle)
    {
      return this.GetDescendantsCount(groupRowHandle, (Func<int, bool>) (dataRowHandle => this.View.IsRowSelected(dataRowHandle)));
    }

    private int GetDescendantsCount(int groupRowHandle, Func<int, bool> dataRowFilter)
    {
      int childrenCount = 0;
      this.IterateDescendants(groupRowHandle, (Action<int>) (rowHandle =>
      {
        if (this.Grid.IsGroupRowHandle(rowHandle) || !dataRowFilter(rowHandle))
          return;
        ++childrenCount;
      }));
      return childrenCount;
    }

    private void IterateDescendants(int groupRowHandle, Action<int> action)
    {
      Stack<int> rowHandles = new Stack<int>((IEnumerable<int>) new List<int>() { groupRowHandle });
      while (rowHandles.Count != 0)
      {
        int num = rowHandles.Pop();
        if (this.Grid.IsGroupRowHandle(num))
          this.PushChildren(rowHandles, num);
        action(num);
      }
    }

    private void PushChildren(Stack<int> rowHandles, int groupRowHandle)
    {
      this.IterateChildren(groupRowHandle, (Action<int>) (rowHandle => rowHandles.Push(rowHandle)));
    }

    public override void OnSelectionChanged(SelectionChangedEventArgs e)
    {
      this.CheckBoxSelectionChangedLocker.DoLockedActionIfNotLocked((Action) (() =>
      {
        this.UpdateGroupRowsSelection(e);
        base.OnSelectionChanged(e);
        this.UpdateCheckBoxes(e);
      }));
      if (!this.CheckBoxSelectionChangedLocker.IsLocked || !this.IsRowSelectedOrUnselected(e))
        return;
      this.RaiseSelectionChanged(e);
    }

    private void UpdateGroupRowsSelection(SelectionChangedEventArgs e)
    {
      if (this.Grid.GroupCount == 0 || this.UpdateGroupRowsLocker.IsLocked)
        return;
      if (this.IsOneDataRowSelected)
        this.UpdateGroupRowsSelection(this.Grid.GetSelectedRowHandles()[0]);
      else if (this.IsDataRowSelectedOrUnselected(e))
      {
        this.UpdateGroupRowSelection(this.Grid.GetParentRowHandle(e.ControllerRow), e.Action == CollectionChangeAction.Add);
      }
      else
      {
        if (e.Action != CollectionChangeAction.Refresh)
          return;
        this.UpdateGroupRowsSelection();
      }
    }

    private void UpdateGroupRowsSelection(int selectedRowHandle)
    {
      this.DoSelectionAction((Action) (() =>
      {
        this.SetGroupRowsSelection(false);
        int parentRowHandle = this.Grid.GetParentRowHandle(selectedRowHandle);
        bool? isSelected = this.HasOneSelectedChild(parentRowHandle);
        this.UpdateGroupRow(parentRowHandle, isSelected);
        this.UpdateGroupRowSelection(this.Grid.GetParentRowHandle(parentRowHandle), true);
      }));
    }

    private void UpdateGroupRowSelection(int groupRowHandle, bool hasSelectedDescendant)
    {
      if (!this.Grid.IsGroupRowHandle(groupRowHandle))
        return;
      bool? isSelected = this.AreAllDescendantsSelected(this.AreAllChildrenSelected(groupRowHandle), hasSelectedDescendant);
      this.UpdateGroupRow(groupRowHandle, isSelected);
      this.UpdateGroupRowSelection(this.Grid.GetParentRowHandle(groupRowHandle), !isSelected.HasValue || isSelected.Value);
    }

    private bool? AreAllDescendantsSelected(bool? allChildrenSelected, bool hasSelectedDescendant)
    {
      if (allChildrenSelected.HasValue && allChildrenSelected.Value)
        return new bool?(true);
      if (!allChildrenSelected.HasValue || hasSelectedDescendant)
        return new bool?();
      return new bool?(false);
    }

    private void UpdateGroupRowsSelection()
    {
      this.DoSelectionAction((Action) (() =>
      {
        if (this.IsAllItemsUnselected)
          this.SetGroupRowsSelection(false);
        else if (this.IsAllItemsSelected)
          this.SetGroupRowsSelection(true);
        else
          this.UpdateGroupRowsSelection(this.Grid.DataController.GroupInfo.Where<GroupRowInfo>((Func<GroupRowInfo, bool>) (groupInfo => (int) groupInfo.Level == 0)).Select<GroupRowInfo, int>((Func<GroupRowInfo, int>) (groupInfo => groupInfo.Handle)));
      }));
    }

    private void UpdateGroupRowsSelection(IEnumerable<int> groupRowHandles)
    {
      foreach (int groupRowHandle in groupRowHandles)
        this.CheckRowSelection(groupRowHandle);
    }

    private void SetGroupRowsSelection(bool isSelected)
    {
      foreach (GroupRowInfo groupRowInfo in (Collection<GroupRowInfo>) this.Grid.DataController.GroupInfo)
        this.UpdateGroupRow(groupRowInfo.Handle, new bool?(isSelected));
    }

    private bool? CheckRowSelection(int rowHandle)
    {
      if (!this.Grid.IsGroupRowHandle(rowHandle))
        return new bool?(this.View.IsRowSelected(rowHandle));
      bool hasSelectedDescendant = false;
      int selectedChildrenCount = 0;
      this.IterateChildren(rowHandle, (Action<int>) (childRowHandle =>
      {
        bool? nullable = this.CheckRowSelection(childRowHandle);
        if (nullable.HasValue && nullable.Value)
          ++selectedChildrenCount;
        if (nullable.HasValue && !nullable.Value)
          return;
        hasSelectedDescendant = true;
      }));
      bool? isSelected = this.AreAllDescendantsSelected(SelectionStrategyBase.AreAllItemsSelected(selectedChildrenCount, this.Grid.GetChildRowCount(rowHandle)), hasSelectedDescendant);
      this.UpdateGroupRow(rowHandle, isSelected);
      return isSelected;
    }

    private void UpdateCheckBoxes(SelectionChangedEventArgs e)
    {
      this.View.UpdateCellDataValues((ColumnBase) this.View.CheckBoxSelectorColumn);
    }

    private void UpdateGroupRow(int groupRowHandle, bool? isSelected)
    {
      if (isSelected.HasValue && isSelected.Value)
        this.SelectRowCore(groupRowHandle);
      else
        this.UnselectRowCore(groupRowHandle);
      this.View.UpdateRowDataByRowHandle(groupRowHandle, (UpdateRowDataDelegate) (rowData =>
      {
        GroupRowData groupRowData = rowData as GroupRowData;
        if (groupRowData == null)
          return;
        groupRowData.SetAllItemsSelected(isSelected);
      }));
    }

    internal override void SelectOnlyThisMasterDetailRange(int startCommonVisibleIndex, int endCommonVisibleIndex)
    {
      this.SelectOnlyThisRange(this.DataControl.GetRowHandleByVisibleIndexCore(startCommonVisibleIndex), this.DataControl.GetRowHandleByVisibleIndexCore(endCommonVisibleIndex));
    }
  }
}
