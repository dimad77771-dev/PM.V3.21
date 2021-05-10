// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.BandedColumnWrapper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.XtraExport.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Grid.Printing
{
  public class BandedColumnWrapper : ColumnWrapper, IBandedGridColumn, IColumn
  {
    private int columnIndex;
    private int columnRowIndex;
    private int id;

    public virtual bool AutoFillDown
    {
      get
      {
        return true;
      }
    }

    public int Id
    {
      get
      {
        return this.id;
      }
    }

    public virtual int ColVIndex
    {
      get
      {
        return this.columnIndex;
      }
    }

    public virtual int ColIndex
    {
      get
      {
        return this.columnIndex;
      }
    }

    public virtual int RowIndex
    {
      get
      {
        return this.columnRowIndex;
      }
    }

    public virtual int RowCount
    {
      get
      {
        return 1;
      }
    }

    public BandedColumnWrapper(ColumnBase column, int logicalPosition)
      : base(column, logicalPosition)
    {
      if (column == null)
        return;
      this.columnIndex = this.GetColumnIndex(column);
      this.columnRowIndex = this.GetColumnRowIndex(column);
    }

    internal BandedColumnWrapper(ColumnBase column, int logicalPosition, int id)
      : this(column, logicalPosition)
    {
      this.id = id;
    }

    public override int GetColumnGroupLevel()
    {
      return this.Column.ParentBand.Level + 1;
    }

    public override int GetHashCode()
    {
      return this.Column.GetHashCode();
    }

    public override IEnumerable<IColumn> GetAllColumns()
    {
      return (IEnumerable<IColumn>) new List<IColumn>();
    }

    private int GetColumnIndex(ColumnBase column)
    {
      if (column.BandRow == null)
        return -1;
      return column.BandRow.Columns.Where<ColumnBase>((Func<ColumnBase, bool>) (c => c.AllowPrinting)).ToList<ColumnBase>().IndexOf(this.Column);
    }

    private int GetColumnRowIndex(ColumnBase column)
    {
      int num = -1;
      if (column.ParentBand == null)
        return -1;
      foreach (BandRow actualRow in column.ParentBand.ActualRows)
      {
        if (actualRow.Columns.Count<ColumnBase>((Func<ColumnBase, bool>) (c => c.AllowPrinting)) > 0)
          ++num;
        if (actualRow.Columns.Contains(column))
          break;
      }
      return num;
    }
  }
}
