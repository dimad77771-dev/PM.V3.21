// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridColumnToGridColumnDataConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Globalization;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class GridColumnToGridColumnDataConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      ColumnBase column = value as ColumnBase;
      if (column != null && column.View != null && column.View.HeadersData != null)
        return (object) column.View.HeadersData.GetCellDataByColumn(column);
      return (object) null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
