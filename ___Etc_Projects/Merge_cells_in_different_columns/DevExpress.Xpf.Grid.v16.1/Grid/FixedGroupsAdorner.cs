// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.FixedGroupsAdorner
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DevExpress.Xpf.Grid
{
  public class FixedGroupsAdorner : ContentControl
  {
    public static readonly DependencyProperty FixedElementsProperty = DependencyPropertyManager.Register("FixedElements", typeof (IList<FrameworkElement>), typeof (FixedGroupsAdorner), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((FixedGroupsAdorner) d).OnChanged())));
    public static readonly DependencyProperty ParentControlProperty = DependencyPropertyManager.Register("ParentControl", typeof (FrameworkElement), typeof (FixedGroupsAdorner), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((FixedGroupsAdorner) d).OnChanged())));
    public static readonly DependencyProperty AdornerBrushProperty = DependencyPropertyManager.Register("AdornerBrush", typeof (Brush), typeof (FixedGroupsAdorner), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((FixedGroupsAdorner) d).OnChanged())));
    public static readonly DependencyProperty AdornerHeightProperty = DependencyPropertyManager.Register("AdornerHeight", typeof (double), typeof (FixedGroupsAdorner), new PropertyMetadata((object) 0.0, (PropertyChangedCallback) ((d, e) => ((FixedGroupsAdorner) d).OnChanged())));
    public static readonly DependencyProperty DrawAdornerUnderWholeGroupProperty = DependencyPropertyManager.Register("DrawAdornerUnderWholeGroup", typeof (bool), typeof (FixedGroupsAdorner), new PropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((FixedGroupsAdorner) d).OnChanged())));
    private bool queued;

    public IList<FrameworkElement> FixedElements
    {
      get
      {
        return (IList<FrameworkElement>) this.GetValue(FixedGroupsAdorner.FixedElementsProperty);
      }
      set
      {
        this.SetValue(FixedGroupsAdorner.FixedElementsProperty, (object) value);
      }
    }

    public FrameworkElement ParentControl
    {
      get
      {
        return (FrameworkElement) this.GetValue(FixedGroupsAdorner.ParentControlProperty);
      }
      set
      {
        this.SetValue(FixedGroupsAdorner.ParentControlProperty, (object) value);
      }
    }

    public Brush AdornerBrush
    {
      get
      {
        return (Brush) this.GetValue(FixedGroupsAdorner.AdornerBrushProperty);
      }
      set
      {
        this.SetValue(FixedGroupsAdorner.AdornerBrushProperty, (object) value);
      }
    }

    public double AdornerHeight
    {
      get
      {
        return (double) this.GetValue(FixedGroupsAdorner.AdornerHeightProperty);
      }
      set
      {
        this.SetValue(FixedGroupsAdorner.AdornerHeightProperty, (object) value);
      }
    }

    public bool DrawAdornerUnderWholeGroup
    {
      get
      {
        return (bool) this.GetValue(FixedGroupsAdorner.DrawAdornerUnderWholeGroupProperty);
      }
      set
      {
        this.SetValue(FixedGroupsAdorner.DrawAdornerUnderWholeGroupProperty, (object) value);
      }
    }

    private Panel Panel
    {
      get
      {
        return (Panel) this.Content;
      }
    }

    public FixedGroupsAdorner()
    {
      this.SetDefaultStyleKey(typeof (FixedGroupsAdorner));
      this.IsHitTestVisible = false;
      System.Windows.Controls.Grid grid = new System.Windows.Controls.Grid();
      grid.IsHitTestVisible = false;
      this.Content = (object) grid;
    }

    [Browsable(false)]
    public bool ShouldSerializeFixedElements(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    private void OnChanged()
    {
      if (this.queued || this.ParentControl == null)
        return;
      DataViewBase parentObject = LayoutHelper.FindParentObject<DataViewBase>((DependencyObject) this.ParentControl);
      if (parentObject == null || parentObject.DataPresenter == null)
        return;
      this.queued = true;
      parentObject.EnqueueImmediateAction((Action) (() =>
      {
        if (this.FixedElements == null || this.ParentControl == null)
          return;
        this.queued = false;
        this.Panel.Children.Clear();
        for (int index = 0; index < this.FixedElements.Count; ++index)
        {
          Rect relativeElementRect = LayoutHelper.GetRelativeElementRect((UIElement) this.FixedElements[index], (UIElement) this.ParentControl);
          Path path = new Path() { Stroke = this.AdornerBrush, StrokeThickness = this.AdornerHeight, IsHitTestVisible = false };
          double y = relativeElementRect.Bottom + this.AdornerHeight / 2.0;
          IFixedGroupElement fixedGroupElement = this.FixedElements[index] as IFixedGroupElement;
          double num1 = fixedGroupElement != null ? fixedGroupElement.GetLeftMargin(this.DrawAdornerUnderWholeGroup) : 0.0;
          double num2 = fixedGroupElement != null ? fixedGroupElement.GetRightMargin(this.DrawAdornerUnderWholeGroup) : 0.0;
          path.Data = (Geometry) new LineGeometry()
          {
            StartPoint = new Point(relativeElementRect.Left + num1, y),
            EndPoint = new Point(relativeElementRect.Right - num2, y)
          };
          path.Width = relativeElementRect.Width;
          this.Panel.Children.Add((UIElement) path);
        }
      }));
    }
  }
}
