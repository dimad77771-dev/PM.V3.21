// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListViewRowNavigation
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListViewRowNavigation : GridViewRowNavigation
  {
    protected TreeListView TreeListView
    {
      get
      {
        return this.View as TreeListView;
      }
    }

    public TreeListViewRowNavigation(DataViewBase view)
      : base(view)
    {
    }

    protected internal override void ProcessKey(KeyEventArgs e)
    {
      bool flag = false;
      if (e.Key == Key.Multiply)
        flag = this.OnMultiply();
      else if (e.Key == Key.Space)
        flag = this.OnSpace();
      if (flag)
        e.Handled = true;
      else
        base.ProcessKey(e);
    }

    public override void OnRight(bool isCtrlPressed)
    {
      bool nodesOnNavigation = this.TreeListView.ShouldExpandCollapseNodesOnNavigation;
      if (!isCtrlPressed && !nodesOnNavigation || (this.OnPlus(true) || !nodesOnNavigation) || (this.TreeListView.FocusedNode == null || !this.TreeListView.FocusedNode.IsTogglable))
        return;
      this.OnDown();
    }

    public override void OnLeft(bool isCtrlPressed)
    {
      bool nodesOnNavigation = this.TreeListView.ShouldExpandCollapseNodesOnNavigation;
      if (!isCtrlPressed && !nodesOnNavigation || (this.OnMinus(true) || !nodesOnNavigation) || (this.TreeListView.FocusedNode == null || this.TreeListView.FocusedNode.ParentNode == null))
        return;
      this.TreeListView.FocusedNode = this.TreeListView.FocusedNode.ParentNode;
    }

    public virtual bool OnMultiply()
    {
      if (this.View.IsInvalidFocusedRowHandle || this.View.IsExpanded(this.View.FocusedRowHandle) || this.View.IsKeyboardFocusInSearchPanel())
        return false;
      return this.TreeListView.ExpandNodeAndAllChildren();
    }

    public virtual bool OnSpace()
    {
      if (this.View.IsInvalidFocusedRowHandle || !this.TreeListView.ShowCheckboxes || (!this.TreeListView.GetNodeByRowHandle(this.View.FocusedRowHandle).IsCheckBoxEnabled || this.View.IsKeyboardFocusInSearchPanel()))
        return false;
      return this.TreeListView.ChangeNodeCheckState(this.View.FocusedRowHandle);
    }

    protected internal override void ProcessMouse(DependencyObject originalSource)
    {
      if (this.TreeListView.FindRowMarginControl(originalSource) != null && !(originalSource is Image))
        return;
      base.ProcessMouse(originalSource);
    }

    public override void OnTab(bool isShiftPressed)
    {
      if (isShiftPressed)
        this.OnUp(true);
      else
        this.OnDown();
    }
  }
}
