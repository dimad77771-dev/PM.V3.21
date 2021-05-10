// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListViewClipboardSelectionProvider
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Printing;
using DevExpress.XtraExport.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListViewClipboardSelectionProvider : ClipboardSelectionProvider<ColumnWrapper, TreeListNodeWrapper>
  {
    private TreeListVisibleNodesProvider nodesProvider;

    protected TreeListView View
    {
      get
      {
        return base.View as TreeListView;
      }
    }

    public TreeListViewClipboardSelectionProvider(TreeListView view, TreeListVisibleNodesProvider nodesProvider)
      : base((DataViewBase) view)
    {
      this.nodesProvider = nodesProvider;
    }

    public override IEnumerable<TreeListNodeWrapper> GetSelectedRows()
    {
      List<TreeListNodeWrapper> treeListNodeWrapperList = new List<TreeListNodeWrapper>();
      foreach (TreeListNode selectedNode in this.View.TreeListDataProvider.TreeListSelection.GetSelectedNodes())
      {
        if (selectedNode.ParentNode == null || !selectedNode.DataProvider.TreeListSelection.GetSelected(selectedNode.ParentNode))
          treeListNodeWrapperList.Add(new TreeListNodeWrapper(this.nodesProvider.GetNodeObject(selectedNode)));
      }
      return (IEnumerable<TreeListNodeWrapper>) treeListNodeWrapperList;
    }

    public override IList<ColumnWrapper> GetColumns(IEnumerable collection, bool isGroupColumn = false)
    {
      List<ColumnWrapper> columnWrapperList = new List<ColumnWrapper>();
      int num = 0;
      foreach (ColumnBase column in collection)
        columnWrapperList.Add(DataViewExportHelperBase<ColumnWrapper, TreeListNodeWrapper>.CreateColumn(column, num++));
      return (IList<ColumnWrapper>) columnWrapperList;
    }

    public override int GetSelectedCellsCountCore(IGroupRow<TreeListNodeWrapper> groupRow)
    {
      if (this.View.IsMultiCellSelection)
        return this.View.GetSelectedCells().Count;
      return this.GetColumns((IEnumerable) this.View.VisibleColumns, false).Count<ColumnWrapper>() * this.View.GetSelectedRowHandlesCore().Length;
    }
  }
}
