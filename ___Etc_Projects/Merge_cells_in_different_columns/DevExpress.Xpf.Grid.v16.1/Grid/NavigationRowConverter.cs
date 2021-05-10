// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.NavigationRowConverter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Globalization;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class NavigationRowConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (values == null || values.Length == 0 || values[0] == null)
        return (object) null;
      DataViewBase dataViewBase = values[0] as DataViewBase;
      TableView tableView = dataViewBase as TableView;
      if (dataViewBase == null || dataViewBase.DataControl == null)
        return (object) null;
      string format = dataViewBase.LocalizationDescriptor.GetValue("NavigationRecord");
      int visibleRowCount = dataViewBase.DataControl.VisibleRowCount;
      if (tableView != null && tableView.ActualNewItemRowPosition == NewItemRowPosition.Bottom && visibleRowCount > 0)
        --visibleRowCount;
      int focusedRowHandle = dataViewBase.FocusedRowHandle;
      switch (focusedRowHandle)
      {
        case int.MinValue:
        case -999997:
          return (object) string.Format(format, (object) 0, (object) visibleRowCount);
        default:
          if (visibleRowCount != 0)
          {
            if (focusedRowHandle == -2147483647)
              return (object) string.Format(format, (object) (visibleRowCount + 1), (object) (visibleRowCount + 1));
            int indexByHandleCore = dataViewBase.DataControl.GetRowVisibleIndexByHandleCore(focusedRowHandle);
            return (object) string.Format(format, (object) (indexByHandleCore + 1), (object) visibleRowCount);
          }
          goto case -2147483648;
      }
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
