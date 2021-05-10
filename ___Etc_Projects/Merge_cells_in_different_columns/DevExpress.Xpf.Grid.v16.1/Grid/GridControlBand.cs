// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridControlBand
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Utils.Serializing;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid.Native;
using System;
using System.ComponentModel;
using System.Windows.Markup;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>The grid band.
  /// </para>
  ///             </summary>
  [ContentProperty("Columns")]
  public class GridControlBand : BandBase, IDetailElement<BaseColumn>
  {
    /// <summary>
    ///                 <para>Provides access to the band's child band collection.
    /// </para>
    ///             </summary>
    /// <value>A collection of bands.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    [XtraSerializableProperty(true, true, true)]
    [DevExpressXpfGridLocalizedDescription("GridControlBandBands")]
    [GridStoreAlwaysProperty]
    public ObservableCollectionCore<GridControlBand> Bands
    {
      get
      {
        return (ObservableCollectionCore<GridControlBand>) this.BandsCore;
      }
    }

    /// <summary>
    ///                 <para>Provides access to the band's child column collection.
    /// </para>
    ///             </summary>
    /// <value>A collection of columns.</value>
    [DevExpressXpfGridLocalizedDescription("GridControlBandColumns")]
    [XtraSerializableProperty(true, true, true)]
    [GridStoreAlwaysProperty]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    public ObservableCollectionCore<GridColumn> Columns
    {
      get
      {
        return (ObservableCollectionCore<GridColumn>) this.ColumnsCore;
      }
    }

    internal override IBandsCollection CreateBands()
    {
      return (IBandsCollection) new BandCollection<GridControlBand>();
    }

    internal override IBandColumnsCollection CreateColumns()
    {
      return (IBandColumnsCollection) new BandColumnCollection<GridColumn>();
    }

    internal override bool IsServiceColumn()
    {
      GridControlBandsLayout controlBandsLayout = this.BandsLayout as GridControlBandsLayout;
      if (controlBandsLayout != null)
        return controlBandsLayout.CheckBoxSelectorBand == this;
      return false;
    }

    BaseColumn IDetailElement<BaseColumn>.CreateNewInstance(params object[] args)
    {
      return (BaseColumn) Activator.CreateInstance(this.GetType());
    }
  }
}
