// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Helpers.GridColumnSortData
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;

namespace DevExpress.Xpf.Grid.Helpers
{
  public class GridColumnSortData : BaseGridColumnData
  {
    public GridColumnSortData(GridControl grid)
      : base(grid)
    {
    }

    protected override GridDataColumnInfo CreateColumnInfo(GridColumn column)
    {
      GridDataColumnSortInfo dataColumnSortInfo = new GridDataColumnSortInfo(this, column);
      dataColumnSortInfo.SortMode = column.GetSortMode();
      dataColumnSortInfo.GroupInterval = column.GroupInterval;
      return (GridDataColumnInfo) dataColumnSortInfo;
    }

    public GridDataColumnSortInfo GetSortInfo(DataColumnInfo column)
    {
      return this.GetInfo(column) as GridDataColumnSortInfo;
    }

    protected override string[] GetOutlookLocalizedStrings()
    {
      return GridControlLocalizer.GetString(GridControlStringId.GridOutlookIntervals).Split(';');
    }
  }
}
