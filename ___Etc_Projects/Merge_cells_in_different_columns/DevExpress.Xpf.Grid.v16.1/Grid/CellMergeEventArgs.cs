// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CellMergeEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;

namespace DevExpress.Xpf.Grid
{
  public class CellMergeEventArgs : EventArgs
  {
    public GridColumn Column { get; private set; }

    public int RowHandle1 { get; private set; }

    public int RowHandle2 { get; private set; }

    public object CellValue1 { get; private set; }

    public object CellValue2 { get; private set; }

    public bool Handled { get; set; }

    public bool Merge { get; set; }

    internal void SetArgs(GridColumn column, int rowHandle1, int rowHandle2, object cellValue1, object cellValue2)
    {
      this.Column = column;
      this.RowHandle1 = rowHandle1;
      this.RowHandle2 = rowHandle2;
      this.CellValue1 = cellValue1;
      this.CellValue2 = cellValue2;
      this.Handled = false;
      this.Merge = false;
    }
  }
}
