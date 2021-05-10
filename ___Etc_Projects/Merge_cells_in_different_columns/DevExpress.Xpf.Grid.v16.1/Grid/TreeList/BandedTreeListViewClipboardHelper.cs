// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.BandedTreeListViewClipboardHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Printing;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraPrinting;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class BandedTreeListViewClipboardHelper : TreeListViewClipboardHelper, IClipboardBandedGridView<ColumnWrapper, TreeListNodeWrapper>, IClipboardGridView<ColumnWrapper, TreeListNodeWrapper>, IGridView<ColumnWrapper, TreeListNodeWrapper>, IGridViewBase<ColumnWrapper, TreeListNodeWrapper, ColumnWrapper, TreeListNodeWrapper>
  {
    public BandedTreeListViewClipboardHelper(TreeListView view, ExportTarget target = ExportTarget.Xlsx)
      : base(view, target)
    {
    }

    ClipboardBandLayoutInfo IClipboardBandedGridView<ColumnWrapper, TreeListNodeWrapper>.GetBandsInfo()
    {
      return this.GetBandsInfo();
    }

    bool IClipboardBandedGridView<ColumnWrapper, TreeListNodeWrapper>.IsAdvBandedView()
    {
      return true;
    }

    public override bool UseHierarchyIndent(TreeListNodeWrapper row, ColumnWrapper col)
    {
      if (this.DataControl.BandsLayoutCore.VisibleBands.Count > 0 && this.DataControl.BandsLayoutCore.GetRootBand(col.Column.ParentBand) == this.DataControl.BandsLayoutCore.VisibleBands[0])
        return base.UseHierarchyIndent(row, col);
      return false;
    }
  }
}
