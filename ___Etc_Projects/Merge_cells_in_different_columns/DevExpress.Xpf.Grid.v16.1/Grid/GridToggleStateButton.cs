// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridToggleStateButton
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Utils;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace DevExpress.Xpf.Grid
{
  public class GridToggleStateButton : ToggleStateButton, IDataObjectReset
  {
    public static readonly DependencyProperty ShowRowBreakProperty = DependencyProperty.Register("ShowRowBreak", typeof (bool), typeof (GridToggleStateButton), new PropertyMetadata((object) false));
    private bool isReady;

    public bool ShowRowBreak
    {
      get
      {
        return (bool) this.GetValue(GridToggleStateButton.ShowRowBreakProperty);
      }
      set
      {
        this.SetValue(GridToggleStateButton.ShowRowBreakProperty, (object) value);
      }
    }

    public Storyboard Expand { get; set; }

    public Storyboard Collapse { get; set; }

    public GridToggleStateButton()
    {
      ControlExtensions.SetDefaultStyleKey(this, typeof (GridToggleStateButton));
      this.Checked += new RoutedEventHandler(this.GridToggleStateButton_Checked);
      this.Unchecked += new RoutedEventHandler(this.GridToggleStateButton_Unchecked);
    }

    [Browsable(false)]
    public bool ShouldSerializeIsChecked(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    private void GridToggleStateButton_Unchecked(object sender, RoutedEventArgs e)
    {
      if (this.Collapse == null)
        return;
      this.isReady = true;
      this.Collapse.Begin();
    }

    private void GridToggleStateButton_Checked(object sender, RoutedEventArgs e)
    {
      if (this.Expand == null)
        return;
      this.isReady = true;
      this.Expand.Begin();
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      FrameworkElement frameworkElement = this.GetTemplateChild("root") as FrameworkElement;
      if (frameworkElement == null)
        return;
      this.Expand = frameworkElement.Resources[(object) "expand"] as Storyboard;
      this.Collapse = frameworkElement.Resources[(object) "collapse"] as Storyboard;
      this.InitializeAnimation(this.IsChecked.GetValueOrDefault(false) ? this.Expand : this.Collapse);
      this.isReady = false;
    }

    private void InitializeAnimation(Storyboard storyboard)
    {
      if (storyboard == null)
        return;
      storyboard.Begin();
      storyboard.SkipToFill();
    }

    void IDataObjectReset.Reset()
    {
      if (!this.isReady)
        return;
      if (this.Expand != null)
        this.Expand.SkipToFill();
      if (this.Collapse == null)
        return;
      this.Collapse.SkipToFill();
    }
  }
}
