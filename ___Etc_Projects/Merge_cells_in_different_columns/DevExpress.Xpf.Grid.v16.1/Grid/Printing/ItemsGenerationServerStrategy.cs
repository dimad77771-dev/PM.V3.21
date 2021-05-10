// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.ItemsGenerationServerStrategy
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Xpf.Data;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DevExpress.Xpf.Grid.Printing
{
  public abstract class ItemsGenerationServerStrategy : ItemsGenerationStrategyBase
  {
    protected internal PrintSelectedRowsInfo SelectedRowsInfo;
    private BaseGridController oldController;

    private GridDataProvider DataProvider
    {
      get
      {
        return (GridDataProvider) base.DataProvider;
      }
    }

    public override bool RequireFullExpand
    {
      get
      {
        return true;
      }
    }

    private GridServerModeDataControllerPrintInfo Info { get; set; }

    public ItemsGenerationServerStrategy(DataViewBase view)
      : base(view)
    {
    }

    protected virtual IList GetAllFilteredAndSortedRows()
    {
      if (!this.View.PrintSelectedRowsOnly)
        return this.FetchAllFilteredAndSortedRows();
      return PrintSelectedRowsHelper.GetSelectedRows((DataProviderBase) this.DataProvider, this.View, out this.SelectedRowsInfo, this.GetPrintSelectedRowsOnlyAllRows());
    }

    protected virtual IList GetPrintSelectedRowsOnlyAllRows()
    {
      return (IList) new List<object>();
    }

    protected abstract IList FetchAllFilteredAndSortedRows();

    protected override void GenerateAllCore()
    {
      this.oldController = this.DataProvider.DataController;
      this.View.DataControl.dataResetLocker.DoLockedAction((Action) (() => this.Info = this.DataProvider.SubstituteDataControllerForPrinting(this.GetAllFilteredAndSortedRows(), this.View.PrintAllGroupsCore)));
    }

    public override string GetTotalSummaryText(ColumnBase column)
    {
      if (this.Info == null || !this.Info.Summaries.ContainsKey(column))
        return string.Empty;
      return this.Info.Summaries[column] ?? string.Empty;
    }

    public override string GetFixedTotalSummaryLeftText()
    {
      if (this.Info == null)
        return (string) null;
      return this.Info.FixedLeftSummaryText;
    }

    public override string GetFixedTotalSummaryRightText()
    {
      if (this.Info == null)
        return (string) null;
      return this.Info.FixedRightSummaryText;
    }

    protected override void ClearAllCore()
    {
      this.View.DataControl.dataResetLocker.DoLockedAction((Action) (() =>
      {
        this.DataProvider.SetDataController(this.oldController);
        this.View.UpdateDataObjects(true, true);
      }));
    }

    public override object GetRowValue(RowData rowData)
    {
      if (this.Info == null)
        return (object) null;
      return GridDataProvider.GetRowByListIndex((DataController) this.Info.Controller, rowData.DataRowNode.PrintInfo.ListIndex);
    }

    public override object GetCellValue(RowData rowData, string fieldName)
    {
      if (this.Info == null)
        return (object) null;
      return GridDataProvider.GetRowValueByListIndex((DataController) this.Info.Controller, rowData.DataRowNode.PrintInfo.ListIndex, fieldName);
    }

    public override void Clear()
    {
      base.Clear();
      if (this.Info == null || this.Info.Controller == null)
        return;
      this.DataProvider.ClearPrintingControllerEvents(this.Info.Controller);
      this.Info.Controller.Dispose();
      this.Info = (GridServerModeDataControllerPrintInfo) null;
    }
  }
}
