// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.ScrollBarAttachedBehavior
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid.Native
{
  public class ScrollBarAttachedBehavior
  {
    public static readonly DependencyProperty UpdateThumbOrientationProperty = DependencyProperty.RegisterAttached("UpdateThumbOrientation", typeof (Orientation?), typeof (ScrollBarAttachedBehavior), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(ScrollBarAttachedBehavior.OnUpdateThumbOrientationChanged)));
    public static readonly DependencyProperty ScrollBarAttachedBehaviorProperty = DependencyProperty.RegisterAttached("ScrollBarAttachedBehavior", typeof (ScrollBarAttachedBehavior), typeof (ScrollBarAttachedBehavior), new PropertyMetadata((PropertyChangedCallback) null));
    private Thumb thumb;
    private SizeHelperBase sizeHelper;
    private Rect oldRect;

    public ScrollBarAttachedBehavior(Thumb thumb, Orientation orientation)
    {
      this.thumb = thumb;
      this.sizeHelper = SizeHelperBase.GetDefineSizeHelper(orientation);
      this.thumb.LayoutUpdated += new EventHandler(this.thumb_LayoutUpdated);
    }

    public static Orientation? GetUpdateThumbOrientation(DependencyObject obj)
    {
      return (Orientation?) obj.GetValue(ScrollBarAttachedBehavior.UpdateThumbOrientationProperty);
    }

    public static void SetUpdateThumbOrientation(DependencyObject obj, Orientation? value)
    {
      obj.SetValue(ScrollBarAttachedBehavior.UpdateThumbOrientationProperty, (object) value);
    }

    public static ScrollBarAttachedBehavior GetScrollBarAttachedBehavior(DependencyObject obj)
    {
      return (ScrollBarAttachedBehavior) obj.GetValue(ScrollBarAttachedBehavior.ScrollBarAttachedBehaviorProperty);
    }

    public static void SetScrollBarAttachedBehavior(DependencyObject obj, ScrollBarAttachedBehavior value)
    {
      obj.SetValue(ScrollBarAttachedBehavior.ScrollBarAttachedBehaviorProperty, (object) value);
    }

    private static void OnUpdateThumbOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (!(d is Thumb))
        return;
      Orientation? thumbOrientation = ScrollBarAttachedBehavior.GetUpdateThumbOrientation(d);
      if (thumbOrientation.HasValue)
      {
        ScrollBarAttachedBehavior.SetScrollBarAttachedBehavior(d, new ScrollBarAttachedBehavior((Thumb) d, thumbOrientation.Value));
      }
      else
      {
        ScrollBarAttachedBehavior.GetScrollBarAttachedBehavior(d).Release();
        ScrollBarAttachedBehavior.SetScrollBarAttachedBehavior(d, (ScrollBarAttachedBehavior) null);
      }
    }

    private void thumb_LayoutUpdated(object sender, EventArgs e)
    {
      if (!this.thumb.IsDragging)
        return;
      FrameworkElement frameworkElement = VisualTreeHelper.GetParent((DependencyObject) this.thumb) as FrameworkElement;
      if (frameworkElement == null)
        return;
      Rect relativeElementRect = LayoutHelper.GetRelativeElementRect((UIElement) this.thumb, (UIElement) frameworkElement);
      if (this.oldRect == relativeElementRect || this.sizeHelper.GetDefinePoint(relativeElementRect.TopLeft) == 0.0 || Math.Round(this.sizeHelper.GetDefinePoint(relativeElementRect.BottomRight)) == this.sizeHelper.GetDefinePoint(new Point(frameworkElement.ActualWidth, frameworkElement.ActualHeight)))
        return;
      this.oldRect = relativeElementRect;
      FieldInfo field = typeof (Thumb).GetField("_originThumbPoint", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
      Point point = (Point) field.GetValue((object) this.thumb);
      Point position = Mouse.GetPosition((IInputElement) this.thumb);
      if (Math.Abs(this.sizeHelper.GetSecondaryPoint(point) - this.sizeHelper.GetSecondaryPoint(position)) > 150.0)
        return;
      field.SetValue((object) this.thumb, (object) this.sizeHelper.CreatePoint(this.sizeHelper.GetDefinePoint(position), this.sizeHelper.GetSecondaryPoint(point)));
    }

    private void Release()
    {
      this.thumb.LayoutUpdated -= new EventHandler(this.thumb_LayoutUpdated);
      this.thumb = (Thumb) null;
    }
  }
}
