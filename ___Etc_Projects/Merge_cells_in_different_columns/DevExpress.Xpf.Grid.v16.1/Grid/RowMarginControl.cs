// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.RowMarginControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.HitTest;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Grid.TreeList;
using DevExpress.Xpf.Utils;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DevExpress.Xpf.Grid
{
  public class RowMarginControl : RowOffsetBase, ISupportLoadingAnimation
  {
    public static readonly DependencyProperty IsReadyProperty = DependencyPropertyManager.Register("IsReady", typeof (bool), typeof (RowMarginControl), new PropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((RowMarginControl) d).OnIsReadyChanged())));
    public static readonly DependencyProperty TreeLineStyleProperty = DependencyPropertyManager.Register("TreeLineStyle", typeof (TreeListLineStyle), typeof (RowMarginControl), new PropertyMetadata((object) TreeListLineStyle.Solid, (PropertyChangedCallback) ((d, e) => ((RowOffsetBase) d).UpdateContent(((FrameworkElement) d).ActualHeight))));
    public static readonly DependencyProperty ShowVerticalLinesProperty = DependencyPropertyManager.Register("ShowVerticalLines", typeof (bool), typeof (RowMarginControl), new PropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((RowOffsetBase) d).UpdateContent(((FrameworkElement) d).ActualHeight))));
    private LoadingAnimationHelper loadingAnimationHelper;

    public TreeListLineStyle TreeLineStyle
    {
      get
      {
        return (TreeListLineStyle) this.GetValue(RowMarginControl.TreeLineStyleProperty);
      }
      set
      {
        this.SetValue(RowMarginControl.TreeLineStyleProperty, (object) value);
      }
    }

    public bool ShowVerticalLines
    {
      get
      {
        return (bool) this.GetValue(RowMarginControl.ShowVerticalLinesProperty);
      }
      set
      {
        this.SetValue(RowMarginControl.ShowVerticalLinesProperty, (object) value);
      }
    }

    public bool IsReady
    {
      get
      {
        return (bool) this.GetValue(RowMarginControl.IsReadyProperty);
      }
      set
      {
        this.SetValue(RowMarginControl.IsReadyProperty, (object) value);
      }
    }

    protected TreeListRowData RowData
    {
      get
      {
        return base.RowData as TreeListRowData;
      }
    }

    protected TreeListView View
    {
      get
      {
        return base.View as TreeListView;
      }
    }

    protected bool ShowTreeLines
    {
      get
      {
        return this.TreeLineStyle != TreeListLineStyle.None;
      }
    }

    protected new int RowLevel
    {
      get
      {
        return this.RowData.RowLevel;
      }
    }

    protected int ActualRowLevel
    {
      get
      {
        return this.RowLevel + this.View.ServiceIndentsCount;
      }
    }

    protected Path TreeLinePath { get; private set; }

    protected TreeListNodeExpandButton ExpandButton { get; private set; }

    protected CheckBox CheckBox { get; private set; }

    protected GeometryGroup TreeLineGeometry { get; private set; }

    protected Image Image { get; private set; }

    DataViewBase ISupportLoadingAnimation.DataView
    {
      get
      {
        return (DataViewBase) this.View;
      }
    }

    FrameworkElement ISupportLoadingAnimation.Element
    {
      get
      {
        return (FrameworkElement) this.Image;
      }
    }

    bool ISupportLoadingAnimation.IsGroupRow
    {
      get
      {
        return false;
      }
    }

    internal LoadingAnimationHelper LoadingAnimationHelper
    {
      get
      {
        if (this.loadingAnimationHelper == null)
          this.loadingAnimationHelper = new LoadingAnimationHelper((ISupportLoadingAnimation) this);
        return this.loadingAnimationHelper;
      }
    }

    public RowMarginControl()
    {
      this.SetDefaultStyleKey(typeof (RowMarginControl));
      this.SetBinding(RowMarginControl.IsReadyProperty, (BindingBase) new Binding("IsReady"));
      GridViewHitInfoBase.SetHitTestAcceptor((DependencyObject) this, (DataViewHitTestAcceptorBase) new RowMarginControlHitTestAcceptor());
    }

    protected override void ChangeWidth()
    {
      if (this.View == null)
        return;
      this.Offset = this.View.RowIndent;
      this.Width = this.Offset * (double) this.ActualRowLevel;
    }

    protected override void OnNextRowLevelChanged()
    {
      this.UpdateContent(this.ActualHeight);
    }

    protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
    {
      e.Handled = this.CanProcessMouseDown();
      base.OnPreviewMouseDown(e);
    }

    private bool CanProcessMouseDown()
    {
      if (this.View != null)
        return !this.View.RequestUIUpdate();
      return false;
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.TreeLinePath = this.GetTemplateChild("PART_TreeLinePath") as Path;
      this.ExpandButton = this.GetTemplateChild("PART_ExpandButton") as TreeListNodeExpandButton;
      this.CheckBox = this.GetTemplateChild("PART_NodeCheckBox") as CheckBox;
      this.Image = this.GetTemplateChild("PART_NodeImage") as Image;
      if (this.View == null)
        return;
      this.SetBinding(RowMarginControl.TreeLineStyleProperty, (BindingBase) new Binding("View.TreeLineStyle"));
      this.SetBinding(RowMarginControl.ShowVerticalLinesProperty, (BindingBase) new Binding("View.ShowVerticalLines"));
    }

    protected override Size MeasureOverride(Size constraint)
    {
      Size size = this.BaseMeasureOverride(constraint);
      size.Height = Math.Min(1.0, size.Height);
      size.Width = this.View.RowIndent * (double) this.ActualRowLevel;
      return size;
    }

    protected override void UpdateContent(double height)
    {
      if (this.RowData == null || this.View == null)
        return;
      base.UpdateContent(height);
    }

    protected override void DrawLinesCore(double height)
    {
      base.DrawLinesCore(height);
      if (this.RowData == null || this.View == null)
        return;
      height = this.CorrectHeight(height);
      this.DrawRowLines(height);
      this.DrawTreeLines(height);
    }

    protected virtual double CorrectHeight(double height)
    {
      return height;
    }

    protected void DrawTreeLines(double height)
    {
      if (this.TreeLinePath == null)
        return;
      this.TreeLineGeometry = new GeometryGroup();
      if (this.RowData.Indents != null && this.ShowTreeLines)
      {
        List<TreeListIndentType> indents = this.RowData.Indents;
        for (int index = 0; index < indents.Count; ++index)
          this.DrawTreeLine(index, indents[index], height);
      }
      this.TreeLinePath.Data = (Geometry) this.TreeLineGeometry;
    }

    protected virtual void DrawRowLines(double height)
    {
      this.DrawRowHorizontalLine(height);
      this.DrawRowVerticalLine(height);
    }

    protected virtual void DrawRowHorizontalLine(double height)
    {
      if (!this.View.ShowHorizontalLines || this.NextRowLevel <= -1 || this.RowLevel <= this.NextRowLevel)
        return;
      this.Group.Children.Add((Geometry) this.CreateLine(new Point((double) this.ActualRowLevel * this.Offset + 1.0, height - 0.5), new Point((double) (this.NextRowLevel + this.View.ServiceIndentsCount) * this.Offset, height - 0.5)));
    }

    protected virtual void DrawRowVerticalLine(double height)
    {
      if (!this.ShowVerticalLines)
        return;
      this.Group.Children.Add((Geometry) this.CreateLine(new Point(Math.Abs((double) this.ActualRowLevel * this.Offset - 0.5), this.View.ShowHorizontalLines ? -1.0 : 0.0), new Point(Math.Abs((double) this.ActualRowLevel * this.Offset - 0.5), height)));
    }

    protected virtual void DrawTreeLine(int index, TreeListIndentType indent, double height)
    {
      double num1 = (double) index * this.Offset;
      double x = num1 + this.Offset;
      double num2 = Math.Round(this.Offset / 2.0) - 0.5;
      double y = Math.Floor(height / 2.0) + 0.5;
      switch (indent)
      {
        case TreeListIndentType.Line:
          this.TreeLineGeometry.Children.Add((Geometry) this.CreateLine(new Point(num1 + num2, 0.0), new Point(num1 + num2, height)));
          break;
        case TreeListIndentType.Root:
          this.TreeLineGeometry.Children.Add((Geometry) this.CreateLine(new Point(num1 + num2, y), new Point(x, y)));
          break;
        case TreeListIndentType.First:
          this.TreeLineGeometry.Children.Add((Geometry) this.CreateLine(new Point(num1 + num2, y), new Point(num1 + num2, height)));
          this.TreeLineGeometry.Children.Add((Geometry) this.CreateLine(new Point(num1 + num2, y), new Point(x, y)));
          break;
        case TreeListIndentType.Last:
          this.TreeLineGeometry.Children.Add((Geometry) this.CreateLine(new Point(num1 + num2, 0.0), new Point(num1 + num2, y)));
          this.TreeLineGeometry.Children.Add((Geometry) this.CreateLine(new Point(num1 + num2, y), new Point(x, y)));
          break;
        case TreeListIndentType.Middle:
          this.TreeLineGeometry.Children.Add((Geometry) this.CreateLine(new Point(num1 + num2, y), new Point(x, y)));
          this.TreeLineGeometry.Children.Add((Geometry) this.CreateLine(new Point(num1 + num2, 0.0), new Point(num1 + num2, height)));
          break;
      }
    }

    private LineGeometry CreateLine(Point startPoint, Point endPoint)
    {
      return new LineGeometry() { StartPoint = startPoint, EndPoint = endPoint };
    }

    private void OnIsReadyChanged()
    {
      if (this.DataContext == null)
        return;
      this.LoadingAnimationHelper.ApplyAnimation();
    }
  }
}
