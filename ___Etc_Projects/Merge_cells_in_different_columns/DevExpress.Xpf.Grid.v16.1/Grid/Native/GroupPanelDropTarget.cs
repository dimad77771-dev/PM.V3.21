// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.GroupPanelDropTarget
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid.Native
{
  public class GroupPanelDropTarget : ColumnHeaderDropTargetBase
  {
    private readonly bool mouseOverTargetPanel;

    protected override int DropIndexCorrection
    {
      get
      {
        return 0;
      }
    }

    public GroupPanelDropTarget(Panel panel, bool mouseOverTargetPanel)
      : base(panel)
    {
      this.mouseOverTargetPanel = mouseOverTargetPanel;
    }

    protected override bool DenyDropIfGroupingIsNotAllowed(HeaderPresenterType sourceType)
    {
      return true;
    }

    protected override bool CanDropCore(int dropIndex, ColumnBase sourceColumn, HeaderPresenterType headerPresenterType)
    {
      return true;
    }

    protected override int GetDropIndexFromDragSource(UIElement element, Point pt)
    {
      if (!this.mouseOverTargetPanel)
        return this.ChildrenCount;
      return base.GetDropIndexFromDragSource(element, pt);
    }
  }
}
