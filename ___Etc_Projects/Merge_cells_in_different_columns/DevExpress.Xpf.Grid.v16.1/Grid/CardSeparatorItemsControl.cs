// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CardSeparatorItemsControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class CardSeparatorItemsControl : CachedItemsControl
  {
    protected override FrameworkElement CreateChild(object item)
    {
      FrameworkElement child = base.CreateChild(item);
      child.SetBinding(FrameworkElement.MarginProperty, (BindingBase) new Binding("Margin"));
      return child;
    }
  }
}
