// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GridViewReportHelper`2
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
  public class GridViewReportHelper<TCol, TRow> : GridViewExportHelperBase<TCol, TRow> where TCol : ColumnWrapper where TRow : RowBaseWrapper
  {
    public GridViewReportHelper(TableView view, ExportTarget target)
      : base(view, target)
    {
    }

    protected override IEnumerable<ISummaryItemEx> GetGroupFooterSummary()
    {
      if (!this.PrintGroupFooters)
        return Enumerable.Empty<ISummaryItemEx>();
      return this.GetGroupFooterSummaryCore();
    }

    protected override IEnumerable<ISummaryItemEx> GetGroupHeaderSummary()
    {
      return this.GetGroupHeaderSummaryCore();
    }

    protected override List<TCol> GetPrintableColumns(IEnumerable<ColumnBase> processedColumns)
    {
      return processedColumns.Cast<ColumnBase>().Select<ColumnBase, TCol>((Func<ColumnBase, int, TCol>) ((column, index) => this.CreateColumnCore(column, index))).ToList<TCol>();
    }
  }
}
