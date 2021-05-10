// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GridViewClipboardSelectionProvider
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Grid.Printing
{
  public class GridViewClipboardSelectionProvider : ClipboardSelectionProvider<ColumnWrapper, RowBaseWrapper>
  {
    private GridViewClipboardHelper helper;

    protected TableView View
    {
      get
      {
        return base.View as TableView;
      }
    }

    public GridViewClipboardSelectionProvider(GridViewClipboardHelper helper)
      : base(((DataViewExportHelperBase<ColumnWrapper, RowBaseWrapper>) helper).View)
    {
      this.helper = helper;
    }

    public override IList<ColumnWrapper> GetColumns(IEnumerable collection, bool isGroupColumn = false)
    {
      List<ColumnWrapper> columnWrapperList = new List<ColumnWrapper>();
      int num = 0;
      foreach (GridColumn gridColumn in collection)
      {
        if (this.CanAddColumn(gridColumn, isGroupColumn))
          columnWrapperList.Add(DataViewExportHelperBase<ColumnWrapper, RowBaseWrapper>.CreateColumn((ColumnBase) gridColumn, num++));
      }
      return (IList<ColumnWrapper>) columnWrapperList;
    }

    public override IEnumerable<RowBaseWrapper> GetSelectedRows()
    {
      int[] selectedRowHandles = this.View.DataControl.GetSelectedRowHandles();
      Array.Sort<int>(selectedRowHandles, (Comparison<int>) ((i1, i2) => this.View.DataControl.GetRowVisibleIndexByHandleCore(i1).CompareTo(this.View.DataControl.GetRowVisibleIndexByHandleCore(i2))));
      return ((IEnumerable<int>) selectedRowHandles).Select<int, RowBaseWrapper>((Func<int, RowBaseWrapper>) (i => this.helper.GetRowByRowHandle(i)));
    }

    private bool CanAddColumn(GridColumn gridColumn, bool isGroupColumn)
    {
      bool flag = isGroupColumn || this.View.ShowGroupedColumns || gridColumn.GroupIndex == -1;
      if (gridColumn == this.View.CheckBoxSelectorColumn && !this.View.ShowCheckBoxSelectorColumn)
        return false;
      return flag;
    }
  }
}
