// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.GridControlAutomationPeer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace DevExpress.Xpf.Grid.Automation
{
  public class GridControlAutomationPeer : GridDataControlAutomationPeer, IGridProvider
  {
    public GridControl GridControl
    {
      get
      {
        return this.DataControl as GridControl;
      }
    }

    public HeaderPanelAutomationPeer HeaderPanelPeer
    {
      get
      {
        return (HeaderPanelAutomationPeer) base.HeaderPanelPeer;
      }
    }

    protected override string ControlName
    {
      get
      {
        return "GridControl";
      }
    }

    public GridControlAutomationPeer(GridControl gridControl)
      : base((DataControlBase) gridControl)
    {
      gridControl.AutomationPeer = (DataControlAutomationPeer) this;
    }

    protected override HeaderPanelAutomationPeerBase CreateHeaderPeer()
    {
      return (HeaderPanelAutomationPeerBase) new HeaderPanelAutomationPeer(this.GridControl);
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
      return AutomationControlType.DataGrid;
    }

    protected internal override AutomationPeer CreateRowPeer(int rowHandle)
    {
      if (this.GridControl.IsGroupRowHandle(rowHandle))
        return (AutomationPeer) new GridGroupRowAutomationPeer(rowHandle, this.GridControl);
      if (this.GridControl.View is TreeListView)
        return (AutomationPeer) new TreeListRowAutomationPeer(rowHandle, (DataControlBase) this.GridControl);
      if (this.GridControl.View is TableView)
        return (AutomationPeer) new GridRowAutomationPeer(rowHandle, (DataControlBase) this.GridControl);
      return (AutomationPeer) new CardRowAutomationPeer(rowHandle, this.GridControl);
    }

    protected internal override AutomationPeer GetGroupFooterAutomationPeer(int rowHandle)
    {
      AutomationPeer automationPeer = (AutomationPeer) null;
      if (!this.GridControl.LogicalPeerCache.GroupFooterRows.TryGetValue(rowHandle, out automationPeer))
      {
        automationPeer = this.CreateGroupFooterAutomationPeer(rowHandle);
        this.GridControl.LogicalPeerCache.GroupFooterRows[rowHandle] = automationPeer;
      }
      return automationPeer;
    }

    protected virtual AutomationPeer CreateGroupFooterAutomationPeer(int rowHandle)
    {
      return (AutomationPeer) new GroupFooterAutomationPeer(rowHandle, this.GridControl);
    }
  }
}
