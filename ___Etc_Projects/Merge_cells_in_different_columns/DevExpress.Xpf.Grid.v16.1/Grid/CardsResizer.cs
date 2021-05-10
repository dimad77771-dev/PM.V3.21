// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CardsResizer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid.Hierarchy;
using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class CardsResizer : DXThumb, IResizeHelperOwner
  {
    private CardView View
    {
      get
      {
        return (CardView) DataControlBase.FindCurrentView((DependencyObject) this);
      }
    }

    public int RowIndex
    {
      get
      {
        return this.Separator.RowIndex;
      }
    }

    private SeparatorInfo Separator
    {
      get
      {
        return (SeparatorInfo) this.DataContext;
      }
    }

    double IResizeHelperOwner.ActualSize
    {
      get
      {
        if (this.View == null)
          return 0.0;
        return this.View.FixedSize;
      }
      set
      {
        if (this.View == null)
          return;
        this.View.FixedSize = value;
      }
    }

    SizeHelperBase IResizeHelperOwner.SizeHelper
    {
      get
      {
        return SizeHelperBase.GetDefineSizeHelper(this.Separator.Orientation);
      }
    }

    public CardsResizer()
    {
      ControlExtensions.SetDefaultStyleKey(this, typeof (CardsResizer));
      new ResizeHelper((IResizeHelperOwner) this).Init((Thumb) this);
    }

    protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
    {
      HitTestResult hitTestResult = base.HitTestCore(hitTestParameters);
      if (hitTestResult != null || !this.IsHitTestVisible)
        return hitTestResult;
      return (HitTestResult) new PointHitTestResult((Visual) this, hitTestParameters.HitPoint);
    }

    void IResizeHelperOwner.ChangeSize(double delta)
    {
      CardsResizer cardsResizer = this;
      double num = ((IResizeHelperOwner) cardsResizer).ActualSize + delta / (double) this.RowIndex;
      ((IResizeHelperOwner) cardsResizer).ActualSize = num;
    }

    void IResizeHelperOwner.OnDoubleClick()
    {
    }

    void IResizeHelperOwner.SetIsResizing(bool isResizing)
    {
    }
  }
}
