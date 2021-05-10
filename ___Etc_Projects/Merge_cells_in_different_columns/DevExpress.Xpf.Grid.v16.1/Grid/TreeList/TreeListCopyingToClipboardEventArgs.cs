// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListCopyingToClipboardEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;
using System.ComponentModel;

namespace DevExpress.Xpf.Grid.TreeList
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.TreeListView.CopyingToClipboard" /> event.
  /// </para>
  ///             </summary>
  public class TreeListCopyingToClipboardEventArgs : CopyingToClipboardEventArgs
  {
    private IEnumerable<TreeListNode> nodes;

    /// <summary>
    ///                 <para>Gets an array of nodes whose values are about to be copied to the clipboard.
    /// </para>
    ///             </summary>
    /// <value>An array of <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> objects.
    /// </value>
    public IEnumerable<TreeListNode> Nodes
    {
      get
      {
        if (this.nodes == null)
          this.nodes = ((TreeListView) this.Source).GetNodesFromRowHandles(this.RowHandles);
        return this.nodes;
      }
    }

    /// <summary>
    ///                 <para>Gets an array of cells whose values are about to be copied to the clipboard.
    /// </para>
    ///             </summary>
    /// <value>An array of TreeListCell objects whose values are about to be copied to the clipboard.
    /// </value>
    public IEnumerable<TreeListCell> Cells { get; private set; }

    /// <summary>
    ///                 <para>Gets an array of cells whose values are about to be copied to the clipboard.
    /// </para>
    ///             </summary>
    /// <value>An array of the <see cref="T:DevExpress.Xpf.Grid.GridCell" /> objects whose values are about to be copied to the clipboard.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override IEnumerable<GridCell> GridCells
    {
      get
      {
        return base.GridCells;
      }
    }

    public TreeListCopyingToClipboardEventArgs(TreeListView view, IEnumerable<int> rowHandles, bool copyHeader)
      : base((DataViewBase) view, rowHandles, copyHeader)
    {
    }

    public TreeListCopyingToClipboardEventArgs(TreeListView view, IEnumerable<TreeListCell> cells, bool copyHeader)
      : base((DataViewBase) view, (IEnumerable<GridCell>) null, copyHeader)
    {
      this.Cells = cells;
    }
  }
}
