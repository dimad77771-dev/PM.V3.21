// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.BandHeaderControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid.HitTest;
using DevExpress.Xpf.Utils;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class BandHeaderControl : BaseGridHeader
  {
    public BandHeaderControl()
    {
      ControlExtensions.SetDefaultStyleKey(this, typeof (BandHeaderControl));
    }

    protected internal override DependencyObject CreateDragElementDataContext()
    {
      return (DependencyObject) new BandData() { Column = this.BaseColumn };
    }

    protected override bool IsCompatibleDropTargetFactoryCore(IDropTargetFactory factory, UIElement dropTargetElement)
    {
      if (!base.IsCompatibleDropTargetFactoryCore(factory, dropTargetElement))
        return false;
      if (!(factory is BandedViewBandHeaderDropTargetFactory) && !(factory is BandedViewColumnHeaderDropTargetFactory))
        return factory is BandChooserDropTargetFactory;
      return true;
    }

    protected override IDropTarget CreateEmptyDropTargetCore()
    {
      return this.GridView.CreateEmptyBandDropTarget();
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      GridViewHitInfoBase.SetHitTestAcceptorSafe((DependencyObject) this.HeaderGripper, (DataViewHitTestAcceptorBase) new BandEdgeTableViewHitTestAcceptor());
    }

    protected override DXThumb CreateGripper()
    {
      return (DXThumb) new GridThumb();
    }

    protected override XPFContentControl CreateCustomHeaderPresenter()
    {
      return (XPFContentControl) new HeaderContentControl();
    }

    protected override FrameworkElement CreateDesignTimeSelectionControl()
    {
      DesignTimeSelectionControl selectionControl = new DesignTimeSelectionControl();
      selectionControl.IsTabStop = false;
      return (FrameworkElement) selectionControl;
    }
  }
}
