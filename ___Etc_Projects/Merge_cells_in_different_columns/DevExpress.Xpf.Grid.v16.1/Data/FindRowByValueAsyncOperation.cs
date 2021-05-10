// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.FindRowByValueAsyncOperation
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Xpf.Grid;
using System;
using System.Linq;
using System.Threading;

namespace DevExpress.Xpf.Data
{
  internal class FindRowByValueAsyncOperation : SingleRowAsyncOperation<int>
  {
    private readonly string columnName;
    private readonly object value;

    protected override int FallbackValue
    {
      get
      {
        return int.MinValue;
      }
    }

    protected override bool CanCompleteWithoutSynchronization
    {
      get
      {
        return true;
      }
    }

    public FindRowByValueAsyncOperation(GridControl grid, string columnName, object value)
      : base(grid)
    {
      this.value = value;
      this.columnName = columnName;
    }

    protected override void BeginLoadCore(ManualResetEvent localWaitHandle)
    {
      int rowByValue = this.DataController.FindRowByValue(this.columnName, this.value, (OperationCompleted) (o => this.OnLoaded(localWaitHandle, o)));
      if (rowByValue == -2147483638)
        return;
      this.OnLoaded(localWaitHandle, (object) rowByValue);
    }

    protected override bool IsValidOperationCore()
    {
      if (string.IsNullOrEmpty(this.columnName) || !this.Grid.Columns.Any<GridColumn>((Func<GridColumn, bool>) (col => col.FieldName == this.columnName)))
        return false;
      return this.DataController.FindRowByValue(this.columnName, this.value) != int.MinValue;
    }

    protected override int GetValueCore()
    {
      if (!this.IsAsyncServerMode)
        return this.DataController.FindRowByValue(this.columnName, this.value);
      return this.AsyncResult;
    }
  }
}
