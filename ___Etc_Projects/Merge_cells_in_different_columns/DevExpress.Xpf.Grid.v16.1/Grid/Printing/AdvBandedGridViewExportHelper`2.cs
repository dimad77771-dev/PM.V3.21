// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.AdvBandedGridViewExportHelper`2
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.XtraExport.Helpers;
using DevExpress.XtraPrinting;

namespace DevExpress.Xpf.Grid.Printing
{
  public class AdvBandedGridViewExportHelper<TCol, TRow> : BandedGridViewExportHelper<TCol, TRow>, IAdvancedBandedGridView<TCol, TRow>, IBandedGridView<TCol, TRow>, IGridView<TCol, TRow>, IGridViewBase<TCol, TRow, TCol, TRow> where TCol : ColumnWrapper where TRow : RowBaseWrapper
  {
    private int columnPanelRowCount;

    public int ColumnPanelRowsCount
    {
      get
      {
        return this.columnPanelRowCount;
      }
    }

    public override int HeaderRowCount
    {
      get
      {
        return base.HeaderRowCount + this.ColumnPanelRowsCount - 1;
      }
    }

    IAdvBandedOptionsView IAdvancedBandedGridView<TCol, TRow>.AdvBandedOptionsView
    {
      get
      {
        return (IAdvBandedOptionsView) new BandedGridOptionsViewWrapper<TCol, TRow>((DataViewExportHelperBase<TCol, TRow>) this);
      }
    }

    public AdvBandedGridViewExportHelper(TableView view, ExportTarget target)
      : base(view, target)
    {
      this.columnPanelRowCount = this.CalcColumnRowCount();
    }
  }
}
