// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.BandedGridViewClipboardHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.XtraExport.Helpers;
using DevExpress.XtraPrinting;

namespace DevExpress.Xpf.Grid.Printing
{
  public class BandedGridViewClipboardHelper : GridViewClipboardHelper, IClipboardBandedGridView<ColumnWrapper, RowBaseWrapper>, IClipboardGridView<ColumnWrapper, RowBaseWrapper>, IGridView<ColumnWrapper, RowBaseWrapper>, IGridViewBase<ColumnWrapper, RowBaseWrapper, ColumnWrapper, RowBaseWrapper>
  {
    public BandedGridViewClipboardHelper(TableView view, ExportTarget target = ExportTarget.Xlsx)
      : base(view, target)
    {
    }

    ClipboardBandLayoutInfo IClipboardBandedGridView<ColumnWrapper, RowBaseWrapper>.GetBandsInfo()
    {
      return this.GetBandsInfo();
    }

    bool IClipboardBandedGridView<ColumnWrapper, RowBaseWrapper>.IsAdvBandedView()
    {
      return true;
    }
  }
}
