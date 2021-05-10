// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Helpers.GridDataColumnSortInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Utils;
using DevExpress.XtraGrid;

namespace DevExpress.Xpf.Grid.Helpers
{
  public class GridDataColumnSortInfo : GridDataColumnInfo
  {
    public override bool Required
    {
      get
      {
        return base.Required || this.SortMode == ColumnSortMode.Custom || this.SortMode == ColumnSortMode.DisplayText || this.GroupInterval != ColumnGroupInterval.Default && this.GroupInterval != ColumnGroupInterval.Value;
      }
      set
      {
        base.Required = value;
      }
    }

    public GridDataColumnSortInfo(GridColumnSortData data, GridColumn column)
      : base((BaseGridColumnData) data, column)
    {
    }

    public string GetColumnGroupFormatString()
    {
      FormatInfo columnGroupFormat = this.GetColumnGroupFormat();
      if (columnGroupFormat == null)
        return string.Empty;
      return columnGroupFormat.FormatString;
    }
  }
}
