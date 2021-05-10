// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.BandsContainerControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class BandsContainerControl : Control
  {
    public static readonly DependencyProperty BandsLayoutProperty = DependencyPropertyManager.Register("BandsLayout", typeof (BandsLayoutBase), typeof (BandsContainerControl), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((BandsContainerControl) d).OnBandsLayoutChanged())));
    private BandsPanel fixedNonePanel;
    private BandsPanel fixedLeftPanel;
    private BandsPanel fixedRightPanel;

    public BandsLayoutBase BandsLayout
    {
      get
      {
        return (BandsLayoutBase) this.GetValue(BandsContainerControl.BandsLayoutProperty);
      }
      set
      {
        this.SetValue(BandsContainerControl.BandsLayoutProperty, (object) value);
      }
    }

    public BandsContainerControl()
    {
      ControlExtensions.SetDefaultStyleKey(this, typeof (BandsContainerControl));
    }

    private void OnBandsLayoutChanged()
    {
      if (this.BandsLayout == null)
        this.Visibility = Visibility.Collapsed;
      else
        this.BandsLayout.UpdateBandsContainer((FrameworkElement) this);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.fixedNonePanel = this.FindBandsPanel("bandsPanel");
      this.fixedLeftPanel = this.FindBandsPanel("fixedLeftBandsPanel");
      this.fixedRightPanel = this.FindBandsPanel("fixedRightBandsPanel");
    }

    private BandsPanel FindBandsPanel(string name)
    {
      return this.GetTemplateChild(name) as BandsPanel;
    }

    internal BandsPanel GetBandsPanel(FixedStyle fixedStyle)
    {
      switch (fixedStyle)
      {
        case FixedStyle.None:
          return this.fixedNonePanel;
        case FixedStyle.Left:
          return this.fixedLeftPanel;
        case FixedStyle.Right:
          return this.fixedRightPanel;
        default:
          return (BandsPanel) null;
      }
    }

    protected override Size MeasureOverride(Size constraint)
    {
      return MeasurePixelSnapperHelper.MeasureOverride(base.MeasureOverride(constraint), SnapperType.Ceil);
    }
  }
}
