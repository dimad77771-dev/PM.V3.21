// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.CollectionViewItemsSourceWrapper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Linq;
using System;
using System.Linq;

namespace DevExpress.Xpf.Grid.Printing
{
  public class CollectionViewItemsSourceWrapper : ServerModeItemsSourceWrapperBase, IListAdapter
  {
    private readonly ICollectionViewHelper Server;

    public CollectionViewItemsSourceWrapper(TableView view)
    {
      this.Server = view.DataProviderBase.DataController.DataSource as ICollectionViewHelper;
    }

    public void FillList(IServiceProvider servProvider)
    {
      this.IsFilled = false;
      this.Clear();
      this.AddRange(this.Server.GetAllFilteredAndSortedRows().Cast<object>());
      this.IsFilled = true;
    }
  }
}
