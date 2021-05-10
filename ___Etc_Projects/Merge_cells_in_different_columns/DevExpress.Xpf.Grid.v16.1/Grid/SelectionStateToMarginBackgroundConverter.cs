// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.SelectionStateToMarginBackgroundConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class SelectionStateToMarginBackgroundConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (values == null || values.Length != 2)
        return (object) new Thickness(0.0);
      if (!(values[0] is SelectionState))
        return (object) new Thickness(0.0);
      switch ((SelectionState) values[0])
      {
        case SelectionState.None:
        case SelectionState.CellMerge:
          return (object) new Thickness(0.0);
        case SelectionState.Focused:
        case SelectionState.Selected:
        case SelectionState.FocusedAndSelected:
          if (values[1] is CardView)
            return (object) new Thickness(0.0, -1.0, 0.0, -1.0);
          return (object) new Thickness(0.0, 0.0, 0.0, -1.0);
        default:
          return (object) new Thickness(0.0);
      }
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
