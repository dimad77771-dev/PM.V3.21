// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridColumnDataEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Data;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.GridControl.CustomUnboundColumnData" /> event.
  /// </para>
  ///             </summary>
  public class GridColumnDataEventArgs : ColumnDataEventArgsBase
  {
    private readonly GridControl gridControl;
    private readonly int listSourceRow;

    /// <summary>
    ///                 <para>Gets the index of the record in a data source to which the processed row corresponds.
    /// </para>
    ///             </summary>
    /// <value>A zero-based integer value that specifies the index of the record in a data source to which the processed row corresponds.
    /// </value>
    public int ListSourceRowIndex
    {
      get
      {
        return this.listSourceRow;
      }
    }

    /// <summary>
    ///                 <para>Gets the unbound column currently being processed.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the unbound column.
    /// </value>
    public GridColumn Column
    {
      get
      {
        return (GridColumn) base.Column;
      }
    }

    /// <summary>
    ///                 <para>Gets the grid control that raised the event.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridControl" /> object that raised the event.
    /// </value>
    public GridControl Source
    {
      get
      {
        return this.gridControl;
      }
    }

    protected internal GridColumnDataEventArgs(GridControl gridControl, GridColumn column, int listSourceRow, object _value, bool isGetAction)
      : base((ColumnBase) column, _value, isGetAction)
    {
      this.gridControl = gridControl;
      this.listSourceRow = listSourceRow;
    }

    /// <summary>
    ///                 <para>Returns the value of the specified cell within the processed row in the grid's data source.
    /// </para>
    ///             </summary>
    /// <param name="fieldName">
    /// A <see cref="T:System.String" /> value that specifies the name of the data source field.
    /// 
    ///           </param>
    /// <returns>An object that represents a value from the grid's data source.</returns>
    public object GetListSourceFieldValue(string fieldName)
    {
      return this.GetListSourceFieldValue(this.ListSourceRowIndex, fieldName);
    }

    /// <summary>
    ///                 <para>Returns the value of the specified cell within the specified row in the grid's data source.
    /// </para>
    ///             </summary>
    /// <param name="listSourceRowIndex">
    /// A zero-based integer value that identifies the record in a data source by its index.
    /// 
    ///           </param>
    /// <param name="fieldName">
    /// A <see cref="T:System.String" /> value that specifies the name of the data source field.
    /// 
    ///           </param>
    /// <returns>An object that represents a value from the grid's data source.</returns>
    public object GetListSourceFieldValue(int listSourceRowIndex, string fieldName)
    {
      return ((GridDataProvider) this.gridControl.DataProviderBase).GetListSourceRowValue(listSourceRowIndex, fieldName);
    }
  }
}
