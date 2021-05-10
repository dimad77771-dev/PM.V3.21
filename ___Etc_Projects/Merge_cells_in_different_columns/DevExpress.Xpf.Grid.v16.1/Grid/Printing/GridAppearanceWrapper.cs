// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GridAppearanceWrapper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Export.Xl;
using DevExpress.XtraExport.Helpers;

namespace DevExpress.Xpf.Grid.Printing
{
  internal class GridAppearanceWrapper : IBandedViewAppearance, IGridViewAppearance
  {
    XlCellFormatting IGridViewAppearance.AppearanceEvenRow
    {
      get
      {
        return new XlCellFormatting();
      }
    }

    XlCellFormatting IGridViewAppearance.AppearanceOddRow
    {
      get
      {
        return new XlCellFormatting();
      }
    }

    XlCellFormatting IGridViewAppearance.AppearanceGroupRow
    {
      get
      {
        return new XlCellFormatting();
      }
    }

    XlCellFormatting IGridViewAppearance.AppearanceFooter
    {
      get
      {
        return new XlCellFormatting();
      }
    }

    XlCellFormatting IGridViewAppearance.AppearanceGroupFooter
    {
      get
      {
        return new XlCellFormatting();
      }
    }

    XlCellFormatting IGridViewAppearance.AppearanceRow
    {
      get
      {
        return new XlCellFormatting();
      }
    }

    XlCellFormatting IGridViewAppearance.AppearanceHeader
    {
      get
      {
        return new XlCellFormatting();
      }
    }

    XlCellFormatting IBandedViewAppearance.BandPanel
    {
      get
      {
        return new XlCellFormatting();
      }
    }

    XlCellFormatting IBandedViewAppearance.BandPanelBackground
    {
      get
      {
        return new XlCellFormatting();
      }
    }

    XlCellFormatting IBandedViewAppearance.HeaderPanelBackground
    {
      get
      {
        return new XlCellFormatting();
      }
    }
  }
}
