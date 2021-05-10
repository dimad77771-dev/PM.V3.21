// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DetailMarginVisibilityConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class DetailMarginVisibilityConverter : IValueConverter
  {
    public Side MarginSide { get; set; }

    internal static Visibility GetDetailMarginControlVisibility(IList<DetailIndent> detailIndents, Side marginSide)
    {
      if (detailIndents == null)
        return Visibility.Collapsed;
      double num = 0.0;
      switch (marginSide)
      {
        case Side.Left:
          foreach (DetailIndent detailIndent in (IEnumerable<DetailIndent>) detailIndents)
            num += detailIndent.Width;
          return num <= 0.0 ? Visibility.Collapsed : Visibility.Visible;
        case Side.Right:
          foreach (DetailIndent detailIndent in (IEnumerable<DetailIndent>) detailIndents)
            num += detailIndent.WidthRight;
          return num <= 0.0 ? Visibility.Collapsed : Visibility.Visible;
        default:
          return Visibility.Collapsed;
      }
    }

    public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (object) DetailMarginVisibilityConverter.GetDetailMarginControlVisibility(value as IList<DetailIndent>, this.MarginSide);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
