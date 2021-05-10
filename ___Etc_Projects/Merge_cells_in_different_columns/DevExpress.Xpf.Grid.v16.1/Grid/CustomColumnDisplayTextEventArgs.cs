// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.GridControl.CustomColumnDisplayText" /> event.
  /// </para>
  ///             </summary>
  public class CustomColumnDisplayTextEventArgs : EventArgs
  {
    private GridViewBase view;
    private DataControllerLazyValuesContainer values;

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
        return this.view.Grid;
      }
    }

    /// <summary>
    ///                 <para>Gets the processed row's handle.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the row's handle.</value>
    public int RowHandle
    {
      get
      {
        return this.values.RowHandle;
      }
    }

    /// <summary>
    ///                 <para>Gets the index of a record in a data source that corresponds to the processed data row.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the index of the processed record in a data source.</value>
    public int ListSourceIndex
    {
      get
      {
        return this.values.ListSourceIndex;
      }
    }

    /// <summary>
    ///                 <para>Gets the row which owns the processed cell.
    /// </para>
    ///             </summary>
    /// <value>An object that represents the row in which the processed cell resides.</value>
    public object Row
    {
      get
      {
        return this.Source.GetRow(this.RowHandle);
      }
    }

    /// <summary>
    ///                 <para>Gets the column which owns the processed cell.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column to which the processed cell belongs.
    /// </value>
    public GridColumn Column { get; private set; }

    /// <summary>
    ///                 <para>Gets the processed cell's value.
    /// </para>
    ///             </summary>
    /// <value>An object that represents the value of the processed cell.</value>
    public object Value { get; private set; }

    /// <summary>
    ///                 <para>Gets or sets the display text for the cell currently being processed.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.String" /> value that specifies the cell's display text.
    /// </value>
    public string DisplayText { get; set; }

    /// <summary>
    ///                 <para>Specifies whether text corresponding to a null value appears faded.
    /// 
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to fade the null text; otherwise, <b>false</b>.
    /// </value>
    public bool ShowAsNullText { get; set; }

    internal void SetArgs(GridViewBase view, int? rowHandle, int? listSourceIndex, GridColumn column, object value, string displayText)
    {
      this.view = view;
      this.values = new DataControllerLazyValuesContainer(view, rowHandle, listSourceIndex);
      this.Column = column;
      this.Value = value;
      this.DisplayText = displayText;
      this.ShowAsNullText = false;
    }
  }
}
