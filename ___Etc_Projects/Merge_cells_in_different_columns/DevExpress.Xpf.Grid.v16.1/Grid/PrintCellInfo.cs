// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintCellInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class PrintCellInfo : PrintInfoBase
  {
    private string totalSummaryText;
    private double printColumnWidth;

    public string TotalSummaryText
    {
      get
      {
        return this.totalSummaryText;
      }
      internal set
      {
        if (this.totalSummaryText == value)
          return;
        this.totalSummaryText = value;
        this.OnPropertyChanged("TotalSummaryText");
      }
    }

    public bool IsTotalSummaryLeftBorderVisible { get; set; }

    public bool IsLast { get; private set; }

    public Style PrintTotalSummaryStyle { get; private set; }

    public Style PrintColumnHeaderStyle { get; private set; }

    public Style PrintCellStyle { get; private set; }

    public HorizontalAlignment HorizontalHeaderContentAlignment { get; private set; }

    public double PrintColumnWidth
    {
      get
      {
        return this.printColumnWidth;
      }
      internal set
      {
        if (this.printColumnWidth == value)
          return;
        this.printColumnWidth = value;
        this.OnPropertyChanged("PrintColumnWidth");
      }
    }

    public object HeaderCaption { get; private set; }

    internal bool IsColumnHeaderVisible { get; private set; }

    public int DetailLevel { get; private set; }

    public bool HasTopElement { get; private set; }

    public bool IsRight { get; private set; }

    public PrintCellInfo(bool isLast, string totalSummaryText, Style printTotalSummaryStyle, double printColumnWidth, object headerCaption, Style printColumnHeaderStyle, Style printCellStyle, bool isColumnHeaderVisible, int detailLevel, HorizontalAlignment horizontalHeaderContentAlignment, bool hasTopElement, bool isRight)
    {
      this.IsLast = isLast;
      this.TotalSummaryText = totalSummaryText;
      this.PrintTotalSummaryStyle = printTotalSummaryStyle;
      this.PrintColumnHeaderStyle = printColumnHeaderStyle;
      this.PrintCellStyle = printCellStyle;
      this.PrintColumnWidth = printColumnWidth;
      this.HeaderCaption = headerCaption;
      this.IsColumnHeaderVisible = isColumnHeaderVisible;
      this.DetailLevel = detailLevel;
      this.HorizontalHeaderContentAlignment = horizontalHeaderContentAlignment;
      this.HasTopElement = hasTopElement;
      this.IsRight = isRight;
      this.IsTotalSummaryLeftBorderVisible = true;
    }
  }
}
