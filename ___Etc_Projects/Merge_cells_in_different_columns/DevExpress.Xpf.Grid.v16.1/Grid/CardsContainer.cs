// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CardsContainer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class CardsContainer : Control
  {
    public static readonly DependencyProperty DataPresenterProperty = DependencyProperty.Register("DataPresenter", typeof (DataPresenterBase), typeof (CardsContainer), new PropertyMetadata((PropertyChangedCallback) null));

    public DataPresenterBase DataPresenter
    {
      get
      {
        return (DataPresenterBase) this.GetValue(CardsContainer.DataPresenterProperty);
      }
      set
      {
        this.SetValue(CardsContainer.DataPresenterProperty, (object) value);
      }
    }

    public CardsContainer()
    {
      this.Focusable = false;
      this.SetDefaultStyleKey(typeof (CardsContainer));
    }
  }
}
