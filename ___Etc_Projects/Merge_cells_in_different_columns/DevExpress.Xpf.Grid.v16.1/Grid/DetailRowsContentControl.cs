// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DetailRowsContentControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class DetailRowsContentControl : ContentControl
  {
    public static readonly DependencyProperty ViewProperty = DependencyPropertyManager.Register("View", typeof (DataViewBase), typeof (DetailRowsContentControl), (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty DetailDescriptorProperty = DependencyPropertyManager.Register("DetailDescriptor", typeof (DetailDescriptorBase), typeof (DetailRowsContentControl), (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty NeedBottomLineProperty = DependencyPropertyManager.Register("NeedBottomLine", typeof (bool), typeof (DetailRowsContentControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) false));

    public DataViewBase View
    {
      get
      {
        return (DataViewBase) this.GetValue(DetailRowsContentControl.ViewProperty);
      }
      set
      {
        this.SetValue(DetailRowsContentControl.ViewProperty, (object) value);
      }
    }

    public DetailDescriptorBase DetailDescriptor
    {
      get
      {
        return (DetailDescriptorBase) this.GetValue(DetailRowsContentControl.DetailDescriptorProperty);
      }
      set
      {
        this.SetValue(DetailRowsContentControl.DetailDescriptorProperty, (object) value);
      }
    }

    public bool NeedBottomLine
    {
      get
      {
        return (bool) this.GetValue(DetailRowsContentControl.NeedBottomLineProperty);
      }
      set
      {
        this.SetValue(DetailRowsContentControl.NeedBottomLineProperty, (object) value);
      }
    }

    public DetailRowsContentControl()
    {
      this.SetDefaultStyleKey(typeof (DetailRowsContentControl));
    }
  }
}
