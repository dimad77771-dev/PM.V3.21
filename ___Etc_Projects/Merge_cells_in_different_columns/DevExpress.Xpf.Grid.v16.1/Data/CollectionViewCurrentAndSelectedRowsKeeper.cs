// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.CollectionViewCurrentAndSelectedRowsKeeper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Helpers;
using DevExpress.Data.Linq;

namespace DevExpress.Xpf.Data
{
  public class CollectionViewCurrentAndSelectedRowsKeeper : ServerModeCurrentAndSelectedRowsKeeper
  {
    private CollectionViewDataController CollectionViewDataController
    {
      get
      {
        return (CollectionViewDataController) this.Controller;
      }
    }

    public CollectionViewCurrentAndSelectedRowsKeeper(CollectionViewDataController controller, bool allowKeepSelection)
      : base((ServerModeDataController) controller, allowKeepSelection)
    {
    }

    protected override void RestoreCurrentRow()
    {
      if (this.CollectionViewDataController.IsSynchronizedWithCurrentItem)
      {
        ICollectionViewHelper icollectionViewHelper = (ICollectionViewHelper) this.Controller.ListSource;
        if (this.Controller.CurrentControllerRow == -2147483647 || this.Controller.IsGroupRowHandle(this.Controller.CurrentControllerRow))
          return;
        this.Controller.CurrentControllerRow = this.Controller.FindRowByRowValue(icollectionViewHelper.Collection.CurrentItem, -1);
      }
      else
        base.RestoreCurrentRow();
    }
  }
}
