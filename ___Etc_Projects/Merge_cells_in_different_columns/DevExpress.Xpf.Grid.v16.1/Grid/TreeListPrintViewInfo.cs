// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListPrintViewInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Grid
{
  public class TreeListPrintViewInfo : TreeListViewInfo
  {
    private BandsLayoutBase bandsLayoutCore;
    private readonly List<ColumnBase> visibleColumns;

    public override BandsLayoutBase BandsLayout
    {
      get
      {
        return this.bandsLayoutCore;
      }
    }

    public override IList<ColumnBase> VisibleColumns
    {
      get
      {
        return (IList<ColumnBase>) this.visibleColumns;
      }
    }

    public int MaxLevel
    {
      get
      {
        return this.TreeListView.TreeListDataProvider.MaxLevel;
      }
    }

    public override int GroupCount
    {
      get
      {
        return (this.TreeListView.PrintAllNodes ? this.MaxLevel : this.MaxVisibleLevel) + 1 + (this.TreeListView.PrintNodeImages ? 1 : 0);
      }
    }

    public override double TotalGroupAreaIndent
    {
      get
      {
        return 20.0 * (double) this.GroupCount;
      }
    }

    public TreeListPrintViewInfo(TreeListView view, BandsLayoutBase bandsLayout)
      : base(view)
    {
      this.visibleColumns = view.PrintableColumns.ToList<ColumnBase>();
      this.bandsLayoutCore = bandsLayout;
    }
  }
}
