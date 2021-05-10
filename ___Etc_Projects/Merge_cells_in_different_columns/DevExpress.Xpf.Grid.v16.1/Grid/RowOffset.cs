// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.RowOffset
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class RowOffset : RowOffsetBase
  {
    public static readonly DependencyProperty AllowVerticalLinesProperty = DependencyPropertyManager.Register("AllowVerticalLines", typeof (bool), typeof (RowOffset));
    public static readonly DependencyProperty AllowHorizontalLinesProperty = DependencyPropertyManager.Register("AllowHorizontalLines", typeof (bool), typeof (RowOffset), (PropertyMetadata) new FrameworkPropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((RowOffsetBase) d).UpdateContent(((FrameworkElement) d).ActualHeight))));
    public static readonly DependencyProperty ShowRowBreakProperty = DependencyProperty.Register("ShowRowBreak", typeof (bool), typeof (RowOffset), new PropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((RowOffset) d).OnShowRowBreakChanged())));

    protected bool ShowGroupSummaryFooter
    {
      get
      {
        if (this.View == null)
          return false;
        return this.View.ShowGroupSummaryFooter;
      }
    }

    public bool AllowVerticalLines
    {
      get
      {
        return (bool) this.GetValue(RowOffset.AllowVerticalLinesProperty);
      }
      set
      {
        this.SetValue(RowOffset.AllowVerticalLinesProperty, (object) value);
      }
    }

    public bool AllowHorizontalLines
    {
      get
      {
        return (bool) this.GetValue(RowOffset.AllowHorizontalLinesProperty);
      }
      set
      {
        this.SetValue(RowOffset.AllowHorizontalLinesProperty, (object) value);
      }
    }

    public bool ShowRowBreak
    {
      get
      {
        return (bool) this.GetValue(RowOffset.ShowRowBreakProperty);
      }
      set
      {
        this.SetValue(RowOffset.ShowRowBreakProperty, (object) value);
      }
    }

    private void OnShowRowBreakChanged()
    {
      this.DrawLines(this.ActualHeight);
    }

    protected override void DrawLinesCore(double height)
    {
      if (this.AllowHorizontalLines && !this.ShowRowBreak)
        this.DrawHorizontalLines(height);
      if (!this.AllowVerticalLines)
        return;
      this.DrawVerticalLines(height);
    }

    protected virtual void DrawHorizontalLines(double height)
    {
      int num = this.RowLevel - this.NextRowLevel;
      if (this.ShowGroupSummaryFooter && num > 0)
        num = 1;
      int rowLevel = this.RowLevel;
      double y = height - 0.5;
      while (num > 0)
      {
        this.Group.Children.Add((Geometry) this.GetLine(new Point((double) rowLevel * this.Offset, y), new Point((double) (rowLevel - 1) * this.Offset, y)));
        --num;
        --rowLevel;
      }
    }

    protected virtual void DrawVerticalLines(double height)
    {
      for (int index = 1; index <= this.RowLevel; ++index)
        this.Group.Children.Add((Geometry) this.GetLine(new Point((double) index * this.Offset - 0.5, 0.0), new Point((double) index * this.Offset - 0.5, height)));
    }

    private LineGeometry GetLine(Point startPoint, Point endPoint)
    {
      return new LineGeometry() { StartPoint = startPoint, EndPoint = endPoint };
    }
  }
}
