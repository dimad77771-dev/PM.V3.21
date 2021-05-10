// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DateColumnFilterInfoBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data.Filtering;
using DevExpress.Data.Helpers;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid.Filtering;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Markup;

namespace DevExpress.Xpf.Grid
{
  public abstract class DateColumnFilterInfoBase : ColumnFilterInfoBase, ICalendarFilterOwner
  {
    protected DataControlBase Grid
    {
      get
      {
        return this.View.DataControl;
      }
    }

    protected string FieldName
    {
      get
      {
        return this.Column.FieldName;
      }
    }

    protected bool IsFiltered
    {
      get
      {
        return !object.ReferenceEquals((object) this.ColumnCriteria, (object) null);
      }
    }

    protected CriteriaOperator ColumnCriteria
    {
      get
      {
        return this.Grid.GetColumnFilterCriteria(this.Column);
      }
    }

    private FilterData[] UpperFilters { get; set; }

    private FilterData[] BottomFilters { get; set; }

    private IEnumerable<FilterData> Filters
    {
      get
      {
        return ((IEnumerable<FilterData>) (this.UpperFilters ?? new FilterData[0])).Concat<FilterData>((IEnumerable<FilterData>) (this.BottomFilters ?? new FilterData[0])).Distinct<FilterData>((IEqualityComparer<FilterData>) new AnonymousEqualityComparer<FilterData>((Func<FilterData, FilterData, bool>) ((x, y) => x.FilterType == y.FilterType), (Func<FilterData, int>) (x => x.GetHashCode())));
      }
    }

    protected bool CalendarFilterExists
    {
      get
      {
        return this.CalendarFilter != null;
      }
    }

    protected ICalendarFilter CalendarFilter { get; private set; }

    protected override bool ImmediateUpdateFilter
    {
      get
      {
        return this.Column.ImmediateUpdateColumnFilter;
      }
    }

    FilterData[] ICalendarFilterOwner.UpperFilters
    {
      get
      {
        return this.UpperFilters;
      }
    }

    FilterData[] ICalendarFilterOwner.BottomFilters
    {
      get
      {
        return this.BottomFilters;
      }
    }

    public DateColumnFilterInfoBase(ColumnBase column)
      : base(column)
    {
    }

    protected abstract FilterData[] CreateUpperFilters();

    protected abstract FilterData[] CreateBottomFilters();

    internal override PopupBaseEdit CreateColumnFilterPopup()
    {
      PopupBaseEdit popupBaseEdit = new PopupBaseEdit();
      popupBaseEdit.ShowNullText = false;
      popupBaseEdit.IsTextEditable = new bool?(false);
      popupBaseEdit.DataContext = (object) this;
      popupBaseEdit.PopupContentTemplate = this.CreatePopupTemplate();
      return popupBaseEdit;
    }

    private ControlTemplate CreatePopupTemplate()
    {
      return XamlReader.Parse("<ControlTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:dxg=\"http://schemas.devexpress.com/winfx/2008/xaml/grid\"><dxg:CalendarFilterControl Owner=\"{Binding DataContext, RelativeSource={RelativeSource TemplatedParent}}\"/></ControlTemplate>") as ControlTemplate;
    }

    protected override void UpdatePopupData(PopupBaseEdit popup)
    {
      if (this.UpperFilters == null)
        this.UpdateUpperFilters();
      if (this.BottomFilters != null)
        return;
      this.UpdateBottomFilters();
    }

    protected virtual void UpdateUpperFilters()
    {
      this.UpperFilters = this.CreateUpperFilters();
    }

    protected virtual void UpdateBottomFilters()
    {
      this.BottomFilters = this.CreateBottomFilters();
    }

    protected override void UpdateSizeGripVisibility(PopupBaseEdit popup)
    {
    }

