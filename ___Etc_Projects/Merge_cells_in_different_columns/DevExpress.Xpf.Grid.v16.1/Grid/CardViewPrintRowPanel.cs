// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CardViewPrintRowPanel
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Printing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class CardViewPrintRowPanel : FrameworkElement
  {
    private int afterGroupRowHandle = int.MaxValue;
    private int lastRowHandle = int.MaxValue;
    public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register("ItemTemplate", typeof (DataTemplate), typeof (CardViewPrintRowPanel), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty RowDataProperty = DependencyProperty.Register("RowData", typeof (RowData), typeof (CardViewPrintRowPanel), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty RowProperty = DependencyProperty.Register("Row", typeof (object), typeof (CardViewPrintRowPanel), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((CardViewPrintRowPanel) d).OnRowChanged())));
    public static readonly DependencyProperty RowIndentControlTemplateProperty = DependencyProperty.Register("RowIndentControlTemplate", typeof (DataTemplate), typeof (CardViewPrintRowPanel), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty IsFirstCardInRowProperty = DependencyProperty.RegisterAttached("IsFirstCardInRow", typeof (bool), typeof (CardViewPrintRowPanel), new PropertyMetadata((object) false));
    public static readonly DependencyProperty PrintMaximumCardColumnsProperty = DependencyProperty.RegisterAttached("PrintMaximumCardColumns", typeof (int), typeof (CardViewPrintRowPanel), new PropertyMetadata((object) -1));
    public static readonly DependencyProperty PrintAutoCardWidthProperty = DependencyProperty.RegisterAttached("PrintAutoCardWidth", typeof (bool), typeof (CardViewPrintRowPanel), new PropertyMetadata((object) false));
    private readonly List<ContentControl> VisualChildrenCache;
    private readonly List<ContentControl> VisualChildren;
    private ContentControl RowIndentControl;
    private RowData LastPrintRowData;

    public RowData RowData
    {
      get
      {
        return (RowData) this.GetValue(CardViewPrintRowPanel.RowDataProperty);
      }
      set
      {
        this.SetValue(CardViewPrintRowPanel.RowDataProperty, (object) value);
      }
    }

    public object Row
    {
      get
      {
        return this.GetValue(CardViewPrintRowPanel.RowProperty);
      }
      set
      {
        this.SetValue(CardViewPrintRowPanel.RowProperty, value);
      }
    }

    public DataTemplate ItemTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(CardViewPrintRowPanel.ItemTemplateProperty);
      }
      set
      {
        this.SetValue(CardViewPrintRowPanel.ItemTemplateProperty, (object) value);
      }
    }

    public DataTemplate RowIndentControlTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(CardViewPrintRowPanel.RowIndentControlTemplateProperty);
      }
      set
      {
        this.SetValue(CardViewPrintRowPanel.RowIndentControlTemplateProperty, (object) value);
      }
    }

    public int PrintMaximumCardColumns
    {
      get
      {
        return (int) this.GetValue(CardViewPrintRowPanel.PrintMaximumCardColumnsProperty);
      }
      set
      {
        this.SetValue(CardViewPrintRowPanel.PrintMaximumCardColumnsProperty, (object) value);
      }
    }

    public bool PrintAutoCardWidth
    {
      get
      {
        return (bool) this.GetValue(CardViewPrintRowPanel.PrintAutoCardWidthProperty);
      }
      set
      {
        this.SetValue(CardViewPrintRowPanel.PrintAutoCardWidthProperty, (object) value);
      }
    }

    protected override int VisualChildrenCount
    {
      get
      {
        if (this.VisualChildren.Count != 0)
          return this.VisualChildren.Count + 1;
        return 0;
      }
    }

    public CardViewPrintRowPanel()
    {
      this.VisualChildrenCache = new List<ContentControl>();
      this.VisualChildren = new List<ContentControl>();
    }

    private void OnRowChanged()
    {
      this.InvalidateMeasure();
    }

    public static bool GetIsFirstCardInRow(DependencyObject obj)
    {
      return (bool) obj.GetValue(CardViewPrintRowPanel.IsFirstCardInRowProperty);
    }

    public static void SetIsFirstCardInRow(DependencyObject obj, bool value)
    {
      obj.SetValue(CardViewPrintRowPanel.IsFirstCardInRowProperty, (object) value);
    }

    protected override Visual GetVisualChild(int index)
    {
      if (index == 0)
        return (Visual) this.RowIndentControl;
      return (Visual) this.VisualChildren[index - 1];
    }

    private RowData GetNextRowData()
    {
      CardViewPrintingDataTreeBuilder treeBuilder = (CardViewPrintingDataTreeBuilder) this.RowData.treeBuilder;
      if (this.LastPrintRowData == null)
        return (RowData) this.CreateCardData(treeBuilder, this.RowData.node);
      int currentIndex = treeBuilder.AllNodes.IndexOf((DataRowNode) this.LastPrintRowData.node) + 1;
      if (currentIndex >= treeBuilder.AllNodes.Count)
        return (RowData) null;
      DataRowNode dataRowNode = treeBuilder.AllNodes[currentIndex];
      if (!(dataRowNode is GroupNode))
        return (RowData) this.CreateCardData(treeBuilder, (RowNode) dataRowNode);
      this.UpdateAfterGroupRowHandle(treeBuilder, currentIndex);
      return (RowData) null;
    }

    private CardData CreateCardData(CardViewPrintingDataTreeBuilder treeBuilder, RowNode node)
    {
      RowDataBase rowData = node.CreateRowData();
      rowData.AssignFromInternal((RowsContainer) null, treeBuilder.ReusingRowData.parentNodeContainer, node, false);
      treeBuilder.UpdateRowData((RowData) rowData);
      return (CardData) rowData;
    }

    private void UpdateAfterGroupRowHandle(CardViewPrintingDataTreeBuilder treeBuilder, int currentIndex)
    {
      for (int index = currentIndex + 1; index < treeBuilder.AllNodes.Count; ++index)
      {
        DataRowNode dataRowNode = treeBuilder.AllNodes[index];
        if (!(dataRowNode is GroupNode))
        {
          this.afterGroupRowHandle = dataRowNode.RowHandle.Value;
          break;
        }
      }
    }

    private Size RemergeCurrentRow(Size availableSize)
    {
      Size indentControlSize = this.GetRowIndentControlSize(availableSize);
      Size size1 = new Size(Math.Max(0.0, availableSize.Width - indentControlSize.Width), availableSize.Height);
      availableSize = new Size(size1.Width, availableSize.Height);
      Size size2 = new Size(availableSize.Width, double.IsInfinity(availableSize.Height) ? 0.0 : availableSize.Height);
      int index1 = 0;
      int num = this.PrintMaximumCardColumns > 0 ? this.PrintMaximumCardColumns : int.MaxValue;
      ContentControl control;
      while (true)
      {
        if (index1 == this.VisualChildren.Count)
        {
          RowData nextRowData = this.GetNextRowData();
          if (nextRowData != null)
          {
            this.AddVisualChildFromCache();
            this.VisualChildren[index1].Content = (object) nextRowData;
          }
          else
            goto label_7;
        }
        control = this.VisualChildren[index1];
        RowData rowData = (RowData) control.Content;
        CardViewPrintRowPanel.SetIsFirstCardInRow((DependencyObject) rowData, index1 == 0);
        control.Measure(availableSize);
        if ((control.DesiredSize.Width <= size1.Width || index1 <= 0) && index1 != num)
        {
          size1 = new Size(Math.Max(0.0, size1.Width - control.DesiredSize.Width), size1.Height);
          size2 = new Size(size2.Width, Math.Max(size2.Height, control.DesiredSize.Height));
          this.LastPrintRowData = rowData;
          ++index1;
        }
        else
          break;
      }
      this.LastPrintRowData = (RowData) this.VisualChildren[index1 - 1].Content;
      this.PutVisualChildInCache(control);
label_7:
      for (int index2 = this.VisualChildren.Count - index1; index2 > 0; --index2)
        this.PutVisualChildInCache(this.VisualChildren.Last<ContentControl>());
      return size2;
    }

    private Size GetRowIndentControlSize(Size availableSize)
    {
      if (this.RowIndentControl == null)
      {
        this.RowIndentControl = new ContentControl();
        this.RowIndentControl.ContentTemplate = this.RowIndentControlTemplate;
        this.AddVisualChild((Visual) this.RowIndentControl);
        this.AddLogicalChild((object) this.RowIndentControl);
      }
      this.RowIndentControl.Content = this.DataContext;
      this.RowIndentControl.Measure(availableSize);
      return this.RowIndentControl.DesiredSize;
    }

    protected override Size MeasureOverride(Size availableSize)
    {
      if (this.RowData.RowHandle.Value == this.lastRowHandle)
        return this.RemergeCurrentRow(availableSize);
      if (this.RowData.RowHandle.Value == this.afterGroupRowHandle)
        this.LastPrintRowData = (RowData) null;
      this.lastRowHandle = this.RowData.RowHandle.Value;
      Size indentControlSize = this.GetRowIndentControlSize(availableSize);
      Size size1 = new Size(Math.Max(0.0, availableSize.Width - indentControlSize.Width), availableSize.Height);
      availableSize = new Size(size1.Width, availableSize.Height);
      Size size2 = new Size(availableSize.Width, double.IsInfinity(availableSize.Height) ? 0.0 : availableSize.Height);
      int index1 = 0;
      int num = this.PrintMaximumCardColumns > 0 ? this.PrintMaximumCardColumns : int.MaxValue;
      ContentControl control;
      while (true)
      {
        RowData nextRowData = this.GetNextRowData();
        if (nextRowData != null)
        {
          CardViewPrintRowPanel.SetIsFirstCardInRow((DependencyObject) nextRowData, index1 == 0);
          if (index1 == this.VisualChildren.Count)
            this.AddVisualChildFromCache();
          control = this.VisualChildren[index1];
          control.Content = (object) nextRowData;
          control.Measure(availableSize);
          if ((control.DesiredSize.Width <= size1.Width || index1 <= 0) && index1 != num)
          {
            size1 = new Size(Math.Max(0.0, size1.Width - control.DesiredSize.Width), size1.Height);
            size2 = new Size(size2.Width, Math.Max(size2.Height, control.DesiredSize.Height));
            this.LastPrintRowData = nextRowData;
            ++index1;
          }
          else
            break;
        }
        else
          goto label_11;
      }
      this.PutVisualChildInCache(control);
label_11:
      for (int index2 = this.VisualChildren.Count - index1; index2 > 0; --index2)
        this.PutVisualChildInCache(this.VisualChildren.Last<ContentControl>());
      return size2;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      if (this.VisualChildren.Count == 0)
        return finalSize;
      this.RowIndentControl.Arrange(new Rect(0.0, 0.0, this.RowIndentControl.DesiredSize.Width, this.RowIndentControl.DesiredSize.Height));
      double width = this.RowIndentControl.DesiredSize.Width;
      foreach (ContentControl visualChild in this.VisualChildren)
      {
        visualChild.Arrange(new Rect(width, 0.0, visualChild.DesiredSize.Width, visualChild.DesiredSize.Height));
        width += visualChild.DesiredSize.Width;
      }
      return finalSize;
    }

    private void PutVisualChildInCache(ContentControl control)
    {
      this.VisualChildrenCache.Add(control);
      this.RemoveVisualChild((Visual) control);
      this.VisualChildren.Remove(control);
      this.RemoveLogicalChild((object) control);
      control.Content = (object) null;
    }

    private void AddVisualChildFromCache()
    {
      if (this.VisualChildrenCache.Count > 0)
      {
        ContentControl contentControl = this.VisualChildrenCache[0];
        this.VisualChildrenCache.RemoveAt(0);
        this.VisualChildren.Add(contentControl);
        this.AddVisualChild((Visual) contentControl);
        this.AddLogicalChild((object) contentControl);
      }
      else
      {
        ContentControl contentControl = new ContentControl();
        contentControl.ContentTemplate = this.ItemTemplate;
        this.VisualChildren.Add(contentControl);
        this.AddVisualChild((Visual) contentControl);
        this.AddLogicalChild((object) contentControl);
      }
    }
  }
}
