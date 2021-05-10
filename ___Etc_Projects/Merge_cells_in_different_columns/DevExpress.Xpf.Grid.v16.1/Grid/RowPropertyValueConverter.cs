// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.RowPropertyValueConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Data;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class RowPropertyValueConverter : IValueConverter
  {
    public IValueConverter InnerConverter { get; set; }

    object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      object obj = this.ConvertCore(value, parameter);
      if (this.InnerConverter != null)
        obj = this.InnerConverter.Convert(obj, targetType, parameter, culture);
      return obj;
    }

    private object ConvertCore(object value, object parameter)
    {
      RowTypeDescriptorBase typeDescriptorBase = value as RowTypeDescriptorBase;
      if (typeDescriptorBase == null)
        return (object) null;
      return typeDescriptorBase.GetValue(parameter as string);
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
