// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridTableViewBehaviorBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using System;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public abstract class GridTableViewBehaviorBase : TableViewBehavior
  {
    private const string HelpLinkString = "http://go.devexpress.com/xpf-optimized-mode.aspx";
    private const string LearnMoreMessage = " To learn more about the grid's optimized mode, see http://go.devexpress.com/xpf-optimized-mode.aspx";
    internal const string WrongCellStyleTargetTypeError = "The CellStyle target type is not supported in the grid's optimized mode. Either disable the optimized mode or change the target type to LightweightCellEditor. To learn more about the grid's optimized mode, see http://go.devexpress.com/xpf-optimized-mode.aspx";
    internal const string WrongCellStyleTargetTypeErrorUnoptimized = "The CellStyle target type is not supported in the grid's unoptimized mode.";
    internal const string WrongRowStyleTargetTypeError = "The RowStyle target type is not supported in the grid's optimized mode. Either disable the optimized mode or change the target type to RowControl. To learn more about the grid's optimized mode, see http://go.devexpress.com/xpf-optimized-mode.aspx";
    internal const string WrongRowStyleTargetTypeErrorUnoptimized = "The RowStyle target type is not supported in the grid's unoptimized mode.";
    internal const string RowDetailsUnoptimizedModeError = "RowDetailsTemplate and RowDetailsTemplateSelector is only supported in the grid's optimized mode.";
    internal const string RowDecorationTemplateInOptimizedModeError = "RowDecorationTemplate is not supported in the grid's optimized mode. To learn more about the grid's optimized mode, see http://go.devexpress.com/xpf-optimized-mode.aspx";
    internal const string GroupValueContentStyleInOptimizedModeError = "GroupValueContentStyle is not supported in the grid's optimized mode. To learn more about the grid's optimized mode, see http://go.devexpress.com/xpf-optimized-mode.aspx";
    internal const string GroupSummaryContentStyleInOptimizedModeError = "GroupSummaryContentStyle  is not supported in the grid's optimized mode. To learn more about the grid's optimized mode, see http://go.devexpress.com/xpf-optimized-mode.aspx";
    internal const string DefaultDataRowTemplateInOptimizedModeError = "CellItemsControl (that is typically used in DefaultDataRowTemplate) is not supported in the grid's optimized mode. To learn more about the grid's optimized mode, see http://go.devexpress.com/xpf-optimized-mode.aspx";

    protected bool UseLightweightRows
    {
      get
      {
        return this.UseLightweightTemplatesHasFlag(UseLightweightTemplates.Row);
      }
    }

    protected virtual bool UseOptimizedTemplate
    {
      get
      {
        return this.UseLightweightTemplatesHasFlag(UseLightweightTemplates.Row);
      }
    }

    internal override bool UseMouseUpFocusedEditorShowModeStrategy
    {
      get
      {
        if (!this.View.IsMultiCellSelection && !this.IsDataViewDragDropManagerAttached())
          return base.UseMouseUpFocusedEditorShowModeStrategy;
        return true;
      }
    }

    protected GridTableViewBehaviorBase(DataViewBase view)
      : base(view)
    {
    }

    internal override sealed GridColumnHeaderBase CreateGridColumnHeader()
    {
      return (GridColumnHeaderBase) new GridColumnHeader();
    }

    internal override BestFitControlBase CreateBestFitControl(ColumnBase column)
    {
      return this.CreateElement((Func<FrameworkElement>) (() => (FrameworkElement) new LightweightBestFitControl(this.View, column)), (Func<FrameworkElement>) (() => (FrameworkElement) new BestFitControl(this.View, column)), UseLightweightTemplates.Row) as BestFitControlBase;
    }

    internal override BestFitControlBase CreateBestFitGroupControl(ColumnBase column)
    {
      return this.CreateElement((Func<FrameworkElement>) (() => (FrameworkElement) new LightweightBestFitGroupRowControl(this.View, column)), (Func<FrameworkElement>) (() => (FrameworkElement) new BestFitGroupControl(this.View, column)), UseLightweightTemplates.GroupRow) as BestFitControlBase;
    }

    internal override sealed FrameworkElement CreateGridTotalSummaryControl()
    {
      return (FrameworkElement) new GridTotalSummary();
    }

    internal override FrameworkElement CreateGroupFooterSummaryControl()
    {
      return (FrameworkElement) new GroupFooterSummaryControl();
    }

    protected internal override IndicatorState GetIndicatorState(RowData rowData)
    {
      if (rowData is GroupSummaryRowData)
        return IndicatorState.None;
      return base.GetIndicatorState(rowData);
    }

    internal override DetailHeaderControlBase CreateDetailHeaderElement()
    {
      return (DetailHeaderControlBase) new DetailHeaderControl();
    }

    internal override DetailHeaderControlBase CreateDetailContentElement()
    {
      return (DetailHeaderControlBase) new DetailContentControl();
    }

    internal override DetailTabHeadersControlBase CreateDetailTabHeadersElement()
    {
      return (DetailTabHeadersControlBase) new DetailTabHeadersControl();
    }

    internal override DetailRowControlBase CreateDetailColumnHeadersElement()
    {
      return (DetailRowControlBase) new DetailColumnHeadersControl();
    }

    internal override DetailRowControlBase CreateDetailTotalSummaryElement()
    {
      return (DetailRowControlBase) new DetailTotalSummaryControl();
    }

    internal override DetailRowControlBase CreateDetailFixedTotalSummaryElement()
    {
      return (DetailRowControlBase) new DetailFixedTotalSummaryControl();
    }

    internal override DetailRowControlBase CreateDetailNewItemRowElement()
    {
      return (DetailRowControlBase) new DetailNewItemRowControl();
    }

    internal override Style GetActualCellStyle(ColumnBase column)
    {
      return this.ValidateStyle(base.GetActualCellStyle(column), typeof (GridCellContentPresenter), typeof (LightweightCellEditor), this.UseLightweightRows ? "The CellStyle target type is not supported in the grid's optimized mode. Either disable the optimized mode or change the target type to LightweightCellEditor. To learn more about the grid's optimized mode, see http://go.devexpress.com/xpf-optimized-mode.aspx" : "The CellStyle target type is not supported in the grid's unoptimized mode.");
    }

    internal override void ValidateRowStyle(Style newStyle)
    {
      if (this.canChangeUseLightweightTemplates)
        return;
      this.ValidateStyle(newStyle, typeof (GridRowContent), typeof (RowControl), this.UseLightweightRows ? "The RowStyle target type is not supported in the grid's optimized mode. Either disable the optimized mode or change the target type to RowControl. To learn more about the grid's optimized mode, see http://go.devexpress.com/xpf-optimized-mode.aspx" : "The RowStyle target type is not supported in the grid's unoptimized mode.");
    }

    protected virtual Style ValidateStyle(Style style, Type normalTargetType, Type optimizedTargetType, string errorMessage)
    {
      if (style == null)
        return (Style) null;
      if (style is DefaultStyle)
      {
        if (this.UseLightweightRows)
          return (Style) null;
      }
      else if (!this.View.IsDesignTime)
      {
        Type c = this.UseLightweightRows ? optimizedTargetType : normalTargetType;
        if (this.View.IsInitialized && style.TargetType != (Type) null && (!style.TargetType.IsAssignableFrom(c) && !DataViewBase.DisableOptimizedModeVerification))
          throw new InvalidOperationException(errorMessage);
      }
      return style;
    }

    protected override void UpdateActualRowDetailsTemplateSelector()
    {
      base.UpdateActualRowDetailsTemplateSelector();
      if (!this.UseLightweightTemplatesHasFlag(UseLightweightTemplates.Row))
        throw new InvalidOperationException("RowDetailsTemplate and RowDetailsTemplateSelector is only supported in the grid's optimized mode.");
    }

    protected internal override void OnRowDecorationTemplateChanged()
    {
      base.OnRowDecorationTemplateChanged();
      if (!this.View.IsDesignTime && this.TableView.RowDecorationTemplate != null && (!(this.TableView.RowDecorationTemplate is DefaultControlTemplate) && this.UseLightweightTemplatesHasFlag(UseLightweightTemplates.Row)) && !DataViewBase.DisableOptimizedModeVerification)
        throw new InvalidOperationException("RowDecorationTemplate is not supported in the grid's optimized mode. To learn more about the grid's optimized mode, see http://go.devexpress.com/xpf-optimized-mode.aspx");
    }

    protected internal override void OnCellItemsControlLoaded()
    {
      base.OnCellItemsControlLoaded();
      if (!this.View.IsDesignTime && this.UseOptimizedTemplate && !DataViewBase.DisableOptimizedModeVerification)
        throw new InvalidOperationException("CellItemsControl (that is typically used in DefaultDataRowTemplate) is not supported in the grid's optimized mode. To learn more about the grid's optimized mode, see http://go.devexpress.com/xpf-optimized-mode.aspx");
    }

    private bool IsDataViewDragDropManagerAttached()
    {
      if (this.View.DataControl != null)
        return DataViewDragDropManagerHelper.GetIsAttached((DependencyObject) this.View.DataControl);
      return false;
    }
  }
}
