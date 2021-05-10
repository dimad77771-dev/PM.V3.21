// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListSummarySettings
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListSummarySettings
  {
    public static readonly DependencyProperty IsRecursiveProperty = DependencyPropertyManager.RegisterAttached("IsRecursive", typeof (bool), typeof (TreeListSummarySettings), (PropertyMetadata) new FrameworkPropertyMetadata((object) true));

    public static bool GetIsRecursive(DependencyObject d)
    {
      return (bool) d.GetValue(TreeListSummarySettings.IsRecursiveProperty);
    }

    public static void SetIsRecursive(DependencyObject d, bool value)
    {
      d.SetValue(TreeListSummarySettings.IsRecursiveProperty, (object) value);
    }
  }
}
