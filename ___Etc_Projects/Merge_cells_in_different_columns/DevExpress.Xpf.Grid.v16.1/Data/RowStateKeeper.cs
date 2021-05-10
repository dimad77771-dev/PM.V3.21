// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.RowStateKeeper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Helpers;

namespace DevExpress.Xpf.Data
{
  public class RowStateKeeper : ListSourceRowsKeeper
  {
    private DetailInfoKeeper detailInfoHash;
    private CollapsedRowsKeeper collapsedRowsHash;

    private RowStateController RowStateController
    {
      get
      {
        return this.Controller.Selection as RowStateController;
      }
    }

    private DetailInfoCollection DetailInfoCollection
    {
      get
      {
        return this.RowStateController.DetailInfoCollection;
      }
    }

    public RowStateKeeper(DataController dataController, SelectedRowsKeeper selectedRowsKeeper)
      : base(dataController, selectedRowsKeeper)
    {
      this.detailInfoHash = new DetailInfoKeeper(dataController, true);
      this.collapsedRowsHash = new CollapsedRowsKeeper(dataController, true);
    }

    public override void Save()
    {
      base.Save();
      if (this.detailInfoHash.IsEmpty)
        this.detailInfoHash.SaveCore();
      if (!this.collapsedRowsHash.IsEmpty)
        return;
      this.collapsedRowsHash.SaveCore();
    }

    protected override bool RestoreCore(bool clear)
    {
      this.detailInfoHash.RestoreCore(clear);
      this.collapsedRowsHash.RestoreCore(clear);
      return base.RestoreCore(clear);
    }

    public override void Clear()
    {
      this.detailInfoHash.Clear();
      this.collapsedRowsHash.Clear();
      base.Clear();
    }
  }
}
