// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.CardViewPrintingDataTreeBuilder
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.XtraPrinting.DataNodes;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DevExpress.Xpf.Grid.Printing
{
  public class CardViewPrintingDataTreeBuilder : GridPrintingDataTreeBuilderBase
  {
    private bool PrintAutoCardWidth;
    private int PrintMaximumCardColumns;
    private Thickness PrintCardMargin;
    public List<DataRowNode> AllNodes;

    public CardView View
    {
      get
      {
        return base.View as CardView;
      }
    }

    protected CardViewPrintRowInfo basePrintRowInfo
    {
      get
      {
        return (CardViewPrintRowInfo) base.basePrintRowInfo;
      }
    }

    public CardViewPrintingDataTreeBuilder(CardView view, double pageWidth, ItemsGenerationStrategyBase itemsGenerationStrategy)
      : base((GridViewBase) view, pageWidth, (BandsLayoutBase) null, itemsGenerationStrategy, (MasterDetailPrintInfo) null)
    {
      this.PrintAutoCardWidth = view.PrintAutoCardWidth;
      this.PrintMaximumCardColumns = view.PrintMaximumCardColumns;
      this.PrintCardMargin = view.PrintCardMargin;
    }

    internal override void UpdateRowData(RowData rowData)
    {
      double totalHeaderWidth = this.TotalHeaderWidth;
      bool flag1 = false;
      Thickness thickness = new Thickness();
      bool flag2 = true;
      if (rowData is GroupRowData)
      {
        totalHeaderWidth -= 20.0 * (double) rowData.Level;
        RowNodePrintInfo printInfo = rowData.DataRowNode.PrintInfo;
        if (this.View.DataControl.IsGroupRowHandleCore(printInfo.PrevRowHandle))
          flag1 = true;
        if (this.View.DataControl.IsGroupRowHandleCore(printInfo.NextRowHandle))
        {
          if (printInfo.NextNodeLevel <= rowData.Level)
            thickness = new Thickness(0.0, 0.0, 0.0, 0.0);
        }
        else if (printInfo.NextRowHandle == int.MinValue && this.IsPrintFooters())
        {
          thickness = new Thickness(0.0, 0.0, 0.0, 0.0);
          flag2 = false;
        }
      }
      double dataIndentWidth = 20.0 * (rowData is GroupRowData ? (double) rowData.Level : (double) Math.Max(0, rowData.Level - 1));
      CardViewPrintRowInfo viewPrintRowInfo = CardViewPrintingHelper.GetPrintCardInfo((DependencyObject) rowData);
      if (viewPrintRowInfo == null)
      {
        viewPrintRowInfo = new CardViewPrintRowInfo();
        CardViewPrintingHelper.SetPrintCardInfo((DependencyObject) rowData, viewPrintRowInfo);
      }
      this.ClonePrintRowInfoProperties((PrintRowInfoBase) viewPrintRowInfo, (PrintRowInfoBase) this.basePrintRowInfo);
      viewPrintRowInfo.TotalHeaderWidth = totalHeaderWidth;
      viewPrintRowInfo.IsPrevGroupRowCollapsed = flag1;
      viewPrintRowInfo.PrintCardsRowWidth = this.TotalHeaderWidth;
      viewPrintRowInfo.PrintCardTemplate = this.basePrintRowInfo.PrintCardTemplate;
      viewPrintRowInfo.PrintCardRowIndentTemplate = this.basePrintRowInfo.PrintCardRowIndentTemplate;
      viewPrintRowInfo.PrintCardContentTemplate = this.basePrintRowInfo.PrintCardContentTemplate;
      viewPrintRowInfo.PrintCardHeaderTemplate = this.basePrintRowInfo.PrintCardHeaderTemplate;
      viewPrintRowInfo.PrintCardMargin = this.basePrintRowInfo.PrintCardMargin;
      viewPrintRowInfo.PrintAutoCardWidth = this.basePrintRowInfo.PrintAutoCardWidth;
      viewPrintRowInfo.PrintMaximumCardColumns = this.basePrintRowInfo.PrintMaximumCardColumns;
      viewPrintRowInfo.PrintDataIndent = dataIndentWidth;
      viewPrintRowInfo.PrintCardWidth = this.GetPrintCardWidth(dataIndentWidth);
      viewPrintRowInfo.PrintDataIndentBorderThickness = thickness;
      viewPrintRowInfo.PrintTotalSummarySeparatorStyle = this.basePrintRowInfo.PrintTotalSummarySeparatorStyle;
      viewPrintRowInfo.IsGroupBottomBorderVisible = flag2;
    }

    protected override PrintRowInfoBase CreateBasePrintRowInfoObject()
    {
      return (PrintRowInfoBase) new CardViewPrintRowInfo();
    }

    protected override PrintRowInfoBase CreateBasePrintRowInfo()
    {
      CardViewPrintRowInfo actualInfo = (CardViewPrintRowInfo) base.CreateBasePrintRowInfo();
      actualInfo.PrintCardsRowWidth = this.TotalHeaderWidth;
      actualInfo.PrintCardTemplate = this.View.PrintCardTemplate;
      actualInfo.PrintCardRowIndentTemplate = this.View.PrintCardRowIndentTemplate;
      actualInfo.PrintCardContentTemplate = this.View.PrintCardContentTemplate;
      actualInfo.PrintCardHeaderTemplate = this.View.PrintCardHeaderTemplate;
      actualInfo.FixedTotalSummaryTopBorderVisible = !this.IsPrintFooter();
      actualInfo.PrintCardMargin = this.View.PrintCardMargin;
      actualInfo.PrintAutoCardWidth = this.View.PrintAutoCardWidth;
      actualInfo.PrintMaximumCardColumns = this.View.PrintMaximumCardColumns;
      actualInfo.PrintTotalSummarySeparatorStyle = this.View.PrintTotalSummarySeparatorStyle;
      actualInfo.TotalSummaries = this.GetTotalSummaries(actualInfo);
      return (PrintRowInfoBase) actualInfo;
    }

    private double GetPrintCardWidth(double dataIndentWidth)
    {
      if (!this.PrintAutoCardWidth)
        return double.NaN;
      double num = this.TotalHeaderWidth - dataIndentWidth;
      for (int index = 0; index < this.PrintMaximumCardColumns; ++index)
      {
        Thickness actualPrintCardMargin = CardViewPrintingHelper.GetActualPrintCardMargin(this.PrintCardMargin, index == 0);
        num -= actualPrintCardMargin.Left + actualPrintCardMargin.Right;
      }
      return Math.Ceiling(num / (double) this.PrintMaximumCardColumns) - 1.0;
    }

    private List<PrintTotalSummaryItem> GetTotalSummaries(CardViewPrintRowInfo actualInfo)
    {
      List<PrintTotalSummaryItem> totalSummaryItemList = new List<PrintTotalSummaryItem>();
      List<Tuple<Style, string>> tupleList = new List<Tuple<Style, string>>();
      foreach (GridColumn visibleColumn in (IEnumerable<GridColumn>) this.View.VisibleColumns)
      {
        string totalSummaryText = this.GetTotalSummaryText((ColumnBase) visibleColumn);
        string[] separator = new string[1]{ Environment.NewLine };
        int num = 1;
        foreach (string str in totalSummaryText.Split(separator, (StringSplitOptions) num))
        {
          if (!string.IsNullOrWhiteSpace(str))
            tupleList.Add(new Tuple<Style, string>(visibleColumn.ActualPrintTotalSummaryStyle, str));
        }
      }
      for (int index = 0; index < tupleList.Count; ++index)
      {
        if (totalSummaryItemList.Count > 0)
          totalSummaryItemList.Add(new PrintTotalSummaryItem()
          {
            TotalSummaryText = "  /",
            PrintTotalSummaryStyle = actualInfo.PrintTotalSummarySeparatorStyle
          });
        totalSummaryItemList.Add(new PrintTotalSummaryItem()
        {
          TotalSummaryText = (totalSummaryItemList.Count > 0 ? "  " : "") + tupleList[index].Item2,
          PrintTotalSummaryStyle = tupleList[index].Item1
        });
      }
      return totalSummaryItemList;
    }

    protected override void UpdateTotalSummary(ColumnsRowDataBase rowData)
    {
      CardViewPrintRowInfo printCardInfo = CardViewPrintingHelper.GetPrintCardInfo((DependencyObject) rowData);
      printCardInfo.TotalSummaries = this.GetTotalSummaries(printCardInfo);
    }

    protected override bool GetPrintGroupFooters()
    {
      return false;
    }

    protected override void ProcessNode(DataRowNode node)
    {
      if (this.AllNodes == null)
        this.AllNodes = new List<DataRowNode>();
      this.AllNodes.Add(node);
    }

    protected override double GetHeaderFooterLeftIndent()
    {
      return 0.0;
    }

    protected override void SetHeadersPrintRowInfo(HeadersData headersData, PrintRowInfoBase printRowInfo)
    {
      CardViewPrintingHelper.SetPrintCardInfo((DependencyObject) headersData, (CardViewPrintRowInfo) printRowInfo);
    }

    public override IDataNode CreateMasterDetailPrintingNode(NodeContainer container, RowNode rowNode, IDataNode parentNode, int index, Size pageSize)
    {
      throw new NotImplementedException();
    }

    public override IDataNode CreateDetailPrintingNode(NodeContainer container, RowNode rowNode, IDataNode parentNode, int index)
    {
      return (IDataNode) new GridCardViewPrintingNode(container, rowNode, this, parentNode, index);
    }

    public override IDataNode CreateGroupPrintingNode(NodeContainer container, RowNode groupNode, IDataNode parentNode, int index, Size pageSize)
    {
      return (IDataNode) new CardViewGroupPrintingNode(container, (GroupNode) groupNode, this, parentNode, index, pageSize);
    }

    protected override bool IsGeneratedControl(GridControl grid)
    {
      return false;
    }

    internal override void UpdateGroupRowData(RowData rowData)
    {
      GroupRowData groupRowData = (GroupRowData) rowData;
      if (groupRowData is GroupSummaryRowData)
        return;
      TreeBuilderPrintingHelper.UpdatePrintGroupRowInfo((PrintRowInfoBase) CardViewPrintingHelper.GetPrintCardInfo((DependencyObject) rowData), groupRowData, GroupSummaryDisplayMode.Default, GroupSummaryDisplayMode.Default, this.GetGroupRowText(groupRowData), 0);
    }

    protected virtual string GetGroupRowText(GroupRowData rowData)
    {
      return TreeBuilderPrintingHelper.GetGroupRowText(rowData, GroupSummaryDisplayMode.Default, GroupSummaryDisplayMode.Default, "{0}", false);
    }
  }
}
