// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListCustomColumnDisplayTextEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;

namespace DevExpress.Xpf.Grid.TreeList
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.TreeListView.CustomColumnDisplayText" /> event.
  /// </para>
  ///             </summary>
  public class TreeListCustomColumnDisplayTextEventArgs : EventArgs
  {
    /// <summary>
    ///                 <para>Gets the node which owns the processed cell.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that specifies the node in which the processed cell resides.
    /// 
    /// </value>
    public TreeListNode Node { get; private set; }

    /// <summary>
    ///                 <para>Gets the column which owns the processed cell.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that is the column to which the processed cell belongs.
    /// 
    /// </value>
    public ColumnBase Column { get; private set; }

    /// <summary>
    ///                 <para>Gets the processed cell's value.
    /// </para>
    ///             </summary>
    /// <value>An object that is the value of the processed cell.</value>
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

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListCustomColumnDisplayTextEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that specifies the node in which the processed cell resides. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListCustomColumnDisplayTextEventArgs.Node" /> property.
    /// 
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that specifies the column to which the processed cell belongs. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListCustomColumnDisplayTextEventArgs.Column" /> property.
    /// 
    /// 
    ///           </param>
    /// <param name="value">
    /// An object that is the value of the processed cell. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListCustomColumnDisplayTextEventArgs.Value" /> property.
    /// 
    /// 
    ///           </param>
    /// <param name="displayText">
    /// A <see cref="T:System.String" /> value that specifies the cell's display text. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListCustomColumnDisplayTextEventArgs.DisplayText" /> property.
    /// 
    ///           </param>
    public TreeListCustomColumnDisplayTextEventArgs(TreeListNode node, ColumnBase column, object value, string displayText)
    {
      this.SetArgs(node, column, value, displayText);
    }

    protected internal void SetArgs(TreeListNode node, ColumnBase column, object value, string displayText)
    {
      this.Node = node;
      this.Column = column;
      this.Value = value;
      this.DisplayText = displayText;
      this.ShowAsNullText = false;
    }

    internal void Clear()
    {
      this.Node = (TreeListNode) null;
      this.Column = (ColumnBase) null;
      this.Value = (object) null;
    }
  }
}
