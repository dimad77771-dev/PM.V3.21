// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintLayoutAssigner
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class PrintLayoutAssigner : LayoutAssigner
  {
    public static readonly LayoutAssigner Printing = (LayoutAssigner) new PrintLayoutAssigner();

    public override bool UseDataAreaIndent
    {
      get
      {
        return false;
      }
    }

    public override bool UseFixedColumnIndents
    {
      get
      {
        return false;
      }
    }

    public override bool UseDetailButtonsIndents
    {
      get
      {
        return false;
      }
    }

    public override double GetWidth(BaseColumn column)
    {
      return GridPrintingHelper.GetPrintColumnWidth((DependencyObject) column);
    }

    public override void SetWidth(BaseColumn column, double value)
    {
      GridPrintingHelper.SetPrintColumnWidth((DependencyObject) column, value);
    }

    public override void CreateLayout(ColumnsLayoutCalculator calculator)
    {
    }

    public override ColumnPosition GetColumnPosition(BaseColumn column)
    {
      return GridPrintingHelper.GetPrintColumnPosition((DependencyObject) column);
    }

    public override void SetColumnPosition(BaseColumn column, ColumnPosition position)
    {
      GridPrintingHelper.SetPrintColumnPosition((DependencyObject) column, position);
    }

    public override bool GetHasLeftSibling(BaseColumn column)
    {
      return GridPrintingHelper.GetPrintHasLeftSibling((DependencyObject) column);
    }

    public override void SetHasLeftSibling(BaseColumn column, bool value)
    {
      GridPrintingHelper.SetPrintHasLeftSibling((DependencyObject) column, value);
    }

    public override bool GetHasRightSibling(BaseColumn column)
    {
      return GridPrintingHelper.GetPrintHasRightSibling((DependencyObject) column);
    }

    public override void SetHasRightSibling(BaseColumn column, bool value)
    {
      GridPrintingHelper.SetPrintHasRightSibling((DependencyObject) column, value);
    }
  }
}
