// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.HorizontalScrollBarIndicatorVisibilityConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class HorizontalScrollBarIndicatorVisibilityConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (!(values[0] is bool) || !(values[1] is bool) || (!(values[2] is double) || !(values[3] is bool)) || (!(values[4] is bool) || !(values[5] is ScrollBarMode)))
        return (object) Visibility.Collapsed;
      if (!(bool) values[0])
        return (object) Visibility.Collapsed;
      if (!(bool) values[1])
        return (object) Visibility.Visible;
      return (object) (Visibility) ((bool) values[3] || (double) values[2] == 0.0 || (bool) values[4] && (ScrollBarMode) values[5] != ScrollBarMode.TouchOverlap ? 2 : 0);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
