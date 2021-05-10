// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CellValueEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm;
using DevExpress.Xpf.Core.Native;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.GridViewBase.CellValueChanging" /> and <see cref="E:DevExpress.Xpf.Grid.GridViewBase.CellValueChanged" /> events.
  /// </para>
  ///             </summary>
  public class CellValueEventArgs : RowEventArgs, IDataCellEventArgs
  {
    /// <summary>
    ///                 <para>Gets a column that contains the edited cell.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column that contains the edited cell.
    /// </value>
    public GridColumn Column { get; protected set; }

    /// <summary>
    ///                 <para>Gets or sets the processed cell's value.
    /// </para>
    ///             </summary>
    /// <value>An object that represents the cell's value.</value>
    public object Value { get; protected set; }

    /// <summary>
    ///                 <para>Gets or sets the <b>CellValue</b> object consisting of information about the cell being processed.
    /// 
    /// </para>
    ///             </summary>
    /// <value>The <b>CellValue</b> object consisting of information about the cell being processed.
    /// 
    /// </value>
    public CellValue Cell
    {
      get
      {
        return new CellValue(this.Row, this.Column.FieldName, this.Value);
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the CellValueEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="routedEvent">The routed event.</param>
    /// <param name="view">
    /// A <see cref="T:DevExpress.Xpf.Grid.DataViewBase" /> descendant that is the view that raised the event.
    /// 
    ///           </param>
    /// <param name="rowHandle">
    /// An integer value that specifies the row's handle. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.RowEventArgs.RowHandle" /> property.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that is the column. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.CellValueEventArgs.Column" /> property.
    /// 
    ///           </param>
    /// <param name="value">
    /// An object that represents the cell's new value. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.CellValueEventArgs.Value" /> property.
    /// 
    ///           </param>
    public CellValueEventArgs(RoutedEvent routedEvent, GridViewBase view, int rowHandle, GridColumn column, object value)
      : base(routedEvent, view, rowHandle)
    {
      this.Column = column;
      this.Value = value;
    }
  }
}
