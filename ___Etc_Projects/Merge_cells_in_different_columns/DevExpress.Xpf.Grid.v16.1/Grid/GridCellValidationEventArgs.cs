// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridCellValidationEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.GridColumn.Validate" /> event.
  /// </para>
  ///             </summary>
  public class GridCellValidationEventArgs : GridRowValidationEventArgs, IDataCellEventArgs
  {
    /// <summary>
    ///                 <para>Gets the column that contains the prcoessed cell.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that represents the column that contains the prcoessed cell.
    /// </value>
    public ColumnBase Column { get; private set; }

    /// <summary>
    ///                 <para>Gets the cell's old valid value.
    /// </para>
    ///             </summary>
    /// <value>An object that represents the cell's old valid value.</value>
    public object CellValue
    {
      get
      {
        return this.DataControl.GetCellValueCore(this.RowHandle, this.Column);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the <b>CellValue</b> object consisting of information about the cell being processed.
    /// </para>
    ///             </summary>
    /// <value>The <b>CellValue</b> object consisting of information about the cell being processed.
    /// </value>
    public DevExpress.Mvvm.CellValue Cell
    {
      get
      {
        return new DevExpress.Mvvm.CellValue(this.Row, this.Column.FieldName, this.CellValue);
      }
    }

    /// <summary>
    ///     <para> </para>
    /// </summary>
    /// <param name="source">
    /// 
    /// </param>
    /// <param name="value">
    /// 
    /// </param>
    /// <param name="rowHandle">
    /// 
    /// </param>
    /// <param name="view">
    /// 
    /// </param>
    /// <param name="column">
    /// 
    /// </param>
    public GridCellValidationEventArgs(object source, object value, int rowHandle, DataViewBase view, ColumnBase column)
      : base(source, value, rowHandle, view)
    {
      this.Column = column;
    }
  }
}
