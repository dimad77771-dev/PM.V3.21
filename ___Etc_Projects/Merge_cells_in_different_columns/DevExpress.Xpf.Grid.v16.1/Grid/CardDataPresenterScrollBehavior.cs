// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CardDataPresenterScrollBehavior
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class CardDataPresenterScrollBehavior : DataPresenterScrollBehavior
  {
    public override void ScrollToHorizontalOffset(DependencyObject source, double offset)
    {
      CardDataPresenter cardDataPresenter = this.GetDataPresenter(source) as CardDataPresenter;
      if (cardDataPresenter != null)
      {
        switch (((CardView) cardDataPresenter.View).Orientation)
        {
          case Orientation.Horizontal:
            this.ScrollToHorizontalOffsetCore(source, offset, !cardDataPresenter.View.ViewBehavior.AllowPerPixelScrolling);
            return;
          case Orientation.Vertical:
            base.ScrollToHorizontalOffset(source, offset);
            return;
        }
      }
      base.ScrollToHorizontalOffset(source, offset);
    }

    public override void ScrollToVerticalOffset(DependencyObject source, double offset)
    {
      CardDataPresenter cardDataPresenter = this.GetDataPresenter(source) as CardDataPresenter;
      if (cardDataPresenter == null)
        base.ScrollToVerticalOffset(source, offset);
      else if (((CardView) cardDataPresenter.View).Orientation == Orientation.Horizontal)
      {
        if (offset > 0.0 && !cardDataPresenter.CanScrollUp() || offset < 0.0 && !cardDataPresenter.CanScrollDown())
          this.ScrollToHorizontalOffset(source, -offset);
        else
          this.ScrollToVerticalOffsetCore(source, -offset, false);
      }
      else
        this.ScrollToVerticalOffsetCore(source, offset, true);
    }

    public override bool CanScrollDown(DependencyObject source)
    {
      if (base.CanScrollDown(source))
        return true;
      CardDataPresenter cardDataPresenter = this.GetDataPresenter(source) as CardDataPresenter;
      if (cardDataPresenter != null && ((CardView) cardDataPresenter.View).Orientation == Orientation.Horizontal)
        return this.CanScrollRight(source);
      return false;
    }

    public override bool CanScrollUp(DependencyObject source)
    {
      if (base.CanScrollUp(source))
        return true;
      CardDataPresenter cardDataPresenter = this.GetDataPresenter(source) as CardDataPresenter;
      if (cardDataPresenter != null && ((CardView) cardDataPresenter.View).Orientation == Orientation.Horizontal)
        return this.CanScrollLeft(source);
      return false;
    }
  }
}
