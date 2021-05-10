// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GridViewExportHelper`2
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
  public class GridViewExportHelper<TCol, TRow> : GridViewExportHelperBase<TCol, TRow>, IDisposable where TCol : ColumnWrapper where TRow : RowBaseWrapper
  {
    private readonly ItemsGenerationStrategyBase itemsGenerationStrategy;
    private bool allowPartialGrouping;

    public GridViewExportHelper(TableView view, ExportTarget target)
      : base(view, target)
    {
      this.itemsGenerationStrategy = view.CreateItemsGenerationStrategy();
      this.GenerateAll();
    }

    private void GenerateAll()
    {
      this.Grid.LockUpdateLayout = true;
      if (this.View.AllowPartialGrouping)
      {
        this.Grid.DisableAllowPartialGrouping();
        this.allowPartialGrouping = true;
      }
      this.itemsGenerationStrategy.GenerateAll();
    }

    private void ClearAll()
    {
      if (this.allowPartialGrouping)
      {
        this.allowPartialGrouping = false;
        this.Grid.UpdateAllowPartialGrouping();
      }
      this.itemsGenerationStrategy.ClearAll();
      this.Grid.LockUpdateLayout = false;
    }

    public void Dispose()
    {
      this.ClearAll();
    }

    protected override IEnumerable<ISummaryItemEx> GetGroupHeaderSummary()
    {
      return Enumerable.Empty<ISummaryItemEx>();
    }

    protected override IEnumerable<ISummaryItemEx> GetGroupFooterSummary()
    {
      return this.GetGroupHeaderSummaryCore().Concat<ISummaryItemEx>(this.PrintGroupFooters ? this.GetGroupFooterSummaryCore() : Enumerable.Empty<ISummaryItemEx>());
    }
  }
}
