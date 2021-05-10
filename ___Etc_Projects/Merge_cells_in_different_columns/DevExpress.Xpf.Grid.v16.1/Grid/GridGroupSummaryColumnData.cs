// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridGroupSummaryColumnData
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class GridGroupSummaryColumnData : GridColumnData
  {
    private double actualWidth = double.NaN;
    private double actualWidthFooter = double.NaN;
    private static readonly DependencyPropertyKey HasRightSiblingPropertyKey = DependencyPropertyManager.RegisterReadOnly("HasRightSibling", typeof (bool), typeof (GridGroupSummaryColumnData), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty HasRightSiblingProperty = GridGroupSummaryColumnData.HasRightSiblingPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey HasSummaryPropertyKey = DependencyPropertyManager.RegisterReadOnly("HasSummary", typeof (bool), typeof (GridGroupSummaryColumnData), new PropertyMetadata((PropertyChangedCallback) ((d, e) => ((GridGroupSummaryColumnData) d).OnHasSummaryChanged())));
    public static readonly DependencyProperty HasSummaryProperty = GridGroupSummaryColumnData.HasSummaryPropertyKey.DependencyProperty;
    private IGroupRowColumnSummaryClient columnSummaryClient;
    private InlineCollectionInfo summaryTextInfo;

    public bool HasRightSibling
    {
      get
      {
        return (bool) this.GetValue(GridGroupSummaryColumnData.HasRightSiblingProperty);
      }
      internal set
      {
        this.SetValue(GridGroupSummaryColumnData.HasRightSiblingPropertyKey, (object) value);
      }
    }

    public bool HasSummary
    {
      get
      {
        return (bool) this.GetValue(GridGroupSummaryColumnData.HasSummaryProperty);
      }
      protected internal set
      {
        this.SetValue(GridGroupSummaryColumnData.HasSummaryPropertyKey, (object) value);
      }
    }

    public GroupRowData GroupRowData
    {
      get
      {
        return (GroupRowData) this.RowDataBase;
      }
    }

    public double ActualWidth
    {
      get
      {
        return this.actualWidth;
      }
      set
      {
        if (value == this.actualWidth || this.IgnoreActualWidth)
          return;
        this.actualWidth = value;
        this.RaisePropertyChanged("ActualWidth");
      }
    }

    public double ActualWidthFooter
    {
      get
      {
        return this.actualWidthFooter;
      }
      set
      {
        if (value == this.actualWidthFooter)
          return;
        this.actualWidthFooter = value;
        this.RaisePropertyChanged("ActualWidthFooter");
      }
    }

    public InlineCollectionInfo SummaryTextInfo
    {
      get
      {
        return this.summaryTextInfo;
      }
      internal set
      {
        if (value == this.summaryTextInfo)
          return;
        this.summaryTextInfo = value;
        this.OnInlineInfoChanged();
      }
    }

    private bool IgnoreActualWidth { get; set; }

    public GridGroupSummaryColumnData(GroupRowData rowData)
      : base((ColumnsRowDataBase) rowData)
    {
    }

    internal void SetColumnSummaryClient(IGroupRowColumnSummaryClient client)
    {
      this.columnSummaryClient = client;
    }

    internal void UpdateSummaryClientFocusState()
    {
      if (this.columnSummaryClient == null)
        return;
      this.columnSummaryClient.UpdateFocusState();
    }

    internal void UpdateSummaryIsReady()
    {
      if (this.columnSummaryClient == null)
        return;
      this.columnSummaryClient.UpdateIsReady();
    }

    private void OnHasSummaryChanged()
    {
      if (this.columnSummaryClient != null)
        this.columnSummaryClient.UpdateHasSummary();
      this.OnActualHeaderWidthChange();
    }

    protected override void OnValueChanged(object oldValue)
    {
      base.OnValueChanged(oldValue);
      this.UpdateClientSummaryValue();
      this.OnActualHeaderWidthChange();
    }

    private void UpdateClientSummaryValue()
    {
      if (this.columnSummaryClient == null)
        return;
      this.columnSummaryClient.UpdateSummaryValue();
    }

    protected override void OnColumnChanged(ColumnBase newValue)
    {
      base.OnColumnChanged(newValue);
      this.OnActualHeaderWidthChange();
    }

    public void OnActualHeaderWidthChange()
    {
      if (this.Column == null)
        return;
      GridViewBase gridViewBase = this.View as GridViewBase;
      if (gridViewBase != null && gridViewBase.IsGroupRowOptimized)
      {
        double num = GridGroupSummaryColumnData.CalcWidth(this.Column, this.Column.ActualHeaderWidth, this.GroupRowData);
        this.ActualWidth = num;
        this.ActualWidthFooter = num;
      }
      else
      {
        this.ActualWidth = this.Column.ActualHeaderWidth;
        this.ActualWidthFooter = GridGroupSummaryColumnData.CalcWidth(this.Column, this.Column.ActualHeaderWidth, this.GroupRowData);
      }
    }

    private void OnInlineInfoChanged()
    {
      this.RaisePropertyChanged("SummaryTextInfo");
      string str = this.SummaryTextInfo != null ? this.SummaryTextInfo.TextSource : (string) null;
      if (object.Equals((object) str, this.Value))
        this.UpdateClientSummaryValue();
      else
        this.Value = (object) str;
    }

    private static double CalcWidth(ColumnBase column, double headerWidth, GroupRowData rowData)
    {
      if (column != null && headerWidth > 0.0 && rowData != null && (GroupSummaryLayoutCalculator.IsFirstColumn(column, (RowData) rowData) || GroupSummaryLayoutCalculator.IsFirstVisibleColumn(column, (RowData) rowData)))
        return Math.Max(0.0, headerWidth - rowData.Offset);
      return headerWidth;
    }

    internal static GridGroupSummaryColumnData CreateBestFitData(GroupRowData rowData)
    {
      return new GridGroupSummaryColumnData(rowData) { IgnoreActualWidth = true };
    }
  }
}
