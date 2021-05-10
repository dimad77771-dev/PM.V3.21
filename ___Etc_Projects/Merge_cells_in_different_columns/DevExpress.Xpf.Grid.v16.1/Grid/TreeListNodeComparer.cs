// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListNodeComparer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Xpf.Grid.TreeList;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;

namespace DevExpress.Xpf.Grid
{
  public class TreeListNodeComparer : Comparer<TreeListNode>
  {
    private TreeListDataProvider provider;
    private IList<GridSortInfo> sortInfo;

    protected TreeListDataProvider DataProvider
    {
      get
      {
        return this.provider;
      }
    }

    public TreeListNodeComparer(TreeListDataProvider provider)
    {
      this.provider = provider;
    }

    public void SetSortInfo(IList<GridSortInfo> sortInfo)
    {
      this.sortInfo = sortInfo;
    }

    public override int Compare(TreeListNode node1, TreeListNode node2)
    {
      if (node1 == node2)
        return 0;
      int num = 0;
      TreeListCustomColumnSortEventArgs e = (TreeListCustomColumnSortEventArgs) null;
      ColumnSortOrder columnSortOrder = ColumnSortOrder.None;
      if (this.sortInfo != null)
      {
        foreach (GridSortInfo gridSortInfo in (IEnumerable<GridSortInfo>) this.sortInfo)
        {
          ColumnBase column = this.DataProvider.View.ColumnsCore[gridSortInfo.FieldName];
          if (column != null)
          {
            object nodeValue1 = this.GetNodeValue(node1, column);
            object nodeValue2 = this.GetNodeValue(node2, column);
            if (e == null)
              e = new TreeListCustomColumnSortEventArgs(column, node1, node2, nodeValue1, nodeValue2);
            else
              e.SetArgs(column, nodeValue1, nodeValue2);
            num = this.CompareInternal(node1, node2, nodeValue1, nodeValue2, column, e);
            ColumnSortOrder sortOrder = gridSortInfo.GetSortOrder();
            if (sortOrder == ColumnSortOrder.Descending)
              num = -num;
            columnSortOrder = sortOrder;
            if (num != 0)
              break;
          }
        }
      }
      if (num == 0)
      {
        num = Comparer<int>.Default.Compare(node1.Id, node2.Id);
        if (columnSortOrder == ColumnSortOrder.Descending)
          num = -num;
      }
      return num;
    }

    protected object GetNodeValue(TreeListNode node, ColumnBase column)
    {
      return this.DataProvider.View.GetNodeValue(node, column);
    }

    protected int CompareInternal(TreeListNode node1, TreeListNode node2, object value1, object value2, ColumnBase column, TreeListCustomColumnSortEventArgs e)
    {
      ColumnSortMode sortMode = column.SortMode;
      if (sortMode == ColumnSortMode.Custom)
      {
        this.DataProvider.CustomNodeSort(e);
        if (e.Handled)
          return e.Result;
      }
      if (value1 == DBNull.Value || value1 == null)
        return value2 == DBNull.Value || value2 == null ? 0 : -1;
      if (value2 == DBNull.Value || value2 == null)
        return 1;
      if (sortMode == ColumnSortMode.DisplayText)
        return Comparer<string>.Default.Compare(this.DataProvider.View.GetNodeDisplayText(node1, column.FieldName, value1), this.DataProvider.View.GetNodeDisplayText(node2, column.FieldName, value2));
      return Comparer<object>.Default.Compare(value1, value2);
    }
  }
}
