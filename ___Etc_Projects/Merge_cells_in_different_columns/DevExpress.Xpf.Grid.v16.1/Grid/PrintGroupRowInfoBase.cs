// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintGroupRowInfoBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;

namespace DevExpress.Xpf.Grid
{
  public abstract class PrintGroupRowInfoBase : PrintInfoBase
  {
    private List<PrintGroupRowCellInfo> groupCells;
    private PrintGroupRowCellInfo captionCell;
    private bool isExpanded;
    private bool isLast;

    public List<PrintGroupRowCellInfo> GroupCells
    {
      get
      {
        return this.groupCells;
      }
      internal set
      {
        if (this.groupCells == value)
          return;
        this.groupCells = value;
        this.OnPropertyChanged("GroupCells");
      }
    }

    public PrintGroupRowCellInfo CaptionCell
    {
      get
      {
        return this.captionCell;
      }
      internal set
      {
        if (this.captionCell == value)
          return;
        this.captionCell = value;
        this.OnPropertyChanged("CaptionCell");
      }
    }

    public bool IsExpanded
    {
      get
      {
        return this.isExpanded;
      }
      internal set
      {
        if (this.isExpanded == value)
          return;
        this.isExpanded = value;
        this.OnPropertyChanged("IsExpanded");
      }
    }

    public bool IsLast
    {
      get
      {
        return this.isLast;
      }
      internal set
      {
        if (this.isLast == value)
          return;
        this.isLast = value;
        this.OnPropertyChanged("IsLast");
      }
    }
  }
}
