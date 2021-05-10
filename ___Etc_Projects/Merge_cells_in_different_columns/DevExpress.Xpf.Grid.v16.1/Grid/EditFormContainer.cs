// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.EditFormContainer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class EditFormContainer : ContentControl
  {
    public static readonly DependencyProperty ShowModeProperty = DependencyProperty.Register("ShowMode", typeof (EditFormShowMode), typeof (EditFormContainer), new PropertyMetadata((object) EditFormShowMode.None));

    public EditFormShowMode ShowMode
    {
      get
      {
        return (EditFormShowMode) this.GetValue(EditFormContainer.ShowModeProperty);
      }
      set
      {
        this.SetValue(EditFormContainer.ShowModeProperty, (object) value);
      }
    }

    static EditFormContainer()
    {
      Type forType = typeof (EditFormContainer);
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, (PropertyMetadata) new FrameworkPropertyMetadata((object) forType));
    }
  }
}
