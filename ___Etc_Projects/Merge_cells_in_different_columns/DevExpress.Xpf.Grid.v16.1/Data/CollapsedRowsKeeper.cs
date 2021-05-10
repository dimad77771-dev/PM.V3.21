// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.CollapsedRowsKeeper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Helpers;

namespace DevExpress.Xpf.Data
{
  public class CollapsedRowsKeeper : SelectedRowsKeeper
  {
    protected RowStateController RowStateController
    {
      get
      {
        return this.Controller.Selection as RowStateController;
      }
    }

    protected virtual RowStateCollection RowStateCollection
    {
      get
      {
        return this.RowStateController.CollapsedRows;
      }
    }

    public CollapsedRowsKeeper(DataController controller, bool allowKeepSelection)
      : base(controller, allowKeepSelection)
    {
    }

    protected override void RestoreCore(object row, int level, object value)
    {
      this.RowStateCollection.SetRowSelected(this.GetRowHandleByObject(row), true, value);
    }

    internal int GetRowHandleByObject(object row)
    {
      if (row is GroupRowInfo)
        return int.MinValue;
      return this.Controller.GetControllerRow((int) row);
    }

    public void SaveRow(int listSourceRow)
    {
      int controllerRow = this.Controller.GetControllerRow(listSourceRow);
      this.SaveRowCore(controllerRow, this.RowStateCollection.GetRowSelectedObject(controllerRow));
    }

    public void SaveCore()
    {
      if (this.RowStateCollection.Count == 0)
        return;
      foreach (int listSourceRow in this.RowStateCollection.CopyToArray())
        this.SaveRow(listSourceRow);
    }

    public virtual void RestoreCore(bool clear)
    {
      if (this.Helper.Count > 0 && !this.IsEmpty)
      {
        for (int listSourceRow = 0; listSourceRow < this.Helper.Count; ++listSourceRow)
          this.Restore(this.GetRowKey(listSourceRow), (object) listSourceRow);
      }
      if (clear)
        return;
      this.Clear();
    }
  }
}
