// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridColumnMenuInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Data;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class GridColumnMenuInfo : ColumnMenuInfoBase
  {
    internal readonly GridGroupSummaryHelper summaryHelper;

    public GridViewBase View
    {
      get
      {
        return (GridViewBase) base.View;
      }
    }

    public GridControl Grid
    {
      get
      {
        return (GridControl) this.DataControl;
      }
    }

    public GridColumn Column
    {
      get
      {
        return (GridColumn) base.Column;
      }
    }

    protected GridViewBase TargetView
    {
      get
      {
        if (this.View.HasClonedDetails)
          return (GridViewBase) this.View.DataControl.DetailClones.First<DataControlBase>().viewCore;
        return this.View;
      }
    }

    public GridColumnMenuInfo(GridPopupMenu menu)
      : base((DataControlPopupMenu) menu)
    {
      this.summaryHelper = new GridGroupSummaryHelper((DataViewBase) this.TargetView);
    }

    protected override void CreateItemsCore()
    {
      this.CreateFullExpandCollapseItems();
      this.CreateSortingItems();
      this.CreateGroupSummarySortInfoItems();
      this.CreateGroupingItems();
      this.CreateColumnChooserItems();
      this.CreateBestFitItems();
      this.CreateGroupSummaryEditorItems();
      this.CreateExpressionEditorItems();
      this.CreateFilterControlItems();
      this.CreateSearchPanelItems();
      this.CreateFixedStyleItems();
      this.CreateConditionalFormattingMenuItems();
    }

    protected override bool CanClearSorting()
    {
      return !this.Column.IsGrouped;
    }

    protected override bool CanCreateFixedStyleMenu()
    {
      if ((!this.Column.IsGrouped || this.View.ShowGroupedColumns) && this.Grid.Bands.Count == 0)
        return !TableView.IsCheckBoxSelectorColumn(this.Column.FieldName);
      return false;
    }

    private void CreateGroupSummaryEditorItems()
    {
      if (!this.Column.IsGrouped)
        return;
      this.CreateBarButtonItem("ItemGroupSummaryEditor", GridControlStringId.MenuColumnGroupSummaryEditor, true, (ImageSource) null, (ICommand) DelegateCommandFactory.Create((Action) (() => this.summaryHelper.ShowSummaryEditor()), (Func<bool>) (() =>
      {
        if (!this.View.IsRootView)
          return this.View.HasClonedDetails;
        return true;
      }), false), (object) null);
    }

    private void CreateBestFitItems()
    {
      if (!(this.View is TableView))
        return;
      BarButtonItem barButtonItem = this.CreateBarButtonItem("BestFit", GridControlStringId.MenuColumnBestFit, false, (ImageSource) ImageHelper.GetImage("BestFit"), ((this.View as TableView).Commands as TableViewCommands).BestFitColumn, (object) null);
      barButtonItem.CommandParameter = (object) this.Column;
      barButtonItem.IsVisible = this.View.ViewBehavior.CanBestFitColumnCore((ColumnBase) this.Column) && this.View.IsColumnVisibleInHeaders((BaseColumn) this.Column);
      this.CreateBarButtonItem("BestFitColumns", GridControlStringId.MenuColumnBestFitColumns, false, (ImageSource) null, ((this.View as TableView).Commands as TableViewCommands).BestFitColumns, (object) null).IsVisible = this.View.ViewBehavior.CanBestFitAllColumns();
    }

    private void CreateGroupingItems()
    {
      this.CreateGroupByItem();
      this.CreateGroupPanelItem();
      if (!this.Column.IsGrouped || !this.IsDateTimeColumn() || (!this.View.AllowDateTimeGroupIntervalMenu || !this.IsGroupIntervalSupported()))
        return;
      BarSubItem barSubItem = this.CreateBarSubItem("GroupInterval", GridControlStringId.MenuColumnGroupInterval, false, (ImageSource) null, (ICommand) null);
      this.CreateGroupIntervalItem("GroupIntervalNone", GridControlStringId.MenuColumnGroupIntervalNone, barSubItem.ItemLinks, ColumnGroupInterval.Default);
      this.CreateGroupIntervalItem("GroupIntervalDay", GridControlStringId.MenuColumnGroupIntervalDay, barSubItem.ItemLinks, ColumnGroupInterval.Date);
      this.CreateGroupIntervalItem("GroupIntervalMonth", GridControlStringId.MenuColumnGroupIntervalMonth, barSubItem.ItemLinks, ColumnGroupInterval.DateMonth);
      this.CreateGroupIntervalItem("GroupIntervalYear", GridControlStringId.MenuColumnGroupIntervalYear, barSubItem.ItemLinks, ColumnGroupInterval.DateYear);
      this.CreateGroupIntervalItem("GroupIntervalSmart", GridControlStringId.MenuColumnGroupIntervalSmart, barSubItem.ItemLinks, ColumnGroupInterval.DateRange);
    }

    private void CreateGroupByItem()
    {
      if (!this.Column.IsGrouped && this.Column.Visible && (!this.View.ShowGroupedColumns && this.View.IsLastVisibleColumn((BaseColumn) this.Column)))
        return;
      string str = this.Column.IsGrouped ? "ItemUnGroupColumn" : "ItemGroupColumn";
      this.CreateBarButtonItem(str, this.Column.IsGrouped ? GridControlStringId.MenuColumnUnGroup : GridControlStringId.MenuColumnGroup, true, (ImageSource) ImageHelper.GetImage(str), (ICommand) DelegateCommandFactory.Create((Action) (() => this.GroupColumn()), (Func<bool>) (() => this.DataControl.DataControlOwner.CanGroupColumn((ColumnBase) this.Column)), false), (object) null);
    }

    private bool IsGroupIntervalSupported()
    {
      if (!this.View.DataProviderBase.IsICollectionView)
        return !this.View.DataControl.IsWcfSource();
      return false;
    }

    protected void CreateGroupPanelItem()
    {
      this.CreateBarButtonItem("ItemGroupBox", !this.View.ShowGroupPanel ? GridControlStringId.MenuColumnShowGroupPanel : GridControlStringId.MenuColumnHideGroupPanel, false, (ImageSource) ImageHelper.GetImage("ItemGroupBox"), (ICommand) DelegateCommandFactory.Create((Action) (() => this.View.ShowGroupPanel = !this.View.ShowGroupPanel), (Func<bool>) (() => true), false), (object) null);
    }

    private bool IsDateTimeColumn()
    {
      Type columnType = this.TargetView.GetColumnType((ColumnBase) this.Column, (DataProviderBase) null);
      if (columnType == (Type) null)
        return false;
      if (Type.GetTypeCode(columnType) == TypeCode.DateTime)
        return true;
      Type underlyingType = Nullable.GetUnderlyingType(columnType);
      return underlyingType != (Type) null && Type.GetTypeCode(underlyingType) == TypeCode.DateTime;
    }

    private void CreateGroupSummarySortInfoItems()
    {
      if (!this.Column.IsGrouped || this.Grid.GroupSummary.Count <= 0)
        return;
      this.CreateSummarySortInfo();
    }

    private void CreateFullExpandCollapseItems()
    {
      if (!this.Column.IsGrouped)
        return;
      this.CreateBarButtonItem("ItemFullExpand", GridControlStringId.MenuGroupPanelFullExpand, false, (ImageSource) ImageHelper.GetImage("ItemFullExpand"), this.View.GridViewCommands.ExpandAllGroups, (object) null);
      this.CreateBarButtonItem("ItemFullCollapse", GridControlStringId.MenuGroupPanelFullCollapse, false, (ImageSource) ImageHelper.GetImage("ItemFullCollapse"), this.View.GridViewCommands.CollapseAllGroups, (object) null);
    }

    private void CreateGroupIntervalItem(string name, GridControlStringId id, BarItemLinkCollection links, ColumnGroupInterval groupIntreval)
    {
      this.Menu.CreateBarCheckItem(name, (object) this.View.GetLocalizedString(id), new bool?(this.Column.GroupInterval == groupIntreval), false, (ImageSource) null, links).Command = (ICommand) DelegateCommandFactory.Create((Action) (() => this.Column.GroupInterval = groupIntreval), (Func<bool>) (() => true), false);
    }

    private void GroupColumn()
    {
      if (this.Column.IsGrouped)
      {
        this.Grid.UngroupBy(this.Column);
      }
      else
      {
        this.Column.Visible = true;
        this.Grid.GroupBy(this.Column, this.Column.SortOrder == ColumnSortOrder.None ? ColumnSortOrder.Ascending : this.Column.SortOrder);
      }
    }

    protected virtual void CreateSummarySortInfo()
    {
      if (this.Grid.GroupSummarySortInfo.Count > 0)
        this.CreateBarButtonItem("ItemClearGroupSummarySorting", GridControlStringId.MenuColumnResetGroupSummarySort, false, (ImageSource) null, (ICommand) DelegateCommandFactory.Create((Action) (() => this.Grid.GroupSummarySortInfo.Clear()), (Func<bool>) (() => true), false), (object) null);
      BarSubItem barSubItem = this.CreateBarSubItem("ItemSortBySummary", GridControlStringId.MenuColumnSortGroupBySummaryMenu, false, (ImageSource) null, (ICommand) null);
      foreach (GridSummaryItem gridSummaryItem in (Collection<GridSummaryItem>) this.Grid.GroupSummary)
      {
        if (gridSummaryItem.Visible && gridSummaryItem.SummaryType != SummaryItemType.None)
        {
          this.CreateSummarySortMenuItem(gridSummaryItem, ColumnSortOrder.Ascending, barSubItem.ItemLinks, barSubItem.ItemLinks.Count > 0);
          this.CreateSummarySortMenuItem(gridSummaryItem, ColumnSortOrder.Descending, barSubItem.ItemLinks, false);
        }
      }
    }

    protected BarButtonItem CreateSummarySortMenuItem(GridSummaryItem item, ColumnSortOrder order, BarItemLinkCollection links, bool beginGroup)
    {
      string localizedString = this.View.GetLocalizedString(GridControlStringId.MenuColumnGroupSummarySortFormat);
      string str1 = order == ColumnSortOrder.Ascending ? this.View.GetLocalizedString(GridControlStringId.MenuColumnSortBySummaryAscending) : this.View.GetLocalizedString(GridControlStringId.MenuColumnSortBySummaryDescending);
      GridControlStringId? groupSummaryStringId = GridControlLocalizer.GetMenuSortByGroupSummaryStringId(item.SummaryType);
      string str2 = groupSummaryStringId.HasValue ? this.View.GetLocalizedString(groupSummaryStringId.Value) : item.SummaryType.ToString();
      BarButtonItem barButtonItem = this.Menu.CreateBarButtonItem(this.Menu.GetItemName() + (object) item.SummaryType + (object) order + (object) item.GetHashCode(), (object) string.Format(localizedString, (object) this.GetDisplaySummaryName(item), (object) str2, (object) str1), (beginGroup ? 1 : 0) != 0, (ImageSource) null, links);
      barButtonItem.Command = (ICommand) DelegateCommandFactory.Create((Action) (() => this.GroupBySummary(item, order)), (Func<bool>) (() => this.GetIsGroupSummaryEnabled(this.Column.FieldName, item, order)), false);
      return barButtonItem;
    }

    private bool GetIsGroupSummaryEnabled(string fieldName, GridSummaryItem item, ColumnSortOrder sortOrder)
    {
      bool flag = true;
      foreach (GridGroupSummarySortInfo groupSummarySortInfo in (Collection<GridGroupSummarySortInfo>) this.Grid.GroupSummarySortInfo)
      {
        if (groupSummarySortInfo.FieldName == fieldName && groupSummarySortInfo.SummaryItem == item)
          flag = groupSummarySortInfo.GetSortOrder() != sortOrder;
      }
      return flag;
    }

    private void GroupBySummary(GridSummaryItem summaryItem, ColumnSortOrder sortOrder)
    {
      if (!this.Column.IsGrouped)
        return;
      List<GridGroupSummarySortInfo> groupSummarySortInfoList = new List<GridGroupSummarySortInfo>();
      foreach (GridGroupSummarySortInfo groupSummarySortInfo in (Collection<GridGroupSummarySortInfo>) this.Grid.GroupSummarySortInfo)
      {
        if (!(groupSummarySortInfo.FieldName == this.Column.FieldName))
          groupSummarySortInfoList.Add(groupSummarySortInfo);
      }
      groupSummarySortInfoList.Add(new GridGroupSummarySortInfo(summaryItem, this.Column.FieldName, sortOrder == ColumnSortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending));
      this.Grid.GroupSummarySortInfo.ClearAndAddRange(groupSummarySortInfoList.ToArray());
    }

    private string GetDisplaySummaryName(GridSummaryItem item)
    {
      return ColumnBase.GetSummaryDisplayName((ColumnBase) this.View.Columns[item.FieldName], (SummaryItemBase) item);
    }

    protected override void ExecuteMenuController()
    {
      base.ExecuteMenuController();
      this.Menu.ExecuteOriginationViewMenuController((Func<DataViewBase, BarManagerMenuController>) (view => view.ColumnMenuController));
    }
  }
}
