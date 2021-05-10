// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupRowLayout.GroupPanelVisualItemOwner
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows.Controls;

namespace DevExpress.Xpf.Grid.GroupRowLayout
{
  public class GroupPanelVisualItemOwner : IGroupPanelItemOwner
  {
    private readonly Panel panelCore;

    public Panel Panel
    {
      get
      {
        return this.panelCore;
      }
    }

    private UIElementCollection Children
    {
      get
      {
        return this.Panel.Children;
      }
    }

    public GroupPanelVisualItemOwner(Panel panel)
    {
      this.panelCore = panel;
    }

    void IGroupPanelItemOwner.Connect(Column column)
    {
      this.Children.Add(column.Element);
    }

    void IGroupPanelItemOwner.Disconnect(Column column)
    {
      this.Children.Remove(column.Element);
    }
  }
}
