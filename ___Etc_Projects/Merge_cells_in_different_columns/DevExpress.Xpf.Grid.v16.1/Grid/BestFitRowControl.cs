// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.BestFitRowControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Collections.Generic;

namespace DevExpress.Xpf.Grid
{
  public class BestFitRowControl : RowControl
  {
    public GridColumnData CellData { get; private set; }

    protected override bool ShowDetails
    {
      get
      {
        return false;
      }
    }

    protected override bool AllowTreeIndentScrolling
    {
      get
      {
        return false;
      }
    }

    public BestFitRowControl(RowData rowData, GridColumnData cellData)
      : base(rowData)
    {
      this.CellData = cellData;
    }

    protected override void CreateDefaultContent()
    {
      this.CellsControl = this.CreateAndInitFixedNoneCellsControl(0, (Func<RowData, IList<GridColumnData>>) (x => (IList<GridColumnData>) new List<GridColumnData>()
      {
        this.CellData
      }), (Func<BandsLayoutBase, IList<BandBase>>) (x => (IList<BandBase>) null));
    }

    protected override CellsControl CreateCellsControl(Func<RowData, IList<GridColumnData>> getCellDataFunc, Func<BandsLayoutBase, IList<BandBase>> getBandsFunc)
    {
      return (CellsControl) new BestFitCellsControl((RowControl) this, getCellDataFunc, getBandsFunc);
    }

    protected override void UpdateOffsetPresenterLevel()
    {
    }
  }
}
