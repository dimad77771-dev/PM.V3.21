// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CalendarFilterControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm.Native;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  [DXBrowsable(false)]
  [DXToolboxBrowsable(false)]
  public class CalendarFilterControl : ContentControl, ICalendarFilter, IDisposable
  {
    public static readonly DependencyProperty OwnerProperty = DependencyProperty.Register("Owner", typeof (ICalendarFilterOwner), typeof (CalendarFilterControl), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => (d as CalendarFilterControl).OnOwnerChanged())));
    public static readonly DependencyProperty DateFilterStyleProperty = DependencyProperty.Register("DateFilterStyle", typeof (Style), typeof (CalendarFilterControl), new PropertyMetadata((PropertyChangedCallback) null));
    private readonly Locker eventLocker = new Locker();
    public const string ClassName = "CalendarFilterControl";
    private CheckBox cbShowAll;
    private CheckBox cbFilterBySpecificDate;
    private DevExpress.Xpf.Editors.DateNavigator.DateNavigator dateNavigator;
    private Panel upperFiltersPanel;
    private Panel bottomFiltersPanel;

    public ICalendarFilterOwner Owner
    {
      get
      {
        return (ICalendarFilterOwner) this.GetValue(CalendarFilterControl.OwnerProperty);
      }
      set
      {
        this.SetValue(CalendarFilterControl.OwnerProperty, (object) value);
      }
    }

    public Style DateFilterStyle
    {
      get
      {
        return (Style) this.GetValue(CalendarFilterControl.DateFilterStyleProperty);
      }
      set
      {
        this.SetValue(CalendarFilterControl.DateFilterStyleProperty, (object) value);
      }
    }

    public DateTime FocusedDate
    {
      get
      {
        if (this.dateNavigator == null)
          return DateTime.Now.Date;
        return this.dateNavigator.FocusedDate;
      }
    }

    public IList<DateTime> SelectedDates
    {
      get
      {
        if (this.dateNavigator == null)
          return (IList<DateTime>) null;
        return this.dateNavigator.SelectedDates;
      }
      set
      {
        if (this.dateNavigator == null)
          return;
        if (value == null || value.Count == 0)
        {
          if (!this.HasSelectedDates)
            return;
          this.dateNavigator.SelectedDates = (IList<DateTime>) null;
        }
        else
          this.Locked((Action) (() => this.dateNavigator.SelectedDates = value));
      }
    }

    private FilterData[] UpperFiltersData
    {
      get
      {
        if (this.Owner == null)
          return new FilterData[0];
        return this.Owner.UpperFilters;
      }
    }

    private FilterData[] BottomFiltersData
    {
      get
      {
        if (this.Owner == null)
          return new FilterData[0];
        return this.Owner.BottomFilters;
      }
    }

    public bool HasCheckedFilters
    {
      get
      {
        return this.CheckedFilterElements.Any<CheckBox>();
      }
    }

    public bool HasSelectedDates
    {
      get
      {
        if (this.SelectedDates != null)
          return this.SelectedDates.Count != 0;
        return false;
      }
    }

    public FilterData[] CheckedFilters
    {
      get
      {
        return this.CheckedFilterElements.Select<CheckBox, FilterData>((Func<CheckBox, FilterData>) (cb => cb.Tag as FilterData)).ToArray<FilterData>();
      }
    }

    public bool IsShowAllChecked
    {
      get
      {
        if (this.cbShowAll == null)
          return false;
        return this.cbShowAll.IsChecked.Value;
      }
    }

    private IEnumerable<CheckBox> CheckedFilterElements
    {
      get
      {
        return this.FilterElements.Where<CheckBox>((Func<CheckBox, bool>) (cb => cb.IsChecked.Value));
      }
    }

    private IEnumerable<CheckBox> FilterElements
    {
      get
      {
        return this.UpperFilterElements.Concat<CheckBox>(this.BottomFilterElements).Where<CheckBox>((Func<CheckBox, bool>) (x =>
        {
          if (x != this.cbShowAll)
            return x != this.cbFilterBySpecificDate;
          return false;
        }));
      }
    }

    private IEnumerable<CheckBox> BottomFilterElements
    {
      get
      {
        if (this.bottomFiltersPanel == null)
          return Enumerable.Empty<CheckBox>();
        return this.bottomFiltersPanel.Children.Cast<CheckBox>();
      }
    }

    private IEnumerable<CheckBox> UpperFilterElements
    {
      get
      {
        if (this.upperFiltersPanel == null)
          return Enumerable.Empty<CheckBox>();
        return this.upperFiltersPanel.Children.Cast<CheckBox>();
      }
    }

    public CalendarFilterControl()
    {
      this.DefaultStyleKey = (object) typeof (CalendarFilterControl);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.Unsubscribe();
      this.upperFiltersPanel = this.GetTemplateChild("PART_UpperFiltersPanel") as Panel;
      this.bottomFiltersPanel = this.GetTemplateChild("PART_BottomFiltersPanel") as Panel;
      this.dateNavigator = this.GetTemplateChild("PART_DateNavigator") as DevExpress.Xpf.Editors.DateNavigator.DateNavigator;
      this.PopulateUpperFilters();
      this.PopulateBottomFilters();
      this.Subscribe();
    }

    private void Subscribe()
    {
      this.SubscribeShowAll();
      this.SubscribeFilterBySpecificDate();
      this.SubscribeDateNavigator();
      this.SubscribeFilters();
    }

    private void SubscribeDateNavigator()
    {
      if (this.dateNavigator == null)
        return;
      this.dateNavigator.SelectedDatesChanged += new EventHandler(this.OnSelectedDatesChanged);
    }

    private void SubscribeFilters()
    {
      this.ForEachFilterElement(new Action<CheckBox>(this.SubscribeFilter));
    }

    private void SubscribeFilter(CheckBox filter)
    {
      if (filter == null)
        return;
      filter.Checked += new RoutedEventHandler(this.OnFilterChecked);
      filter.Unchecked += new RoutedEventHandler(this.OnFilterUnchecked);
    }

    private void SubscribeShowAll()
    {
      if (this.cbShowAll == null)
        return;
      this.cbShowAll.Checked += new RoutedEventHandler(this.OnShowAll);
    }

    private void SubscribeFilterBySpecificDate()
    {
      if (this.cbFilterBySpecificDate == null)
        return;
      this.cbFilterBySpecificDate.Checked += new RoutedEventHandler(this.OnFilterBySpecificDate);
      this.cbFilterBySpecificDate.Unchecked += new RoutedEventHandler(this.OnFilterBySpecificDateUnchecked);
    }

    private void Unsubscribe()
    {
      this.UnsubscribeShowAll();
      this.UnsubscribeFilterBySpecificDate();
      this.UnsubscribeDateNavigator();
      this.UnsubscribeFilters();
    }

    private void UnsubscribeDateNavigator()
    {
      if (this.dateNavigator == null)
        return;
      this.dateNavigator.SelectedDatesChanged -= new EventHandler(this.OnSelectedDatesChanged);
    }

    private void UnsubscribeFilters()
    {
      this.ForEachFilterElement((Action<CheckBox>) (filter => this.UnsubscribeFilter(filter)));
    }

    private void UnsubscribeFilter(CheckBox filter)
    {
      if (filter == null)
        return;
      filter.Checked -= new RoutedEventHandler(this.OnFilterChecked);
      filter.Unchecked -= new RoutedEventHandler(this.OnFilterUnchecked);
    }

    private void UnsubscribeShowAll()
    {
      if (this.cbShowAll == null)
        return;
      this.cbShowAll.Checked -= new RoutedEventHandler(this.OnShowAll);
    }

    private void UnsubscribeFilterBySpecificDate()
    {
      if (this.cbFilterBySpecificDate == null)
        return;
      this.cbFilterBySpecificDate.Checked -= new RoutedEventHandler(this.OnFilterBySpecificDate);
      this.cbFilterBySpecificDate.Unchecked -= new RoutedEventHandler(this.OnFilterBySpecificDateUnchecked);
    }

    private void PopulateUpperFilters()
    {
      if (this.upperFiltersPanel == null || this.UpperFiltersData == null)
        return;
      this.CreateFilters((IEnumerable<FilterData>) this.UpperFiltersData).ForEach<CheckBox>((Action<CheckBox>) (x => this.upperFiltersPanel.Children.Add((UIElement) x)));
      this.cbShowAll = this.FindShowAll();
      this.cbFilterBySpecificDate = this.FindFilterBySpecificDate();
    }

    private CheckBox FindShowAll()
    {
      return this.FindFilter(FilterDateType.None);
    }

    private CheckBox FindFilterBySpecificDate()
    {
      return this.FindFilter(FilterDateType.SpecificDate);
    }

    private void PopulateBottomFilters()
    {
      if (this.bottomFiltersPanel == null || this.BottomFiltersData == null)
        return;
      this.CreateFilters((IEnumerable<FilterData>) this.BottomFiltersData).ForEach<CheckBox>((Action<CheckBox>) (filter => this.bottomFiltersPanel.Children.Add((UIElement) filter)));
    }

    private IEnumerable<CheckBox> CreateFilters(IEnumerable<FilterData> data)
    {
      if (data == null)
        return Enumerable.Empty<CheckBox>();
      return data.Select<FilterData, CheckBox>(new Func<FilterData, CheckBox>(this.CreateFilterElement));
    }

    private CheckBox CreateFilterElement(FilterData filter)
    {
      CheckBox checkBox = new CheckBox();
      checkBox.Content = (object) filter.Caption;
      checkBox.ToolTip = !string.IsNullOrEmpty(filter.Tooltip) ? (object) filter.Tooltip : (object) (string) null;
      checkBox.Style = this.DateFilterStyle;
      checkBox.Tag = (object) filter;
      checkBox.IsThreeState = false;
      return checkBox;
    }

    private CheckBox FindFilter(FilterDateType type)
    {
      return this.UpperFilterElements.Concat<CheckBox>(this.BottomFilterElements).FirstOrDefault<CheckBox>((Func<CheckBox, bool>) (x =>
      {
        if (x.Tag is FilterData)
          return (x.Tag as FilterData).FilterType == type;
        return false;
      }));
    }

    private CheckBox FindFilter(FilterData filter)
    {
      return this.FilterElements.FirstOrDefault<CheckBox>((Func<CheckBox, bool>) (x => x.Tag == filter));
    }

    private FilterData ToFilterData(object filterElement)
    {
      CheckBox checkBox = filterElement as CheckBox;
      if (checkBox == null)
        return (FilterData) null;
      return checkBox.Tag as FilterData ?? FilterData.Null;
    }

    private void ForEachFilterElement(Action<CheckBox> visitor)
    {
      this.FilterElements.ForEach<CheckBox>(visitor);
    }

    private void OnOwnerChanged()
    {
      if (this.Owner == null)
        return;
      this.Owner.SetCalendarFilter((ICalendarFilter) this);
    }

    private void OnShowAll(object sender, RoutedEventArgs e)
    {
      if (this.Owner == null)
        return;
      this.LockedIfCan((Action) (() => this.Owner.OnShowAll()));
    }

    private void OnFilterBySpecificDate(object sender, RoutedEventArgs e)
    {
      if (this.Owner == null)
        return;
      this.LockedIfCan((Action) (() => this.Owner.OnFilterBySpecificDateChecked()));
    }

    private void OnFilterBySpecificDateUnchecked(object sender, RoutedEventArgs e)
    {
      if (this.Owner == null)
        return;
      this.LockedIfCan((Action) (() => this.Owner.OnFilterBySpecificDateUnchecked()));
    }

    private void OnFilterChecked(object sender, RoutedEventArgs e)
    {
      if (this.Owner == null)
        return;
      this.LockedIfCan((Action) (() => this.Owner.OnFilterChecked(this.ToFilterData(sender))));
    }

    private void OnFilterUnchecked(object sender, RoutedEventArgs e)
    {
      if (this.Owner == null)
        return;
      this.LockedIfCan((Action) (() => this.Owner.OnFilterUnchecked(this.ToFilterData(sender))));
    }

    private void OnSelectedDatesChanged(object sender, EventArgs e)
    {
      if (this.Owner == null)
        return;
      this.LockedIfCan((Action) (() => this.Owner.OnSelectedDatesChanged()));
    }

    private void LockedIfCan(Action action)
    {
      this.eventLocker.DoLockedActionIfNotLocked(action);
    }

    private void Locked(Action action)
    {
      this.eventLocker.DoLockedAction(action);
    }

    public void UncheckAllFilters()
    {
      this.ForEachFilterElement((Action<CheckBox>) (x => this.Uncheck(x)));
    }

    public void CheckFilters(params FilterData[] filters)
    {
      ((IEnumerable<FilterData>) filters).ForEach<FilterData>((Action<FilterData>) (x => this.SetFilterChecked(x, true)));
    }

    public void UncheckFilters(params FilterData[] filters)
    {
      ((IEnumerable<FilterData>) filters).ForEach<FilterData>((Action<FilterData>) (x => this.SetFilterChecked(x, false)));
    }

    private void SetFilterChecked(FilterData filter, bool @checked)
    {
      this.SetChecked(this.FindFilter(filter), @checked);
    }

    public void CheckShowAll()
    {
      this.Check(this.cbShowAll);
    }

    public void UncheckShowAll()
    {
      this.Uncheck(this.cbShowAll);
    }

    public void CheckFilterBySpecificDate()
    {
      this.Check(this.cbFilterBySpecificDate);
    }

    public void UncheckFilterBySpecificDate()
    {
      this.Uncheck(this.cbFilterBySpecificDate);
    }

    private void Check(CheckBox cb)
    {
      this.SetChecked(cb, true);
    }

    private void Uncheck(CheckBox cb)
    {
      this.SetChecked(cb, false);
    }

    private void SetChecked(CheckBox cb, bool @checked)
    {
      if (cb == null)
        return;
      this.Locked((Action) (() => cb.IsChecked = new bool?(@checked)));
    }

    public void RebuildFilters()
    {
      this.UnsubscribeFilters();
      this.ClearBottomFilters();
      this.PopulateBottomFilters();
      this.SubscribeFilters();
    }

    private void ClearBottomFilters()
    {
      if (this.bottomFiltersPanel == null)
        return;
      this.bottomFiltersPanel.Children.Clear();
    }

    public void Dispose()
    {
      this.Unsubscribe();
      if (this.Owner == null)
        return;
      this.Owner.SetCalendarFilter((ICalendarFilter) null);
      this.Owner = (ICalendarFilterOwner) null;
      this.ClearValue(CalendarFilterControl.OwnerProperty);
    }
  }
}
