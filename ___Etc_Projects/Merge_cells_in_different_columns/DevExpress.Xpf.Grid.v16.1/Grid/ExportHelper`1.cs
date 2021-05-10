// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.ExportHelper`1
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Export;
using DevExpress.Utils;
using DevExpress.Xpf.Grid.Printing;
using DevExpress.XtraPrinting;
using System;
using System.IO;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  internal class ExportHelper<T> where T : ExportOptionsBase
  {
    private Action<DataViewBase, Stream> ExportToStream;
    private Action<DataViewBase, Stream, T> ExportToStreamWithOptions;
    private Action<DataViewBase, string> ExportToFile;
    private Action<DataViewBase, string, T> ExportToFileWithOptions;

    public DataViewBase View { get; private set; }

    public ExportTarget ExportTarget { get; private set; }

    protected ITableView TableView
    {
      get
      {
        return this.View as ITableView;
      }
    }

    public ExportHelper(DataViewBase view, ExportTarget exportTarget, Action<DataViewBase, Stream> exportToStream, Action<DataViewBase, Stream, T> exportToStreamWithOptions, Action<DataViewBase, string> exportToFile, Action<DataViewBase, string, T> exportToFileWithOptions)
    {
      this.View = view;
      this.ExportToStream = exportToStream;
      this.ExportToStreamWithOptions = exportToStreamWithOptions;
      this.ExportToFile = exportToFile;
      this.ExportToFileWithOptions = exportToFileWithOptions;
      this.ExportTarget = exportTarget;
    }

    public void Export(Stream stream)
    {
      ExportHelper<T>.Export((Action) (() => this.ExportToStream(this.View, stream)), (Action) (() => this.ExportData(stream, default (T))), (IDataAwareExportOptions) null);
    }

    public void Export(Stream stream, T options)
    {
      ExportHelper<T>.Export((Action) (() => this.ExportToStreamWithOptions(this.View, stream, options)), (Action) (() => this.ExportData(stream, options)), (object) options as IDataAwareExportOptions);
    }

    public void Export(string filePath)
    {
      ExportHelper<T>.Export((Action) (() => this.ExportToFile(this.View, filePath)), (Action) (() => this.ExportData(filePath, default (T))), (IDataAwareExportOptions) null);
    }

    public void Export(string filePath, T options)
    {
      ExportHelper<T>.Export((Action) (() => this.ExportToFileWithOptions(this.View, filePath, options)), (Action) (() => this.ExportData(filePath, options)), (object) options as IDataAwareExportOptions);
    }

    private static void Export(Action layoutExport, Action dataExport, IDataAwareExportOptions options)
    {
      if (ExportHelper<T>.IsDataAwareExport(options))
        dataExport();
      else
        layoutExport();
    }

    private static bool IsDataAwareExport(IDataAwareExportOptions options)
    {
      ExportType exportType = ExportSettings.DefaultExportType;
      if (options != null)
        exportType = options.ExportType;
      return exportType == ExportType.DataAware;
    }

    private IDataAwareExportHelper GetDataAwareExportHelper(ExportTarget exportTarget, IDataAwareExportOptions options)
    {
      return this.View.CreateDataAwareExportHelper(exportTarget, options);
    }

    internal IDataAwareExportOptions CreateExporterOptions(T printOptions)
    {
      IDataAwareExportOptions awareExportOptions = DataAwareExportOptionsFactory.Create(this.ExportTarget, (object) printOptions);
      this.ApplyViewOptions(awareExportOptions);
      if ((object) printOptions != null)
        this.ApplyPrintOptions(awareExportOptions, printOptions);
      this.ApplyDefaults(awareExportOptions);
      return awareExportOptions;
    }

    protected virtual void ApplyDefaults(IDataAwareExportOptions options)
    {
      options.InitDefaults();
    }

    protected virtual void ApplyViewOptions(IDataAwareExportOptions options)
    {
      options.AllowCellMerge = DataAwareExportOptionsFactory.UpdateDefaultBoolean(options.AllowCellMerge, this.GetAllowCellMerge());
      options.ShowTotalSummaries = DataAwareExportOptionsFactory.UpdateDefaultBoolean(options.ShowTotalSummaries, this.View.ShouldPrintTotalSummary || this.View.ShouldPrintFixedTotalSummary);
      options.ShowColumnHeaders = DataAwareExportOptionsFactory.UpdateDefaultBoolean(options.ShowColumnHeaders, this.View.ShowColumnHeaders && this.TableView.PrintColumnHeaders);
      options.AllowHorzLines = DataAwareExportOptionsFactory.UpdateDefaultBoolean(options.AllowHorzLines, this.TableView.ShowHorizontalLines);
      options.AllowVertLines = DataAwareExportOptionsFactory.UpdateDefaultBoolean(options.AllowVertLines, this.TableView.ShowVerticalLines);
      options.RightToLeftDocument = this.View.FlowDirection == FlowDirection.RightToLeft ? DefaultBoolean.True : DefaultBoolean.False;
    }

    protected virtual bool GetAllowCellMerge()
    {
      return this.View.DataControl.BandsCore.Count == 0;
    }

    protected virtual void ApplyPrintOptions(IDataAwareExportOptions target, T source)
    {
    }

    private void ExportData(string filePath, T options)
    {
      this.GetDataAwareExportHelper(this.ExportTarget, this.CreateExporterOptions(options)).Export(filePath);
    }

    private void ExportData(Stream stream, T options)
    {
      this.GetDataAwareExportHelper(this.ExportTarget, this.CreateExporterOptions(options)).Export(stream);
    }
  }
}
