// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.CheckBoxColumnTemplateSelector
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Data;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid.Native
{
  internal class CheckBoxColumnTemplateSelector : DataTemplateSelector
  {
    private readonly DataTemplate emptyTemplate;

    public CheckBoxColumnTemplateSelector()
    {
      this.emptyTemplate = CheckBoxColumnTemplateSelector.CreateEmptyTemplate();
    }

    private static DataTemplate CreateEmptyTemplate()
    {
      DataTemplate dataTemplate = new DataTemplate();
      dataTemplate.Seal();
      return dataTemplate;
    }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      if (!this.IsAdditionalRowCellData(item as GridCellData))
        return (DataTemplate) null;
      return this.emptyTemplate;
    }

    private bool IsAdditionalRowCellData(GridCellData data)
    {
      return data.With<GridCellData, RowData>((Func<GridCellData, RowData>) (x => x.RowData)).With<RowData, RowHandle>((Func<RowData, RowHandle>) (y => y.RowHandle)).Return<RowHandle, int>((Func<RowHandle, int>) (z => z.Value), (Func<int>) (() => int.MinValue)) == -2147483647;
    }
  }
}
