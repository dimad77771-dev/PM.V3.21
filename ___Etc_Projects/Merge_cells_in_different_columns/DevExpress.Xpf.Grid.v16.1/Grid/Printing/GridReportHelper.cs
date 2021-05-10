// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GridReportHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.ComponentModel;
using System.Data;

namespace DevExpress.Xpf.Grid.Printing
{
  public static class GridReportHelper
  {
    public static object GetSource(TableView view)
    {
      if (view.DataProviderBase == null)
        return (object) null;
      if (view.DataProviderBase.IsAsyncServerMode)
        return (object) new AsyncServerModeItemsSourceWrapper(view);
      if (view.DataProviderBase.IsServerMode)
      {
        if (view.DataProviderBase.IsICollectionView)
          return (object) new CollectionViewItemsSourceWrapper(view);
        return (object) new ServerModeItemsSourceWrapper(view);
      }
      if (view.DataProviderBase.DataSource is DataTable)
        return view.DataProviderBase.DataSource;
      if (view.DataProviderBase.DataController.DataSource is ITypedList)
        return view.DataProviderBase.DataController.DataSource;
      return view.DataProviderBase.DataSource;
    }
  }
}
