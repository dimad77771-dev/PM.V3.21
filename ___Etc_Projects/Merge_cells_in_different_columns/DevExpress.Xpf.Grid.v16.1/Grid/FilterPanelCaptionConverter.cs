// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.FilterPanelCaptionConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Globalization;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class FilterPanelCaptionConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value == null)
        return (object) null;
      return (object) string.Format(GridControlLocalizer.GetString(GridControlStringId.FilterPanelCaptionFormatStringForMasterDetail), (object) value.ToString());
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
