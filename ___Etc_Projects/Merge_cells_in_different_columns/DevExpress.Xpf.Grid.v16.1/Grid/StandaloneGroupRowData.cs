// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.StandaloneGroupRowData
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class StandaloneGroupRowData : GroupRowData
  {
    public StandaloneGroupRowData(DataTreeBuilder treeBuilder)
      : base(treeBuilder)
    {
    }

    protected override void UpdateMasterDetailInfo(bool updateRowObjectIfRowExpanded, bool updateDetailRow)
    {
    }

    internal override bool CanReuseCellData()
    {
      return !this.UpdateOnlyData;
    }

    protected override FrameworkElement CreateRowElement()
    {
      return (FrameworkElement) new ContentPresenter();
    }

    internal override void UpdateCellDataError(ColumnBase column, GridColumnData cellData, bool customValidate = true)
    {
    }

    internal override void UpdateRowDataError()
    {
    }

    protected internal override void UpdateGridGroupSummaryData(ColumnBase column, GridGroupSummaryColumnData cellData)
    {
      IList<GridSummaryItem> gridGroupSummaries = this.ExtractGridGroupSummaries(column.GroupSummariesCore);
      cellData.Column = column;
      cellData.HasSummary = gridGroupSummaries.Count > 0;
      cellData.SummaryTextInfo = this.GridView.GetGroupSummaryTextValues(column, this.RowHandle.Value, this.IsGroupFooter);
    }

    protected internal override void OnActualHeaderWidthChange()
    {
    }
  }
}
