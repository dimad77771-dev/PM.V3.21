// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CustomGroupDisplayTextEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.GridControl.CustomGroupDisplayText" /> event.
  /// </para>
  ///             </summary>
  public class CustomGroupDisplayTextEventArgs : CellValueEventArgs
  {
    /// <summary>
    ///                 <para>Gets or sets the text displayed within the processed group row.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.String" /> value that specifies the text displayed within the processed group row.
    /// </value>
    public string DisplayText { get; set; }

    /// <summary>
    ///                 <para>Initializes a new instance of the CustomGroupDisplayTextEventArgs class with the specified view, row handle, column, value and display text.
    /// 
    /// </para>
    ///             </summary>
    /// <param name="view">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridViewBase" /> class descendant, which specifies the grid view containing an editor, for which an event has been raised.
    /// 
    ///           </param>
    /// <param name="rowHandle">
    /// An integer value, which specifies the handle of the row, for which an event has been raised. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.RowEventArgs.RowHandle" />
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object, which specifies the grid column, for which an event has been raised. This object is assigned to the <see cref="P:DevExpress.Xpf.Grid.CellValueEventArgs.Column" />  property.
    /// 
    ///           </param>
    /// <param name="value">
    /// A <see cref="T:System.Object" />, which specifies the value, for which an event has been raised. This object is assigned to the <see cref="P:DevExpress.Xpf.Grid.CellValueEventArgs.Value" />  property.
    /// 
    ///           </param>
    /// <param name="displayText">
    /// A <see cref="T:System.String" /> value, which specifies the text to be displayed for a custom group. This object is assigned to the <see cref="P:DevExpress.Xpf.Grid.CustomGroupDisplayTextEventArgs.DisplayText" /> property.
    /// 
    ///           </param>
    public CustomGroupDisplayTextEventArgs(GridViewBase view, int rowHandle, GridColumn column, object value, string displayText)
      : base(GridControl.CustomGroupDisplayTextEvent, view, rowHandle, column, value)
    {
      this.DisplayText = displayText;
    }

    internal void SetArgs(GridViewBase view, int rowHandle, GridColumn column, object value, string displayText)
    {
      this.view = view;
      this.RowHandle = rowHandle;
      this.Column = column;
      this.Value = value;
      this.DisplayText = displayText;
    }
  }
}
