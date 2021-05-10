// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CardViewPrintGroupPositionToBorderConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class CardViewPrintGroupPositionToBorderConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (values.Length != 3 || !(values[0] is PrintGroupRowCellInfo) || (!(values[1] is bool) || !(values[2] is bool)))
        return (object) new Thickness(0.0);
      PrintGroupRowCellInfo groupRowCellInfo = (PrintGroupRowCellInfo) values[0];
      bool flag = (bool) values[1];
      int num1 = (bool) values[2] ? 1 : 0;
      int num2 = flag ? 0 : 1;
      int num3 = groupRowCellInfo.IsLeftGroupSummaryValueEmpty ? 1 : 0;
      int num4 = groupRowCellInfo.IsRightGroupSummaryValueEmpty ? 1 : 0;
      switch (groupRowCellInfo.Position)
      {
        case PrintGroupCellPosition.Default:
          return (object) new Thickness((double) num3, (double) num2, (double) num4, (double) num1);
        case PrintGroupCellPosition.None:
          return (object) new Thickness(0.0, (double) num2, 0.0, (double) num1);
        case PrintGroupCellPosition.Last:
          return (object) new Thickness((double) num3, (double) num2, 1.0, (double) num1);
        case PrintGroupCellPosition.Separator:
          return (object) new Thickness(0.0, (double) num2, (double) num4, (double) num1);
        default:
          return (object) null;
      }
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
