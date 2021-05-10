// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.IndentScroller
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class IndentScroller : Control
  {
    private Queue<UIElement> freeElements = new Queue<UIElement>();
    private System.Windows.Controls.Grid scrollableContent;

    public IndentScroller()
    {
      this.SetDefaultStyleKey(typeof (IndentScroller));
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.scrollableContent = this.GetTemplateChild("PART_ScrollableContent") as System.Windows.Controls.Grid;
      this.UpdateScrollableContent();
    }

    private void UpdateScrollableContent()
    {
      if (this.scrollableContent == null)
        return;
      while (this.freeElements.Count > 0)
        this.scrollableContent.Children.Add(this.freeElements.Dequeue());
    }

    internal void AddScrollableElement(UIElement element, int position)
    {
      if (element == null)
        return;
      this.freeElements.Enqueue(element);
      System.Windows.Controls.Grid.SetColumn(element, position);
    }

    internal void SetScrollOffset(Thickness offset)
    {
      if (this.scrollableContent == null)
        return;
      this.scrollableContent.Margin = offset;
    }

    internal double GetRowIndentWidth()
    {
      if (this.scrollableContent == null || this.scrollableContent.ColumnDefinitions.Count < 1)
        return 0.0;
      return Math.Max(0.0, this.scrollableContent.ColumnDefinitions[0].ActualWidth + this.scrollableContent.Margin.Left);
    }
  }
}
