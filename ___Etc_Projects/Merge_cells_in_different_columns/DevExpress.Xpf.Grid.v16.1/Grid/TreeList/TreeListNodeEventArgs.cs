// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListNodeEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;
using System.Windows;

namespace DevExpress.Xpf.Grid.TreeList
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.TreeListView.NodeExpanded" /> and <see cref="E:DevExpress.Xpf.Grid.TreeListView.NodeCollapsed" /> events.
  /// </para>
  ///             </summary>
  public class TreeListNodeEventArgs : RoutedEventArgs, IDataRowEventArgs
  {
    /// <summary>
    ///                 <para>Gets the processed node.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that specifies the processed node.
    /// 
    /// </value>
    public TreeListNode Node { get; protected set; }

    /// <summary>
    ///                 <para>Gets the processed row.
    /// </para>
    ///             </summary>
    /// <value>An object that represents the processed row.</value>
    public object Row
    {
      get
      {
        return this.Node.Content;
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListNodeEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that represents the processed node. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListNodeEventArgs.Node" /> property.
    /// 
    ///           </param>
    public TreeListNodeEventArgs(TreeListNode node)
    {
      this.Node = node;
    }
  }
}
