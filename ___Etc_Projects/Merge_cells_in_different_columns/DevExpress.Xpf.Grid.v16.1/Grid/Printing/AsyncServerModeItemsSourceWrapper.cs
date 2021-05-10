// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.AsyncServerModeItemsSourceWrapper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Async;
using DevExpress.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace DevExpress.Xpf.Grid.Printing
{
  public class AsyncServerModeItemsSourceWrapper : ServerModeItemsSourceWrapperBase, IListAdapterAsync, IListAdapter, ITypedList
  {
    private readonly IAsyncListServer Server;
    private readonly DataViewBase View;
    private ReportsAsyncResult Result;

    private ITypedList TypedList
    {
      get
      {
        return this.Server as ITypedList;
      }
    }

    public AsyncServerModeItemsSourceWrapper(TableView view)
    {
      this.View = (DataViewBase) view;
      this.Server = (view.DataProviderBase.DataController.DataSource as IDXCloneable).DXClone() as IAsyncListServer;
      this.Server.SetReceiver((IAsyncResultReceiver) new AsyncListWrapper(view.DataProviderBase.DataController as AsyncServerModeDataController, this.Server));
    }

    public IAsyncResult BeginFillList(IServiceProvider servProvider, CancellationToken token)
    {
      this.IsFilled = false;
      this.Clear();
      this.Result = new ReportsAsyncResult(this.View, this.Server, this.Server.Apply(this.GetReportFilter(servProvider), (ICollection<ServerModeOrderDescriptor>) null, 0, (ICollection<ServerModeSummaryDescriptor>) null, (ICollection<ServerModeSummaryDescriptor>) null), this.Server.GetAllFilteredAndSortedRows());
      this.Result.Start();
      return (IAsyncResult) this.Result;
    }

    public void EndFillList(IAsyncResult result, CancellationToken token)
    {
      this.AddRange((IEnumerable<object>) this.Result.Rows);
      this.IsFilled = true;
    }

    public void FillList(IServiceProvider servProvider)
    {
      CancellationToken token = new CancellationToken();
      this.EndFillList(this.BeginFillList(servProvider, token), token);
    }

    public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
    {
      return this.TypedList.GetItemProperties(listAccessors);
    }

    public string GetListName(PropertyDescriptor[] listAccessors)
    {
      return this.TypedList.GetListName(listAccessors);
    }
  }
}
