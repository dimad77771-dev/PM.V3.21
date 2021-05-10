// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.BandWrapper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Export.Xl;
using DevExpress.XtraExport.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Grid.Printing
{
  public class BandWrapper : BandedColumnWrapper, IGridBand, IBandedGridColumn, IColumn
  {
    private BandBase band;
    private List<IBandedGridColumn> bandsAndColumns;
    private List<IBandedGridColumn> columns;
    private XlCellFormatting formatingCellCore;

    public override bool AutoFillDown
    {
      get
      {
        return true;
      }
    }

    public int VisibleChildrenBandsCount
    {
      get
      {
        return this.band.VisibleBands.Count;
      }
    }

    public override string Name
    {
      get
      {
        return this.band.GetHashCode().ToString();
      }
    }

    public override XlCellFormatting AppearanceHeader
    {
      get
      {
        return this.Appearance;
      }
    }

    public override int ColIndex
    {
      get
      {
        return this.band.index;
      }
    }

    public override int ColVIndex
    {
      get
      {
        return this.band.index;
      }
    }

    public override int GroupIndex
    {
      get
      {
        return this.band.Level;
      }
    }

    public override bool IsFixedLeft
    {
      get
      {
        return this.band.Fixed == FixedStyle.Left;
      }
    }

    public override bool IsGroupColumn
    {
      get
      {
        return true;
      }
    }

    public override bool IsVisible
    {
      get
      {
        return this.band.Visible;
      }
    }

    public override string FieldName
    {
      get
      {
        return this.band.HeaderCaption.ToString();
      }
    }

    public override int RowCount
    {
      get
      {
        return 1;
      }
    }

    public override int RowIndex
    {
      get
      {
        return this.band.Level;
      }
    }

    public override int VisibleIndex
    {
      get
      {
        return this.band.VisibleIndex;
      }
    }

    public override FormatSettings FormatSettings
    {
      get
      {
        return new FormatSettings();
      }
    }

    public override string Header
    {
      get
      {
        return this.band.HeaderCaption.ToString();
      }
    }

    public override XlCellFormatting Appearance
    {
      get
      {
        if (this.formatingCellCore == null)
        {
          XlCellFormatting xlCellFormatting = new XlCellFormatting();
          xlCellFormatting.Font = new XlFont();
          xlCellFormatting.Alignment = ColumnWrapper.GetReportAlignment(this.band.HorizontalHeaderContentAlignment);
          xlCellFormatting.Fill = (XlFill) null;
          this.formatingCellCore = xlCellFormatting;
        }
        return this.formatingCellCore;
      }
    }

    public override int Width
    {
      get
      {
        if (double.IsInfinity(this.band.ActualHeaderWidth) || double.IsNaN(this.band.ActualHeaderWidth))
          return 0;
        return Convert.ToInt32(this.band.ActualHeaderWidth);
      }
    }

    public BandWrapper(BandBase band, int logicalPosition)
      : base((ColumnBase) null, logicalPosition)
    {
      this.band = band;
      this.CreatePrintableColumnList();
    }

    protected virtual void CreatePrintableColumnList()
    {
      this.bandsAndColumns = new List<IBandedGridColumn>();
      this.columns = new List<IBandedGridColumn>();
      if (this.band.PrintableBands.Count<BandBase>() > 0)
      {
        foreach (BandBase printableBand in this.band.PrintableBands)
          this.bandsAndColumns.Add((IBandedGridColumn) new BandWrapper(printableBand, printableBand.VisibleIndex));
      }
      else
      {
        foreach (BandRow actualRow in this.band.ActualRows)
        {
          foreach (ColumnBase column in actualRow.Columns)
          {
            if (column.AllowPrinting)
            {
              BandedColumnWrapper bandedColumnWrapper = new BandedColumnWrapper(column, column.VisibleIndex);
              this.bandsAndColumns.Add((IBandedGridColumn) bandedColumnWrapper);
              this.columns.Add((IBandedGridColumn) bandedColumnWrapper);
            }
          }
        }
      }
    }

    public IEnumerable<IBandedGridColumn> GetColumns()
    {
      return (IEnumerable<IBandedGridColumn>) this.columns;
    }

    public override IEnumerable<IColumn> GetAllColumns()
    {
      return (IEnumerable<IColumn>) this.bandsAndColumns;
    }

    public override int GetColumnGroupLevel()
    {
      return this.band.Level;
    }

    public override string GetGroupColumnHeader()
    {
      return this.Header;
    }

    public override int GetHashCode()
    {
      return this.band.GetHashCode();
    }
  }
}
