// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListControlBand
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Utils.Serializing;
using DevExpress.Xpf.Core;
using System.ComponentModel;
using System.Windows.Markup;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>The treelist band.
  /// </para>
  ///             </summary>
  [ContentProperty("Columns")]
  public class TreeListControlBand : BandBase
  {
    /// <summary>
    ///                 <para>Provides access to the band's child band collection.
    /// </para>
    ///             </summary>
    /// <value>A collection of bands.</value>
    [XtraSerializableProperty(true, true, true)]
    [GridStoreAlwaysProperty]
    [DevExpressXpfGridLocalizedDescription("TreeListControlBandBands")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    public ObservableCollectionCore<TreeListControlBand> Bands
    {
      get
      {
        return (ObservableCollectionCore<TreeListControlBand>) this.BandsCore;
      }
    }

    /// <summary>
    ///                 <para>Provides access to the band's child column collection.
    /// </para>
    ///             </summary>
    /// <value>A collection of columns.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [GridStoreAlwaysProperty]
    [XtraSerializableProperty(true, true, true)]
    [Category("Data")]
    [DevExpressXpfGridLocalizedDescription("TreeListControlBandColumns")]
    public ObservableCollectionCore<TreeListColumn> Columns
    {
      get
      {
        return (ObservableCollectionCore<TreeListColumn>) this.ColumnsCore;
      }
    }

    internal override IBandsCollection CreateBands()
    {
      return (IBandsCollection) new BandCollection<TreeListControlBand>();
    }

    internal override IBandColumnsCollection CreateColumns()
    {
      return (IBandColumnsCollection) new BandColumnCollection<TreeListColumn>();
    }
  }
}
