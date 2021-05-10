// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.ReportsAsyncResult
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Threading;

namespace DevExpress.Xpf.Grid.Printing
{
  public class ReportsAsyncResult : IAsyncResult
  {
    private readonly CommandApply CommandApplyFilter;
    private readonly CommandGetAllFilteredAndSortedRows CommandGetRows;
    private readonly IAsyncListServer Server;
    private readonly DataViewBase View;
    private bool completed;

    public IList<object> Rows { get; private set; }

    public object AsyncState
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public WaitHandle AsyncWaitHandle
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public bool CompletedSynchronously
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public bool IsCompleted
    {
      get
      {
        return this.completed;
      }
    }

    public ReportsAsyncResult(DataViewBase view, IAsyncListServer server, CommandApply commandApplyFilter, CommandGetAllFilteredAndSortedRows commandGetRows)
    {
      this.View = view;
      this.Server = server;
      this.CommandApplyFilter = commandApplyFilter;
      this.CommandGetRows = commandGetRows;
    }

    public void Start()
    {
      this.Rows = this.ApplyAsyncCommand();
      this.completed = true;
    }

    private IList<object> ApplyAsyncCommand()
    {
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
        while (!this.Server.WaitFor((Command) this.CommandApplyFilter))
        {
          if (thread.Join(100))
          {
            this.Server.Cancel((Command) this.CommandApplyFilter);
            break;
          }
        }
        while (!this.Server.WaitFor((Command) this.CommandGetRows))
        {
          if (thread.Join(100))
          {
            this.Server.Cancel((Command) this.CommandGetRows);
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
      if (!this.CommandGetRows.IsCanceled)
        return (IList<object>) this.CommandGetRows.Rows.Cast<object>().ToList<object>();
      return (IList<object>) new List<object>();
    }
  }
}
