// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintGroupSummaryBorderConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class PrintGroupSummaryBorderConverter : IValueConverter
  {
    public Thickness BorderThickness { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      PrintGroupRowCellInfo groupRowCellInfo = value as PrintGroupRowCellInfo;
      if (groupRowCellInfo == null)
        return (object) new Thickness(0.0);
      return (object) new Thickness(groupRowCellInfo.HasLeftBorder ? this.BorderThickness.Left : 0.0, this.BorderThickness.Top, groupRowCellInfo.HasRightBorder ? this.BorderThickness.Right : 0.0, this.BorderThickness.Bottom);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
