// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DisplayedNavigationConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class DisplayedNavigationConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      NavigatorButtonType navigatorButtonType1 = (NavigatorButtonType) Enum.Parse(typeof (NavigatorButtonType), parameter.ToString());
      NavigatorButtonType navigatorButtonType2 = (NavigatorButtonType) value;
      switch (navigatorButtonType1)
      {
        case NavigatorButtonType.DeleteFocusedRow:
          return (object) (Visibility) ((navigatorButtonType2 & NavigatorButtonType.DeleteFocusedRow) == NavigatorButtonType.DeleteFocusedRow ? 0 : 2);
        case NavigatorButtonType.EditFocusedRow:
          return (object) (Visibility) ((navigatorButtonType2 & NavigatorButtonType.EditFocusedRow) == NavigatorButtonType.EditFocusedRow ? 0 : 2);
        case NavigatorButtonType.MoveLastRow:
          return (object) (Visibility) ((navigatorButtonType2 & NavigatorButtonType.MoveLastRow) == NavigatorButtonType.MoveLastRow ? 0 : 2);
        case NavigatorButtonType.AddNewRow:
          return (object) (Visibility) ((navigatorButtonType2 & NavigatorButtonType.AddNewRow) == NavigatorButtonType.AddNewRow ? 0 : 2);
        case NavigatorButtonType.MoveFirstRow:
          return (object) (Visibility) ((navigatorButtonType2 & NavigatorButtonType.MoveFirstRow) == NavigatorButtonType.MoveFirstRow ? 0 : 2);
        case NavigatorButtonType.MovePrevPage:
          return (object) (Visibility) ((navigatorButtonType2 & NavigatorButtonType.MovePrevPage) == NavigatorButtonType.MovePrevPage ? 0 : 2);
        case NavigatorButtonType.MovePrevRow:
          return (object) (Visibility) ((navigatorButtonType2 & NavigatorButtonType.MovePrevRow) == NavigatorButtonType.MovePrevRow ? 0 : 2);
        case NavigatorButtonType.MoveNextRow:
          return (object) (Visibility) ((navigatorButtonType2 & NavigatorButtonType.MoveNextRow) == NavigatorButtonType.MoveNextRow ? 0 : 2);
        case NavigatorButtonType.MoveNextPage:
          return (object) (Visibility) ((navigatorButtonType2 & NavigatorButtonType.MoveNextPage) == NavigatorButtonType.MoveNextPage ? 0 : 2);
        default:
          throw new NotImplementedException();
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
