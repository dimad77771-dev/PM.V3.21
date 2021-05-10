// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GridPrintingDataTreeBuilderBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DevExpress.Xpf.Grid.Printing
{
  public abstract class GridPrintingDataTreeBuilderBase : PrintingDataTreeBuilder
  {
    private readonly IList<DevExpress.Xpf.Grid.SummaryItemBase> groupSummaries;
    internal GroupRowData reusingGroupRowData;
    private Dictionary<DevExpress.Xpf.Grid.SummaryItemBase, object> totalSummary;
    private Dictionary<int, int> OriginalRowHandles;
    private ItemsGenerationStrategyBase itemsGenerationStrategy;

    internal DataTemplate PrintGroupRowTemplate { get; private set; }

    public GridViewBase View
    {
      get
      {
        return base.View as GridViewBase;
      }
    }

    protected GridDataProvider GridDataProvider
    {
      get
      {
        return (GridDataProvider) this.View.DataProviderBase;
      }
    }

    internal ItemsGenerationStrategyBase ItemsGenerationStrategy
    {
      get
      {
        if (this.itemsGenerationStrategy == null)
          this.itemsGenerationStrategy = this.CreateItemsGenerationStrategy();
        return this.itemsGenerationStrategy;
      }
    }

    public GridPrintingDataTreeBuilderBase(GridViewBase view, double pageWidth, BandsLayoutBase bandsLayout, ItemsGenerationStrategyBase itemsGenerationStrategy, MasterDetailPrintInfo masterDetailPrintInfo = null)
      : base((DataViewBase) view, pageWidth, bandsLayout, masterDetailPrintInfo)
    {
      this.itemsGenerationStrategy = itemsGenerationStrategy;
      this.PrintGroupRowTemplate = this.View.PrintGroupRowTemplate;
      this.groupSummaries = TreeBuilderPrintingHelper.CloneGroupSummaries(this.View.Grid.GroupSummary);
      this.reusingGroupRowData = new GroupRowData((DataTreeBuilder) this);
    }

    private ItemsGenerationStrategyBase CreateItemsGenerationStrategy()
    {
      return this.View.CreateItemsGenerationStrategy();
    }

    public int GetOriginalRowHandle(int rowHandle)
    {
      if (!this.View.PrintSelectedRowsOnly || this.OriginalRowHandles == null)
        return rowHandle;
      int num = 0;
      if (this.OriginalRowHandles.TryGetValue(rowHandle, out num))
        return num;
      return rowHandle;
    }

    protected override PrintRowInfoBase GetHeaderFooterPrintInfo()
    {
      PrintRowInfoBase basePrintRowInfo = this.CreateBasePrintRowInfo();
      int detailLevel = this.GetDetailLevel();
      double footerLeftIndent = this.GetHeaderFooterLeftIndent();
      basePrintRowInfo.PrintDataIndentMargin = new Thickness(basePrintRowInfo.PrintDataIndentMargin.Left + footerLeftIndent, basePrintRowInfo.PrintDataIndentMargin.Top, basePrintRowInfo.PrintDataIndentMargin.Right, basePrintRowInfo.PrintDataIndentMargin.Bottom);
      basePrintRowInfo.IsPrintHeaderBottomIndentVisible = !this.IsPrintFooters() && detailLevel > 0 && this.View.Grid.VisibleRowCount == 0;
      if (detailLevel > 0)
      {
        basePrintRowInfo.IsPrintFooterBottomIndentVisible = this.IsPrintFooter() && !this.IsPrintFixedFooter();
        basePrintRowInfo.IsPrintFixedFooterBottomIndentVisible = this.IsPrintFixedFooter();
      }
      return basePrintRowInfo;
    }

    protected override double GetHeaderFooterLeftIndent()
    {
      return (double) (this.GetDetailLevel() + this.MasterDetailPrintInfo.DetailGroupLevel) * 20.0;
    }

    protected override PrintRowInfoBase CreateBasePrintRowInfo()
    {
      PrintRowInfoBase printRowInfoObject = this.CreateBasePrintRowInfoObject();
      printRowInfoObject.PrintGroupRowStyle = this.View.PrintGroupRowStyle;
      printRowInfoObject.PrintFixedFooterStyle = this.View.PrintFixedTotalSummaryStyle;
      printRowInfoObject.PrintRowIndentStyle = this.View.PrintRowIndentStyle;
      printRowInfoObject.PrintDataIndentBorderThickness = new Thickness(0.0, 0.0, 0.0, this.GetBorderThickness());
      printRowInfoObject.PrintDataIndentMargin = FillControl.EmptyThickness;
      printRowInfoObject.PrintDataIndent = 0.0;
      printRowInfoObject.TotalHeaderWidth = this.TotalHeaderWidth;
      printRowInfoObject.PrintCellStyle = this.View.PrintCellStyle;
      return printRowInfoObject;
    }

    protected abstract PrintRowInfoBase CreateBasePrintRowInfoObject();

    protected void ClonePrintRowInfoProperties(PrintRowInfoBase actualInfo, PrintRowInfoBase basePrintRowInfo)
    {
      actualInfo.PrintColumnHeaderStyle = basePrintRowInfo.PrintColumnHeaderStyle;
      actualInfo.PrintGroupRowStyle = basePrintRowInfo.PrintGroupRowStyle;
      actualInfo.PrintFixedFooterStyle = basePrintRowInfo.PrintFixedFooterStyle;
      actualInfo.PrintRowIndentStyle = basePrintRowInfo.PrintRowIndentStyle;
      actualInfo.PrintCellStyle = basePrintRowInfo.PrintCellStyle;
    }

    protected override bool IsPrintTotalSummary()
    {
      return this.View.PrintTotalSummary;
    }

    protected override bool IsPrintFixedTotalSummary()
    {
      return this.View.PrintFixedTotalSummary;
    }

    protected bool IsPrintFooter(TableView view)
    {
      if (view.ShouldPrintTotalSummary)
        return this.PrintFooterTemplate != null;
      return false;
    }

    protected bool IsPrintFixedFooter(TableView view)
    {
      if (view.ShouldPrintFixedTotalSummary)
        return this.PrintFixedFooterTemplate != null;
      return false;
    }

    internal override ColumnBase GetGroupColumnByNode(DataRowNode node)
    {
      return (ColumnBase) GridPrintingDataTreeBuilderBase.GetGroupRowNodePrintInfo(node).GroupColumn;
    }

    internal override object GetGroupValueByNode(DataRowNode node)
    {
      return GridPrintingDataTreeBuilderBase.GetGroupRowNodePrintInfo(node).GroupValue;
    }

    internal override string GetGroupRowDisplayTextByNode(DataRowNode node)
    {
      return GridPrintingDataTreeBuilderBase.GetGroupRowNodePrintInfo(node).GroupDisplayText;
    }

    internal override string GetGroupRowHeaderCaptionByNode(DataRowNode node)
    {
      return GridPrintingDataTreeBuilderBase.GetGroupRowNodePrintInfo(node).GroupColumnHeaderCaption.ToString();
    }

    internal static GroupRowNodePrintInfo GetGroupRowNodePrintInfo(DataRowNode node)
    {
      return (GroupRowNodePrintInfo) node.PrintInfo;
    }

    internal override IList<DevExpress.Xpf.Grid.SummaryItemBase> GetGroupSummaries()
    {
      return this.groupSummaries;
    }

    internal override object GetRowValue(RowData rowData)
    {
      return this.ItemsGenerationStrategy.GetRowValue(rowData);
    }

    internal override object GetCellValue(RowData rowData, string fieldName)
    {
      return this.ItemsGenerationStrategy.GetCellValue(rowData, fieldName);
    }

    internal override bool TryGetGroupSummaryValue(RowData rowData, DevExpress.Xpf.Grid.SummaryItemBase item, out object value)
    {
      return GridPrintingDataTreeBuilderBase.GetGroupRowNodePrintInfo(rowData.DataRowNode).GroupSummaryValues.TryGetValue(item, out value);
    }

    protected abstract bool IsGeneratedControl(GridControl grid);

    protected override string GetTotalSummaryText(ColumnBase column)
    {
      return this.ItemsGenerationStrategy.GetTotalSummaryText(column);
    }

    protected override string GetFixedLeftTotalSummaryText()
    {
      return this.ItemsGenerationStrategy.GetFixedTotalSummaryLeftText();
    }

    protected override string GetFixedRightTotalSummaryText()
    {
      return this.ItemsGenerationStrategy.GetFixedTotalSummaryRightText();
    }

    public override void GenerateAllItems()
    {
      GridControl grid = this.View.Grid;
      try
      {
        if (!this.IsGeneratedControl(grid))
          grid.LockUpdateLayout = true;
        this.ItemsGenerationStrategy.PrepareDataControllerAndPerformPrintingAction((Action) (() => this.GenerateAllItemsAndCalcPrintInfo()));
      }
      finally
      {
        if (!this.IsGeneratedControl(grid))
          grid.LockUpdateLayout = false;
      }
    }

    private void GenerateAllItemsAndCalcPrintInfo()
    {
      this.View.layoutUpdatedLocker.DoLockedAction((Action) (() =>
      {
        this.GridDataProvider.VisibleIndicesProvider.ShowGroupSummaryFooterInternal = this.GetPrintGroupFooters();
        this.GridDataProvider.VisibleIndicesProvider.InvalidateCache();
        try
        {
          this.RootNodeContainer.ReGenerateItemsCore(0, this.View.Grid.VisibleRowCount + (this.GetPrintGroupFooters() ? this.View.CalcGroupSummaryVisibleRowCount() : 0));
          this.UpdateTotalSummary((ColumnsRowDataBase) this.HeadersData);
          this.CalcNodesPrintInfo();
          this.StoreTotalSummary();
        }
        finally
        {
          this.GridDataProvider.VisibleIndicesProvider.InvalidateCache();
          this.GridDataProvider.VisibleIndicesProvider.ShowGroupSummaryFooterInternal = true;
        }
      }));
    }

    protected abstract bool GetPrintGroupFooters();

    internal override object GetWpfRow(RowData rowData, int listSourceRowIndex)
    {
      return this.GridDataProvider.GetWpfRowByListIndex(rowData.DataRowNode.PrintInfo.ListIndex);
    }

    private void CalcNodesPrintInfo()
    {
      this.OriginalRowHandles = this.ItemsGenerationStrategy.With<ItemsGenerationStrategyBase, ItemsGenerationServerStrategy>((Func<ItemsGenerationStrategyBase, ItemsGenerationServerStrategy>) (strategy => strategy as ItemsGenerationServerStrategy)).With<ItemsGenerationServerStrategy, PrintSelectedRowsInfo>((Func<ItemsGenerationServerStrategy, PrintSelectedRowsInfo>) (strategy => strategy.SelectedRowsInfo)).With<PrintSelectedRowsInfo, Dictionary<int, int>>((Func<PrintSelectedRowsInfo, Dictionary<int, int>>) (info => info.OriginalRowHandles));
      VirtualItemsEnumerator virtualItemsEnumerator = new VirtualItemsEnumerator((NodeContainer) this.RootNodeContainer);
      List<DataRowNode> dataRowNodeList = new List<DataRowNode>();
      this.UpdateContainerPrintInfo((DataNodeContainer) this.RootNodeContainer);
      DataRowNode dataRowNode1 = (DataRowNode) null;
      Dictionary<ColumnBase, int> mergeValueCounters = new Dictionary<ColumnBase, int>();
      while (virtualItemsEnumerator.MoveNext())
      {
        DataRowNode node = virtualItemsEnumerator.Current as DataRowNode;
        this.ProcessNode(node);
        GroupNode groupNode = node as GroupNode;
        node.PrintInfo = groupNode != null ? (RowNodePrintInfo) this.CreateGroupRowNodePrintInfo(node) : this.CreateRowNodePrintInfo(node, mergeValueCounters);
        node.PrintInfo.RowPosition = ((DataNodeContainer) virtualItemsEnumerator.CurrentContainer).GetRowPosition((RowNode) node);
        node.PrintInfo.ListIndex = this.GridDataProvider.GetListIndexByRowHandle(node.RowHandle.Value);
        node.PrintInfo.PrevRowHandle = dataRowNode1 == null ? int.MinValue : dataRowNode1.RowHandle.Value;
        node.PrintInfo.PrevRowPosition = dataRowNode1 == null ? RowPosition.Middle : dataRowNode1.PrintInfo.RowPosition;
        node.PrintInfo.IsSelected = this.View.IsRowSelected(node.RowHandle.Value);
        if (groupNode != null)
        {
          this.UpdateContainerPrintInfo(groupNode.NodesContainer);
          foreach (DataRowNode dataRowNode2 in dataRowNodeList)
            dataRowNode2.PrintInfo.NextNodeLevel = node.Level;
          dataRowNodeList.Clear();
        }
        dataRowNodeList.Add(node);
        if (dataRowNode1 != null)
          dataRowNode1.PrintInfo.NextRowHandle = node.RowHandle.Value;
        dataRowNode1 = node;
      }
      if (dataRowNode1 == null)
        return;
      dataRowNode1.PrintInfo.NextRowHandle = int.MinValue;
      dataRowNode1.PrintInfo.IsLast = true;
    }

    protected abstract void ProcessNode(DataRowNode node);

    private void UpdateContainerPrintInfo(DataNodeContainer container)
    {
      container.PrintInfo = new NodeContainerPrintInfo()
      {
        IsGroupRowsContainer = container.IsGroupRowsContainer
      };
    }

    protected virtual RowNodePrintInfo CreateRowNodePrintInfo(DataRowNode node, Dictionary<ColumnBase, int> mergeValueCounters)
    {
      return new RowNodePrintInfo();
    }

    private void StoreTotalSummary()
    {
      if (!this.View.ViewBehavior.GetServiceSummaries().Any<ServiceSummaryItem>())
        return;
      this.totalSummary = this.View.Grid.DataController.TotalSummary.Cast<SummaryItem>().ToDictionary<SummaryItem, DevExpress.Xpf.Grid.SummaryItemBase, object>((Func<SummaryItem, DevExpress.Xpf.Grid.SummaryItemBase>) (x => (DevExpress.Xpf.Grid.SummaryItemBase) x.Key), (Func<SummaryItem, object>) (x => x.SummaryValue));
    }

    internal override object GetServiceTotalSummaryValue(ServiceSummaryItem item)
    {
      object result = (object) null;
      this.totalSummary.Do<Dictionary<DevExpress.Xpf.Grid.SummaryItemBase, object>>((Action<Dictionary<DevExpress.Xpf.Grid.SummaryItemBase, object>>) (x => x.TryGetValue((DevExpress.Xpf.Grid.SummaryItemBase) item, out result)));
      return result;
    }

    private GroupRowNodePrintInfo CreateGroupRowNodePrintInfo(DataRowNode node)
    {
      GridColumn gridColumn = (GridColumn) this.View.GetColumnBySortLevel(node.Level);
      Dictionary<DevExpress.Xpf.Grid.SummaryItemBase, object> dictionary = new Dictionary<DevExpress.Xpf.Grid.SummaryItemBase, object>();
      for (int index = 0; index < this.View.Grid.GroupSummary.Count; ++index)
      {
        object obj = (object) null;
        if (this.GridDataProvider.TryGetGroupSummaryValue(node.RowHandle.Value, (DevExpress.Xpf.Grid.SummaryItemBase) this.View.Grid.GroupSummary[index], out obj))
          dictionary[this.groupSummaries[index]] = obj;
      }
      return new GroupRowNodePrintInfo() { GroupColumn = gridColumn, GroupColumnHeaderCaption = gridColumn.HeaderCaption, GroupValue = this.View.GetGroupDisplayValue(node.RowHandle.Value), GroupDisplayText = this.View.GetGroupRowDisplayText(node.RowHandle.Value), GroupSummaryValues = dictionary };
    }

    internal override void OnRootNodeDispose()
    {
      base.OnRootNodeDispose();
      this.ItemsGenerationStrategy.Clear();
    }
  }
}
