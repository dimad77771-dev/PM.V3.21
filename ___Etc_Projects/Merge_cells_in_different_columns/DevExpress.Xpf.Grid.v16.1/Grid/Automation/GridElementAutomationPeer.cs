// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.GridElementAutomationPeer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid.Automation
{
  public class GridElementAutomationPeer : DataControlAutomationPeerBase
  {
    public GridElementAutomationPeer(DataControlBase dataControl, FrameworkElement element)
      : base(dataControl, element)
    {
    }

    protected override List<AutomationPeer> GetChildrenCore()
    {
      return this.GetUIChildrenCore((DependencyObject) this.Owner);
    }

    public override AutomationPeer CreatePeerCore(DependencyObject obj)
    {
      if (obj is TextBlock)
        return (AutomationPeer) new GroupInfoAutomationPeer(obj as TextBlock);
      return base.CreatePeerCore(obj);
    }
  }
}
