// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.GroupFooterAutomationPeer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation.Peers;

namespace DevExpress.Xpf.Grid.Automation
{
  public class GroupFooterAutomationPeer : GridControlVirtualElementAutomationPeerBase
  {
    private Dictionary<int, GroupFooterSummaryAutomationPeer> summaryPeers;

    protected int RowHandle { get; set; }

    protected TableView TableView
    {
      get
      {
        return this.DataControl.viewCore as TableView;
      }
    }

    protected GridControl GridControl
    {
      get
      {
        return this.DataControl as GridControl;
      }
    }

    public GroupFooterAutomationPeer(int rowHandle, GridControl dataControl)
      : base((DataControlBase) dataControl)
    {
      this.RowHandle = rowHandle;
      this.summaryPeers = new Dictionary<int, GroupFooterSummaryAutomationPeer>();
    }

    protected override List<AutomationPeer> GetChildrenCore()
    {
      List<AutomationPeer> automationPeerList = new List<AutomationPeer>();
      for (int columnIndex = 0; columnIndex < this.TableView.VisibleColumnsCore.Count; ++columnIndex)
      {
        if (this.HasSummaries(this.TableView.VisibleColumnsCore[columnIndex]))
          automationPeerList.Add(this.GetGroupFooterSummaryPeer(columnIndex));
      }
      return automationPeerList;
    }

    protected virtual bool HasSummaries(ColumnBase column)
    {
      return column.GroupSummariesCore.Count > 0;
    }

    protected AutomationPeer GetGroupFooterSummaryPeer(int columnIndex)
    {
      GroupFooterSummaryAutomationPeer summaryAutomationPeer = (GroupFooterSummaryAutomationPeer) null;
      if (!this.summaryPeers.TryGetValue(columnIndex, out summaryAutomationPeer))
      {
        summaryAutomationPeer = new GroupFooterSummaryAutomationPeer(this.GridControl, this.RowHandle, columnIndex);
        this.summaryPeers[columnIndex] = summaryAutomationPeer;
      }
      return (AutomationPeer) summaryAutomationPeer;
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
      return AutomationControlType.Pane;
    }

    public override object GetPattern(PatternInterface patternInterface)
    {
      return (object) null;
    }

    protected override string GetNameCore()
    {
      return "GroupFooterRow";
    }

    protected override FrameworkElement GetFrameworkElement()
    {
      return this.TableView.GetGroupFooterRowElementByRowHandle(this.RowHandle);
    }
  }
}
