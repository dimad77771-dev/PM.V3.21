// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.FixedNoneBandsColumnBorder
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm.Native;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class FixedNoneBandsColumnBorder : Border
  {
    protected override Geometry GetLayoutClip(Size layoutSlotSize)
    {
      return this.GetLayoutClipInternal(layoutSlotSize);
    }

    internal Geometry GetLayoutClipInternal(Size layoutSlotSize)
    {
      double mergeMargin = this.GetMergeMargin();
      return (Geometry) new RectangleGeometry(new Rect(0.0, -mergeMargin, this.RenderSize.Width, this.RenderSize.Height + mergeMargin));
    }

    private double GetMergeMargin()
    {
      return (this.DataContext as RowDataBase).With<RowDataBase, DataViewBase>((Func<RowDataBase, DataViewBase>) (r => r.View)).With<DataViewBase, DataControlBase>((Func<DataViewBase, DataControlBase>) (v => v.DataControl)).Return<DataControlBase, double>((Func<DataControlBase, double>) (dc => dc.ActualHeight), (Func<double>) (() => 0.0));
    }
  }
}
