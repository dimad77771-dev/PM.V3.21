// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridTableViewBehavior
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Windows;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class GridTableViewBehavior : GridTableViewBehaviorBase
  {
    private DependencyObject newItemRowState = (DependencyObject) new RowStateObject();

    protected TableView GridTableView
    {
      get
      {
        return (TableView) this.View;
      }
    }

    protected internal override Style AutoFilterRowCellStyle
    {
      get
      {
        return this.GridTableView.AutoFilterRowCellStyle;
      }
    }

    internal override bool IsNewItemRowVisible
    {
      get
      {
        return this.GridTableView.ActualNewItemRowPosition == NewItemRowPosition.Top;
      }
    }

    protected internal override bool IsNewItemRowFocused
    {
      get
      {
        return this.View.FocusedRowHandle == -2147483647;
      }
    }

    protected internal override bool IsNewItemRowEditing
    {
      get
      {
        return this.GridTableView.GridDataProvider.IsNewItemRowEditing;
      }
    }

    protected internal override Brush ActualAlternateRowBackground
    {
      get
      {
        return this.GridTableView.ActualAlternateRowBackground;
      }
    }

    public GridTableViewBehavior(TableView view)
      : base((DataViewBase) view)
    {
    }

    protected internal override bool IsAdditionalRow(int rowHandle)
    {
      if (!base.IsAdditionalRow(rowHandle))
        return false;
      if (this.View.ShouldDisplayBottomRow)
        return rowHandle != -2147483647;
      return true;
    }

    protected internal override bool IsAdditionalRowData(RowData rowData)
    {
      if (!base.IsAdditionalRowData(rowData))
        return rowData == this.GridTableView.NewItemRowData;
      return true;
    }

    protected internal override bool IsAlternateRow(int rowHandle)
    {
      if (this.GridTableView.AlternationCount < 1 || this.GridTableView.ActualAlternateRowBackground == null)
        return false;
      return rowHandle % this.GridTableView.AlternationCount == this.GridTableView.AlternationCount - 1;
    }

    internal override int GetValueForSelectionAnchorRowHandle(int value)
    {
      if (value == -2147483647 && this.GridTableView.ActualNewItemRowPosition == NewItemRowPosition.Top)
        return this.GridTableView.Grid.GetRowHandleByVisibleIndex(0);
      return base.GetValueForSelectionAnchorRowHandle(value);
    }

    protected internal override void UpdateAdditionalRowsData()
    {
      if (this.IsNewItemRowVisible)
        this.GridTableView.NewItemRowData.UpdateData();
      base.UpdateAdditionalRowsData();
    }

    protected internal override DependencyObject GetRowState(int rowHandle)
    {
      if (rowHandle == -2147483647)
        return this.newItemRowState;
      return base.GetRowState(rowHandle);
    }

    internal override bool CheckNavigationStyle(int newValue)
    {
      if (base.CheckNavigationStyle(newValue))
        return newValue != -2147483647;
      return false;
    }

    protected internal override void UpdateRowData(UpdateRowDataDelegate updateMethod, bool updateInvisibleRows = true, bool updateFocusedRow = true)
    {
      base.UpdateRowData(updateMethod, updateInvisibleRows, updateFocusedRow);
      if (!updateInvisibleRows && !this.IsNewItemRowVisible)
        return;
      updateMethod(this.GridTableView.NewItemRowData);
    }

    protected internal override RowData GetRowData(int rowHandle)
    {
      if (rowHandle == -2147483647 && this.GridTableView.ActualNewItemRowPosition != NewItemRowPosition.Bottom)
        return this.GridTableView.NewItemRowData;
      return base.GetRowData(rowHandle);
    }

    protected override void UpdateAdditionalFocusedRowDataCore()
    {
      if (this.IsNewItemRowFocused)
        this.View.FocusedRowData = this.GridTableView.NewItemRowData;
      base.UpdateAdditionalFocusedRowDataCore();
    }

    protected override int GetAdditionalRowHandle()
    {
      if (!this.IsNewItemRowVisible)
        return base.GetAdditionalRowHandle();
      return -2147483647;
    }

    protected override bool CanNavigateToAdditionalRow(bool allowNavigateToAutoFilterRow)
    {
      if (!base.CanNavigateToAdditionalRow(allowNavigateToAutoFilterRow))
        return this.IsNewItemRowVisible;
      return true;
    }

    protected internal override double GetFixedNoneContentWidth(double totalWidth, int rowHandle)
    {
      if (this.View.DataControl != null && !this.HasFixedLeftElements && (!this.View.DataControl.IsGroupRowHandleCore(rowHandle) && !this.IsAdditionalRowCore(rowHandle)))
        return totalWidth - this.ViewInfo.RightGroupAreaIndent - this.FirstColumnIndent;
      if (this.IsAdditionalRowCore(rowHandle) && !this.HasFixedLeftElements && !this.TableView.ShowIndicator)
        return totalWidth - this.TableView.LeftDataAreaIndent;
      return totalWidth;
    }

    internal override int GetTopRow(int pageVisibleTopRowIndex)
    {
      return this.GridTableView.Grid.GetDataRowHandleByGroupRowHandle(this.GridTableView.Grid.GetRowHandleByVisibleIndex(pageVisibleTopRowIndex));
    }

    internal override CustomBestFitEventArgsBase RaiseCustomBestFit(ColumnBase column, BestFitMode bestFitMode)
    {
      CustomBestFitEventArgs e = new CustomBestFitEventArgs((GridColumn) column, bestFitMode);
      this.GridTableView.RaiseCustomBestFit(e);
      return (CustomBestFitEventArgsBase) e;
    }

    internal override void UpdateActualExpandDetailButtonWidth()
    {
      this.View.UpdateAllDependentViews((Action<DataViewBase>) (view => ((TableView) view).ActualExpandDetailButtonWidth = this.GridTableView.ExpandDetailButtonWidth));
    }

    internal override void UpdateActualDetailMargin()
    {
      this.View.UpdateAllDependentViews((Action<DataViewBase>) (view => ((TableView) view).ActualDetailMargin = this.GridTableView.DetailMargin));
    }

    protected internal override void OnFocusedRowCellModified()
    {
      if (this.IsNewItemRowFocused && !this.GridTableView.GridDataProvider.IsNewItemRowEditing)
        this.GridTableView.AddNewRow();
      base.OnFocusedRowCellModified();
    }

    protected internal override void BeforeShowEditForm()
    {
      if (!this.IsNewItemRowFocused || this.IsNewItemRowEditing)
        return;
      this.GridTableView.AddNewRow();
    }

    internal override GridColumnData GetGroupSummaryColumnData(int rowHandle, IBestFitColumn column)
    {
      ColumnBase column1 = (ColumnBase) column;
      RowData rowData = (RowData) null;
      GridGroupSummaryColumnData cellData = (GridGroupSummaryColumnData) null;
      if (this.View.VisualDataTreeBuilder.GroupSummaryRows.TryGetValue(rowHandle, out rowData))
        cellData = ((GroupRowData) rowData).SafeGetGroupSummaryColumnData(column1);
      if (cellData == null)
      {
        GroupSummaryRowData groupSummaryRowData = new GroupSummaryRowData((DataTreeBuilder) this.View.VisualDataTreeBuilder, new RowHandle(rowHandle));
        groupSummaryRowData.AssignFrom(rowHandle);
        cellData = groupSummaryRowData.SafeGetGroupSummaryColumnData(column1);
        if (cellData == null)
        {
          cellData = new GridGroupSummaryColumnData((GroupRowData) groupSummaryRowData);
          groupSummaryRowData.UpdateGridGroupSummaryData(column1, cellData);
        }
      }
      return (GridColumnData) cellData;
    }

    internal override void SetGroupElementsForBestFit(FrameworkElement element, IBestFitColumn column, int rowHandle)
    {
      ColumnBase column1 = (ColumnBase) column;
      IBestFitGroupRow bestFitGroupRow = element as IBestFitGroupRow;
      GroupRowData rowData = (GroupRowData) new StandaloneGroupRowData((DataTreeBuilder) this.View.VisualDataTreeBuilder);
      bestFitGroupRow.SetRowData(rowData);
      rowData.AssignFrom(rowHandle);
      GridGroupSummaryColumnData bestFitData = GridGroupSummaryColumnData.CreateBestFitData(rowData);
      rowData.UpdateGridGroupSummaryData(column1, bestFitData);
      bool flag = this.IsFirstColumn((BaseColumn) column);
      if (bestFitData == null || !bestFitData.HasSummary || (bestFitData.SummaryTextInfo == null || string.IsNullOrEmpty(bestFitData.SummaryTextInfo.TextSource)))
      {
        bestFitGroupRow.SummaryData = (GridGroupSummaryColumnData) null;
        if (!flag)
        {
          bestFitGroupRow.SetRowData((GroupRowData) null);
          bestFitGroupRow.UpdateContent();
          return;
        }
      }
      else
        bestFitGroupRow.SummaryData = bestFitData;
      if (flag)
      {
        bestFitGroupRow.IsFirstColumn = true;
        rowData.InitGroupValue();
        rowData.GroupValue.Column = column1;
        rowData.GroupValue.ColumnHeader = this.View.GetGroupRowHeaderCaption(rowHandle);
        rowData.GroupValue.Value = (object) this.View.GetGroupRowDisplayText(rowHandle);
        object groupDisplayValue = this.View.GetGroupDisplayValue(rowHandle);
        rowData.GroupValue.Text = groupDisplayValue != null ? groupDisplayValue.ToString() : (string) null;
      }
      else
        bestFitGroupRow.IsFirstColumn = false;
      bestFitGroupRow.UpdateContent();
    }

    protected internal override MouseMoveSelectionBase GetMouseMoveSelection(IDataViewHitInfo hitInfo)
    {
      if (this.GridTableView.ShowCheckBoxSelectorColumn && this.GridTableView.RetainSelectionOnClickOutsideCheckBoxSelector)
        return (MouseMoveSelectionBase) MouseMoveSelectionNone.Instance;
      return base.GetMouseMoveSelection(hitInfo);
    }

    protected internal override FrameworkElement GetAdditionalRowElement(int rowHandle)
    {
      if (rowHandle == -2147483647 && this.IsNewItemRowVisible && !this.View.IsRootView)
      {
        RowDataBase rowDataBase = (RowDataBase) this.DataControl.DataControlParent.GetNewItemRowData();
        if (rowDataBase != null && rowDataBase.WholeRowElement != null)
          return LayoutHelper.FindElementByName(rowDataBase.WholeRowElement, "PART_NewItemRow");
      }
      return base.GetAdditionalRowElement(rowHandle);
    }
  }
}
