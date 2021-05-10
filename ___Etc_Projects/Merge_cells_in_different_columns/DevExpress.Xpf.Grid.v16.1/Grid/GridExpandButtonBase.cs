// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridExpandButtonBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid
{
  public class GridExpandButtonBase : ContentControl
  {
    public static readonly DependencyProperty IsCheckedProperty = DependencyPropertyManager.Register("IsChecked", typeof (bool), typeof (GridExpandButtonBase), new PropertyMetadata((object) false));
    public static readonly DependencyProperty CommandProperty = DependencyPropertyManager.Register("Command", typeof (ICommand), typeof (GridExpandButtonBase), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty CommandParameterProperty = DependencyPropertyManager.Register("CommandParameter", typeof (object), typeof (GridExpandButtonBase), new PropertyMetadata((PropertyChangedCallback) null));

    public bool IsChecked
    {
      get
      {
        return (bool) this.GetValue(GridExpandButtonBase.IsCheckedProperty);
      }
      set
      {
        this.SetValue(GridExpandButtonBase.IsCheckedProperty, (object) value);
      }
    }

    public ICommand Command
    {
      get
      {
        return (ICommand) this.GetValue(GridExpandButtonBase.CommandProperty);
      }
      set
      {
        this.SetValue(GridExpandButtonBase.CommandProperty, (object) value);
      }
    }

    public object CommandParameter
    {
      get
      {
        return this.GetValue(GridExpandButtonBase.CommandParameterProperty);
      }
      set
      {
        this.SetValue(GridExpandButtonBase.CommandParameterProperty, value);
      }
    }
  }
}
