// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GridViewExportHelperBase`2
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.XtraExport.Helpers;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Grid.Printing
{
  public abstract class GridViewExportHelperBase<TCol, TRow> : DataViewExportHelperBase<TCol, TRow> where TCol : ColumnWrapper where TRow : RowBaseWrapper
  {
    private readonly List<ISummaryItemEx> groupFooterSummary;
    private readonly List<ISummaryItemEx> groupHeaderSummary;
    private readonly List<TCol> groupedColumns;

    protected GridControl Grid
    {
      get
      {
        return this.View.Grid;
      }
    }

    protected TableView View
    {
      get
      {
        return base.View as TableView;
      }
    }

    public override bool ShowBandsPanel
    {
      get
      {
        return this.View.ShowBandsPanel;
      }
    }

    public override bool ShowGroupedColumns
    {
      get
      {
        return this.View.ShowGroupedColumns;
      }
    }

    public override IEnumerable<ISummaryItemEx> GridGroupSummaryItemCollection
    {
      get
      {
        return (IEnumerable<ISummaryItemEx>) this.groupFooterSummary;
      }
    }

    public override IEnumerable<ISummaryItemEx> GroupHeaderSummaryItems
    {
      get
      {
        return (IEnumerable<ISummaryItemEx>) this.groupHeaderSummary;
      }
    }

    protected bool PrintGroupFooters
    {
      get
      {
        if (this.View.ShowGroupFooters)
          return this.View.PrintGroupFooters;
        return false;
      }
    }

    protected override long RowCountCore
    {
      get
      {
        return (long) (this.Grid.DataController.ListSourceRowCount + this.Grid.DataController.GroupRowCount);
      }
    }

    protected override FormatConditionCollection FormatConditionsCore
    {
      get
      {
        return this.View.FormatConditions;
      }
    }

    public GridViewExportHelperBase(TableView view, ExportTarget target)
      : base((DataViewBase) view, target)
    {
      this.groupFooterSummary = this.GetGroupFooterSummary().ToList<ISummaryItemEx>();
      this.groupHeaderSummary = this.GetGroupHeaderSummary().ToList<ISummaryItemEx>();
      this.groupedColumns = this.GetPrintableColumns((IEnumerable<ColumnBase>) view.GroupedColumns);
    }

    public override IEnumerable<TCol> GetGroupedColumns()
    {
      return (IEnumerable<TCol>) this.groupedColumns;
    }

    protected abstract IEnumerable<ISummaryItemEx> GetGroupHeaderSummary();

    protected abstract IEnumerable<ISummaryItemEx> GetGroupFooterSummary();

    protected IEnumerable<ISummaryItemEx> GetGroupHeaderSummaryCore()
    {
      return this.GetPrintableGroupSummary((Func<SummaryItemBase, bool>) (item => !this.IsGroupFooterSummary(item)));
    }

    protected IEnumerable<ISummaryItemEx> GetGroupFooterSummaryCore()
    {
      return this.GetPrintableGroupSummary((Func<SummaryItemBase, bool>) (item => this.IsGroupFooterSummary(item)));
    }

    protected IEnumerable<ISummaryItemEx> GetPrintableGroupSummary(Func<SummaryItemBase, bool> canPrintSummaryItem)
    {
      return this.GetPrintableSummary((ISummaryItemOwner) this.Grid.GroupSummary, canPrintSummaryItem, new Func<SummaryItemBase, ISummaryItemEx>(this.CreateGroupSummaryItemWrapper));
    }

    protected bool IsGroupFooterSummary(SummaryItemBase item)
    {
      return !string.IsNullOrEmpty(((GridSummaryItem) item).ShowInGroupColumnFooter);
    }

    private SummaryItemExportWrapper CreateGroupSummaryItemWrapper(SummaryItemBase item)
    {
      string summaryFieldName = this.GetSummaryFieldName(item, SummaryItemBase.ColumnSummaryType.Group);
      string exportToColumn = this.GetExportToColumn(summaryFieldName, item.ActualShowInColumn);
      string displayFormat = this.GetDisplayFormat(item, SummaryItemBase.ColumnSummaryType.Group);
      return new SummaryItemExportWrapper(summaryFieldName, exportToColumn, item.SummaryType, displayFormat, (object) null, (Func<int, object>) (rowHandle => this.Grid.GetGroupSummaryValue(rowHandle, (GridSummaryItem) item)));
    }

    public override int RaiseMergeEvent(int row1, int row2, TCol col)
    {
      bool? nullable = this.View.RaiseCellMerge(col.Column, row1, row2, false);
      if (!nullable.HasValue)
        return 3;
      return !nullable.Value ? -1 : 0;
    }

    public override bool GetAllowMerge(TCol col)
    {
      ColumnWrapper columnWrapper = (ColumnWrapper) col;
      if (columnWrapper.Column == null)
        return false;
      if (!columnWrapper.Column.AllowCellMerge.HasValue)
        return this.View.AllowCellMerge;
      return columnWrapper.Column.AllowCellMerge.Value;
    }

    public override IEnumerable<TRow> GetAllRows()
    {
      for (int visibleIndex = 0; visibleIndex < this.DataProvider.VisibleCount; ++visibleIndex)
      {
        int rowHandle = this.Grid.GetRowHandleByVisibleIndex(visibleIndex);
        if (this.Grid.GetRowLevelByRowHandle(rowHandle) <= 0)
          yield return this.GetRowByRowHandle(rowHandle);
      }
    }

    internal TRow GetRowByRowHandle(int rowHandle)
    {
      if (this.Grid.IsGroupRowHandle(rowHandle))
        return (TRow) this.CreateGroupRow(rowHandle);
      return this.CreateDataRow(rowHandle);
    }

    private IGroupRow<RowBaseWrapper> CreateGroupRow(int rowHandle)
    {
      int levelByRowHandle = this.Grid.GetRowLevelByRowHandle(rowHandle);
      string groupRowDisplayText = this.View.GetGroupRowDisplayText(rowHandle);
      bool isExpanded = this.Grid.IsGroupRowExpanded(rowHandle);
      IEnumerable<TRow> childRows = this.GetChildRows(rowHandle);
      return (IGroupRow<RowBaseWrapper>) new GroupRowWrapper(rowHandle, levelByRowHandle, -1, groupRowDisplayText, isExpanded, (IEnumerable<RowBaseWrapper>) childRows, this.View);
    }

    private IEnumerable<TRow> GetChildRows(int groupRowHandle)
    {
      if (this.Grid.IsGroupRowHandle(groupRowHandle))
      {
        int childCount = this.Grid.GetChildRowCount(groupRowHandle);
        for (int childIndex = 0; childIndex < childCount; ++childIndex)
        {
          int childRowHandle = this.Grid.GetChildRowHandle(groupRowHandle, childIndex);
          yield return this.GetRowByRowHandle(childRowHandle);
        }
      }
    }

    private TRow CreateDataRow(int rowHandle)
    {
      return new DataRowWrapper(rowHandle, this.Grid.GetRowLevelByRowHandle(rowHandle), this.Grid.GetListIndexByRowHandle(rowHandle)) as TRow;
    }
  }
}
