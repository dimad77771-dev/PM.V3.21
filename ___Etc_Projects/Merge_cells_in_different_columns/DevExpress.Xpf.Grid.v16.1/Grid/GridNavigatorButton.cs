// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridNavigatorButton
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace DevExpress.Xpf.Grid
{
  public class GridNavigatorButton : RepeatButton
  {
    public static readonly DependencyProperty ButtonTypeProperty = DependencyPropertyManager.Register("ButtonType", typeof (NavigatorButtonType), typeof (GridNavigatorButton), (PropertyMetadata) new FrameworkPropertyMetadata((object) NavigatorButtonType.MoveFirstRow));

    public NavigatorButtonType ButtonType
    {
      get
      {
        return (NavigatorButtonType) this.GetValue(GridNavigatorButton.ButtonTypeProperty);
      }
      set
      {
        this.SetValue(GridNavigatorButton.ButtonTypeProperty, (object) value);
      }
    }

    public GridNavigatorButton()
    {
      this.SetDefaultStyleKey(typeof (GridNavigatorButton));
    }
  }
}
