// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.FooterPanelAutomationPeer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Automation.Peers;

namespace DevExpress.Xpf.Grid.Automation
{
  public class FooterPanelAutomationPeer : DataControlAutomationPeerBase
  {
    public FooterPanelAutomationPeer(DataControlBase dataControl, FrameworkElement element)
      : base(dataControl, element)
    {
    }

    public FooterPanelAutomationPeer(DataControlBase dataControl)
      : base(dataControl, (FrameworkElement) dataControl)
    {
    }

    public override AutomationPeer CreatePeerCore(DependencyObject obj)
    {
      if (obj is GridTotalSummary)
        return (AutomationPeer) new TotalSummaryAutomationPeer(this.DataControl, obj as GridTotalSummary);
      return base.CreatePeerCore(obj);
    }

    protected override List<AutomationPeer> GetChildrenCore()
    {
      List<AutomationPeer> uiChildrenCore = this.GetUIChildrenCore((DependencyObject) GridControlAutomationPeerHelper.GetFooterPanelUIElement((FrameworkElement) this.DataControl));
      if (this.DataControl.DataView is CardView && uiChildrenCore != null)
        return uiChildrenCore.Where<AutomationPeer>((Func<AutomationPeer, bool>) (peer =>
        {
          if (peer is FrameworkElementAutomationPeer)
            return ((UIElementAutomationPeer) peer).Owner.GetVisible();
          return false;
        })).ToList<AutomationPeer>();
      return uiChildrenCore;
    }

    protected override string GetClassNameCore()
    {
      return typeof (ItemsControlBase).Name;
    }

    protected override string GetNameCore()
    {
      return "FooterPanel";
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
      return AutomationControlType.Pane;
    }
  }
}
