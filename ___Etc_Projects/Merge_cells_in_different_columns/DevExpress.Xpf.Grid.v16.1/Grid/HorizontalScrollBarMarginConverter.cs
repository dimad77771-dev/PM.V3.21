// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.HorizontalScrollBarMarginConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class HorizontalScrollBarMarginConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (!(values[0] is bool) || !(values[1] is double) || (!(values[2] is Thickness) || !(values[3] is bool)))
        return (object) new Thickness();
      Thickness thickness = (Thickness) values[2];
      if ((bool) values[3] || !(bool) values[0])
        return (object) thickness;
      return (object) new Thickness(thickness.Left + (double) values[1], thickness.Top, thickness.Right, thickness.Bottom);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
