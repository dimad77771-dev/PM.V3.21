// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListSelectionStrategyRowRange
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListSelectionStrategyRowRange : TreeListSelectionStrategyRow
  {
    private HashSet<int> selectedOldRowHandle = new HashSet<int>();
    private List<int> selectedRowHandle = new List<int>();
    private int? ancor = new int?();
    private readonly SelectionStrategyRowRangeHelper helper;
    private bool isRowIndicator;

    public TreeListSelectionStrategyRowRange(TreeListView view)
      : base(view)
    {
      this.helper = new SelectionStrategyRowRangeHelper((SelectionStrategyRowBase) this);
    }

    public override void SelectRowRange(IEnumerable<int> selectedRowsHandles)
    {
      if (this.view.GetActualSelectionMode() == MultiSelectMode.MultipleRow)
      {
        base.SelectRowRange(selectedRowsHandles);
      }
      else
      {
        this.BeginSelection();
        this.helper.SelectRowRange(selectedRowsHandles);
        this.EndSelection();
      }
    }

    internal override void SelectOnlyThisMasterDetailRange(int startCommonVisibleIndex, int endCommonVisibleIndex)
    {
      if (this.view.GetActualSelectionMode() == MultiSelectMode.MultipleRow)
        base.SelectOnlyThisMasterDetailRange(startCommonVisibleIndex, endCommonVisibleIndex);
      else if (this.isRowIndicator)
        base.SelectOnlyThisMasterDetailRange(startCommonVisibleIndex, endCommonVisibleIndex);
      else
        this.helper.SelectOnlyThisMasterDetailRange(startCommonVisibleIndex, endCommonVisibleIndex);
    }

    public override void OnAfterMouseLeftButtonDown(IDataViewHitInfo hitInfo)
    {
      if (this.view.GetActualSelectionMode() == MultiSelectMode.MultipleRow)
      {
        base.OnAfterMouseLeftButtonDown(hitInfo);
      }
      else
      {
        if (this.HasValidationError || Mouse.RightButton == MouseButtonState.Pressed || (hitInfo.RowHandle == int.MinValue || this.view.IsEditing))
          return;
        this.isRowIndicator = false;
        ITableViewHitInfo tableViewHitInfo = hitInfo as ITableViewHitInfo;
        if (hitInfo != null && tableViewHitInfo != null && tableViewHitInfo.IsRowIndicator)
        {
          this.BeginSelection();
          base.OnAfterMouseLeftButtonDown(hitInfo);
          this.isRowIndicator = true;
          this.EndSelection();
        }
        else
        {
          TreeListViewHitInfo treeListViewHitInfo = (TreeListViewHitInfo) hitInfo;
          if (treeListViewHitInfo.InNodeIndent || treeListViewHitInfo.InNodeExpandButton || (!this.IsValidRowHandle(hitInfo.RowHandle) || this.HasValidationError))
            return;
          this.BeginSelection();
          if (this.IsShiftPressed)
          {
            if (!this.ancor.HasValue)
              this.ancor = new int?(this.TreeListView.FocusedRowHandle);
            else
              this.ClearSelection();
            this.SelectRange(this.TreeListView.FocusedRowHandle, this.ancor.Value);
            this.TreeListView.EditorSetInactiveAfterClick = true;
          }
          else
          {
            this.helper.OnAfterMouseLeftButtonDown(hitInfo, new Func<IDataViewHitInfo, bool>(this.OnAfterMouseLeftButtonCore), this.DataControl);
            this.ancor = new int?(this.TreeListView.FocusedRowHandle);
          }
          this.EndSelection();
        }
      }
    }

    private bool OnAfterMouseLeftButtonCore(IDataViewHitInfo hitInfo)
    {
      if (this.HasValidationError || Mouse.RightButton == MouseButtonState.Pressed || hitInfo.RowHandle == int.MinValue)
        return false;
      int rowHandle = hitInfo.RowHandle;
      if (!this.CanInvertSelection(hitInfo))
        return false;
      this.InvertRowSelection(hitInfo.RowHandle);
      return !this.view.IsRowSelected(hitInfo.RowHandle);
    }
  }
}
