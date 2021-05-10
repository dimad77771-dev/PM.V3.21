// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListCustomColumnSortEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using System;

namespace DevExpress.Xpf.Grid.TreeList
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.TreeListView.CustomColumnSort" /> event.
  /// </para>
  ///             </summary>
  public class TreeListCustomColumnSortEventArgs : EventArgs
  {
    /// <summary>
    ///                 <para>Gets the column whose values are being compared.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> descendant that is the column which contains the values to compare.
    /// 
    /// </value>
    public ColumnBase Column { get; private set; }

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
        return this.Column.SortOrder;
      }
    }

    /// <summary>
    ///                 <para>Gets the first of the two nodes being compared.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that specifies the first node.
    /// 
    /// </value>
    public TreeListNode Node1 { get; private set; }

    /// <summary>
    ///                 <para>Gets the second of the two nodes being compared.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that specifies the second node.
    /// 
    /// </value>
    public TreeListNode Node2 { get; private set; }

    /// <summary>
    ///                 <para>Gets the first value being compared.
    /// </para>
    ///             </summary>
    /// <value>An object that is the first of the two values being compared.</value>
    public object Value1 { get; private set; }

    /// <summary>
    ///                 <para>Gets the second value being compared.
    /// </para>
    ///             </summary>
    /// <value>An object that is the second of the two values being compared.</value>
    public object Value2 { get; private set; }

    /// <summary>
    ///                 <para>Gets or sets whether a comparison operation is handled, and no default processing is required.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if a comparison operation is handled; otherwise <b>false</b>.
    /// </value>
    public bool Handled { get; set; }

    /// <summary>
    ///                 <para>Gets or sets the result of a custom comparison.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the result.</value>
    public int Result { get; set; }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListCustomColumnSortEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> descendant that is the column which contains the values to compare. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListCustomColumnSortEventArgs.Column" /> property.
    /// 
    ///           </param>
    /// <param name="node1">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the first node. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListCustomColumnSortEventArgs.Node1" /> property.
    /// 
    ///           </param>
    /// <param name="node2">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the second node. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListCustomColumnSortEventArgs.Node2" /> property.
    /// 
    ///           </param>
    /// <param name="value1">
    /// An object that is the first of the two values being compared. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListCustomColumnSortEventArgs.Value1" /> property.
    /// 
    ///           </param>
    /// <param name="value2">
    /// An object that is the second of the two values being compared. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListCustomColumnSortEventArgs.Value2" /> property.
    /// 
    ///           </param>
    public TreeListCustomColumnSortEventArgs(ColumnBase column, TreeListNode node1, TreeListNode node2, object value1, object value2)
    {
      this.Node1 = node1;
      this.Node2 = node2;
      this.SetArgs(column, value1, value2);
    }

    protected internal void SetArgs(ColumnBase column, object value1, object value2)
    {
      this.Column = column;
      this.Value1 = value1;
      this.Value2 = value2;
      this.Handled = false;
      this.Result = 0;
    }
  }
}
