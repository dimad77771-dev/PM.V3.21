// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupColumnSummaryControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Utils;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class GroupColumnSummaryControl : Control, ISupportLoadingAnimation
  {
    public static readonly DependencyProperty IsReadyProperty = DependencyProperty.Register("IsReady", typeof (bool), typeof (GroupColumnSummaryControl), new PropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((GroupColumnSummaryControl) d).OnIsReadyChanged())));
    public static readonly DependencyProperty NormalBorderBrushProperty = DependencyProperty.Register("NormalBorderBrush", typeof (Brush), typeof (GroupColumnSummaryControl), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GroupColumnSummaryControl) d).UpdateBrushes())));
    public static readonly DependencyProperty FocusedBorderBrushProperty = DependencyProperty.Register("FocusedBorderBrush", typeof (Brush), typeof (GroupColumnSummaryControl), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GroupColumnSummaryControl) d).UpdateBrushes())));
    public static readonly DependencyProperty IsGroupRowFocusedProperty = DependencyProperty.Register("IsGroupRowFocused", typeof (bool), typeof (GroupColumnSummaryControl), new PropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((GroupColumnSummaryControl) d).UpdateBrushes())));
    private LoadingAnimationHelper loadingAnimationHelper;

    public bool IsReady
    {
      get
      {
        return (bool) this.GetValue(GroupColumnSummaryControl.IsReadyProperty);
      }
      set
      {
        this.SetValue(GroupColumnSummaryControl.IsReadyProperty, (object) value);
      }
    }

    public Brush NormalBorderBrush
    {
      get
      {
        return (Brush) this.GetValue(GroupColumnSummaryControl.NormalBorderBrushProperty);
      }
      set
      {
        this.SetValue(GroupColumnSummaryControl.NormalBorderBrushProperty, (object) value);
      }
    }

    public Brush FocusedBorderBrush
    {
      get
      {
        return (Brush) this.GetValue(GroupColumnSummaryControl.FocusedBorderBrushProperty);
      }
      set
      {
        this.SetValue(GroupColumnSummaryControl.FocusedBorderBrushProperty, (object) value);
      }
    }

    public bool IsGroupRowFocused
    {
      get
      {
        return (bool) this.GetValue(GroupColumnSummaryControl.IsGroupRowFocusedProperty);
      }
      set
      {
        this.SetValue(GroupColumnSummaryControl.IsGroupRowFocusedProperty, (object) value);
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

    public FrameworkElement Element { get; private set; }

    public DataViewBase DataView
    {
      get
      {
        return ((GridColumnData) this.DataContext).View;
      }
    }

    public bool IsGroupRow
    {
      get
      {
        return true;
      }
    }

    public GroupColumnSummaryControl()
    {
      this.SetDefaultStyleKey(typeof (GroupColumnSummaryControl));
    }

    protected virtual void UpdateBrushes()
    {
      if (!this.IsGroupRowFocused || !this.DataView.IsKeyboardFocusWithinView && this.DataView.FadeSelectionOnLostFocus)
        this.BorderBrush = this.NormalBorderBrush;
      else
        this.BorderBrush = this.FocusedBorderBrush;
    }

    private void OnIsReadyChanged()
    {
      if (this.DataContext == null)
        return;
      this.LoadingAnimationHelper.ApplyAnimation();
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.Element = this.GetTemplateChild("PART_Content") as FrameworkElement;
      if (!(this.DataContext is GridColumnData))
        return;
      this.DataView.IsKeyboardFocusWithinViewChanged += (Delegate) ((s, e) => this.UpdateBrushes());
    }
  }
}
