// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.IndentsPanel
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class IndentsPanel : Panel
  {
    public static readonly DependencyProperty RowIndentProperty = DependencyPropertyManager.Register("RowIndent", typeof (double), typeof (IndentsPanel), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public double RowIndent
    {
      get
      {
        return (double) this.GetValue(IndentsPanel.RowIndentProperty);
      }
      set
      {
        this.SetValue(IndentsPanel.RowIndentProperty, (object) value);
      }
    }

    protected override Size MeasureOverride(Size availableSize)
    {
      int num = 0;
      double height = 0.0;
      foreach (UIElement child in this.Children)
      {
        child.Measure(new Size(this.RowIndent, availableSize.Height));
        if (this.CanProcessChild(child))
          ++num;
        if (child.DesiredSize.Height > height)
          height = child.DesiredSize.Height;
      }
      return new Size(this.RowIndent * (double) num, height);
    }

    protected virtual bool CanProcessChild(UIElement child)
    {
      if (child.Visibility != Visibility.Visible)
        return child.Visibility == Visibility.Hidden;
      return true;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      int num = 0;
      foreach (UIElement child in this.Children)
      {
        if (this.CanProcessChild(child) || child is TreeListNodeExpandButton)
        {
          double x = (double) num * this.RowIndent + (this.RowIndent - child.DesiredSize.Width) / 2.0;
          double y = (finalSize.Height - child.DesiredSize.Height) / 2.0;
          child.Arrange(new Rect(new Point(x, y), child.DesiredSize));
          ++num;
        }
      }
      return finalSize;
    }
  }
}
