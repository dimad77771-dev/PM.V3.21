// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.FilterPanelControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors.Filtering;
using DevExpress.Xpf.Grid.HitTest;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace DevExpress.Xpf.Grid
{
  public class FilterPanelControl : FilterPanelControlBase
  {
    public FilterPanelControl()
    {
      this.SetBinding(FilterPanelControlBase.ClearFilterCommandProperty, (BindingBase) new Binding("Commands.ClearFilter"));
      this.SetBinding(FilterPanelControlBase.ShowFilterEditorCommandProperty, (BindingBase) new Binding("Commands.ShowFilterEditor"));
      this.SetBinding(FilterPanelControlBase.IsFilterEnabledProperty, (BindingBase) new Binding("DataControl.IsFilterEnabled")
      {
        Mode = BindingMode.TwoWay
      });
      this.SetBinding(FilterPanelControlBase.AllowFilterEditorProperty, (BindingBase) new Binding("ShowEditFilterButton"));
      this.SetBinding(FilterPanelControlBase.IsCanEnableFilterProperty, (BindingBase) new Binding("DataControl.FilterCriteria")
      {
        Converter = (IValueConverter) new ObjectToBooleanConverter()
      });
      this.SetBinding(FilterPanelControlBase.MRUFiltersProperty, (BindingBase) new Binding("DataControl.MRUFilters"));
      this.SetBinding(FilterPanelControlBase.ActiveFilterInfoProperty, (BindingBase) new Binding("DataControl.ActiveFilterInfo"));
      this.SetBinding(FilterPanelControlBase.AllowMRUFilterListProperty, (BindingBase) new Binding("DataControl.AllowMRUFilterList"));
      GridViewHitInfoBase.SetHitTestAcceptor((DependencyObject) this, (DataViewHitTestAcceptorBase) new FilterPanelTableViewHitTestAcceptor());
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      GridViewHitInfoBase.SetHitTestAcceptorSafe(this.GetTemplateChild("PART_FilterPanelCloseButton"), (DataViewHitTestAcceptorBase) new FilterPanelCloseButtonTableViewHitTestAcceptor());
      GridViewHitInfoBase.SetHitTestAcceptorSafe(this.GetTemplateChild("PART_FilterControlButton"), (DataViewHitTestAcceptorBase) new FilterPanelCustomizeButtonTableViewHitTestAcceptor());
      GridViewHitInfoBase.SetHitTestAcceptorSafe(this.GetTemplateChild("PART_FilterPanelIsActiveButton"), (DataViewHitTestAcceptorBase) new FilterPanelActiveButtonTableViewHitTestAcceptor());
      GridViewHitInfoBase.SetHitTestAcceptorSafe(this.GetTemplateChild("PART_FilterPanelText"), (DataViewHitTestAcceptorBase) new FilterPanelTextTableViewHitTestAcceptor());
      GridViewHitInfoBase.SetHitTestAcceptorSafe(this.GetTemplateChild("PART_FilterPanelMRUComboBox"), (DataViewHitTestAcceptorBase) new MRUFilterListComboBoxHitTestAcceptor());
    }

    protected override void FilterPanelMRUComboBoxSelectedIndexChanged(object sender, RoutedEventArgs args)
    {
      CriteriaOperatorInfo selectedFilter = this.GetSelectedFilter();
      if (selectedFilter == null)
        return;
      DataViewBase dataViewBase = this.DataContext as DataViewBase;
      if (dataViewBase == null || dataViewBase.DataControl == null)
        return;
      dataViewBase.DataControl.FilterCriteria = selectedFilter.FilterOperator;
    }

    [Browsable(false)]
    public bool ShouldSerializeMRUFilters(XamlDesignerSerializationManager manager)
    {
      return false;
    }
  }
}
