// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.VerticalScrollBarRowSpanCardViewConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class VerticalScrollBarRowSpanCardViewConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (!(values[0] is ScrollBarMode) || !(values[1] is Orientation?))
        return (object) 1;
      if ((ScrollBarMode) values[0] != ScrollBarMode.TouchOverlap || values[1] == null)
        return (object) 1;
      return (object) (((Orientation?) values[1]).Value == Orientation.Vertical ? 2 : 1);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
