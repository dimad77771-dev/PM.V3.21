// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.DetailInfoKeeper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Xpf.Grid.Native;
using System.Collections.Generic;

namespace DevExpress.Xpf.Data
{
  public class DetailInfoKeeper : CollapsedRowsKeeper
  {
    private List<object> cache = new List<object>();

    protected override RowStateCollection RowStateCollection
    {
      get
      {
        return (RowStateCollection) this.RowStateController.DetailInfoCollection;
      }
    }

    public DetailInfoKeeper(DataController controller, bool allowKeepSelection)
      : base(controller, allowKeepSelection)
    {
    }

    protected override void SaveRowCore(int controllerRow, object selectedObject)
    {
      base.SaveRowCore(controllerRow, selectedObject);
      if (selectedObject == null)
        return;
      this.cache.Add(selectedObject);
    }

    protected override void RestoreCore(object row, int level, object value)
    {
      base.RestoreCore(row, level, value);
      if (!this.RowStateCollection.GetRowSelected(this.GetRowHandleByObject(row)))
        return;
      this.cache.Remove(value);
    }

    public override void RestoreCore(bool clear)
    {
      base.RestoreCore(clear);
      foreach (RowDetailContainer rowDetailContainer in this.cache)
      {
        rowDetailContainer.RemoveFromDetailClones();
        rowDetailContainer.Detach();
      }
      this.cache.Clear();
    }
  }
}
