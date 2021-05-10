// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.CardRowAutomationPeer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace DevExpress.Xpf.Grid.Automation
{
  public class CardRowAutomationPeer : GridRowAutomationPeer, IExpandCollapseProvider
  {
    private CardView CardView
    {
      get
      {
        return this.DataControl.viewCore as CardView;
      }
    }

    ExpandCollapseState IExpandCollapseProvider.ExpandCollapseState
    {
      get
      {
        return !this.CardView.IsCardExpanded(this.RowHandle) ? ExpandCollapseState.Collapsed : ExpandCollapseState.Expanded;
      }
    }

    public CardRowAutomationPeer(int rowHandle, GridControl grid)
      : base(rowHandle, (DataControlBase) grid)
    {
    }

    public override object GetPattern(PatternInterface patternInterface)
    {
      if (patternInterface == PatternInterface.ExpandCollapse)
        return (object) this;
      return base.GetPattern(patternInterface);
    }

    void IExpandCollapseProvider.Collapse()
    {
      this.CardView.CollapseCard(this.RowHandle);
    }

    void IExpandCollapseProvider.Expand()
    {
      this.CardView.ExpandCard(this.RowHandle);
    }
  }
}