    protected override void OnPopupOpenedCore(PopupBaseEdit popup)
    {
      this.UpdateShowAllCheckState();
      this.UpdateFilterBySpecificDateCheckState();
      this.UpdateSelectedDates();
      this.UpdateFiltersCheckState();
    }

    protected virtual void UpdateShowAllCheckState()
    {
      if (!this.CalendarFilterExists)
        return;
      if (!this.IsFiltered)
        this.CalendarFilter.CheckShowAll();
      else
        this.CalendarFilter.UncheckShowAll();
    }

    protected virtual void UpdateFilterBySpecificDateCheckState()
    {
      if (!this.CalendarFilterExists)
        return;
      if (!this.IsFiltered)
      {
        this.CalendarFilter.UncheckFilterBySpecificDate();
      }
      else
      {
        if (this.ColumnCriteria.ToDates(this.FieldName).Length == 0)
          return;
        this.CalendarFilter.CheckFilterBySpecificDate();
      }
    }

    protected virtual void UpdateSelectedDates()
    {
      if (!this.CalendarFilterExists)
        return;
      if (!this.IsFiltered)
        this.CalendarFilter.SelectedDates = (IList<DateTime>) null;
      else
        this.CalendarFilter.SelectedDates = (IList<DateTime>) this.ColumnCriteria.ToDates(this.FieldName);
    }

    protected virtual void UpdateFiltersCheckState()
    {
      if (!this.CalendarFilterExists || !this.IsFiltered)
        return;
      this.CalendarFilter.CheckFilters(this.MapToFilterData(this.ColumnCriteria.ToFilters(this.FieldName)));
    }

    private FilterData[] MapToFilterData(FilterDateType[] types)
    {
      Dictionary<FilterDateType, FilterData> map = this.Filters.ToDictionary<FilterData, FilterDateType>((Func<FilterData, FilterDateType>) (pair => pair.FilterType));
      return ((IEnumerable<FilterDateType>) types).Where<FilterDateType>((Func<FilterDateType, bool>) (x => map.ContainsKey(x))).Select<FilterDateType, FilterData>((Func<FilterDateType, FilterData>) (x => map[x])).ToArray<FilterData>();
    }

    protected override CriteriaOperator GetFilterCriteria(PopupBaseEdit popup)
    {
      return this.GetCriteria();
    }

    protected override void ClearPopupData(PopupBaseEdit popup)
    {
      if (!this.CalendarFilterExists)
        return;
      this.CalendarFilter.Dispose();
      this.CalendarFilter = (ICalendarFilter) null;
    }

    private void OnShowAll()
    {
      if (!this.CalendarFilterExists)
        return;
      this.CalendarFilter.CheckShowAll();
      this.CalendarFilter.UncheckFilterBySpecificDate();
      this.CalendarFilter.UncheckAllFilters();
      this.ClearColumnFilter();
    }

    private void OnFilterBySpecificDateChecked()
    {
      if (!this.CalendarFilterExists)
        return;
      this.CalendarFilter.UncheckShowAll();
      this.CalendarFilter.CheckFilterBySpecificDate();
      this.CalendarFilter.UncheckAllFilters();
      if (this.CalendarFilter.HasSelectedDates)
        this.FilterBySelectedDates();
      else
        this.FilterByFocusedDate();
    }

    private void OnFilterBySpecificDateUnchecked()
    {
      if (!this.CalendarFilterExists)
        return;
      this.CalendarFilter.CheckShowAll();
      this.CalendarFilter.UncheckFilterBySpecificDate();
      this.CalendarFilter.UncheckAllFilters();
      this.ClearColumnFilter();
    }

    private void OnSelectedDatesChanged()
    {
      if (!this.CalendarFilterExists)
        return;
      this.CalendarFilter.UncheckShowAll();
      this.CalendarFilter.CheckFilterBySpecificDate();
      this.CalendarFilter.UncheckAllFilters();
      this.FilterBySelectedDates();
    }

