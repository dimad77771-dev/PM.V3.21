// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.BandsCellsPanel
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class BandsCellsPanel : BandsColumnHeadersPanel
  {
    public static readonly DependencyProperty FixedNoneContentWidthProperty = DependencyPropertyManager.Register("FixedNoneContentWidth", typeof (double), typeof (BandsCellsPanel), new PropertyMetadata((object) double.NaN, (PropertyChangedCallback) ((d, e) => ((BandsCellsPanel) d).OnFixedNoneContentWidthChanged())));

    public double FixedNoneContentWidth
    {
      get
      {
        return (double) this.GetValue(BandsCellsPanel.FixedNoneContentWidthProperty);
      }
      set
      {
        this.SetValue(BandsCellsPanel.FixedNoneContentWidthProperty, (object) value);
      }
    }

    internal RowData RowData
    {
      get
      {
        return this.DataContext as RowData;
      }
    }

    protected override bool AllowHeaderMerge
    {
      get
      {
        return false;
      }
    }

    protected internal override ColumnBase GetColumn(FrameworkElement element)
    {
      return ((IGridCellEditorOwner) element).AssociatedColumn;
    }

    protected internal override double GetColumnWidth(ColumnBase column)
    {
      return Math.Max(0.0, column.ActualDataWidth + this.RowData.GetRowIndent(column));
    }

    protected internal override double GetBandWidth(BandBase band)
    {
      if (!band.HasLeftSibling)
        return Math.Max(0.0, band.ActualHeaderWidth - ((ITableView) this.RowData.View).TableViewBehavior.ViewInfo.FirstColumnIndent + this.RowData.GetRowIndent(true));
      return band.ActualHeaderWidth;
    }

    private void OnFixedNoneContentWidthChanged()
    {
      this.InvalidateMeasure();
    }
  }
}
