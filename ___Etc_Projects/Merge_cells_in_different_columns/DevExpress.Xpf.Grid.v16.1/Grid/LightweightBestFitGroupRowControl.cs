// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.LightweightBestFitGroupRowControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class LightweightBestFitGroupRowControl : BestFitControlBase, IBestFitGroupRow
  {
    private GroupRowData rowData;

    public bool IsFirstColumn { get; set; }

    public GridGroupSummaryColumnData SummaryData { get; set; }

    public LightweightBestFitGroupRowControl(DataViewBase view, ColumnBase column)
      : base(view, column)
    {
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.UpdateContent();
    }

    public void SetRowData(GroupRowData rowData)
    {
      this.rowData = rowData;
    }

    protected override Size MeasureOverride(Size constraint)
    {
      Size size = base.MeasureOverride(constraint);
      ++size.Width;
      return size;
    }

    public void UpdateContent()
    {
      this.Content = (object) new BestFitGroupRowControl(this.rowData, this.SummaryData, this.IsFirstColumn, this.Column);
    }
  }
}
