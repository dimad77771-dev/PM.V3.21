// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.BandsColumnHeadersPanel
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class BandsColumnHeadersPanel : Panel
  {
    public static readonly DependencyProperty BandsProperty = DependencyPropertyManager.Register("Bands", typeof (IList<BandBase>), typeof (BandsColumnHeadersPanel), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((UIElement) d).InvalidateMeasure())));

    public IList<BandBase> Bands
    {
      get
      {
        return (IList<BandBase>) this.GetValue(BandsColumnHeadersPanel.BandsProperty);
      }
      set
      {
        this.SetValue(BandsColumnHeadersPanel.BandsProperty, (object) value);
      }
    }

    protected virtual bool AllowHeaderMerge
    {
      get
      {
        return true;
      }
    }

    internal ColumnsRowDataBase RowData
    {
      get
      {
        return this.DataContext as ColumnsRowDataBase;
      }
    }

    private BandsColumnHeadersPanel.BandLayoutStrategyBase GetLayoutStrategy(BandBase band)
    {
      if (band.ColumnDefinitions.Count > 0 || band.RowDefinitions.Count > 0)
        return BandsColumnHeadersPanel.GridBandLayoutStrategy.Instance;
      return BandsColumnHeadersPanel.StackBandLayoutStrategy.Instance;
    }

    protected override Size MeasureOverride(Size availableSize)
    {
      double width = 0.0;
      double height = 0.0;
      double x = 0.0;
      BandsColumnHeadersPanel.IMergeHeightProvider mergeHeightProvider = this.CreateMergeHeightProvider();
      BandsLayoutBase.ForeachVisibleBand((IEnumerable) this.Bands, (Action<BandBase>) (band =>
      {
        Size size = this.GetLayoutStrategy(band).MeasureElements(band, availableSize, x, this, mergeHeightProvider);
        if (band.VisibleBands.Count == 0)
          x += this.GetBandWidth(band);
        width += size.Width;
        height = Math.Max(height, size.Height);
      }));
      foreach (UIElement child in this.Children)
      {
        if (OrderPanelBase.GetVisibleIndex((DependencyObject) child) < 0)
        {
          child.Visibility = Visibility.Collapsed;
          child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        }
      }
      return new Size(width, height);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      double width = 0.0;
      BandsColumnHeadersPanel.IMergeHeightProvider mergeHeightProvider = this.CreateMergeHeightProvider();
      BandsLayoutBase.ForeachVisibleBand((IEnumerable) this.Bands, (Action<BandBase>) (band => width = this.GetLayoutStrategy(band).ArrangeElements(band, finalSize, width, this, mergeHeightProvider)));
      return finalSize;
    }

    protected internal virtual ColumnBase GetColumn(FrameworkElement element)
    {
      return element.DataContext as ColumnBase;
    }

    protected internal virtual double GetColumnWidth(ColumnBase column)
    {
      return column.ActualHeaderWidth;
    }

    protected internal virtual double GetBandWidth(BandBase band)
    {
      return band.ActualHeaderWidth;
    }

    protected virtual void ArrangeChild(UIElement child, double x, double y, double width, double height)
    {
      child.Arrange(new Rect(x, y, width, height));
    }

    protected virtual void MeasureChild(UIElement child, double availableWidth, double availableHeight, double x)
    {
      child.Measure(new Size(availableWidth, availableHeight));
    }

    private BandsPanel GetBandsPanel()
    {
      BandsLayoutBase bandsLayoutBase = this.RowData.With<ColumnsRowDataBase, DataViewBase>((Func<ColumnsRowDataBase, DataViewBase>) (r => r.View)).With<DataViewBase, DataControlBase>((Func<DataViewBase, DataControlBase>) (v => v.DataControl)).With<DataControlBase, BandsLayoutBase>((Func<DataControlBase, BandsLayoutBase>) (dc => dc.BandsLayoutCore));
      if (bandsLayoutBase == null)
        return (BandsPanel) null;
      BandsContainerControl containerControl = bandsLayoutBase.GetBandsContainerControl() as BandsContainerControl;
      if (containerControl == null)
        return (BandsPanel) null;
      FixedStyle fixedStyle = FixedStyle.None;
      BandBase band = this.Bands.FirstOrDefault<BandBase>();
      if (band != null)
      {
        BandBase rootBand = bandsLayoutBase.GetRootBand(band);
        if (rootBand != null)
          fixedStyle = rootBand.Fixed;
      }
      return containerControl.GetBandsPanel(fixedStyle);
    }

    private BandsColumnHeadersPanel.IMergeHeightProvider CreateMergeHeightProvider()
    {
      if (!this.AllowHeaderMerge)
        return (BandsColumnHeadersPanel.IMergeHeightProvider) new BandsColumnHeadersPanel.EmptyMergeHeightProvider();
      return (BandsColumnHeadersPanel.IMergeHeightProvider) new BandsColumnHeadersPanel.BandMergeHeightProvider(new Func<BandsPanel>(this.GetBandsPanel));
    }

    [Browsable(false)]
    public bool ShouldSerializeBands(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    protected override Geometry GetLayoutClip(Size layoutSlotSize)
    {
      return this.GetLayoutClipInternal(layoutSlotSize);
    }

    internal Geometry GetLayoutClipInternal(Size layoutSlotSize)
    {
      return (Geometry) null;
    }

    private abstract class BandLayoutStrategyBase
    {
      public abstract Size MeasureElements(BandBase band, Size availableSize, double width, BandsColumnHeadersPanel panel, BandsColumnHeadersPanel.IMergeHeightProvider mergeHeightProvider);

      public abstract double ArrangeElements(BandBase band, Size finalSize, double width, BandsColumnHeadersPanel panel, BandsColumnHeadersPanel.IMergeHeightProvider mergeHeightProvider);
    }

    private class StackBandLayoutStrategy : BandsColumnHeadersPanel.BandLayoutStrategyBase
    {
      public static readonly BandsColumnHeadersPanel.BandLayoutStrategyBase Instance = (BandsColumnHeadersPanel.BandLayoutStrategyBase) new BandsColumnHeadersPanel.StackBandLayoutStrategy();

      private StackBandLayoutStrategy()
      {
      }

      public override Size MeasureElements(BandBase band, Size availableSize, double width, BandsColumnHeadersPanel panel, BandsColumnHeadersPanel.IMergeHeightProvider mergeHeightProvider)
      {
        double height = 0.0;
        double val1_1 = 0.0;
        double mergeHeight = mergeHeightProvider.GetMergeHeight(band);
        Dictionary<ColumnBase, FrameworkElement> bandElements = this.GetBandElements(band, panel);
        if (band.ActualRows.Count != 0)
        {
          for (int index = 0; index < band.ActualRows.Count; ++index)
          {
            BandRow bandRow = band.ActualRows[index];
            double val1_2 = 0.0;
            double val2 = 0.0;
            double x = width;
            bool flag = index == band.ActualRows.Count - 1;
            foreach (ColumnBase column in bandRow.Columns)
            {
              if (bandElements.ContainsKey(column))
              {
                FrameworkElement frameworkElement = bandElements[column];
                frameworkElement.Visibility = Visibility.Visible;
                panel.MeasureChild((UIElement) frameworkElement, panel.GetColumnWidth(column) + column.GetHorizontalCorrection(), Math.Max(0.0, availableSize.Height - height + (flag ? mergeHeight : 0.0)), x);
                val1_2 = Math.Max(val1_2, frameworkElement.DesiredSize.Height);
                val2 += panel.GetColumnWidth(column);
              }
              x += panel.GetColumnWidth(column);
            }
            height += val1_2;
            val1_1 = Math.Max(val1_1, val2);
          }
        }
        else
          val1_1 = panel.GetBandWidth(band);
        return new Size(band.VisibleBands.Count == 0 ? val1_1 : 0.0, height);
      }

      public override double ArrangeElements(BandBase band, Size finalSize, double width, BandsColumnHeadersPanel panel, BandsColumnHeadersPanel.IMergeHeightProvider mergeHeightProvider)
      {
        Dictionary<ColumnBase, FrameworkElement> bandElements = this.GetBandElements(band, panel);
        double num = 0.0;
        double val1 = 0.0;
        double mergeHeight = mergeHeightProvider.GetMergeHeight(band);
        if (band.ActualRows.Count != 0)
        {
          for (int index1 = 0; index1 < band.ActualRows.Count; ++index1)
          {
            double val2_1 = width;
            double val2_2 = 0.0;
            bool flag = index1 == band.ActualRows.Count - 1;
            if (index1 == band.ActualRows.Count - 1)
            {
              val2_2 = finalSize.Height - num;
            }
            else
            {
              foreach (ColumnBase column in band.ActualRows[index1].Columns)
              {
                if (bandElements.ContainsKey(column))
                  val2_2 = Math.Max(bandElements[column].DesiredSize.Height, val2_2);
              }
            }
            for (int index2 = 0; index2 < band.ActualRows[index1].Columns.Count; ++index2)
            {
              ColumnBase index3 = band.ActualRows[index1].Columns[index2];
              if (bandElements.ContainsKey(index3))
              {
                FrameworkElement frameworkElement = bandElements[index3];
                double horizontalCorrection = index3.GetHorizontalCorrection();
                panel.ArrangeChild((UIElement) frameworkElement, val2_1 - horizontalCorrection, num - mergeHeight, panel.GetColumnWidth(index3) + horizontalCorrection, val2_2 + (flag ? mergeHeight : 0.0));
              }
              val2_1 += panel.GetColumnWidth(index3);
            }
            num += val2_2;
            val1 = Math.Max(val1, val2_1);
          }
        }
        else
          val1 = panel.GetBandWidth(band) + width;
        if (band.VisibleBands.Count != 0)
          return width;
        return val1;
      }

      private Dictionary<ColumnBase, FrameworkElement> GetBandElements(BandBase band, BandsColumnHeadersPanel panel)
      {
        Dictionary<ColumnBase, FrameworkElement> dictionary = new Dictionary<ColumnBase, FrameworkElement>();
        foreach (FrameworkElement child in panel.Children)
        {
          ColumnBase column = panel.GetColumn(child);
          if (column != null)
            dictionary[column] = child;
        }
        return dictionary;
      }
    }

    private class GridBandLayoutStrategy : BandsColumnHeadersPanel.BandLayoutStrategyBase
    {
      public static readonly BandsColumnHeadersPanel.BandLayoutStrategyBase Instance = (BandsColumnHeadersPanel.BandLayoutStrategyBase) new BandsColumnHeadersPanel.GridBandLayoutStrategy();

      private GridBandLayoutStrategy()
      {
      }

      public override Size MeasureElements(BandBase band, Size availableSize, double width, BandsColumnHeadersPanel panel, BandsColumnHeadersPanel.IMergeHeightProvider mergeHeightProvider)
      {
        double num = 0.0;
        double width1 = 0.0;
        Dictionary<GridColumn, UIElement> bandElements = this.GetBandElements(band, panel);
        foreach (GridColumn key in bandElements.Keys)
        {
          UIElement child = bandElements[key];
          panel.MeasureChild(child, double.PositiveInfinity, double.PositiveInfinity, width1 + width);
          num = Math.Max(num, child.DesiredSize.Height);
          width1 += child.DesiredSize.Width;
        }
        if (band.RowDefinitions.Count != 0)
        {
          num = 0.0;
          foreach (BandRowDefinition rowDefinition in (Collection<BandRowDefinition>) band.RowDefinitions)
            num += rowDefinition.Height.Value;
        }
        if (band.ColumnDefinitions.Count != 0)
        {
          width1 = 0.0;
          foreach (BandColumnDefinition columnDefinition in (Collection<BandColumnDefinition>) band.ColumnDefinitions)
            width1 += columnDefinition.Width.Value;
        }
        return new Size(width1, num);
      }

      public override double ArrangeElements(BandBase band, Size finalSize, double width, BandsColumnHeadersPanel panel, BandsColumnHeadersPanel.IMergeHeightProvider mergeHeightProvider)
      {
        Dictionary<GridColumn, UIElement> bandElements = this.GetBandElements(band, panel);
        foreach (GridColumn key in bandElements.Keys)
        {
          UIElement child = bandElements[key];
          double y = 0.0;
          for (int index = 0; index < BandBase.GetGridRow((DependencyObject) key); ++index)
            y += band.RowDefinitions[index].Height.Value;
          double num = 0.0;
          for (int index = 0; index < BandBase.GetGridColumn((DependencyObject) key); ++index)
            num += band.ColumnDefinitions[index].Width.Value;
          panel.ArrangeChild(child, width + num, y, this.GetColumnWidth(BandBase.GetGridColumn((DependencyObject) key), band), this.GetRowHeight(BandBase.GetGridRow((DependencyObject) key), band, finalSize.Height));
        }
        double num1 = 0.0;
        foreach (BandColumnDefinition columnDefinition in (Collection<BandColumnDefinition>) band.ColumnDefinitions)
          num1 += columnDefinition.Width.Value;
        return width + num1;
      }

      private double GetColumnWidth(int i, BandBase band)
      {
        if (i < 0 || band.ColumnDefinitions.Count < i + 1)
          return 120.0;
        return band.ColumnDefinitions[i].Width.Value;
      }

      private double GetRowHeight(int i, BandBase band, double maxHeight)
      {
        if (i < 0 || band.RowDefinitions.Count < i + 1)
          return maxHeight;
        return band.RowDefinitions[i].Height.Value;
      }

      private Dictionary<GridColumn, UIElement> GetBandElements(BandBase band, BandsColumnHeadersPanel panel)
      {
        Dictionary<GridColumn, UIElement> dictionary = new Dictionary<GridColumn, UIElement>();
        if (band.VisibleBands.Count == 0)
        {
          foreach (FrameworkElement child in panel.Children)
          {
            GridColumn index = child.DataContext as GridColumn;
            if (band.ColumnsCore.Contains((object) index))
              dictionary[index] = (UIElement) child;
          }
        }
        return dictionary;
      }
    }

    private interface IMergeHeightProvider
    {
      double GetMergeHeight(BandBase band);
    }

    private class BandMergeHeightProvider : BandsColumnHeadersPanel.IMergeHeightProvider
    {
      private readonly Func<BandsPanel> getBandsPanelCore;
      private BandsPanel panelCore;

      private BandsPanel Panel
      {
        get
        {
          if (this.panelCore == null)
            this.panelCore = this.getBandsPanelCore();
          return this.panelCore;
        }
      }

      public BandMergeHeightProvider(Func<BandsPanel> getBandsPanel)
      {
        if (getBandsPanel == null)
          throw new ArgumentNullException();
        this.getBandsPanelCore = getBandsPanel;
      }

      public double GetMergeHeight(BandBase band)
      {
        if (band == null || band.ActualShowInBandsPanel)
          return 0.0;
        BandsPanel panel = this.Panel;
        if (panel == null)
          return 0.0;
        return panel.GetMergeHeight(band, (Func<FrameworkElement, double>) (x => x.ActualHeight));
      }
    }

    private class EmptyMergeHeightProvider : BandsColumnHeadersPanel.IMergeHeightProvider
    {
      public double GetMergeHeight(BandBase band)
      {
        return 0.0;
      }
    }
  }
}
