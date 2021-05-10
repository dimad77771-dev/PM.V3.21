// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.ShowingEditorEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.GridViewBase.ShowingEditor" /> event.
  /// </para>
  ///             </summary>
  public class ShowingEditorEventArgs : ShowingEditorEventArgsBase
  {
    private GridControl Grid
    {
      get
      {
        return (GridControl) this.view.DataControl;
      }
    }

    /// <summary>
    ///                 <para>Gets the data row for which an event has been raised.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Object" /> specifying the data row.
    /// 
    /// </value>
    public object Row
    {
      get
      {
        return this.Grid.GetRow(this.RowHandle);
      }
    }

    /// <summary>
    ///                 <para>Gets a column, for which an event has been raised.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object, that is the grid column.
    /// 
    /// </value>
    public GridColumn Column
    {
      get
      {
        return (GridColumn) base.Column;
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the ShowingEditorEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="view">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridViewBase" /> descendant that represents a grid's view.
    /// 
    ///           </param>
    /// <param name="rowHandle">
    /// An integer value that specifies the handle of the row containing the cell.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column containing the cell.
    /// 
    ///           </param>
    public ShowingEditorEventArgs(GridViewBase view, int rowHandle, GridColumn column)
      : base(GridViewBase.ShowingEditorEvent, (DataViewBase) view, rowHandle, (ColumnBase) column)
    {
    }
  }
}
