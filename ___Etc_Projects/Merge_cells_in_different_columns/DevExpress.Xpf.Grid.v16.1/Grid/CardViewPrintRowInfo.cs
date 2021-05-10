// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CardViewPrintRowInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class CardViewPrintRowInfo : PrintRowInfoBase
  {
    private double printCardWidth = double.NaN;
    private int printMaximumCardColumns = -1;
    private string totalSummaryText;
    private Style printTotalSummarySeparatorStyle;
    private List<PrintTotalSummaryItem> totalSummaries;
    private bool fixedTotalSummaryTopBorderVisible;
    private Thickness printCardMargin;
    private bool printAutoCardWidth;
    private bool isGroupBottomBorderVisible;
    private bool isPrevGroupRowCollapsed;
    private DataTemplate printCardTemplate;
    private DataTemplate printCardRowIndentTemplate;
    private DataTemplate printCardContentTemplate;
    private DataTemplate printCardHeaderTemplate;
    private double printCardsRowWidth;

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

    public Style PrintTotalSummarySeparatorStyle
    {
      get
      {
        return this.printTotalSummarySeparatorStyle;
      }
      set
      {
        if (this.printTotalSummarySeparatorStyle == value)
          return;
        this.printTotalSummarySeparatorStyle = value;
        this.OnPropertyChanged("PrintTotalSummarySeparatorStyle");
      }
    }

    public List<PrintTotalSummaryItem> TotalSummaries
    {
      get
      {
        return this.totalSummaries;
      }
      set
      {
        if (this.totalSummaries == value)
          return;
        this.totalSummaries = value;
        this.OnPropertyChanged("TotalSummaries");
      }
    }

    public bool FixedTotalSummaryTopBorderVisible
    {
      get
      {
        return this.fixedTotalSummaryTopBorderVisible;
      }
      set
      {
        if (this.fixedTotalSummaryTopBorderVisible == value)
          return;
        this.fixedTotalSummaryTopBorderVisible = value;
        this.OnPropertyChanged("FixedTotalSummaryTopBorderVisible");
      }
    }

    public Thickness PrintCardMargin
    {
      get
      {
        return this.printCardMargin;
      }
      set
      {
        if (this.printCardMargin == value)
          return;
        this.printCardMargin = value;
        this.OnPropertyChanged("PrintCardMargin");
      }
    }

    public double PrintCardWidth
    {
      get
      {
        return this.printCardWidth;
      }
      set
      {
        if (this.printCardWidth == value)
          return;
        this.printCardWidth = value;
        this.OnPropertyChanged("PrintCardWidth");
      }
    }

    public int PrintMaximumCardColumns
    {
      get
      {
        return this.printMaximumCardColumns;
      }
      set
      {
        if (this.printMaximumCardColumns == value)
          return;
        this.printMaximumCardColumns = value;
        this.OnPropertyChanged("PrintMaximumCardColumns");
      }
    }

    public bool PrintAutoCardWidth
    {
      get
      {
        return this.printAutoCardWidth;
      }
      set
      {
        if (this.printAutoCardWidth == value)
          return;
        this.printAutoCardWidth = value;
        this.OnPropertyChanged("PrintAutoCardWidth");
      }
    }

    public bool IsGroupBottomBorderVisible
    {
      get
      {
        return this.isGroupBottomBorderVisible;
      }
      set
      {
        if (this.isGroupBottomBorderVisible == value)
          return;
        this.isGroupBottomBorderVisible = value;
        this.OnPropertyChanged("IsGroupBottomBorderVisible");
      }
    }

    public bool IsPrevGroupRowCollapsed
    {
      get
      {
        return this.isPrevGroupRowCollapsed;
      }
      set
      {
        if (this.isPrevGroupRowCollapsed == value)
          return;
        this.isPrevGroupRowCollapsed = value;
        this.OnPropertyChanged("IsPrevGroupRowCollapsed");
      }
    }

    public DataTemplate PrintCardTemplate
    {
      get
      {
        return this.printCardTemplate;
      }
      internal set
      {
        if (this.printCardTemplate == value)
          return;
        this.printCardTemplate = value;
        this.OnPropertyChanged("PrintCardTemplate");
      }
    }

    public DataTemplate PrintCardRowIndentTemplate
    {
      get
      {
        return this.printCardRowIndentTemplate;
      }
      internal set
      {
        if (this.printCardRowIndentTemplate == value)
          return;
        this.printCardRowIndentTemplate = value;
        this.OnPropertyChanged("PrintCardRowIndentTemplate");
      }
    }

    public DataTemplate PrintCardContentTemplate
    {
      get
      {
        return this.printCardContentTemplate;
      }
      internal set
      {
        if (this.printCardContentTemplate == value)
          return;
        this.printCardContentTemplate = value;
        this.OnPropertyChanged("PrintCardContentTemplate");
      }
    }

    public DataTemplate PrintCardHeaderTemplate
    {
      get
      {
        return this.printCardHeaderTemplate;
      }
      internal set
      {
        if (this.printCardHeaderTemplate == value)
          return;
        this.printCardHeaderTemplate = value;
        this.OnPropertyChanged("PrintCardHeaderTemplate");
      }
    }

    public double PrintCardsRowWidth
    {
      get
      {
        return this.printCardsRowWidth;
      }
      set
      {
        if (this.printCardsRowWidth == value)
          return;
        this.printCardsRowWidth = value;
        this.OnPropertyChanged("PrintCardsRowWidth");
      }
    }
  }
}
