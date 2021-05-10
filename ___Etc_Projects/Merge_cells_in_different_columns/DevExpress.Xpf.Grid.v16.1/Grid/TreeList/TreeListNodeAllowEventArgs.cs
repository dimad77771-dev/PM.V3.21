// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListNodeAllowEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid.TreeList
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.TreeListView.NodeExpanding" /> and <see cref="E:DevExpress.Xpf.Grid.TreeListView.NodeCollapsing" /> events.
  /// </para>
  ///             </summary>
  public class TreeListNodeAllowEventArgs : TreeListNodeEventArgs
  {
    /// <summary>
    ///                 <para>Gets or sets whether or not the operation is allowed.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow the operation; otherwise, <b>false</b>.
    /// </value>
    public bool Allow { get; set; }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListNodeAllowEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that represents the processed node. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListNodeEventArgs.Node" /> property.
    /// 
    ///           </param>
    public TreeListNodeAllowEventArgs(TreeListNode node)
      : base(node)
    {
      this.Allow = true;
    }
  }
}
