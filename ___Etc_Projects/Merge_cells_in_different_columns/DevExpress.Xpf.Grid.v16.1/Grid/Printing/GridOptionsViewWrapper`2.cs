// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GridOptionsViewWrapper`2
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.XtraExport.Helpers;

namespace DevExpress.Xpf.Grid.Printing
{
  internal class GridOptionsViewWrapper<TCol, TRow> : IGridOptionsView where TCol : ColumnWrapper where TRow : IRowBase
  {
    protected DataViewExportHelperBase<TCol, TRow> helper;

    bool IGridOptionsView.ShowFooter
    {
      get
      {
        return this.helper.ShowFooter;
      }
    }

    bool IGridOptionsView.ShowGroupFooter
    {
      get
      {
        return this.helper.ShowGroupFooter;
      }
    }

    bool IGridOptionsView.ColumnAutoWidth
    {
      get
      {
        return this.helper.ColumnAutoWidth;
      }
    }

    bool IGridOptionsView.ShowGroupedColumns
    {
      get
      {
        return this.helper.ShowGroupedColumns;
      }
    }

    public GridOptionsViewWrapper(DataViewExportHelperBase<TCol, TRow> helper)
    {
      this.helper = helper;
    }
  }
}
