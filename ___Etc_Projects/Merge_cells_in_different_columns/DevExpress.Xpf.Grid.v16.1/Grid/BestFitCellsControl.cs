// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.BestFitCellsControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Collections.Generic;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class BestFitCellsControl : CellsControl
  {
    public BestFitCellsControl(RowControl rowControl, Func<RowData, IList<GridColumnData>> getCellDataFunc, Func<BandsLayoutBase, IList<BandBase>> getBandsFunc)
      : base(rowControl, getCellDataFunc, getBandsFunc)
    {
    }

    protected override void UpdateElementWidth(FrameworkElement element, GridCellData cellData)
    {
    }

    protected override void ValidateElementCore(FrameworkElement element, GridCellData cellData)
    {
      base.ValidateElementCore(element, cellData);
      ((CellEditorBase) element).IsFocusedCell = this.RowControl.rowData.View.GetIsCellFocused(this.RowControl.rowData.RowHandle.Value, cellData.Column);
    }

    protected internal override void UpdatePanel()
    {
    }

    protected override FrameworkElement CreateChildCore(GridCellData cellData)
    {
      BestFitCellEditor bestFitCellEditor = new BestFitCellEditor((CellsControl) this);
      bestFitCellEditor.RowData = cellData.RowData;
      return (FrameworkElement) bestFitCellEditor;
    }
  }
}
