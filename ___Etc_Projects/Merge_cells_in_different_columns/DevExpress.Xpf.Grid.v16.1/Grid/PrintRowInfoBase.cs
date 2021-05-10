// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintRowInfoBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public abstract class PrintRowInfoBase : PrintInfoBase
  {
    private Style printGroupRowStyle;
    private Style printRowIndentStyle;
    private Thickness printDataIndentBorderThickness;
    private Thickness printDataIndentMargin;
    private double printDataIndent;
    private double totalHeaderWidth;
    private Style printCellStyle;
    private Style printFixedFooterStyle;
    private Style printColumnHeaderStyle;
    private bool isPrintColumnHeadersVisible;
    private bool isPrintTopRowVisible;
    private bool isPrintBandHeadersVisible;
    private bool isPrintHeaderBottomIndentVisible;
    private bool isPrintFooterBottomIndentVisible;
    private bool isPrintFixedFooterBottomIndentVisible;

    public Style PrintGroupRowStyle
    {
      get
      {
        return this.printGroupRowStyle;
      }
      internal set
      {
        if (this.printGroupRowStyle == value)
          return;
        this.printGroupRowStyle = value;
        this.OnPropertyChanged("PrintGroupRowStyle");
      }
    }

    public Style PrintRowIndentStyle
    {
      get
      {
        return this.printRowIndentStyle;
      }
      internal set
      {
        if (this.printRowIndentStyle == value)
          return;
        this.printRowIndentStyle = value;
        this.OnPropertyChanged("PrintRowIndentStyle");
      }
    }

    public Thickness PrintDataIndentBorderThickness
    {
      get
      {
        return this.printDataIndentBorderThickness;
      }
      internal set
      {
        if (this.printDataIndentBorderThickness == value)
          return;
        this.printDataIndentBorderThickness = value;
        this.OnPropertyChanged("PrintDataIndentBorderThickness");
      }
    }

    public Thickness PrintDataIndentMargin
    {
      get
      {
        return this.printDataIndentMargin;
      }
      internal set
      {
        if (this.printDataIndentMargin == value)
          return;
        this.printDataIndentMargin = value;
        this.OnPropertyChanged("PrintDataIndentMargin");
      }
    }

    public double PrintDataIndent
    {
      get
      {
        return this.printDataIndent;
      }
      internal set
      {
        if (this.printDataIndent == value)
          return;
        this.printDataIndent = value;
        this.OnPropertyChanged("PrintDataIndent");
      }
    }

    public double TotalHeaderWidth
    {
      get
      {
        return this.totalHeaderWidth;
      }
      internal set
      {
        if (this.totalHeaderWidth == value)
          return;
        this.totalHeaderWidth = value;
        this.OnPropertyChanged("TotalHeaderWidth");
      }
    }

    public Style PrintCellStyle
    {
      get
      {
        return this.printCellStyle;
      }
      internal set
      {
        if (this.printCellStyle == value)
          return;
        this.printCellStyle = value;
        this.OnPropertyChanged("PrintCellStyle");
      }
    }

    public Style PrintFixedFooterStyle
    {
      get
      {
        return this.printFixedFooterStyle;
      }
      internal set
      {
        if (this.printFixedFooterStyle == value)
          return;
        this.printFixedFooterStyle = value;
        this.OnPropertyChanged("PrintFixedFooterStyle");
      }
    }

    public Style PrintColumnHeaderStyle
    {
      get
      {
        return this.printColumnHeaderStyle;
      }
      internal set
      {
        if (this.printColumnHeaderStyle == value)
          return;
        this.printColumnHeaderStyle = value;
        this.OnPropertyChanged("PrintColumnHeaderStyle");
      }
    }

    public bool IsPrintColumnHeadersVisible
    {
      get
      {
        return this.isPrintColumnHeadersVisible;
      }
      set
      {
        this.IsPrintTopRowVisible = this.GetIsPrintTopRowVisible(!value);
        if (this.isPrintColumnHeadersVisible == value)
          return;
        this.isPrintColumnHeadersVisible = value;
        this.OnPropertyChanged("IsPrintColumnHeadersVisible");
      }
    }

    public bool IsPrintTopRowVisible
    {
      get
      {
        return this.isPrintTopRowVisible;
      }
      private set
      {
        if (this.isPrintTopRowVisible == value)
          return;
        this.isPrintTopRowVisible = value;
        this.OnPropertyChanged("IsPrintTopRowVisible");
      }
    }

    public bool IsPrintBandHeadersVisible
    {
      get
      {
        return this.isPrintBandHeadersVisible;
      }
      set
      {
        if (this.isPrintBandHeadersVisible == value)
          return;
        this.isPrintBandHeadersVisible = value;
        this.OnPropertyChanged("IsPrintBandHeadersVisible");
      }
    }

    public bool IsPrintHeaderBottomIndentVisible
    {
      get
      {
        return this.isPrintHeaderBottomIndentVisible;
      }
      internal set
      {
        if (this.isPrintHeaderBottomIndentVisible == value)
          return;
        this.isPrintHeaderBottomIndentVisible = value;
        this.OnPropertyChanged("IsPrintHeaderBottomIndentVisible");
      }
    }

    public bool IsPrintFooterBottomIndentVisible
    {
      get
      {
        return this.isPrintFooterBottomIndentVisible;
      }
      internal set
      {
        if (this.isPrintFooterBottomIndentVisible == value)
          return;
        this.isPrintFooterBottomIndentVisible = value;
        this.OnPropertyChanged("IsPrintFooterBottomIndentVisible");
      }
    }

    public bool IsPrintFixedFooterBottomIndentVisible
    {
      get
      {
        return this.isPrintFixedFooterBottomIndentVisible;
      }
      internal set
      {
        if (this.isPrintFixedFooterBottomIndentVisible == value)
          return;
        this.isPrintFixedFooterBottomIndentVisible = value;
        this.OnPropertyChanged("IsPrintFixedFooterBottomIndentVisible");
      }
    }

    protected virtual bool GetIsPrintTopRowVisible(bool isVisible)
    {
      return isVisible;
    }
  }
}
