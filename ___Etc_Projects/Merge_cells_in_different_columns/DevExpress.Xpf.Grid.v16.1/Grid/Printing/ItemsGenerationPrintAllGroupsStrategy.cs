// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.ItemsGenerationPrintAllGroupsStrategy
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data.Helpers;

namespace DevExpress.Xpf.Grid.Printing
{
  public class ItemsGenerationPrintAllGroupsStrategy : ItemsGenerationSimpleStrategy
  {
    private ListSourceRowsKeeper keeper;

    public override bool RequireFullExpand
    {
      get
      {
        return true;
      }
    }

    public ItemsGenerationPrintAllGroupsStrategy(DataViewBase view)
      : base(view)
    {
    }

    protected override void GenerateAllCore()
    {
      this.keeper = this.DataProvider.DataController.CreateControllerRowsKeeper();
      this.DataProvider.DataController.SaveRowState(this.keeper);
      this.DataProvider.ExpandAll();
    }

    protected override void ClearAllCore()
    {
      this.DataProvider.CollapseAll();
      this.DataProvider.DataController.RestoreRowState(this.keeper);
      this.keeper.Dispose();
      this.keeper = (ListSourceRowsKeeper) null;
    }
  }
}
