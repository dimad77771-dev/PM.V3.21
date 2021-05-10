// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Helpers.GridDataColumnInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Helpers;

namespace DevExpress.Xpf.Grid.Helpers
{
  public class GridDataColumnInfo : BaseGridColumnInfo
  {
    private CustomColumnSortEventArgs sortArgs;

    protected BaseGridColumnData Data
    {
      get
      {
        return base.Data as BaseGridColumnData;
      }
    }

    protected CustomColumnSortEventArgs SortArgs
    {
      get
      {
        return this.sortArgs;
      }
    }

    public GridColumn Column
    {
      get
      {
        return base.Column as GridColumn;
      }
    }

    public GridDataColumnInfo(BaseGridColumnData data, GridColumn column)
      : base((BaseFilterData) data, (object) column)
    {
      this.sortArgs = new CustomColumnSortEventArgs(column, -1, -1, (object) null, (object) null, ColumnSortOrder.Ascending);
    }

    public override string GetDisplayText(int listSourceIndex, object val)
    {
      return this.Data.GetColumnDisplayText(listSourceIndex, this.Column, val);
    }

    protected override int? RaiseCustomGroup(int listSourceRow1, int listSourceRow2, object value1, object value2, ColumnSortOrder columnSortOrder)
    {
      this.SortArgs.SetArgs(listSourceRow1, listSourceRow2, value1, value2, ColumnSortOrder.None);
      this.Data.RaiseCustomGroup(this.SortArgs);
      return this.SortArgs.GetSortResult();
    }

    protected override int? RaiseCustomSort(int listSourceRow1, int listSourceRow2, object value1, object value2, ColumnSortOrder sortOrder)
    {
      this.SortArgs.SetArgs(listSourceRow1, listSourceRow2, value1, value2, sortOrder);
      this.Data.RaiseCustomSort(this.SortArgs);
      return this.SortArgs.GetSortResult();
    }
  }
}
