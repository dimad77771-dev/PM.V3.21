// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CardDataPresenter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Grid.Hierarchy;
using DevExpress.Xpf.Grid.HitTest;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class CardDataPresenter : DataPresenterManipulation
  {
    private CardView CardView
    {
      get
      {
        return (CardView) this.View;
      }
    }

    private CardsHierarchyPanel CardsPanel
    {
      get
      {
        return this.Panel as CardsHierarchyPanel;
      }
    }

    protected internal override int GenerateItemsOffset
    {
      get
      {
        if (this.CardsPanel == null)
          return this.ScrollOffset;
        return this.CardsPanel.GenerateItemsOffset;
      }
    }

    protected override double Extent
    {
      get
      {
        if (this.CardsPanel == null)
          return base.Extent;
        return this.CardsPanel.Extent;
      }
    }

    protected override DataControlScrollMode VerticalScrollModeCore
    {
      get
      {
        if (this.CardView.Orientation == Orientation.Horizontal)
          return DataControlScrollMode.Pixel;
        return !this.View.ViewBehavior.AllowPerPixelScrolling ? DataControlScrollMode.Item : DataControlScrollMode.RowPixel;
      }
    }

    protected override DataControlScrollMode HorizontalScrollModeCore
    {
      get
      {
        if (this.CardView.Orientation == Orientation.Vertical)
          return DataControlScrollMode.Pixel;
        return !this.View.ViewBehavior.AllowPerPixelScrolling ? DataControlScrollMode.Item : DataControlScrollMode.RowPixel;
      }
    }

    public CardDataPresenter()
    {
      GridViewHitInfoBase.SetHitTestAcceptor((DependencyObject) this, (DataViewHitTestAcceptorBase) new DataAreaTableViewHitTestAcceptor());
    }

    protected override void OnDefineScrollInfoChangedCore()
    {
      base.OnDefineScrollInfoChangedCore();
      if (this.CardsPanel == null)
        return;
      this.CardsPanel.OnDefineScrollInfoChanged();
    }

    protected override FrameworkElement CreateContent()
    {
      return (FrameworkElement) new CardsContainer() { DataPresenter = (DataPresenterBase) this };
    }

    protected override void UpdateViewCore()
    {
      if (this.Content == null)
        return;
      ((FrameworkElement) this.Content).DataContext = (object) this.View;
    }

    protected override Size GetMeasureSize(Size constraint)
    {
      return constraint;
    }

    protected override bool OnLayoutUpdatedCore()
    {
      if (this.CardsPanel.Return<CardsHierarchyPanel, bool>((Func<CardsHierarchyPanel, bool>) (x => x.OnLayoutUpdated()), (Func<bool>) (() => true)))
        return base.OnLayoutUpdatedCore();
      return false;
    }

    protected override VirtualDataStackPanelScrollInfo CreateScrollInfo()
    {
      VirtualDataStackPanelScrollInfo scrollInfo = base.CreateScrollInfo();
      BindingOperations.SetBinding((DependencyObject) scrollInfo, VirtualDataStackPanelScrollInfo.OrientationProperty, (BindingBase) new Binding(CardView.OrientationProperty.Name)
      {
        Source = (object) this.View
      });
      return scrollInfo;
    }

    protected override Size GetFirstElementSize()
    {
      if (this.CardsPanel == null || this.CardsPanel.RowsInfo == null || this.CardsPanel.RowsInfo.Count == 0)
        return Size.Empty;
      return this.CardsPanel.RowsInfo[0].RenderSize;
    }

    protected override double DefineDelta(Point translation, Size firstElementSize)
    {
      if (this.View.ViewBehavior.AllowPerPixelScrolling)
        return this.SizeHelper.GetDefinePoint(translation) / this.SizeHelper.GetDefineSize(firstElementSize);
      if (this.CardView.CardLayout == CardLayout.Columns)
        return translation.X / firstElementSize.Width * 3.0;
      return translation.Y / firstElementSize.Height * 3.0;
    }

    protected override double GetTranslation(double delta, Size firstElementSize)
    {
      if (this.CardView.CardLayout == CardLayout.Columns)
      {
        if (!this.View.ViewBehavior.AllowPerPixelScrolling)
          return delta * firstElementSize.Width / 3.0;
        return delta;
      }
      if (!this.View.ViewBehavior.AllowPerPixelScrolling)
        return delta * firstElementSize.Height / 3.0;
      return delta;
    }

    protected override void ChangeOffset(DataViewBehavior behavior, double delta, Point translation)
    {
      behavior.ChangeHorizontalOffsetBy(this.CardView.CardLayout == CardLayout.Columns ? -delta : -translation.X);
      behavior.ChangeVerticalOffsetBy(this.CardView.CardLayout == CardLayout.Columns ? -translation.Y : -delta);
    }

    protected override Point GetAccumulator(double translation)
    {
      if (this.CardView.CardLayout != CardLayout.Columns)
        return new Point(0.0, translation);
      return new Point(translation, 0.0);
    }

    protected override Size ArrangeOverride(Size arrangeBounds)
    {
      return base.ArrangeOverride(arrangeBounds);
    }

    protected override void OnMouseWheelDown()
    {
      if (!this.CanScrollDown())
        this.ScrollInfo.MouseWheelRight();
      else
        this.ScrollInfo.MouseWheelDown();
    }

    protected override void OnMouseWheelUp()
    {
      if (!this.CanScrollUp())
        this.ScrollInfo.MouseWheelLeft();
      else
        this.ScrollInfo.MouseWheelUp();
    }

    protected internal bool CanScrollDown()
    {
      return this.ScrollInfoCore.VerticalScrollInfo.Extent > this.ScrollInfoCore.VerticalScrollInfo.Viewport;
    }

    protected internal bool CanScrollUp()
    {
      return this.ScrollInfoCore.VerticalScrollInfo.Extent > this.ScrollInfoCore.VerticalScrollInfo.Viewport;
    }
  }
}
