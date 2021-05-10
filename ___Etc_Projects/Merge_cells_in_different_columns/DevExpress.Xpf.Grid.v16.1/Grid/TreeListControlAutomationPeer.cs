// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListControlAutomationPeer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Automation;
using System;
using System.Windows.Automation.Peers;

namespace DevExpress.Xpf.Grid
{
  public class TreeListControlAutomationPeer : GridDataControlAutomationPeer
  {
    public TreeListControl TreeListControl
    {
      get
      {
        return this.DataControl as TreeListControl;
      }
    }

    protected override string ControlName
    {
      get
      {
        return "TreeListControl";
      }
    }

    public TreeListControlAutomationPeer(TreeListControl treeListControl)
      : base((DataControlBase) treeListControl)
    {
      treeListControl.AutomationPeer = (DataControlAutomationPeer) this;
    }

    protected override HeaderPanelAutomationPeerBase CreateHeaderPeer()
    {
      return new HeaderPanelAutomationPeerBase((DataControlBase) this.TreeListControl);
    }

    protected internal override AutomationPeer CreateRowPeer(int rowHandle)
    {
      return (AutomationPeer) new TreeListRowAutomationPeer(rowHandle, (DataControlBase) this.TreeListControl);
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
      return AutomationControlType.Tree;
    }

    protected internal override AutomationPeer GetGroupFooterAutomationPeer(int rowHandle)
    {
      throw new NotImplementedException();
    }
  }
}
