﻿// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.AddNewRowVisibleConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class AddNewRowVisibleConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is TableView)
        return (object) Visibility.Visible;
      return (object) Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (object) !(bool) value;
    }
  }
}
