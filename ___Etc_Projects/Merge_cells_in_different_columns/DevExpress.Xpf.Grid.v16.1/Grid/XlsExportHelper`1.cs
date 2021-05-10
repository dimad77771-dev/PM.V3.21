// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.XlsExportHelper`1
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Export;
using DevExpress.XtraPrinting;
using System;
using System.IO;

namespace DevExpress.Xpf.Grid
{
  internal class XlsExportHelper<T> : ExportHelper<T> where T : XlExportOptionsBase
  {
    public XlsExportHelper(DataViewBase view, ExportTarget exportTarget, Action<DataViewBase, Stream> exportToStream, Action<DataViewBase, Stream, T> exportToStreamWithOptions, Action<DataViewBase, string> exportToFile, Action<DataViewBase, string, T> exportToFileWithOptions)
      : base(view, exportTarget, exportToStream, exportToStreamWithOptions, exportToFile, exportToFileWithOptions)
    {
    }

    protected override void ApplyPrintOptions(IDataAwareExportOptions target, T source)
    {
      base.ApplyPrintOptions(target, source);
      target.AllowHyperLinks = DataAwareExportOptionsFactory.UpdateDefaultBoolean(target.AllowHyperLinks, source.ExportHyperlinks);
      target.SheetName = source.SheetName;
    }
  }
}
