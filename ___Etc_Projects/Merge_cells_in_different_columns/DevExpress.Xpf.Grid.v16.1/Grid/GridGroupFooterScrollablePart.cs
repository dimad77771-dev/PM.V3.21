// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridGroupFooterScrollablePart
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class GridGroupFooterScrollablePart : GridScrollablePart
  {
    public static readonly DependencyProperty LevelProperty = DependencyPropertyManager.Register("Level", typeof (int), typeof (GridGroupFooterScrollablePart), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0, (PropertyChangedCallback) ((d, e) => ((GridGroupFooterScrollablePart) d).UpdateMargin())));

    public int Level
    {
      get
      {
        return (int) this.GetValue(GridGroupFooterScrollablePart.LevelProperty);
      }
      set
      {
        this.SetValue(GridGroupFooterScrollablePart.LevelProperty, (object) value);
      }
    }

    protected virtual GroupSummaryRowData RowData
    {
      get
      {
        return this.DataContext as GroupSummaryRowData;
      }
    }

    protected ITableView TableView
    {
      get
      {
        if (this.RowData == null)
          return (ITableView) null;
        return this.RowData.View as ITableView;
      }
    }

    protected override bool HasFixedLeftColumns
    {
      get
      {
        if (this.TableView != null && this.TableView.TableViewBehavior.FixedLeftVisibleColumns != null)
          return this.TableView.TableViewBehavior.FixedLeftVisibleColumns.Count > 0;
        return false;
      }
    }

    private void UpdateMargin()
    {
      this.OnScrollingMarginChanged();
    }

    protected override void OnScrollingMarginChanged()
    {
      if (this.FixedNoneContentCore == null)
        return;
      if (this.TableView != null && !this.HasFixedLeftColumns && (this.TableView.ViewportVisibleColumns != null && this.TableView.ViewportVisibleColumns.Count > 0) && this.TableView.ViewportVisibleColumns[0].VisibleIndex > 0)
        this.FixedNoneContentCore.Margin = new Thickness(this.ScrollingMargin.Left - this.RowData.Offset, this.ScrollingMargin.Top, this.ScrollingMargin.Right, this.ScrollingMargin.Bottom);
      else
        this.FixedNoneContentCore.Margin = this.ScrollingMargin;
    }
  }
}
