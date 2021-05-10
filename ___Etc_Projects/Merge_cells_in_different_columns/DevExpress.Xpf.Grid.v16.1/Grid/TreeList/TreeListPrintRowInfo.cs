// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListPrintRowInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Printing;
using System.Windows;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListPrintRowInfo : PrintRowInfo
  {
    protected readonly object ExpandedButtonKey = new object();
    protected readonly object CollapsedButtonKey = new object();
    private PrintRowState rowState;
    private ImageSource image;
    private Thickness printImageIndentBorderThickness;
    private double printImageIndent;
    private Thickness printButtonIndentBorderThickness;
    private double printButtonIndent;
    private TargetType printButtonTargetType;
    private object printButtonKey;

    public PrintRowState RowState
    {
      get
      {
        return this.rowState;
      }
      internal set
      {
        this.PrintButtonTargetType = this.GetPrintButtonTargetType(value);
        this.PrintButtonKey = this.GetPrintButtonKey(value);
        if (this.rowState == value)
          return;
        this.rowState = value;
        this.OnPropertyChanged("RowState");
      }
    }

    public ImageSource Image
    {
      get
      {
        return this.image;
      }
      internal set
      {
        if (this.image == value)
          return;
        this.image = value;
        this.OnPropertyChanged("Image");
      }
    }

    public Thickness PrintImageIndentBorderThickness
    {
      get
      {
        return this.printImageIndentBorderThickness;
      }
      internal set
      {
        if (this.printImageIndentBorderThickness == value)
          return;
        this.printImageIndentBorderThickness = value;
        this.OnPropertyChanged("PrintImageIndentBorderThickness");
      }
    }

    public double PrintImageIndent
    {
      get
      {
        return this.printImageIndent;
      }
      internal set
      {
        if (this.printImageIndent == value)
          return;
        this.printImageIndent = value;
        this.OnPropertyChanged("PrintImageIndent");
      }
    }

    public Thickness PrintButtonIndentBorderThickness
    {
      get
      {
        return this.printButtonIndentBorderThickness;
      }
      internal set
      {
        if (this.printButtonIndentBorderThickness == value)
          return;
        this.printButtonIndentBorderThickness = value;
        this.OnPropertyChanged("PrintButtonIndentBorderThickness");
      }
    }

    public double PrintButtonIndent
    {
      get
      {
        return this.printButtonIndent;
      }
      internal set
      {
        if (this.printButtonIndent == value)
          return;
        this.printButtonIndent = value;
        this.OnPropertyChanged("PrintButtonIndent");
      }
    }

    public TargetType PrintButtonTargetType
    {
      get
      {
        return this.printButtonTargetType;
      }
      private set
      {
        if (this.printButtonTargetType == value)
          return;
        this.printButtonTargetType = value;
        this.OnPropertyChanged("PrintButtonTargetType");
      }
    }

    public object PrintButtonKey
    {
      get
      {
        return this.printButtonKey;
      }
      private set
      {
        if (this.printButtonKey == value)
          return;
        this.printButtonKey = value;
        this.OnPropertyChanged("PrintButtonKey");
      }
    }

    protected override bool GetIsPrintTopRowVisible(bool isVisible)
    {
      return false;
    }

    private TargetType GetPrintButtonTargetType(PrintRowState newRowState)
    {
      return newRowState != PrintRowState.Default ? TargetType.Image : TargetType.Text;
    }

    private object GetPrintButtonKey(PrintRowState newRowState)
    {
      if (newRowState == PrintRowState.Default)
        return (object) null;
      if (newRowState != PrintRowState.Expanded)
        return this.CollapsedButtonKey;
      return this.ExpandedButtonKey;
    }
  }
}
