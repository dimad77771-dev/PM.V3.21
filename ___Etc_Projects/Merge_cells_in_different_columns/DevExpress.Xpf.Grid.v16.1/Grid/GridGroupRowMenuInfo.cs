// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridGroupRowMenuInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Bars;
using System;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class GridGroupRowMenuInfo : GridMenuInfo
  {
    public GroupRowData Row { get; private set; }

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
        return GridMenuType.GroupRow;
      }
    }

    public override bool CanCreateItems
    {
      get
      {
        return this.View.IsGroupRowMenuEnabled;
      }
    }

    public override BarManagerMenuController MenuController
    {
      get
      {
        return this.View.GroupRowMenuController;
      }
    }

    public GridGroupRowMenuInfo(GridPopupMenu menu)
      : base((DataControlPopupMenu) menu)
    {
    }

    protected override void CreateItems()
    {
    }

    public override bool Initialize(IInputElement value)
    {
      this.Row = RowData.FindRowData(value as DependencyObject) as GroupRowData;
      return base.Initialize(value);
    }

    protected override void ExecuteMenuController()
    {
      base.ExecuteMenuController();
      this.Menu.ExecuteOriginationViewMenuController((Func<DataViewBase, BarManagerMenuController>) (view => ((GridViewBase) view).GroupRowMenuController));
    }
  }
}
