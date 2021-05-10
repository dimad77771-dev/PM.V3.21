// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DragIndicatorPanel
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class DragIndicatorPanel : Panel
  {
    public static readonly DependencyProperty DropPlaceOrientationProperty = DependencyProperty.Register("DropPlaceOrientation", typeof (Orientation), typeof (DragIndicatorPanel), new PropertyMetadata((object) Orientation.Horizontal, (PropertyChangedCallback) ((d, e) => ((UIElement) d).InvalidateMeasure())));

    public Orientation DropPlaceOrientation
    {
      get
      {
        return (Orientation) this.GetValue(DragIndicatorPanel.DropPlaceOrientationProperty);
      }
      set
      {
        this.SetValue(DragIndicatorPanel.DropPlaceOrientationProperty, (object) value);
      }
    }

    protected override Size MeasureOverride(Size availableSize)
    {
      if (this.Children.Count == 0)
        return Size.Empty;
      UIElement uiElement = this.Children[0];
      uiElement.Measure(this.GetCorrectSize(availableSize));
      return this.GetCorrectSize(uiElement.DesiredSize);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      if (this.Children.Count == 0)
        return Size.Empty;
      UIElement uiElement = this.Children[0];
      Size correctSize = this.GetCorrectSize(finalSize);
      uiElement.Arrange(new Rect(0.0, 0.0, correctSize.Width, correctSize.Height));
      this.RenderTransform = this.GetTransform();
      return this.GetCorrectSize(correctSize);
    }

    private Size GetCorrectSize(Size size)
    {
      if (this.DropPlaceOrientation != Orientation.Horizontal)
        return new Size(size.Height, size.Width);
      return size;
    }

    private Transform GetTransform()
    {
      if (this.DropPlaceOrientation != Orientation.Horizontal)
        return this.GetTransformVertical();
      return this.GetTransformHorizontal();
    }

    private Transform GetTransformVertical()
    {
      return (Transform) new RotateTransform() { Angle = -90.0, CenterX = 0.5, CenterY = 0.5 };
    }

    private Transform GetTransformHorizontal()
    {
      return (Transform) new MatrixTransform();
    }
  }
}
