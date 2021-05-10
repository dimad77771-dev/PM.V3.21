// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GridMasterDetailPrintingNode
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting.DataNodes;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DevExpress.Xpf.Grid.Printing
{
  public class GridMasterDetailPrintingNode : GridContainerPrintingNodeBase, IVisualGroupNode, IGroupNode, IDataNode
  {
    private readonly RowNode rowNode;
    private readonly NodeContainer parentContainer;
    private DataControlBase detailDataControl;
    private List<KeyValuePair<DataControlBase, bool>> details;
    private bool printStub;

    protected internal GridPrintingDataTreeBuilder TreeBuilder
    {
      get
      {
        return base.TreeBuilder as GridPrintingDataTreeBuilder;
      }
    }

    GroupUnion IGroupNode.Union
    {
      get
      {
        return GroupUnion.WithFirstDetail;
      }
    }

    bool IGroupNode.RepeatHeaderEveryPage
    {
      get
      {
        return true;
      }
    }

    public GridMasterDetailPrintingNode(NodeContainer parentContainer, RowNode rowNode, GridPrintingDataTreeBuilder treeBuilder, IDataNode parent, int index, Size pageSize)
      : base((DataNodeContainer) treeBuilder.RootNodeContainer, (PrintingDataTreeBuilderBase) treeBuilder, parent, index, pageSize)
    {
      this.rowNode = rowNode;
      this.parentContainer = parentContainer;
    }

    RowViewInfo IVisualGroupNode.GetFooter(bool allowContentReuse)
    {
      return (RowViewInfo) null;
    }

    protected override bool GetIsDetailContainerCore()
    {
      return this.printStub;
    }

    protected void UpdateReusingRowData()
    {
      this.TreeBuilder.ReusingRowData.AssignFromInternal((RowsContainer) null, this.parentContainer, this.rowNode, true);
    }

    RowViewInfo IVisualGroupNode.GetHeader(bool allowContentReuse)
    {
      this.UpdateReusingRowData();
      return this.CreateRowElement(this.TreeBuilder.ReusingRowData, this.TreeBuilder.PrintRowTemplate);
    }

    protected override bool CanGetChildCore(int index)
    {
      if (!this.TreeBuilder.GetAllowPrintDetailsValue() || index > this.GetPrintDeailsLimit())
        return false;
      this.UpdateReusingRowData();
      if (this.details == null)
        this.ActualizeDetails();
      if (!this.TreeBuilder.GetPrintAllDetailsValue())
      {
        if (!this.IsMasterRowExpanded())
        {
          if (index != 0)
            return false;
          this.printStub = true;
          return true;
        }
        DataControlDetailDescriptor detailDescriptor = MasterDetailPrintHelper.GetActiveDetailDescriptor(this.TreeBuilder, this.TreeBuilder.ReusingRowData.RowHandle.Value, (GridControl) null, true);
        if (detailDescriptor == null)
          return false;
        this.detailDataControl = MasterDetailPrintHelper.FindDetailDataControl(this.TreeBuilder, this.TreeBuilder.ReusingRowData.RowHandle.Value, detailDescriptor);
        if (MasterDetailPrintHelper.IsDetailContainsRows(this.TreeBuilder, this.TreeBuilder.ReusingRowData.RowHandle.Value, this.detailDataControl))
          return true;
        return this.CanPrintEmptyDetails(this.detailDataControl);
      }
      if (this.details.Count == 0)
      {
        if (index != 0)
          return false;
        this.printStub = true;
        return true;
      }
      if (!this.TreeBuilder.View.PrintSelectedRowsOnly)
        return true;
      foreach (KeyValuePair<DataControlBase, bool> detail in this.details)
      {
        if (MasterDetailPrintHelper.IsDetailContainsRows(this.TreeBuilder, this.TreeBuilder.ReusingRowData.RowHandle.Value, detail.Key))
          return true;
      }
      return false;
    }

    private void ActualizeDetails()
    {
      List<IDescriptorAndDataControlBase> descriptors = new List<IDescriptorAndDataControlBase>();
      MasterDetailPrintHelper.GetAllDetailDescriptors((DataControlBase) this.TreeBuilder.View.Grid).ForEach((Action<DataControlDetailDescriptor>) (descr => descriptors.Add(this.TreeBuilder.MasterDetailPrintInfo.RootPrintingDataTreeBuilder.GetDescriptorAndGridControl(descr))));
      this.details = new List<KeyValuePair<DataControlBase, bool>>();
      foreach (IDescriptorAndDataControlBase andDataControlBase in descriptors)
      {
        bool isGenerated = true;
        DataControlBase dataControlBase = MasterDetailPrintHelper.FindDetailDataControl(this.TreeBuilder, this.TreeBuilder.ReusingRowData.RowHandle.Value, (DataControlDetailDescriptor) andDataControlBase.Descriptor);
        if (dataControlBase == null || !dataControlBase.viewCore.PrintSelectedRowsOnly)
          dataControlBase = andDataControlBase.GetDetailGridControl((PrintingDataTreeBuilderBase) this.TreeBuilder, out isGenerated);
        if (MasterDetailPrintHelper.IsDetailContainsRows(this.TreeBuilder, this.TreeBuilder.ReusingRowData.RowHandle.Value, dataControlBase) || this.CanPrintEmptyDetails(dataControlBase))
          this.details.Add(new KeyValuePair<DataControlBase, bool>(dataControlBase, isGenerated));
      }
    }

    private int GetPrintDeailsLimit()
    {
      if (this.details == null || !this.TreeBuilder.GetPrintAllDetailsValue() || this.details == null)
        return 0;
      return this.details.Count - 1;
    }

    private bool IsMasterRowExpanded()
    {
      return MasterDetailPrintHelper.IsMasterRowExpanded(this.TreeBuilder, this.TreeBuilder.ReusingRowData.RowHandle.Value);
    }

    private bool CanPrintEmptyDetails(DataControlBase dataControl)
    {
      if (!this.TreeBuilder.GetAllowPrintEmptyDetailsValue())
        return false;
      return this.TreeBuilder.IsGridHeaderFooterVisible((TableView) ((GridControl) dataControl).View);
    }

    protected override IDataNode GetChildCore(int index)
    {
      if (index == 0 && this.printStub)
        return (IDataNode) new StubNode((DataTreeBuilder) this.TreeBuilder, (IDataNode) this, 0);
      this.UpdateReusingRowData();
      if (!this.TreeBuilder.GetPrintAllDetailsValue())
        return this.GetDetailPrintNode((TableView) this.detailDataControl.viewCore, PrintDetailType.Last, false);
      return this.GetDetailPrintNode((TableView) this.details[index].Key.DataView, index == this.details.Count - 1 ? PrintDetailType.Last : PrintDetailType.None, this.details[index].Value);
    }

    private IDataNode GetDetailPrintNode(TableView view, PrintDetailType printDetailType, bool isDescriptorGenerated)
    {
      double totalIndent = 0.0;
      this.TreeBuilder.View.DataControl.GetOriginationDataControl().EnumerateThisAndParentDataControls((Action<DataControlBase>) (dataControl => totalIndent += 20.0));
      totalIndent += (double) this.TreeBuilder.ReusingRowData.Level * 20.0;
      return GridPrintingHelper.CreatePrintingTreeNode((ITableView) view, new Size(this.pageSize.Width - totalIndent, this.pageSize.Height), MasterDetailPrintHelper.GetInheritedPrintInfo(view, this.TreeBuilder, printDetailType), (ItemsGenerationStrategyBase) null).GetChild(-1);
    }

    private DataControlDetailInfo GetDetailInfo(DataControlDetailDescriptor dcdd, RowDetailInfoBase detailInfoBase)
    {
      TabsDetailInfo tabsDetailInfo = (TabsDetailInfo) detailInfoBase;
      return dcdd.CreateRowDetailInfo(tabsDetailInfo.container) as DataControlDetailInfo;
    }
  }
}
