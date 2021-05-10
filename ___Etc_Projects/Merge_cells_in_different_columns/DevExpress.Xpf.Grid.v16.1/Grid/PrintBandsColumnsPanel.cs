// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintBandsColumnsPanel
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Printing;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class PrintBandsColumnsPanel : BandsNoneDropPanel
  {
    internal new ColumnsRowDataBase RowData
    {
      get
      {
        return ((RowContent) this.DataContext).Content as ColumnsRowDataBase;
      }
    }

    protected internal override double GetColumnWidth(ColumnBase column)
    {
      return GridPrintingHelper.GetPrintCellInfo((DependencyObject) this.RowData.GetCellDataByColumn(column)).PrintColumnWidth;
    }

    protected internal override double GetBandWidth(BandBase band)
    {
      return GridPrintingHelper.GetPrintCellInfo((DependencyObject) band).PrintColumnWidth;
    }
  }
}
