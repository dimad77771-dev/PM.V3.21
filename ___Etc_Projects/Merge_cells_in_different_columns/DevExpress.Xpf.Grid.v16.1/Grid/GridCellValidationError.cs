// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridCellValidationError
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.XtraEditors.DXErrorProvider;
using System;

namespace DevExpress.Xpf.Grid
{
  public class GridCellValidationError : RowValidationError
  {
    public int RowHandle
    {
      get
      {
        return this.fRowHandle;
      }
    }

    public GridColumn Column { get; private set; }

    public GridCellValidationError(object errorContent, Exception exception, ErrorType errorType, int rowHandle, GridColumn column)
      : base(errorContent, exception, errorType, rowHandle, true)
    {
      this.Column = column;
    }

    public override bool Equals(object obj)
    {
      GridCellValidationError cellValidationError = obj as GridCellValidationError;
      if (cellValidationError != null && object.Equals(this.ErrorContent, cellValidationError.ErrorContent) && (object.Equals((object) this.Exception, (object) cellValidationError.Exception) && object.Equals((object) this.ErrorType, (object) cellValidationError.ErrorType)) && object.Equals((object) this.RowHandle, (object) cellValidationError.RowHandle))
        return object.Equals((object) this.Column, (object) cellValidationError.Column);
      return false;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
