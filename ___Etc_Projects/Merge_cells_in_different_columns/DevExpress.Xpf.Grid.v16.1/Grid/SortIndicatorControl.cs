// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.SortIndicatorControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class SortIndicatorControl : Control
  {
    public static readonly DependencyProperty SortOrderProperty;

    public ListSortDirection SortOrder
    {
      get
      {
        return (ListSortDirection) this.GetValue(SortIndicatorControl.SortOrderProperty);
      }
      set
      {
        this.SetValue(SortIndicatorControl.SortOrderProperty, (object) value);
      }
    }

    static SortIndicatorControl()
    {
      Type type = typeof (SortIndicatorControl);
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(type, (PropertyMetadata) new FrameworkPropertyMetadata((object) type));
      SortIndicatorControl.SortOrderProperty = DependencyProperty.Register("SortOrder", typeof (ListSortDirection), type, new PropertyMetadata((object) ListSortDirection.Ascending));
    }
  }
}
