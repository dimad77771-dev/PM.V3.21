// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridCell
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Represents an object which corresponds to a data cell.
  /// </para>
  ///             </summary>
  public class GridCell : CellBase
  {
    /// <summary>
    ///                 <para>Gets the handle of the row which owns the cell.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the handle of the row which owns the cell.</value>
    [DevExpressXpfGridLocalizedDescription("GridCellRowHandle")]
    public int RowHandle
    {
      get
      {
        return this.RowHandleCore;
      }
    }

    /// <summary>
    ///                 <para>Gets the column which contains the cell.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column which contains the cell.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCellColumn")]
    public GridColumn Column
    {
      get
      {
        return (GridColumn) this.ColumnCore;
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the GridCell class.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the handle of the row which owns the cell. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.GridCell.RowHandle" /> property.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column which contains the cell. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.GridCell.Column" /> property.
    /// 
    ///           </param>
    public GridCell(int rowHandle, GridColumn column)
      : base(rowHandle, (ColumnBase) column)
    {
    }
  }
}
