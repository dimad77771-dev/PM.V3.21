// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintRowStackPanel
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Utils;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class PrintRowStackPanel : StackPanel
  {
    public static readonly DependencyProperty IsRowDataBottomIndentVisibleProperty;
    public static readonly DependencyProperty IsRowDataBottomLastIndentVisibleProperty;

    public bool IsRowDataBottomIndentVisible
    {
      get
      {
        return (bool) this.GetValue(PrintRowStackPanel.IsRowDataBottomIndentVisibleProperty);
      }
      set
      {
        this.SetValue(PrintRowStackPanel.IsRowDataBottomIndentVisibleProperty, (object) value);
      }
    }

    public bool IsRowDataBottomLastIndentVisible
    {
      get
      {
        return (bool) this.GetValue(PrintRowStackPanel.IsRowDataBottomLastIndentVisibleProperty);
      }
      set
      {
        this.SetValue(PrintRowStackPanel.IsRowDataBottomLastIndentVisibleProperty, (object) value);
      }
    }

    static PrintRowStackPanel()
    {
      Type ownerType = typeof (PrintRowStackPanel);
      PrintRowStackPanel.IsRowDataBottomIndentVisibleProperty = DependencyPropertyManager.Register("IsRowDataBottomIndentVisible", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((PrintRowStackPanel) d).OnIndentVisibleChanged())));
      PrintRowStackPanel.IsRowDataBottomLastIndentVisibleProperty = DependencyPropertyManager.Register("IsRowDataBottomLastIndentVisible", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((PrintRowStackPanel) d).OnIndentVisibleChanged())));
    }

    private void OnIndentVisibleChanged()
    {
      ExportSettings.SetTargetType((DependencyObject) this, this.IsRowDataBottomIndentVisible || this.IsRowDataBottomLastIndentVisible ? TargetType.Panel : ExportSettingDefaultValue.TargetType);
    }
  }
}
