// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.BandsPanelBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace DevExpress.Xpf.Grid
{
  public abstract class BandsPanelBase : Panel
  {
    private Dictionary<BandBase, FrameworkElement> bandsDictionary = new Dictionary<BandBase, FrameworkElement>();
    private List<FrameworkElement> freeBands = new List<FrameworkElement>();
    public static readonly DependencyProperty BandsProperty = DependencyProperty.Register("Bands", typeof (IList<BandBase>), typeof (BandsPanelBase), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((UIElement) d).InvalidateMeasure())));

    public IList<BandBase> Bands
    {
      get
      {
        return (IList<BandBase>) this.GetValue(BandsPanelBase.BandsProperty);
      }
      set
      {
        this.SetValue(BandsPanelBase.BandsProperty, (object) value);
      }
    }

    public bool AllowVirtualization { get; set; }

    private bool ActualAllowVirtualization
    {
      get
      {
        if (this.AllowVirtualization)
          return this.TableView.AllowHorizontalScrollingVirtualization;
        return false;
      }
    }

    protected ITableView TableView
    {
      get
      {
        return this.DataView as ITableView;
      }
    }

    protected DataViewBase DataView
    {
      get
      {
        return DataControlBase.GetCurrentView((DependencyObject) this);
      }
    }

    protected override Size MeasureOverride(Size availableSize)
    {
      if (this.Bands == null)
        return Size.Empty;
      List<BandBase> list = this.bandsDictionary.Keys.ToList<BandBase>();
      this.RemoveUnusedBands(this.Bands, list, 0.0);
      foreach (BandBase key in list)
      {
        this.freeBands.Add(this.bandsDictionary[key]);
        this.bandsDictionary.Remove(key);
      }
      double num1 = 0.0;
      double num2 = 0.0;
      foreach (BandBase band in (IEnumerable<BandBase>) this.Bands)
      {
        num1 = Math.Max(num1, this.MeasureBand(band, num2));
        num2 += this.GetBandWidth(band);
      }
      foreach (FrameworkElement freeBand in this.freeBands)
      {
        freeBand.Visibility = Visibility.Collapsed;
        freeBand.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      }
      return new Size(num2, num1);
    }

    private void RemoveUnusedBands(IList<BandBase> bands, List<BandBase> unusedBands, double currentWidth)
    {
      foreach (BandBase band in (IEnumerable<BandBase>) bands)
      {
        if (!this.ActualAllowVirtualization || currentWidth + this.GetBandWidth(band) > this.TableView.TableViewBehavior.HorizontalOffset && currentWidth <= this.TableView.TableViewBehavior.HorizontalOffset + this.TableView.HorizontalViewport)
          unusedBands.Remove(band);
        if (band.VisibleBands.Count != 0)
          this.RemoveUnusedBands((IList<BandBase>) band.VisibleBands, unusedBands, currentWidth);
        currentWidth += band.ActualHeaderWidth;
      }
    }

    private double MeasureBand(BandBase band, double currentWidth)
    {
      if (this.ActualAllowVirtualization && (currentWidth > this.TableView.TableViewBehavior.HorizontalOffset + this.TableView.HorizontalViewport + this.TableView.TotalGroupAreaIndent || currentWidth + this.GetBandWidth(band) <= this.TableView.TableViewBehavior.HorizontalOffset))
        return 0.0;
      FrameworkElement frameworkElement = (FrameworkElement) null;
      if (!this.bandsDictionary.TryGetValue(band, out frameworkElement))
      {
        if (this.freeBands.Count > 0)
        {
          frameworkElement = this.freeBands[0];
          this.freeBands.RemoveAt(0);
          frameworkElement.Visibility = Visibility.Visible;
          frameworkElement.DataContext = (object) band;
          BaseGridHeader.SetGridColumn((DependencyObject) frameworkElement, (BaseColumn) band);
        }
        else
        {
          frameworkElement = this.CreateBandElement(band);
          this.Children.Add((UIElement) frameworkElement);
        }
        this.bandsDictionary[band] = frameworkElement;
      }
      frameworkElement.Measure(new Size(this.GetBandWidth(band), double.PositiveInfinity));
      double val1 = 0.0;
      foreach (BandBase visibleBand in band.VisibleBands)
      {
        val1 = Math.Max(val1, this.MeasureBand(visibleBand, currentWidth));
        currentWidth += visibleBand.ActualHeaderWidth;
      }
      return frameworkElement.DesiredSize.Height + val1;
    }

    protected abstract FrameworkElement CreateBandElement(BandBase band);

    protected override Size ArrangeOverride(Size finalSize)
    {
      if (this.Bands == null)
        return finalSize;
      double x = 0.0;
      foreach (BandBase band in (IEnumerable<BandBase>) this.Bands)
      {
        this.ArrangeBand(band, new Point(x, 0.0), finalSize.Height);
        x += this.GetBandWidth(band);
      }
      return finalSize;
    }

    private void ArrangeBand(BandBase band, Point offset, double height)
    {
      if (this.ActualAllowVirtualization && (offset.X > this.TableView.TableViewBehavior.HorizontalOffset + this.TableView.HorizontalViewport + this.TableView.TotalGroupAreaIndent || offset.X + this.GetBandWidth(band) <= this.TableView.TableViewBehavior.HorizontalOffset))
        return;
      FrameworkElement frameworkElement = this.bandsDictionary[band];
      double num1 = 0.0;
      foreach (BandBase visibleBand in band.VisibleBands)
      {
        this.ArrangeBand(visibleBand, new Point(offset.X + num1, offset.Y + frameworkElement.DesiredSize.Height), height - frameworkElement.DesiredSize.Height);
        num1 += this.GetBandWidth(visibleBand);
      }
      double num2 = band.ActualShowInBandsPanel ? this.GetMergeHeight(band.ParentBand) : 0.0;
      frameworkElement.Arrange(new Rect(offset.X, this.GetBandElementY(band, offset.Y - num2), this.GetBandWidth(band), (band.VisibleBands.Count > 0 ? frameworkElement.DesiredSize.Height : height) + num2));
    }

    protected virtual double GetBandElementY(BandBase band, double offset)
    {
      return offset;
    }

    protected virtual double GetBandWidth(BandBase band)
    {
      return band.ActualHeaderWidth;
    }

    internal FrameworkElement GetBandHeader(BandBase band)
    {
      FrameworkElement frameworkElement = (FrameworkElement) null;
      this.bandsDictionary.TryGetValue(band, out frameworkElement);
      return frameworkElement;
    }

    [Browsable(false)]
    public bool ShouldSerializeBands(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    protected virtual double GetMergeHeight(BandBase band)
    {
      return 0.0;
    }
  }
}
