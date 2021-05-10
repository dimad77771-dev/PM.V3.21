// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListNodesInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListNodesInfo
  {
    private Dictionary<int, TreeListNode> rowHandleToNodeCache;
    private Dictionary<int, TreeListNode> visibleIndexToNodeCache;
    private Dictionary<TreeListNode, int> nodeToVisibleIndexCache;
    private bool shouldRefreshRowHandles;
    private bool shouldRefreshVisibleIndicies;
    private int oldVisibleNodesCount;

    protected TreeListDataProvider DataProvider { get; private set; }

    public int TotalNodesCount
    {
      get
      {
        this.EnsureRowHandles();
        return this.rowHandleToNodeCache.Count;
      }
    }

    public int TotalVisibleNodesCount
    {
      get
      {
        this.EnsureVisibleIndicies();
        return this.visibleIndexToNodeCache.Count;
      }
    }

    public TreeListNodesInfo(TreeListDataProvider dataProvider)
    {
      this.DataProvider = dataProvider;
      this.rowHandleToNodeCache = new Dictionary<int, TreeListNode>();
      this.visibleIndexToNodeCache = new Dictionary<int, TreeListNode>();
      this.nodeToVisibleIndexCache = new Dictionary<TreeListNode, int>();
      this.SetDirty();
    }

    public TreeListNode GetNodeByRowHandle(int rowHandle)
    {
      this.EnsureRowHandles();
      TreeListNode treeListNode = (TreeListNode) null;
      if (this.rowHandleToNodeCache.TryGetValue(rowHandle, out treeListNode))
        return treeListNode;
      return (TreeListNode) null;
    }

    public int GetRowHandleByNode(TreeListNode node)
    {
      this.EnsureRowHandles();
      return node.rowHandle;
    }

    public int GetVisibleIndexByNode(TreeListNode node)
    {
      this.EnsureVisibleIndicies();
      if (!this.nodeToVisibleIndexCache.ContainsKey(node))
        return -1;
      return node.visibleIndex;
    }

    public TreeListNode GetNodeByVisibleIndex(int visibleIndex)
    {
      TreeListNode treeListNode = (TreeListNode) null;
      this.EnsureVisibleIndicies();
      if (this.visibleIndexToNodeCache.TryGetValue(visibleIndex, out treeListNode))
        return treeListNode;
      return (TreeListNode) null;
    }

    internal TreeListNode FindVisibleNode(int rowHandle)
    {
      if (rowHandle < 0 || this.TotalNodesCount == 0)
        return (TreeListNode) null;
      for (int rowHandle1 = rowHandle; rowHandle1 < this.TotalNodesCount; ++rowHandle1)
      {
        TreeListNode nodeByRowHandle = this.GetNodeByRowHandle(rowHandle1);
        if (nodeByRowHandle != null && nodeByRowHandle.IsVisible && this.GetVisibleIndexByNode(nodeByRowHandle) >= 0)
          return nodeByRowHandle;
      }
      for (int rowHandle1 = rowHandle - 1; rowHandle1 >= 0; --rowHandle1)
      {
        TreeListNode nodeByRowHandle = this.GetNodeByRowHandle(rowHandle1);
        if (nodeByRowHandle != null && nodeByRowHandle.IsVisible && this.GetVisibleIndexByNode(nodeByRowHandle) >= 0)
          return nodeByRowHandle;
      }
      return (TreeListNode) null;
    }

    public void SetDirty()
    {
      this.SetDirty(false);
    }

    public void SetDirty(bool visibleIndiciesOnly)
    {
      if (!this.shouldRefreshVisibleIndicies)
      {
        this.oldVisibleNodesCount = this.visibleIndexToNodeCache.Count;
        this.shouldRefreshVisibleIndicies = true;
        this.ClearVisibleIndicies();
      }
      if (visibleIndiciesOnly)
        return;
      this.shouldRefreshRowHandles = true;
      this.ClearRowHandles();
    }

    protected void EnsureRowHandles()
    {
      if (!this.shouldRefreshRowHandles)
        return;
      int index = 0;
      foreach (TreeListNode treeListNode in (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.DataProvider.Nodes))
      {
        treeListNode.rowHandle = index;
        this.rowHandleToNodeCache[index] = treeListNode;
        ++index;
      }
      this.shouldRefreshRowHandles = false;
    }

    protected void EnsureVisibleIndicies()
    {
      if (!this.shouldRefreshVisibleIndicies)
        return;
      int num = this.oldVisibleNodesCount;
      int index1 = 0;
      foreach (TreeListNode index2 in (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.DataProvider.Nodes, true))
      {
        if (index2.IsVisible)
        {
          index2.visibleIndex = index1;
          this.visibleIndexToNodeCache[index1] = index2;
          this.nodeToVisibleIndexCache[index2] = index1;
          ++index1;
        }
      }
      this.shouldRefreshVisibleIndicies = false;
      if (this.visibleIndexToNodeCache.Count == num || this.DataProvider.View.DataControl == null)
        return;
      this.DataProvider.View.DataControl.RaiseVisibleRowCountChanged();
    }

    protected void ClearRowHandles()
    {
      this.rowHandleToNodeCache.Clear();
    }

    protected void ClearVisibleIndicies()
    {
      this.visibleIndexToNodeCache.Clear();
      this.nodeToVisibleIndexCache.Clear();
    }
  }
}
