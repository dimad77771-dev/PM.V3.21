// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.HorizontalScrollBarLeftCornerBorderThicknessConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class HorizontalScrollBarLeftCornerBorderThicknessConverter : IMultiValueConverter
  {
    public bool UseVerticalBorder { get; set; }

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (!(values[0] is bool) || !(values[1] is double) || (!(values[2] is bool) || !(values[3] is bool)))
        return (object) new Thickness();
      return (object) new Thickness(0.0, !this.UseVerticalBorder || (bool) values[0] ? 0.0 : 1.0, !this.UseVerticalBorder && (bool) values[0] || !(bool) values[2] && (double) values[1] != 0.0 && !(bool) values[3] ? 0.0 : 1.0, 0.0);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
