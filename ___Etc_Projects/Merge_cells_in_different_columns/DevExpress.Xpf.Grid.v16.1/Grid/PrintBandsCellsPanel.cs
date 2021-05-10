// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintBandsCellsPanel
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class PrintBandsCellsPanel : BandsCellsPanel
  {
    public static readonly DependencyProperty LevelProperty = DependencyPropertyManager.Register("Level", typeof (int), typeof (PrintBandsCellsPanel), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0, (PropertyChangedCallback) ((d, e) => ((UIElement) d).InvalidateMeasure())));

    public int Level
    {
      get
      {
        return (int) this.GetValue(PrintBandsCellsPanel.LevelProperty);
      }
      set
      {
        this.SetValue(PrintBandsCellsPanel.LevelProperty, (object) value);
      }
    }

    internal new ColumnsRowDataBase RowData
    {
      get
      {
        return ((RowContent) this.DataContext).Content as ColumnsRowDataBase;
      }
    }

    protected internal override ColumnBase GetColumn(FrameworkElement element)
    {
      return ((GridColumnData) ((ContentPresenter) element).Content).Column;
    }

    protected internal override double GetColumnWidth(ColumnBase column)
    {
      return GridPrintingHelper.GetPrintCellInfo((DependencyObject) this.RowData.GetCellDataByColumn(column)).PrintColumnWidth;
    }

    protected internal override double GetBandWidth(BandBase band)
    {
      return GridPrintingHelper.GetPrintCellInfo((DependencyObject) band).PrintColumnWidth;
    }
  }
}
