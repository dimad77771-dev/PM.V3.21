// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.FixedGroupElement
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;

namespace DevExpress.Xpf.Grid
{
  internal class FixedGroupElement : IFixedGroupElement
  {
    private readonly Func<GroupRowData> getRowDataFunc;

    public FixedGroupElement(Func<GroupRowData> getRowDataFunc)
    {
      this.getRowDataFunc = getRowDataFunc;
    }

    double IFixedGroupElement.GetLeftMargin(bool drawAdornerUnderWholeGroup)
    {
      GroupRowData groupRowData = this.getRowDataFunc();
      if (groupRowData == null)
        return 0.0;
      TableView view = (TableView) groupRowData.View;
      double detailLeftIndent = 0.0;
      double detailRightIndent = 0.0;
      int detailParentGroupLevel = 0;
      FixedGroupElement.GetDetailIndents(view, out detailLeftIndent, out detailRightIndent, out detailParentGroupLevel);
      return (view.ActualShowIndicator ? view.IndicatorWidth : 0.0) + detailLeftIndent + (double) (detailParentGroupLevel + (view.UseGroupShadowIndent ? groupRowData.GroupLevel : 0) + (drawAdornerUnderWholeGroup ? 0 : 1)) * view.LeftGroupAreaIndent;
    }

    double IFixedGroupElement.GetRightMargin(bool drawAdornerUnderWholeGroup)
    {
      GroupRowData groupRowData = this.getRowDataFunc();
      if (groupRowData == null)
        return 0.0;
      TableView view = (TableView) groupRowData.View;
      int detailParentGroupLevel = 0;
      double detailLeftIndent = 0.0;
      double detailRightIndent = 0.0;
      FixedGroupElement.GetDetailIndents(view, out detailLeftIndent, out detailRightIndent, out detailParentGroupLevel);
      return detailRightIndent;
    }

    private static void GetDetailIndents(TableView view, out double detailLeftIndent, out double detailRightIndent, out int detailParentGroupLevel)
    {
      double leftIndent = 0.0;
      double rightIndent = 0.0;
      int parentGroupLevel = 0;
      view.Grid.EnumerateThisAndParentDataControls((Action<DataControlBase>) (dataControl =>
      {
        GridControl gridControl = dataControl as GridControl;
        if (gridControl == null)
          return;
        GridControl masterGrid = gridControl.GetMasterGrid();
        if (masterGrid != null)
          parentGroupLevel += masterGrid.View.GetRowData(gridControl.GetMasterRowHandle()).Level;
        TableView tableView = gridControl.View as TableView;
        if (tableView == view)
          return;
        leftIndent += tableView.ActualDetailMargin.Left;
        rightIndent += tableView.ActualDetailMargin.Right;
      }));
      detailLeftIndent = leftIndent;
      detailRightIndent = rightIndent;
      detailParentGroupLevel = parentGroupLevel;
    }

    private static void GetDetailLevels(TableView view, out int detailLevel, out int detailParentGroupLevel)
    {
      int level = -1;
      int parentGroupLevel = 0;
      view.Grid.EnumerateThisAndParentDataControls((Action<DataControlBase>) (dataControl =>
      {
        ++level;
        GridControl gridControl = dataControl as GridControl;
        if (gridControl == null)
          return;
        GridControl masterGrid = gridControl.GetMasterGrid();
        if (masterGrid == null)
          return;
        parentGroupLevel += masterGrid.View.GetRowData(gridControl.GetMasterRowHandle()).Level;
      }));
      detailLevel = level;
      detailParentGroupLevel = parentGroupLevel;
    }
  }
}
