// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.HitTest.TotalSummaryTableViewHitTestAcceptor
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;

namespace DevExpress.Xpf.Grid.HitTest
{
  public class TotalSummaryTableViewHitTestAcceptor : DataViewHitTestAcceptorBase
  {
    public override void Accept(FrameworkElement element, DataViewHitTestVisitorBase visitor)
    {
      GridColumnData gridColumnData = element.DataContext as GridColumnData;
      if (gridColumnData == null)
        return;
      ColumnBase column = gridColumnData.Column;
      if (column == null || !column.HasTotalSummaries)
        return;
      visitor.VisitTotalSummary(column);
    }
  }
}
