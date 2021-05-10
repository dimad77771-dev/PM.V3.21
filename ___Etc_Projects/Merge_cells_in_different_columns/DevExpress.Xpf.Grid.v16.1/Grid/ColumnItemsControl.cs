// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.ColumnItemsControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class ColumnItemsControl : CachedItemsControl
  {
    protected override FrameworkElement CreateChild(object item)
    {
      object obj = (object) null;
      GridColumnBase gridColumnBase = item as GridColumnBase;
      if (gridColumnBase != null && gridColumnBase.View != null)
        obj = (object) new GridColumnData((ColumnsRowDataBase) gridColumnBase.View.HeadersData)
        {
          Column = (ColumnBase) gridColumnBase
        };
      BandBase bandBase = item as BandBase;
      if (bandBase != null)
        obj = (object) new BandData()
        {
          Column = (BaseColumn) bandBase
        };
      if (obj == null)
        return base.CreateChild(item);
      return (FrameworkElement) new ContentPresenter() { ContentTemplate = this.ItemTemplate, Content = obj };
    }
  }
}
