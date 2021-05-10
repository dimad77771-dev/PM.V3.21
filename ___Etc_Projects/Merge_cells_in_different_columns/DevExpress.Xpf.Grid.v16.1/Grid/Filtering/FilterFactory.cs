// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Filtering.FilterFactory
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data.Filtering;
using DevExpress.Utils;
using System.Collections.Generic;

namespace DevExpress.Xpf.Grid.Filtering
{
  public class FilterFactory
  {
    private readonly DataControlBase grid;
    private readonly string fieldName;
    private readonly OperandProperty property;
    private readonly FilterFactory.FilterNames filterNamesMap;

    public FilterFactory(DataControlBase grid, string fieldName)
    {
      Guard.ArgumentNotNull((object) grid, "grid");
      Guard.ArgumentNotNull((object) fieldName, "fieldName");
      this.grid = grid;
      this.fieldName = fieldName;
      this.property = new OperandProperty(fieldName);
      this.filterNamesMap = new FilterFactory.FilterNames(grid);
    }

    public FilterData CreateNoneData()
    {
      return new FilterData(this.GetName(FilterDateType.None), (CriteriaOperator) null, string.Empty, FilterDateType.None);
    }

    public FilterData CreateEmptyData()
    {
      return this.CreateFilter(FilterDateType.Empty);
    }

    public FilterData CreateSpecificDateData()
    {
      return new FilterData(this.GetName(FilterDateType.SpecificDate), (CriteriaOperator) null, string.Empty, FilterDateType.SpecificDate);
    }

    public FilterData CreateFilter(FilterDateType type)
    {
      string name = this.GetName(type);
      CriteriaOperator criteria = this.GetCriteria(type);
      string tooltip = this.GetTooltip(type, criteria);
      return new FilterData(name, criteria, tooltip, type);
    }

    private string GetName(FilterDateType type)
    {
      return this.filterNamesMap[type];
    }

    private CriteriaOperator GetCriteria(FilterDateType type)
    {
      if (type == FilterDateType.None)
        return (CriteriaOperator) null;
      return ((IEnumerable<FilterDateType>) new FilterDateType[1]{ type }).ToCriteria(this.fieldName);
    }

    private string GetTooltip(FilterDateType type, CriteriaOperator criteria)
    {
      if (!type.IsFilterValid())
        return string.Empty;
      return this.grid.GetInfoFromCriteriaOperator(criteria.ExpandDates()).FilterText;
    }

    private class FilterNames
    {
      private static readonly Dictionary<FilterDateType, GridControlStringId> TypeToStringIdMap = new Dictionary<FilterDateType, GridControlStringId>() { { FilterDateType.None, GridControlStringId.DateFiltering_ShowAllFilterName }, { FilterDateType.SpecificDate, GridControlStringId.DateFiltering_FilterBySpecificDateFilterName }, { FilterDateType.PriorThisYear, GridControlStringId.DateFiltering_PriorToThisYearFilterName }, { FilterDateType.EarlierThisYear, GridControlStringId.DateFiltering_EarlierThisYearFilterName }, { FilterDateType.EarlierThisMonth, GridControlStringId.DateFiltering_EarlierThisMonthFilterName }, { FilterDateType.LastWeek, GridControlStringId.DateFiltering_LastWeekFilterName }, { FilterDateType.EarlierThisWeek, GridControlStringId.DateFiltering_EarlierThisWeekFilterName }, { FilterDateType.Yesterday, GridControlStringId.DateFiltering_YesterdayFilterName }, { FilterDateType.Today, GridControlStringId.DateFiltering_TodayFilterName }, { FilterDateType.Tomorrow, GridControlStringId.DateFiltering_TomorrowFilterName }, { FilterDateType.LaterThisWeek, GridControlStringId.DateFiltering_LaterThisWeekFilterName }, { FilterDateType.NextWeek, GridControlStringId.DateFiltering_NextWeekFilterName }, { FilterDateType.LaterThisMonth, GridControlStringId.DateFiltering_LaterThisMonthFilterName }, { FilterDateType.LaterThisYear, GridControlStringId.DateFiltering_LaterThisYearFilterName }, { FilterDateType.BeyondThisYear, GridControlStringId.DateFiltering_BeyondThisYearFilterName }, { FilterDateType.Earlier, GridControlStringId.DateFiltering_EarlierFilterName }, { FilterDateType.ThisMonth, GridControlStringId.DateFiltering_ThisMonthFilterName }, { FilterDateType.ThisWeek, GridControlStringId.DateFiltering_ThisWeekFilterName }, { FilterDateType.Beyond, GridControlStringId.DateFiltering_BeyondFilterName }, { FilterDateType.Empty, GridControlStringId.DateFiltering_EmptyFilterName } };
      private static readonly Dictionary<FilterDateType, string> TypeToNameMap = new Dictionary<FilterDateType, string>() { { FilterDateType.MonthAgo6, DateFiltersHelper.SixMonthsAgoFilterName }, { FilterDateType.MonthAgo5, DateFiltersHelper.FiveMonthsAgoFilterName }, { FilterDateType.MonthAgo4, DateFiltersHelper.FourMonthsAgoFilterName }, { FilterDateType.MonthAgo3, DateFiltersHelper.ThreeMonthsAgoFilterName }, { FilterDateType.MonthAgo2, DateFiltersHelper.TwoMonthsAgoFilterName }, { FilterDateType.MonthAgo1, DateFiltersHelper.MonthAgoFilterName }, { FilterDateType.MonthAfter1, DateFiltersHelper.MonthAfterFilterName }, { FilterDateType.MonthAfter2, DateFiltersHelper.TwoMonthsAfterFilterName } };
      private readonly DataControlBase grid;

      private DataViewBase View
      {
        get
        {
          return this.grid.viewCore;
        }
      }

      public string this[FilterDateType type]
      {
        get
        {
          return this.GetName(type);
        }
      }

      public FilterNames(DataControlBase grid)
      {
        Guard.ArgumentNotNull((object) grid, "grid");
        this.grid = grid;
      }

      public string GetName(FilterDateType type)
      {
        if (this.View == null)
          return string.Empty;
        if (FilterFactory.FilterNames.TypeToNameMap.ContainsKey(type))
          return FilterFactory.FilterNames.TypeToNameMap[type];
        if (!FilterFactory.FilterNames.TypeToStringIdMap.ContainsKey(type))
          return string.Empty;
        string localizedString = this.View.GetLocalizedString(FilterFactory.FilterNames.TypeToStringIdMap[type]);
        FilterFactory.FilterNames.TypeToNameMap[type] = localizedString;
        return localizedString;
      }
    }
  }
}
