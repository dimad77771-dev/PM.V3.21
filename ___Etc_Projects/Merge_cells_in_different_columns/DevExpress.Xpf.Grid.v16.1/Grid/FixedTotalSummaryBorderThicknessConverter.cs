// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.FixedTotalSummaryBorderThicknessConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class FixedTotalSummaryBorderThicknessConverter : IMultiValueConverter
  {
    public Thickness BorderThickness { get; set; }

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (values.Length == 3 && values[1] is string && values[2] is string)
      {
        string str1 = (string) values[1];
        string str2 = (string) values[2];
        string @string = values[0].ToString();
        if (@string == "PART_EditLeft")
          return (object) (str2 == string.Empty ? new Thickness(this.BorderThickness.Left, 0.0, this.BorderThickness.Right, this.BorderThickness.Bottom) : new Thickness(this.BorderThickness.Left, 0.0, 0.0, this.BorderThickness.Bottom));
        if (@string == "PART_EditRight")
          return (object) (str1 == string.Empty ? new Thickness(this.BorderThickness.Left, 0.0, this.BorderThickness.Right, this.BorderThickness.Bottom) : new Thickness(0.0, 0.0, this.BorderThickness.Right, this.BorderThickness.Bottom));
      }
      return (object) new Thickness();
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
