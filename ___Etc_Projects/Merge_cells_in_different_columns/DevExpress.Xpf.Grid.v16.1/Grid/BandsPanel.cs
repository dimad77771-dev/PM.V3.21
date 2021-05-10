// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.BandsPanel
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid.HitTest;
using System;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class BandsPanel : BandsPanelBase, ILayoutNotificationHelperOwner
  {
    private LayoutNotificationHelper notificationHelper;

    DependencyObject ILayoutNotificationHelperOwner.NotificationManager
    {
      get
      {
        return (DependencyObject) this.DataControl;
      }
    }

    private DataControlBase DataControl
    {
      get
      {
        return this.DataView.Return<DataViewBase, DataControlBase>((Func<DataViewBase, DataControlBase>) (x => x.DataControl), (Func<DataControlBase>) null);
      }
    }

    public BandsPanel()
    {
      this.notificationHelper = new LayoutNotificationHelper((ILayoutNotificationHelperOwner) this);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
      if (this.DataControl != null)
        this.notificationHelper.Subscribe();
      return base.MeasureOverride(availableSize);
    }

    protected override FrameworkElement CreateBandElement(BandBase band)
    {
      BandHeaderControl bandHeaderControl = new BandHeaderControl();
      bandHeaderControl.BeginInit();
      bandHeaderControl.DataContext = (object) band;
      bandHeaderControl.CanSyncColumnPosition = true;
      BaseGridHeader.SetGridColumn((DependencyObject) bandHeaderControl, (BaseColumn) band);
      BarManager.SetDXContextMenu((UIElement) bandHeaderControl, (IPopupControl) band.View.DataControlMenu);
      DataControlPopupMenu.SetGridMenuType((DependencyObject) bandHeaderControl, new GridMenuType?(GridMenuType.Band));
      ColumnBase.SetHeaderPresenterType((DependencyObject) bandHeaderControl, HeaderPresenterType.Headers);
      GridViewHitInfoBase.SetHitTestAcceptor((DependencyObject) bandHeaderControl, (DataViewHitTestAcceptorBase) new BandHeaderTableViewHitTestAcceptor());
      bandHeaderControl.EndInit();
      return (FrameworkElement) bandHeaderControl;
    }

    protected override double GetBandElementY(BandBase band, double offset)
    {
      if (band.ActualShowInBandsPanel)
        return offset;
      return double.MinValue;
    }

    protected override double GetMergeHeight(BandBase band)
    {
      if (band == null)
        return 0.0;
      return this.GetMergeHeight(band, (Func<FrameworkElement, double>) (x => x.DesiredSize.Height));
    }

    protected internal double GetMergeHeight(BandBase band, Func<FrameworkElement, double> getHeight)
    {
      double num = 0.0;
      for (BandBase band1 = band; band1 != null && !band1.ActualShowInBandsPanel; band1 = band1.ParentBand)
      {
        FrameworkElement bandHeader = this.GetBandHeader(band1);
        num += bandHeader != null ? getHeight(bandHeader) : 0.0;
      }
      return num;
    }

    void ILayoutNotificationHelperOwner.InvalidateMeasure()
    {
      this.InvalidateMeasure();
    }
  }
}
