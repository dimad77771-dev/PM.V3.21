// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupBandSummaryControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System;
using System.Windows;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class GroupBandSummaryControl : GroupColumnSummaryControl
  {
    public static readonly DependencyProperty NormalForegroundProperty;
    public static readonly DependencyProperty FocusedForegroundProperty;
    public static readonly DependencyProperty NormalBackgroundProperty;
    public static readonly DependencyProperty FocusedBackgroundProperty;
    public static readonly DependencyProperty HasTopElementProperty;

    public Brush NormalForeground
    {
      get
      {
        return (Brush) this.GetValue(GroupBandSummaryControl.NormalForegroundProperty);
      }
      set
      {
        this.SetValue(GroupBandSummaryControl.NormalForegroundProperty, (object) value);
      }
    }

    public Brush FocusedForeground
    {
      get
      {
        return (Brush) this.GetValue(GroupBandSummaryControl.FocusedForegroundProperty);
      }
      set
      {
        this.SetValue(GroupBandSummaryControl.FocusedForegroundProperty, (object) value);
      }
    }

    public Brush NormalBackground
    {
      get
      {
        return (Brush) this.GetValue(GroupBandSummaryControl.NormalBackgroundProperty);
      }
      set
      {
        this.SetValue(GroupBandSummaryControl.NormalBackgroundProperty, (object) value);
      }
    }

    public Brush FocusedBackground
    {
      get
      {
        return (Brush) this.GetValue(GroupBandSummaryControl.FocusedBackgroundProperty);
      }
      set
      {
        this.SetValue(GroupBandSummaryControl.FocusedBackgroundProperty, (object) value);
      }
    }

    public bool HasTopElement
    {
      get
      {
        return (bool) this.GetValue(GroupBandSummaryControl.HasTopElementProperty);
      }
      set
      {
        this.SetValue(GroupBandSummaryControl.HasTopElementProperty, (object) value);
      }
    }

    static GroupBandSummaryControl()
    {
      Type ownerType = typeof (GroupBandSummaryControl);
      GroupBandSummaryControl.NormalForegroundProperty = DependencyProperty.Register("NormalForeground", typeof (Brush), ownerType, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GroupColumnSummaryControl) d).UpdateBrushes())));
      GroupBandSummaryControl.FocusedForegroundProperty = DependencyProperty.Register("FocusedForeground", typeof (Brush), ownerType, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GroupColumnSummaryControl) d).UpdateBrushes())));
      GroupBandSummaryControl.NormalBackgroundProperty = DependencyProperty.Register("NormalBackground", typeof (Brush), ownerType, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GroupColumnSummaryControl) d).UpdateBrushes())));
      GroupBandSummaryControl.FocusedBackgroundProperty = DependencyProperty.Register("FocusedBackground", typeof (Brush), ownerType, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GroupColumnSummaryControl) d).UpdateBrushes())));
      GroupBandSummaryControl.HasTopElementProperty = DependencyProperty.Register("HasTopElement", typeof (bool), ownerType, new PropertyMetadata((object) false));
    }

    public GroupBandSummaryControl()
    {
      this.SetDefaultStyleKey(typeof (GroupBandSummaryControl));
    }

    protected override void UpdateBrushes()
    {
      base.UpdateBrushes();
      if (this.IsGroupRowFocused)
      {
        this.Foreground = this.FocusedForeground;
        this.Background = this.FocusedBackground;
      }
      else
      {
        this.Foreground = this.NormalForeground;
        this.Background = this.NormalBackground;
      }
    }
  }
}
