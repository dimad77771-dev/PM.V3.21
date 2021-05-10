// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListPrintingDataTreeBuilder
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Grid.Printing;
using DevExpress.XtraPrinting.DataNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListPrintingDataTreeBuilder : PrintingDataTreeBuilder
  {
    protected Dictionary<ColumnBase, string> TotalSummaries = new Dictionary<ColumnBase, string>();
    protected string FixedLeftSummary = string.Empty;
    protected string FixedRightSummary = string.Empty;
    private Dictionary<SummaryItemBase, object> printServiceSummaryData;

    protected PrintRowInfo basePrintRowInfo
    {
      get
      {
        return (PrintRowInfo) base.basePrintRowInfo;
      }
    }

    public TreeListView View
    {
      get
      {
        return base.View as TreeListView;
      }
    }

    public override int VisibleCount
    {
      get
      {
        if (!this.PrintAllNodes)
          return base.VisibleCount;
        return this.View.TotalNodesCount;
      }
    }

    protected bool PrintAllNodes
    {
      get
      {
        return this.View.PrintAllNodes;
      }
    }

    protected bool PrintNodeImages
    {
      get
      {
        return this.View.PrintNodeImages;
      }
    }

    public TreeListPrintingDataTreeBuilder(TreeListView view, double totalHeaderWidth, BandsLayoutBase bandsLayout)
      : base((DataViewBase) view, totalHeaderWidth, bandsLayout, (MasterDetailPrintInfo) null)
    {
      this.RootNodeContainer.EnumerateOnlySelectedRows = view.PrintSelectedRowsOnly;
      this.UpdatePrintingTotalSummaries();
    }

    private void UpdatePrintingTotalSummaries()
    {
      if (this.View.PrintSelectedRowsOnly)
        this.UpdatePrintingTotalSummaries_PrintSelectedRows();
      else
        this.UpdatePrintingTotalSummaries_AllRows();
    }

    private void UpdatePrintingTotalSummaries_AllRows()
    {
      this.TotalSummaries = this.View.ColumnsCore.Cast<ColumnBase>().ToDictionary<ColumnBase, ColumnBase, string>((Func<ColumnBase, ColumnBase>) (c => c), (Func<ColumnBase, string>) (c => c.TotalSummaryText));
      this.FixedLeftSummary = this.View.GetFixedSummariesLeftString();
      this.FixedRightSummary = this.View.GetFixedSummariesRightString();
    }

    private void UpdatePrintingTotalSummaries_PrintSelectedRows()
    {
      Dictionary<TreeListNode, TreeListDataProvider.SummaryItem> summaryData = new Dictionary<TreeListNode, TreeListDataProvider.SummaryItem>();
      this.View.TreeListDataProvider.CalcSummaryCore((IEnumerable<SummaryItemBase>) this.View.TreeListDataProvider.TotalSummaryCore, summaryData, this.View.PrintSelectedRowsOnly, (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.View.TreeListDataProvider.RootNode));
      this.TotalSummaries = this.View.DataControl.ColumnsCore.Cast<ColumnBase>().ToDictionary<ColumnBase, ColumnBase, string>((Func<ColumnBase, ColumnBase>) (c => c), (Func<ColumnBase, string>) (c => this.GetTotalSummaryValue(c, summaryData)));
      IList<GridTotalSummaryData> totalSummaryDataList1 = (IList<GridTotalSummaryData>) new List<GridTotalSummaryData>();
      IList<GridTotalSummaryData> totalSummaryDataList2 = (IList<GridTotalSummaryData>) new List<GridTotalSummaryData>();
      FixedTotalSummaryHelper.GenerateTotalSummaries(this.View.FixedSummariesHelper.FixedSummariesLeftCore, this.View.ColumnsCore, (Func<SummaryItemBase, object>) (summaryItem => this.View.TreeListDataProvider.GetSummaryValueCore(this.View.TreeListDataProvider.RootNode, summaryItem, summaryData)), totalSummaryDataList1);
      FixedTotalSummaryHelper.GenerateTotalSummaries(this.View.FixedSummariesHelper.FixedSummariesRightCore, this.View.ColumnsCore, (Func<SummaryItemBase, object>) (summaryItem => this.View.TreeListDataProvider.GetSummaryValueCore(this.View.TreeListDataProvider.RootNode, summaryItem, summaryData)), totalSummaryDataList2);
      this.FixedLeftSummary = FixedTotalSummaryHelper.GetFixedSummariesString(totalSummaryDataList1);
      this.FixedRightSummary = FixedTotalSummaryHelper.GetFixedSummariesString(totalSummaryDataList2);
    }

    private string GetTotalSummaryValue(ColumnBase column, Dictionary<TreeListNode, TreeListDataProvider.SummaryItem> summaryData)
    {
      return column.GetTotalSummaryText((Func<SummaryItemBase, object>) (summaryItem => this.View.TreeListDataProvider.GetSummaryValueCore(this.View.TreeListDataProvider.RootNode, summaryItem, summaryData)));
    }

    protected override string GetTotalSummaryText(ColumnBase column)
    {
      if (!this.TotalSummaries.ContainsKey(column))
        return string.Empty;
      return this.TotalSummaries[column] ?? string.Empty;
    }

    protected override string GetFixedLeftTotalSummaryText()
    {
      return this.FixedLeftSummary;
    }

    protected override string GetFixedRightTotalSummaryText()
    {
      return this.FixedRightSummary;
    }

    protected override PrintRowInfoBase CreateBasePrintRowInfo()
    {
      TreeListPrintRowInfo listPrintRowInfo = new TreeListPrintRowInfo();
      listPrintRowInfo.PrintColumnHeaderStyle = this.View.PrintColumnHeaderStyle;
      listPrintRowInfo.PrintFixedFooterStyle = this.View.PrintFixedTotalSummaryStyle;
      listPrintRowInfo.PrintRowIndentStyle = this.View.PrintRowIndentStyle;
      listPrintRowInfo.PrintDataIndentBorderThickness = new Thickness(0.0, 0.0, 0.0, this.GetBorderThickness());
      listPrintRowInfo.PrintDataIndentMargin = FillControl.EmptyThickness;
      listPrintRowInfo.PrintDataIndent = 0.0;
      listPrintRowInfo.TotalHeaderWidth = this.TotalHeaderWidth;
      listPrintRowInfo.IsPrintColumnHeadersVisible = this.View.PrintColumnHeaders && this.View.ShowColumnHeaders;
      listPrintRowInfo.IsPrintBandHeadersVisible = this.View.DataControl.BandsLayoutCore != null && this.View.ShowBandsPanel && this.View.PrintBandHeaders;
      listPrintRowInfo.RowState = PrintRowState.Default;
      listPrintRowInfo.Image = (ImageSource) null;
      listPrintRowInfo.PrintImageIndentBorderThickness = new Thickness(0.0, 0.0, 0.0, this.GetBorderThickness());
      listPrintRowInfo.PrintImageIndent = 0.0;
      listPrintRowInfo.PrintButtonIndentBorderThickness = new Thickness(0.0, 0.0, 0.0, this.GetBorderThickness());
      listPrintRowInfo.PrintButtonIndent = 0.0;
      listPrintRowInfo.BandsLayout = this.BandsLayout;
      return (PrintRowInfoBase) listPrintRowInfo;
    }

    protected override double GetHeaderFooterLeftIndent()
    {
      return 0.0;
    }

    protected override void SetHeadersPrintRowInfo(HeadersData headersData, PrintRowInfoBase printRowInfo)
    {
      GridPrintingHelper.SetPrintRowInfo((DependencyObject) headersData, (PrintRowInfo) printRowInfo);
    }

    protected override RowData CreateReusingRowData()
    {
      return (RowData) new TreeListRowData((DataTreeBuilder) this);
    }

    protected override int GetActualRowLevel(ColumnsRowDataBase rowData)
    {
      if (rowData is HeadersData)
        return base.GetActualRowLevel(rowData);
      return rowData.Level + 1 + (this.PrintNodeImages ? 1 : 0);
    }

    private int GetActualNextNodeLevel(RowNodePrintInfo info)
    {
      return info.NextNodeLevel + 1 + (this.PrintNodeImages ? 1 : 0);
    }

    internal override void UpdateRowData(RowData rowData)
    {
      double num1 = this.TotalHeaderWidth - 20.0 * (double) this.GetActualRowLevel((ColumnsRowDataBase) rowData);
      RowNodePrintInfo printInfo = rowData.DataRowNode.PrintInfo;
      int actualRowLevel = this.GetActualRowLevel((ColumnsRowDataBase) rowData);
      int num2 = printInfo.RowPosition == RowPosition.Bottom ? 0 : this.GetActualNextNodeLevel(printInfo);
      Thickness thickness1;
      double num3;
      Thickness thickness2;
      double num4;
      Thickness thickness3;
      if (rowData.Level == 0 && (printInfo.RowPosition == RowPosition.Bottom || printInfo.RowPosition == RowPosition.Single) && this.IsPrintFooters())
      {
        thickness1 = new Thickness(0.0, 0.0, 0.0, this.GetBorderThickness());
        num3 = 20.0;
        thickness2 = FillControl.EmptyThickness;
        num4 = this.PrintNodeImages ? 20.0 : 0.0;
        thickness3 = new Thickness(0.0, 0.0, 0.0, this.GetBorderThickness());
      }
      else if (rowData.Level > printInfo.NextNodeLevel)
      {
        double num5 = (double) (actualRowLevel - num2);
        if (num5 > 1.0)
        {
          thickness1 = new Thickness(0.0, 0.0, 0.0, this.GetBorderThickness());
          num3 = 20.0 * (num5 - (this.PrintNodeImages ? 1.0 : 0.0));
          thickness2 = new Thickness((double) num2 * 20.0, 0.0, 0.0, 0.0);
        }
        else
        {
          thickness1 = this.PrintNodeImages ? FillControl.EmptyThickness : new Thickness(0.0, 0.0, 0.0, this.GetBorderThickness());
          num3 = 20.0 * num5;
          thickness2 = new Thickness((double) (num2 - (this.PrintNodeImages ? 1 : 0)) * 20.0, 0.0, 0.0, 0.0);
        }
        num4 = this.PrintNodeImages ? 20.0 : 0.0;
        thickness3 = new Thickness(0.0, 0.0, 0.0, this.GetBorderThickness());
      }
      else
      {
        num4 = this.PrintNodeImages ? 20.0 : 0.0;
        thickness3 = FillControl.EmptyThickness;
        thickness1 = FillControl.EmptyThickness;
        num3 = 20.0;
        thickness2 = new Thickness(20.0 * (double) rowData.Level, 0.0, 0.0, 0.0);
      }
      TreeListPrintRowInfo listPrintRowInfo = GridPrintingHelper.GetPrintRowInfo((DependencyObject) rowData) as TreeListPrintRowInfo;
      if (listPrintRowInfo == null)
      {
        listPrintRowInfo = new TreeListPrintRowInfo();
        GridPrintingHelper.SetPrintRowInfo((DependencyObject) rowData, (PrintRowInfo) listPrintRowInfo);
      }
      listPrintRowInfo.PrintColumnHeaderStyle = this.basePrintRowInfo.PrintColumnHeaderStyle;
      listPrintRowInfo.PrintFixedFooterStyle = this.basePrintRowInfo.PrintFixedFooterStyle;
      listPrintRowInfo.PrintRowIndentStyle = this.basePrintRowInfo.PrintRowIndentStyle;
      listPrintRowInfo.PrintDataIndentBorderThickness = new Thickness(0.0, 0.0, 0.0, this.GetBorderThickness());
      listPrintRowInfo.PrintDataIndentMargin = thickness2;
      listPrintRowInfo.TotalHeaderWidth = num1;
      listPrintRowInfo.IsPrintColumnHeadersVisible = this.basePrintRowInfo.IsPrintColumnHeadersVisible;
      listPrintRowInfo.IsPrintBandHeadersVisible = this.basePrintRowInfo.IsPrintBandHeadersVisible;
      listPrintRowInfo.RowState = this.GetRowState(rowData);
      listPrintRowInfo.Image = this.GetRowImage(rowData);
      listPrintRowInfo.PrintImageIndentBorderThickness = thickness3;
      listPrintRowInfo.PrintImageIndent = num4;
      listPrintRowInfo.PrintButtonIndentBorderThickness = thickness1;
      listPrintRowInfo.PrintButtonIndent = num3;
      listPrintRowInfo.BandsLayout = this.basePrintRowInfo.BandsLayout;
    }

    protected virtual PrintRowState GetRowState(RowData rowData)
    {
      if (!this.View.PrintExpandButtons)
        return PrintRowState.Default;
      TreeListNode nodeByRowHandle = this.View.GetNodeByRowHandle(rowData.RowHandle.Value);
      if (nodeByRowHandle == null || !nodeByRowHandle.HasVisibleChildren)
        return PrintRowState.Default;
      return this.PrintAllNodes || nodeByRowHandle.IsExpanded ? PrintRowState.Expanded : PrintRowState.Collapsed;
    }

    protected virtual ImageSource GetRowImage(RowData rowData)
    {
      if (!this.PrintNodeImages)
        return (ImageSource) null;
      return ((TreeListRowData) rowData).GetImageSource();
    }

    public override void GenerateAllItems()
    {
      this.RootNodeContainer.ReGenerateItemsCore(0, this.VisibleCount);
      this.RootNodeContainer.PrintInfo = new NodeContainerPrintInfo()
      {
        IsGroupRowsContainer = false
      };
      VirtualItemsEnumerator virtualItemsEnumerator = new VirtualItemsEnumerator((NodeContainer) this.RootNodeContainer);
      DataRowNode dataRowNode1 = (DataRowNode) null;
      while (virtualItemsEnumerator.MoveNext())
      {
        DataRowNode dataRowNode2 = virtualItemsEnumerator.Current as DataRowNode;
        dataRowNode2.PrintInfo = new RowNodePrintInfo();
        dataRowNode2.PrintInfo.RowPosition = ((DataNodeContainer) virtualItemsEnumerator.CurrentContainer).GetRowPosition((RowNode) dataRowNode2);
        if (dataRowNode1 != null)
          dataRowNode1.PrintInfo.NextNodeLevel = dataRowNode2.Level;
        dataRowNode1 = dataRowNode2;
      }
      if (this.View.PrintSelectedRowsOnly && dataRowNode1 != null)
        dataRowNode1.PrintInfo.RowPosition = RowPosition.Bottom;
      GridPrintingHelper.SetPrintFixedFooterTextLeft((DependencyObject) this.HeadersData, this.View.GetFixedSummariesLeftString());
      GridPrintingHelper.SetPrintFixedFooterTextRight((DependencyObject) this.HeadersData, this.View.GetFixedSummariesRightString());
      this.CreatePrintSerivceSummaryData();
      this.UpdateTotalSummary((ColumnsRowDataBase) this.HeadersData);
    }

    protected virtual void CreatePrintSerivceSummaryData()
    {
      if (!this.View.ViewBehavior.GetServiceSummaries().Any<ServiceSummaryItem>())
        return;
      TreeListDataProvider.SummaryItem rootSummaryItem = this.View.TreeListDataProvider.GetRootSummaryItem();
      if (rootSummaryItem == null)
        return;
      this.printServiceSummaryData = rootSummaryItem.Where<KeyValuePair<SummaryItemBase, TreeListSummaryValue>>((Func<KeyValuePair<SummaryItemBase, TreeListSummaryValue>, bool>) (x => x.Key is ServiceSummaryItem)).ToDictionary<KeyValuePair<SummaryItemBase, TreeListSummaryValue>, SummaryItemBase, object>((Func<KeyValuePair<SummaryItemBase, TreeListSummaryValue>, SummaryItemBase>) (x => x.Key), (Func<KeyValuePair<SummaryItemBase, TreeListSummaryValue>, object>) (x => x.Value.Value));
    }

    public override IDataNode CreateDetailPrintingNode(NodeContainer container, RowNode rowNode, IDataNode node, int index)
    {
      return (IDataNode) new GridDetailPrintingNode(container, rowNode, (PrintingDataTreeBuilderBase) this, node, index);
    }

    public override IDataNode CreateGroupPrintingNode(NodeContainer container, RowNode groupNode, IDataNode node, int index, Size pageSize)
    {
      return (IDataNode) null;
    }

    public override IDataNode CreateMasterDetailPrintingNode(NodeContainer container, RowNode rowNode, IDataNode parentNode, int index, Size pageSize)
    {
      throw new NotImplementedException();
    }

    internal override object GetWpfRow(RowData rowData, int listSourceRowIndex)
    {
      return this.View.TreeListDataProvider.GetWpfRow(rowData.RowHandle, -1);
    }

    internal override object GetRowValue(RowData rowData)
    {
      return this.View.TreeListDataProvider.GetRowValue(rowData.RowHandle.Value);
    }

    internal override object GetCellValue(RowData rowData, string fieldName)
    {
      return this.View.TreeListDataProvider.GetRowValue(rowData.RowHandle.Value, fieldName);
    }

    protected internal override int GetRowLevelByControllerRow(int rowHandle)
    {
      return this.View.TreeListDataProvider.GetRowLevelByControllerRowCore(rowHandle, !this.PrintAllNodes);
    }

    protected internal override int GetRowLevelByVisibleIndex(int visibleIndex)
    {
      if (this.PrintAllNodes && visibleIndex < this.VisibleCount)
        return this.GetRowLevelByControllerRow(visibleIndex);
      return base.GetRowLevelByVisibleIndex(visibleIndex);
    }

    protected internal override int GetRowHandleByVisibleIndexCore(int visibleIndex)
    {
      if (this.PrintAllNodes)
        return visibleIndex;
      return base.GetRowHandleByVisibleIndexCore(visibleIndex);
    }

    protected internal override int GetRowVisibleIndexByHandleCore(int rowHandle)
    {
      if (this.PrintAllNodes)
        return rowHandle;
      return base.GetRowVisibleIndexByHandleCore(rowHandle);
    }

    protected override bool IsPrintTotalSummary()
    {
      return this.View.PrintTotalSummary;
    }

    protected override bool IsPrintFixedTotalSummary()
    {
      return this.View.PrintFixedTotalSummary;
    }

    internal override object GetServiceTotalSummaryValue(ServiceSummaryItem item)
    {
      if (this.printServiceSummaryData == null)
        return (object) null;
      object obj = (object) null;
      this.printServiceSummaryData.TryGetValue((SummaryItemBase) item, out obj);
      return obj;
    }
  }
}
