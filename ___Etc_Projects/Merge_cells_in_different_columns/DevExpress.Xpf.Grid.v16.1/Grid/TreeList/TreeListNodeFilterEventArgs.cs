// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListNodeFilterEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;

namespace DevExpress.Xpf.Grid.TreeList
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.TreeListView.CustomNodeFilter" /> event.
  /// </para>
  ///             </summary>
  public class TreeListNodeFilterEventArgs : EventArgs
  {
    /// <summary>
    ///                 <para>Gets or sets whether or not custom filtering is handled and no default processing is required.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if custom filtering is handled; otherwise <b>false</b>.
    /// </value>
    public bool Handled { get; set; }

    /// <summary>
    ///                 <para>Gets or sets the processed node's visibility.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to show the node; otherwise, <b>false</b>.
    /// </value>
    public bool Visible { get; set; }

    /// <summary>
    ///                 <para>Gets the processed node.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that specifies the processed node.
    /// 
    /// </value>
    public TreeListNode Node { get; private set; }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListNodeFilterEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that represents the processed node. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListNodeFilterEventArgs.Node" /> property.
    /// 
    ///           </param>
    public TreeListNodeFilterEventArgs(TreeListNode node)
    {
      this.Node = node;
      this.Handled = false;
      this.Visible = true;
    }
  }
}
