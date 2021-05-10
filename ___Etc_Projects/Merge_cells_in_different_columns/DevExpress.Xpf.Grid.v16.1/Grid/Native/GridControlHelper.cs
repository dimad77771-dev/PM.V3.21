// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.GridControlHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Data.Native;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid.Native
{
  public static class GridControlHelper
  {
    public static void SetVerticalOffsetForce(GridViewBase view, double value)
    {
      view.DataPresenter.SetVerticalOffsetForce(value);
    }

    public static GridRow GetRow(GridViewBase view, int rowHandle)
    {
      return (GridRow) view.GetRowElementByRowHandle(rowHandle);
    }

    public static IScrollInfo GetScrollInfo(GridViewBase view)
    {
      return (IScrollInfo) view.DataPresenter;
    }

    public static DataPresenterBase GetDataPresenter(DataViewBase view)
    {
      return view.DataPresenter;
    }

    public static void DoBandMoveAction(DataControlBase dataControl, Action action)
    {
      dataControl.BandsLayoutCore.DoMoveAction(action);
    }

    public static BandsLayoutBase GetBandsLayout(DataControlBase control)
    {
      return control.BandsLayoutCore;
    }

    public static IBandsOwner GetOwner(BandBase band)
    {
      return band.Owner;
    }

    public static object GetDesignTimeSource(DataControlBase dataControl)
    {
      object itemsSource = dataControl.ItemsSource;
      if (GridControlHelper.CanUseItemsSource(itemsSource))
        return itemsSource;
      if (GridControlHelper.IsDesignTimeDataSource(itemsSource))
        dataControl.DesignTimeShowSampleData = false;
      if (dataControl != null && dataControl.DesignTimeShowSampleData)
        return (object) new GridDesignTimeDataSource(dataControl.DesignTimeDataObjectType, dataControl.DesignTimeDataSourceRowCount, dataControl.DesignTimeUseDistinctSampleValues, itemsSource, GridControlHelper.GetDesignTimePropertyInfo(dataControl));
      return itemsSource;
    }

    private static bool CanUseItemsSource(object itemsSource)
    {
      if (!(itemsSource is IEnumerable))
        return itemsSource is Array;
      return true;
    }

    private static bool IsDesignTimeDataSource(object value)
    {
      if (value == null)
        return false;
      Type type = value.GetType();
      if (type.FullName == "MS.Internal.Data.CollectionViewProxy")
        return type.GetProperty("ProxiedView").GetValue(value, (object[]) null) is IDesignTimeDataSource;
      return value is IDesignTimeDataSource;
    }

    public static void FillBandsColumns(DataControlBase control)
    {
      if (control.BandsLayoutCore == null)
        return;
      control.BandsLayoutCore.FillColumns();
    }

    public static void OnColumnHeaderClick(GridViewBase view, GridColumn column)
    {
      view.OnColumnHeaderClick((ColumnBase) column);
    }

    public static void InvalidateDesignTimeDataSource(DataControlBase dataControl)
    {
      dataControl.InvalidateDesignTimeDataSource();
    }

    public static void ChangeGroupExpanded(GridViewBase view, int visibleIndex)
    {
      view.Grid.ChangeGroupExpanded(visibleIndex);
    }

    public static GridViewNavigationBase GetNavigation(GridViewBase view)
    {
      return view.Navigation;
    }

    public static DataViewBase GetView(DataControlBase dataControl)
    {
      return dataControl.DataView;
    }

    public static IColumnCollection GetColumns(DataControlBase dataControl)
    {
      return dataControl.ColumnsCore;
    }

    public static IBandColumnsCollection GetColumns(BandBase bandBase)
    {
      return bandBase.ColumnsCore;
    }

    public static DataProviderBase GetDataProvider(DataControlBase dataControl)
    {
      return dataControl.DataProviderBase;
    }

    public static NodeContainer GetRootNode(GridViewBase view)
    {
      return (NodeContainer) view.RootNodeContainer;
    }

    public static CellEditorBase GetCellPresenterEditor(CellContentPresenter presenter)
    {
      return presenter.Editor;
    }

    public static DragDropElementHelper GetColumnHeaderDragDropHelper(BaseGridHeader header)
    {
      return header.DragDropHelper;
    }

    public static ContentPresenter GetDragPreviewElement(HeaderDragElementBase headerDragElement)
    {
      return headerDragElement.DragPreviewElement;
    }

    public static FrameworkElement GetColumnHeaderContent(GridColumnHeader header)
    {
      return header.HeaderContent;
    }

    public static ReadOnlyGridSortInfoCollection GetActualSortInfo(GridControl gridControl)
    {
      return gridControl.ActualSortInfo;
    }

    public static void SetDesignTimeEventsListener(DataControlBase dataControl, IDesignTimeAdornerBase listener)
    {
      dataControl.DesignTimeAdorner = listener;
    }

    public static void BeginUpdateColumnsLayout(DataViewBase view)
    {
      view.BeginUpdateColumnsLayout();
    }

    public static void EndUpdateColumnsLayout(DataViewBase view, bool calcLayout)
    {
      view.EndUpdateColumnsLayout(calcLayout);
    }

    public static IEnumerable<string> GetAllColumnNames(DataControlBase dataControl)
    {
      return GridControlHelper.GetDataColumnInfo(dataControl).Select<DataColumnInfo, string>((Func<DataColumnInfo, string>) (x => x.Name));
    }

    public static IEnumerable<DataColumnInfo> GetDataColumnInfo(DataControlBase dataControl)
    {
      dataControl.InvalidateDesignTimeDataSource();
      if (dataControl.DataProviderBase != null)
        return dataControl.DataProviderBase.Columns.Cast<DataColumnInfo>().Where<DataColumnInfo>((Func<DataColumnInfo, bool>) (x => x.Visible));
      return (IEnumerable<DataColumnInfo>) new List<DataColumnInfo>();
    }

    public static void ClearAutoGeneratedBandsAndColumns(DataControlBase dataControl)
    {
      dataControl.ClearBands(dataControl.BandsCore);
      dataControl.ClearAutoGeneratedColumns();
    }

    public static object GetActualItemsSource(DataControlBase dataControl)
    {
      if (dataControl == null)
        return (object) null;
      return dataControl.ActualItemsSource;
    }

    public static void ApplyColumnAttributes(IModelItem dataControl, IModelItem column)
    {
      ((DataControlBase) dataControl.GetCurrentValue()).ApplyDesignTimeColumnAttributes(dataControl, column);
    }

    public static void PopulateColumns(IModelItem dataControl, bool expandProperties)
    {
      ((DataControlBase) dataControl.GetCurrentValue()).PopulateDesignTimeColumns(dataControl, expandProperties);
    }

    public static IDesignTimeAdornerBase GetDesignTimeAdorner(DataViewBase view)
    {
      return view.DesignTimeAdorner;
    }

    public static bool GetIsDesignTimeAdornerPanelLeftAligned(DataViewBase view)
    {
      return view.IsDesignTimeAdornerPanelLeftAligned;
    }

    public static FloatingWindowContainer GetFloatingWindowContainer(ColumnHeaderDragElement headerDragElement)
    {
      return headerDragElement.WindowContainer;
    }

    public static IEnumerable<DesignTimePropertyInfo> GetDesignTimePropertyInfo(DataControlBase dataControl)
    {
      foreach (ColumnBase columnBase in (IEnumerable) dataControl.ColumnsCore)
        yield return new DesignTimePropertyInfo(columnBase.FieldName, typeof (string), false);
    }

    public static IList<ColumnBase> GetVisibleColumns(DataViewBase view)
    {
      return view.VisibleColumnsCore;
    }

    public static IList<BandBase> GetVisibleBands(DataControlBase dataControl)
    {
      return (IList<BandBase>) dataControl.BandsLayoutCore.VisibleBands;
    }

    public static IBandsOwner GetBandOwner(BandBase band)
    {
      return band.Owner;
    }

    public static GridColumnHeader[] GetColumnHeaderElements(DataControlBase dataControl, ColumnBase column)
    {
      if (!column.Visible)
        return (GridColumnHeader[]) null;
      List<GridColumnHeader> gridColumnHeaderList = new List<GridColumnHeader>();
      bool flag1 = column is GridColumn && ((GridColumn) column).IsGrouped;
      if (flag1)
      {
        GridColumnHeader elementFromPanel = GridControlHelper.GetHeaderElementFromPanel(dataControl, column, ((GridViewBase) dataControl.DataView).GroupPanel);
        if (elementFromPanel != null)
          gridColumnHeaderList.Add(elementFromPanel);
      }
      bool flag2 = dataControl.DataView is GridViewBase && ((GridViewBase) dataControl.DataView).ShowGroupedColumns;
      if (!flag1 || flag2)
      {
        GridColumnHeader elementFromPanel = GridControlHelper.GetHeaderElementFromPanel(dataControl, column, dataControl.DataView.HeadersPanel);
        if (elementFromPanel != null)
          gridColumnHeaderList.Add(elementFromPanel);
      }
      if (gridColumnHeaderList.Count <= 0)
        return (GridColumnHeader[]) null;
      return gridColumnHeaderList.ToArray();
    }

    public static BandHeaderControl GetBandHeaderElement(DataControlBase dataControl, BandBase band)
    {
      if (dataControl == null || !band.Visible)
        return (BandHeaderControl) null;
      foreach (DependencyObject dependencyObject in new VisualTreeEnumerable((DependencyObject) dataControl.BandsLayoutCore.GetBandsContainerControl()))
      {
        BandHeaderControl element = dependencyObject as BandHeaderControl;
        if (element != null && element.DataContext == band && element.IsVisible())
          return element;
      }
      return (BandHeaderControl) null;
    }

    private static GridColumnHeader GetHeaderElementFromPanel(DataControlBase dataControl, ColumnBase column, FrameworkElement rootPanel)
    {
      if (rootPanel == null)
        return (GridColumnHeader) null;
      foreach (DependencyObject dependencyObject in new VisualTreeEnumerable((DependencyObject) rootPanel))
      {
        GridColumnHeader element = dependencyObject as GridColumnHeader;
        if (element != null && element.DataContext == column && element.IsVisible())
          return element;
      }
      return (GridColumnHeader) null;
    }

    public static bool GetIsDirty(RowData rowData)
    {
      return rowData.IsDirty;
    }

    public static List<RowData> GetRowsToUpdate(GridViewBase view)
    {
      return view.MasterRootRowsContainer.RowsToUpdate;
    }

    public static DataNodeContainer GetRootNodeContainer(GridViewBase view)
    {
      return (DataNodeContainer) view.RootNodeContainer;
    }

    public static FrameworkElement GetRowElement(RowData rowData)
    {
      return rowData.RowElement;
    }

    public static RowData GetRowData(GridViewBase view, int rowHandle)
    {
      return view.GetRowData(rowHandle);
    }

    public static string GetOwnerPredefinedFormatsPropertyName(FormatConditionBase formatCondition)
    {
      return formatCondition.OwnerPredefinedFormatsPropertyName;
    }

    public static void ClearFormatProperty(FormatConditionBase formatCondition)
    {
      BindingOperations.ClearBinding((DependencyObject) formatCondition, formatCondition.FormatPropertyForBinding);
    }

    public static bool SupportStarColumns(DataViewBase view)
    {
      ITableView tableView = view as ITableView;
      if (tableView == null)
        return false;
      return tableView.TableViewBehavior.ColumnsLayoutCalculator.SupportStarColumns;
    }
  }
}
