// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListViewCellNavigation
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListViewCellNavigation : GridViewCellNavigation
  {
    protected TreeListView TreeListView
    {
      get
      {
        return this.View as TreeListView;
      }
    }

    public TreeListViewCellNavigation(DataViewBase view)
      : base(view)
    {
    }

    protected internal override void ProcessKey(KeyEventArgs e)
    {
      switch (e.Key)
      {
        case Key.Space:
          e.Handled = this.OnSpace();
          break;
        case Key.Multiply:
          e.Handled = this.OnMultiply();
          break;
      }
      base.ProcessKey(e);
    }

    public override void OnRight(bool isCtrlPressed)
    {
      bool flag = this.TreeListView.ShouldExpandCollapseNodesOnNavigation && this.View.ViewBehavior.NavigationStrategyBase.IsEndNavigationIndex(this.View);
      if ((isCtrlPressed || flag) && !this.View.IsEditing)
      {
        this.OnPlus(true);
        if (!flag)
          return;
        this.View.MoveNextCell(false, false);
      }
      else if (this.UseAdvHorzNavigation)
        this.DoBandedViewHorzNavigation(true);
      else
        this.View.MoveNextCell(false, false);
    }

    public override void OnLeft(bool isCtrlPressed)
    {
      bool flag = this.TreeListView.ShouldExpandCollapseNodesOnNavigation && this.View.ViewBehavior.NavigationStrategyBase.IsBeginNavigationIndex(this.View);
      if ((isCtrlPressed || flag) && !this.View.IsEditing)
      {
        this.OnMinus(true);
        if (!flag)
          return;
        this.View.MovePrevCell(false);
      }
      else if (this.UseAdvHorzNavigation)
        this.DoBandedViewHorzNavigation(false);
      else
        this.View.MovePrevCell(false);
    }

    public override bool OnMinus(bool isCtrlPressed)
    {
      if (this.View.IsEditing || this.View.IsKeyboardFocusInSearchPanel())
        return false;
      return base.OnMinus(isCtrlPressed);
    }

    public override bool OnPlus(bool isCtrlPressed)
    {
      if (this.View.IsEditing || this.View.IsKeyboardFocusInSearchPanel())
        return false;
      return base.OnPlus(isCtrlPressed);
    }

    public virtual bool OnMultiply()
    {
      if (this.View.IsInvalidFocusedRowHandle || this.View.IsExpanded(this.View.FocusedRowHandle) || (this.View.IsEditing || this.View.IsKeyboardFocusInSearchPanel()))
        return false;
      return this.TreeListView.ExpandNodeAndAllChildren();
    }

    public virtual bool OnSpace()
    {
      if (this.View.IsInvalidFocusedRowHandle || this.View.IsEditing || (!this.TreeListView.ShowCheckboxes || !this.TreeListView.GetNodeByRowHandle(this.View.FocusedRowHandle).IsCheckBoxEnabled) || this.View.IsKeyboardFocusInSearchPanel())
        return false;
      return this.TreeListView.ChangeNodeCheckState(this.View.FocusedRowHandle);
    }

    protected internal override void ProcessMouse(DependencyObject originalSource)
    {
      if (this.TreeListView.FindRowMarginControl(originalSource) != null && !(originalSource is Image))
        return;
      base.ProcessMouse(originalSource);
    }
  }
}
