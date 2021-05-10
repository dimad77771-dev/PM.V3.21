// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.BandedGridViewExportHelper`2
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.XtraExport.Helpers;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Grid.Printing
{
  public class BandedGridViewExportHelper<TCol, TRow> : GridViewExportHelper<TCol, TRow>, IBandedGridView<TCol, TRow>, IGridView<TCol, TRow>, IGridViewBase<TCol, TRow, TCol, TRow> where TCol : ColumnWrapper where TRow : RowBaseWrapper
  {
    private int bandRowCount;
    private List<TCol> groupedColumns;
    private List<TCol> bands;

    public override bool ShowGroupedColumns
    {
      get
      {
        return true;
      }
    }

    public int BandRowCount
    {
      get
      {
        return this.bandRowCount;
      }
    }

    public virtual int HeaderRowCount
    {
      get
      {
        return this.BandRowCount;
      }
    }

    IBandedViewAppearance IBandedGridView<TCol, TRow>.BandedViewAppearance
    {
      get
      {
        return (IBandedViewAppearance) new GridAppearanceWrapper();
      }
    }

    IBandedViewAppearance IBandedGridView<TCol, TRow>.BandedViewAppearancePrint
    {
      get
      {
        return (IBandedViewAppearance) new GridAppearanceWrapper();
      }
    }

    IBandedGridOptionsView IBandedGridView<TCol, TRow>.BandedGridOptionsView
    {
      get
      {
        return (IBandedGridOptionsView) new BandedGridOptionsViewWrapper<TCol, TRow>((DataViewExportHelperBase<TCol, TRow>) this);
      }
    }

    public BandedGridViewExportHelper(TableView view, ExportTarget target)
      : base(view, target)
    {
      this.groupedColumns = this.GetPrintableGroupedColumns();
      this.bands = this.GetPrintableBands();
      this.bandRowCount = this.CalcBandRowCount();
    }

    protected List<TCol> GetPrintableGroupedColumns()
    {
      return this.View.GroupedColumns.Select<GridColumn, TCol>((Func<GridColumn, TCol>) (col => (TCol) new BandedColumnWrapper((ColumnBase) col, col.VisibleIndex))).ToList<TCol>();
    }

    protected List<TCol> GetPrintableBands()
    {
      return this.DataControl.BandsLayoutCore.PrintableBands.Select<BandBase, TCol>((Func<BandBase, TCol>) (band => (TCol) new BandWrapper(band, band.VisibleIndex))).ToList<TCol>();
    }

    public override IEnumerable<TCol> GetAllColumns()
    {
      return (IEnumerable<TCol>) this.bands;
    }

    public override IEnumerable<TCol> GetGroupedColumns()
    {
      return (IEnumerable<TCol>) this.groupedColumns;
    }

    protected override TCol CreateColumnCore(ColumnBase column, int logicalPosition)
    {
      return (TCol) new BandedColumnWrapper(column, logicalPosition);
    }

    protected override int CalcBandRowCount()
    {
      return this.CalcBandRowCountCore(true);
    }

    protected override int CalcColumnRowCount()
    {
      return this.CalcColumnRowCountCore(true);
    }
  }
}
