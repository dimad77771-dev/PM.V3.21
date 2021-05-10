// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CellsControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class CellsControl : CellItemsControlBase
  {
    private static readonly ControlTemplate OrdinarPanelTemplate;
    private static readonly ControlTemplate BandsPanelTemplate;
    private static readonly ControlTemplate CellMergingPanelTemplate;
    private readonly Func<RowData, IList<GridColumnData>> getCellDataFunc;
    private readonly Func<BandsLayoutBase, IList<BandBase>> getBandsFunc;
    private Thickness panelOffset;

    internal RowControl RowControl { get; private set; }

    static CellsControl()
    {
      FrameworkElementFactory panelFactory1 = new FrameworkElementFactory(typeof (StackVisibleIndexPanel));
      panelFactory1.SetValue(OrderPanelBase.ArrangeAccordingToVisibleIndexProperty, (object) true);
      panelFactory1.SetValue(OrderPanelBase.OrientationProperty, (object) Orientation.Horizontal);
      CellsControl.OrdinarPanelTemplate = CellsControl.CreatePanelTemplate(panelFactory1);
      ItemsControlBase.ItemsPanelProperty.OverrideMetadata(typeof (CellsControl), new PropertyMetadata((object) CellsControl.OrdinarPanelTemplate));
      CellsControl.BandsPanelTemplate = CellsControl.CreatePanelTemplate(new FrameworkElementFactory(typeof (BandsCellsPanel)));
      FrameworkElementFactory panelFactory2 = new FrameworkElementFactory(typeof (CellMergingPanel));
      panelFactory2.SetValue(OrderPanelBase.ArrangeAccordingToVisibleIndexProperty, (object) true);
      panelFactory2.SetValue(OrderPanelBase.OrientationProperty, (object) Orientation.Horizontal);
      CellsControl.CellMergingPanelTemplate = CellsControl.CreatePanelTemplate(panelFactory2);
    }

    public CellsControl(RowControl rowControl, Func<RowData, IList<GridColumnData>> getCellDataFunc, Func<BandsLayoutBase, IList<BandBase>> getBandsFunc)
    {
      this.RowControl = rowControl;
      this.getCellDataFunc = getCellDataFunc;
      this.getBandsFunc = getBandsFunc;
    }

    private static ControlTemplate CreatePanelTemplate(FrameworkElementFactory panelFactory)
    {
      ControlTemplate controlTemplate = new ControlTemplate(typeof (ItemsControlBase));
      controlTemplate.VisualTree = panelFactory;
      controlTemplate.Seal();
      return controlTemplate;
    }

    protected override Size ArrangeOverride(Size arrangeBounds)
    {
      Size size = base.ArrangeOverride(arrangeBounds);
      if (this.View.ActualAllowCellMerge)
      {
        double height = this.View.RootDataPresenter.LastConstraint.Height;
        double width = TableViewProperties.GetFixedAreaStyle((DependencyObject) this) == FixedStyle.None ? this.RowControl.rowData.FixedNoneContentWidth : size.Width;
        this.Clip = (Geometry) new RectangleGeometry(new Rect(0.0, -height, width, height + size.Height));
      }
      else
        this.Clip = (Geometry) new RectangleGeometry(new Rect(0.0, 0.0, size.Width, size.Height));
      return size;
    }

    internal void UpdateItemsSource()
    {
      this.ItemsSource = (IEnumerable) this.getCellDataFunc(this.RowControl.rowData);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.UpdatePanelOffset();
      this.UpdateBands();
    }

    internal void UpdateBands()
    {
      BandsCellsPanel bandsCellsPanel = this.Panel as BandsCellsPanel;
      if (bandsCellsPanel == null || !this.RowControl.IsBandedLayout)
        return;
      bandsCellsPanel.Bands = this.getBandsFunc(this.RowControl.BandsLayout);
    }

    protected override FrameworkElement CreateChildCore(GridCellData cellData)
    {
      LightweightCellEditor lightweightCellEditor = new LightweightCellEditor(this);
      lightweightCellEditor.RowData = cellData.RowData;
      return (FrameworkElement) lightweightCellEditor;
    }

    protected override void ValidateElementCore(FrameworkElement element, GridCellData cellData)
    {
      LightweightCellEditor lightweightCellEditor = (LightweightCellEditor) element;
      lightweightCellEditor.CellData = (EditGridCellData) cellData;
      lightweightCellEditor.Column = cellData.Column;
      cellData.OnEditorContentUpdated();
      this.UpdateElementWidth(element, cellData);
      cellData.SyncLeftMargin(element);
      ColumnBase.SetNavigationIndex((DependencyObject) element, BaseColumn.GetVisibleIndex((DependencyObject) cellData.Column));
      lightweightCellEditor.SetBorderState(cellData, this.RowControl.rowData.SelectionState);
    }

    protected virtual void UpdateElementWidth(FrameworkElement element, GridCellData cellData)
    {
      element.Width = cellData.GetActualCellWidth();
    }

    internal void SetPanelOffset(Thickness panelOffset)
    {
      if (!(this.panelOffset != panelOffset))
        return;
      this.panelOffset = panelOffset;
      this.UpdatePanelOffset();
    }

    private void UpdatePanelOffset()
    {
      if (this.Panel == null)
        return;
      this.Panel.Margin = this.panelOffset;
    }

    protected internal virtual void UpdatePanel()
    {
      this.Template = this.GetTemplate();
    }

    internal void InvalidatePanel()
    {
      if (this.Panel == null)
        return;
      this.Panel.InvalidateArrange();
    }

    private ControlTemplate GetTemplate()
    {
      if (this.RowControl.IsBandedLayout)
        return CellsControl.BandsPanelTemplate;
      if (!this.View.ActualAllowCellMerge)
        return CellsControl.OrdinarPanelTemplate;
      return CellsControl.CellMergingPanelTemplate;
    }
  }
}
