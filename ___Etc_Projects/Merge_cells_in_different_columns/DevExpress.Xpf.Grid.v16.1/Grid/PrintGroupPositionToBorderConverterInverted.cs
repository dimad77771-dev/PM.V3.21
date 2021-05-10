// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintGroupPositionToBorderConverterInverted
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class PrintGroupPositionToBorderConverterInverted : IValueConverter
  {
    public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      PrintGroupRowCellInfo groupRowCellInfo = value as PrintGroupRowCellInfo;
      if (groupRowCellInfo == null)
        return (object) new Thickness(0.0);
      int num1 = !groupRowCellInfo.IsLeftGroupSummaryValueEmpty ? 1 : 0;
      int num2 = !groupRowCellInfo.IsRightGroupSummaryValueEmpty ? 1 : 0;
      switch (groupRowCellInfo.Position)
      {
        case PrintGroupCellPosition.Default:
          return (object) new Thickness((double) num1, 0.0, (double) num2, 1.0);
        case PrintGroupCellPosition.None:
          return (object) new Thickness(0.0, 0.0, 0.0, 1.0);
        case PrintGroupCellPosition.Last:
          return (object) new Thickness((double) num1, 0.0, 1.0, 1.0);
        case PrintGroupCellPosition.Separator:
          return (object) new Thickness(0.0, 0.0, (double) num2, 1.0);
        default:
          return (object) null;
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
