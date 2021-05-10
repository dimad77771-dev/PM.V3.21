// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DataNavigator
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.HitTest;
using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class DataNavigator : ContentControl
  {
    public static readonly DependencyProperty ViewProperty = DependencyProperty.Register("View", typeof (DataViewBase), typeof (DataNavigator), new PropertyMetadata((PropertyChangedCallback) null));

    public DataViewBase View
    {
      get
      {
        return (DataViewBase) this.GetValue(DataNavigator.ViewProperty);
      }
      set
      {
        this.SetValue(DataNavigator.ViewProperty, (object) value);
      }
    }

    public DataNavigator()
    {
      this.SetDefaultStyleKey(typeof (DataNavigator));
      GridViewHitInfoBase.SetHitTestAcceptor((DependencyObject) this, (DataViewHitTestAcceptorBase) new DataNavigatorTableViewHitTestAcceptor());
    }
  }
}
