// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintGroupRowCellInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using System.ComponentModel;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class PrintGroupRowCellInfo : DependencyObject, INotifyPropertyChanged, ISupportVisibleIndex
  {
    private int groupLevel;
    private double width;
    private PrintGroupCellPosition position;
    private bool hasLeftBorder;
    private bool hasRightBorder;
    private bool isLeftGroupSummaryValueEmpty;
    private bool isRightGroupSummaryValueEmpty;
    private int visibleIndex;
    private string text;
    private int detailLevel;
    private Style printGroupRowStyle;

    public int GroupLevel
    {
      get
      {
        return this.groupLevel;
      }
      internal set
      {
        if (this.groupLevel == value)
          return;
        this.groupLevel = value;
        this.OnPropertyChanged("GroupLevel");
      }
    }

    public double Width
    {
      get
      {
        return this.width;
      }
      internal set
      {
        if (this.width == value)
          return;
        this.width = value;
        this.OnPropertyChanged("Width");
      }
    }

    public PrintGroupCellPosition Position
    {
      get
      {
        return this.position;
      }
      internal set
      {
        if (this.position == value)
          return;
        this.position = value;
        this.OnPropertyChanged("Position");
      }
    }

    public bool HasLeftBorder
    {
      get
      {
        return this.hasLeftBorder;
      }
      set
      {
        if (this.hasLeftBorder == value)
          return;
        this.hasLeftBorder = value;
        this.OnPropertyChanged("HasLeftBorder");
      }
    }

    public bool HasRightBorder
    {
      get
      {
        return this.hasRightBorder;
      }
      set
      {
        if (this.hasRightBorder == value)
          return;
        this.hasRightBorder = value;
        this.OnPropertyChanged("HasRightBorder");
      }
    }

    public bool IsLeftGroupSummaryValueEmpty
    {
      get
      {
        return this.isLeftGroupSummaryValueEmpty;
      }
      set
      {
        if (this.isLeftGroupSummaryValueEmpty == value)
          return;
        this.isLeftGroupSummaryValueEmpty = value;
        this.OnPropertyChanged("IsLeftGroupSummaryValueEmpty");
      }
    }

    public bool IsRightGroupSummaryValueEmpty
    {
      get
      {
        return this.isRightGroupSummaryValueEmpty;
      }
      set
      {
        if (this.isRightGroupSummaryValueEmpty == value)
          return;
        this.isRightGroupSummaryValueEmpty = value;
        this.OnPropertyChanged("IsRightGroupSummaryValueEmpty");
      }
    }

    public int VisibleIndex
    {
      get
      {
        return this.visibleIndex;
      }
      internal set
      {
        if (this.visibleIndex == value)
          return;
        this.visibleIndex = value;
        this.OnPropertyChanged("VisibleIndex");
      }
    }

    public string Text
    {
      get
      {
        return this.text;
      }
      internal set
      {
        if (this.text == value)
          return;
        this.text = value;
        this.OnPropertyChanged("Text");
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

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
