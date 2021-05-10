// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.HeaderPanelScrollViewerAutomationPeer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows.Controls;

namespace DevExpress.Xpf.Grid.Automation
{
  public class HeaderPanelScrollViewerAutomationPeer : HeaderPanelScrollViewerAutomationPeerBase
  {
    protected GridControl DataControl
    {
      get
      {
        return base.DataControl as GridControl;
      }
    }

    public HeaderPanelScrollViewerAutomationPeer(GridControl gridControl, ScrollViewer viewer)
      : base((DataControlBase) gridControl, viewer)
    {
    }

    protected override ColumnHeaderAutomationPeerBase CreateColumnHeaderAutomationPeer(GridColumnHeader header)
    {
      return (ColumnHeaderAutomationPeerBase) new ColumnHeaderAutomationPeer(this.DataControl, header);
    }
  }
}
