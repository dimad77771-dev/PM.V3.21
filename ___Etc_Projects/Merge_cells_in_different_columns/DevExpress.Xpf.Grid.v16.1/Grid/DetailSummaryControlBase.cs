// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DetailSummaryControlBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class DetailSummaryControlBase : DetailRowControlBase
  {
    public static readonly DependencyProperty SummaryDetailLevelProperty = DependencyPropertyManager.RegisterAttached("SummaryDetailLevel", typeof (int), typeof (DetailSummaryControlBase), new PropertyMetadata((object) -1));

    public static int GetSummaryDetailLevel(DependencyObject element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      return (int) element.GetValue(DetailSummaryControlBase.SummaryDetailLevelProperty);
    }

    public static void SetSummaryDetailLevel(DependencyObject element, int value)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      element.SetValue(DetailSummaryControlBase.SummaryDetailLevelProperty, (object) value);
    }
  }
}
