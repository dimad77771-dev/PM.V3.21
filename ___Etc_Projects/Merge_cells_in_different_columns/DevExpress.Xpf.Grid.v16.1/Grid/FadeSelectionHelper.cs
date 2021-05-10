// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.FadeSelectionHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class FadeSelectionHelper
  {
    public static readonly DependencyProperty IsKeyboardFocusWithinViewProperty = DependencyPropertyManager.RegisterAttached("IsKeyboardFocusWithinView", typeof (bool), typeof (FadeSelectionHelper), (PropertyMetadata) new FrameworkPropertyMetadata((object) false, new PropertyChangedCallback(FadeSelectionHelper.OnFadeSelectionHelperPropertyChanged)));
    public static readonly DependencyProperty FadeSelectionOnLostFocusProperty = DependencyPropertyManager.RegisterAttached("FadeSelectionOnLostFocus", typeof (bool), typeof (FadeSelectionHelper), (PropertyMetadata) new FrameworkPropertyMetadata((object) false, new PropertyChangedCallback(FadeSelectionHelper.OnFadeSelectionHelperPropertyChanged)));
    public static readonly DependencyProperty OpacityProperty = DependencyPropertyManager.RegisterAttached("Opacity", typeof (double), typeof (FadeSelectionHelper), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.5, new PropertyChangedCallback(FadeSelectionHelper.OnFadeSelectionHelperPropertyChanged)));
    public static readonly DependencyProperty IsSelectedProperty = DependencyPropertyManager.RegisterAttached("IsSelected", typeof (bool?), typeof (FadeSelectionHelper), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(FadeSelectionHelper.OnFadeSelectionHelperPropertyChanged)));

    public static bool GetIsKeyboardFocusWithinView(DependencyObject obj)
    {
      return (bool) obj.GetValue(FadeSelectionHelper.IsKeyboardFocusWithinViewProperty);
    }

    public static void SetIsKeyboardFocusWithinView(DependencyObject obj, bool value)
    {
      obj.SetValue(FadeSelectionHelper.IsKeyboardFocusWithinViewProperty, (object) value);
    }

    public static bool GetFadeSelectionOnLostFocus(DependencyObject obj)
    {
      return (bool) obj.GetValue(FadeSelectionHelper.FadeSelectionOnLostFocusProperty);
    }

    public static void SetFadeSelectionOnLostFocus(DependencyObject obj, bool value)
    {
      obj.SetValue(FadeSelectionHelper.FadeSelectionOnLostFocusProperty, (object) value);
    }

    public static double GetOpacity(DependencyObject obj)
    {
      return (double) obj.GetValue(FadeSelectionHelper.OpacityProperty);
    }

    public static void SetOpacity(DependencyObject obj, double value)
    {
      obj.SetValue(FadeSelectionHelper.OpacityProperty, (object) value);
    }

    public static bool? GetIsSelected(DependencyObject obj)
    {
      return (bool?) obj.GetValue(FadeSelectionHelper.IsSelectedProperty);
    }

    public static void SetIsSelected(DependencyObject obj, bool? value)
    {
      obj.SetValue(FadeSelectionHelper.IsSelectedProperty, (object) value);
    }

    private static void UpdateElementOpacity(DependencyObject element, bool fadeSelectionOnLostFocus, bool isKeyboardFocusWithin)
    {
      bool isFadeNeeded = FadeSelectionHelper.IsFadeNeeded(fadeSelectionOnLostFocus, isKeyboardFocusWithin);
      if (isFadeNeeded)
        element.SetValue(UIElement.OpacityProperty, (object) FadeSelectionHelper.GetOpacity(element));
      else
        element.ClearValue(UIElement.OpacityProperty);
      FadeSelectionHelper.OnSelectionChanged(element, isFadeNeeded);
    }

    internal static bool IsFadeNeeded(bool fadeSelectionOnLostFocus, bool isKeyboardFocusWithin)
    {
      if (fadeSelectionOnLostFocus)
        return !isKeyboardFocusWithin;
      return false;
    }

    private static void OnFadeSelectionHelperPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      FadeSelectionHelper.UpdateElementOpacity(d, FadeSelectionHelper.GetFadeSelectionOnLostFocus(d), FadeSelectionHelper.GetIsKeyboardFocusWithinView(d));
    }

    private static void OnSelectionChanged(DependencyObject d, bool isFadeNeeded)
    {
      if (!FadeSelectionHelper.GetIsSelected(d).HasValue)
        return;
      if (isFadeNeeded)
      {
        bool? isSelected = FadeSelectionHelper.GetIsSelected(d);
        if ((!isSelected.GetValueOrDefault() ? 0 : (isSelected.HasValue ? 1 : 0)) != 0)
        {
          FadeSelectionHelper.SetVisibility(d, Visibility.Visible);
          return;
        }
      }
      FadeSelectionHelper.SetVisibility(d, Visibility.Collapsed);
    }

    private static bool IsFadeNeeded(DependencyObject d)
    {
      if (FadeSelectionHelper.GetFadeSelectionOnLostFocus(d))
        return !FadeSelectionHelper.GetIsKeyboardFocusWithinView(d);
      return false;
    }

    private static void SetVisibility(DependencyObject d, Visibility value)
    {
      UIElement uiElement = d as UIElement;
      if (uiElement == null)
        return;
      uiElement.Visibility = value;
    }
  }
}
