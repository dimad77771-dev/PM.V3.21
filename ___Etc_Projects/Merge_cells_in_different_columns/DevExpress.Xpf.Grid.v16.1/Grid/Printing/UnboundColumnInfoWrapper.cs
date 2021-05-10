// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.UnboundColumnInfoWrapper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.XtraExport.Helpers;

namespace DevExpress.Xpf.Grid.Printing
{
  public class UnboundColumnInfoWrapper : IUnboundInfo
  {
    private ColumnBase column;

    public string UnboundExpression
    {
      get
      {
        return this.column.UnboundExpression;
      }
    }

    public UnboundColumnType UnboundType
    {
      get
      {
        return this.column.UnboundType;
      }
    }

    public UnboundColumnInfoWrapper(ColumnBase column)
    {
      this.column = column;
    }
  }
}
