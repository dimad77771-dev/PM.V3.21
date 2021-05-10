// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CsvExportHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Export;
using DevExpress.XtraPrinting;
using System;
using System.IO;

namespace DevExpress.Xpf.Grid
{
  internal class CsvExportHelper : ExportHelper<CsvExportOptions>
  {
    public CsvExportHelper(DataViewBase view, ExportTarget exportTarget, Action<DataViewBase, Stream> exportToStream, Action<DataViewBase, Stream, CsvExportOptions> exportToStreamWithOptions, Action<DataViewBase, string> exportToFile, Action<DataViewBase, string, CsvExportOptions> exportToFileWithOptions)
      : base(view, exportTarget, exportToStream, exportToStreamWithOptions, exportToFile, exportToFileWithOptions)
    {
    }

    protected override void ApplyDefaults(IDataAwareExportOptions options)
    {
      options.AllowFixedColumnHeaderPanel = DataAwareExportOptionsFactory.UpdateDefaultBoolean(options.AllowFixedColumnHeaderPanel, true);
    }

    protected override void ApplyViewOptions(IDataAwareExportOptions options)
    {
      options.ShowColumnHeaders = DataAwareExportOptionsFactory.UpdateDefaultBoolean(options.ShowColumnHeaders, this.View.ShowColumnHeaders && this.TableView.PrintColumnHeaders);
      options.AllowHorzLines = DataAwareExportOptionsFactory.UpdateDefaultBoolean(options.AllowHorzLines, this.TableView.ShowHorizontalLines);
      options.AllowVertLines = DataAwareExportOptionsFactory.UpdateDefaultBoolean(options.AllowVertLines, this.TableView.ShowVerticalLines);
    }

    protected override void ApplyPrintOptions(IDataAwareExportOptions target, CsvExportOptions source)
    {
      this.ApplyPrintOptions(target, source);
      target.CSVEncoding = source.Encoding;
      target.CSVSeparator = source.Separator;
    }
  }
}
