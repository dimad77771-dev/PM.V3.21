// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.GetRowsAsyncOperation
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevExpress.Xpf.Data
{
  internal class GetRowsAsyncOperation : AsyncOperationBase<IList>
  {
    private readonly int startFrom;
    private readonly int count;
    private Task<object>[] tasks;

    private int EndRowHandle
    {
      get
      {
        return this.startFrom + this.count - 1;
      }
    }

    public GetRowsAsyncOperation(GridControl grid, int startFrom, int count)
      : base(grid)
    {
      this.count = count;
      this.startFrom = startFrom;
      this.PopulateTasks();
    }

    private void PopulateTasks()
    {
      if (!this.IsValidOperation())
        return;
      this.tasks = new Task<object>[this.count];
      for (int index = 0; index < this.count; ++index)
        this.tasks[index] = this.Grid.GetRowAsync(index + this.startFrom);
    }

    protected override Task<IList> GetTaskCore()
    {
      return new Task<IList>((Func<IList>) (() =>
      {
        Task.WaitAll((Task[]) this.tasks);
        List<object> list = ((IEnumerable<Task<object>>) this.tasks).Select<Task<object>, object>((Func<Task<object>, object>) (t => t.Result)).ToList<object>();
        if (!list.Contains((object) null))
          return (IList) list;
        return (IList) null;
      }));
    }

    protected override bool IsValidOperationCore()
    {
      if (this.Grid.IsValidRowHandle(this.startFrom))
        return this.Grid.IsValidRowHandle(this.EndRowHandle);
      return false;
    }
  }
}
