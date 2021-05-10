// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.RowOffsetBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DevExpress.Xpf.Grid
{
  public class RowOffsetBase : Control
  {
    public static readonly DependencyProperty OffsetProperty = DependencyPropertyManager.Register("Offset", typeof (double), typeof (RowOffsetBase));
    public static readonly DependencyProperty RowLevelProperty = DependencyPropertyManager.Register("RowLevel", typeof (int), typeof (RowOffsetBase), (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) ((d, e) => ((RowOffsetBase) d).UpdateContent(((FrameworkElement) d).ActualHeight))));
    public static readonly DependencyProperty NextRowLevelProperty = DependencyPropertyManager.Register("NextRowLevel", typeof (int), typeof (RowOffsetBase), (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) ((d, e) => ((RowOffsetBase) d).OnNextRowLevelChanged())));

    public double Offset
    {
      get
      {
        return (double) this.GetValue(RowOffsetBase.OffsetProperty);
      }
      set
      {
        this.SetValue(RowOffsetBase.OffsetProperty, (object) value);
      }
    }

    public int RowLevel
    {
      get
      {
        return (int) this.GetValue(RowOffsetBase.RowLevelProperty);
      }
      set
      {
        this.SetValue(RowOffsetBase.RowLevelProperty, (object) value);
      }
    }

    public int NextRowLevel
    {
      get
      {
        return (int) this.GetValue(RowOffsetBase.NextRowLevelProperty);
      }
      set
      {
        this.SetValue(RowOffsetBase.NextRowLevelProperty, (object) value);
      }
    }

    protected Path OffsetPath { get; set; }

    private double OldHeight { get; set; }

    protected RowData RowData
    {
      get
      {
        return this.DataContext as RowData;
      }
    }

    protected DataViewBase View
    {
      get
      {
        if (this.RowData == null)
          return (DataViewBase) null;
        return this.RowData.View;
      }
    }

    protected GeometryGroup Group { get; set; }

    public RowOffsetBase()
    {
      this.OldHeight = double.MinValue;
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.OffsetPath = this.GetTemplateChild("PART_OffsetPath") as Path;
      this.Loaded += new RoutedEventHandler(this.RowOffsetBase_Loaded);
      this.SizeChanged += new SizeChangedEventHandler(this.RowOffsetBase_SizeChanged);
    }

    private void RowOffsetBase_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      if (this.OldHeight == double.MinValue || this.OldHeight == this.ActualHeight)
        return;
      this.DrawLines(this.ActualHeight);
    }

    private void RowOffsetBase_Loaded(object sender, RoutedEventArgs e)
    {
      this.UpdateContent(this.ActualHeight);
    }

    protected virtual void OnNextRowLevelChanged()
    {
      this.DrawLines(this.ActualHeight);
    }

    protected void DrawLines(double height)
    {
      if (this.OffsetPath == null)
        return;
      this.OldHeight = height;
      this.Group = new GeometryGroup();
      this.DrawLinesCore(height);
      if (this.OffsetPath == null)
        return;
      this.OffsetPath.Data = (Geometry) this.Group;
      this.OffsetPath.Height = height;
    }

    protected virtual void DrawLinesCore(double height)
    {
    }

    protected virtual void ChangeWidth()
    {
      this.Width = this.Offset * (double) this.RowLevel;
    }

    protected virtual void UpdateContent(double height)
    {
      this.ChangeWidth();
      this.DrawLines(height);
    }

    protected override Size MeasureOverride(Size constraint)
    {
      return new Size(0.0, 0.0);
    }

    protected Size BaseMeasureOverride(Size constraint)
    {
      return base.MeasureOverride(constraint);
    }
  }
}
