// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.CardViewBehavior
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace DevExpress.Xpf.Grid.Native
{
  public class CardViewBehavior : DataViewBehavior
  {
    private static UseCardLightweightTemplates? DefaultUseLightweightTemplates = new UseCardLightweightTemplates?();
    internal bool canChangeUseLightweightTemplates = true;
    private DispatcherTimer scrollTimer;
    private MouseMoveSelectionCardBase mouseMoveSelection;

    protected internal override DispatcherTimer ScrollTimer
    {
      get
      {
        return this.scrollTimer;
      }
    }

    private CardView CardView
    {
      get
      {
        return (CardView) this.View;
      }
    }

    internal override double HorizontalViewportCore
    {
      get
      {
        return this.CardView.CardsPanelViewPort;
      }
    }

    protected internal override bool AllowPerPixelScrolling
    {
      get
      {
        return this.CardView.ScrollMode == ScrollMode.Pixel;
      }
    }

    private UseCardLightweightTemplates ActualUseLightweightTemplates
    {
      get
      {
        return this.CardView.UseLightweightTemplates ?? ((CardView) this.View.RootView).UseLightweightTemplates ?? CardViewBehavior.DefaultUseLightweightTemplates ?? UseCardLightweightTemplates.GroupRow;
      }
    }

    private MouseMoveSelectionCardBase MouseMoveSelection
    {
      get
      {
        return this.mouseMoveSelection ?? (MouseMoveSelectionCardBase) MouseMoveSelectionCardNone.Instance;
      }
      set
      {
        this.mouseMoveSelection = value;
      }
    }

    private bool IsRowModeSelection
    {
      get
      {
        return this.View.DataControl.SelectionMode == MultiSelectMode.Row;
      }
    }

    public CardViewBehavior(DataViewBase view)
      : base(view)
    {
      this.scrollTimer = new DispatcherTimer();
      this.scrollTimer.Interval = TimeSpan.FromMilliseconds(10.0);
      this.scrollTimer.Tick += new EventHandler(this.OnScrollTimer_Tick);
    }

    protected internal override RowData CreateRowDataCore(DataTreeBuilder treeBuilder, bool updateDataOnly)
    {
      return (RowData) new CardData(treeBuilder);
    }

    protected internal override void OnTopRowIndexChangedCore()
    {
      switch (this.CardView.CardLayout)
      {
        case CardLayout.Rows:
          this.View.DataPresenter.SetVerticalOffsetForce((double) this.View.TopRowIndex);
          break;
        case CardLayout.Columns:
          this.View.DataPresenter.SetHorizontalOffsetForce((double) this.View.TopRowIndex);
          break;
      }
    }

    protected internal override bool OnVisibleColumnsAssigned(bool changed)
    {
      return changed;
    }

    protected internal override void UpdateCellData(ColumnsRowDataBase rowData)
    {
      rowData.ReuseCellDataNotVirtualized((Func<ColumnsRowDataBase, IList<GridColumnData>>) (x => x.CellData), (Action<ColumnsRowDataBase, IList>) ((x, val) => x.CellData = (IList<GridColumnData>) val), this.View.VisibleColumnsCore);
    }

    internal override void UpdateSecondaryScrollInfoCore(double secondaryOffset, bool allowUpdateViewportVisibleColumns)
    {
      Point point = SizeHelperBase.GetDefineSizeHelper(this.CardView.Orientation).CreatePoint(0.0, secondaryOffset);
      this.View.DataPresenter.ContentElement.Margin = new Thickness(point.X, point.Y, 0.0, 0.0);
    }

    internal override GridViewNavigationBase CreateRowNavigation()
    {
      return (GridViewNavigationBase) new CardViewRowNavigation(this.CardView);
    }

    internal override GridViewNavigationBase CreateCellNavigation()
    {
      return (GridViewNavigationBase) new CardViewCellNavigation(this.CardView);
    }

    protected internal override double GetFixedExtent()
    {
      return this.CardView.CardsPanelMaxSize;
    }

    protected internal override void GetDataRowText(StringBuilder sb, int rowHandle)
    {
      this.CardView.GetDataRowTextCore(sb, rowHandle);
    }

    internal static DependencyProperty RegisterUseLightweightTemplatesProperty(Type ownerType)
    {
      return DependencyProperty.Register("UseLightweightTemplates", typeof (UseCardLightweightTemplates?), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((CardViewBehavior) ((DataViewBase) d).ViewBehavior).OnUseLightweightTemplatesChanged())));
    }

    private void ForbidChangeUseLightweightTemplatesProperty()
    {
      this.canChangeUseLightweightTemplates = false;
    }

    internal bool UseLightweightTemplatesHasFlag(UseCardLightweightTemplates flag)
    {
      return this.ActualUseLightweightTemplates.HasFlag((Enum) flag);
    }

    internal FrameworkElement CreateElement(Func<FrameworkElement> lightweightDelegate, Func<FrameworkElement> ordinaryDelegate, UseCardLightweightTemplates flag)
    {
      if (this.canChangeUseLightweightTemplates)
        this.ForbidChangeUseLightweightTemplatesProperty();
      if (this.UseLightweightTemplatesHasFlag(flag))
        return lightweightDelegate();
      return ordinaryDelegate();
    }

    private void OnUseLightweightTemplatesChanged()
    {
      if (!this.canChangeUseLightweightTemplates && !DataViewBase.DisableOptimizedModeVerification)
        throw new InvalidOperationException("Can't change the UseLightweightTemplates property after the GridControl has been initialized.");
      this.View.UpdateColumnsAppearance();
    }

    public virtual void OnScrollTimer_Tick(object sender, EventArgs e)
    {
      if (this.View == null || this.View.ScrollContentPresenter == null || this.LastMousePosition.X == double.NaN || this.LastMousePosition.Y == double.NaN)
        return;
      double delta1 = 0.0;
      double delta2 = 0.0;
      this.DragScroll();
      if (this.MouseMoveSelection.CanScrollVertically)
      {
        double num = this.View.DataPresenter.ScrollInfoCore.VerticalScrollInfo.Viewport / 20.0;
        if (this.LastMousePosition.Y < 0.0)
          delta1 = -num;
        if (this.LastMousePosition.Y > this.View.ScrollContentPresenter.ActualHeight)
          delta1 = num;
      }
      if (this.MouseMoveSelection.CanScrollHorizontally)
      {
        double num = this.View.DataPresenter.ScrollInfoCore.HorizontalScrollInfo.Viewport / 20.0;
        if (this.LastMousePosition.X < 0.0)
          delta2 = -num;
        if (this.LastMousePosition.X > this.View.ScrollContentPresenter.ActualWidth)
          delta2 = num;
      }
      if (delta2 != 0.0)
        this.ChangeHorizontalOffsetBy(delta2);
      if (delta1 != 0.0)
        this.ChangeVerticalOffsetBy(delta1);
      if (delta1 == 0.0 && delta2 == 0.0)
        return;
      this.View.EnqueueImmediateAction((Action) (() => this.MouseMoveSelection.UpdateSelection(this.View)));
    }

    private void DragScroll()
    {
      if (!DragDropScroller.IsDragging((DependencyObject) this.View))
        return;
      if (this.LastMousePosition.X < 0.0)
        this.ChangeHorizontalOffsetBy(-1.0);
      if (this.LastMousePosition.X <= this.View.ScrollContentPresenter.ActualWidth)
        return;
      this.ChangeHorizontalOffsetBy(1.0);
    }

    internal override void OnViewMouseLeave()
    {
      this.MouseMoveSelection.CaptureMouse(this.View);
    }

    internal override void OnViewMouseMove(MouseEventArgs e)
    {
      this.LastMousePosition = e.GetPosition((IInputElement) this.View.ScrollContentPresenter);
      this.MouseMoveSelection.UpdateSelection(this.View);
    }

    internal override void ProcessMouseLeftButtonUp(MouseButtonEventArgs e)
    {
      base.ProcessMouseLeftButtonUp(e);
      this.StopSelection();
    }

    internal override void OnMouseLeftButtonUp()
    {
      this.StopSelection();
    }

    internal override void OnAfterMouseLeftButtonDown(IDataViewHitInfo hitInfo)
    {
      base.OnAfterMouseLeftButtonDown(hitInfo);
      if (this.View.IsEditing || Mouse.RightButton != MouseButtonState.Released)
        return;
      this.MouseMoveSelection = this.GetMouseMoveSelection(hitInfo);
      this.MouseMoveSelection.OnMouseDown(this.View, hitInfo);
    }

    protected internal override void StopSelection()
    {
      this.MouseMoveSelection.OnMouseUp(this.View);
      this.MouseMoveSelection = (MouseMoveSelectionCardBase) null;
      this.MouseMoveSelection.ReleaseMouseCapture(this.View);
    }

    protected virtual MouseMoveSelectionCardBase GetMouseMoveSelection(IDataViewHitInfo hitInfo)
    {
      if (!this.View.AllowMouseMoveSelection)
        return (MouseMoveSelectionCardBase) MouseMoveSelectionCardNone.Instance;
      if (this.View.ShowSelectionRectangle && this.IsRowModeSelection && (hitInfo.IsDataArea || hitInfo.InRow))
        return (MouseMoveSelectionCardBase) MouseMoveSelectionRectangleCard.Instance;
      return (MouseMoveSelectionCardBase) MouseMoveSelectionCardNone.Instance;
    }
  }
}
