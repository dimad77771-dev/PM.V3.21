// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CustomColumnSortEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using System;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.GridControl.CustomColumnSort" /> event.
  /// </para>
  ///             </summary>
  public class CustomColumnSortEventArgs : EventArgs
  {
    private ColumnSortOrder sortOrder;
    private bool handled;
    private object value1;
    private object value2;
    private int result;
    private int listSourceRowIndex1;
    private int listSourceRowIndex2;
    private GridColumn column;

    /// <summary>
    ///                 <para>Gets the sort order applied to the column.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Data.ColumnSortOrder" /> value that specifies the column's sort order.
    /// </value>
    public ColumnSortOrder SortOrder
    {
      get
      {
        return this.sortOrder;
      }
    }

    /// <summary>
    ///                 <para>Gets the column whose values are being compared.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object, representing the column that contains the values to compare.
    /// </value>
    public GridColumn Column
    {
      get
      {
        return this.column;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether a comparison operation is handled and no default processing is required.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if a comparison operation is handled; otherwise <b>false</b>.
    /// </value>
    public bool Handled
    {
      get
      {
        return this.handled;
      }
      set
      {
        this.handled = value;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the result of a custom comparison.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the result.</value>
    public int Result
    {
      get
      {
        return this.result;
      }
      set
      {
        this.result = value;
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
        return (GridControl) this.Column.OwnerControl;
      }
    }

    /// <summary>
    ///                 <para>Gets the first value being compared.
    /// </para>
    ///             </summary>
    /// <value>An object that represents the first of the two values being compared.</value>
    public object Value1
    {
      get
      {
        return this.value1;
      }
    }

    /// <summary>
    ///                 <para>Gets the second value being compared.
    /// </para>
    ///             </summary>
    /// <value>An object that represents the second of the two values being compared.</value>
    public object Value2
    {
      get
      {
        return this.value2;
      }
    }

    /// <summary>
    ///                 <para>Gets the index of the first of the two rows being compared in the data source.
    /// 
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the first row's index in the data source.</value>
    public int ListSourceRowIndex1
    {
      get
      {
        return this.listSourceRowIndex1;
      }
    }

    /// <summary>
    ///                 <para>Gets the index of the second of the two rows being compared in the data source.
    /// 
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the second row's index in the data source.</value>
    public int ListSourceRowIndex2
    {
      get
      {
        return this.listSourceRowIndex2;
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the CustomColumnSortEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object, representing the column that contains the values to compare. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.CustomColumnSortEventArgs.Column" /> property.
    /// 
    ///           </param>
    /// <param name="listSourceRowIndex1">
    /// An integer value that specifies the first row's index in the data source. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.CustomColumnSortEventArgs.ListSourceRowIndex1" /> property.
    /// 
    ///           </param>
    /// <param name="listSourceRowIndex2">
    /// An integer value that specifies the second row's index in the data source. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.CustomColumnSortEventArgs.ListSourceRowIndex2" /> property.
    /// 
    ///           </param>
    /// <param name="value1">
    /// An object that represents the first of the two values being compared. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.CustomColumnSortEventArgs.Value1" /> property.
    /// 
    ///           </param>
    /// <param name="value2">
    /// An object that represents the second of the two values being compared. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.CustomColumnSortEventArgs.Value2" /> property.
    /// 
    ///           </param>
    /// <param name="sortOrder">
    /// A <see cref="T:DevExpress.Data.ColumnSortOrder" /> value that specifies the sort order applied to the specified column. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.CustomColumnSortEventArgs.SortOrder" /> property.
    /// 
    ///           </param>
    public CustomColumnSortEventArgs(GridColumn column, int listSourceRowIndex1, int listSourceRowIndex2, object value1, object value2, ColumnSortOrder sortOrder)
    {
      this.sortOrder = sortOrder;
      this.listSourceRowIndex1 = listSourceRowIndex1;
      this.listSourceRowIndex2 = listSourceRowIndex2;
      this.value1 = value1;
      this.value2 = value2;
      this.column = column;
    }

    internal void SetArgs(int listSourceRowIndex1, int listSourceRowIndex2, object value1, object value2, ColumnSortOrder sortOrder)
    {
      this.sortOrder = sortOrder;
      this.value1 = value1;
      this.value2 = value2;
      this.result = 0;
      this.handled = false;
      this.listSourceRowIndex1 = listSourceRowIndex1;
      this.listSourceRowIndex2 = listSourceRowIndex2;
    }

    protected internal int? GetSortResult()
    {
      if (!this.Handled)
        return new int?();
      return new int?(this.Result);
    }
  }
}
