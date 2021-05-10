// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridTotalSummaryPanelMenuInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Mvvm.Native;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class GridTotalSummaryPanelMenuInfo : GridTotalSummaryMenuInfo
  {
    public override GridMenuType MenuType
    {
      get
      {
        return GridMenuType.FixedTotalSummary;
      }
    }

    public GridTotalSummaryPanelMenuInfo(DataControlPopupMenu menu)
      : base(menu)
    {
    }

    protected override GridTotalSummaryHelper CreateSummaryHelper()
    {
      return (GridTotalSummaryHelper) new GridTotalSummaryPanelHelper(this.View, (Func<ColumnBase>) (() => this.Column));
    }

    protected override void CreateItems()
    {
      this.CreateBarCheckItem("ItemCount", GridControlStringId.MenuFooterCount, new bool?(this.IsCountButtonEnabled()), false, (ImageSource) ImageHelper.GetImage("ItemCount"), (ICommand) DelegateCommandFactory.Create((Action) (() => this.SetSummary(string.Empty, SummaryItemType.Count)), (Func<bool>) (() => true)), false);
      this.CreateBarButtonItem("ItemCustomize", GridControlStringId.MenuFooterCustomize, true, (ImageSource) null, (ICommand) DelegateCommandFactory.Create((Action) (() => this.summaryHelper.ShowSummaryEditor()), (Func<bool>) (() => true), false), (object) null);
    }

    private bool IsCountButtonEnabled()
    {
      if (this.View.FixedSummariesHelper.FixedSummariesRightCore.Count + this.View.FixedSummariesHelper.FixedSummariesLeftCore.Count == 0)
        return false;
      foreach (SummaryItemBase summaryItemBase in (IEnumerable<SummaryItemBase>) this.View.FixedSummariesHelper.FixedSummariesRightCore)
      {
        if (summaryItemBase.SummaryType == SummaryItemType.Count)
          return true;
      }
      foreach (SummaryItemBase summaryItemBase in (IEnumerable<SummaryItemBase>) this.View.FixedSummariesHelper.FixedSummariesLeftCore)
      {
        if (summaryItemBase.SummaryType == SummaryItemType.Count)
          return true;
      }
      return false;
    }

    protected override void SetSummary(string fieldName, SummaryItemType type)
    {
      this.Controller.SetSummary(fieldName, type, !this.Controller.HasSummary(type));
      this.Controller.Apply();
    }
  }
}
