// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.ScrollBarAnnotationsControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class ScrollBarAnnotationsControl : ContentControl
  {
    public static readonly DependencyProperty ScrollBarProperty;
    public static readonly DependencyProperty ViewportSizeProperty;
    public static readonly DependencyProperty MaximumProperty;
    public static readonly DependencyProperty RangeCollectionProperty;
    private double upButtonOffset;
    private double actualHeightTrack;
    private double startWidth;
    private RepeatButton _upButton;

    public ScrollBar ScrollBar
    {
      get
      {
        return (ScrollBar) this.GetValue(ScrollBarAnnotationsControl.ScrollBarProperty);
      }
      set
      {
        this.SetValue(ScrollBarAnnotationsControl.ScrollBarProperty, (object) value);
      }
    }

    public double ViewportSize
    {
      get
      {
        return (double) this.GetValue(ScrollBarAnnotationsControl.ViewportSizeProperty);
      }
      set
      {
        this.SetValue(ScrollBarAnnotationsControl.ViewportSizeProperty, (object) value);
      }
    }

    public double Maximum
    {
      get
      {
        return (double) this.GetValue(ScrollBarAnnotationsControl.MaximumProperty);
      }
      set
      {
        this.SetValue(ScrollBarAnnotationsControl.MaximumProperty, (object) value);
      }
    }

    public IEnumerable<ScrollBarAnnotationRowInfo> RangeCollection
    {
      get
      {
        return (IEnumerable<ScrollBarAnnotationRowInfo>) this.GetValue(ScrollBarAnnotationsControl.RangeCollectionProperty);
      }
      set
      {
        this.SetValue(ScrollBarAnnotationsControl.RangeCollectionProperty, (object) value);
      }
    }

    private RepeatButton UpButton
    {
      get
      {
        if (this._upButton == null)
          this._upButton = LayoutHelper.FindElementByName((FrameworkElement) this.ScrollBar, "PART_LineUpButton") as RepeatButton;
        return this._upButton;
      }
    }

    static ScrollBarAnnotationsControl()
    {
      Type ownerType = typeof (ScrollBarAnnotationsControl);
      ScrollBarAnnotationsControl.ScrollBarProperty = DependencyProperty.Register("ScrollBar", typeof (ScrollBar), ownerType, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((ScrollBarAnnotationsControl) d).ScrollBarChanged(e))));
      ScrollBarAnnotationsControl.ViewportSizeProperty = DependencyProperty.Register("ViewportSize", typeof (double), ownerType, new PropertyMetadata((object) 0.0, (PropertyChangedCallback) ((d, e) => ((UIElement) d).InvalidateVisual())));
      ScrollBarAnnotationsControl.MaximumProperty = DependencyProperty.Register("Maximum", typeof (double), ownerType, new PropertyMetadata((object) 0.0, (PropertyChangedCallback) ((d, e) => ((UIElement) d).InvalidateVisual())));
      ScrollBarAnnotationsControl.RangeCollectionProperty = DependencyProperty.Register("RangeCollection", typeof (IEnumerable<ScrollBarAnnotationRowInfo>), ownerType, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((ScrollBarAnnotationsControl) d).RangeCollectionChange())));
    }

    [Browsable(false)]
    public bool ShouldSerializeRangeCollection(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    private void ScrollBarChanged(DependencyPropertyChangedEventArgs e)
    {
      if (e.OldValue != null)
        ((UIElement) e.OldValue).LayoutUpdated -= new EventHandler(this.ScrollBar_LayoutUpdated);
      this.upButtonOffset = 0.0;
      this.actualHeightTrack = 0.0;
      if (this.ScrollBar == null)
        return;
      this.ScrollBar.LayoutUpdated += new EventHandler(this.ScrollBar_LayoutUpdated);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this._upButton = (RepeatButton) null;
    }

    private void ScrollBar_LayoutUpdated(object sender, EventArgs e)
    {
      if (this.ScrollBar == null)
        return;
      if (ScrollBarExtensions.GetScrollBarMode((DependencyObject) this.ScrollBar) == ScrollBarMode.TouchOverlap)
      {
        this.actualHeightTrack = this.ScrollBar.ActualHeight;
        this.upButtonOffset = 0.0;
      }
      else
      {
        if (this.UpButton == null || this.ScrollBar.Track == null)
          return;
        double num = this.UpButton != null ? this.UpButton.ActualHeight : 0.0;
        if (this.actualHeightTrack == this.ScrollBar.Track.ActualHeight && this.upButtonOffset == num)
          return;
        this.actualHeightTrack = this.ScrollBar.Track.ActualHeight;
        this.upButtonOffset = num;
        this.InvalidateVisual();
      }
    }

    private void RangeCollectionChange()
    {
      if (this.RangeCollection == null || !this.RangeCollection.Any<ScrollBarAnnotationRowInfo>())
        this.Visibility = Visibility.Collapsed;
      else
        this.Visibility = Visibility.Visible;
      this.InvalidateVisual();
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
      if (this.RangeCollection == null)
        return;
      base.OnRender(drawingContext);
      if (this.ScrollBar == null || this.Maximum + this.ViewportSize == 0.0)
        return;
      this.startWidth = 0.0;
      if (ScrollBarExtensions.GetScrollBarMode((DependencyObject) this.ScrollBar) == ScrollBarMode.TouchOverlap)
      {
        this.startWidth = Math.Floor(this.ActualWidth - 10.0);
        drawingContext.DrawRectangle((Brush) Brushes.Transparent, (Pen) null, new Rect(this.startWidth, 0.0, this.ActualWidth - this.startWidth, this.ActualHeight));
      }
      foreach (ScrollBarAnnotationRowInfo range in this.RangeCollection)
      {
        Tuple<double, double> y = this.GetY(range);
        double width = Math.Floor(Math.Min(range.ScrollAnnotationInfo.Width, this.ActualWidth));
        Point point0;
        Point point1;
        if (y.Item1 == y.Item2)
        {
          point0 = this.GetPoint0(range.ScrollAnnotationInfo.Alignment, y.Item1, width);
          point1 = this.GetPoint1(range.ScrollAnnotationInfo.Alignment, y.Item1 + range.ScrollAnnotationInfo.MinHeight, width);
        }
        else
        {
          point0 = this.GetPoint0(range.ScrollAnnotationInfo.Alignment, y.Item1, width);
          point1 = this.GetPoint1(range.ScrollAnnotationInfo.Alignment, y.Item2, width);
          if (point1.Y - point0.Y < range.ScrollAnnotationInfo.MinHeight)
            point1.Y = point0.Y + range.ScrollAnnotationInfo.MinHeight;
        }
        drawingContext.DrawRectangle(range.ScrollAnnotationInfo.Brush, (Pen) null, new Rect(point0, point1));
      }
    }

    private double GetY(int visibleIndex, double thickness, bool center = false, bool end = false)
    {
      if (center)
      {
        double num1 = Math.Min(Math.Ceiling(Math.Max(this.ScrollBar.Track.ActualHeight, this.actualHeightTrack) / (this.Maximum + this.ViewportSize) * (double) visibleIndex + this.upButtonOffset), this.ActualHeight - thickness * 2.0);
        double num2 = Math.Min(Math.Ceiling(Math.Max(this.ScrollBar.Track.ActualHeight, this.actualHeightTrack) / (this.Maximum + this.ViewportSize) * (double) (visibleIndex + 1) + this.upButtonOffset), this.ActualHeight - thickness * 2.0);
        return num1 + (num2 - num1) / 2.0;
      }
      if (end)
        return Math.Min(Math.Ceiling(Math.Max(this.ScrollBar.Track.ActualHeight, this.actualHeightTrack) / (this.Maximum + this.ViewportSize) * (double) (visibleIndex + 1) + this.upButtonOffset), this.ActualHeight - thickness * 2.0);
      return Math.Min(Math.Ceiling(Math.Max(this.ScrollBar.Track.ActualHeight, this.actualHeightTrack) / (this.Maximum + this.ViewportSize) * (double) visibleIndex + this.upButtonOffset), this.ActualHeight - thickness * 2.0);
    }

    private Tuple<double, double> GetY(ScrollBarAnnotationRowInfo info)
    {
      double minHeight = info.ScrollAnnotationInfo.MinHeight;
      return new Tuple<double, double>(this.GetY(info.RowIndex, minHeight, info.ScrollAnnotationInfo.Alignment == ScrollBarAnnotationAlignment.Full, false), this.GetY(info.EndRowIndex, minHeight, false, info.ScrollAnnotationInfo.Alignment != ScrollBarAnnotationAlignment.Full));
    }

    private Point GetPoint0(ScrollBarAnnotationAlignment alignment, double y, double width)
    {
      switch (alignment)
      {
        case ScrollBarAnnotationAlignment.Left:
        case ScrollBarAnnotationAlignment.Full:
          return this.PointFloor(this.startWidth, y);
        case ScrollBarAnnotationAlignment.Right:
          return this.PointFloor(Math.Floor(this.ActualWidth - width), y);
        case ScrollBarAnnotationAlignment.Center:
          return this.PointFloor((this.ActualWidth - this.startWidth) / 2.0 - width / 2.0 + this.startWidth, y);
        default:
          return this.PointFloor(0.0, 0.0);
      }
    }

    private Point GetPoint1(ScrollBarAnnotationAlignment alignment, double y, double width)
    {
      switch (alignment)
      {
        case ScrollBarAnnotationAlignment.Left:
          return this.PointFloor(this.startWidth + width, y);
        case ScrollBarAnnotationAlignment.Right:
        case ScrollBarAnnotationAlignment.Full:
          return this.PointFloor(Math.Floor(this.ActualWidth), y);
        case ScrollBarAnnotationAlignment.Center:
          return this.PointFloor((this.ActualWidth - this.startWidth) / 2.0 + width / 2.0 + this.startWidth, y);
        default:
          return this.PointFloor(0.0, 0.0);
      }
    }

    private Point PointFloor(double x, double y)
    {
      return new Point(Math.Floor(x), Math.Floor(y));
    }
  }
}
