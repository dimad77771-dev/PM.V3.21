// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.ChildMinWidthPanel
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  [DXToolboxBrowsable(false)]
  public class ChildMinWidthPanel : Decorator
  {
    public static readonly DependencyProperty ChildMinWidthProperty = DependencyProperty.Register("ChildMinWidth", typeof (double), typeof (ChildMinWidthPanel), new PropertyMetadata((object) 0.0, (PropertyChangedCallback) ((d, e) => ((ChildMinWidthPanel) d).OnChildMinWidthChanged())));

    public double ChildMinWidth
    {
      get
      {
        return (double) this.GetValue(ChildMinWidthPanel.ChildMinWidthProperty);
      }
      set
      {
        this.SetValue(ChildMinWidthPanel.ChildMinWidthProperty, (object) value);
      }
    }

    private void OnChildMinWidthChanged()
    {
      this.InvalidateMeasure();
    }

    protected override Size MeasureOverride(Size availableSize)
    {
      this.Child.Measure(availableSize);
      Size desiredSize = this.Child.DesiredSize;
      return new Size(Math.Min(Math.Max(this.ChildMinWidth, desiredSize.Width), availableSize.Width), desiredSize.Height);
    }
  }
}
