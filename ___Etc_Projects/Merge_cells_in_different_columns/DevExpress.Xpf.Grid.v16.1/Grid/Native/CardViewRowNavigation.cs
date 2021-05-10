// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.CardViewRowNavigation
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows.Controls;

namespace DevExpress.Xpf.Grid.Native
{
  internal class CardViewRowNavigation : GridViewRowNavigationBase
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

    public CardViewRowNavigation(CardView view)
      : base((DataViewBase) view)
    {
    }

    public override void OnLeft(bool isCtrlPressed)
    {
      if (this.Orientation == Orientation.Vertical)
      {
        if (this.ShouldCollapseRow())
          this.CardView.CollapseFocusedRow();
        else
          this.CardView.MovePrevColumnCard();
      }
      else
        this.CardView.MovePrevRowCard();
    }

    public override void OnRight(bool isCtrlPressed)
    {
      if (this.Orientation == Orientation.Vertical)
      {
        if (this.ShouldExpandRow())
          this.CardView.ExpandFocusedRow();
        else
          this.CardView.MoveNextColumnCard();
      }
      else
        this.CardView.MoveNextRowCard();
    }

    public override void OnUp(bool isCtrlPressed)
    {
      if (this.Orientation == Orientation.Vertical)
        this.CardView.MovePrevRowCard();
      else if (this.ShouldExpandRow())
        this.CardView.ExpandFocusedRow();
      else
        this.CardView.MovePrevColumnCard();
    }

    public override void OnDown()
    {
      if (this.Orientation == Orientation.Vertical)
        this.CardView.MoveNextRowCard();
      else if (this.ShouldCollapseRow())
        this.CardView.CollapseFocusedRow();
      else if (this.View.IsExpandableRowFocused())
        this.CardView.MovePrevRow();
      else
        this.CardView.MoveNextColumnCard();
    }
  }
}
