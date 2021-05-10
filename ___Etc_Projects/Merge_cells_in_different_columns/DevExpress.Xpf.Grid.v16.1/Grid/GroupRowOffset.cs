// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupRowOffset
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class GroupRowOffset : RowOffset
  {
    public static readonly DependencyProperty IsExpandedProperty = DependencyPropertyManager.Register("IsExpanded", typeof (bool), typeof (GroupRowOffset), (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) ((d, e) => ((RowOffsetBase) d).DrawLines(((FrameworkElement) d).ActualHeight))));

    public bool IsExpanded
    {
      get
      {
        return (bool) this.GetValue(GroupRowOffset.IsExpandedProperty);
      }
      set
      {
        this.SetValue(GroupRowOffset.IsExpandedProperty, (object) value);
      }
    }

    protected override void DrawHorizontalLines(double height)
    {
      if (this.IsExpanded || this.ShowGroupSummaryFooter)
        return;
      base.DrawHorizontalLines(height);
    }
  }
}
