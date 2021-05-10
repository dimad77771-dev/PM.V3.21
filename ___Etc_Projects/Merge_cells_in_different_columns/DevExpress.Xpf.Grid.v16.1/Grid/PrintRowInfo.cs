// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintRowInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class PrintRowInfo : PrintRowInfoBase
  {
    private Thickness printTopRowIndentMargin;
    private double printTopRowWidth;
    private bool isPrintTopDetailRowVisible;
    private bool isPrintBottomDetailIndentVisible;
    private bool showRowBreak;
    private bool showIndentRowBreak;
    private bool isPrintBottomLastDetailIndentVisible;
    private Style printRowDataBottomIndentControlStyle;
    private Style printRowDataBottomLastIndentControlStyle;
    private bool isPrintDetailTopIndentVisible;
    private bool printDetailBottomIndentThickness;
    private Thickness printDataTopIndentBorderThickness;
    private double detailTopIndent;
    private double detailBottomIndent;
    private int detailLevel;

    public Thickness PrintTopRowIndentMargin
    {
      get
      {
        return this.printTopRowIndentMargin;
      }
      internal set
      {
        if (this.printTopRowIndentMargin == value)
          return;
        this.printTopRowIndentMargin = value;
        this.OnPropertyChanged("PrintTopRowIndentMargin");
      }
    }

    public double PrintTopRowWidth
    {
      get
      {
        return this.printTopRowWidth;
      }
      internal set
      {
        if (this.printTopRowWidth == value)
          return;
        this.printTopRowWidth = value;
        this.OnPropertyChanged("PrintTopRowWidth");
      }
    }

    public bool IsPrintTopDetailRowVisible
    {
      get
      {
        return this.isPrintTopDetailRowVisible;
      }
      internal set
      {
        if (this.isPrintTopDetailRowVisible == value)
          return;
        this.isPrintTopDetailRowVisible = value;
        this.OnPropertyChanged("IsPrintTopDetailRowVisible");
      }
    }

    public bool IsPrintBottomDetailIndentVisible
    {
      get
      {
        return this.isPrintBottomDetailIndentVisible;
      }
      internal set
      {
        if (this.isPrintBottomDetailIndentVisible == value)
          return;
        this.isPrintBottomDetailIndentVisible = value;
        this.OnPropertyChanged("IsPrintBottomDetailIndentVisible");
      }
    }

    public bool ShowRowBreak
    {
      get
      {
        return this.showRowBreak;
      }
      set
      {
        if (this.showRowBreak == value)
          return;
        this.showRowBreak = value;
        this.OnPropertyChanged("ShowRowBreak");
      }
    }

    public bool ShowIndentRowBreak
    {
      get
      {
        return this.showIndentRowBreak;
      }
      set
      {
        if (this.showIndentRowBreak == value)
          return;
        this.showIndentRowBreak = value;
        this.OnPropertyChanged("ShowIndentRowBreak");
      }
    }

    public bool IsPrintBottomLastDetailIndentVisible
    {
      get
      {
        return this.isPrintBottomLastDetailIndentVisible;
      }
      internal set
      {
        if (this.isPrintBottomLastDetailIndentVisible == value)
          return;
        this.isPrintBottomLastDetailIndentVisible = value;
        this.OnPropertyChanged("IsPrintBottomLastDetailIndentVisible");
      }
    }

    public Style PrintRowDataBottomIndentControlStyle
    {
      get
      {
        return this.printRowDataBottomIndentControlStyle;
      }
      internal set
      {
        if (this.printRowDataBottomIndentControlStyle == value)
          return;
        this.printRowDataBottomIndentControlStyle = value;
        this.OnPropertyChanged("PrintRowDataBottomIndentControlStyle");
      }
    }

    public Style PrintRowDataBottomLastIndentControlStyle
    {
      get
      {
        return this.printRowDataBottomLastIndentControlStyle;
      }
      internal set
      {
        if (this.printRowDataBottomLastIndentControlStyle == value)
          return;
        this.printRowDataBottomLastIndentControlStyle = value;
        this.OnPropertyChanged("PrintRowDataBottomLastIndentControlStyle");
      }
    }

    public bool IsPrintDetailTopIndentVisible
    {
      get
      {
        return this.isPrintDetailTopIndentVisible;
      }
      internal set
      {
        if (this.isPrintDetailTopIndentVisible == value)
          return;
        this.isPrintDetailTopIndentVisible = value;
        this.OnPropertyChanged("IsPrintDetailTopIndentVisible");
      }
    }

    public bool IsPrintDetailBottomIndentVisible
    {
      get
      {
        return this.printDetailBottomIndentThickness;
      }
      internal set
      {
        if (this.printDetailBottomIndentThickness == value)
          return;
        this.printDetailBottomIndentThickness = value;
        this.OnPropertyChanged("IsPrintDetailBottomIndentVisible");
      }
    }

    public Thickness PrintDataTopIndentBorderThickness
    {
      get
      {
        return this.printDataTopIndentBorderThickness;
      }
      internal set
      {
        if (this.printDataTopIndentBorderThickness == value)
          return;
        this.printDataTopIndentBorderThickness = value;
        this.OnPropertyChanged("PrintDataTopIndentBorderThickness");
      }
    }

    public double DetailTopIndent
    {
      get
      {
        return this.detailTopIndent;
      }
      internal set
      {
        if (this.detailTopIndent == value)
          return;
        this.detailTopIndent = value;
        this.OnPropertyChanged("DetailTopIndent");
      }
    }

    public double DetailBottomIndent
    {
      get
      {
        return this.detailBottomIndent;
      }
      internal set
      {
        if (this.detailBottomIndent == value)
          return;
        this.detailBottomIndent = value;
        this.OnPropertyChanged("DetailBottomIndent");
      }
    }

    public int DetailLevel
    {
      get
      {
        return this.detailLevel;
      }
      internal set
      {
        if (this.detailLevel == value)
          return;
        this.detailLevel = value;
        this.OnPropertyChanged("DetailLevel");
      }
    }

    public BandsLayoutBase BandsLayout { get; internal set; }
  }
}
