// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.ColumnHeaderAutomationPeer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace DevExpress.Xpf.Grid.Automation
{
  public class ColumnHeaderAutomationPeer : ColumnHeaderAutomationPeerBase, IDockProvider
  {
    public GridControl DataControl
    {
      get
      {
        return base.DataControl as GridControl;
      }
    }

    public GridColumn Column
    {
      get
      {
        return base.Column as GridColumn;
      }
    }

    public DockPosition DockPosition
    {
      get
      {
        return this.Column == null || !this.Column.IsGrouped ? DockPosition.None : DockPosition.Top;
      }
    }

    public ColumnHeaderAutomationPeer(GridControl gridControl, GridColumnHeader header)
      : base((DataControlBase) gridControl, header)
    {
    }

    public ColumnHeaderAutomationPeer(GridControl gridControl, GridColumn column)
      : base((DataControlBase) gridControl, (ColumnBase) column)
    {
    }

    public override object GetPattern(PatternInterface patternInterface)
    {
      if (patternInterface == PatternInterface.Dock)
        return (object) this;
      return (object) null;
    }

    protected bool ShouldDockItem(DockPosition dockPosition)
    {
      if (dockPosition != DockPosition.Top && dockPosition != DockPosition.None)
        return false;
      return dockPosition != this.DockPosition;
    }

    public void SetDockPosition(DockPosition dockPosition)
    {
      if (!this.ShouldDockItem(dockPosition))
        return;
      if (dockPosition == DockPosition.Top)
        this.DataControl.GroupBy(this.Column);
      else
        this.DataControl.UngroupBy(this.Column);
    }
  }
}
