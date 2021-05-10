// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.CardViewCellNavigation
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid.Native
{
  internal class CardViewCellNavigation : GridViewRowNavigationBase
  {
    protected CardView CardView
    {
      get
      {
        return (CardView) this.View;
      }
    }

    protected Orientation Orientation
    {
      get
      {
        return this.CardView.Orientation;
      }
    }

    public CardViewCellNavigation(CardView view)
      : base((DataViewBase) view)
    {
    }

    public override void OnLeft(bool isCtrlPressed)
    {
      if (this.Orientation == Orientation.Vertical && this.ShouldCollapseRow())
        this.CardView.CollapseFocusedRow();
      else
        this.CardView.MoveLeftCell();
    }

    public override void OnRight(bool isCtrlPressed)
    {
      if (this.Orientation == Orientation.Vertical && this.ShouldExpandRow())
        this.CardView.ExpandFocusedRow();
      else
        this.CardView.MoveRightCell();
    }

    public override void OnUp(bool isCtrlPressed)
    {
      if (this.Orientation == Orientation.Horizontal && this.ShouldExpandRow())
        this.CardView.ExpandFocusedRow();
      else
        this.CardView.MoveUpCell();
    }

    public override void OnDown()
    {
      if (this.Orientation == Orientation.Horizontal && this.ShouldCollapseRow())
        this.CardView.CollapseFocusedRow();
      else
        this.CardView.MoveDownCell();
    }

    public override void OnTab(bool isShiftPressed)
    {
      this.TabNavigation(isShiftPressed);
    }

    protected override bool ShouldCollapseRow()
    {
      if (this.View.CurrentCell == null)
        return base.ShouldCollapseRow();
      return false;
    }

    protected override bool ShouldExpandRow()
    {
      if (this.View.CurrentCell == null)
        return base.ShouldExpandRow();
      return false;
    }

    protected override void SetRowFocus(DependencyObject row, bool focus)
    {
      this.SetRowFocusOnCell(row, focus);
      base.SetRowFocus(row, focus);
    }

    protected internal override void ClearAllStates()
    {
      this.ClearAllCellsStates();
    }

    protected internal override void ProcessMouse(DependencyObject originalSource)
    {
      base.ProcessMouse(originalSource);
      this.ProcessMouseOnCell(originalSource);
    }

    public override bool GetIsFocusedCell(int rowHandle, ColumnBase column)
    {
      if (this.View.FocusedRowHandle == rowHandle)
        return this.View.DataControl.CurrentColumn == column;
      return false;
    }
  }
}
