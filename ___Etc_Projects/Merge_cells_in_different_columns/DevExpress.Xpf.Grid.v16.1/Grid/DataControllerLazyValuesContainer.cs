// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DataControllerLazyValuesContainer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;

namespace DevExpress.Xpf.Grid
{
  public struct DataControllerLazyValuesContainer
  {
    private GridViewBase view;
    private int? rowHandle;
    private int? listSourceIndex;

    private GridControl Grid
    {
      get
      {
        return this.view.Grid;
      }
    }

    public int RowHandle
    {
      get
      {
        if (!this.rowHandle.HasValue)
          this.rowHandle = new int?(this.Grid.GetRowHandleByListIndex(this.ListSourceIndex));
        return this.rowHandle.Value;
      }
    }

    public int ListSourceIndex
    {
      get
      {
        if (!this.listSourceIndex.HasValue)
          this.listSourceIndex = new int?(this.RowHandle == int.MinValue ? int.MinValue : this.Grid.GetListIndexByRowHandle(this.RowHandle));
        return this.listSourceIndex.Value;
      }
    }

    public DataControllerLazyValuesContainer(GridViewBase view, int? rowHandle, int? listSourceIndex)
    {
      this.view = view;
      if (rowHandle.HasValue && listSourceIndex.HasValue || !rowHandle.HasValue && !listSourceIndex.HasValue)
        throw new ArgumentException("rowHandle, listSourceIndex");
      this.rowHandle = rowHandle;
      this.listSourceIndex = listSourceIndex;
    }
  }
}
