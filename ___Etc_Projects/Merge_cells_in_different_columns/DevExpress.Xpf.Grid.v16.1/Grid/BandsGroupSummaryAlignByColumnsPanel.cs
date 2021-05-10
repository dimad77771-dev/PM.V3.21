// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.BandsGroupSummaryAlignByColumnsPanel
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class BandsGroupSummaryAlignByColumnsPanel : BandsNoneDropPanel
  {
    public static readonly DependencyProperty FixedProperty = DependencyProperty.Register("Fixed", typeof (FixedStyle), typeof (BandsGroupSummaryAlignByColumnsPanel), (PropertyMetadata) new FrameworkPropertyMetadata((object) FixedStyle.None, FrameworkPropertyMetadataOptions.AffectsMeasure));
    public static readonly DependencyProperty LeftMarginProperty = DependencyProperty.Register("LeftMargin", typeof (double), typeof (BandsGroupSummaryAlignByColumnsPanel), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
    private double leftIndent;

    private GroupRowData GroupRowData
    {
      get
      {
        return this.DataContext as GroupRowData;
      }
    }

    public FixedStyle Fixed
    {
      get
      {
        return (FixedStyle) this.GetValue(BandsGroupSummaryAlignByColumnsPanel.FixedProperty);
      }
      set
      {
        this.SetValue(BandsGroupSummaryAlignByColumnsPanel.FixedProperty, (object) value);
      }
    }

    public double LeftMargin
    {
      get
      {
        return (double) this.GetValue(BandsGroupSummaryAlignByColumnsPanel.LeftMarginProperty);
      }
      set
      {
        this.SetValue(BandsGroupSummaryAlignByColumnsPanel.LeftMarginProperty, (object) value);
      }
    }

    private TableView View
    {
      get
      {
        return ((RowDataBase) this.DataContext).View as TableView;
      }
    }

    protected override void ArrangeChild(UIElement child, double x, double y, double width, double height)
    {
      bool hasOverlap = this.HasOverlap(x);
      base.ArrangeChild(child, this.CalcX(x, hasOverlap), y, this.CalcWidth(width, x, hasOverlap), height);
    }

    protected override void MeasureChild(UIElement child, double availableWidth, double availableHeight, double x)
    {
      base.MeasureChild(child, this.CalcWidth(availableWidth, x, this.HasOverlap(x)), availableHeight, x);
    }

    private bool HasOverlap(double x)
    {
      return x < this.leftIndent;
    }

    private double CalcX(double x, bool hasOverlap)
    {
      if (!hasOverlap)
        return x;
      return this.leftIndent;
    }

    private double CalcWidth(double width, double x, bool hasOverlap)
    {
      if (!hasOverlap)
        return width;
      return Math.Max(0.0, width - this.leftIndent + x);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
      this.UpdateLeftIndent();
      return base.MeasureOverride(availableSize);
    }

    private void UpdateLeftIndent()
    {
      if (!(this.DataContext is GroupRowData))
        return;
      double num = 0.0;
      if (this.Fixed == FixedStyle.None || this.Fixed == FixedStyle.Right)
      {
        if (this.View.FixedLeftVisibleColumns != null && this.View.FixedLeftVisibleColumns.Count > 0)
          num += this.View.FixedLeftContentWidth + this.View.TotalGroupAreaIndent + this.View.FixedLineWidth;
        if (this.Fixed == FixedStyle.None)
          num += this.View.ScrollingHeaderVirtualizationMargin.Left;
        else
          num += this.View.FixedNoneContentWidth + this.View.FixedLineWidth;
      }
      this.leftIndent = Math.Max(0.0, -this.LeftMargin - num);
    }

    protected internal override double GetBandWidth(BandBase band)
    {
      return this.GetActualColumnWidth((BaseColumn) band);
    }

    protected internal override double GetColumnWidth(ColumnBase column)
    {
      return this.GetActualColumnWidth((BaseColumn) column);
    }

    private double GetActualColumnWidth(BaseColumn column)
    {
      if (this.GroupRowData != null && column != null)
      {
        GridViewBase gridViewBase = column.View as GridViewBase;
        if (gridViewBase != null && gridViewBase.IsGroupRowOptimized && (gridViewBase.ViewBehavior != null && column.ActualHeaderWidth > 0.0) && gridViewBase.ViewBehavior.IsFirstColumn(column))
          return Math.Max(0.0, column.ActualHeaderWidth - this.GroupRowData.Offset);
      }
      return column.ActualHeaderWidth;
    }
  }
}
