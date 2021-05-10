// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.FixedTotalSummaryLeftVisibilityConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class FixedTotalSummaryLeftVisibilityConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (!(values[0] is string) || !(values[1] is string))
        return (object) Visibility.Visible;
      if (!string.IsNullOrEmpty(values[0] as string) || string.IsNullOrEmpty(values[1] as string))
        return (object) Visibility.Visible;
      return (object) Visibility.Collapsed;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
