// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintBandsPanel
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Editors;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class PrintBandsPanel : BandsPanelBase
  {
    protected override FrameworkElement CreateBandElement(BandBase band)
    {
      PrintCellInfo printCellInfo = GridPrintingHelper.GetPrintCellInfo((DependencyObject) band);
      TextEdit textEdit1 = new TextEdit();
      textEdit1.EditValue = printCellInfo.HeaderCaption;
      textEdit1.Style = printCellInfo.PrintColumnHeaderStyle;
      TextEdit textEdit2 = textEdit1;
      textEdit2.DataContext = (object) band;
      return (FrameworkElement) textEdit2;
    }

    protected override double GetBandWidth(BandBase band)
    {
      return GridPrintingHelper.GetPrintCellInfo((DependencyObject) band).PrintColumnWidth;
    }
  }
}
