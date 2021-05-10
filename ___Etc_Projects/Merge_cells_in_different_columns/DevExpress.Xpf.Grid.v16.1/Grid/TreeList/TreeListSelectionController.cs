// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListSelectionController
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Xpf.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListSelectionController : ISelectionController
  {
    private int selectionLockCount;
    private bool actuallyChanged;
    private TreeListDataProvider dataProvider;

    public bool IsSelectionLocked
    {
      get
      {
        return this.selectionLockCount != 0;
      }
    }

    public int Count
    {
      get
      {
        return this.Selection.Count;
      }
    }

    protected Dictionary<TreeListNode, object> Selection { get; private set; }

    protected TreeListDataProvider DataProvider
    {
      get
      {
        return this.dataProvider;
      }
    }

    public TreeListSelectionController(TreeListDataProvider dataProvider)
    {
      this.dataProvider = dataProvider;
      this.Selection = new Dictionary<TreeListNode, object>();
    }

    public void BeginSelection()
    {
      if (this.selectionLockCount++ != 0)
        return;
      this.actuallyChanged = false;
    }

    public void EndSelection()
    {
      if (--this.selectionLockCount != 0 || !this.actuallyChanged)
        return;
      this.OnSelectionChanged(new SelectionChangedEventArgs());
    }

    public void CancelSelection()
    {
      --this.selectionLockCount;
    }

    public bool GetSelected(int rowHandle)
    {
      return this.GetSelected(this.DataProvider.GetNodeByRowHandle(rowHandle));
    }

    public bool GetSelected(TreeListNode node)
    {
      if (node == null)
        return false;
      return this.Selection.ContainsKey(node);
    }

    public void SetSelected(TreeListNode node, bool selected)
    {
      this.SetSelected(node, selected, (object) null);
    }

    public void SetSelected(TreeListNode node, bool selected, object selectedObject)
    {
      if (node == null || this.GetSelected(node) == selected && (!selected || selectedObject == null || object.Equals(this.GetSelectedObject(node), selectedObject)))
        return;
      if (!selected)
        this.Selection.Remove(node);
      else
        this.Selection[node] = selectedObject;
      this.OnSelectionChanged(new SelectionChangedEventArgs(selected ? CollectionChangeAction.Add : CollectionChangeAction.Remove, node.rowHandle));
    }

    public void SetSelected(int rowHandle, bool selected)
    {
      this.SetSelected(this.DataProvider.GetNodeByRowHandle(rowHandle), selected);
    }

    public void SetSelected(int rowHandle, bool selected, object selectedObject)
    {
      this.SetSelected(this.DataProvider.GetNodeByRowHandle(rowHandle), selected, selectedObject);
    }

    public void Clear()
    {
      if (this.Count == 0)
        return;
      this.Selection.Clear();
      this.OnSelectionChanged(new SelectionChangedEventArgs());
    }

    public void SelectAll()
    {
      this.BeginSelection();
      try
      {
        this.Clear();
        foreach (TreeListNode node in (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.DataProvider.Nodes))
        {
          if (node.IsVisible)
            this.SetSelected(node, true);
        }
      }
      finally
      {
        this.EndSelection();
      }
    }

    public int[] GetSelectedRows()
    {
      if (this.Count == 0)
        return new int[0];
      int[] array = this.Selection.Keys.Cast<TreeListNode>().Select<TreeListNode, int>((Func<TreeListNode, int>) (node => node.RowHandle)).ToArray<int>();
      Array.Sort<int>(array);
      return array;
    }

    public TreeListNode[] GetSelectedNodes()
    {
      if (this.Count == 0)
        return new TreeListNode[0];
      return this.Selection.Keys.OrderBy<TreeListNode, int>((Func<TreeListNode, int>) (n => n.RowHandle)).ToArray<TreeListNode>();
    }

    public void SetActuallyChanged()
    {
      if (!this.IsSelectionLocked)
        return;
      this.actuallyChanged = true;
    }

    protected void OnSelectionChanged(SelectionChangedEventArgs e)
    {
      this.actuallyChanged = true;
      if (this.IsSelectionLocked)
        return;
      this.DataProvider.OnSelectionChanged(e);
    }

    protected object GetSelectedObject(TreeListNode node)
    {
      if (node != null && this.Selection.ContainsKey(node))
        return this.Selection[node];
      return (object) null;
    }

    object ISelectionController.GetSelectedObject(int rowHandle)
    {
      return this.GetSelectedObject(this.DataProvider.GetNodeByRowHandle(rowHandle));
    }

    void ISelectionController.SetSelected(int rowHandle, bool selected, object selectionObject)
    {
      this.SetSelected(rowHandle, selected, selectionObject);
    }

    private class RowHandleComparer : IComparer<TreeListNode>
    {
      private static TreeListSelectionController.RowHandleComparer defaultComparer = new TreeListSelectionController.RowHandleComparer();

      public static TreeListSelectionController.RowHandleComparer Default
      {
        get
        {
          return TreeListSelectionController.RowHandleComparer.defaultComparer;
        }
      }

      public int Compare(TreeListNode x, TreeListNode y)
      {
        return Comparer<int>.Default.Compare(x.RowHandle, y.RowHandle);
      }
    }
  }
}
