// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintTotalSummaryItem
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class PrintTotalSummaryItem : PrintInfoBase
  {
    private string totalSummaryText;
    private Style printTotalSummaryStyle;

    public string TotalSummaryText
    {
      get
      {
        return this.totalSummaryText;
      }
      set
      {
        if (this.totalSummaryText == value)
          return;
        this.totalSummaryText = value;
        this.OnPropertyChanged("TotalSummaryText");
      }
    }

    public Style PrintTotalSummaryStyle
    {
      get
      {
        return this.printTotalSummaryStyle;
      }
      set
      {
        if (this.printTotalSummaryStyle == value)
          return;
        this.printTotalSummaryStyle = value;
        this.OnPropertyChanged("PrintTotalSummaryStyle");
      }
    }
  }
}
