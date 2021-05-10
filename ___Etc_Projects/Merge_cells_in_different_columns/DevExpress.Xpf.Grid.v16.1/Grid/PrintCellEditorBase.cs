// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintCellEditorBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;
using System;
using System.Windows;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class PrintCellEditorBase : CellEditor
  {
    [IgnoreDependencyPropertiesConsistencyChecker]
    internal static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof (Brush), typeof (PrintCellEditorBase), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((PrintCellEditorBase) d).UpdateBackground())));

    internal Brush Background
    {
      get
      {
        return (Brush) this.GetValue(PrintCellEditorBase.BackgroundProperty);
      }
    }

    protected void UpdateBackground()
    {
      (this.editCore as BaseEdit).Do<BaseEdit>((Action<BaseEdit>) (x => x.Background = (Brush) this.GetValue(PrintCellEditorBase.BackgroundProperty)));
    }
  }
}
