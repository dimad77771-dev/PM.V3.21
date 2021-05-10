// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.CustomColumnFilterListEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;

namespace DevExpress.Xpf.Grid.TreeList
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.TreeListView.CustomFiterPopupList" /> event.
  /// </para>
  ///             </summary>
  public class CustomColumnFilterListEventArgs : EventArgs
  {
    /// <summary>
    ///                 <para>Gets or sets whether the processed value is displayed within a column's Filter Dropdown.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to display the value within the list of filter values; otherwise, <b>false</b>.
    /// </value>
    public bool Visible { get; set; }

    /// <summary>
    ///                 <para>Gets or sets a column which owns the processed cell.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListColumn" /> object that is the column which owns the processed cell.
    /// </value>
    public TreeListColumn Column { get; set; }

    /// <summary>
    ///                 <para>Gets or sets a node which contains the processed cell.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node which contains the processed cell.
    /// </value>
    public TreeListNode Node { get; set; }

    /// <summary>
    ///                 <para>Initializes a new instance of the CustomColumnFilterListEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node which contains the processed cell. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.CustomColumnFilterListEventArgs.Node" /> property.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListColumn" /> object that is the column which contains the processed cell. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.CustomColumnFilterListEventArgs.Column" /> property.
    /// 
    ///           </param>
    public CustomColumnFilterListEventArgs(TreeListNode node, TreeListColumn column)
    {
      this.Node = node;
      this.Column = column;
      this.Visible = true;
    }
  }
}
