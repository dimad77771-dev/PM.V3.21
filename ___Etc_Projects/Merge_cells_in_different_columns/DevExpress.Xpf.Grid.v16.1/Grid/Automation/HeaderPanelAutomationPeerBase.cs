// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.HeaderPanelAutomationPeerBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid.Automation
{
  public class HeaderPanelAutomationPeerBase : DataControlAutomationPeerBase
  {
    private Dictionary<int, ColumnHeaderAutomationPeerBase> headerPeers;

    protected Dictionary<int, ColumnHeaderAutomationPeerBase> HeaderPeers
    {
      get
      {
        return this.headerPeers;
      }
    }

    public HeaderPanelAutomationPeerBase(DataControlBase dataControl)
      : base(dataControl, (FrameworkElement) dataControl)
    {
      this.headerPeers = new Dictionary<int, ColumnHeaderAutomationPeerBase>();
    }

    protected override List<AutomationPeer> GetChildrenCore()
    {
      List<AutomationPeer> automationPeerList = new List<AutomationPeer>();
      foreach (ColumnBase column in (IEnumerable<ColumnBase>) this.DataControl.viewCore.VisibleColumnsCore)
      {
        ColumnHeaderAutomationPeerBase automationPeerBase;
        if (!this.headerPeers.TryGetValue(column.VisibleIndex, out automationPeerBase))
          this.headerPeers[column.VisibleIndex] = new ColumnHeaderAutomationPeerBase(this.DataControl, column);
        automationPeerList.Add((AutomationPeer) this.headerPeers[column.VisibleIndex]);
      }
      return automationPeerList;
    }

    public override AutomationPeer CreatePeerCore(DependencyObject obj)
    {
      if (obj is ScrollViewer)
        return (AutomationPeer) this.CreateHeaderPanelScrollViewerAutomationPeer((ScrollViewer) obj);
      if (obj is GridColumnHeader)
        return (AutomationPeer) this.CreateColumnHeaderAutomationPeer((GridColumnHeader) obj);
      return base.CreatePeerCore(obj);
    }

    protected virtual ColumnHeaderAutomationPeerBase CreateColumnHeaderAutomationPeer(GridColumnHeader columnHeader)
    {
      return new ColumnHeaderAutomationPeerBase(this.DataControl, columnHeader);
    }

    protected virtual HeaderPanelScrollViewerAutomationPeerBase CreateHeaderPanelScrollViewerAutomationPeer(ScrollViewer viewer)
    {
      return new HeaderPanelScrollViewerAutomationPeerBase(this.DataControl, viewer);
    }

    protected override string GetClassNameCore()
    {
      return typeof (ItemsControlBase).Name;
    }

    protected override string GetNameCore()
    {
      return "HeaderPanel";
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
      return AutomationControlType.Pane;
    }
  }
}
