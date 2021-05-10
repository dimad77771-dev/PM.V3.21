// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TableViewDataAwareExportHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Export;
using DevExpress.Xpf.Grid.Printing;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraPrinting;
using System;
using System.IO;

namespace DevExpress.Xpf.Grid
{
  internal class TableViewDataAwareExportHelper : IDataAwareExportHelper
  {
    private IDataAwareExportOptions options;
    private ExportTarget exportTarget;
    private TableView view;

    public TableViewDataAwareExportHelper(TableView view, ExportTarget exportTarget, IDataAwareExportOptions options)
    {
      this.view = view;
      this.exportTarget = exportTarget;
      this.options = options;
    }

    public void Export(Stream stream)
    {
      this.ExportDataCore((Action<GridViewExcelExporter<ColumnWrapper, RowBaseWrapper>>) (exporter => exporter.Export(stream)));
    }

    public void Export(string filePath)
    {
      this.ExportDataCore((Action<GridViewExcelExporter<ColumnWrapper, RowBaseWrapper>>) (exporter => exporter.Export(filePath)));
    }

    private void ExportDataCore(Action<GridViewExcelExporter<ColumnWrapper, RowBaseWrapper>> exportAction)
    {
      if (this.options.BandedLayoutMode != BandedLayoutMode.LinearColumns && this.view.DataControl != null && this.view.DataControl.BandsCore.Count > 0)
      {
        using (BandedGridViewExportHelper<ColumnWrapper, RowBaseWrapper> viewExportHelper = this.CreateBandedGridViewExportHelper(this.view, this.exportTarget))
        {
          BandedGridViewExcelExporter<ColumnWrapper, RowBaseWrapper> viewExcelExporter = this.view.AllowBandMultiRow ? (BandedGridViewExcelExporter<ColumnWrapper, RowBaseWrapper>) new AdvBandedGridViewExcelExporter<ColumnWrapper, RowBaseWrapper>((IBandedGridView<ColumnWrapper, RowBaseWrapper>) viewExportHelper, this.options) : new BandedGridViewExcelExporter<ColumnWrapper, RowBaseWrapper>((IBandedGridView<ColumnWrapper, RowBaseWrapper>) viewExportHelper, this.options);
          exportAction((GridViewExcelExporter<ColumnWrapper, RowBaseWrapper>) viewExcelExporter);
        }
      }
      else
      {
        using (GridViewExportHelper<ColumnWrapper, RowBaseWrapper> viewExportHelper = new GridViewExportHelper<ColumnWrapper, RowBaseWrapper>(this.view, this.exportTarget))
        {
          GridViewExcelExporter<ColumnWrapper, RowBaseWrapper> viewExcelExporter = new GridViewExcelExporter<ColumnWrapper, RowBaseWrapper>((IGridView<ColumnWrapper, RowBaseWrapper>) viewExportHelper, this.options);
          exportAction(viewExcelExporter);
        }
      }
    }

    protected virtual BandedGridViewExportHelper<ColumnWrapper, RowBaseWrapper> CreateBandedGridViewExportHelper(TableView view, ExportTarget exportTarget)
    {
      if (view.AllowBandMultiRow)
        return (BandedGridViewExportHelper<ColumnWrapper, RowBaseWrapper>) new AdvBandedGridViewExportHelper<ColumnWrapper, RowBaseWrapper>(view, exportTarget);
      return new BandedGridViewExportHelper<ColumnWrapper, RowBaseWrapper>(view, exportTarget);
    }
  }
}
