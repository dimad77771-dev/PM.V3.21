// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GridPrintingDataTreeBuilder
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Xpf.Data;
using DevExpress.XtraPrinting.DataNodes;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DevExpress.Xpf.Grid.Printing
{
  public class GridPrintingDataTreeBuilder : GridPrintingDataTreeBuilderBase, ISupportMasterDetailPrinting
  {
    private Dictionary<DataControlDetailDescriptor, IDescriptorAndDataControlBase> cache = new Dictionary<DataControlDetailDescriptor, IDescriptorAndDataControlBase>();
    private int prevRowHandle = int.MinValue;
    private readonly bool AllowPartialGrouping;
    private bool IsPrevMaserRowExpanded;

    protected PrintRowInfo basePrintRowInfo
    {
      get
      {
        return (PrintRowInfo) base.basePrintRowInfo;
      }
    }

    internal DataTemplate PrintGroupFooterTemplate { get; private set; }

    public TableView View
    {
      get
      {
        return base.View as TableView;
      }
    }

    protected new GridDataProvider GridDataProvider
    {
      get
      {
        return this.View.GridDataProvider;
      }
    }

    public GridPrintingDataTreeBuilder(TableView view, double totalHeaderWidth, ItemsGenerationStrategyBase itemsGenerationStrategy, BandsLayoutBase bandsLayout, MasterDetailPrintInfo masterDetailPrintInfo = null)
      : base((GridViewBase) view, totalHeaderWidth, bandsLayout, itemsGenerationStrategy, masterDetailPrintInfo)
    {
      this.AllowPartialGrouping = view.AllowPartialGrouping;
      this.PrintGroupFooterTemplate = this.View.PrintGroupFooterTemplate;
    }

    protected override PrintRowInfoBase CreateBasePrintRowInfo()
    {
      PrintRowInfo printRowInfo = (PrintRowInfo) base.CreateBasePrintRowInfo();
      printRowInfo.DetailLevel = this.GetDetailLevel();
      printRowInfo.BandsLayout = this.BandsLayout;
      printRowInfo.IsPrintTopDetailRowVisible = false;
      printRowInfo.IsPrintBottomDetailIndentVisible = false;
      printRowInfo.IsPrintDetailTopIndentVisible = false;
      printRowInfo.IsPrintDetailBottomIndentVisible = false;
      printRowInfo.PrintDataTopIndentBorderThickness = FillControl.EmptyThickness;
      printRowInfo.IsPrintBottomLastDetailIndentVisible = false;
      printRowInfo.PrintTopRowIndentMargin = FillControl.EmptyThickness;
      printRowInfo.PrintTopRowWidth = this.TotalHeaderWidth;
      printRowInfo.IsPrintHeaderBottomIndentVisible = false;
      printRowInfo.IsPrintFooterBottomIndentVisible = false;
      printRowInfo.IsPrintFixedFooterBottomIndentVisible = false;
      printRowInfo.PrintColumnHeaderStyle = this.View.PrintColumnHeaderStyle;
      printRowInfo.IsPrintColumnHeadersVisible = this.View.PrintColumnHeaders && this.View.ShowColumnHeaders;
      printRowInfo.IsPrintBandHeadersVisible = this.View.DataControl.BandsLayoutCore != null && this.View.ShowBandsPanel && this.View.PrintBandHeaders;
      TableView tableView = this.View.Grid.GetMasterGrid() != null ? this.View.Grid.GetMasterGrid().View as TableView : (TableView) null;
      printRowInfo.DetailTopIndent = tableView != null ? tableView.PrintDetailTopIndent : this.View.PrintDetailTopIndent;
      printRowInfo.DetailBottomIndent = tableView != null ? tableView.PrintDetailBottomIndent : this.View.PrintDetailBottomIndent;
      return (PrintRowInfoBase) printRowInfo;
    }

    protected override PrintRowInfoBase CreateBasePrintRowInfoObject()
    {
      return (PrintRowInfoBase) new PrintRowInfo();
    }

    protected override MasterDetailPrintInfo GetDefaultMasterDetailPrintInfo()
    {
      return new MasterDetailPrintInfo(this.View.AllowPrintDetails, this.View.AllowPrintEmptyDetails, this.View.PrintAllDetails, (ISupportMasterDetailPrinting) this, PrintDetailType.None, 0);
    }

    public IDescriptorAndDataControlBase GetDescriptorAndGridControl(DataControlDetailDescriptor descriptor)
    {
      IDescriptorAndDataControlBase andDataControlBase;
      if (!this.cache.TryGetValue(descriptor, out andDataControlBase))
      {
        andDataControlBase = (IDescriptorAndDataControlBase) new DescriptorAndGridControl(descriptor);
        this.cache.Add(descriptor, andDataControlBase);
      }
      return andDataControlBase;
    }

    public bool IsGeneratedControl(DataControlBase grid)
    {
      foreach (IDescriptorAndDataControlBase andDataControlBase in this.cache.Values)
      {
        if (andDataControlBase.Grid == grid)
          return true;
      }
      return false;
    }

    protected override void SetHeadersPrintRowInfo(HeadersData headersData, PrintRowInfoBase printRowInfo)
    {
      GridPrintingHelper.SetPrintRowInfo((DependencyObject) headersData, (PrintRowInfo) printRowInfo);
    }

    protected override bool IsGeneratedControl(GridControl grid)
    {
      return this.MasterDetailPrintInfo.RootPrintingDataTreeBuilder.IsGeneratedControl((DataControlBase) grid);
    }

    internal override void UpdateRowData(RowData rowData)
    {
      double totalHeaderWidth = this.TotalHeaderWidth;
      if (rowData is GroupRowData)
        totalHeaderWidth -= 20.0 * (double) rowData.Level;
      Thickness thickness1 = new Thickness();
      RowNodePrintInfo printInfo = rowData.DataRowNode.PrintInfo;
      double borderThickness = this.GetBorderThickness();
      double left = (double) (this.GetDetailLevel() + this.MasterDetailPrintInfo.DetailGroupLevel) * 20.0;
      bool flag1 = this.IsMasterRowExpanded(rowData.RowHandle.Value);
      double num1;
      Thickness thickness2;
      if (printInfo.RowPosition == RowPosition.Bottom || printInfo.RowPosition == RowPosition.Single)
      {
        thickness1 = new Thickness(0.0, 0.0, 0.0, flag1 || printInfo.NextRowHandle == int.MinValue && !this.IsPrintFooters() ? 0.0 : borderThickness);
        int nextNodeLevel = printInfo.NextNodeLevel;
        num1 = 20.0 * (double) (rowData.Level - nextNodeLevel);
        thickness2 = new Thickness(left + (double) nextNodeLevel * 20.0, 0.0, 0.0, 0.0);
      }
      else
      {
        thickness1 = FillControl.EmptyThickness;
        num1 = 20.0 * (double) rowData.Level;
        thickness2 = new Thickness(left, 0.0, 0.0, 0.0);
      }
      bool flag2 = false;
      bool flag3 = false;
      if (!this.IsPrintFooters() || printInfo.RowPosition != RowPosition.Bottom && printInfo.RowPosition != RowPosition.Single)
      {
        if (this.GetAllowPrintDetailsValue())
        {
          if (flag1)
            flag2 = true;
          else if (printInfo.NextRowHandle != int.MinValue && !this.View.Grid.IsGroupRowHandle(printInfo.NextRowHandle) && (printInfo.RowPosition == RowPosition.Bottom || printInfo.RowPosition == RowPosition.Single))
            flag2 = this.GetDetailLevel() > 0 && this.MasterDetailPrintInfo.PrintDetailType == PrintDetailType.Last;
        }
        if (!flag2 && this.GetDetailLevel() > 0 && printInfo.NextRowHandle == int.MinValue)
          flag3 = !(rowData is GroupSummaryRowData) || !this.IsPrintFooters();
      }
      if (!flag2 && !flag3 && flag1)
        flag2 = true;
      Thickness thickness3 = FillControl.EmptyThickness;
      Thickness thickness4 = FillControl.EmptyThickness;
      double printTopRowWidth = this.basePrintRowInfo.PrintTopRowWidth;
      bool flag4 = false;
      if (this.GetAllowPrintDetailsValue() && this.IsPrevMaserRowExpanded)
      {
        flag4 = true;
        thickness3 = new Thickness(0.0, 0.0, 0.0, borderThickness);
        double num2 = 20.0 * (double) rowData.Level;
        thickness4 = new Thickness(left + num2, 0.0, 0.0, 0.0);
        printTopRowWidth -= num2;
      }
      if (this.prevRowHandle == int.MinValue || printInfo.PrevRowHandle == this.prevRowHandle)
      {
        this.prevRowHandle = rowData.RowHandle.Value;
        this.IsPrevMaserRowExpanded = flag1;
      }
      if (rowData is GroupSummaryRowData && printInfo.NextNodeLevel < rowData.Level)
        thickness1 = new Thickness(0.0, 0.0, 0.0, borderThickness);
      bool flag5 = false;
      bool flag6 = false;
      if (this.AllowPartialGrouping)
      {
        VisibleIndexCollection visibleIndexes = this.View.DataControl.DataProviderBase.DataController.GetVisibleIndexes();
        flag5 = visibleIndexes.IsSingleGroupRow(rowData.RowHandle.Value) || (this.View.Grid.IsGroupRowHandle(printInfo.NextRowHandle) || visibleIndexes.IsSingleGroupRow(printInfo.NextRowHandle));
        if (flag5 && printInfo.RowPosition != RowPosition.Bottom && (printInfo.RowPosition != RowPosition.Single && this.View.Grid.IsGroupRowHandle(printInfo.NextRowHandle)) && (printInfo.NextNodeLevel < rowData.Level && !flag1))
        {
          flag6 = true;
          thickness1 = new Thickness(0.0, 0.0, 0.0, borderThickness);
        }
      }
      PrintRowInfo printRowInfo = GridPrintingHelper.GetPrintRowInfo((DependencyObject) rowData);
      if (printRowInfo == null)
      {
        printRowInfo = new PrintRowInfo();
        GridPrintingHelper.SetPrintRowInfo((DependencyObject) rowData, printRowInfo);
      }
      this.ClonePrintRowInfoProperties((PrintRowInfoBase) printRowInfo, (PrintRowInfoBase) this.basePrintRowInfo);
      printRowInfo.IsPrintColumnHeadersVisible = this.basePrintRowInfo.IsPrintColumnHeadersVisible;
      printRowInfo.IsPrintBandHeadersVisible = this.basePrintRowInfo.IsPrintBandHeadersVisible;
      printRowInfo.PrintDataIndentBorderThickness = thickness1;
      printRowInfo.PrintDataIndentMargin = thickness2;
      printRowInfo.PrintDataIndent = num1;
      printRowInfo.TotalHeaderWidth = totalHeaderWidth;
      printRowInfo.ShowRowBreak = flag5;
      printRowInfo.ShowIndentRowBreak = flag6;
      printRowInfo.IsPrintBottomDetailIndentVisible = flag2;
      printRowInfo.PrintDataTopIndentBorderThickness = thickness3;
      printRowInfo.IsPrintTopDetailRowVisible = flag4;
      printRowInfo.IsPrintBottomLastDetailIndentVisible = flag3;
      printRowInfo.PrintTopRowIndentMargin = thickness4;
      printRowInfo.PrintTopRowWidth = printTopRowWidth;
      printRowInfo.DetailLevel = this.GetDetailLevel();
      printRowInfo.PrintRowDataBottomIndentControlStyle = this.basePrintRowInfo.PrintRowDataBottomIndentControlStyle;
      printRowInfo.PrintRowDataBottomLastIndentControlStyle = this.basePrintRowInfo.PrintRowDataBottomLastIndentControlStyle;
      printRowInfo.DetailTopIndent = this.basePrintRowInfo.DetailTopIndent;
      printRowInfo.DetailBottomIndent = this.basePrintRowInfo.DetailBottomIndent;
      printRowInfo.BandsLayout = this.basePrintRowInfo.BandsLayout;
    }

    private bool IsMasterRowExpanded(int rowHandle)
    {
      if (rowHandle == int.MinValue || rowHandle < 0 || !this.GetAllowPrintDetailsValue())
        return false;
      if (this.GetPrintAllDetailsValue() && this.View.Grid.DetailDescriptor != null)
        return this.IsFakeDetailExpanded(rowHandle);
      if (!MasterDetailPrintHelper.IsMasterRowExpanded(this, rowHandle))
        return false;
      DataControlBase detailDataControl = MasterDetailPrintHelper.FindDetailDataControl(this, rowHandle, MasterDetailPrintHelper.GetActiveDetailDescriptor(this, rowHandle, (GridControl) null, true));
      if (detailDataControl == null)
        return false;
      return this.IsMasterRowExpandedCore(detailDataControl, rowHandle);
    }

    private bool IsMasterRowExpandedCore(DataControlBase dataControl, int rowHandle)
    {
      if (MasterDetailPrintHelper.IsDetailContainsRows(this, rowHandle, dataControl))
        return true;
      if (!this.GetAllowPrintEmptyDetailsValue())
        return false;
      TableView view = dataControl.viewCore as TableView;
      if (view == null)
        return false;
      return this.IsGridHeaderFooterVisible(view);
    }

    private bool IsFakeDetailExpanded(int rowHandle)
    {
      List<IDescriptorAndDataControlBase> descriptors = new List<IDescriptorAndDataControlBase>();
      MasterDetailPrintHelper.GetAllDetailDescriptors((DataControlBase) this.View.Grid).ForEach((Action<DataControlDetailDescriptor>) (descr => descriptors.Add(this.MasterDetailPrintInfo.RootPrintingDataTreeBuilder.GetDescriptorAndGridControl(descr))));
      foreach (IDescriptorAndDataControlBase andDataControlBase in descriptors)
      {
        bool isGenerated = true;
        DataControlBase dataControl = MasterDetailPrintHelper.FindDetailDataControl(this, this.ReusingRowData.RowHandle.Value, (DataControlDetailDescriptor) andDataControlBase.Descriptor);
        if (dataControl == null || !dataControl.viewCore.PrintSelectedRowsOnly)
          dataControl = andDataControlBase.GetDetailGridControl((PrintingDataTreeBuilderBase) this, out isGenerated);
        if (this.IsMasterRowExpandedCore(dataControl, rowHandle))
          return true;
      }
      return false;
    }

    internal bool GetAllowPrintDetailsValue()
    {
      return this.MasterDetailPrintInfo.AllowPrintDetails.ToBoolean(true);
    }

    internal bool GetAllowPrintEmptyDetailsValue()
    {
      return this.MasterDetailPrintInfo.AllowPrintEmptyDetails.ToBoolean(false);
    }

    internal bool GetPrintAllDetailsValue()
    {
      return this.MasterDetailPrintInfo.PrintAllDetails.ToBoolean(false);
    }

    internal bool IsGridHeaderFooterVisible(TableView view)
    {
      if (!view.PrintColumnHeaders && !this.IsPrintFooter(view))
        return this.IsPrintFixedFooter(view);
      return true;
    }

    protected override int GetDetailLevel()
    {
      int level = -1;
      this.View.DataControl.EnumerateThisAndParentDataControls((Action<DataControlBase>) (dataControl => ++level));
      return level;
    }

    internal override void UpdateGroupRowData(RowData rowData)
    {
      GroupRowData groupRowData = (GroupRowData) rowData;
      if (groupRowData is GroupSummaryRowData)
        this.UpdatePrintGroupSummaryInfo(groupRowData);
      else
        TreeBuilderPrintingHelper.UpdatePrintGroupRowInfo((PrintRowInfoBase) GridPrintingHelper.GetPrintRowInfo((DependencyObject) rowData), groupRowData, this.View.GroupSummaryDisplayMode, this.View.PrintGroupSummaryDisplayMode, this.GetGroupRowText(groupRowData), this.GetDetailLevel());
    }

    private void UpdatePrintGroupSummaryInfo(GroupRowData groupRowData)
    {
      foreach (GridGroupSummaryColumnData summaryColumnData in (IEnumerable<GridGroupSummaryColumnData>) groupRowData.FixedNoneGroupSummaryData)
      {
        PrintGroupSummaryInfo groupSummaryInfo = new PrintGroupSummaryInfo(GridPrintingHelper.GetPrintCellInfo((DependencyObject) groupRowData.GetCellDataByColumn(summaryColumnData.Column)).PrintColumnWidth, summaryColumnData.Value.ToString(), this.View.PrintGroupFooterStyle, !summaryColumnData.Column.HasRightSibling);
        GridPrintingHelper.SetPrintGroupSummaryInfo((DependencyObject) summaryColumnData, groupSummaryInfo);
      }
    }

    protected virtual string GetGroupRowText(GroupRowData rowData)
    {
      return TreeBuilderPrintingHelper.GetGroupRowText(rowData, this.View.GroupSummaryDisplayMode, this.View.PrintGroupSummaryDisplayMode, "({0})", true);
    }

    public override IDataNode CreateDetailPrintingNode(NodeContainer container, RowNode rowNode, IDataNode node, int index)
    {
      return (IDataNode) new GridDetailPrintingNode(container, rowNode, (PrintingDataTreeBuilderBase) this, node, index);
    }

    public override IDataNode CreateGroupPrintingNode(NodeContainer container, RowNode groupNode, IDataNode node, int index, Size pageSize)
    {
      if (!(groupNode is GroupSummaryRowNode))
        return (IDataNode) new GridGroupPrintingNode(container, (GroupNode) groupNode, this, node, index, pageSize);
      return (IDataNode) new GridGroupSummaryPrintingNode(container, (GroupNode) groupNode, this, node, index, pageSize);
    }

    public override IDataNode CreateMasterDetailPrintingNode(NodeContainer container, RowNode rowNode, IDataNode node, int index, Size pageSize)
    {
      return (IDataNode) new GridMasterDetailPrintingNode(container, rowNode, this, node, index, pageSize);
    }

    protected override RowNodePrintInfo CreateRowNodePrintInfo(DataRowNode node, Dictionary<ColumnBase, int> mergeValueCounters)
    {
      RowNodePrintInfo rowNodePrintInfo = base.CreateRowNodePrintInfo(node, mergeValueCounters);
      if (this.View.ActualAllowCellMerge)
      {
        rowNodePrintInfo.MergeValues = new Dictionary<ColumnBase, int>();
        foreach (ColumnBase visibleColumn in (IEnumerable<ColumnBase>) this.GetVisibleColumns())
        {
          int? mergeValue = this.GetMergeValue(visibleColumn, node.RowHandle.Value, mergeValueCounters);
          if (mergeValue.HasValue)
            rowNodePrintInfo.MergeValues[visibleColumn] = mergeValue.Value;
        }
      }
      return rowNodePrintInfo;
    }

    private int? GetMergeValue(ColumnBase column, int rowHandle, Dictionary<ColumnBase, int> mergeValueCounters)
    {
      if (!this.View.IsMergedCell(rowHandle, column))
        return new int?();
      if (!this.View.IsPrevRowCellMerged(this.View.DataControl.GetRowVisibleIndexByHandleCore(rowHandle), column, false) && mergeValueCounters.ContainsKey(column))
      {
        Dictionary<ColumnBase, int> dictionary;
        ColumnBase index;
        (dictionary = mergeValueCounters)[index = column] = dictionary[index] + 1;
      }
      int num;
      if (mergeValueCounters.TryGetValue(column, out num))
        return new int?(num);
      mergeValueCounters[column] = 0;
      return new int?(0);
    }

    protected override bool GetPrintGroupFooters()
    {
      return this.View.PrintGroupFooters;
    }

    protected override void ProcessNode(DataRowNode node)
    {
    }
  }
}
