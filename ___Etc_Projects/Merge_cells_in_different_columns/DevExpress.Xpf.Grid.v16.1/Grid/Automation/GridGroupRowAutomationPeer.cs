// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.GridGroupRowAutomationPeer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace DevExpress.Xpf.Grid.Automation
{
  public class GridGroupRowAutomationPeer : GridRowAutomationPeer, IExpandCollapseProvider
  {
    protected GridControl DataControl
    {
      get
      {
        return base.DataControl as GridControl;
      }
    }

    ExpandCollapseState IExpandCollapseProvider.ExpandCollapseState
    {
      get
      {
        return !this.DataControl.IsGroupRowExpanded(this.RowHandle) ? ExpandCollapseState.Collapsed : ExpandCollapseState.Expanded;
      }
    }

    public GridGroupRowAutomationPeer(int rowHandle, GridControl grid)
      : base(rowHandle, (DataControlBase) grid)
    {
    }

    void IExpandCollapseProvider.Collapse()
    {
      this.DataControl.CollapseGroupRow(this.RowHandle);
    }

    void IExpandCollapseProvider.Expand()
    {
      this.DataControl.ExpandGroupRow(this.RowHandle);
    }

    protected override void Invoke()
    {
      this.DataControl.ChangeGroupExpanded(this.DataControl.GetRowVisibleIndexByHandle(this.RowHandle));
    }

    public override object GetPattern(PatternInterface patternInterface)
    {
      if (patternInterface == PatternInterface.ExpandCollapse)
        return (object) this;
      return base.GetPattern(patternInterface);
    }

    protected override List<AutomationPeer> GetChildrenCore()
    {
      if (this.UIAutomationPeer != null)
        return this.UIAutomationPeer.GetChildren();
      return new List<AutomationPeer>();
    }

    protected internal override GridCellAutomationPeer GetCellPeer(int columnIndex, bool force = false)
    {
      if (!force)
        return (GridCellAutomationPeer) null;
      return base.GetCellPeer(columnIndex, force);
    }

    protected override object GetBoundObject()
    {
      return this.DataControl.DataView.GetGroupDisplayValue(this.RowHandle);
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
      return AutomationControlType.Group;
    }

    protected override bool HasKeyboardFocusCore()
    {
      return this.DataControl.DataView.FocusedRowHandle == this.RowHandle;
    }
  }
}
