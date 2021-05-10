// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.RowIndicatorViewInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class RowIndicatorViewInfo : DependencyObject
  {
    public static readonly DependencyProperty AnimationProgressProperty = DependencyPropertyManager.RegisterAttached("AnimationProgress", typeof (double), typeof (RowIndicatorViewInfo), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0, new PropertyChangedCallback(RowIndicatorViewInfo.OnPropertyChanged)));
    public static readonly DependencyProperty VisibleSizeProperty = DependencyPropertyManager.RegisterAttached("VisibleSize", typeof (Size), typeof (RowIndicatorViewInfo), (PropertyMetadata) new FrameworkPropertyMetadata((object) Size.Empty, new PropertyChangedCallback(RowIndicatorViewInfo.OnPropertyChanged)));

    public static void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
      FrameworkElement frameworkElement = sender as FrameworkElement;
      if (frameworkElement == null)
        return;
      RowData rowData = frameworkElement.DataContext as RowData;
      if (rowData == null)
        return;
      rowData.SetValue(e.Property, e.NewValue);
    }

    public static void SetAnimationProgress(DependencyObject d, double progress)
    {
      d.SetValue(RowIndicatorViewInfo.AnimationProgressProperty, (object) progress);
    }

    public static double GetAnimationProgress(DependencyObject d)
    {
      return (double) d.GetValue(RowIndicatorViewInfo.AnimationProgressProperty);
    }

    public static void SetVisibleSize(DependencyObject d, double size)
    {
      d.SetValue(RowIndicatorViewInfo.VisibleSizeProperty, (object) size);
    }

    public static Size GetVisibleSize(DependencyObject d)
    {
      return (Size) d.GetValue(RowIndicatorViewInfo.VisibleSizeProperty);
    }
  }
}
