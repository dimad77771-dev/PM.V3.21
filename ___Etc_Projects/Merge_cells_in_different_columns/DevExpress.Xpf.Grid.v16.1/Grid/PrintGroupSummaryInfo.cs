// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintGroupSummaryInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class PrintGroupSummaryInfo : PrintGroupSummaryInfoBase
  {
    private double printColumnWidth;

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

    public string GroupFooterText { get; private set; }

    public Style PrintGroupFooterStyle { get; private set; }

    public bool IsRight { get; private set; }

    public PrintGroupSummaryInfo(double printColumnWidth, string groupFooterText, Style printGroupFooterStyle, bool isRight)
    {
      this.PrintColumnWidth = printColumnWidth;
      this.GroupFooterText = groupFooterText;
      this.PrintGroupFooterStyle = printGroupFooterStyle;
      this.IsRight = isRight;
    }
  }
}
