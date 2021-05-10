// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListBestFitRowControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class TreeListBestFitRowControl : BestFitRowControl
  {
    private bool allowDefaultContent;

    protected BestFitGridCellContentPresenter ContentPresenter { get; private set; }

    public TreeListBestFitRowControl(RowData rowData, GridColumnData cellData, bool allowDefaultContent)
      : base(rowData, cellData)
    {
      this.allowDefaultContent = allowDefaultContent;
    }

    protected override void CreateTemplateContent()
    {
      if (this.allowDefaultContent)
      {
        BestFitGridCellContentPresenter contentPresenter = new BestFitGridCellContentPresenter();
        contentPresenter.HasRightSibling = true;
        this.ContentPresenter = contentPresenter;
        this.ContentPresenter.DataContext = (object) this.CellData;
        this.ContentPresenter.Column = this.CellData.Column;
        this.ContentPresenter.RowData = this.rowData;
        this.ContentPresenter.Style = this.CellData.Column.ActualCellStyle;
        this.AddPanelElement((FrameworkElement) this.ContentPresenter, 0);
      }
      else
        base.CreateTemplateContent();
    }

    protected internal virtual void UpdateIsFocusedCell(bool isFocusedCell)
    {
      if (this.ContentPresenter == null)
        return;
      this.ContentPresenter.IsFocusedCell = isFocusedCell;
    }
  }
}
