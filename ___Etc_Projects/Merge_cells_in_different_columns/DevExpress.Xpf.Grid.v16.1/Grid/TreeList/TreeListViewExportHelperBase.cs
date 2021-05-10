// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListViewExportHelperBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Printing;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Grid.TreeList
{
  public abstract class TreeListViewExportHelperBase : DataViewExportHelperBase<ColumnWrapper, TreeListNodeWrapper>
  {
    protected TreeListVisibleNodesProvider nodesProvider;

    protected List<TreeListNodeObject> Nodes
    {
      get
      {
        return this.nodesProvider.Nodes;
      }
    }

    public override bool ShowGroupedColumns
    {
      get
      {
        return false;
      }
    }

    public override bool ShowBandsPanel
    {
      get
      {
        return this.View.ShowBandsPanel;
      }
    }

    public override IEnumerable<ISummaryItemEx> GridGroupSummaryItemCollection
    {
      get
      {
        return (IEnumerable<ISummaryItemEx>) new List<ISummaryItemEx>();
      }
    }

    public override IEnumerable<ISummaryItemEx> GroupHeaderSummaryItems
    {
      get
      {
        return (IEnumerable<ISummaryItemEx>) new List<ISummaryItemEx>();
      }
    }

    public TreeListView View
    {
      get
      {
        return base.View as TreeListView;
      }
    }

    protected override long RowCountCore
    {
      get
      {
        return (long) this.View.TreeListDataProvider.VisibleCount;
      }
    }

    protected override FormatConditionCollection FormatConditionsCore
    {
      get
      {
        return this.View.FormatConditions;
      }
    }

    protected TreeListViewExportHelperBase(TreeListView view, ExportTarget target)
      : base((DataViewBase) view, target)
    {
      this.nodesProvider = new TreeListVisibleNodesProvider(view.Nodes);
    }

    public override IEnumerable<ColumnWrapper> GetGroupedColumns()
    {
      return (IEnumerable<ColumnWrapper>) null;
    }

    public override bool GetAllowMerge(ColumnWrapper col)
    {
      return false;
    }

    public override IEnumerable<TreeListNodeWrapper> GetAllRows()
    {
      return this.Nodes.Select<TreeListNodeObject, TreeListNodeWrapper>((Func<TreeListNodeObject, TreeListNodeWrapper>) (node => new TreeListNodeWrapper(node)));
    }
  }
}
