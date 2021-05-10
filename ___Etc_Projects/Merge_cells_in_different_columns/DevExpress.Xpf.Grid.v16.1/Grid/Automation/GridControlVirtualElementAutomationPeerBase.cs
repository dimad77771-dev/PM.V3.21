// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.GridControlVirtualElementAutomationPeerBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Windows;
using System.Windows.Automation.Peers;

namespace DevExpress.Xpf.Grid.Automation
{
  public abstract class GridControlVirtualElementAutomationPeerBase : AutomationPeer
  {
    private WeakReference dataControl;

    protected DataControlBase DataControl
    {
      get
      {
        return (DataControlBase) this.dataControl.Target;
      }
    }

    protected AutomationPeer UIAutomationPeer
    {
      get
      {
        FrameworkElement frameworkElement = this.GetFrameworkElement();
        if (frameworkElement == null)
          return (AutomationPeer) null;
        return UIElementAutomationPeer.FromElement((UIElement) frameworkElement) ?? (AutomationPeer) new GridElementAutomationPeer(this.DataControl, frameworkElement);
      }
    }

    public GridControlVirtualElementAutomationPeerBase(DataControlBase dataControl)
    {
      this.dataControl = new WeakReference((object) dataControl);
    }

    protected abstract FrameworkElement GetFrameworkElement();

    protected override Rect GetBoundingRectangleCore()
    {
      AutomationPeer uiAutomationPeer = this.UIAutomationPeer;
      if (uiAutomationPeer == null)
        return new Rect();
      return uiAutomationPeer.GetBoundingRectangle();
    }

    protected override bool IsOffscreenCore()
    {
      AutomationPeer uiAutomationPeer = this.UIAutomationPeer;
      if (uiAutomationPeer == null)
        return true;
      return uiAutomationPeer.IsOffscreen();
    }

    protected override AutomationOrientation GetOrientationCore()
    {
      return AutomationOrientation.None;
    }

    protected override string GetItemTypeCore()
    {
      return string.Empty;
    }

    protected override string GetClassNameCore()
    {
      return string.Empty;
    }

    protected override string GetItemStatusCore()
    {
      return string.Empty;
    }

    protected override bool IsRequiredForFormCore()
    {
      return false;
    }

    protected override bool IsKeyboardFocusableCore()
    {
      return false;
    }

    protected override bool HasKeyboardFocusCore()
    {
      return false;
    }

    protected override bool IsEnabledCore()
    {
      return true;
    }

    protected override bool IsPasswordCore()
    {
      return false;
    }

    protected override string GetAutomationIdCore()
    {
      AutomationPeer uiAutomationPeer = this.UIAutomationPeer;
      if (uiAutomationPeer == null)
        return string.Empty;
      return uiAutomationPeer.GetAutomationId();
    }

    protected override bool IsContentElementCore()
    {
      return true;
    }

    protected override bool IsControlElementCore()
    {
      return true;
    }

    protected override AutomationPeer GetLabeledByCore()
    {
      return (AutomationPeer) null;
    }

    protected override string GetHelpTextCore()
    {
      return string.Empty;
    }

    protected override string GetAcceleratorKeyCore()
    {
      return string.Empty;
    }

    protected override string GetAccessKeyCore()
    {
      return string.Empty;
    }

    protected override Point GetClickablePointCore()
    {
      AutomationPeer uiAutomationPeer = this.UIAutomationPeer;
      if (uiAutomationPeer == null)
        return new Point(double.NaN, double.NaN);
      return uiAutomationPeer.GetClickablePoint();
    }

    protected override void SetFocusCore()
    {
    }
  }
}
