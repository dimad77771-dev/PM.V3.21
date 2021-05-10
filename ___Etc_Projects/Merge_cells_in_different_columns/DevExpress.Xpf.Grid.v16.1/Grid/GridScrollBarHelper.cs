// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridScrollBarHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class GridScrollBarHelper
  {
    public static readonly DependencyProperty HasFixedRightColumnsProperty = DependencyProperty.RegisterAttached("HasFixedRightColumns", typeof (bool), typeof (GridScrollBarHelper), new PropertyMetadata((object) false));
    public static readonly DependencyProperty ExtendScrollBarToFixedColumnsProperty = DependencyProperty.RegisterAttached("ExtendScrollBarToFixedColumns", typeof (bool), typeof (GridScrollBarHelper), new PropertyMetadata((object) false));

    public static bool GetHasFixedRightColumns(DependencyObject obj)
    {
      return (bool) obj.GetValue(GridScrollBarHelper.HasFixedRightColumnsProperty);
    }

    public static void SetHasFixedRightColumns(DependencyObject obj, bool value)
    {
      obj.SetValue(GridScrollBarHelper.HasFixedRightColumnsProperty, (object) value);
    }

    public static bool GetExtendScrollBarToFixedColumns(DependencyObject obj)
    {
      return (bool) obj.GetValue(GridScrollBarHelper.ExtendScrollBarToFixedColumnsProperty);
    }

    public static void SetExtendScrollBarToFixedColumns(DependencyObject obj, bool value)
    {
      obj.SetValue(GridScrollBarHelper.ExtendScrollBarToFixedColumnsProperty, (object) value);
    }
  }
}
