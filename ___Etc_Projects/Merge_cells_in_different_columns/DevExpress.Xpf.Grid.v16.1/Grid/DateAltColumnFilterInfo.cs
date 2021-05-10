// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DateAltColumnFilterInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid.Filtering;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DevExpress.Xpf.Grid
{
  public class DateAltColumnFilterInfo : DateCompactColumnFilterInfo
  {
    private readonly DateAltColumnFilterInfo.DatesCache selectedDatesBeforePopupLastClose = new DateAltColumnFilterInfo.DatesCache();

    protected override FilterDateType[] Filters
    {
      get
      {
        return new FilterDateType[14]{ FilterDateType.Beyond, FilterDateType.MonthAfter2, FilterDateType.MonthAfter1, FilterDateType.NextWeek, FilterDateType.Today, FilterDateType.ThisWeek, FilterDateType.ThisMonth, FilterDateType.MonthAgo1, FilterDateType.MonthAgo2, FilterDateType.MonthAgo3, FilterDateType.MonthAgo4, FilterDateType.MonthAgo5, FilterDateType.MonthAgo6, FilterDateType.Earlier };
      }
    }

    public DateAltColumnFilterInfo(ColumnBase column)
      : base(column)
    {
    }

    protected override void ClearPopupData(PopupBaseEdit popup)
    {
      this.SaveSelectedDates();
      base.ClearPopupData(popup);
    }

    private void SaveSelectedDates()
    {
      if (!this.CalendarFilterExists)
        return;
      this.selectedDatesBeforePopupLastClose.Store(this.CalendarFilter.SelectedDates);
    }

    protected override void UpdateFilterBySpecificDateCheckState()
    {
      if (!this.CalendarFilterExists)
        return;
      if (!this.IsFiltered || !this.selectedDatesBeforePopupLastClose.HasData)
        this.CalendarFilter.UncheckFilterBySpecificDate();
      else
        this.CalendarFilter.CheckFilterBySpecificDate();
    }

    protected override void UpdateSelectedDates()
    {
      if (!this.CalendarFilterExists)
        return;
      if (!this.IsFiltered)
        this.CalendarFilter.SelectedDates = (IList<DateTime>) null;
      else
        this.CalendarFilter.SelectedDates = this.selectedDatesBeforePopupLastClose.Data;
    }

    protected override void UpdateFiltersCheckState()
    {
      if (!this.CalendarFilterExists || !this.IsFiltered)
        return;
      if (this.selectedDatesBeforePopupLastClose.HasData)
        this.CalendarFilter.UncheckAllFilters();
      else
        base.UpdateFiltersCheckState();
    }

    private class DatesCache
    {
      private DateTime[] data;

      public bool HasData
      {
        get
        {
          if (this.data != null)
            return this.data.Length > 0;
          return false;
        }
      }

      public IList<DateTime> Data
      {
        get
        {
          return (IList<DateTime>) new ReadOnlyCollection<DateTime>((IList<DateTime>) (this.data ?? new DateTime[0]));
        }
      }

      public void Store(IList<DateTime> dates)
      {
        if (dates == null || dates.Count == 0)
        {
          this.Dirty();
        }
        else
        {
          this.data = new DateTime[dates.Count];
          dates.CopyTo(this.data, 0);
        }
      }

      public void Dirty()
      {
        this.data = (DateTime[]) null;
      }
    }
  }
}
