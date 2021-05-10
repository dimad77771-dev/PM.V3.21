// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.PeerCache
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows.Automation.Peers;

namespace DevExpress.Xpf.Grid.Automation
{
  public class PeerCache : PeerCacheBase
  {
    protected override bool ShouldAddPeerToCache(AutomationPeer peer)
    {
      if (!(peer is DataControlAutomationPeerBase) && !(peer is HeaderPanelScrollViewerAutomationPeer))
        return peer is GroupInfoAutomationPeer;
      return true;
    }
  }
}
