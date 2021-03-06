// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.LightweightBestFitControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid
{
  public class LightweightBestFitControl : BestFitControlBase
  {
    public LightweightBestFitControl(DataViewBase view, ColumnBase column)
      : base(view, column)
    {
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.Content = (object) this.CreateBestFitRowControl();
    }

    protected virtual BestFitRowControl CreateBestFitRowControl()
    {
      return new BestFitRowControl(this.RowData, this.CellData);
    }
  }
}
