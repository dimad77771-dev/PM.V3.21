// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupRowOuterMarginConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class GroupRowOuterMarginConverter : IMultiValueConverter
  {
    public Thickness ExpandedMargin { get; set; }

    public Thickness ExpandedIsLastMargin { get; set; }

    public Thickness CollapsedMargin { get; set; }

    public Thickness CollapsedIsLastMargin { get; set; }

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (values.Length != 2 || !(values[0] is bool) || !(values[1] is bool))
        return (object) new Thickness();
      bool flag1 = (bool) values[0];
      bool flag2 = (bool) values[1];
      if (flag1 && flag2)
        return (object) this.ExpandedIsLastMargin;
      if (!flag1 && flag2)
        return (object) this.ExpandedMargin;
      if (flag1 && !flag2)
        return (object) this.CollapsedIsLastMargin;
      if (!flag1 && !flag2)
        return (object) this.CollapsedMargin;
      return (object) new Thickness();
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
