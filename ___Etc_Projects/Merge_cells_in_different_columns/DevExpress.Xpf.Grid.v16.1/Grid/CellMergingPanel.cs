// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CellMergingPanel
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using System.Collections.Generic;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class CellMergingPanel : StackVisibleIndexPanel
  {
    private RowData RowData
    {
      get
      {
        return this.DataContext as RowData;
      }
    }

    private TableView View
    {
      get
      {
        return this.RowData.View as TableView;
      }
    }

    protected override Size ArrangeSortedChildrenOverride(Size finalSize, IList<UIElement> sortedChildren)
    {
      double x = 0.0;
      foreach (LightweightCellEditor sortedChild in (IEnumerable<UIElement>) sortedChildren)
      {
        int indexByHandleCore = this.View.DataControl.GetRowVisibleIndexByHandleCore(this.RowData.RowHandle.Value);
        Rect finalRect = new Rect(x, 0.0, sortedChild.DesiredSize.Width, finalSize.Height);
        if (this.View.IsNextRowCellMerged(indexByHandleCore, sortedChild.Column, true))
          finalRect.Y = double.MinValue;
        else if (this.RowData.IsRowInView())
        {
          for (; this.View.IsPrevRowCellMerged(indexByHandleCore, sortedChild.Column, true); --indexByHandleCore)
          {
            FrameworkElement elementByRowHandle = this.View.GetRowElementByRowHandle(this.View.DataControl.GetRowHandleByVisibleIndexCore(indexByHandleCore - 1));
            finalRect.Y -= elementByRowHandle.DesiredSize.Height;
            finalRect.Height += elementByRowHandle.DesiredSize.Height;
          }
        }
        sortedChild.Arrange(finalRect);
        x += finalRect.Width;
      }
      return finalSize;
    }

    protected override Size MeasureSortedChildrenOverride(Size availableSize, IList<UIElement> sortedChildren)
    {
      Size size = base.MeasureSortedChildrenOverride(availableSize, sortedChildren);
      if (TableViewProperties.GetFixedAreaStyle((DependencyObject) this) == FixedStyle.None)
        return new Size(0.0, size.Height);
      return size;
    }
  }
}
