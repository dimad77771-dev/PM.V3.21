// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.DataPanelAutomationPeer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation.Peers;

namespace DevExpress.Xpf.Grid.Automation
{
  public class DataPanelAutomationPeer : DataControlAutomationPeerBase
  {
    public DataPanelAutomationPeer(DataControlBase dataControl)
      : base(dataControl, (FrameworkElement) dataControl.viewCore.DataPresenter)
    {
    }

    protected override string GetNameCore()
    {
      return "DataPanel";
    }

    protected override List<AutomationPeer> GetChildrenCore()
    {
      if (this.DataControl == null || this.DataControl.AutomationPeer == null)
        return new List<AutomationPeer>();
      return this.DataControl.AutomationPeer.GetRowPeers();
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
      return AutomationControlType.Pane;
    }
  }
}
