// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.HorizontalScrollBarRowSpanConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class HorizontalScrollBarRowSpanConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (!(values[0] is ScrollBarMode) || !(values[1] is bool))
        return (object) 3;
      if ((bool) values[1])
        return (object) ((ScrollBarMode) values[0] == ScrollBarMode.TouchOverlap ? 2 : 1);
      return (object) ((ScrollBarMode) values[0] == ScrollBarMode.TouchOverlap ? 3 : 1);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
