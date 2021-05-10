// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupRowLayout.Column
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;

namespace DevExpress.Xpf.Grid.GroupRowLayout
{
  public class Column : IGroupPanelItem
  {
    private readonly UIElement elementCore;
    private IGroupPanelItemOwner ownerCore;

    public UIElement Element
    {
      get
      {
        return this.elementCore;
      }
    }

    public IGroupPanelItemOwner Parent
    {
      get
      {
        return this.ownerCore;
      }
      set
      {
        if (this.ownerCore == value)
          return;
        IGroupPanelItemOwner oldValue = this.ownerCore;
        this.ownerCore = value;
        this.OnItemOwnerReplaced(oldValue);
      }
    }

    public int Index { get; set; }

    public Column(UIElement element)
    {
      this.elementCore = element;
    }

    public void OnItemOwnerReplaced(IGroupPanelItemOwner oldValue)
    {
      if (oldValue != null)
        oldValue.Disconnect(this);
      if (this.Parent == null)
        return;
      this.Parent.Connect(this);
    }
  }
}
