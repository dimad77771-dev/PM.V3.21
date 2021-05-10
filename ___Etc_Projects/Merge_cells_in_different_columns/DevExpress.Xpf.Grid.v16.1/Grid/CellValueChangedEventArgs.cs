// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CellValueChangedEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.GridViewBase.CellValueChanged" /> event.
  /// </para>
  ///             </summary>
  public class CellValueChangedEventArgs : CellValueEventArgs
  {
    /// <summary>
    ///                 <para>Gets the cell's previous value.
    /// </para>
    ///             </summary>
    /// <value>An object that represents the cell's previous value.</value>
    public object OldValue { get; private set; }

    /// <summary>
    ///                 <para>Initializes a new instance of the CellValueChangedEventArgs class.
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
    /// <param name="oldValue">
    /// An object that represents the cell's previous value. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.CellValueChangedEventArgs.OldValue" /> property.
    /// 
    ///           </param>
    public CellValueChangedEventArgs(RoutedEvent routedEvent, GridViewBase view, int rowHandle, GridColumn column, object value, object oldValue)
      : base(routedEvent, view, rowHandle, column, value)
    {
      this.OldValue = oldValue;
    }
  }
}
