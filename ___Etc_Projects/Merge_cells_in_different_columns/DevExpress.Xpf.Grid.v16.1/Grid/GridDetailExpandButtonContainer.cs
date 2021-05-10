// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridDetailExpandButtonContainer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class GridDetailExpandButtonContainer : ContentControl, INotifyVisibilityChanged
  {
    public static readonly DependencyProperty IsDetailButtonVisibleProperty = DependencyPropertyManager.RegisterAttached("IsDetailButtonVisible", typeof (bool), typeof (GridDetailExpandButtonContainer), (PropertyMetadata) new FrameworkPropertyMetadata((object) true, new PropertyChangedCallback(GridDetailExpandButtonContainer.OnIsDetailButtonVisibleChanged)));
    public static readonly DependencyProperty IsDetailButtonVisibleBindingContainerProperty = DependencyPropertyManager.Register("IsDetailButtonVisibleBindingContainer", typeof (BindingContainer), typeof (GridDetailExpandButtonContainer), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridDetailExpandButtonContainer) d).UpdateExpandButtonVisibilityBinding())));
    public static readonly DependencyProperty IsMasterRowExpandedProperty = DependencyPropertyManager.Register("IsMasterRowExpanded", typeof (bool), typeof (GridDetailExpandButtonContainer), new PropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((GridDetailExpandButtonContainer) d).UpdateMargin())));
    public static readonly DependencyProperty RowPositionProperty = DependencyPropertyManager.Register("RowPosition", typeof (RowPosition), typeof (GridDetailExpandButtonContainer), new PropertyMetadata((object) RowPosition.Single, (PropertyChangedCallback) ((d, e) => ((GridDetailExpandButtonContainer) d).UpdateMargin())));
    public static readonly DependencyProperty ShowHorizontalLinesProperty = DependencyPropertyManager.Register("ShowHorizontalLines", typeof (bool), typeof (GridDetailExpandButtonContainer), new PropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((GridDetailExpandButtonContainer) d).UpdateMargin())));
    public static readonly DependencyProperty RowHandleProperty = DependencyPropertyManager.Register("RowHandle", typeof (int), typeof (GridDetailExpandButtonContainer), new PropertyMetadata((object) int.MinValue, (PropertyChangedCallback) ((d, e) => ((GridDetailExpandButtonContainer) d).OnRowHandleChanged())));
    private GridDetailExpandButton button;

    public BindingContainer IsDetailButtonVisibleBindingContainer
    {
      get
      {
        return (BindingContainer) this.GetValue(GridDetailExpandButtonContainer.IsDetailButtonVisibleBindingContainerProperty);
      }
      set
      {
        this.SetValue(GridDetailExpandButtonContainer.IsDetailButtonVisibleBindingContainerProperty, (object) value);
      }
    }

    public bool IsMasterRowExpanded
    {
      get
      {
        return (bool) this.GetValue(GridDetailExpandButtonContainer.IsMasterRowExpandedProperty);
      }
      set
      {
        this.SetValue(GridDetailExpandButtonContainer.IsMasterRowExpandedProperty, (object) value);
      }
    }

    public RowPosition RowPosition
    {
      get
      {
        return (RowPosition) this.GetValue(GridDetailExpandButtonContainer.RowPositionProperty);
      }
      set
      {
        this.SetValue(GridDetailExpandButtonContainer.RowPositionProperty, (object) value);
      }
    }

    public bool ShowHorizontalLines
    {
      get
      {
        return (bool) this.GetValue(GridDetailExpandButtonContainer.ShowHorizontalLinesProperty);
      }
      set
      {
        this.SetValue(GridDetailExpandButtonContainer.ShowHorizontalLinesProperty, (object) value);
      }
    }

    public int RowHandle
    {
      get
      {
        return (int) this.GetValue(GridDetailExpandButtonContainer.RowHandleProperty);
      }
      set
      {
        this.SetValue(GridDetailExpandButtonContainer.RowHandleProperty, (object) value);
      }
    }

    public GridDetailExpandButtonContainer()
    {
      ControlExtensions.SetDefaultStyleKey(this, typeof (GridDetailExpandButtonContainer));
    }

    private static void OnIsDetailButtonVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      GridDetailExpandButtonContainer expandButtonContainer = d as GridDetailExpandButtonContainer;
      if (expandButtonContainer == null)
        return;
      expandButtonContainer.OnIsDetailButtonVisibleChangedCore();
    }

    public static bool GetIsDetailButtonVisible(DependencyObject d)
    {
      if (d == null)
        return true;
      return (bool) d.GetValue(GridDetailExpandButtonContainer.IsDetailButtonVisibleProperty);
    }

    public static void SetIsDetailButtonVisible(DependencyObject d, bool value)
    {
      if (d == null)
        return;
      d.SetValue(GridDetailExpandButtonContainer.IsDetailButtonVisibleProperty, (object) value);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.button = this.GetTemplateChild("PART_ExpandButton") as GridDetailExpandButton;
      this.UpdateBindings();
      this.UpdateExpandButtonVisibilityBinding();
      this.UpdateMargin();
    }

    private void OnIsDetailButtonVisibleChangedCore()
    {
      if (this.button == null)
        return;
      this.button.IsHitTestVisible = GridDetailExpandButtonContainer.GetIsDetailButtonVisible((DependencyObject) this);
    }

    private void UpdateExpandButtonVisibilityBinding()
    {
      if (this.RowHandle != -2147483647)
      {
        if (this.IsDetailButtonVisibleBindingContainer != null)
          BindingOperations.SetBinding((DependencyObject) this, GridDetailExpandButtonContainer.IsDetailButtonVisibleProperty, this.IsDetailButtonVisibleBindingContainer.Binding);
        else
          this.ClearValue(GridDetailExpandButtonContainer.IsDetailButtonVisibleProperty);
      }
      else
        GridDetailExpandButtonContainer.SetIsDetailButtonVisible((DependencyObject) this, false);
    }

    private void UpdateBindings()
    {
      if (this.Visibility == Visibility.Visible)
      {
        BindingOperations.SetBinding((DependencyObject) this, GridDetailExpandButtonContainer.IsDetailButtonVisibleBindingContainerProperty, (BindingBase) new Binding("View." + TableView.IsDetailButtonVisibleBindingContainerProperty.GetName()));
        BindingOperations.SetBinding((DependencyObject) this, FrameworkElement.WidthProperty, (BindingBase) new Binding("View." + TableView.ActualExpandDetailButtonWidthProperty.GetName()));
        BindingOperations.SetBinding((DependencyObject) this, GridDetailExpandButtonContainer.ShowHorizontalLinesProperty, (BindingBase) new Binding("View." + TableView.ShowHorizontalLinesProperty.GetName()));
        BindingOperations.SetBinding((DependencyObject) this, GridDetailExpandButtonContainer.RowHandleProperty, (BindingBase) new Binding("RowHandle.Value"));
      }
      else
      {
        this.ClearValue(GridDetailExpandButtonContainer.IsDetailButtonVisibleBindingContainerProperty);
        this.ClearValue(FrameworkElement.WidthProperty);
        this.ClearValue(GridDetailExpandButtonContainer.ShowHorizontalLinesProperty);
        this.ClearValue(GridDetailExpandButtonContainer.RowHandleProperty);
      }
    }

    void INotifyVisibilityChanged.OnVisibilityChanged()
    {
      this.UpdateBindings();
    }

    private void UpdateMargin()
    {
      if (this.ShowHorizontalLines && (this.RowPosition == RowPosition.Bottom || this.RowPosition == RowPosition.Single) && !this.IsMasterRowExpanded)
        this.Margin = new Thickness(0.0, 0.0, 0.0, 1.0);
      else
        this.Margin = new Thickness(0.0);
    }

    private void OnRowHandleChanged()
    {
      this.UpdateExpandButtonVisibilityBinding();
    }
  }
}
