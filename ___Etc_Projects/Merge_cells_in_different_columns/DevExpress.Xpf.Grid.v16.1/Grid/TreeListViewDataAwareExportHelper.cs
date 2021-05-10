// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListViewDataAwareExportHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Export;
using DevExpress.Xpf.Grid.Printing;
using DevExpress.Xpf.Grid.TreeList;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraPrinting;
using System.IO;

namespace DevExpress.Xpf.Grid
{
  internal class TreeListViewDataAwareExportHelper : IDataAwareExportHelper
  {
    private IDataAwareExportOptions options;
    private ExportTarget exportTarget;
    private TreeListView view;

    public TreeListViewDataAwareExportHelper(TreeListView view, ExportTarget exportTarget, IDataAwareExportOptions options)
    {
      this.view = view;
      this.exportTarget = exportTarget;
      this.options = options;
    }

    public void Export(Stream stream)
    {
      if (this.view.DataControl != null && this.view.DataControl.BandsCore.Count > 0)
        this.CreateBandedTreeListExporter(this.view, this.exportTarget, this.options).Export(stream);
      else
        new TreeListExcelExporter<ColumnWrapper, TreeListNodeWrapper>((IGridView<ColumnWrapper, TreeListNodeWrapper>) new TreeListViewExportHelper(this.view, this.exportTarget), this.options).Export(stream);
    }

    public void Export(string filePath)
    {
      if (this.options.BandedLayoutMode != BandedLayoutMode.LinearColumns && this.view.DataControl != null && this.view.DataControl.BandsCore.Count > 0)
        this.CreateBandedTreeListExporter(this.view, this.exportTarget, this.options).Export(filePath);
      else
        new TreeListExcelExporter<ColumnWrapper, TreeListNodeWrapper>((IGridView<ColumnWrapper, TreeListNodeWrapper>) new TreeListViewExportHelper(this.view, this.exportTarget), this.options).Export(filePath);
    }

    protected virtual BandedTreeListExcelExporter<ColumnWrapper, TreeListNodeWrapper> CreateBandedTreeListExporter(TreeListView view, ExportTarget exportTarget, IDataAwareExportOptions options)
    {
      if (view.AllowBandMultiRow)
        return (BandedTreeListExcelExporter<ColumnWrapper, TreeListNodeWrapper>) new AdvBandedTreeListExcelExporter<ColumnWrapper, TreeListNodeWrapper>((IGridView<ColumnWrapper, TreeListNodeWrapper>) new AdvBandedTreeListViewExportHelper(view, exportTarget), options);
      return new BandedTreeListExcelExporter<ColumnWrapper, TreeListNodeWrapper>((IGridView<ColumnWrapper, TreeListNodeWrapper>) new BandedTreeListViewExportHelper(view, exportTarget), options);
    }
  }
}
