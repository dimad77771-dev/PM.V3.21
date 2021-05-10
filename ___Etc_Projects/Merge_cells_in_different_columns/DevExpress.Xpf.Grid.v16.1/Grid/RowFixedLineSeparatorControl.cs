// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.RowFixedLineSeparatorControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class RowFixedLineSeparatorControl : Control
  {
    public static readonly DependencyProperty ShowVerticalLinesProperty = DependencyProperty.Register("ShowVerticalLines", typeof (bool), typeof (RowFixedLineSeparatorControl), new PropertyMetadata((object) true));
    private readonly Func<TableViewBehavior, IList<ColumnBase>> getFixedColumnsFunc;
    private readonly Func<BandsLayoutBase, IList<BandBase>> getFixedBandsFunc;

    public bool ShowVerticalLines
    {
      get
      {
        return (bool) this.GetValue(RowFixedLineSeparatorControl.ShowVerticalLinesProperty);
      }
      set
      {
        this.SetValue(RowFixedLineSeparatorControl.ShowVerticalLinesProperty, (object) value);
      }
    }

    static RowFixedLineSeparatorControl()
    {
      Type forType = typeof (RowFixedLineSeparatorControl);
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, (PropertyMetadata) new FrameworkPropertyMetadata((object) forType));
    }

    public RowFixedLineSeparatorControl(Func<TableViewBehavior, IList<ColumnBase>> getFixedColumnsFunc, Func<BandsLayoutBase, IList<BandBase>> getFixedBandsFunc)
    {
      this.getFixedColumnsFunc = getFixedColumnsFunc;
      this.getFixedBandsFunc = getFixedBandsFunc;
    }

    internal void UpdateVisibility(DataControlBase dataControl)
    {
      IList list = dataControl.BandsLayoutCore != null ? (IList) this.getFixedBandsFunc(dataControl.BandsLayoutCore) : (IList) this.getFixedColumnsFunc((TableViewBehavior) dataControl.viewCore.ViewBehavior);
      this.Visibility = list == null || list.Count <= 0 ? Visibility.Collapsed : Visibility.Visible;
    }
  }
}
