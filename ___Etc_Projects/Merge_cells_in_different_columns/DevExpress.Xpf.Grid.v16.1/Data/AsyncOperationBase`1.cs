// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.AsyncOperationBase`1
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Xpf.Grid;
using System;
using System.Threading.Tasks;

namespace DevExpress.Xpf.Data
{
  internal abstract class AsyncOperationBase<T>
  {
    private readonly GridControl grid;

    protected GridControl Grid
    {
      get
      {
        return this.grid;
      }
    }

    protected AsyncOperationWaitHandle WaitHandle
    {
      get
      {
        return this.Grid.AsyncWaitHandle;
      }
    }

    protected DataController DataController
    {
      get
      {
        return this.Grid.DataController;
      }
    }

    protected virtual T FallbackValue
    {
      get
      {
        return default (T);
      }
    }

    protected bool IsAsyncServerMode
    {
      get
      {
        return this.Grid.DataProviderBase.IsAsyncServerMode;
      }
    }

    protected AsyncOperationBase(GridControl grid)
    {
      this.grid = grid;
    }

    protected bool IsValidOperation()
    {
      if (this.DataController != null)
        return this.IsValidOperationCore();
      return false;
    }

    private Task<T> RunDataControllerTask(Task<T> task)
    {
      if (this.IsAsyncServerMode)
        task.Start();
      else
        task.RunSynchronously();
      return task;
    }

    public Task<T> GetTask()
    {
      return this.RunDataControllerTask(this.IsValidOperation() ? this.GetTaskCore() : this.GetDefaultTask());
    }

    protected virtual bool IsValidOperationCore()
    {
      return true;
    }

    protected Task<T> GetDefaultTask()
    {
      return new Task<T>((Func<T>) (() => this.FallbackValue));
    }

    protected abstract Task<T> GetTaskCore();
  }
}
