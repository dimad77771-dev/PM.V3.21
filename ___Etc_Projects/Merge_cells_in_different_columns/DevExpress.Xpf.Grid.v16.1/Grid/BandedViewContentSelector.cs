// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.BandedViewContentSelector
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class BandedViewContentSelector : Control
  {
    public static readonly DependencyProperty TableViewContentProperty;
    public static readonly DependencyProperty BandedViewContentProperty;
    public static readonly DependencyProperty BandsLayoutProperty;
    public static readonly DependencyProperty OwnerElementProperty;

    public ControlTemplate TableViewContent
    {
      get
      {
        return (ControlTemplate) this.GetValue(BandedViewContentSelector.TableViewContentProperty);
      }
      set
      {
        this.SetValue(BandedViewContentSelector.TableViewContentProperty, (object) value);
      }
    }

    public ControlTemplate BandedViewContent
    {
      get
      {
        return (ControlTemplate) this.GetValue(BandedViewContentSelector.BandedViewContentProperty);
      }
      set
      {
        this.SetValue(BandedViewContentSelector.BandedViewContentProperty, (object) value);
      }
    }

    public BandsLayoutBase BandsLayout
    {
      get
      {
        return (BandsLayoutBase) this.GetValue(BandedViewContentSelector.BandsLayoutProperty);
      }
      set
      {
        this.SetValue(BandedViewContentSelector.BandsLayoutProperty, (object) value);
      }
    }

    public FrameworkElement OwnerElement
    {
      get
      {
        return (FrameworkElement) this.GetValue(BandedViewContentSelector.OwnerElementProperty);
      }
      set
      {
        this.SetValue(BandedViewContentSelector.OwnerElementProperty, (object) value);
      }
    }

    static BandedViewContentSelector()
    {
      Type ownerType = typeof (BandedViewContentSelector);
      BandedViewContentSelector.TableViewContentProperty = DependencyProperty.Register("TableViewContent", typeof (ControlTemplate), ownerType, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((BandedViewContentSelector) d).UpdateTemplate())));
      BandedViewContentSelector.BandedViewContentProperty = DependencyProperty.Register("BandedViewContent", typeof (ControlTemplate), ownerType, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((BandedViewContentSelector) d).UpdateTemplate())));
      BandedViewContentSelector.BandsLayoutProperty = DependencyProperty.Register("BandsLayout", typeof (BandsLayoutBase), ownerType, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((BandedViewContentSelector) d).UpdateTemplate())));
      BandedViewContentSelector.OwnerElementProperty = DependencyProperty.Register("OwnerElement", typeof (FrameworkElement), ownerType, new PropertyMetadata((PropertyChangedCallback) null));
    }

    private void UpdateTemplate()
    {
      this.Template = this.BandsLayout == null ? this.TableViewContent : this.BandedViewContent;
    }
  }
}
