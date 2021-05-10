// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridCardPanel
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class GridCardPanel : Panel
  {
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof (UIElement), typeof (GridCardPanel), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridCardPanel) d).OnChildChanged((UIElement) e.OldValue, (UIElement) e.NewValue))));
    public static readonly DependencyProperty BodyProperty = DependencyProperty.Register("Body", typeof (UIElement), typeof (GridCardPanel), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridCardPanel) d).OnBodyChanged((UIElement) e.OldValue))));
    public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register("IsExpanded", typeof (bool), typeof (GridCardPanel), (PropertyMetadata) new FrameworkPropertyMetadata((object) true, FrameworkPropertyMetadataOptions.AffectsMeasure, (PropertyChangedCallback) ((d, e) => ((GridCardPanel) d).UpdateBodyVisibility())));
    public static readonly DependencyProperty RotateOnCollapseProperty = DependencyProperty.Register("RotateOnCollapse", typeof (bool), typeof (GridCardPanel), (PropertyMetadata) new FrameworkPropertyMetadata((object) false, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public UIElement Header
    {
      get
      {
        return (UIElement) this.GetValue(GridCardPanel.HeaderProperty);
      }
      set
      {
        this.SetValue(GridCardPanel.HeaderProperty, (object) value);
      }
    }

    public UIElement Body
    {
      get
      {
        return (UIElement) this.GetValue(GridCardPanel.BodyProperty);
      }
      set
      {
        this.SetValue(GridCardPanel.BodyProperty, (object) value);
      }
    }

    public bool IsExpanded
    {
      get
      {
        return (bool) this.GetValue(GridCardPanel.IsExpandedProperty);
      }
      set
      {
        this.SetValue(GridCardPanel.IsExpandedProperty, (object) value);
      }
    }

    public bool RotateOnCollapse
    {
      get
      {
        return (bool) this.GetValue(GridCardPanel.RotateOnCollapseProperty);
      }
      set
      {
        this.SetValue(GridCardPanel.RotateOnCollapseProperty, (object) value);
      }
    }

    private void OnBodyChanged(UIElement oldValue)
    {
      this.OnChildChanged(oldValue, this.Body);
      this.UpdateBodyVisibility();
    }

    private void OnChildChanged(UIElement oldValue, UIElement newValue)
    {
      if (oldValue != null)
        this.Children.Remove(oldValue);
      if (newValue == null)
        return;
      this.Children.Add(newValue);
    }

    private void UpdateBodyVisibility()
    {
      if (this.Body == null)
        return;
      if (this.IsExpanded)
        this.Body.Visibility = Visibility.Visible;
      else
        this.Body.Visibility = Visibility.Hidden;
    }

    protected override Size MeasureOverride(Size availableSize)
    {
      this.Header.Measure(availableSize);
      this.Body.Measure(new Size(availableSize.Width, availableSize.Height - this.Header.DesiredSize.Height));
      if (!this.RotateOnCollapse || this.IsExpanded)
        return new Size(Math.Max(this.Header.DesiredSize.Width, this.Body.DesiredSize.Width), this.Header.DesiredSize.Height + (this.IsExpanded ? this.Body.DesiredSize.Height : 0.0));
      this.Header.InvalidateMeasure();
      this.Header.Measure(new Size(this.Header.DesiredSize.Height + this.Body.DesiredSize.Height, double.PositiveInfinity));
      return new Size(this.Header.DesiredSize.Height, this.Header.DesiredSize.Height + this.Body.DesiredSize.Height);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      TransformGroup transformGroup = (TransformGroup) null;
      if (!this.IsExpanded && this.RotateOnCollapse)
      {
        transformGroup = new TransformGroup();
        transformGroup.Children.Add((Transform) new RotateTransform()
        {
          Angle = -90.0
        });
        transformGroup.Children.Add((Transform) new TranslateTransform()
        {
          Y = finalSize.Height
        });
      }
      this.Header.RenderTransform = (Transform) transformGroup ?? Transform.Identity;
      this.Header.Arrange(this.IsExpanded || !this.RotateOnCollapse ? new Rect(0.0, 0.0, finalSize.Width, this.Header.DesiredSize.Height) : new Rect(0.0, 0.0, finalSize.Height, finalSize.Width));
      this.Body.Arrange(this.IsExpanded ? new Rect(0.0, this.Header.DesiredSize.Height, finalSize.Width, finalSize.Height - this.Header.DesiredSize.Height) : new Rect());
      return finalSize;
    }
  }
}
