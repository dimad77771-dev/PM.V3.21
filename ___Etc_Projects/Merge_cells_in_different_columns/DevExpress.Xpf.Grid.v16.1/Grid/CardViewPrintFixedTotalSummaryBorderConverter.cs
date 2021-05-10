// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CardViewPrintFixedTotalSummaryBorderConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class CardViewPrintFixedTotalSummaryBorderConverter : IMultiValueConverter
  {
    public bool IsLeft { get; set; }

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (values.Length != 2 || !(values[0] is string) || !(values[1] is bool))
        return (object) new Thickness();
      string @string = values[0].ToString();
      bool flag = (bool) values[1];
      switch (this.IsLeft)
      {
        case true:
          return (object) new Thickness(1.0, flag ? 1.0 : 0.0, string.IsNullOrWhiteSpace(@string) ? 1.0 : 0.0, 1.0);
        default:
          return (object) new Thickness(string.IsNullOrWhiteSpace(@string) ? 1.0 : 0.0, flag ? 1.0 : 0.0, 1.0, 1.0);
      }
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
