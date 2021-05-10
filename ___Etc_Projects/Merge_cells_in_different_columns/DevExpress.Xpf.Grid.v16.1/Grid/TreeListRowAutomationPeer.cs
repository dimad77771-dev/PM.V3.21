// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListRowAutomationPeer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Automation;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace DevExpress.Xpf.Grid
{
  public class TreeListRowAutomationPeer : GridRowAutomationPeer, IExpandCollapseProvider
  {
    protected TreeListView View
    {
      get
      {
        return this.DataControl.viewCore as TreeListView;
      }
    }

    ExpandCollapseState IExpandCollapseProvider.ExpandCollapseState
    {
      get
      {
        TreeListNode nodeByRowHandle = this.View.GetNodeByRowHandle(this.RowHandle);
        if (!nodeByRowHandle.HasVisibleChildren)
          return ExpandCollapseState.LeafNode;
        return !nodeByRowHandle.IsExpanded ? ExpandCollapseState.Collapsed : ExpandCollapseState.Expanded;
      }
    }

    public TreeListRowAutomationPeer(int rowHandle, DataControlBase dataControl)
      : base(rowHandle, dataControl)
    {
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
      return AutomationControlType.TreeItem;
    }

    public override object GetPattern(PatternInterface patternInterface)
    {
      if (patternInterface == PatternInterface.ExpandCollapse)
        return (object) this;
      return base.GetPattern(patternInterface);
    }

    void IExpandCollapseProvider.Collapse()
    {
      this.View.CollapseNode(this.RowHandle);
    }

    void IExpandCollapseProvider.Expand()
    {
      this.View.ExpandNode(this.RowHandle);
    }
  }
}
