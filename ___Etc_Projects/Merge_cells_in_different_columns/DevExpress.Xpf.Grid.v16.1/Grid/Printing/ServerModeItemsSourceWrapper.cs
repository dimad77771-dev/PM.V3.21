// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.ServerModeItemsSourceWrapper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Grid.Printing
{
  public class ServerModeItemsSourceWrapper : ServerModeItemsSourceWrapperBase, IListAdapter
  {
    private readonly IListServer Server;

    bool IListAdapter.IsFilled
    {
      get
      {
        return this.IsFilled;
      }
    }

    public ServerModeItemsSourceWrapper(TableView view)
    {
      this.Server = (view.DataProviderBase.DataController.DataSource as IDXCloneable).DXClone() as IListServer;
    }

    public void FillList(IServiceProvider servProvider)
    {
      this.IsFilled = false;
      this.Clear();
      this.Server.Apply(this.GetReportFilter(servProvider), (ICollection<ServerModeOrderDescriptor>) null, 0, (ICollection<ServerModeSummaryDescriptor>) null, (ICollection<ServerModeSummaryDescriptor>) null);
      this.AddRange(this.Server.GetAllFilteredAndSortedRows().Cast<object>());
      this.IsFilled = true;
    }
  }
}
