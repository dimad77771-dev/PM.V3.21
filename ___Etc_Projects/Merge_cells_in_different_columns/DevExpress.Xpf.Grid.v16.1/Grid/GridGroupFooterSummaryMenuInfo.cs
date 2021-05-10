// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridGroupFooterSummaryMenuInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Bars;
using System;

namespace DevExpress.Xpf.Grid
{
  public class GridGroupFooterSummaryMenuInfo : GridTotalSummaryMenuInfo
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
        return GridMenuType.GroupFooterSummary;
      }
    }

    public override bool CanCreateItems
    {
      get
      {
        if (this.View.IsGroupFooterMenuEnabled)
          return !this.IsCheckBoxSelectorColumnMenu();
        return false;
      }
    }

    public override BarManagerMenuController MenuController
    {
      get
      {
        return this.View.GroupFooterMenuController;
      }
    }

    protected override ISummaryItemOwner SummaryItemsCore
    {
      get
      {
        return this.DataControl.GroupSummaryCore;
      }
    }

    public GridGroupFooterSummaryMenuInfo(DataControlPopupMenu menu)
      : base(menu)
    {
    }

    protected override GridTotalSummaryHelper CreateSummaryHelper()
    {
      return (GridTotalSummaryHelper) new GridGroupFooterSummaryHelper((DataViewBase) this.View, (Func<ColumnBase>) (() => this.Column));
    }
  }
}
