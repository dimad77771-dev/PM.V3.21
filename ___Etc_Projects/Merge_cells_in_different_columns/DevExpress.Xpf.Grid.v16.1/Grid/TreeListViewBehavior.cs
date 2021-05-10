// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListViewBehavior
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Grid.TreeList;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class TreeListViewBehavior : GridTableViewBehaviorBase
  {
    internal const string WrongStyleTargetTypeInHierarchicalDataTemplateModeError = "When the TreeList is in HierarchicalDataTemplate mode, optimized mode is disabled, thus the following Style target type is not valid.";

    protected TreeListView TreeListView
    {
      get
      {
        return (TreeListView) this.View;
      }
    }

    protected internal override Brush ActualAlternateRowBackground
    {
      get
      {
        return this.TreeListView.ActualAlternateRowBackground;
      }
    }

    protected internal override Style AutoFilterRowCellStyle
    {
      get
      {
        return this.TreeListView.AutoFilterRowCellStyle;
      }
    }

    protected override bool UseOptimizedTemplate
    {
      get
      {
        if (base.UseOptimizedTemplate)
          return this.TreeListView.TreeDerivationMode != TreeDerivationMode.HierarchicalDataTemplate;
        return false;
      }
    }

    internal bool AllowTreeIndentScrolling
    {
      get
      {
        if (this.TreeListView.AllowTreeIndentScrolling && !this.HasFixedLeftElements)
          return this.UseOptimizedTemplate;
        return false;
      }
    }

    public TreeListViewBehavior(TreeListView view)
      : base((DataViewBase) view)
    {
    }

    protected internal override bool IsAlternateRow(int rowHandle)
    {
      if (this.DataControl != null && this.TreeListView.AlternationCount > 0 && this.TreeListView.ActualAlternateRowBackground != null)
        return this.DataControl.GetRowVisibleIndexByHandleCore(rowHandle) % this.TreeListView.AlternationCount == this.TreeListView.AlternationCount - 1;
      return false;
    }

    protected internal override RowData CreateRowDataCore(DataTreeBuilder treeBuilder, bool updateDataOnly)
    {
      return (RowData) new TreeListRowData(treeBuilder);
    }

    protected internal override GridViewInfo CreateViewInfo()
    {
      return (GridViewInfo) new TreeListViewInfo(this.TreeListView);
    }

    protected internal override GridViewInfo CreatePrintViewInfo()
    {
      return this.CreatePrintViewInfo((BandsLayoutBase) null);
    }

    protected internal override GridViewInfo CreatePrintViewInfo(BandsLayoutBase bandsLayout)
    {
      return (GridViewInfo) new TreeListPrintViewInfo(this.TreeListView, bandsLayout);
    }

    internal override GridViewNavigationBase CreateRowNavigation()
    {
      return (GridViewNavigationBase) new TreeListViewRowNavigation(this.View);
    }

    internal override GridViewNavigationBase CreateCellNavigation()
    {
      return (GridViewNavigationBase) new TreeListViewCellNavigation(this.View);
    }

    internal override BestFitControlBase CreateBestFitControl(ColumnBase column)
    {
      return this.CreateElement((Func<FrameworkElement>) (() => (FrameworkElement) new TreeListLightweightBestFitControl(this.TreeListView, column)), (Func<FrameworkElement>) (() => (FrameworkElement) new BestFitControl(this.View, column)), UseLightweightTemplates.Row) as BestFitControlBase;
    }

    internal override BestFitControlBase CreateBestFitGroupControl(ColumnBase column)
    {
      return (BestFitControlBase) new BestFitGroupControl(this.View, column);
    }

    internal override void UpdateColumnsLayout()
    {
      base.UpdateColumnsLayout();
      this.TreeListView.CalcMinWidth();
    }

    internal override void UpdateActualDataRowTemplateSelector()
    {
      this.View.UpdateActualTemplateSelector(this.TableView.ActualDataRowTemplateSelectorPropertyKey, this.TreeListView.DataRowTemplateSelector, this.TreeListView.DataRowTemplate, (Func<DataTemplateSelector, DataTemplate, ActualTemplateSelectorWrapper>) ((s, t) => (ActualTemplateSelectorWrapper) new TreeListRowTemplateSelectorWrapper(s, t)));
      if (this.TreeListView.TreeDerivationMode != TreeDerivationMode.HierarchicalDataTemplate)
        return;
      this.TreeListView.DoRefresh(true);
    }

    internal bool CanBestFitColumn(object columnId)
    {
      GridColumnBase gridColumnBase = (GridColumnBase) this.View.GetColumnByCommandParameter(columnId);
      if (gridColumnBase != null)
        return this.CanBestFitColumn((ColumnBase) gridColumnBase);
      return false;
    }

    protected internal override double GetFixedNoneContentWidth(double totalWidth, int rowHandle)
    {
      if (this.TreeListView.FixedLeftVisibleColumns.Count != 0)
        return totalWidth;
      int num = 0;
      TreeListNode nodeByRowHandle = this.TreeListView.GetNodeByRowHandle(rowHandle);
      if (nodeByRowHandle != null)
        num = nodeByRowHandle.ActualLevel + this.TreeListView.ServiceIndentsCount;
      return totalWidth - this.TreeListView.RowIndent * (double) num - (this.TableView.ShowIndicator ? 0.0 : this.TableView.LeftDataAreaIndent);
    }

    internal override int GetTopRow(int pageVisibleTopRowIndex)
    {
      return this.View.DataProviderBase.GetControllerRow(pageVisibleTopRowIndex);
    }

    internal override CustomBestFitEventArgsBase RaiseCustomBestFit(ColumnBase column, BestFitMode bestFitMode)
    {
      TreeListCustomBestFitEventArgs bestFitEventArgs = new TreeListCustomBestFitEventArgs(column, bestFitMode);
      this.View.RaiseEvent((RoutedEventArgs) bestFitEventArgs);
      return (CustomBestFitEventArgsBase) bestFitEventArgs;
    }

    internal override GridColumnData GetGroupSummaryColumnData(int rowHandle, IBestFitColumn column)
    {
      return (GridColumnData) null;
    }

    internal override void SetGroupElementsForBestFit(FrameworkElement element, IBestFitColumn column, int rowHandle)
    {
    }

    internal override bool UseDataRowTemplate(RowData rowData)
    {
      if (this.TreeListView.TreeDerivationMode != TreeDerivationMode.HierarchicalDataTemplate)
        return base.UseDataRowTemplate(rowData);
      return true;
    }

    internal override void ValidateRowStyle(Style newStyle)
    {
      if (this.canChangeUseLightweightTemplates)
        return;
      base.ValidateStyle(newStyle, typeof (GridRowContent), typeof (RowControl), this.UseLightweightRows ? "The RowStyle target type is not supported in the grid's optimized mode. Either disable the optimized mode or change the target type to RowControl. To learn more about the grid's optimized mode, see http://go.devexpress.com/xpf-optimized-mode.aspx" : "The RowStyle target type is not supported in the grid's unoptimized mode.");
    }

    protected override Style ValidateStyle(Style style, Type normalTargetType, Type optimizedTargetType, string errorMessage)
    {
      if (!this.UseLightweightRows || this.TreeListView.TreeDerivationMode != TreeDerivationMode.HierarchicalDataTemplate)
        return base.ValidateStyle(style, normalTargetType, optimizedTargetType, errorMessage);
      if (style is DefaultStyle)
        return style;
      return base.ValidateStyle(style, normalTargetType, normalTargetType, "When the TreeList is in HierarchicalDataTemplate mode, optimized mode is disabled, thus the following Style target type is not valid.");
    }
  }
}
