// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.AsyncOperationWaitHandle
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Collections.Generic;
using System.Threading;

namespace DevExpress.Xpf.Data
{
  internal class AsyncOperationWaitHandle
  {
    private readonly List<ManualResetEvent> handlers = new List<ManualResetEvent>();
    private bool isInterrupted;

    public bool IsInterrupted
    {
      get
      {
        return this.isInterrupted;
      }
      set
      {
        this.isInterrupted = value;
        if (!value)
          return;
        foreach (EventWaitHandle handler in this.handlers)
          handler.Set();
      }
    }

    public T Run<T>(Func<ManualResetEvent, T> action)
    {
      ManualResetEvent manualResetEvent = new ManualResetEvent(true);
      this.handlers.Add(manualResetEvent);
      return action(manualResetEvent);
    }
  }
}
