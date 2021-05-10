// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListLightweightBestFitControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid
{
  public class TreeListLightweightBestFitControl : LightweightBestFitControl
  {
    protected TreeListView TreeListView { get; private set; }

    public TreeListLightweightBestFitControl(TreeListView view, ColumnBase column)
      : base((DataViewBase) view, column)
    {
      this.TreeListView = view;
    }

    protected override BestFitRowControl CreateBestFitRowControl()
    {
      return (BestFitRowControl) new TreeListBestFitRowControl(this.RowData, this.CellData, this.TreeListView.TreeDerivationMode == TreeDerivationMode.HierarchicalDataTemplate);
    }

    public override void UpdateIsFocusedCell(bool isFocusedCell)
    {
      if (this.Content == null)
        return;
      ((TreeListBestFitRowControl) this.Content).UpdateIsFocusedCell(isFocusedCell);
    }
  }
}
