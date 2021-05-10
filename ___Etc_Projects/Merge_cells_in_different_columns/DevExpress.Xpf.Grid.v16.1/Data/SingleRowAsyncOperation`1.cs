// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.SingleRowAsyncOperation`1
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DevExpress.Xpf.Data
{
  internal abstract class SingleRowAsyncOperation<T> : AsyncOperationBase<T>
  {
    private readonly Func<T> getValueCallback;
    private bool isLoaded;
    private T result;

    protected T AsyncResult
    {
      get
      {
        return this.result;
      }
    }

    protected virtual bool CanCompleteWithoutSynchronization
    {
      get
      {
        return false;
      }
    }

    protected SingleRowAsyncOperation(GridControl grid, Func<T> getValueCallback)
      : base(grid)
    {
      this.getValueCallback = getValueCallback;
    }

    protected SingleRowAsyncOperation(GridControl grid)
      : base(grid)
    {
    }

    protected override Task<T> GetTaskCore()
    {
      return this.WaitHandle.Run<Task<T>>((Func<ManualResetEvent, Task<T>>) (wh => this.LoadRowAsyncCore(wh)));
    }

    private Task<T> LoadRowAsyncCore(ManualResetEvent localWaitHandle)
    {
      SynchronizationContext uiContext = (SynchronizationContext) new DispatcherSynchronizationContext(this.Grid.Dispatcher);
      this.BeginLoadIfNeeded(localWaitHandle);
      return new Task<T>((Func<T>) (() =>
      {
        localWaitHandle.WaitOne();
        if (!this.isLoaded && this.WaitHandle.IsInterrupted)
          return this.FallbackValue;
        T result = default (T);
        if (this.IsAsyncServerMode && this.CanCompleteWithoutSynchronization)
          return this.GetValueCore();
        uiContext.Send((SendOrPostCallback) (o => result = this.GetValueCore()), (object) result);
        return result;
      }));
    }

    private void BeginLoadIfNeeded(ManualResetEvent localWaitHandle)
    {
      if (!this.NeedLoad())
      {
        this.isLoaded = true;
      }
      else
      {
        this.isLoaded = false;
        localWaitHandle.Reset();
        this.BeginLoadCore(localWaitHandle);
      }
    }

    protected void OnLoaded(ManualResetEvent localWaitHandle, object result)
    {
      this.isLoaded = true;
      this.result = (T) result;
      localWaitHandle.Set();
    }

    private bool NeedLoad()
    {
      if (this.IsAsyncServerMode)
        return this.NeedLoadCore();
      return false;
    }

    protected virtual T GetValueCore()
    {
      return this.getValueCallback();
    }

    protected virtual bool NeedLoadCore()
    {
      return true;
    }

    protected abstract void BeginLoadCore(ManualResetEvent localWaitHandle);
  }
}
