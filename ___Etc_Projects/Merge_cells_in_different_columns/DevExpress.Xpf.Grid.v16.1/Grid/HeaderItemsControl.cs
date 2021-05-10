// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.HeaderItemsControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid.HitTest;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace DevExpress.Xpf.Grid
{
  public class HeaderItemsControl : CachedItemsControl
  {
    protected virtual bool CanSyncWidth
    {
      get
      {
        return true;
      }
    }

    protected override FrameworkElement CreateChild(object item)
    {
      GridColumnHeader gridColumnHeader1 = new GridColumnHeader();
      gridColumnHeader1.IsTabStop = false;
      gridColumnHeader1.CanSyncWidth = this.CanSyncWidth;
      gridColumnHeader1.CanSyncColumnPosition = true;
      gridColumnHeader1.DataContext = (object) null;
      GridColumnHeader gridColumnHeader2 = gridColumnHeader1;
      GridViewHitInfoBase.SetHitTestAcceptor((DependencyObject) gridColumnHeader2, (DataViewHitTestAcceptorBase) new ColumnHeaderTableViewHitTestAcceptor());
      return (FrameworkElement) gridColumnHeader2;
    }

    protected override void ValidateElement(FrameworkElement element, object item)
    {
      base.ValidateElement(element, item);
      GridColumnData gridColumnData = item as GridColumnData;
      if (gridColumnData == null || gridColumnData.Column == null || gridColumnData.Column.View == null)
        return;
      BarManager.SetDXContextMenu((UIElement) element, (IPopupControl) gridColumnData.View.DataControlMenu);
      DataControlPopupMenu.SetGridMenuType((DependencyObject) element, new GridMenuType?(GridMenuType.Column));
      BaseColumn.SetVisibleIndex((DependencyObject) element, gridColumnData.Column.VisibleIndex);
      ColumnBase.SetHeaderPresenterType((DependencyObject) element, HeaderPresenterType.Headers);
      BaseGridHeader.SetGridColumn((DependencyObject) element, (BaseColumn) gridColumnData.Column);
      element.DataContext = (object) gridColumnData.Column;
      ColumnBase column = gridColumnData.Column;
      ((ButtonBase) element).Command = column.View.GetColumnCommand(column);
    }
  }
}
