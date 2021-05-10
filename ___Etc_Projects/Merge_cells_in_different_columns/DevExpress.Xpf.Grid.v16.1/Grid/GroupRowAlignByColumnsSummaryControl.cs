// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupRowAlignByColumnsSummaryControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class GroupRowAlignByColumnsSummaryControl : CachedItemsControl, ILayoutNotificationHelperOwner
  {
    private bool actualUseDefaultItemTemplate = true;
    private bool useDefaultItemTemplateCore = true;
    private readonly FixedStyle fixedStyle;
    private IList<BandBase> bandsCore;
    private double leftIndentCore;
    private readonly LayoutNotificationHelper layoutNotificationHelper;
    private static readonly ControlTemplate OrdinarPanelTemplate;
    private static readonly ControlTemplate BandsPanelTemplate;
    private static readonly DataTemplate BandsItemTemplate;
    private static readonly DataTemplate OrdinarItemTemplate;
    private Thickness scrollingMarginCore;

    internal bool ActualUseDefaultItemTemplate
    {
      get
      {
        return this.actualUseDefaultItemTemplate;
      }
    }

    internal bool UseDefaultItemTemplate
    {
      get
      {
        return this.useDefaultItemTemplateCore;
      }
      set
      {
        if (this.useDefaultItemTemplateCore == value)
          return;
        this.useDefaultItemTemplateCore = value;
        this.OnItemTemplateChanged();
      }
    }

    private BandsGroupSummaryAlignByColumnsPanel BandsPanel
    {
      get
      {
        return this.Panel as BandsGroupSummaryAlignByColumnsPanel;
      }
    }

    internal IList<BandBase> Bands
    {
      get
      {
        return this.bandsCore;
      }
      set
      {
        if (this.bandsCore == value)
          return;
        this.bandsCore = value;
        this.OnBandsChanged();
      }
    }

    internal double LeftIndent
    {
      get
      {
        return this.leftIndentCore;
      }
      set
      {
        if (this.leftIndentCore == value)
          return;
        this.leftIndentCore = value;
        this.OnLeftIndentChanged();
      }
    }

    internal Thickness ScrollingMargin
    {
      get
      {
        return this.scrollingMarginCore;
      }
      set
      {
        if (!(this.scrollingMarginCore != value))
          return;
        this.scrollingMarginCore = value;
        this.UpdatePanelScrollingMargin();
      }
    }

    DependencyObject ILayoutNotificationHelperOwner.NotificationManager
    {
      get
      {
        GroupRowData groupRowData = this.DataContext as GroupRowData;
        if (groupRowData != null)
        {
          DataViewBase view = groupRowData.View;
          if (view != null)
            return (DependencyObject) view.DataControl;
        }
        return (DependencyObject) null;
      }
    }

    static GroupRowAlignByColumnsSummaryControl()
    {
      FrameworkElementFactory factory1 = new FrameworkElementFactory(typeof (StackVisibleIndexPanel));
      factory1.SetValue(OrderPanelBase.ArrangeAccordingToVisibleIndexProperty, (object) true);
      factory1.SetValue(OrderPanelBase.OrientationProperty, (object) Orientation.Horizontal);
      GroupRowAlignByColumnsSummaryControl.OrdinarPanelTemplate = GroupRowAlignByColumnsSummaryControl.CreateTemplate<ControlTemplate>(factory1, (Func<ControlTemplate>) (() => new ControlTemplate(typeof (ItemsControlBase))));
      GroupRowAlignByColumnsSummaryControl.BandsPanelTemplate = GroupRowAlignByColumnsSummaryControl.CreateTemplate<ControlTemplate>(new FrameworkElementFactory(typeof (BandsGroupSummaryAlignByColumnsPanel)), (Func<ControlTemplate>) (() => new ControlTemplate(typeof (ItemsControlBase))));
      FrameworkElementFactory factory2 = new FrameworkElementFactory(typeof (GroupBandSummaryControl));
      GroupRowAlignByColumnsSummaryControl.SetCustomItemTemplateBindings(factory2);
      factory2.SetBinding(GroupBandSummaryControl.HasTopElementProperty, (BindingBase) new Binding("Column.HasTopElement"));
      factory2.SetBinding(FrameworkElement.StyleProperty, (BindingBase) new Binding("View.GroupBandSummaryContentStyle"));
      GroupRowAlignByColumnsSummaryControl.BandsItemTemplate = GroupRowAlignByColumnsSummaryControl.CreateTemplate<DataTemplate>(factory2, (Func<DataTemplate>) (() => new DataTemplate()));
      FrameworkElementFactory factory3 = new FrameworkElementFactory(typeof (GroupColumnSummaryControl));
      GroupRowAlignByColumnsSummaryControl.SetCustomItemTemplateBindings(factory3);
      factory3.SetBinding(FrameworkElement.StyleProperty, (BindingBase) new Binding("View.GroupColumnSummaryContentStyle"));
      GroupRowAlignByColumnsSummaryControl.OrdinarItemTemplate = (DataTemplate) GroupRowAlignByColumnsSummaryControl.CreateTemplate<DefaultDataTemplate>(factory3, (Func<DefaultDataTemplate>) (() => new DefaultDataTemplate()));
      ItemsControlBase.ItemsPanelProperty.OverrideMetadata(typeof (GroupRowAlignByColumnsSummaryControl), new PropertyMetadata((object) GroupRowAlignByColumnsSummaryControl.OrdinarPanelTemplate));
      ItemsControlBase.ItemTemplateProperty.OverrideMetadata(typeof (GroupRowAlignByColumnsSummaryControl), new PropertyMetadata((object) GroupRowAlignByColumnsSummaryControl.OrdinarItemTemplate));
    }

    public GroupRowAlignByColumnsSummaryControl(FixedStyle fixedStyle)
    {
      this.fixedStyle = fixedStyle;
      this.layoutNotificationHelper = new LayoutNotificationHelper((ILayoutNotificationHelperOwner) this);
    }

    protected override Size MeasureOverride(Size constraint)
    {
      this.layoutNotificationHelper.Subscribe();
      return base.MeasureOverride(constraint);
    }

    private static void SetCustomItemTemplateBindings(FrameworkElementFactory factory)
    {
      factory.SetBinding(GroupColumnSummaryControl.IsReadyProperty, (BindingBase) new Binding("GroupRowData.IsReady"));
      factory.SetBinding(GroupColumnSummaryControl.IsGroupRowFocusedProperty, (BindingBase) new Binding("GroupRowData.IsFocused"));
    }

    private static T CreateTemplate<T>(FrameworkElementFactory factory, Func<T> creator) where T : FrameworkTemplate
    {
      T obj = creator();
      obj.VisualTree = factory;
      obj.Seal();
      return obj;
    }

    protected override FrameworkElement CreateChild(object item)
    {
      if (this.actualUseDefaultItemTemplate)
        return (FrameworkElement) new GroupRowColumnSummaryControl();
      return base.CreateChild(item);
    }

    protected override void ValidateElement(FrameworkElement element, object item)
    {
      base.ValidateElement(element, item);
      if (!this.actualUseDefaultItemTemplate)
        return;
      GroupRowColumnSummaryControl columnSummaryControl = (GroupRowColumnSummaryControl) element;
      columnSummaryControl.ColumnData = (GridGroupSummaryColumnData) item;
      columnSummaryControl.SyncWithColumnData();
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.UpdateBandsPanel();
      this.UpdatePanelScrollingMargin();
    }

    internal void UpdatePanel(bool hasBands)
    {
      if (hasBands)
      {
        this.ItemTemplate = GroupRowAlignByColumnsSummaryControl.BandsItemTemplate;
        this.Template = GroupRowAlignByColumnsSummaryControl.BandsPanelTemplate;
      }
      else
      {
        this.ItemTemplate = GroupRowAlignByColumnsSummaryControl.OrdinarItemTemplate;
        this.Template = GroupRowAlignByColumnsSummaryControl.OrdinarPanelTemplate;
      }
    }

    private void UpdateBandsPanel()
    {
      if (this.BandsPanel == null)
        return;
      this.BandsPanel.Fixed = this.fixedStyle;
      this.OnBandsChanged();
      this.OnLeftIndentChanged();
    }

    private void OnBandsChanged()
    {
      if (this.BandsPanel == null)
        return;
      this.BandsPanel.Bands = this.Bands;
    }

    private void OnLeftIndentChanged()
    {
      if (this.BandsPanel == null)
        return;
      this.BandsPanel.LeftMargin = -this.LeftIndent;
    }

    protected override void OnItemTemplateChanged()
    {
      this.actualUseDefaultItemTemplate = this.UseDefaultItemTemplate && (this.ItemTemplate == null || this.ItemTemplate is DefaultDataTemplate);
      base.OnItemTemplateChanged();
    }

    private void UpdatePanelScrollingMargin()
    {
      if (this.Panel == null)
        return;
      this.Panel.Margin = this.ScrollingMargin;
    }

    void ILayoutNotificationHelperOwner.InvalidateMeasure()
    {
      this.InvalidateMeasure();
    }
  }
}
