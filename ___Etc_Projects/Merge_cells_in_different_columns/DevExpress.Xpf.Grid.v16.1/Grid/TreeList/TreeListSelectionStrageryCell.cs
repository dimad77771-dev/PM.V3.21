// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListSelectionStrageryCell
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListSelectionStrageryCell : SelectionStrategyCellBase
  {
    private TreeListView TreeListView
    {
      get
      {
        return this.DataView as TreeListView;
      }
    }

    public TreeListSelectionStrageryCell(TreeListView treeListView)
      : base((DataViewBase) treeListView)
    {
    }

    public override void CopyToClipboard()
    {
      this.TreeListView.CopySelectedCellsToClipboard();
    }

    protected override CellBase CreateCell(int rowHandle, ColumnBase column)
    {
      return ((ITableView) this.TreeListView).CreateGridCell(rowHandle, column);
    }

    protected override void OnAfterMouseLeftButtonDownCore(IDataViewHitInfo hitInfo)
    {
      TreeListViewHitInfo treeListViewHitInfo = (TreeListViewHitInfo) hitInfo;
      if (treeListViewHitInfo.InNodeIndent || treeListViewHitInfo.InNodeExpandButton)
        return;
      base.OnAfterMouseLeftButtonDownCore(hitInfo);
    }
  }
}
