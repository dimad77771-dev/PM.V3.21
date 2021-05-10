// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.ErrorPanel
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Utils;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid
{
  public class ErrorPanel : Control
  {
    private FrameworkElement root;

    private FrameworkElement Root
    {
      get
      {
        return this.root;
      }
      set
      {
        if (this.root == value)
          return;
        if (this.root != null)
          this.root.RemoveHandler(UIElement.MouseLeftButtonDownEvent, (Delegate) new MouseButtonEventHandler(this.InternalOnMouseLeftButtonDown));
        this.root = value;
        if (this.root == null)
          return;
        this.root.AddHandler(UIElement.MouseLeftButtonDownEvent, (Delegate) new MouseButtonEventHandler(this.InternalOnMouseLeftButtonDown), true);
      }
    }

    public ErrorPanel()
    {
      this.SetDefaultStyleKey(typeof (ErrorPanel));
      this.Loaded += new RoutedEventHandler(this.OnLoaded);
      this.Unloaded += new RoutedEventHandler(this.OnUnloaded);
    }

    public void OnLoaded(object sender, RoutedEventArgs e)
    {
      this.Root = this.GetTopLevelVisual();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
      this.Root = (FrameworkElement) null;
    }

    internal void InternalOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      this.Visibility = Visibility.Collapsed;
    }

    private FrameworkElement GetTopLevelVisual()
    {
      FrameworkElement topLevelVisual = LayoutHelper.GetTopLevelVisual((DependencyObject) this);
      if (topLevelVisual is Popup)
        return ((Popup) topLevelVisual).Child as FrameworkElement;
      return topLevelVisual;
    }
  }
}
