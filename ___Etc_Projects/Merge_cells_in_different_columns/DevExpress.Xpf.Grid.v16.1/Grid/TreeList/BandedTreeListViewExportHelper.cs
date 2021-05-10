// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.BandedTreeListViewExportHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Printing;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class BandedTreeListViewExportHelper : TreeListViewExportHelper, IBandedGridView<ColumnWrapper, TreeListNodeWrapper>, IGridView<ColumnWrapper, TreeListNodeWrapper>, IGridViewBase<ColumnWrapper, TreeListNodeWrapper, ColumnWrapper, TreeListNodeWrapper>
  {
    private int bandRowCount;
    private List<ColumnWrapper> bands;

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

    IBandedViewAppearance IBandedGridView<ColumnWrapper, TreeListNodeWrapper>.BandedViewAppearance
    {
      get
      {
        return (IBandedViewAppearance) new GridAppearanceWrapper();
      }
    }

    IBandedViewAppearance IBandedGridView<ColumnWrapper, TreeListNodeWrapper>.BandedViewAppearancePrint
    {
      get
      {
        return (IBandedViewAppearance) new GridAppearanceWrapper();
      }
    }

    IBandedGridOptionsView IBandedGridView<ColumnWrapper, TreeListNodeWrapper>.BandedGridOptionsView
    {
      get
      {
        return (IBandedGridOptionsView) new BandedGridOptionsViewWrapper<ColumnWrapper, TreeListNodeWrapper>((DataViewExportHelperBase<ColumnWrapper, TreeListNodeWrapper>) this);
      }
    }

    public BandedTreeListViewExportHelper(TreeListView view, ExportTarget target)
      : base(view, target)
    {
      this.bands = this.GetPrintableBands();
      this.bandRowCount = this.CalcBandRowCount();
    }

    protected List<ColumnWrapper> GetPrintableBands()
    {
      return this.DataControl.BandsLayoutCore.PrintableBands.Select<BandBase, ColumnWrapper>((Func<BandBase, ColumnWrapper>) (band => (ColumnWrapper) new BandWrapper(band, band.VisibleIndex))).ToList<ColumnWrapper>();
    }

    public override IEnumerable<ColumnWrapper> GetAllColumns()
    {
      return (IEnumerable<ColumnWrapper>) this.bands;
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
