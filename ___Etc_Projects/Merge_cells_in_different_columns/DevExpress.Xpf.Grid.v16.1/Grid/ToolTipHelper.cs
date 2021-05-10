// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.ToolTipHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class ToolTipHelper : DependencyObject
  {
    public static readonly DependencyProperty ShowTooltipProperty = DependencyProperty.RegisterAttached("ShowTooltip", typeof (bool), typeof (ToolTipHelper), (PropertyMetadata) new FrameworkPropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ToolTipHelper.UpdateToolTip(d, e))));
    public static readonly DependencyProperty ContentProperty = DependencyProperty.RegisterAttached("Content", typeof (object), typeof (ToolTipHelper), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ToolTipHelper.UpdateToolTip(d, e))));
    public static readonly DependencyProperty ContentTemplateProperty = DependencyProperty.RegisterAttached("ContentTemplate", typeof (DataTemplate), typeof (ToolTipHelper), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ToolTipHelper.UpdateToolTip(d, e))));

    public static bool GetShowTooltip(DependencyObject obj)
    {
      return (bool) obj.GetValue(ToolTipHelper.ShowTooltipProperty);
    }

    public static void SetShowTooltip(DependencyObject obj, bool value)
    {
      obj.SetValue(ToolTipHelper.ShowTooltipProperty, (object) value);
    }

    public static object GetContent(DependencyObject obj)
    {
      return obj.GetValue(ToolTipHelper.ContentProperty);
    }

    public static void SetContent(DependencyObject obj, object value)
    {
      obj.SetValue(ToolTipHelper.ContentProperty, value);
    }

    public static DataTemplate GetContentTemplate(DependencyObject obj)
    {
      return (DataTemplate) obj.GetValue(ToolTipHelper.ContentTemplateProperty);
    }

    public static void SetContentTemplate(DependencyObject obj, DataTemplate value)
    {
      obj.SetValue(ToolTipHelper.ContentTemplateProperty, (object) value);
    }

    private static void UpdateToolTip(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (ToolTipHelper.GetShowTooltip(d) && ToolTipHelper.GetContent(d) != null && ToolTipHelper.GetContentTemplate(d) != null)
      {
        DependencyObject element = d;
        ToolTip toolTip1 = new ToolTip();
        toolTip1.Content = ToolTipHelper.GetContent(d);
        toolTip1.ContentTemplate = ToolTipHelper.GetContentTemplate(d);
        ToolTip toolTip2 = toolTip1;
        ToolTipService.SetToolTip(element, (object) toolTip2);
      }
      else
        d.ClearValue(ToolTipService.ToolTipProperty);
    }
  }
}
