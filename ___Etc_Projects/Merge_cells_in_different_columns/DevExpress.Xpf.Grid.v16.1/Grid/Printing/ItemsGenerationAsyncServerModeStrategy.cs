// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.ItemsGenerationAsyncServerModeStrategy
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Async;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Threading;

namespace DevExpress.Xpf.Grid.Printing
{
  public class ItemsGenerationAsyncServerModeStrategy : ItemsGenerationServerStrategy
  {
    public ItemsGenerationAsyncServerModeStrategy(DataViewBase view)
      : base(view)
    {
    }

    protected override IList GetPrintSelectedRowsOnlyAllRows()
    {
      return this.FetchAllFilteredAndSortedRows();
    }

    protected override IList FetchAllFilteredAndSortedRows()
    {
      AsyncServerModeDataController modeDataController = (AsyncServerModeDataController) base.DataProvider.DataController;
      CommandGetAllFilteredAndSortedRows filteredAndSortedRows = modeDataController.Server.GetAllFilteredAndSortedRows();
      string progressWindowTitle = this.View.GetLocalizedString(GridControlStringId.ProgressWindowTitle);
      string cancelButtonCaption = this.View.GetLocalizedString(GridControlStringId.ProgressWindowCancel);
      ManualResetEvent stopEvent = new ManualResetEvent(false);
      try
      {
        Thread thread = new Thread((ThreadStart) (() =>
        {
          ProgressControl.CreateProgressWindow(stopEvent, true, progressWindowTitle, cancelButtonCaption).Show();
          Dispatcher.Run();
        }));
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        while (!modeDataController.Server.WaitFor((Command) filteredAndSortedRows))
        {
          if (thread.Join(100))
          {
            modeDataController.Server.Cancel((Command) filteredAndSortedRows);
            break;
          }
        }
        stopEvent.Set();
        thread.Join();
      }
      finally
      {
        if (stopEvent != null)
          stopEvent.Dispose();
      }
      if (!filteredAndSortedRows.IsCanceled)
        return filteredAndSortedRows.Rows;
      return (IList) new List<object>();
    }
  }
}
