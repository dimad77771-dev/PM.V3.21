// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.ValidationErrorToIsEnabledConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Globalization;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class ValidationErrorToIsEnabledConverter : IValueConverter
  {
    object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value == null)
        return (object) true;
      return (object) (!(value is RowValidationError) || !((RowValidationError) value).IsCellError);
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
