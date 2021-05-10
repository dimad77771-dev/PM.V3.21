// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.OuterBorderElement
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class OuterBorderElement : Control
  {
    public static readonly DependencyProperty ShowColumnHeadersProperty = DependencyPropertyManager.Register("ShowColumnHeaders", typeof (bool), typeof (OuterBorderElement), new PropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((OuterBorderElement) d).OnShowColumnHeadersChanged())));

    public bool ShowColumnHeaders
    {
      get
      {
        return (bool) this.GetValue(OuterBorderElement.ShowColumnHeadersProperty);
      }
      set
      {
        this.SetValue(OuterBorderElement.ShowColumnHeadersProperty, (object) value);
      }
    }

    public OuterBorderElement()
    {
      this.SetDefaultStyleKey(typeof (OuterBorderElement));
      this.OnShowColumnHeadersChanged();
    }

    private void OnShowColumnHeadersChanged()
    {
      this.BorderThickness = this.ShowColumnHeaders ? new Thickness(1.0, 0.0, 1.0, 1.0) : new Thickness(1.0);
    }
  }
}
