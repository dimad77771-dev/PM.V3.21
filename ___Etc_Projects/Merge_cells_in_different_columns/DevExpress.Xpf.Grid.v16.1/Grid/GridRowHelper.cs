// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridRowHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows.Controls;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  internal static class GridRowHelper
  {
    public static Control CreateRowOffsetContent(RowData rowData, Control backgroundSource)
    {
      Control control = (Control) null;
      if (rowData.View.IsRowMarginControlVisible)
      {
        RowMarginControl rowMarginControl = new RowMarginControl();
        rowMarginControl.SetBinding(RowOffsetBase.NextRowLevelProperty, (BindingBase) new Binding("NextRowLevel"));
        control = (Control) rowMarginControl;
      }
      else if (rowData.Level != 0)
        control = (Control) new RowOffsetPresenter();
      if (control != null)
        control.SetBinding(Control.BorderBrushProperty, (BindingBase) new Binding("BorderBrush")
        {
          Source = (object) backgroundSource
        });
      return control;
    }
  }
}
