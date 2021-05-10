// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListCellValueEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm;
using DevExpress.Xpf.Core.Native;

namespace DevExpress.Xpf.Grid.TreeList
{
  /// <summary>
  ///                 <para>Serves as a base for <see cref="T:DevExpress.Xpf.Grid.TreeList.TreeListCellValueChangedEventArgs" /> class.
  /// </para>
  ///             </summary>
  public class TreeListCellValueEventArgs : TreeListNodeEventArgs, IDataCellEventArgs
  {
    /// <summary>
    ///                 <para>Gets a column that contains the edited cell.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> descendant that is the column that contains the edited cell.
    /// 
    /// </value>
    public ColumnBase Column { get; protected set; }

    /// <summary>
    ///                 <para>Gets or sets the processed cell's value.
    /// </para>
    ///             </summary>
    /// <value>An object that is the cell's value.</value>
    public object Value { get; protected set; }

    /// <summary>
    ///                 <para>Gets or sets the <b>CellValue</b> object consisting of information about the cell being processed.
    /// </para>
    ///             </summary>
    /// <value>The <b>CellValue</b> object consisting of information about the cell being processed.
    /// </value>
    public CellValue Cell
    {
      get
      {
        return new CellValue(this.Row, this.Column.FieldName, this.Value);
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListCellValueEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that represents the node. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListNodeEventArgs.Node" /> property.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> descendant that represents the column. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListCellValueEventArgs.Column" /> property.
    /// 
    ///           </param>
    /// <param name="value">
    /// A object that represents the cell's new value. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListCellValueEventArgs.Value" /> property.
    /// 
    ///           </param>
    public TreeListCellValueEventArgs(TreeListNode node, ColumnBase column, object value)
      : base(node)
    {
      this.Column = column;
      this.Value = value;
    }
  }
}
