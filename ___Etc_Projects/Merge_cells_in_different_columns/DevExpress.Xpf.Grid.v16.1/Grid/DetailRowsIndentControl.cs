// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DetailRowsIndentControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Utils;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class DetailRowsIndentControl : ItemsControl
  {
    public static readonly DependencyProperty IsSummaryDetailLevelProperty = DependencyPropertyManager.RegisterAttached("IsSummaryDetailLevel", typeof (bool), typeof (DetailRowsIndentControl));

    public DetailRowsIndentControl()
    {
      ControlExtensions.SetDefaultStyleKey(this, typeof (DetailRowsIndentControl));
    }

    public static bool GetIsSummaryDetailLevel(DependencyObject element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      return (bool) element.GetValue(DetailRowsIndentControl.IsSummaryDetailLevelProperty);
    }

    public static void SetIsSummaryDetailLevel(DependencyObject element, bool value)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      element.SetValue(DetailRowsIndentControl.IsSummaryDetailLevelProperty, (object) value);
    }

    protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
    {
      base.PrepareContainerForItemOverride(element, item);
      element.SetValue(DataObjectBase.DataObjectProperty, this.DataContext);
      FrameworkElement frameworkElement = element as FrameworkElement;
      if (frameworkElement == null || !(frameworkElement.DataContext is DetailIndent))
        return;
      bool flag = ((DetailIndent) frameworkElement.DataContext).Level == DetailSummaryControlBase.GetSummaryDetailLevel((DependencyObject) this);
      DetailRowsIndentControl.SetIsSummaryDetailLevel(element, flag);
    }
  }
}
