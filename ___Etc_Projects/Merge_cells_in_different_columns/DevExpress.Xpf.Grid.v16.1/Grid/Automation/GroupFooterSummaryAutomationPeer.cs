// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.GroupFooterSummaryAutomationPeer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation.Peers;

namespace DevExpress.Xpf.Grid.Automation
{
  public class GroupFooterSummaryAutomationPeer : GridControlVirtualElementAutomationPeerBase
  {
    public int RowHandle { get; private set; }

    public int ColumnIndex { get; private set; }

    protected TableView TableView
    {
      get
      {
        return this.DataControl.viewCore as TableView;
      }
    }

    public GroupFooterSummaryAutomationPeer(GridControl dataControl, int rowHandle, int columnIndex)
      : base((DataControlBase) dataControl)
    {
      this.RowHandle = rowHandle;
      this.ColumnIndex = columnIndex;
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
      return AutomationControlType.Custom;
    }

    protected override FrameworkElement GetFrameworkElement()
    {
      return this.TableView.GetGroupFooterSummaryElementByRowHandleAndColumn(this.RowHandle, this.TableView.VisibleColumnsCore[this.ColumnIndex]);
    }

    protected override string GetNameCore()
    {
      return this.TableView.GetGroupSummaryText(this.TableView.VisibleColumnsCore[this.ColumnIndex], this.RowHandle, false);
    }

    protected override List<AutomationPeer> GetChildrenCore()
    {
      return new List<AutomationPeer>();
    }

    public override object GetPattern(PatternInterface patternInterface)
    {
      return (object) null;
    }
  }
}
