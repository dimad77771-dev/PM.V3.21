// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.AsyncServerModeGridDataController
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Helpers;
using DevExpress.Data.Selection;
using DevExpress.Xpf.Editors.Helpers;
using System;

namespace DevExpress.Xpf.Data
{
  public class AsyncServerModeGridDataController : AsyncServerModeDataController
  {
    private readonly IDataProviderOwner owner;

    private IListServerRefreshable ListServerRefreshable
    {
      get
      {
        return this.DataSource as IListServerRefreshable;
      }
    }

    public AsyncServerModeGridDataController(IDataProviderOwner owner)
    {
      this.owner = owner;
    }

    protected override SelectionController CreateSelectionController()
    {
      return (SelectionController) new RowStateController((DevExpress.Data.DataController) this);
    }

    protected override BaseDataControllerHelper CreateHelper()
    {
      return (BaseDataControllerHelper) new AsyncGridDataControllerHelper((AsyncServerModeDataController) this);
    }

    protected override void OnDataSourceChanged()
    {
      base.OnDataSourceChanged();
      if (this.ListServerRefreshable == null)
        return;
      this.ListServerRefreshable.Refresh += new EventHandler(this.OnListServerRefresh);
    }

    private void OnListServerRefresh(object sender, EventArgs e)
    {
      this.owner.DoActionWithPostponedUpdateLayout((Action) (() =>
      {
        if (this.IsRefreshInProgress)
          return;
        this.DoRefresh();
      }));
    }

    public override void Dispose()
    {
      if (this.ListServerRefreshable != null)
        this.ListServerRefreshable.Refresh -= new EventHandler(this.OnListServerRefresh);
      base.Dispose();
    }
  }
}
