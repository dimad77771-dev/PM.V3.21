// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.LoadRowAsyncOperation`1
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Xpf.Grid;
using System;
using System.Threading;

namespace DevExpress.Xpf.Data
{
  internal class LoadRowAsyncOperation<T> : SingleRowAsyncOperation<T>
  {
    private readonly int rowHandle;

    public LoadRowAsyncOperation(GridControl grid, int rowHandle, Func<T> getValueCallback)
      : base(grid, getValueCallback)
    {
      this.rowHandle = rowHandle;
    }

    protected override bool IsValidOperationCore()
    {
      return this.Grid.IsValidRowHandle(this.rowHandle);
    }

    protected override bool NeedLoadCore()
    {
      return !this.DataController.IsRowLoaded(this.Grid.IsGroupRowHandle(this.rowHandle) ? this.DataController.GetControllerRowByGroupRow(this.rowHandle) : this.rowHandle);
    }

    protected override void BeginLoadCore(ManualResetEvent localWaitHandle)
    {
      this.DataController.GetRow(this.rowHandle, (OperationCompleted) (o => this.OnLoaded(localWaitHandle, o)));
    }
  }
}
