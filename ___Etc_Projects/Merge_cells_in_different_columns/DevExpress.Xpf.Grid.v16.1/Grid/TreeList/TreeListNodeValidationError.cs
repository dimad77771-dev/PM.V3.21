// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListNodeValidationError
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.XtraEditors.DXErrorProvider;
using System;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListNodeValidationError : RowValidationError
  {
    public int RowHandle
    {
      get
      {
        return this.fRowHandle;
      }
    }

    public TreeListNode Node { get; private set; }

    public TreeListNodeValidationError(object errorContent, Exception exception, ErrorType errorType, int rowHandle, TreeListNode node)
      : base(errorContent, exception, errorType, rowHandle, false)
    {
      this.Node = node;
    }
  }
}
