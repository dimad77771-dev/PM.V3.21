// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.HeaderPanelScrollViewerAutomationPeerBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid.Automation
{
  public class HeaderPanelScrollViewerAutomationPeerBase : ScrollViewerAutomationPeer, IAutomationPeerCreator
  {
    private DataControlBase dataControl;

    protected DataControlBase DataControl
    {
      get
      {
        return this.dataControl;
      }
    }

    public HeaderPanelScrollViewerAutomationPeerBase(DataControlBase dataControl, ScrollViewer viewer)
      : base(viewer)
    {
      this.dataControl = dataControl;
    }

    protected virtual AutomationPeer CreatePeerCore(DependencyObject obj)
    {
      if (obj is GridColumnHeader)
        return (AutomationPeer) this.CreateColumnHeaderAutomationPeer((GridColumnHeader) obj);
      return DataControlAutomationPeerBase.CreatePeerDefault(obj);
    }

    protected virtual ColumnHeaderAutomationPeerBase CreateColumnHeaderAutomationPeer(GridColumnHeader header)
    {
      return new ColumnHeaderAutomationPeerBase(this.DataControl, header);
    }

    protected override List<AutomationPeer> GetChildrenCore()
    {
      return DataControlAutomationPeerBase.GetUIChildrenCore((DependencyObject) this.Owner, (IAutomationPeerCreator) this);
    }

    AutomationPeer IAutomationPeerCreator.CreatePeer(DependencyObject obj)
    {
      AutomationPeer peer = this.DataControl.PeerCache.GetPeer(obj);
      if (peer != null)
        return peer;
      AutomationPeer peerCore = this.CreatePeerCore(obj);
      this.DataControl.PeerCache.AddPeer(obj, peerCore, true);
      return peerCore;
    }
  }
}
