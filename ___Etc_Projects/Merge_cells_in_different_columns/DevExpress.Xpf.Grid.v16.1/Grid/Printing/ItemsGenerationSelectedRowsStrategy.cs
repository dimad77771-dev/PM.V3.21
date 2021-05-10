// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.ItemsGenerationSelectedRowsStrategy
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections;

namespace DevExpress.Xpf.Grid.Printing
{
  public class ItemsGenerationSelectedRowsStrategy : ItemsGenerationServerStrategy
  {
    public override bool RequireFullExpand
    {
      get
      {
        return true;
      }
    }

    public ItemsGenerationSelectedRowsStrategy(DataViewBase view)
      : base(view)
    {
    }

    protected override IList FetchAllFilteredAndSortedRows()
    {
      return PrintSelectedRowsHelper.GetSelectedRows(base.DataProvider, this.View, out this.SelectedRowsInfo, (IList) null);
    }

    protected override IList GetAllFilteredAndSortedRows()
    {
      return this.FetchAllFilteredAndSortedRows();
    }
  }
}
