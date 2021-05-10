// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.FixedTotalSummaryPrintPanel
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class FixedTotalSummaryPrintPanel : Panel
  {
    private const string LeftPart = "PART_EditLeft";
    private const string RightPart = "PART_EditRight";
    private FrameworkElement leftControl;
    private FrameworkElement rightControl;
    private bool measureHalfProportionMode;

    public FrameworkElement LeftControl
    {
      get
      {
        if (this.leftControl == null)
          this.leftControl = this.FindPart("PART_EditLeft");
        return this.leftControl;
      }
    }

    public FrameworkElement RightControl
    {
      get
      {
        if (this.rightControl == null)
          this.rightControl = this.FindPart("PART_EditRight");
        return this.rightControl;
      }
    }

    private bool AllowLeftSide
    {
      get
      {
        if (this.LeftControl != null)
          return this.LeftControl.Visibility == Visibility.Visible;
        return false;
      }
    }

    private bool AllowRightSide
    {
      get
      {
        if (this.RightControl != null)
          return this.RightControl.Visibility == Visibility.Visible;
        return false;
      }
    }

    private FrameworkElement FindPart(string name)
    {
      FrameworkElement frameworkElement = (FrameworkElement) null;
      foreach (FrameworkElement child in this.Children)
      {
        if (child.Name == name)
        {
          frameworkElement = child;
          break;
        }
      }
      return frameworkElement;
    }

    protected override Size MeasureOverride(Size constraint)
    {
      this.measureHalfProportionMode = false;
      if (!this.AllowLeftSide)
      {
        this.RightControl.Measure(constraint);
        return new Size(constraint.Width, this.RightControl.DesiredSize.Height);
      }
      double maximumHeight = 0.0;
      if (!this.AllowRightSide)
      {
        this.MeasureRealProportion(constraint, out maximumHeight);
        return new Size(constraint.Width, maximumHeight);
      }
      bool flag = false;
      Size availableSize = new Size(double.PositiveInfinity, Math.Max(0.0, constraint.Height));
      this.RightControl.Measure(availableSize);
      if (this.RightControl.DesiredSize.Width >= constraint.Width)
        flag = true;
      if (!flag)
      {
        this.LeftControl.Measure(availableSize);
        if (this.LeftControl.DesiredSize.Width + this.RightControl.DesiredSize.Width >= constraint.Width)
          flag = true;
      }
      if (!flag)
        this.MeasureRealProportion(constraint, out maximumHeight);
      else
        this.MeasureHalfProportion(constraint, out maximumHeight);
      return new Size(constraint.Width, maximumHeight);
    }

    private void MeasureRealProportion(Size constraint, out double maximumHeight)
    {
      Size availableSize = new Size(Math.Max(0.0, constraint.Width), Math.Max(0.0, constraint.Height));
      this.RightControl.Measure(availableSize);
      double width = this.RightControl.DesiredSize.Width;
      maximumHeight = Math.Max(0.0, this.RightControl.DesiredSize.Height);
      availableSize = new Size(Math.Max(0.0, constraint.Width - width), Math.Max(0.0, constraint.Height));
      this.LeftControl.Measure(availableSize);
      double num = width + this.LeftControl.DesiredSize.Width;
      maximumHeight = Math.Max(maximumHeight, this.LeftControl.DesiredSize.Height);
      maximumHeight = Math.Max(0.0, maximumHeight);
    }

    private void MeasureHalfProportion(Size constraint, out double maximumHeight)
    {
      this.measureHalfProportionMode = true;
      Size availableSize = new Size(Math.Floor(Math.Max(0.0, constraint.Width) / 2.0) - 2.0, constraint.Height);
      this.LeftControl.Measure(availableSize);
      this.RightControl.Measure(availableSize);
      maximumHeight = Math.Max(this.LeftControl.DesiredSize.Height, this.RightControl.DesiredSize.Height);
    }

    protected override Size ArrangeOverride(Size arrangeSize)
    {
      Rect empty = Rect.Empty;
      if (!this.AllowLeftSide)
      {
        this.RightControl.Arrange(this.GetRemainingRect(arrangeSize, 0.0, 0.0, 0.0, 0.0));
        return arrangeSize;
      }
      double left = 0.0;
      double top = 0.0;
      double right1 = 0.0;
      double bottom = 0.0;
      Rect remainingRect = this.GetRemainingRect(arrangeSize, left, top, right1, bottom);
      double right2 = right1 + (this.measureHalfProportionMode ? arrangeSize.Width / 2.0 - 2.0 : this.RightControl.DesiredSize.Width);
      remainingRect.X = Math.Max(0.0, arrangeSize.Width - right2);
      remainingRect.Width = this.measureHalfProportionMode ? arrangeSize.Width / 2.0 - 2.0 : this.RightControl.DesiredSize.Width;
      this.RightControl.Arrange(remainingRect);
      this.LeftControl.Arrange(this.GetRemainingRect(arrangeSize, left, top, right2, bottom));
      return arrangeSize;
    }

    private Rect GetRemainingRect(Size arrangeSize, double left, double top, double right, double bottom)
    {
      return new Rect(left, top, Math.Max(0.0, arrangeSize.Width - left - right), Math.Max(0.0, arrangeSize.Height - top - bottom));
    }
  }
}
