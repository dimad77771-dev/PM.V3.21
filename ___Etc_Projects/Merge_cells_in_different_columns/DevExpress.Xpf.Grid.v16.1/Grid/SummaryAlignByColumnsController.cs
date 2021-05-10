// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.SummaryAlignByColumnsController
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  internal class SummaryAlignByColumnsController
  {
    private readonly SummaryAlignByColumnsController.FixedNoneSummaryControlUpdater fixedNoneUpdater;
    private readonly SummaryAlignByColumnsController.FixedSummaryControlUpdaterBase fixedLeftUpdater;
    private readonly SummaryAlignByColumnsController.FixedSummaryControlUpdaterBase fixedRightUpdater;

    internal System.Windows.Controls.Grid LayoutPanel { get; private set; }

    public SummaryAlignByColumnsController(DataControlBase ownerControl)
    {
      this.LayoutPanel = new System.Windows.Controls.Grid();
      this.LayoutPanel.HorizontalAlignment = HorizontalAlignment.Left;
      ColumnDefinitionCollection columnDefinitions = this.LayoutPanel.ColumnDefinitions;
      columnDefinitions.Add(new ColumnDefinition()
      {
        Width = GridLength.Auto
      });
      columnDefinitions.Add(new ColumnDefinition()
      {
        Width = GridLength.Auto
      });
      columnDefinitions.Add(new ColumnDefinition()
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      columnDefinitions.Add(new ColumnDefinition()
      {
        Width = GridLength.Auto
      });
      columnDefinitions.Add(new ColumnDefinition()
      {
        Width = GridLength.Auto
      });
      this.fixedNoneUpdater = new SummaryAlignByColumnsController.FixedNoneSummaryControlUpdater((Panel) this.LayoutPanel, ownerControl);
      this.fixedLeftUpdater = (SummaryAlignByColumnsController.FixedSummaryControlUpdaterBase) new SummaryAlignByColumnsController.FixedLeftSummaryControlUpdater((Panel) this.LayoutPanel, ownerControl);
      this.fixedRightUpdater = (SummaryAlignByColumnsController.FixedSummaryControlUpdaterBase) new SummaryAlignByColumnsController.FixedRightSummaryControlUpdater((Panel) this.LayoutPanel, ownerControl);
    }

    internal void UpdateData(GroupRowData data)
    {
      this.DoUpdateAction((Action<SummaryAlignByColumnsController.GroupSummaryControlUpdaterBase>) (u => u.UpdateData(data)));
    }

    internal void UpdateBestFitData(IList<GridGroupSummaryColumnData> columnData)
    {
      this.fixedNoneUpdater.UpdateData(columnData);
    }

    internal void SetScrollingMargin(Thickness scrollingMargin)
    {
      this.fixedNoneUpdater.SetScrollingMargin(scrollingMargin);
    }

    internal void UpdateGroupColumnSummaryItemTemplate(bool useDefaultTemplate)
    {
      this.DoUpdateAction((Action<SummaryAlignByColumnsController.GroupSummaryControlUpdaterBase>) (u => u.SetCanUseDefaultTemplate(useDefaultTemplate)));
    }

    internal void UpdatePanel()
    {
      this.DoUpdateAction((Action<SummaryAlignByColumnsController.GroupSummaryControlUpdaterBase>) (u => u.UpdatePanel()));
    }

    internal void UpdateBands(FixedStyle fixedStyle)
    {
      this.DoUpdateAction(fixedStyle, (Action<SummaryAlignByColumnsController.GroupSummaryControlUpdaterBase>) (u => u.UpdateBands()));
    }

    internal void SetLeftIndent(double leftIndent)
    {
      this.DoUpdateAction((Action<SummaryAlignByColumnsController.GroupSummaryControlUpdaterBase>) (u => u.SetLeftIndent(leftIndent)));
    }

    internal void UpdateFixedSeparatorWidth()
    {
      this.DoFixedSummaryUpdateAction((Action<SummaryAlignByColumnsController.FixedSummaryControlUpdaterBase>) (u => u.UpdateFixedSeparatorWidth()));
    }

    internal void UpdateFixedSeparatorShowVertialLines()
    {
      this.DoFixedSummaryUpdateAction((Action<SummaryAlignByColumnsController.FixedSummaryControlUpdaterBase>) (u => u.UpdateFixedSeparatorShowVertialLines()));
    }

    internal void UpdateFixedSeparatorVisibility()
    {
      this.UpdateFixedLeftSeparatorVisibility();
      this.UpdateFixedRightSeparatorVisibility();
    }

    internal void UpdateFixedLeftSeparatorVisibility()
    {
      this.fixedLeftUpdater.UpdateFixedSeparatorVisibility();
    }

    internal void UpdateFixedRightSeparatorVisibility()
    {
      this.fixedRightUpdater.UpdateFixedSeparatorVisibility();
    }

    internal void InvalidateFixedLeft()
    {
      this.fixedLeftUpdater.InvalidatePanel();
    }

    internal void UpdateFixedNoneContentWidth(double width)
    {
      this.fixedNoneUpdater.UpdateWidth(width);
    }

    private void DoUpdateAction(FixedStyle fixedStyle, Action<SummaryAlignByColumnsController.GroupSummaryControlUpdaterBase> updateAction)
    {
      SummaryAlignByColumnsController.GroupSummaryControlUpdaterBase controlUpdaterBase;
      switch (fixedStyle)
      {
        case FixedStyle.None:
          controlUpdaterBase = (SummaryAlignByColumnsController.GroupSummaryControlUpdaterBase) this.fixedNoneUpdater;
          break;
        case FixedStyle.Left:
          controlUpdaterBase = (SummaryAlignByColumnsController.GroupSummaryControlUpdaterBase) this.fixedLeftUpdater;
          break;
        case FixedStyle.Right:
          controlUpdaterBase = (SummaryAlignByColumnsController.GroupSummaryControlUpdaterBase) this.fixedRightUpdater;
          break;
        default:
          throw new InvalidOperationException();
      }
      updateAction(controlUpdaterBase);
    }

    private void DoUpdateAction(Action<SummaryAlignByColumnsController.GroupSummaryControlUpdaterBase> updateAction)
    {
      updateAction((SummaryAlignByColumnsController.GroupSummaryControlUpdaterBase) this.fixedNoneUpdater);
      updateAction((SummaryAlignByColumnsController.GroupSummaryControlUpdaterBase) this.fixedLeftUpdater);
      updateAction((SummaryAlignByColumnsController.GroupSummaryControlUpdaterBase) this.fixedRightUpdater);
    }

    private void DoFixedSummaryUpdateAction(Action<SummaryAlignByColumnsController.FixedSummaryControlUpdaterBase> updateAction)
    {
      updateAction(this.fixedLeftUpdater);
      updateAction(this.fixedRightUpdater);
    }

    internal abstract class GroupSummaryControlUpdaterBase
    {
      private readonly Panel layoutPanel;
      private readonly DataControlBase ownerControl;

      protected GroupRowAlignByColumnsSummaryControl SummaryControl { get; set; }

      protected ITableView View
      {
        get
        {
          return this.ownerControl.viewCore as ITableView;
        }
      }

      private BandsLayoutBase BandsLayout
      {
        get
        {
          return this.ownerControl.BandsLayoutCore;
        }
      }

      protected GroupSummaryControlUpdaterBase(Panel layoutPanel, DataControlBase ownerControl)
      {
        this.layoutPanel = layoutPanel;
        this.ownerControl = ownerControl;
      }

      public void UpdateData(GroupRowData data)
      {
        this.UpdateData(this.GetFixedColumnData(data));
      }

      public void UpdateData(IList<GridGroupSummaryColumnData> columnData)
      {
        if (columnData != null && columnData.Count > 0 && this.HasElements(this.ownerControl.viewCore as TableView))
        {
          if (this.SummaryControl == null)
          {
            this.CreateElements();
            this.UpdateBands();
            this.UpdatePanel();
          }
          if (this.SummaryControl == null)
            return;
          this.SummaryControl.ItemsSource = (IEnumerable) columnData;
        }
        else
          this.Clear();
      }

      protected virtual void Clear()
      {
        this.Remove((FrameworkElement) this.SummaryControl);
        this.SummaryControl = (GroupRowAlignByColumnsSummaryControl) null;
      }

      public void UpdateBands()
      {
        if (this.SummaryControl == null)
          return;
        IList<BandBase> bandBaseList = (IList<BandBase>) null;
        if (this.BandsLayout != null)
          bandBaseList = this.GetBands(this.BandsLayout);
        this.SummaryControl.Bands = bandBaseList;
      }

      public void UpdatePanel()
      {
        if (this.SummaryControl == null)
          return;
        this.SummaryControl.UpdatePanel(this.BandsLayout != null);
      }

      public void SetCanUseDefaultTemplate(bool canUse)
      {
        if (this.SummaryControl == null)
          return;
        this.SummaryControl.UseDefaultItemTemplate = canUse;
      }

      public void SetLeftIndent(double leftIndent)
      {
        if (this.SummaryControl == null)
          return;
        this.SummaryControl.LeftIndent = leftIndent;
      }

      protected void Add(FrameworkElement element)
      {
        if (element == null)
          return;
        this.layoutPanel.Children.Add((UIElement) element);
      }

      protected void Remove(FrameworkElement element)
      {
        if (element == null)
          return;
        this.layoutPanel.Children.Remove((UIElement) element);
      }

      protected abstract void CreateElements();

      protected abstract IList<BandBase> GetBands(BandsLayoutBase bandsLayout);

      protected GroupRowAlignByColumnsSummaryControl CreateSummaryItemsControl(int column, FixedStyle fixedStyle)
      {
        GroupRowAlignByColumnsSummaryControl columnsSummaryControl = new GroupRowAlignByColumnsSummaryControl(fixedStyle);
        this.Add((FrameworkElement) columnsSummaryControl);
        System.Windows.Controls.Grid.SetColumn((UIElement) columnsSummaryControl, column);
        return columnsSummaryControl;
      }

      protected abstract IList<GridGroupSummaryColumnData> GetFixedColumnData(GroupRowData data);

      protected virtual bool HasElements(TableView view)
      {
        return false;
      }
    }

    private class FixedNoneSummaryControlUpdater : SummaryAlignByColumnsController.GroupSummaryControlUpdaterBase
    {
      public FixedNoneSummaryControlUpdater(Panel layoutPanel, DataControlBase ownerControl)
        : base(layoutPanel, ownerControl)
      {
      }

      public void SetScrollingMargin(Thickness scrollingMargin)
      {
        if (this.SummaryControl == null)
          return;
        this.SummaryControl.ScrollingMargin = scrollingMargin;
      }

      protected override void CreateElements()
      {
        GroupRowAlignByColumnsSummaryControl summaryItemsControl = this.CreateSummaryItemsControl(2, FixedStyle.None);
        summaryItemsControl.ClipToBounds = true;
        this.SummaryControl = summaryItemsControl;
      }

      protected override IList<BandBase> GetBands(BandsLayoutBase bandsLayout)
      {
        return (IList<BandBase>) bandsLayout.FixedNoneVisibleBands;
      }

      protected override IList<GridGroupSummaryColumnData> GetFixedColumnData(GroupRowData data)
      {
        return data.FixedNoneGroupSummaryData;
      }

      protected override bool HasElements(TableView view)
      {
        return true;
      }

      public void UpdateWidth(double width)
      {
        if (this.SummaryControl == null)
          return;
        this.SummaryControl.Width = width;
      }
    }

    private abstract class FixedSummaryControlUpdaterBase : SummaryAlignByColumnsController.GroupSummaryControlUpdaterBase
    {
      protected internal GroupRowFixedLineSeparatorControl FixedLine { get; set; }

      protected FixedSummaryControlUpdaterBase(Panel layoutPanel, DataControlBase ownerControl)
        : base(layoutPanel, ownerControl)
      {
      }

      protected void CreateFixedElements(int summaryColumn, int separatorColumn, FixedStyle fixedStyle)
      {
        this.SummaryControl = this.CreateSummaryItemsControl(summaryColumn, fixedStyle);
        this.FixedLine = this.CreateFixedLine(separatorColumn);
        this.UpdateFixedSeparatorWidth();
        this.UpdateFixedSeparatorShowVertialLines();
        this.UpdateFixedSeparatorVisibility();
        this.Add((FrameworkElement) this.FixedLine);
      }

      private GroupRowFixedLineSeparatorControl CreateFixedLine(int column)
      {
        GroupRowFixedLineSeparatorControl separatorControl = new GroupRowFixedLineSeparatorControl(new Func<TableViewBehavior, IList<ColumnBase>>(this.GetFixedColumns), new Func<BandsLayoutBase, IList<BandBase>>(((SummaryAlignByColumnsController.GroupSummaryControlUpdaterBase) this).GetBands));
        System.Windows.Controls.Grid.SetColumn((UIElement) separatorControl, column);
        return separatorControl;
      }

      internal void UpdateFixedSeparatorWidth()
      {
        if (this.FixedLine == null)
          return;
        this.FixedLine.Width = this.View.FixedLineWidth;
      }

      internal void UpdateFixedSeparatorShowVertialLines()
      {
        if (this.FixedLine == null)
          return;
        this.FixedLine.ShowVerticalLines = this.View.ShowVerticalLines;
      }

      internal void UpdateFixedSeparatorVisibility()
      {
        if (this.FixedLine == null)
          return;
        this.FixedLine.UpdateVisibility(this.View.ViewBase.DataControl);
      }

      protected override void Clear()
      {
        base.Clear();
        this.Remove((FrameworkElement) this.FixedLine);
        this.FixedLine = (GroupRowFixedLineSeparatorControl) null;
      }

      protected abstract IList<ColumnBase> GetFixedColumns(TableViewBehavior viewBehavior);

      internal void InvalidatePanel()
      {
        if (this.SummaryControl == null)
          return;
        this.SummaryControl.InvalidateMeasure();
        if (this.SummaryControl.Panel == null)
          return;
        this.SummaryControl.Panel.InvalidateMeasure();
      }
    }

    private class FixedLeftSummaryControlUpdater : SummaryAlignByColumnsController.FixedSummaryControlUpdaterBase
    {
      public FixedLeftSummaryControlUpdater(Panel layoutPanel, DataControlBase ownerControl)
        : base(layoutPanel, ownerControl)
      {
      }

      protected override void CreateElements()
      {
        this.CreateFixedElements(0, 1, FixedStyle.Left);
      }

      protected override IList<BandBase> GetBands(BandsLayoutBase bandsLayout)
      {
        return (IList<BandBase>) bandsLayout.FixedLeftVisibleBands;
      }

      protected override IList<GridGroupSummaryColumnData> GetFixedColumnData(GroupRowData data)
      {
        return data.FixedLeftGroupSummaryData;
      }

      protected override IList<ColumnBase> GetFixedColumns(TableViewBehavior viewBehavior)
      {
        return viewBehavior.FixedLeftVisibleColumns;
      }

      protected override bool HasElements(TableView view)
      {
        if (view != null)
          return view.TableViewBehavior.HasFixedLeftElements;
        return false;
      }
    }

    private class FixedRightSummaryControlUpdater : SummaryAlignByColumnsController.FixedSummaryControlUpdaterBase
    {
      public FixedRightSummaryControlUpdater(Panel layoutPanel, DataControlBase ownerControl)
        : base(layoutPanel, ownerControl)
      {
      }

      protected override void CreateElements()
      {
        this.CreateFixedElements(4, 3, FixedStyle.Right);
      }

      protected override IList<BandBase> GetBands(BandsLayoutBase bandsLayout)
      {
        return (IList<BandBase>) bandsLayout.FixedRightVisibleBands;
      }

      protected override IList<GridGroupSummaryColumnData> GetFixedColumnData(GroupRowData data)
      {
        return data.FixedRightGroupSummaryData;
      }

      protected override IList<ColumnBase> GetFixedColumns(TableViewBehavior viewBehavior)
      {
        return viewBehavior.FixedRightVisibleColumns;
      }

      protected override bool HasElements(TableView view)
      {
        if (view != null)
          return view.TableViewBehavior.HasFixedRightElements;
        return false;
      }
    }
  }
}
