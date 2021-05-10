// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.RowBaseWrapper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.XtraExport.Helpers;

namespace DevExpress.Xpf.Grid.Printing
{
  public abstract class RowBaseWrapper : IRowBase
  {
    private int rowHandle;
    private int rowLevel;
    private int dataSourceRowIndexCore;

    public int LogicalPosition
    {
      get
      {
        return this.rowHandle;
      }
    }

    public abstract bool IsGroupRow { get; }

    public FormatSettings FormatSettings
    {
      get
      {
        return (FormatSettings) null;
      }
    }

    public int DataSourceRowIndex
    {
      get
      {
        return this.dataSourceRowIndexCore;
      }
    }

    public string FormatString
    {
      get
      {
        return (string) null;
      }
    }

    public bool IsDataAreaRow
    {
      get
      {
        return true;
      }
    }

    public RowBaseWrapper(int rowHandle, int rowLevel, int dataSourceRowIndex)
    {
      this.rowHandle = rowHandle;
      this.rowLevel = rowLevel;
      this.dataSourceRowIndexCore = dataSourceRowIndex;
    }

    public int GetRowLevel()
    {
      return this.rowLevel;
    }
  }
}
