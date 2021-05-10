// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.MouseMoveSelectionRectangleCard
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Utils;
using DevExpress.Xpf.Grid.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid.Native
{
  public class MouseMoveSelectionRectangleCard : MouseMoveSelectionCardBase
  {
    public static readonly MouseMoveSelectionRectangleCard Instance = new MouseMoveSelectionRectangleCard();
    private SelectionRectangleCardViewHelper helper = new SelectionRectangleCardViewHelper();
    private bool isFirst;
    private Point startPointFirst;

    public override bool CanScrollHorizontally
    {
      get
      {
        return true;
      }
    }

    public override bool CanScrollVertically
    {
      get
      {
        return true;
      }
    }

    public override bool AllowNavigation
    {
      get
      {
        return false;
      }
    }

    public override void OnMouseDown(DataViewBase cardView, IDataViewHitInfo hitInfo)
    {
      this.isFirst = true;
      cardView.SelectionAnchor = new SelectionAnchorCell(cardView, hitInfo.RowHandle, (ColumnBase) null);
      cardView.ScrollTimer.Start();
      this.helper.OnMouseDown(cardView, 0, (ColumnBase) null);
      this.startPointFirst = this.helper.StartPoint;
    }

    private void UpdateSelectionCore(DataViewBase cardView, IEnumerable<int> rowHandles)
    {
      this.CaptureMouse(cardView);
      cardView.SelectionStrategy.SelectRowRange(rowHandles);
      cardView.SelectionOldCell = new SelectionAnchorCell(cardView, rowHandles.Count<int>() > 0 ? rowHandles.Min() : 0, (ColumnBase) null);
    }

    public override void UpdateSelection(DataViewBase cardView)
    {
      if (this.isFirst)
      {
        Point transformPoint = this.helper.GetTransformPoint(cardView, cardView.ViewBehavior.LastMousePosition);
        if (Math.Abs(this.startPointFirst.X - transformPoint.X) <= SystemParameters.MinimumVerticalDragDistance && Math.Abs(this.startPointFirst.Y - transformPoint.Y) <= SystemParameters.MinimumHorizontalDragDistance)
        {
          this.helper.OnMouseDown(cardView, 0, (ColumnBase) null);
          return;
        }
        this.isFirst = false;
      }
      if (cardView.GetViewAndVisibleIndex(cardView.ViewBehavior.LastMousePosition.Y).Value < 0)
        return;
      this.CaptureMouse(cardView);
      if (this.helper.StartPoint == this.helper.GetTransformPoint(cardView, cardView.ViewBehavior.LastMousePosition))
        return;
      this.helper.UpdateSelection(cardView, cardView.ViewBehavior, 0.0);
      List<CardRowInfo> rowInfoCollection = ((CardView) cardView).CardRowInfoCollection;
      Rect rect1 = new Rect(this.helper.StartPoint, this.helper.EndPoint);
      Point minTransformPoint = SelectionRectangleHelper.GetMinTransformPoint(cardView, 0.0);
      Point maxTransformPoint = SelectionRectangleHelper.GetMaxTransformPoint(cardView);
      cardView.BeginSelectionCore();
      HashSet<int> intSet = new HashSet<int>((IEnumerable<int>) cardView.DataControl.GetSelectedRowHandles());
      foreach (CardRowInfo cardRowInfo in rowInfoCollection)
      {
        foreach (IItem element in (IEnumerable<IItem>) cardRowInfo.Elements)
        {
          if (element.IsRowVisible)
          {
            Rect rectangleFromElement = this.GetRectangleFromElement(element.Element, cardView);
            if (rectangleFromElement.X + rectangleFromElement.Width > minTransformPoint.X && rectangleFromElement.X <= maxTransformPoint.X && (rectangleFromElement.Y + rectangleFromElement.Height > minTransformPoint.Y && rectangleFromElement.Y <= maxTransformPoint.Y))
            {
              Rect rect = Rect.Intersect(rect1, rectangleFromElement);
              if (rect.Width > 0.0 || rect.Height > 0.0)
                intSet.Add(DataViewBase.GetRowHandle((DependencyObject) element.Element).Value);
              else
                intSet.Remove(DataViewBase.GetRowHandle((DependencyObject) element.Element).Value);
            }
          }
        }
      }
      this.UpdateSelectionCore(cardView, (IEnumerable<int>) intSet);
      cardView.EndSelectionCore();
    }

    public override void OnMouseUp(DataViewBase cardView)
    {
      cardView.ScrollTimer.Stop();
      this.helper.OnMouseUp(cardView);
    }

    public override void CaptureMouse(DataViewBase cardView)
    {
      if (MouseHelper.Captured == cardView)
        return;
      if (cardView.IsKeyboardFocusWithin)
        MouseHelper.Capture((UIElement) cardView);
      else
        cardView.StopSelection();
    }

    private Rect GetRectangleFromElement(FrameworkElement element, DataViewBase cardView)
    {
      return new Rect(element.TransformToAncestor((Visual) cardView.DataControl).Transform(new Point(0.0, 0.0)), element.RenderSize);
    }
  }
}
