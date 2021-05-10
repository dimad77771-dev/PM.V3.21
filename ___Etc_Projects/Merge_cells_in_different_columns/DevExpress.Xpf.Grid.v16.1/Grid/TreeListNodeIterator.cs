// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListNodeIterator
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>The <b>Node Iterator</b>.
  /// </para>
  ///             </summary>
  public class TreeListNodeIterator : IEnumerator<TreeListNode>, IDisposable, IEnumerator, IEnumerable<TreeListNode>, IEnumerable
  {
    private Stack<TreeListNode> stack = new Stack<TreeListNode>();
    private TreeListNode startNode;
    private TreeListNodeCollection startNodes;
    private bool visibleOnly;
    private TreeListNode current;

    /// <summary>
    ///                 <para>Gets a node currently being processed by the TreeListNodeIterator object.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node currently being processed.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListNodeIteratorCurrent")]
    public TreeListNode Current
    {
      get
      {
        return this.current;
      }
    }

    object IEnumerator.Current
    {
      get
      {
        return (object) this.Current;
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListNodeIterator class.
    /// </para>
    ///             </summary>
    /// <param name="startNode">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is a starting node.
    /// 
    ///           </param>
    /// <param name="visibleOnly">
    /// <b>true</b> to iterate only through visible (i.e. not collapsed) nodes; otherwise, <b>false</b>.
    /// 
    ///           </param>
    public TreeListNodeIterator(TreeListNode startNode, bool visibleOnly)
    {
      this.startNode = startNode;
      this.visibleOnly = visibleOnly;
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListNodeIterator class.
    /// </para>
    ///             </summary>
    /// <param name="startNode">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is a starting node.
    /// 
    ///           </param>
    public TreeListNodeIterator(TreeListNode startNode)
      : this(startNode, false)
    {
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListNodeIterator class.
    /// </para>
    ///             </summary>
    /// <param name="startNodes">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNodeCollection" /> object that is the collection of nodes from which traversing nodes is initiated.
    /// 
    /// 
    ///           </param>
    /// <param name="visibleOnly">
    /// <b>true</b> to iterate only through visible (i.e. not collapsed) nodes; otherwise, <b>false</b>.
    /// 
    ///           </param>
    public TreeListNodeIterator(TreeListNodeCollection startNodes, bool visibleOnly)
    {
      this.startNodes = startNodes;
      this.visibleOnly = visibleOnly;
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListNodeIterator class.
    /// </para>
    ///             </summary>
    /// <param name="startNodes">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNodeCollection" /> object that is the collection of nodes from which traversing nodes is initialized.
    /// 
    /// 
    ///           </param>
    public TreeListNodeIterator(TreeListNodeCollection startNodes)
      : this(startNodes, false)
    {
    }

    /// <summary>
    ///                 <para>Moves to the next node.
    /// </para>
    ///             </summary>
    /// <returns><b>true</b> if the current node is not the last node; otherwise, <b>false</b>.
    /// </returns>
    public bool MoveNext()
    {
      if (this.current == null)
      {
        this.current = this.startNode;
        if (this.current == null && this.startNodes != null)
        {
          this.TraverseChildNodes(this.startNodes);
          this.UpdateCurrent();
        }
      }
      else
      {
        if (!this.visibleOnly || this.current.IsExpanded)
          this.TraverseChildNodes(this.current.Nodes);
        this.UpdateCurrent();
      }
      return this.current != null;
    }

    /// <summary>
    ///                 <para>Resets the TreeListNodeIterator object to its initial state.
    /// </para>
    ///             </summary>
    public void Reset()
    {
      this.stack.Clear();
      this.current = (TreeListNode) null;
    }

    private void TraverseChildNodes(TreeListNodeCollection nodes)
    {
      int count = nodes.Count;
      for (int index = 0; index < count; ++index)
        this.stack.Push(nodes[count - 1 - index]);
    }

    private void UpdateCurrent()
    {
      this.current = this.stack.Count > 0 ? this.stack.Pop() : (TreeListNode) null;
    }

    void IDisposable.Dispose()
    {
      this.Reset();
    }

    IEnumerator<TreeListNode> IEnumerable<TreeListNode>.GetEnumerator()
    {
      return (IEnumerator<TreeListNode>) this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this;
    }
  }
}
