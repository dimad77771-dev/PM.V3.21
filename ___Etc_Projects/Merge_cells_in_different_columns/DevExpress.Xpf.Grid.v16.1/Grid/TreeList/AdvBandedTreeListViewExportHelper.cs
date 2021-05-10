// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.AdvBandedTreeListViewExportHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Printing;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraPrinting;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class AdvBandedTreeListViewExportHelper : BandedTreeListViewExportHelper, IAdvancedBandedGridView<ColumnWrapper, TreeListNodeWrapper>, IBandedGridView<ColumnWrapper, TreeListNodeWrapper>, IGridView<ColumnWrapper, TreeListNodeWrapper>, IGridViewBase<ColumnWrapper, TreeListNodeWrapper, ColumnWrapper, TreeListNodeWrapper>
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

    IAdvBandedOptionsView IAdvancedBandedGridView<ColumnWrapper, TreeListNodeWrapper>.AdvBandedOptionsView
    {
      get
      {
        return (IAdvBandedOptionsView) new BandedGridOptionsViewWrapper<ColumnWrapper, TreeListNodeWrapper>((DataViewExportHelperBase<ColumnWrapper, TreeListNodeWrapper>) this);
      }
    }

    public AdvBandedTreeListViewExportHelper(TreeListView view, ExportTarget target)
      : base(view, target)
    {
      this.columnPanelRowCount = this.CalcColumnRowCount();
    }
  }
}