    private void OnFilterChecked(FilterData filter)
    {
      if (!this.CalendarFilterExists)
        return;
      this.CalendarFilter.UncheckShowAll();
      this.CalendarFilter.UncheckFilterBySpecificDate();
      this.CalendarFilter.SelectedDates = (IList<DateTime>) null;
      this.CalendarFilter.CheckFilters(filter);
      this.SetColumnFilter();
    }

    private void OnFilterUnchecked(FilterData filter)
    {
      if (!this.CalendarFilterExists)
        return;
      this.CalendarFilter.UncheckShowAll();
      this.CalendarFilter.UncheckFilterBySpecificDate();
      this.CalendarFilter.UncheckFilters(filter);
      if (!this.CalendarFilter.HasCheckedFilters)
        this.CalendarFilter.CheckShowAll();
      this.SetColumnFilter();
    }

    private void SetColumnFilter()
    {
      this.UpdateColumnFilterIfNeeded((Func<CriteriaOperator>) (() => this.GetCriteria()));
    }

    private void ClearColumnFilter()
    {
      this.UpdateColumnFilterIfNeeded((Func<CriteriaOperator>) (() => (CriteriaOperator) null));
    }

    private void FilterByFocusedDate()
    {
      this.UpdateColumnFilterIfNeeded((Func<CriteriaOperator>) (() => this.GetCriteriaForFocusedDate()));
    }

    private void FilterBySelectedDates()
    {
      this.UpdateColumnFilterIfNeeded((Func<CriteriaOperator>) (() => this.GetCriteriaForSelectedDates()));
    }

    private CriteriaOperator GetCriteriaForFocusedDate()
    {
      if (!this.CalendarFilterExists)
        return (CriteriaOperator) null;
      return this.DatesToCriteria((IEnumerable<DateTime>) new DateTime[1]{ this.CalendarFilter.FocusedDate });
    }

    private CriteriaOperator GetCriteriaForSelectedDates()
    {
      if (!this.CalendarFilterExists || this.CalendarFilter.SelectedDates == null)
        return (CriteriaOperator) null;
      return this.DatesToCriteria((IEnumerable<DateTime>) this.CalendarFilter.SelectedDates);
    }

    private CriteriaOperator DatesToCriteria(IEnumerable<DateTime> dates)
    {
      return MultiselectRoundedDateTimeFilterHelper.DatesToCriteria(this.FieldName, dates);
    }

    private CriteriaOperator GetCriteria()
    {
      if (!this.CalendarFilterExists)
        return (CriteriaOperator) null;
      if (this.CalendarFilter.IsShowAllChecked)
        return (CriteriaOperator) null;
      FilterData[] checkedFilters = this.CalendarFilter.CheckedFilters;
      if (checkedFilters.Length == 0)
        return this.GetCriteriaForSelectedDates();
      return ((IEnumerable<FilterData>) checkedFilters).Select<FilterData, FilterDateType>((Func<FilterData, FilterDateType>) (f => f.FilterType)).ToCriteria(this.FieldName);
    }

    void ICalendarFilterOwner.SetCalendarFilter(ICalendarFilter calendar)
    {
      this.CalendarFilter = calendar;
    }

    void ICalendarFilterOwner.OnShowAll()
    {
      this.OnShowAll();
    }

    void ICalendarFilterOwner.OnFilterBySpecificDateChecked()
    {
      this.OnFilterBySpecificDateChecked();
    }

    void ICalendarFilterOwner.OnFilterBySpecificDateUnchecked()
    {
      this.OnFilterBySpecificDateUnchecked();
    }

    void ICalendarFilterOwner.OnFilterChecked(FilterData filter)
    {
      this.OnFilterChecked(filter);
    }

    void ICalendarFilterOwner.OnFilterUnchecked(FilterData filter)
    {
      this.OnFilterUnchecked(filter);
    }

    void ICalendarFilterOwner.OnSelectedDatesChanged()
    {
      this.OnSelectedDatesChanged();
    }
  }
}
