// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintHeaderBorderConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class PrintHeaderBorderConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      PrintCellInfo printCellInfo = value as PrintCellInfo;
      if (printCellInfo == null)
        return (object) new Thickness();
      int num = parameter is int ? (int) parameter : 1;
      double top = !printCellInfo.IsColumnHeaderVisible || printCellInfo.HasTopElement ? 0.0 : (double) num;
      if (printCellInfo.IsRight)
        return (object) new Thickness((double) num, top, (double) num, (double) num);
      return (object) new Thickness((double) num, top, 0.0, (double) num);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
