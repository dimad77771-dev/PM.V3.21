// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListVisibleNodesProvider
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListVisibleNodesProvider
  {
    private Dictionary<TreeListNode, TreeListNodeObject> cache;
    private TreeListNodeCollection nodes;

    public List<TreeListNodeObject> Nodes { get; private set; }

    public TreeListVisibleNodesProvider(TreeListNodeCollection nodes)
    {
      this.nodes = nodes;
      this.Reset();
    }

    public TreeListNodeObject GetNodeObject(TreeListNode node)
    {
      TreeListNodeObject treeListNodeObject = (TreeListNodeObject) null;
      this.cache.TryGetValue(node, out treeListNodeObject);
      return treeListNodeObject;
    }

    public void Reset()
    {
      this.cache = new Dictionary<TreeListNode, TreeListNodeObject>();
      this.Nodes = this.Create(this.nodes);
    }

    private List<TreeListNodeObject> Create(TreeListNodeCollection nodes)
    {
      List<TreeListNodeObject> wrapperList = new List<TreeListNodeObject>();
      this.CreateCore(wrapperList, nodes, 0);
      return wrapperList;
    }

    private void CreateCore(List<TreeListNodeObject> wrapperList, TreeListNodeCollection nodes, int level)
    {
      foreach (TreeListNode node in (Collection<TreeListNode>) nodes)
      {
        if (node.IsVisible)
        {
          TreeListNodeObject treeListNodeObject = new TreeListNodeObject(node) { Level = level };
          this.cache[node] = treeListNodeObject;
          wrapperList.Add(treeListNodeObject);
          this.CreateCore(treeListNodeObject.Nodes, node.Nodes, level + 1);
        }
        else
          this.CreateCore(wrapperList, node.Nodes, level);
      }
    }
  }
}
