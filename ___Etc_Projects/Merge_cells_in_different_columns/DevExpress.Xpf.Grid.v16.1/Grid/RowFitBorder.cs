// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.RowFitBorder
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class RowFitBorder : Border
  {
    static RowFitBorder()
    {
      Type forType = typeof (RowFitBorder);
      Border.BorderThicknessProperty.OverrideMetadata(forType, (PropertyMetadata) new FrameworkPropertyMetadata((object) new Thickness(1.0, 0.0, 0.0, 0.0)));
    }

    protected override Size MeasureOverride(Size constraint)
    {
      return new Size(0.0, base.MeasureOverride(constraint).Height);
    }
  }
}
