// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridGroupPanelMenuInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Bars;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class GridGroupPanelMenuInfo : GridMenuInfo
  {
    public GridViewBase View
    {
      get
      {
        return (GridViewBase) base.View;
      }
    }

    public override GridMenuType MenuType
    {
      get
      {
        return GridMenuType.GroupPanel;
      }
    }

    public override bool CanCreateItems
    {
      get
      {
        return this.View.IsGroupPanelMenuEnabled;
      }
    }

    public override BarManagerMenuController MenuController
    {
      get
      {
        return this.View.GroupPanelMenuController;
      }
    }

    public GridGroupPanelMenuInfo(GridPopupMenu menu)
      : base((DataControlPopupMenu) menu)
    {
    }

    protected override void CreateItems()
    {
      this.CreateBarButtonItem("ItemFullExpand", GridControlStringId.MenuGroupPanelFullExpand, false, (ImageSource) ImageHelper.GetImage("ItemFullExpand"), this.View.GridViewCommands.ExpandAllGroups, (object) null);
      this.CreateBarButtonItem("ItemFullCollapse", GridControlStringId.MenuGroupPanelFullCollapse, false, (ImageSource) ImageHelper.GetImage("ItemFullCollapse"), this.View.GridViewCommands.CollapseAllGroups, (object) null);
      this.CreateBarButtonItem("ItemClearGrouping", GridControlStringId.MenuGroupPanelClearGrouping, true, (ImageSource) ImageHelper.GetImage("ItemClearGrouping"), this.View.GridViewCommands.ClearGrouping, (object) null);
    }
  }
}
