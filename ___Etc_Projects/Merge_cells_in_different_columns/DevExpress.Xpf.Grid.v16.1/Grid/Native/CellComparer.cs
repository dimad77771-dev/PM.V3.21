// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.CellComparer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;

namespace DevExpress.Xpf.Grid.Native
{
  public class CellComparer : IComparer<CellBase>
  {
    private DataViewBase view;

    public CellComparer(DataViewBase view)
    {
      this.view = view;
    }

    public int Compare(CellBase x, CellBase y)
    {
      int num = Comparer<int>.Default.Compare(this.view.DataControl.GetRowVisibleIndexByHandleCore(x.RowHandleCore), this.view.DataControl.GetRowVisibleIndexByHandleCore(y.RowHandleCore));
      if (num != 0)
        return num;
      return Comparer<int>.Default.Compare(x.ColumnCore.VisibleIndex, y.ColumnCore.VisibleIndex);
    }
  }
}
