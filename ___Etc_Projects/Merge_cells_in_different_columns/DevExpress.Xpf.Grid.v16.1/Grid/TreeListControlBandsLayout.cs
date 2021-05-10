// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListControlBandsLayout
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid.Native;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>This class supports the internal infrastructure and is not intended to be used directly from your code.
  /// </para>
  ///             </summary>
  public class TreeListControlBandsLayout : BandsLayoutBase
  {
    /// <summary>
    ///                 <para>Provides access to the treelist's band collection.
    /// </para>
    ///             </summary>
    /// <value>An observable collection of bands within the treelist.</value>
    public ObservableCollectionCore<TreeListControlBand> Bands
    {
      get
      {
        return (ObservableCollectionCore<TreeListControlBand>) this.BandsCore;
      }
    }

    protected override void SetPrintWidth(BaseColumn column, double width)
    {
      GridPrintingHelper.SetPrintColumnWidth((DependencyObject) column, width);
    }

    internal override BandsLayoutBase CloneAndFillEmptyBands()
    {
      BandsLayoutBase bandsLayoutBase = base.CloneAndFillEmptyBands();
      bandsLayoutBase.UpdateFixedBands((LayoutAssigner) new PrintLayoutAssigner());
      return bandsLayoutBase;
    }
  }
}
