// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.ItemsGenerationAsyncServerModeStrategyAsync
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Async;
using DevExpress.Xpf.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Threading;

namespace DevExpress.Xpf.Grid.Printing
{
  public class ItemsGenerationAsyncServerModeStrategyAsync : ItemsGenerationServerStrategy
  {
    private IList allFilteredAndSortedRows = (IList) new List<object>();
    private DispatcherTimer fetchingTimer;

    public ItemsGenerationAsyncServerModeStrategyAsync(DataViewBase view)
      : base(view)
    {
    }

    protected override IList GetPrintSelectedRowsOnlyAllRows()
    {
      return this.FetchAllFilteredAndSortedRows();
    }

    protected override IList FetchAllFilteredAndSortedRows()
    {
      return this.allFilteredAndSortedRows;
    }

    public void StartFetchingAllFilteredAndSortedRows(Action createPrintingNodeAction)
    {
      Dispatcher uiDispatcher = this.View.Dispatcher;
      AsyncServerModeDataController asyncDataController = (AsyncServerModeDataController) base.DataProvider.DataController;
      CommandGetAllFilteredAndSortedRows commandGetRows = asyncDataController.Server.GetAllFilteredAndSortedRows();
      DXWindow progressWindow = ProgressControl.CreateProgressWindow((ManualResetEvent) null, false, this.View.GetLocalizedString(GridControlStringId.ProgressWindowTitle), this.View.GetLocalizedString(GridControlStringId.ProgressWindowCancel));
      progressWindow.Closed += (EventHandler) ((s, e) => asyncDataController.Server.Cancel((Command) commandGetRows));
      progressWindow.Show();
      this.fetchingTimer = new DispatcherTimer();
      this.fetchingTimer.Interval = TimeSpan.FromMilliseconds(100.0);
      this.fetchingTimer.Tick += (EventHandler) ((s, e) =>
      {
        if (!this.fetchingTimer.IsEnabled || !asyncDataController.Server.WaitFor((Command) commandGetRows))
          return;
        this.fetchingTimer.Stop();
        this.allFilteredAndSortedRows = commandGetRows.IsCanceled ? (IList) new List<object>() : commandGetRows.Rows;
        progressWindow.Close();
        uiDispatcher.BeginInvoke((Delegate) createPrintingNodeAction);
      });
      this.fetchingTimer.Start();
    }
  }
}
