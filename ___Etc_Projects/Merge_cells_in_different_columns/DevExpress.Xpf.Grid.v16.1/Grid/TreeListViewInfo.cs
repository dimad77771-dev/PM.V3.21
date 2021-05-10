// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListViewInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid
{
  public class TreeListViewInfo : GridViewInfo
  {
    public TreeListView TreeListView
    {
      get
      {
        return (TreeListView) this.GridView;
      }
    }

    private bool HasVisibleBands
    {
      get
      {
        if (this.Grid != null && this.Grid.BandsLayoutCore != null)
          return this.Grid.BandsLayoutCore.VisibleBands.Count != 0;
        return false;
      }
    }

    public override int GroupCount
    {
      get
      {
        if (this.TreeListView.ColumnsCore.Count == 0 && !this.HasVisibleBands)
          return 0;
        return this.MaxVisibleLevel + this.TreeListView.ServiceIndentsCount;
      }
    }

    public int MaxVisibleLevel
    {
      get
      {
        return this.TreeListView.TreeListDataProvider.MaxVisibleLevel;
      }
    }

    public override double TotalGroupAreaIndent
    {
      get
      {
        return this.TreeListView.RowIndent * (double) this.GroupCount;
      }
    }

    public override double RightGroupAreaIndent
    {
      get
      {
        return 0.0;
      }
    }

    public TreeListViewInfo(TreeListView view)
      : base((DataViewBase) view)
    {
    }
  }
}
