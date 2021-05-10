// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DataPresenterManipulation
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Windows;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid
{
  public abstract class DataPresenterManipulation : GridDataPresenterBase
  {
    internal Point accumulator;
    internal bool deltaOccured;
    internal bool isHeaderPanel;

    public DataPresenterManipulation()
    {
      this.IsManipulationEnabled = true;
    }

    internal void StartManipulation()
    {
      this.deltaOccured = false;
    }

    internal virtual void DoManipulation(Point translation, Point totalTranslation)
    {
      if (this.View == null)
        this.deltaOccured = false;
      else if (!this.deltaOccured && Math.Abs(totalTranslation.X) < this.View.TouchScrollThreshold && Math.Abs(totalTranslation.Y) < this.View.TouchScrollThreshold)
      {
        PointHelper.Offset(ref this.accumulator, translation.X, translation.Y);
      }
      else
      {
        this.deltaOccured = true;
        this.View.ScrollAnimationLocker.Lock();
        try
        {
          DataViewBehavior viewBehavior = this.View.ViewBehavior;
          if (viewBehavior == null)
            return;
          Size firstElementSize = this.GetFirstElementSize();
          if (Size.Empty == firstElementSize || firstElementSize.Height < 1.0)
            return;
          PointHelper.Offset(ref translation, this.accumulator.X, this.accumulator.Y);
          double num = this.DefineDelta(translation, firstElementSize);
          if (!this.View.ViewBehavior.AllowPerPixelScrolling)
          {
            double delta = Math.Truncate(num);
            this.ChangeOffset(viewBehavior, delta, translation);
            this.accumulator = this.GetAccumulator(this.GetTranslation(num - delta, firstElementSize));
          }
          else
          {
            this.ChangeOffset(viewBehavior, num, translation);
            this.accumulator = this.GetAccumulator(0.0);
          }
        }
        finally
        {
          this.View.ScrollAnimationLocker.Unlock();
        }
      }
    }

    protected virtual void ChangeOffset(DataViewBehavior behavior, double delta, Point translation)
    {
      behavior.ChangeVerticalOffsetBy(-delta);
      behavior.ChangeHorizontalOffsetBy(-translation.X);
    }

    protected virtual double DefineDelta(Point translation, Size firstElementSize)
    {
      return translation.Y / firstElementSize.Height;
    }

    protected virtual Size GetFirstElementSize()
    {
      FrameworkElement firstVisibleRow = this.GetFirstVisibleRow();
      if (firstVisibleRow == null)
        return Size.Empty;
      return new Size(firstVisibleRow.ActualWidth, firstVisibleRow.ActualHeight);
    }

    protected virtual double GetTranslation(double delta, Size firstElementSize)
    {
      return delta * firstElementSize.Height;
    }

    protected virtual Point GetAccumulator(double translation)
    {
      return new Point(0.0, translation);
    }

    protected internal override void SetManipulation(bool isColumnFilterOpened)
    {
      this.IsManipulationEnabled = !isColumnFilterOpened;
    }

    internal void OnManipulationDelta(Action onManipulate, Action onCancelManipulate)
    {
      if (this.isHeaderPanel)
      {
        this.isHeaderPanel = false;
        onCancelManipulate();
      }
      else
        onManipulate();
    }

    protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
    {
      this.OnManipulationDelta((Action) (() =>
      {
        this.DoManipulation(new Point(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y), new Point(e.CumulativeManipulation.Translation.X, e.CumulativeManipulation.Translation.Y));
        e.Handled = true;
      }), (Action) (() => e.Cancel()));
    }

    protected override void OnManipulationCompleted(ManipulationCompletedEventArgs e)
    {
    }

    internal void OnStylusDown(object source)
    {
      if (source is DetailColumnHeadersControl)
        this.isHeaderPanel = true;
      this.StartManipulation();
    }

    protected override void OnStylusDown(StylusDownEventArgs e)
    {
      this.OnStylusDown(e.Source);
    }

    protected override void OnStylusUp(StylusEventArgs e)
    {
      if (this.deltaOccured)
        return;
      this.View.ProcessStylusUp((DependencyObject) e.OriginalSource);
    }

    protected override void OnManipulationInertiaStarting(ManipulationInertiaStartingEventArgs e)
    {
      e.TranslationBehavior.DesiredDeceleration = 0.001;
      e.Handled = true;
      base.OnManipulationInertiaStarting(e);
      if (this.deltaOccured)
        return;
      e.Cancel();
    }
  }
}
