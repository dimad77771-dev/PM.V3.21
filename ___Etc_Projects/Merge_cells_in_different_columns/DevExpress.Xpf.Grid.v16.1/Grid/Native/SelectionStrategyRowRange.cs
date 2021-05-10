// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.SelectionStrategyRowRange
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Collections.Generic;

namespace DevExpress.Xpf.Grid.Native
{
  public class SelectionStrategyRowRange : SelectionStrategyRow
  {
    private HashSet<int> selectedOldRowHandle = new HashSet<int>();
    private List<int> selectedRowHandle = new List<int>();
    private readonly SelectionStrategyRowRangeHelper helper;
    private bool isRowIndicator;

    public SelectionStrategyRowRange(GridViewBase view)
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

    protected override bool ShouldProcessMouseClick(IDataViewHitInfo hitInfo)
    {
      if (this.view.GetActualSelectionMode() == MultiSelectMode.MultipleRow)
        return base.ShouldProcessMouseClick(hitInfo);
      if (hitInfo.RowHandle == int.MinValue && !hitInfo.IsDataArea)
        return false;
      if (this.IsMultipleMode)
        return !this.RootView.IsExpandButton(hitInfo);
      return true;
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
        if (this.HasValidationError || this.IsRightMouseButtonPressed || (!this.ShouldProcessMouseClick(hitInfo) || this.view.IsEditing))
          return;
        this.isRowIndicator = false;
        ITableViewHitInfo tableViewHitInfo = hitInfo as ITableViewHitInfo;
        this.BeginSelection();
        if (hitInfo != null && tableViewHitInfo != null && tableViewHitInfo.IsRowIndicator)
        {
          base.OnAfterMouseLeftButtonDown(hitInfo);
          this.isRowIndicator = true;
        }
        else if (this.ShouldProcessMouseClick(hitInfo))
          this.helper.OnAfterMouseLeftButtonDown(hitInfo, new Func<IDataViewHitInfo, bool>(this.OnAfterMouseLeftButtonCore), this.DataControl);
        this.EndSelection();
      }
    }

    private bool OnAfterMouseLeftButtonCore(IDataViewHitInfo hitInfo)
    {
      if (this.HasValidationError || this.IsRightMouseButtonPressed || !this.ShouldProcessMouseClick(hitInfo))
        return false;
      int rowHandle = hitInfo.RowHandle;
      if (!this.CanInvertSelection(hitInfo))
        return false;
      this.InvertSelectionOnClick(hitInfo);
      return !this.view.IsRowSelected(hitInfo.RowHandle);
    }
  }
}
