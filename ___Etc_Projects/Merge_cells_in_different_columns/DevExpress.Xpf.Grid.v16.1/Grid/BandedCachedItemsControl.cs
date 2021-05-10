// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.BandedCachedItemsControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class BandedCachedItemsControl : CachedItemsControl, INotifyCurrentViewChanged, ILayoutNotificationHelperOwner
  {
    private LayoutNotificationHelper layoutNotificationHelper;

    private DataViewBase View
    {
      get
      {
        return DataControlBase.GetCurrentView((DependencyObject) this);
      }
    }

    DependencyObject ILayoutNotificationHelperOwner.NotificationManager
    {
      get
      {
        if (this.View == null)
          return (DependencyObject) null;
        return (DependencyObject) this.View.DataControl;
      }
    }

    public BandedCachedItemsControl()
    {
      this.layoutNotificationHelper = new LayoutNotificationHelper((ILayoutNotificationHelperOwner) this);
    }

    protected override Size MeasureOverride(Size constraint)
    {
      this.layoutNotificationHelper.Subscribe();
      return base.MeasureOverride(constraint);
    }

    void INotifyCurrentViewChanged.OnCurrentViewChanged(DependencyObject d)
    {
      this.InvalidateMeasure();
    }

    void ILayoutNotificationHelperOwner.InvalidateMeasure()
    {
      this.InvalidateMeasure();
    }
  }
}
