// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.ColumnMenuInfoBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core.ConditionalFormatting.Native;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public abstract class ColumnMenuInfoBase : GridMenuInfo, IConditionalFormattingDialogBuilder
  {
    protected ColumnSortOrder SortOrder
    {
      get
      {
        if (this.Column != null)
          return this.Column.SortOrder;
        return ColumnSortOrder.None;
      }
    }

    private HeaderPresenterType HeaderPresenterType { get; set; }

    public override GridMenuType MenuType
    {
      get
      {
        return GridMenuType.Column;
      }
    }

    public override bool CanCreateItems
    {
      get
      {
        return this.View.IsColumnMenuEnabled;
      }
    }

    public override BarManagerMenuController MenuController
    {
      get
      {
        return this.View.ColumnMenuController;
      }
    }

    public ColumnMenuInfoBase(DataControlPopupMenu menu)
      : base(menu)
    {
    }

    protected void CreateFixedStyleItems()
    {
      if (!this.View.ViewBehavior.CanShowFixedColumnMenu || !this.CanCreateFixedStyleMenu())
        return;
      BarSubItem barSubItem = this.CreateBarSubItem("FixedStyle", GridControlStringId.MenuColumnFixedStyle, true, (ImageSource) null, (ICommand) null);
      this.CreateFixedStyleItem("FixedStyleNone", GridControlStringId.MenuColumnFixedNone, (ImageSource) ImageHelper.GetImage("FixedNone"), barSubItem.ItemLinks, FixedStyle.None);
      this.CreateFixedStyleItem("FixedStyleLeft", GridControlStringId.MenuColumnFixedLeft, (ImageSource) ImageHelper.GetImage("FixedLeft"), barSubItem.ItemLinks, FixedStyle.Left);
      this.CreateFixedStyleItem("FixedStyleRight", GridControlStringId.MenuColumnFixedRight, (ImageSource) ImageHelper.GetImage("FixedRight"), barSubItem.ItemLinks, FixedStyle.Right);
      if (this.View.CanBecameFixed(this.BaseColumn))
        return;
      barSubItem.ItemLinks[1].IsEnabled = false;
      barSubItem.ItemLinks[2].IsEnabled = false;
    }

    protected virtual bool CanCreateFixedStyleMenu()
    {
      return true;
    }

    private void CreateFixedStyleItem(string name, GridControlStringId id, ImageSource image, BarItemLinkCollection links, FixedStyle fixedStyle)
    {
      this.Menu.CreateBarCheckItem(name, (object) this.View.GetLocalizedString(id), new bool?(this.BaseColumn.Fixed == fixedStyle), false, image, links).Command = (ICommand) DelegateCommandFactory.Create((Action) (() => this.BaseColumn.Fixed = fixedStyle), (Func<bool>) (() => true), false);
    }

    protected void CreateSortingItems()
    {
      this.CreateBarCheckItem("ItemSortAscending", GridControlStringId.MenuColumnSortAscending, new bool?(this.SortOrder == ColumnSortOrder.Ascending), !this.CanClearSorting(), (ImageSource) ImageHelper.GetImage("ItemSortAscending"), (ICommand) DelegateCommandFactory.Create((Action) (() => this.SetColumnSortOrder(ColumnSortOrder.Ascending)), (Func<bool>) (() => this.CanSortColumn(this.Column)), false));
      this.CreateBarCheckItem("ItemSortDescending", GridControlStringId.MenuColumnSortDescending, new bool?(this.SortOrder == ColumnSortOrder.Descending), false, (ImageSource) ImageHelper.GetImage("ItemSortDescending"), (ICommand) DelegateCommandFactory.Create((Action) (() => this.SetColumnSortOrder(ColumnSortOrder.Descending)), (Func<bool>) (() => this.CanSortColumn(this.Column)), false));
      this.CreateBarButtonItem("ItemClearSorting", GridControlStringId.MenuColumnClearSorting, false, (ImageSource) ImageHelper.GetImage("ItemClearSorting"), (ICommand) DelegateCommandFactory.Create((Action) (() => this.ClearSorting()), (Func<bool>) (() =>
      {
        if (this.CanSortColumn(this.Column) && this.CanClearSorting())
          return this.Column.SortOrder != ColumnSortOrder.None;
        return false;
      }), false), (object) null);
    }

    protected bool CanSortColumn(ColumnBase column)
    {
      return this.DataControl.DataControlOwner.CanSortColumn(column);
    }

    protected virtual bool CanClearSorting()
    {
      return true;
    }

    private void ClearSorting()
    {
      this.DataControl.SortInfoCore.OnColumnHeaderClickRemoveSort(this.Column.FieldName);
    }

    private void SetColumnSortOrder(ColumnSortOrder sortOrder)
    {
      this.SetColumnSortOrderCore(sortOrder);
    }

    private void SetColumnSortOrderCore(ColumnSortOrder sortOrder)
    {
      if (this.Column.SortOrder == sortOrder)
        return;
      this.View.SortInfoCore.OnColumnHeaderClickAddSort(this.Column.FieldName, GridSortInfo.GetSortDirectionBySortOrder(sortOrder));
    }

    protected void CreateColumnChooserItems()
    {
      if (!this.View.IsColumnChooserVisible)
        this.CreateBarButtonItem("ItemColumnChooser", this.DataControl.AllowBandChooser ? GridControlStringId.MenuColumnShowColumnBandChooser : GridControlStringId.MenuColumnShowColumnChooser, true, (ImageSource) ImageHelper.GetImage("ItemColumnChooser"), this.View.Commands.ShowColumnChooser, (object) null);
      else
        this.CreateBarButtonItem("ItemColumnChooser", this.DataControl.AllowBandChooser ? GridControlStringId.MenuColumnHideColumnBandChooser : GridControlStringId.MenuColumnHideColumnChooser, true, (ImageSource) ImageHelper.GetImage("ItemColumnChooser"), this.View.Commands.HideColumnChooser, (object) null);
    }

    protected void CreateFilterControlItems()
    {
      bool showFilterSeparator = true;
      if (this.Column.IsFiltered && this.Column.ActualAllowColumnFiltering)
      {
        this.CreateBarButtonItem("ClearFilter", GridControlStringId.MenuColumnClearFilter, true, (ImageSource) ImageHelper.GetImage("ClearFilter"), this.Column.Commands.ClearColumnFilter, (object) null);
        showFilterSeparator = false;
      }
      if (!this.IsAllowFilterEditorForColumn(this.View, this.Column))
        return;
      this.CreateFilterEditorItem(showFilterSeparator);
    }

    protected void CreateFilterEditorItem(bool showFilterSeparator)
    {
      this.CreateBarButtonItem("FilterEditor", GridControlStringId.MenuColumnFilterEditor, showFilterSeparator, (ImageSource) ImageHelper.GetImage("FilterEditor"), this.View.Commands.ShowFilterEditor, (object) null).CommandParameter = (object) this.Column;
    }

    protected void CreateSearchPanelItems()
    {
      if (!this.View.IsRootView || this.View.ShowSearchPanelMode != ShowSearchPanelMode.Default)
        return;
      if (!this.View.ActualShowSearchPanel)
        this.CreateBarButtonItem("SearchPanel", GridControlStringId.MenuColumnShowSearchPanel, false, (ImageSource) ImageHelper.GetImage("SearchPanel"), this.View.Commands.ShowSearchPanel, (object) null);
      else
        this.CreateBarButtonItem("SearchPanel", GridControlStringId.MenuColumnHideSearchPanel, false, (ImageSource) ImageHelper.GetImage("SearchPanel"), this.View.Commands.HideSearchPanel, (object) null);
    }

    protected void CreateExpressionEditorItems()
    {
      if (!this.Column.AllowUnboundExpressionEditor)
        return;
      this.CreateBarButtonItem("UnboundExpressionEditor", GridControlStringId.MenuColumnUnboundExpressionEditor, true, (ImageSource) ImageHelper.GetImage("UnboundExpressionEditor"), this.View.Commands.ShowUnboundExpressionEditor, (object) null).CommandParameter = (object) this.Column;
    }

    protected virtual void CreateConditionalFormattingMenuItems()
    {
      if (this.HeaderPresenterType != HeaderPresenterType.Headers)
        return;
      ITableView tableView = this.View as ITableView;
      if (tableView == null || !tableView.TableViewBehavior.UseLightweightTemplatesHasFlag(UseLightweightTemplates.Row) || !this.Column.AllowConditionalFormattingMenu.GetValueOrDefault(tableView.AllowConditionalFormattingMenu))
        return;
      new ConditionalFormattingDialogDirector((IDialogContext) new DataControlDialogContext(this.Column), this.View.Commands as IConditionalFormattingCommands, (IConditionalFormattingDialogBuilder) this, (FrameworkElement) this.View)
      {
        AllowConditionalFormattingManager = tableView.AllowConditionalFormattingManager,
        IsServerMode = (this.DataControl.DataProviderBase.IsServerMode || this.DataControl.DataProviderBase.IsAsyncServerMode)
      }.CreateMenuItems((IDataColumnInfo) this.Column);
    }

    BarButtonItem IConditionalFormattingDialogBuilder.CreateBarButtonItem(BarItemLinkCollection links, string name, string content, bool beginGroup, ImageSource image, ICommand command, object commandParameter)
    {
      return this.CreateBarButtonItem(links, name, content, beginGroup, image, command, commandParameter);
    }

    BarSplitButtonItem IConditionalFormattingDialogBuilder.CreateBarSplitButtonItem(BarItemLinkCollection links, string name, string content, bool beginGroup, ImageSource image)
    {
      return this.CreateBarSplitButtonItem(links, name, content, beginGroup, image);
    }

    BarSubItem IConditionalFormattingDialogBuilder.CreateBarSubItem(string name, string content, bool beginGroup, ImageSource image, ICommand command)
    {
      return this.CreateBarSubItem(name, content, beginGroup, image, command);
    }

    BarSubItem IConditionalFormattingDialogBuilder.CreateBarSubItem(BarItemLinkCollection links, string name, string content, bool beginGroup, ImageSource image, ICommand command)
    {
      return this.CreateBarSubItem(links, name, content, beginGroup, image, command);
    }

    private bool IsAllowFilterEditorForColumn(DataViewBase view, ColumnBase column)
    {
      if (column == null)
        return this.IsAllowFilterEditor(view);
      if (this.IsAllowFilterEditor(view))
        return column.ActualAllowColumnFiltering;
      return false;
    }

    private bool IsAllowFilterEditor(DataViewBase view)
    {
      if (!view.AllowFilterEditor || !CriteriaToTreeProcessor.IsConvertibleOperator(this.DataControl.FilterCriteria))
        return false;
      foreach (ColumnBase columnBase in (IEnumerable) view.ColumnsCore)
      {
        if (columnBase.ActualAllowColumnFiltering)
          return true;
      }
      return false;
    }

    public override sealed bool Initialize(IInputElement value)
    {
      BaseGridHeader parentObject = LayoutHelper.FindParentObject<BaseGridHeader>(value as DependencyObject);
      this.HeaderPresenterType = ColumnBase.GetHeaderPresenterType((DependencyObject) parentObject);
      this.InitializeCore(parentObject);
      return base.Initialize(value);
    }

    protected virtual void InitializeCore(BaseGridHeader gridHeader)
    {
      this.BaseColumn = BaseGridHeader.GetGridColumn((DependencyObject) gridHeader);
    }

    protected override sealed void CreateItems()
    {
      if (this.BaseColumn == null)
        return;
      this.CreateItemsCore();
    }

    protected abstract void CreateItemsCore();
  }
}
