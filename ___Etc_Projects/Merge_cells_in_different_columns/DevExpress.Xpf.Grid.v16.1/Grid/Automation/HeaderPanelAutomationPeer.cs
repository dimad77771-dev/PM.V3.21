// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.HeaderPanelAutomationPeer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid.Automation
{
  public class HeaderPanelAutomationPeer : HeaderPanelAutomationPeerBase
  {
    protected GridControl DataControl
    {
      get
      {
        return base.DataControl as GridControl;
      }
    }

    public HeaderPanelAutomationPeer(GridControl gridControl)
      : base((DataControlBase) gridControl)
    {
    }

    protected override List<AutomationPeer> GetChildrenCore()
    {
      List<AutomationPeer> automationPeerList = this.GetUIChildrenCore(DataControlAutomationPeerBase.FindObjectInVisualTree((DependencyObject) this.DataControl, "groupPanelItemsControl")) ?? new List<AutomationPeer>();
      foreach (ColumnBase columnBase in (IEnumerable<ColumnBase>) this.DataControl.View.VisibleColumnsCore)
      {
        ColumnHeaderAutomationPeerBase automationPeerBase;
        if (!this.HeaderPeers.TryGetValue(columnBase.VisibleIndex, out automationPeerBase))
          this.HeaderPeers[columnBase.VisibleIndex] = (ColumnHeaderAutomationPeerBase) new ColumnHeaderAutomationPeer(this.DataControl, (GridColumn) columnBase);
        automationPeerList.Add((AutomationPeer) this.HeaderPeers[columnBase.VisibleIndex]);
      }
      return automationPeerList;
    }

    protected override ColumnHeaderAutomationPeerBase CreateColumnHeaderAutomationPeer(GridColumnHeader columnHeader)
    {
      return (ColumnHeaderAutomationPeerBase) new ColumnHeaderAutomationPeer(this.DataControl, columnHeader);
    }

    protected override HeaderPanelScrollViewerAutomationPeerBase CreateHeaderPanelScrollViewerAutomationPeer(ScrollViewer viewer)
    {
      return (HeaderPanelScrollViewerAutomationPeerBase) new HeaderPanelScrollViewerAutomationPeer(this.DataControl, viewer);
    }
  }
}
