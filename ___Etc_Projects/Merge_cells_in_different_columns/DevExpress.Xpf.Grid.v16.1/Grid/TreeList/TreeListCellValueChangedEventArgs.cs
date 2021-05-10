// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListCellValueChangedEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid.TreeList
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.TreeListView.CellValueChanged" /> event.
  /// </para>
  ///             </summary>
  public class TreeListCellValueChangedEventArgs : TreeListCellValueEventArgs
  {
    /// <summary>
    ///                 <para>Gets the cell's previous value.
    /// </para>
    ///             </summary>
    /// <value>An object that is the cell's previous value.</value>
    public object OldValue { get; private set; }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListCellValueChangedEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that specifies the node. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListNodeEventArgs.Node" /> property.
    /// 
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> descendant that specifies the column. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListCellValueEventArgs.Column" /> property.
    /// 
    /// 
    ///           </param>
    /// <param name="value">
    /// A object that is the cell's new value. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListCellValueEventArgs.Value" /> property.
    /// 
    /// 
    ///           </param>
    /// <param name="oldValue">
    /// A object that represents the cell's previous value. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListCellValueChangedEventArgs.OldValue" /> property.
    /// 
    ///           </param>
    public TreeListCellValueChangedEventArgs(TreeListNode node, ColumnBase column, object value, object oldValue)
      : base(node, column, value)
    {
      this.OldValue = oldValue;
    }
  }
}
