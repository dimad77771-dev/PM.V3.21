// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListNodesState
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections.Generic;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListNodesState
  {
    private Locker currentFocusLocker = new Locker();
    private Locker nodesStateLocker = new Locker();

    protected TreeListDataProvider DataProvider { get; private set; }

    protected TreeListView View
    {
      get
      {
        return this.DataProvider.View;
      }
    }

    protected Dictionary<object, TreeListNodesState.TreeListNodeState> NodesState { get; private set; }

    public object FocusedRow { get; private set; }

    public int FocusedRowHandle { get; private set; }

    public TreeListNodesState(TreeListDataProvider provider)
    {
      this.DataProvider = provider;
      this.NodesState = new Dictionary<object, TreeListNodesState.TreeListNodeState>();
    }

    public void ClearState()
    {
      this.ClearNodesState();
      this.ClearCurrentFocus();
    }

    protected void ClearNodesState()
    {
      this.NodesState.Clear();
    }

    protected void ClearCurrentFocus()
    {
      this.FocusedRow = (object) null;
      this.FocusedRowHandle = int.MinValue;
    }

    internal void LockSaveNodesState()
    {
      this.currentFocusLocker.Lock();
      this.nodesStateLocker.Lock();
    }

    internal void UnlockSaveNodesState()
    {
      this.currentFocusLocker.Unlock();
      this.nodesStateLocker.Unlock();
    }

    public bool SaveCurrentFocus(bool supressLocker = false)
    {
      if (supressLocker || !this.currentFocusLocker.IsLocked)
      {
        this.SaveCurrentFocusCore();
        if (!supressLocker)
          this.currentFocusLocker.Lock();
        return true;
      }
      this.currentFocusLocker.Lock();
      return false;
    }

    protected virtual void SaveCurrentFocusCore()
    {
      if (this.View.DataControl != null && !this.View.DataControl.IsSelectionInitialized)
        return;
      this.FocusedRow = this.View.DataControl != null ? this.View.DataControl.CurrentItem : (object) null;
      this.FocusedRowHandle = this.View.FocusedRowHandle;
    }

    public void RestoreCurrentFocus()
    {
      this.currentFocusLocker.Unlock();
      this.currentFocusLocker.DoIfNotLocked((Action) (() => this.RestoreCurrentFocusCore()));
    }

    protected virtual void RestoreCurrentFocusCore()
    {
      if (this.View.DataControl != null && !this.View.DataControl.IsSelectionInitialized)
        return;
      if (this.View.DataControl != null)
        this.View.DataControl.CurrentItemChangedLocker.Lock();
      this.View.ScrollIntoViewLocker.Lock();
      bool flag = false;
      try
      {
        if (this.FocusedRow == null || !this.DataProvider.IsReady)
          return;
        TreeListNode treeListNode = this.DataProvider.FindNodeByValue(this.FocusedRow) ?? this.DataProvider.FindVisibleNode(this.FocusedRowHandle);
        if (this.View.FocusedNode != treeListNode)
          flag = true;
        this.View.FocusedNode = treeListNode;
        this.View.SetFocusedRowHandle(this.DataProvider.GetRowHandleByNode(this.View.FocusedNode));
        if (this.View.DataControl == null || !this.View.DataControl.AllowUpdateCurrentItem())
          return;
        this.View.DataControl.SetCurrentItemCore(this.View.FocusedNode != null ? this.View.FocusedNode.Content : (object) null);
      }
      finally
      {
        if (this.View.DataControl != null)
          this.View.DataControl.CurrentItemChangedLocker.Unlock();
        this.View.ScrollIntoViewLocker.Unlock();
        this.ClearCurrentFocus();
        if (flag)
          this.DataProvider.ResetCurrentPosition();
      }
    }

    public bool SaveNodesState(bool supressLocker = false)
    {
      if (supressLocker || !this.nodesStateLocker.IsLocked)
      {
        this.SaveNodesStateCore();
        if (!supressLocker)
          this.nodesStateLocker.Lock();
        return true;
      }
      this.nodesStateLocker.Lock();
      return false;
    }

    protected virtual void SaveNodesStateCore()
    {
      this.ClearNodesState();
      foreach (TreeListNode treeListNode in (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.DataProvider.Nodes))
      {
        if (treeListNode.Content != null && !this.NodesState.ContainsKey(treeListNode.Content))
          this.NodesState.Add(treeListNode.Content, new TreeListNodesState.TreeListNodeState()
          {
            IsExpanded = treeListNode.IsExpanded,
            IsChecked = treeListNode.IsChecked,
            IsSelected = this.DataProvider.Selection.GetSelected(treeListNode.RowHandle),
            SelectedObject = this.DataProvider.Selection.GetSelectedObject(treeListNode.RowHandle)
          });
      }
    }

    public void RestoreNodesState(bool supressLocker = false)
    {
      if (supressLocker)
      {
        this.RestoreNodesStateCore();
      }
      else
      {
        this.nodesStateLocker.Unlock();
        this.nodesStateLocker.DoIfNotLocked((Action) (() => this.RestoreNodesStateCore()));
      }
    }

    protected virtual void RestoreNodesStateCore()
    {
      if (this.NodesState.Count == 0)
        return;
      this.DataProvider.Selection.BeginSelection();
      foreach (TreeListNode treeListNode in (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.DataProvider.Nodes))
      {
        TreeListNodesState.TreeListNodeState treeListNodeState;
        if (this.NodesState.TryGetValue(treeListNode.Content, out treeListNodeState))
        {
          treeListNode.IsExpanded = treeListNodeState.IsExpanded;
          if (string.IsNullOrEmpty(this.View.CheckBoxFieldName))
            treeListNode.IsChecked = treeListNodeState.IsChecked;
          this.DataProvider.Selection.SetSelected(treeListNode.RowHandle, treeListNodeState.IsSelected, treeListNodeState.SelectedObject);
        }
      }
      this.DataProvider.Selection.EndSelection();
      this.ClearNodesState();
    }

    protected class TreeListNodeState
    {
      public bool IsExpanded { get; set; }

      public bool? IsChecked { get; set; }

      public bool IsSelected { get; set; }

      public object SelectedObject { get; set; }
    }
  }
}
