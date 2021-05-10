// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.SelectionRectangleCardViewHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;
using System.Windows.Controls.Primitives;

namespace DevExpress.Xpf.Grid.Native
{
  public class SelectionRectangleCardViewHelper : SelectionRectangleHelper
  {
    private SelectionRectangleCardViewHelper.StartPointInfo startClickPoint;
    private double scrollOffsetVertical;
    private double scrollOffsetHorizontal;

    public override void OnMouseDown(DataViewBase view, int rowHandle = 0, ColumnBase column = null)
    {
      CardView cardView = view as CardView;
      this.startPointView = this.endPointView = this.GetTransformPoint(view, view.ViewBehavior);
      if (cardView.CardRowInfoCollection.Count == 0)
        return;
      int numberRow = -1;
      double getRowOffset = cardView.GetRowOffset;
      double verticalOffset = 0.0;
      double horizontalOffset = 0.0;
      IScrollInfo scrollInfo = (IScrollInfo) view.DataPresenter;
      switch (cardView.CardLayout)
      {
        case CardLayout.Rows:
          do
          {
            ++numberRow;
            getRowOffset += cardView.CardRowInfoCollection[numberRow].Size.Height;
          }
          while (getRowOffset < view.ViewBehavior.LastMousePosition.Y && numberRow < cardView.CardRowInfoCollection.Count - 1);
          verticalOffset = view.ViewBehavior.LastMousePosition.Y - (getRowOffset - cardView.CardRowInfoCollection[numberRow].Size.Height);
          numberRow += (int) scrollInfo.VerticalOffset;
          break;
        case CardLayout.Columns:
          do
          {
            ++numberRow;
            getRowOffset += cardView.CardRowInfoCollection[numberRow].Size.Width;
          }
          while (getRowOffset < view.ViewBehavior.LastMousePosition.X && numberRow < cardView.CardRowInfoCollection.Count - 1);
          horizontalOffset = view.ViewBehavior.LastMousePosition.X - (getRowOffset - cardView.CardRowInfoCollection[numberRow].Size.Width);
          numberRow += (int) scrollInfo.HorizontalOffset;
          break;
      }
      this.startClickPoint = new SelectionRectangleCardViewHelper.StartPointInfo(numberRow, verticalOffset, horizontalOffset);
      this.UpdateSelectionRect(view);
      this.scrollOffsetVertical = scrollInfo.VerticalOffset;
      this.scrollOffsetHorizontal = scrollInfo.HorizontalOffset;
    }

    public override void UpdateSelection(DataViewBase view, DataViewBehavior behavior, double indicatorWidth = 0.0)
    {
      CardView cardView = (CardView) view;
      if (cardView.CardRowInfoCollection.Count == 0)
        return;
      Point minTransformPoint = SelectionRectangleHelper.GetMinTransformPoint(view, 0.0);
      Point maxTransformPoint = SelectionRectangleHelper.GetMaxTransformPoint(view);
      this.endPointView = this.GetTransformPoint(view, behavior);
      IScrollInfo scrollInfo = (IScrollInfo) view.DataPresenter;
      switch (cardView.CardLayout)
      {
        case CardLayout.Rows:
          if (this.startClickPoint.NumberRow >= (int) scrollInfo.VerticalOffset && (double) this.startClickPoint.NumberRow < scrollInfo.VerticalOffset + scrollInfo.ViewportHeight)
          {
            double getRowOffset = cardView.GetRowOffset;
            for (int index = 0; index < cardView.CardRowInfoCollection.Count && this.startClickPoint.NumberRow > (int) scrollInfo.VerticalOffset + index; ++index)
              getRowOffset += cardView.CardRowInfoCollection[index].Size.Height;
            this.startPointView.Y = this.GetTransformPoint(view, new Point(0.0, getRowOffset + this.startClickPoint.VerticalOffset)).Y;
            break;
          }
          this.startPointView.Y = scrollInfo.VerticalOffset > this.scrollOffsetVertical ? minTransformPoint.Y : maxTransformPoint.Y;
          break;
        case CardLayout.Columns:
          if (this.startClickPoint.NumberRow >= (int) scrollInfo.HorizontalOffset && (double) this.startClickPoint.NumberRow < scrollInfo.HorizontalOffset + scrollInfo.ViewportWidth)
          {
            double getRowOffset = cardView.GetRowOffset;
            for (int index = 0; index < cardView.CardRowInfoCollection.Count && this.startClickPoint.NumberRow > (int) scrollInfo.HorizontalOffset + index; ++index)
              getRowOffset += cardView.CardRowInfoCollection[index].Size.Width;
            this.startPointView.X = this.GetTransformPoint(view, new Point(getRowOffset + this.startClickPoint.HorizontalOffset, 0.0)).X;
            break;
          }
          this.startPointView.X = scrollInfo.HorizontalOffset > this.scrollOffsetHorizontal ? minTransformPoint.X : maxTransformPoint.X;
          break;
      }
      this.ValidateAndUpdateRectangle(minTransformPoint, maxTransformPoint, view);
    }

    private class StartPointInfo
    {
      private readonly int _numberRow;
      private readonly double _verticalOffset;
      private readonly double _horizontalOffset;

      public int NumberRow
      {
        get
        {
          return this._numberRow;
        }
      }

      public double VerticalOffset
      {
        get
        {
          return this._verticalOffset;
        }
      }

      public double HorizontalOffset
      {
        get
        {
          return this._horizontalOffset;
        }
      }

      public StartPointInfo(int numberRow, double verticalOffset, double horizontalOffset)
      {
        this._numberRow = numberRow;
        this._verticalOffset = verticalOffset;
        this._horizontalOffset = horizontalOffset;
      }
    }
  }
}
