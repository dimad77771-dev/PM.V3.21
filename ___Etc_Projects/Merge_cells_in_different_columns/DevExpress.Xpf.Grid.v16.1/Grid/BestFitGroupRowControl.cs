// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.BestFitGroupRowControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.GroupRowLayout;
using System.Collections.Generic;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class BestFitGroupRowControl : GroupRowControl
  {
    private readonly GridGroupSummaryColumnData SummaryData;
    private readonly bool IsFirstColumn;
    private readonly ColumnBase Column;
    private GroupRowColumnSummaryControl summaryController;
    private GroupBandSummaryControl summaryBandController;

    public BestFitGroupRowControl(GroupRowData rowData, GridGroupSummaryColumnData summaryData, bool isFirstColumn, ColumnBase column)
      : base(rowData)
    {
      this.SummaryData = summaryData;
      this.IsFirstColumn = isFirstColumn;
      this.ColumnSummaryPosition = new IndexDefinition(1, 0, 3);
      this.Column = column;
      if (rowData == null || this.View == null)
        return;
      this.UpdateGroupRowStyle();
    }

    protected override void UpdateDateSummaryAlignByColumns()
    {
      if (this.SummaryAlignByColumnsController == null || this.SummaryData == null)
        return;
      this.SummaryAlignByColumnsController.UpdateBestFitData((IList<GridGroupSummaryColumnData>) new List<GridGroupSummaryColumnData>()
      {
        this.SummaryData
      });
    }

    public override void OnApplyTemplate()
    {
      if (this.SummaryData == null && !this.IsFirstColumn)
        return;
      base.OnApplyTemplate();
    }

    protected override void CreateDefaultContent()
    {
      Group child1 = new Group();
      child1.Add(new Layer(), 0);
      Group child2 = new Group();
      child2.Add(new Layer(), 0);
      child2.Add(new Layer(), 1);
      Group child3 = new Group();
      child3.Add(new Layer(), 0);
      this.layoutPanel.Groups.Add(child1, 0);
      this.layoutPanel.Groups.Add(child2, 1);
      this.layoutPanel.Groups.Add(child3, 2);
      if (this.IsFirstColumn)
      {
        this.UpdateCheckBoxSelector();
        this.UpdateGroupValuePresenter();
        this.UpdateSummary();
        this.UpdateGroupExpandButton();
      }
      else
        this.UpdateSummary();
    }

    protected override void UpdateSummary()
    {
      if ((this.ViewTable != null ? (int) this.ViewTable.GroupSummaryDisplayMode : 0) != 1)
        return;
      this.UpdateGroupSummaryAlignByColumns();
    }

    private void UpdateGroupSummaryAlignByColumns()
    {
      if (this.Column.ParentBand == null)
      {
        if (this.summaryController == null)
        {
          GroupRowColumnSummaryControl columnSummaryControl = new GroupRowColumnSummaryControl();
          columnSummaryControl.DataContext = (object) this.SummaryData;
          columnSummaryControl.ColumnData = this.SummaryData;
          this.summaryController = columnSummaryControl;
          this.RemovePanelElement(this.ColumnSummaryPosition);
          this.AddPanelElement((FrameworkElement) this.summaryController, this.ColumnSummaryPosition);
        }
        else
        {
          this.summaryController.DataContext = (object) this.SummaryData;
          this.summaryController.ColumnData = this.SummaryData;
        }
      }
      else if (this.summaryBandController == null)
      {
        GroupBandSummaryControl bandSummaryControl = new GroupBandSummaryControl();
        bandSummaryControl.DataContext = (object) this.SummaryData;
        this.summaryBandController = bandSummaryControl;
        this.RemovePanelElement(this.ColumnSummaryPosition);
        this.AddPanelElement((FrameworkElement) this.summaryBandController, this.ColumnSummaryPosition);
      }
      else
        this.summaryBandController.DataContext = (object) this.SummaryData;
    }

    protected override void SetGroupRowStateClient(GroupRowData rowData)
    {
    }

    protected override void UpdateLayoutSummaryPanelClip(Size arrangeBounds)
    {
    }

    protected override void UpdateFixedNoneContentWidth()
    {
    }
  }
}
