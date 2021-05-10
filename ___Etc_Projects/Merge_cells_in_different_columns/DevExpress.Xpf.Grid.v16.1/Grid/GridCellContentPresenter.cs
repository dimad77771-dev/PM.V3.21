// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridCellContentPresenter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Utils;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class GridCellContentPresenter : CellContentPresenter, ISupportLoadingAnimation
  {
    private GridCellData data;
    private LoadingAnimationHelper loadingAnimationHelper;

    internal bool ShouldSyncProperties
    {
      get
      {
        return this.data == null;
      }
    }

    internal LoadingAnimationHelper LoadingAnimationHelper
    {
      get
      {
        if (this.loadingAnimationHelper == null)
          this.loadingAnimationHelper = new LoadingAnimationHelper((ISupportLoadingAnimation) this);
        return this.loadingAnimationHelper;
      }
    }

    public FrameworkElement Element
    {
      get
      {
        return (FrameworkElement) this.Editor;
      }
    }

    public DataViewBase DataView
    {
      get
      {
        return this.View;
      }
    }

    public bool IsGroupRow
    {
      get
      {
        return false;
      }
    }

    public GridCellContentPresenter()
    {
      this.SetDefaultStyleKey(typeof (GridCellContentPresenter));
    }

    internal GridCellContentPresenter(GridCellData data)
      : this()
    {
      this.data = data;
      this.DataContext = (object) data;
    }

    protected internal override void OnIsReadyChanged()
    {
      this.LoadingAnimationHelper.ApplyAnimation();
    }

    protected override void OnRowDataChanged()
    {
      base.OnRowDataChanged();
      if (this.RowData == null)
        return;
      this.IsReady = this.RowData.IsReady;
    }

    internal override void SyncProperties(GridCellData cellData)
    {
      if (cellData.Column == null)
        return;
      this.DataContext = (object) cellData;
      if (this.ShowVerticalLines != ((ITableView) cellData.View).ShowVerticalLines)
        this.ShowVerticalLines = ((ITableView) cellData.View).ShowVerticalLines;
      if (this.ShowHorizontalLines != ((ITableView) cellData.View).ShowHorizontalLines)
        this.ShowHorizontalLines = ((ITableView) cellData.View).ShowHorizontalLines;
      if (this.HasRightSibling != cellData.Column.HasRightSibling)
        this.HasRightSibling = cellData.Column.HasRightSibling;
      if (this.HasLeftSibling != cellData.Column.HasLeftSibling)
        this.HasLeftSibling = cellData.Column.HasLeftSibling;
      if (this.HasTopElement != cellData.Column.HasTopElement)
        this.HasTopElement = cellData.Column.HasTopElement;
      this.Style = cellData.Column.ActualCellStyle;
      this.SyncWidth(cellData);
      this.SyncLeftMargin(cellData);
      this.RowData = cellData.RowData;
      ColumnBase.SetNavigationIndex((DependencyObject) this, BaseColumn.GetVisibleIndex((DependencyObject) cellData.Column));
      this.Column = cellData.Column;
      cellData.OnEditorContentUpdated();
      this.ColumnPosition = cellData.Column.ColumnPosition;
      if (this.IsSelected != cellData.IsSelected)
        this.IsSelected = cellData.IsSelected;
      if (this.SelectionState != cellData.SelectionState)
        this.SelectionState = cellData.SelectionState;
      if (this.IsFocusedCell != cellData.IsFocusedCell)
        this.IsFocusedCell = cellData.IsFocusedCell;
      this.UpdateRowSelectionState();
    }

    private void SyncLeftMargin(GridCellData cellData)
    {
      cellData.SyncLeftMargin((FrameworkElement) this);
    }

    protected virtual void SyncWidth(GridCellData cellData)
    {
      this.Width = cellData.GetActualCellWidth();
    }

    public override void OnApplyTemplate()
    {
      if (this.data != null)
      {
        this.SyncProperties(this.data);
        this.data = (GridCellData) null;
      }
      base.OnApplyTemplate();
    }

    [SpecialName]
    bool ISupportLoadingAnimation.get_IsReady()
    {
      return this.IsReady;
    }
  }
}
