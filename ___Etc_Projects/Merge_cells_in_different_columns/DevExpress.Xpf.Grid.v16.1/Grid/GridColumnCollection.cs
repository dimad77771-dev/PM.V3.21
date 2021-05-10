// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridColumnCollection
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Linq;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Represents the grid's column collection.
  /// </para>
  ///             </summary>
  public class GridColumnCollection : ColumnCollectionBase<GridColumn>
  {
    private GridControl grid
    {
      get
      {
        return (GridControl) this.Owner;
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the GridColumnCollection class with the specified owner.
    /// </para>
    ///             </summary>
    /// <param name="grid">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridControl" /> object that owns the current collection.
    /// 
    ///           </param>
    public GridColumnCollection(GridControl grid)
      : base((DataControlBase) grid)
    {
    }

    protected override void OnRemoveItem(GridColumn column)
    {
      if (!this.grid.IsDeserializing && !this.grid.IsDesignColumnMoverApplyActive)
      {
        if (column.IsGrouped)
          this.grid.ActualGroupCountCore = Math.Max(0, this.grid.ActualGroupCountCore - 1);
        this.grid.UngroupBy(column);
        if (this.grid.SortInfo[column.FieldName] != null)
          this.grid.SortInfo.Remove(this.grid.SortInfo[column.FieldName]);
        GridSortInfo gridSortInfo = this.grid.ActualSortInfoCore.FirstOrDefault<GridSortInfo>((Func<GridSortInfo, bool>) (info => info.FieldName == column.FieldName));
        if (gridSortInfo != null)
          this.grid.ActualSortInfoCore.Remove(gridSortInfo);
      }
      this.OnRemoveItem(column);
    }

    protected override void ClearItems()
    {
      if (!this.grid.IsDeserializing && !this.grid.IsDesignColumnMoverApplyActive)
        this.grid.GroupCount = 0;
      base.ClearItems();
    }
  }
}
