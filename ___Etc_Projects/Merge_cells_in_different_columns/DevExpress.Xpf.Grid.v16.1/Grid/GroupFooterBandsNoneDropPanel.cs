// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupFooterBandsNoneDropPanel
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class GroupFooterBandsNoneDropPanel : BandsNoneDropPanel
  {
    public static readonly DependencyProperty LevelProperty = DependencyProperty.Register("Level", typeof (int), typeof (GroupFooterBandsNoneDropPanel), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public int Level
    {
      get
      {
        return (int) this.GetValue(GroupFooterBandsNoneDropPanel.LevelProperty);
      }
      set
      {
        this.SetValue(GroupFooterBandsNoneDropPanel.LevelProperty, (object) value);
      }
    }

    internal GroupSummaryRowData RowData
    {
      get
      {
        return this.DataContext as GroupSummaryRowData;
      }
    }

    protected internal override double GetColumnWidth(ColumnBase column)
    {
      double columnWidth = base.GetColumnWidth(column);
      if (this.RowData != null && GroupSummaryLayoutCalculator.IsFirstColumn(column, (DevExpress.Xpf.Grid.RowData) this.RowData))
        columnWidth -= this.RowData.Offset;
      return columnWidth;
    }
  }
}
