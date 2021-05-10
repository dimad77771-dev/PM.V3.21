// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Helpers.BaseGridColumnData
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Helpers;
using DevExpress.Xpf.Data;
using System.Collections.ObjectModel;

namespace DevExpress.Xpf.Grid.Helpers
{
  public class BaseGridColumnData : BaseFilterData
  {
    private GridControl grid;

    protected DataProviderBase DataProvider
    {
      get
      {
        return this.grid.DataProviderBase;
      }
    }

    protected GridControl Grid
    {
      get
      {
        return this.grid;
      }
    }

    protected DataViewBase View
    {
      get
      {
        return this.Grid.DataView;
      }
    }

    public override int GroupCount
    {
      get
      {
        return this.Grid.ActualGroupCount;
      }
    }

    public override int SortCount
    {
      get
      {
        return this.Grid.ActualSortInfo.Count;
      }
    }

    public BaseGridColumnData(GridControl grid)
    {
      this.grid = grid;
    }

    protected override void OnFillColumns()
    {
      foreach (GridColumn column1 in (Collection<GridColumn>) this.Grid.Columns)
      {
        DataColumnInfo column2 = this.DataProvider.Columns[column1.FieldName];
        if (column2 != null)
        {
          GridDataColumnInfo columnInfo = this.CreateColumnInfo(column1);
          if (columnInfo.Required)
            this.Columns[this.GetKey(column2)] = (BaseGridColumnInfo) columnInfo;
        }
      }
    }

    protected virtual GridDataColumnInfo CreateColumnInfo(GridColumn column)
    {
      return new GridDataColumnInfo(this, column);
    }

    public override int GetSortIndex(object column)
    {
      return this.Grid.ActualSortInfo.IndexOf(this.grid.ActualSortInfo[(column as GridColumn).FieldName]);
    }

    protected internal void RaiseCustomSort(CustomColumnSortEventArgs e)
    {
      this.Grid.RaiseCustomColumnSort(e);
    }

    protected internal void RaiseCustomGroup(CustomColumnSortEventArgs e)
    {
      this.Grid.RaiseCustomColumnGroup(e);
    }

    protected internal string GetColumnDisplayText(int listSourceRowIndex, GridColumn column, object value)
    {
      object displayObject = this.View.GetDisplayObject(value, (ColumnBase) column);
      return this.Grid.RaiseCustomDisplayText(new int?(), new int?(listSourceRowIndex), (ColumnBase) column, value, displayObject != null ? displayObject.ToString() : string.Empty);
    }
  }
}
