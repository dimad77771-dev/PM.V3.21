// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GridOptionsBehaviorWrapper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.XtraExport.Helpers;

namespace DevExpress.Xpf.Grid.Printing
{
  internal class GridOptionsBehaviorWrapper : IGridOptionsBehavior
  {
    private DataViewBase view;

    bool IGridOptionsBehavior.ReadOnly
    {
      get
      {
        return !this.view.AllowEditing;
      }
    }

    bool IGridOptionsBehavior.AlignGroupSummaryInGroupRow
    {
      get
      {
        TableView tableView = this.view as TableView;
        if (tableView != null)
          return tableView.PrintGroupSummaryDisplayMode == GroupSummaryDisplayMode.AlignByColumns;
        return false;
      }
    }

    public GridOptionsBehaviorWrapper(DataViewBase view)
    {
      this.view = view;
    }
  }
}
