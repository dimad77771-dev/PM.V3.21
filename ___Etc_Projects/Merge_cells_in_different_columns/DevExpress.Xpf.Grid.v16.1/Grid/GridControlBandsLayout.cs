// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridControlBandsLayout
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm.Native;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>This class supports the internal infrastructure and is not intended to be used directly from your code.
  /// </para>
  ///             </summary>
  public class GridControlBandsLayout : BandsLayoutBase
  {
    /// <summary>
    ///                 <para>Provides access to the grid's band collection.
    /// </para>
    ///             </summary>
    /// <value>An observable collection of bands within the grid.</value>
    public ObservableCollectionCore<GridControlBand> Bands
    {
      get
      {
        return (ObservableCollectionCore<GridControlBand>) this.BandsCore;
      }
    }

    private GridControl GridControl
    {
      get
      {
        return this.DataControl as GridControl;
      }
    }

    private ITableView TableView
    {
      get
      {
        return this.GridControl.Return<GridControl, ITableView>((Func<GridControl, ITableView>) (grid => grid.View as ITableView), (Func<ITableView>) (() => (ITableView) null));
      }
    }

    internal GridControlBand CheckBoxSelectorBand { get; private set; }

    private bool IsCheckBoxSelectorBandVisible
    {
      get
      {
        if (this.TableView != null)
          return this.TableView.IsCheckBoxSelectorColumnVisible;
        return false;
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the GridControlBandsLayout class.
    /// </para>
    ///             </summary>
    public GridControlBandsLayout()
    {
      this.CheckBoxSelectorBand = this.CreateCheckBoxSelectorBand();
    }

    internal override void ForeachBand(Action<BandBase> action)
    {
      if (this.IsCheckBoxSelectorBandVisible)
        action((BandBase) this.CheckBoxSelectorBand);
      base.ForeachBand(action);
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

    internal override void PatchVisibleBands(List<BandBase> visibleBands, bool hasFixedLeftBands)
    {
      if (!this.IsCheckBoxSelectorBandVisible)
        return;
      this.UpdateCheckBoxSelectorBand(hasFixedLeftBands);
      visibleBands.Insert(0, (BandBase) this.CheckBoxSelectorBand);
    }

    private void UpdateCheckBoxSelectorBand(bool hasFixedLeftBands)
    {
      this.UpdateIsCheckBoxSelectorBandFixed(hasFixedLeftBands);
      this.UpdateCheckBoxSelectorBandColumns();
      this.CheckBoxSelectorBand.OnBandsLayoutChanged();
    }

    private void UpdateIsCheckBoxSelectorBandFixed(bool hasFixedLeftBands)
    {
      if (hasFixedLeftBands)
        this.CheckBoxSelectorBand.Fixed = FixedStyle.Left;
      else
        this.CheckBoxSelectorBand.Fixed = FixedStyle.None;
    }

    private void UpdateCheckBoxSelectorBandColumns()
    {
      GridColumn gridColumn = (GridColumn) null;
      if (this.DataControl != null && this.DataControl.DataView is DevExpress.Xpf.Grid.TableView)
        gridColumn = ((DevExpress.Xpf.Grid.TableView) this.DataControl.DataView).CheckBoxSelectorColumn;
      if (gridColumn == null || this.CheckBoxSelectorBand.Columns.Contains(gridColumn))
        return;
      if (this.CheckBoxSelectorBand.Columns.Count != 0)
        this.CheckBoxSelectorBand.Columns.Clear();
      this.CheckBoxSelectorBand.Columns.Add(gridColumn);
    }

    private GridControlBand CreateCheckBoxSelectorBand()
    {
      GridControlBand gridControlBand = new GridControlBand();
      gridControlBand.AllowMoving = DefaultBoolean.False;
      gridControlBand.AllowResizing = DefaultBoolean.False;
      gridControlBand.Owner = (IBandsOwner) this;
      gridControlBand.BandsLayout = (BandsLayoutBase) this;
      gridControlBand.AllowPrinting = false;
      return gridControlBand;
    }
  }
}
