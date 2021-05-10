// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListNodeCollection
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.TreeList;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>A collection of nodes.
  /// </para>
  ///             </summary>
  public class TreeListNodeCollection : Collection<TreeListNode>
  {
    protected internal TreeListNode Owner { get; set; }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListNodeCollection class.
    /// </para>
    ///             </summary>
    /// <param name="owner">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the owner of the collection.
    /// 
    ///           </param>
    public TreeListNodeCollection(TreeListNode owner)
    {
      this.Owner = owner;
    }

    protected override void InsertItem(int index, TreeListNode item)
    {
      if (!this.CanBeAddedAsChild(item))
        return;
      this.OnChanging(item, NodeChangeType.Add);
      this.InsertItem(index, item);
      this.LinkNode(item);
      this.OnChanged(item, NodeChangeType.Add);
    }

    private bool CanBeAddedAsChild(TreeListNode item)
    {
      if (!object.ReferenceEquals((object) this.Owner, (object) item))
        return !this.Owner.IsDescendantOf(item);
      return false;
    }

    protected override void ClearItems()
    {
      List<TreeListNode> treeListNodeList = new List<TreeListNode>((IEnumerable<TreeListNode>) this.Items);
      foreach (TreeListNode node in treeListNodeList)
      {
        this.OnChanging(node, NodeChangeType.Remove);
        this.UnlinkNode(node);
      }
      base.ClearItems();
      foreach (TreeListNode node in treeListNodeList)
        this.OnChanged(node, NodeChangeType.Remove);
      treeListNodeList.Clear();
    }

    protected virtual void LinkNode(TreeListNode node)
    {
      if (node.ParentNode != null)
        node.ParentNode.Nodes.Remove(node);
      node.ParentNode = this.Owner;
      if (this.Owner.DataProvider != null && this.Owner.DataProvider.IsRecursiveNodesUpdateLocked)
        return;
      TreeListNodeCollection.AssignFromOwner(node, this.Owner);
    }

    protected virtual void UnlinkNode(TreeListNode node)
    {
      node.ParentNode = (TreeListNode) null;
      if (this.Owner.DataProvider != null && this.Owner.DataProvider.IsRecursiveNodesUpdateLocked)
        return;
      TreeListNodeCollection.AssignFromOwner(node, (TreeListNode) null);
    }

    protected static void AssignFromOwner(TreeListNode node, TreeListNode owner)
    {
      node.ProcessNodeAndDescendantsAction((Func<TreeListNode, bool>) (n =>
      {
        n.DataProvider = owner != null ? owner.DataProvider : (TreeListDataProvider) null;
        TreeListNodeCollection.UpdateNodeId(n);
        if (n.DataProvider != null && n.DataProvider.IsUnboundMode)
          n.InitIsChecked();
        if (owner != null && owner.ItemTemplate != null)
        {
          n.Template = owner.ItemTemplate;
          n.ItemTemplate = owner.ItemTemplate;
        }
        return true;
      }));
    }

    private static void UpdateNodeId(TreeListNode n)
    {
      if (n.DataProvider == null)
      {
        n.Id = -1;
        n.visibleIndex = -1;
        n.rowHandle = int.MinValue;
      }
      else
        n.UpdateId();
    }

    protected virtual void OnChanged(TreeListNode node, NodeChangeType changeType)
    {
      if (this.Owner == null || this.Owner.DataProvider == null)
        return;
      this.Owner.DataProvider.OnNodeCollectionChanged(node, changeType, true, (string) null);
    }

    protected virtual void OnChanging(TreeListNode node, NodeChangeType changeType)
    {
      if (this.Owner == null || this.Owner.DataProvider == null)
        return;
      this.Owner.DataProvider.OnNodeCollectionChanging(node, changeType);
    }

    protected override void RemoveItem(int index)
    {
      TreeListNode node = this[index];
      this.EnsureNotEditing(node);
      this.OnChanging(node, NodeChangeType.Remove);
      this.UnlinkNode(node);
      base.RemoveItem(index);
      this.OnChanged(node, NodeChangeType.Remove);
    }

    private void EnsureNotEditing(TreeListNode node)
    {
      if (this.Owner == null || this.Owner.DataProvider == null)
        return;
      TreeListDataProvider dataProvider = this.Owner.DataProvider;
      if (!dataProvider.IsCurrentRowEditing || dataProvider.CurrentControllerRow != node.RowHandle)
        return;
      dataProvider.CloseActiveEditor();
      dataProvider.EndCurrentRowEdit();
    }

    protected override void SetItem(int index, TreeListNode item)
    {
      this.SetItem(index, item);
      this.OnChanged(item, NodeChangeType.Add);
    }

    protected internal virtual void DoSort(IComparer<TreeListNode> comparer)
    {
      ((List<TreeListNode>) this.Items).Sort(comparer);
    }

    protected internal TreeListNode GetFirstVisible()
    {
      for (int index = 0; index < this.Count; ++index)
      {
        TreeListNode treeListNode = (TreeListNode) null;
        if (this[index].IsVisible)
          return this[index];
        if (this[index].IsExpanded)
          treeListNode = this[index].Nodes.GetFirstVisible();
        if (treeListNode != null)
          return treeListNode;
      }
      return (TreeListNode) null;
    }

    protected internal TreeListNode GetLastVisible()
    {
      for (int index = this.Count - 1; index >= 0; --index)
      {
        TreeListNode treeListNode = (TreeListNode) null;
        if (this[index].IsVisible)
          return this[index];
        if (this[index].IsExpanded)
          treeListNode = this[index].Nodes.GetLastVisible();
        if (treeListNode != null)
          return treeListNode;
      }
      return (TreeListNode) null;
    }
  }
}
