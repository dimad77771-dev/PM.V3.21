// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupGridControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid
{
  public abstract class GroupGridControl : ContentControl, IGroupRow
  {
    private FrameworkElement HeaderContentPresenter { get; set; }

    internal RowsContainer LogicalItemsContainer
    {
      get
      {
        return ((RowDataBase) this.DataContext).RowsContainer;
      }
    }

    protected GroupRowData GroupRowData
    {
      get
      {
        return this.DataContext as GroupRowData;
      }
    }

    protected DataViewBase View
    {
      get
      {
        if (this.GroupRowData == null)
          return (DataViewBase) null;
        return this.GroupRowData.View;
      }
    }

    protected bool IsExpanded
    {
      get
      {
        return ExpandHelper.GetIsExpanded((DependencyObject) this);
      }
    }

    FrameworkElement IGroupRow.RowElement
    {
      get
      {
        return this.HeaderContentPresenter;
      }
    }

    static GroupGridControl()
    {
      DataControlPopupMenu.GridMenuTypeProperty.OverrideMetadata(typeof (GroupGridControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) GridMenuType.GroupRow));
    }

    protected GroupGridControl()
    {
      RowData.SetRowHandleBinding((FrameworkElement) this);
    }

    protected override void OnPreviewMouseRightButtonDown(MouseButtonEventArgs e)
    {
      base.OnPreviewMouseRightButtonDown(e);
      this.AssignContextMenu();
    }

    internal void AssignContextMenu()
    {
      if (this.View == null)
        return;
      BarManager.SetDXContextMenu((UIElement) this, (IPopupControl) this.View.DataControlMenu);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.HeaderContentPresenter = this.GetTemplateChild("PART_HeaderContentPresenter") as FrameworkElement;
      if (this.HeaderContentPresenter != null)
      {
        ExpandHelper.SetRowContainer((DependencyObject) this.HeaderContentPresenter, (FrameworkElement) this);
        ExpandHelper.SetItemsContainer((DependencyObject) this.HeaderContentPresenter, (FrameworkElement) this.GetTemplateChild("PART_ItemsContainer"));
        this.AddHandler(ExpandHelper.IsExpandedChangedEvent, (Delegate) ((d, e) => this.OnIsExpandedChanged()));
      }
      RowData.ReassignCurrentRowData((DependencyObject) this, (DependencyObject) this.HeaderContentPresenter);
      this.SetBinding(ExpandHelper.IsExpandedProperty, (BindingBase) new Binding("IsRowExpanded"));
      this.OnIsExpandedChanged();
    }

    protected virtual void OnIsExpandedChanged()
    {
      this.LogicalItemsContainer.AnimationProgress = this.IsExpanded ? 1.0 : 0.0;
    }

    protected override Size MeasureOverride(Size constraint)
    {
      return MeasurePixelSnapperHelper.MeasureOverride(base.MeasureOverride(constraint), SnapperType.Around);
    }
  }
}
