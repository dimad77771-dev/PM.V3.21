// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Filtering.DateFiltersHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DevExpress.Xpf.Grid.Filtering
{
  public static class DateFiltersHelper
  {
    private static readonly FilterDateType[] EmptyFilters = new FilterDateType[0];
    private static readonly Tuple<DateTime?, DateTime?>[] EmptyDates = new Tuple<DateTime?, DateTime?>[0];
    private static readonly DateFiltersHelper.Interval[] Intervals = new DateFiltersHelper.Interval[13]{ new DateFiltersHelper.Interval(new FunctionOperatorType?(), new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeThisYear), new FunctionOperatorType[1]{ FunctionOperatorType.IsOutlookIntervalPriorThisYear }), new DateFiltersHelper.Interval(new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeThisYear), new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeThisMonth), new FunctionOperatorType[1]{ FunctionOperatorType.IsOutlookIntervalEarlierThisYear }), new DateFiltersHelper.Interval(new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeThisMonth), new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeLastWeek), new FunctionOperatorType[1]{ FunctionOperatorType.IsOutlookIntervalEarlierThisMonth }), new DateFiltersHelper.Interval(new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeLastWeek), new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeThisWeek), new FunctionOperatorType[1]{ FunctionOperatorType.IsOutlookIntervalLastWeek }), new DateFiltersHelper.Interval(new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeThisWeek), new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeYesterday), new FunctionOperatorType[1]{ FunctionOperatorType.IsOutlookIntervalEarlierThisWeek }), new DateFiltersHelper.Interval(new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeYesterday), new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeToday), new FunctionOperatorType[1]{ FunctionOperatorType.IsOutlookIntervalYesterday }), new DateFiltersHelper.Interval(new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeToday), new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeTomorrow), new FunctionOperatorType[1]{ FunctionOperatorType.IsOutlookIntervalToday }), new DateFiltersHelper.Interval(new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeTomorrow), new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeDayAfterTomorrow), new FunctionOperatorType[1]{ FunctionOperatorType.IsOutlookIntervalTomorrow }), new DateFiltersHelper.Interval(new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeDayAfterTomorrow), new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeNextWeek), new FunctionOperatorType[1]{ FunctionOperatorType.IsOutlookIntervalLaterThisWeek }), new DateFiltersHelper.Interval(new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeNextWeek), new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeTwoWeeksAway), new FunctionOperatorType[1]{ FunctionOperatorType.IsOutlookIntervalNextWeek }), new DateFiltersHelper.Interval(new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeTwoWeeksAway), new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeNextMonth), new FunctionOperatorType[1]{ FunctionOperatorType.IsOutlookIntervalLaterThisMonth }), new DateFiltersHelper.Interval(new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeNextMonth), new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeNextYear), new FunctionOperatorType[1]{ FunctionOperatorType.IsOutlookIntervalLaterThisYear }), new DateFiltersHelper.Interval(new FunctionOperatorType?(FunctionOperatorType.LocalDateTimeNextYear), new FunctionOperatorType?(), new FunctionOperatorType[1]{ FunctionOperatorType.IsOutlookIntervalBeyondThisYear }) };
    private static readonly Dictionary<FilterDateType, DateFiltersHelper.Interval> FilterToIntervalMap = new Dictionary<FilterDateType, DateFiltersHelper.Interval>() { { FilterDateType.PriorThisYear, DateFiltersHelper.Intervals[0] }, { FilterDateType.EarlierThisYear, DateFiltersHelper.Intervals[1] }, { FilterDateType.EarlierThisMonth, DateFiltersHelper.Intervals[2] }, { FilterDateType.LastWeek, DateFiltersHelper.Intervals[3] }, { FilterDateType.EarlierThisWeek, DateFiltersHelper.Intervals[4] }, { FilterDateType.Yesterday, DateFiltersHelper.Intervals[5] }, { FilterDateType.Today, DateFiltersHelper.Intervals[6] }, { FilterDateType.Tomorrow, DateFiltersHelper.Intervals[7] }, { FilterDateType.LaterThisWeek, DateFiltersHelper.Intervals[8] }, { FilterDateType.NextWeek, DateFiltersHelper.Intervals[9] }, { FilterDateType.LaterThisMonth, DateFiltersHelper.Intervals[10] }, { FilterDateType.LaterThisYear, DateFiltersHelper.Intervals[11] }, { FilterDateType.BeyondThisYear, DateFiltersHelper.Intervals[12] } };
    private static readonly Dictionary<FunctionOperatorType, FilterDateType> IntervalToFilterMap = DateFiltersHelper.FilterToIntervalMap.ToDictionary<KeyValuePair<FilterDateType, DateFiltersHelper.Interval>, FunctionOperatorType, FilterDateType>((Func<KeyValuePair<FilterDateType, DateFiltersHelper.Interval>, FunctionOperatorType>) (kv => kv.Value.Parts[0]), (Func<KeyValuePair<FilterDateType, DateFiltersHelper.Interval>, FilterDateType>) (kv => kv.Key));

    private static DateTime Today
    {
      get
      {
        return DateTime.Today;
      }
    }

    internal static string SixMonthsAgoFilterName
    {
      get
      {
        return DateFiltersHelper.GetFilterNameFromDate(DateFiltersHelper.Today.SixMonthsAgo());
      }
    }

    internal static string FiveMonthsAgoFilterName
    {
      get
      {
        return DateFiltersHelper.GetFilterNameFromDate(DateFiltersHelper.Today.FiveMonthsAgo());
      }
    }

    internal static string FourMonthsAgoFilterName
    {
      get
      {
        return DateFiltersHelper.GetFilterNameFromDate(DateFiltersHelper.Today.FourMonthsAgo());
      }
    }

    internal static string ThreeMonthsAgoFilterName
    {
      get
      {
        return DateFiltersHelper.GetFilterNameFromDate(DateFiltersHelper.Today.ThreeMonthsAgo());
      }
    }

    internal static string TwoMonthsAgoFilterName
    {
      get
      {
        return DateFiltersHelper.GetFilterNameFromDate(DateFiltersHelper.Today.TwoMonthsAgo());
      }
    }

    internal static string MonthAgoFilterName
    {
      get
      {
        return DateFiltersHelper.GetFilterNameFromDate(DateFiltersHelper.Today.MonthAgo());
      }
    }

    internal static string MonthAfterFilterName
    {
      get
      {
        return DateFiltersHelper.GetFilterNameFromDate(DateFiltersHelper.Today.MonthAfter());
      }
    }

    internal static string TwoMonthsAfterFilterName
    {
      get
      {
        return DateFiltersHelper.GetFilterNameFromDate(DateFiltersHelper.Today.TwoMonthsAfter());
      }
    }

    public static CriteriaOperator ToCriteria(this IEnumerable<FilterDateType> filters, string fieldName)
    {
      Guard.ArgumentNotNull((object) fieldName, "fieldName");
      OperandProperty property = new OperandProperty(fieldName);
      return DateFiltersHelper.ToCriteria(filters, property) | DateFiltersHelper.ToCriteriaAlt(filters, property) | DateFiltersHelper.ToNullCriteria(filters, property);
    }

    private static CriteriaOperator ToCriteria(IEnumerable<FilterDateType> filters, OperandProperty property)
    {
      return DateFiltersHelper.ToCriteria(DateFiltersHelper.Merge(filters.Where<FilterDateType>((Func<FilterDateType, bool>) (x =>
      {
        if (x >= FilterDateType.PriorThisYear)
          return x <= FilterDateType.BeyondThisYear;
        return false;
      })).OrderBy<FilterDateType, FilterDateType>((Func<FilterDateType, FilterDateType>) (x => x)).Select<FilterDateType, DateFiltersHelper.Interval>((Func<FilterDateType, DateFiltersHelper.Interval>) (x => DateFiltersHelper.FilterToIntervalMap[x]))), property);
    }

    private static DateFiltersHelper.Interval[] Merge(IEnumerable<DateFiltersHelper.Interval> intervals)
    {
      if (!intervals.Any<DateFiltersHelper.Interval>())
        return new DateFiltersHelper.Interval[1]{ DateFiltersHelper.Interval.Empty };
      Stack<DateFiltersHelper.Interval> source = new Stack<DateFiltersHelper.Interval>();
      DateFiltersHelper.Interval interval1 = intervals.First<DateFiltersHelper.Interval>();
      IEnumerable<DateFiltersHelper.Interval> intervals1 = intervals.Skip<DateFiltersHelper.Interval>(1);
      source.Push(interval1);
      foreach (DateFiltersHelper.Interval other in intervals1)
      {
        DateFiltersHelper.Interval interval2 = source.Peek();
        if (!interval2.IsAdjacent(other))
        {
          source.Push(other);
        }
        else
        {
          DateFiltersHelper.Interval interval3 = interval2.Merge(other);
          source.Pop();
          source.Push(interval3);
        }
      }
      return source.Reverse<DateFiltersHelper.Interval>().ToArray<DateFiltersHelper.Interval>();
    }

    private static CriteriaOperator ToCriteria(DateFiltersHelper.Interval[] intervals, OperandProperty property)
    {
      return ((IEnumerable<DateFiltersHelper.Interval>) intervals).Select<DateFiltersHelper.Interval, CriteriaOperator>((Func<DateFiltersHelper.Interval, CriteriaOperator>) (x =>
      {
        FunctionOperatorType[] filters = x.Parts;
        if (filters.Length == 1)
          return (CriteriaOperator) new FunctionOperator(filters[0], new CriteriaOperator[1]{ (CriteriaOperator) property });
        if (filters.Length != 2 || !DateFiltersHelper.CanCreateOrFilter(filters))
          return DateFiltersHelper.ToLeftClosedInterval(x, property);
        return (CriteriaOperator) new FunctionOperator(filters[0], new CriteriaOperator[1]{ (CriteriaOperator) property }) | (CriteriaOperator) new FunctionOperator(filters[1], new CriteriaOperator[1]{ (CriteriaOperator) property });
      })).AggregateWithOr();
    }

    private static bool CanCreateOrFilter(FunctionOperatorType[] filters)
    {
      if (!DateFiltersHelper.AreFirstTwoFilters(filters))
        return !DateFiltersHelper.AreLastTwoFilters(filters);
      return false;
    }

    private static bool AreFirstTwoFilters(FunctionOperatorType[] filters)
    {
      if (filters[0] == FunctionOperatorType.IsOutlookIntervalPriorThisYear)
        return filters[1] == FunctionOperatorType.IsOutlookIntervalEarlierThisYear;
      return false;
    }

    private static bool AreLastTwoFilters(FunctionOperatorType[] filters)
    {
      if (filters[0] == FunctionOperatorType.IsOutlookIntervalLaterThisYear)
        return filters[1] == FunctionOperatorType.IsOutlookIntervalBeyondThisYear;
      return false;
    }

    private static CriteriaOperator ToLeftClosedInterval(DateFiltersHelper.Interval x, OperandProperty property)
    {
      FunctionOperatorType? nullable1 = x.Start;
      FunctionOperatorType? nullable2 = x.End;
      CriteriaOperator criteriaOperator1 = (CriteriaOperator) null;
      CriteriaOperator criteriaOperator2 = (CriteriaOperator) null;
      if (nullable1.HasValue)
        criteriaOperator1 = (CriteriaOperator) ((CriteriaOperator) property >= (CriteriaOperator) new FunctionOperator(nullable1.Value, new CriteriaOperator[0]));
      if (nullable2.HasValue)
        criteriaOperator2 = (CriteriaOperator) ((CriteriaOperator) property < (CriteriaOperator) new FunctionOperator(nullable2.Value, new CriteriaOperator[0]));
      return criteriaOperator1 & criteriaOperator2;
    }

    private static CriteriaOperator ToCriteriaAlt(IEnumerable<FilterDateType> filters, OperandProperty property)
    {
      filters.Where<FilterDateType>((Func<FilterDateType, bool>) (x =>
      {
        if (x >= FilterDateType.Earlier)
          return x <= FilterDateType.Beyond;
        return false;
      })).OrderBy<FilterDateType, FilterDateType>((Func<FilterDateType, FilterDateType>) (x => x));
      if (filters.Count<FilterDateType>() == 0)
        return (CriteriaOperator) null;
      return filters.Select<FilterDateType, CriteriaOperator>((Func<FilterDateType, CriteriaOperator>) (x => DateFiltersHelper.ToCriteriaAlt(x, property))).AggregateWithOr();
    }

    private static CriteriaOperator ToCriteriaAlt(FilterDateType filter, OperandProperty property)
    {
      if (filter == FilterDateType.Earlier)
        return (CriteriaOperator) ((CriteriaOperator) property < (CriteriaOperator) DateFiltersHelper.Today.SixMonthsAgo());
      if (filter == FilterDateType.MonthAgo6)
        return (CriteriaOperator) ((CriteriaOperator) property >= (CriteriaOperator) DateFiltersHelper.Today.SixMonthsAgo()) & (CriteriaOperator) ((CriteriaOperator) property < (CriteriaOperator) DateFiltersHelper.Today.FiveMonthsAgo());
      if (filter == FilterDateType.MonthAgo5)
        return (CriteriaOperator) ((CriteriaOperator) property >= (CriteriaOperator) DateFiltersHelper.Today.FiveMonthsAgo()) & (CriteriaOperator) ((CriteriaOperator) property < (CriteriaOperator) DateFiltersHelper.Today.FourMonthsAgo());
      if (filter == FilterDateType.MonthAgo4)
        return (CriteriaOperator) ((CriteriaOperator) property >= (CriteriaOperator) DateFiltersHelper.Today.FourMonthsAgo()) & (CriteriaOperator) ((CriteriaOperator) property < (CriteriaOperator) DateFiltersHelper.Today.ThreeMonthsAgo());
      if (filter == FilterDateType.MonthAgo3)
        return (CriteriaOperator) ((CriteriaOperator) property >= (CriteriaOperator) DateFiltersHelper.Today.ThreeMonthsAgo()) & (CriteriaOperator) ((CriteriaOperator) property < (CriteriaOperator) DateFiltersHelper.Today.TwoMonthsAgo());
      if (filter == FilterDateType.MonthAgo2)
        return (CriteriaOperator) ((CriteriaOperator) property >= (CriteriaOperator) DateFiltersHelper.Today.TwoMonthsAgo()) & (CriteriaOperator) ((CriteriaOperator) property < (CriteriaOperator) DateFiltersHelper.Today.MonthAgo());
      if (filter == FilterDateType.MonthAgo1)
        return (CriteriaOperator) ((CriteriaOperator) property >= (CriteriaOperator) DateFiltersHelper.Today.MonthAgo()) & (CriteriaOperator) ((CriteriaOperator) property < (CriteriaOperator) DateFiltersHelper.Today.BeginningOfMonth());
      if (filter == FilterDateType.ThisMonth)
        return (CriteriaOperator) ((CriteriaOperator) property >= (CriteriaOperator) DateFiltersHelper.Today.BeginningOfMonth()) & (CriteriaOperator) ((CriteriaOperator) property < (CriteriaOperator) DateFiltersHelper.Today.MonthAfter());
      if (filter == FilterDateType.ThisWeek)
        return (CriteriaOperator) ((CriteriaOperator) property >= (CriteriaOperator) DateFiltersHelper.Today.WeekStart()) & (CriteriaOperator) ((CriteriaOperator) property < (CriteriaOperator) DateFiltersHelper.Today.WeekAfter().WeekStart());
      if (filter == FilterDateType.MonthAfter1)
        return (CriteriaOperator) ((CriteriaOperator) property >= (CriteriaOperator) DateFiltersHelper.Today.MonthAfter()) & (CriteriaOperator) ((CriteriaOperator) property < (CriteriaOperator) DateFiltersHelper.Today.TwoMonthsAfter());
      if (filter == FilterDateType.MonthAfter2)
        return (CriteriaOperator) ((CriteriaOperator) property >= (CriteriaOperator) DateFiltersHelper.Today.TwoMonthsAfter()) & (CriteriaOperator) ((CriteriaOperator) property < (CriteriaOperator) DateFiltersHelper.Today.ThreeMonthsAfter());
      if (filter == FilterDateType.Beyond)
        return (CriteriaOperator) ((CriteriaOperator) property >= (CriteriaOperator) DateFiltersHelper.Today.ThreeMonthsAfter());
      return (CriteriaOperator) null;
    }

    private static CriteriaOperator ToNullCriteria(IEnumerable<FilterDateType> filters, OperandProperty property)
    {
      if (!filters.Contains<FilterDateType>(FilterDateType.Empty))
        return (CriteriaOperator) null;
      return (CriteriaOperator) new NullOperator((CriteriaOperator) property);
    }

    private static CriteriaOperator AggregateWithOr(this IEnumerable<CriteriaOperator> source)
    {
      return source.Aggregate<CriteriaOperator>((Func<CriteriaOperator, CriteriaOperator, CriteriaOperator>) ((acc, cur) => acc | cur));
    }

    public static FilterDateType[] ToFilters(this CriteriaOperator criteria, string fieldName)
    {
      Guard.ArgumentNotNull((object) fieldName, "fieldName");
      OperandProperty property = new OperandProperty(fieldName);
      return ((IEnumerable<FilterDateType>) DateFiltersHelper.ToFilters(criteria, property)).Concat<FilterDateType>((IEnumerable<FilterDateType>) criteria.ToFiltersAlt(property)).Concat<FilterDateType>((IEnumerable<FilterDateType>) criteria.ToNullFilter(property)).Distinct<FilterDateType>().OrderBy<FilterDateType, FilterDateType>((Func<FilterDateType, FilterDateType>) (x => x)).ToArray<FilterDateType>();
    }

    private static FilterDateType[] ToFilters(CriteriaOperator criteria, OperandProperty property)
    {
      GroupOperator groupOperator = criteria as GroupOperator;
      if (!object.ReferenceEquals((object) groupOperator, (object) null) && groupOperator.OperatorType == GroupOperatorType.Or)
        return groupOperator.Operands.SelectMany<CriteriaOperator, FilterDateType>((Func<CriteriaOperator, IEnumerable<FilterDateType>>) (op => (IEnumerable<FilterDateType>) DateFiltersHelper.ToFilters(op, property))).ToArray<FilterDateType>();
      return DateFiltersHelper.ToFiltersCore(criteria, property);
    }

    private static FilterDateType[] ToFiltersCore(CriteriaOperator criteria, OperandProperty property)
    {
      if (criteria is FunctionOperator)
        return DateFiltersHelper.ExtractFromFunc(criteria, property);
      if (criteria is BinaryOperator)
        return DateFiltersHelper.ExtractFromBinary(criteria, property);
      if (criteria is GroupOperator)
        return DateFiltersHelper.ExtractFromGroup(criteria, property);
      return DateFiltersHelper.EmptyFilters;
    }

    private static FilterDateType[] ExtractFromFunc(CriteriaOperator criteria, OperandProperty property)
    {
      FunctionOperator functionOperator = criteria as FunctionOperator;
      if (object.ReferenceEquals((object) functionOperator, (object) null) || functionOperator.Operands.Count != 1 || !object.Equals((object) functionOperator.Operands[0], (object) property))
        return DateFiltersHelper.EmptyFilters;
      int? index = DateFiltersHelper.ExtractIndex((CriteriaOperator) functionOperator, (Func<DateFiltersHelper.Interval, FunctionOperatorType?>) (x => new FunctionOperatorType?(x.Parts[0])));
      if (!index.HasValue)
        return DateFiltersHelper.EmptyFilters;
      return DateFiltersHelper.GetIntervals(index.Value, index.Value);
    }

    private static FilterDateType[] ExtractFromBinary(CriteriaOperator criteria, OperandProperty property)
    {
      BinaryOperator binary = criteria as BinaryOperator;
      if (object.ReferenceEquals((object) binary, (object) null))
        return DateFiltersHelper.EmptyFilters;
      int? greaterOrEqual = DateFiltersHelper.ExtractGreaterOrEqual(binary, property);
      if (greaterOrEqual.HasValue)
        return DateFiltersHelper.GetIntervals(greaterOrEqual.Value, DateFiltersHelper.Intervals.Length - 1);
      int? less = DateFiltersHelper.ExtractLess(binary, property);
      if (less.HasValue)
        return DateFiltersHelper.GetIntervals(0, less.Value);
      return DateFiltersHelper.EmptyFilters;
    }

    private static FilterDateType[] ExtractFromGroup(CriteriaOperator criteria, OperandProperty property)
    {
      GroupOperator groupOperator = criteria as GroupOperator;
      if (object.ReferenceEquals((object) groupOperator, (object) null))
        return DateFiltersHelper.EmptyFilters;
      int? greaterOrEqual = DateFiltersHelper.ExtractGreaterOrEqual(groupOperator.Operands[0] as BinaryOperator, property);
      int? less = DateFiltersHelper.ExtractLess(groupOperator.Operands[1] as BinaryOperator, property);
      if (!greaterOrEqual.HasValue || !less.HasValue)
        return DateFiltersHelper.EmptyFilters;
      return DateFiltersHelper.GetIntervals(greaterOrEqual.Value, less.Value);
    }

    private static int? ExtractGreaterOrEqual(BinaryOperator binary, OperandProperty property)
    {
      if (!DateFiltersHelper.IsFilterCriteria(binary, property))
        return new int?();
      if (binary.OperatorType != BinaryOperatorType.GreaterOrEqual)
        return new int?();
      return DateFiltersHelper.ExtractIndex(binary.RightOperand, (Func<DateFiltersHelper.Interval, FunctionOperatorType?>) (x => x.Start));
    }

    private static int? ExtractLess(BinaryOperator binary, OperandProperty property)
    {
      if (!DateFiltersHelper.IsFilterCriteria(binary, property))
        return new int?();
      if (binary.OperatorType != BinaryOperatorType.Less)
        return new int?();
      return DateFiltersHelper.ExtractIndex(binary.RightOperand, (Func<DateFiltersHelper.Interval, FunctionOperatorType?>) (x => x.End));
    }

    private static bool IsFilterCriteria(BinaryOperator binary, OperandProperty property)
    {
      if (!object.ReferenceEquals((object) binary, (object) null))
        return object.Equals((object) binary.LeftOperand, (object) property);
      return false;
    }

    private static int? ExtractIndex(CriteriaOperator criteria, Func<DateFiltersHelper.Interval, FunctionOperatorType?> type)
    {
      FunctionOperator func = criteria as FunctionOperator;
      if (object.ReferenceEquals((object) func, (object) null))
        return new int?();
      int index = Array.FindIndex<DateFiltersHelper.Interval>(DateFiltersHelper.Intervals, (Predicate<DateFiltersHelper.Interval>) (x =>
      {
        FunctionOperatorType functionOperatorType = func.OperatorType;
        FunctionOperatorType? nullable = type(x);
        if (functionOperatorType == nullable.GetValueOrDefault())
          return nullable.HasValue;
        return false;
      }));
      if (index != -1)
        return new int?(index);
      return new int?();
    }

    private static FilterDateType[] GetIntervals(int from, int to)
    {
      return Enumerable.Range(from, to + 1 - from).Select<int, DateFiltersHelper.Interval>((Func<int, DateFiltersHelper.Interval>) (i => DateFiltersHelper.Intervals[i])).Select<DateFiltersHelper.Interval, FilterDateType>((Func<DateFiltersHelper.Interval, FilterDateType>) (x => DateFiltersHelper.IntervalToFilterMap[x.Parts[0]])).ToArray<FilterDateType>();
    }

    private static FilterDateType[] ToFiltersAlt(this CriteriaOperator criteria, OperandProperty property)
    {
      GroupOperator groupOperator = criteria as GroupOperator;
      if (!object.ReferenceEquals((object) groupOperator, (object) null) && groupOperator.OperatorType == GroupOperatorType.Or)
        return groupOperator.Operands.SelectMany<CriteriaOperator, FilterDateType>((Func<CriteriaOperator, IEnumerable<FilterDateType>>) (x => (IEnumerable<FilterDateType>) x.ToFiltersAltCore(property))).ToArray<FilterDateType>();
      return criteria.ToFiltersAltCore(property);
    }

    private static FilterDateType[] ToFiltersAltCore(this CriteriaOperator criteria, OperandProperty property)
    {
      if (criteria is BinaryOperator)
        return DateFiltersHelper.ExtractFromBinaryAlt(criteria, property);
      if (criteria is GroupOperator)
        return DateFiltersHelper.ExtractFromGroupAlt(criteria, property);
      return DateFiltersHelper.EmptyFilters;
    }

    private static FilterDateType[] ExtractFromBinaryAlt(CriteriaOperator criteria, OperandProperty property)
    {
      DateTime? nullable = new DateTime?();
      DateTime? date1 = DateFiltersHelper.ExtractDate(criteria, BinaryOperatorType.Less, property);
      if (date1.HasValue && DateFiltersHelper.IsEarlier(date1.Value))
        return new FilterDateType[1]{ FilterDateType.Earlier };
      DateTime? date2 = DateFiltersHelper.ExtractDate(criteria, BinaryOperatorType.GreaterOrEqual, property);
      if (!date2.HasValue || !DateFiltersHelper.IsBeyond(date2.Value))
        return DateFiltersHelper.EmptyFilters;
      return new FilterDateType[1]{ FilterDateType.Beyond };
    }

    private static FilterDateType[] ExtractFromGroupAlt(CriteriaOperator criteria, OperandProperty property)
    {
      Tuple<DateTime?, DateTime?>[] dateSpansCore = DateFiltersHelper.ToDateSpansCore(criteria, property);
      if (dateSpansCore.Length == 0)
        return DateFiltersHelper.EmptyFilters;
      Tuple<DateTime?, DateTime?> pair = dateSpansCore[0];
      if (DateFiltersHelper.IsMonthAgo6(pair))
        return new FilterDateType[1]{ FilterDateType.MonthAgo6 };
      if (DateFiltersHelper.IsMonthAgo5(pair))
        return new FilterDateType[1]{ FilterDateType.MonthAgo5 };
      if (DateFiltersHelper.IsMonthAgo4(pair))
        return new FilterDateType[1]{ FilterDateType.MonthAgo4 };
      if (DateFiltersHelper.IsMonthAgo3(pair))
        return new FilterDateType[1]{ FilterDateType.MonthAgo3 };
      if (DateFiltersHelper.IsMonthAgo2(pair))
        return new FilterDateType[1]{ FilterDateType.MonthAgo2 };
      if (DateFiltersHelper.IsMonthAgo1(pair))
        return new FilterDateType[1]{ FilterDateType.MonthAgo1 };
      if (DateFiltersHelper.IsThisMonth(pair))
        return new FilterDateType[1]{ FilterDateType.ThisMonth };
      if (DateFiltersHelper.IsThisWeek(pair))
        return new FilterDateType[1]{ FilterDateType.ThisWeek };
      if (DateFiltersHelper.IsMonthAfter(pair))
        return new FilterDateType[1]{ FilterDateType.MonthAfter1 };
      if (!DateFiltersHelper.IsTwoMonthAfter(pair))
        return DateFiltersHelper.EmptyFilters;
      return new FilterDateType[1]{ FilterDateType.MonthAfter2 };
    }

    private static bool IsEarlier(DateTime date)
    {
      return date == DateFiltersHelper.Today.SixMonthsAgo();
    }

    private static bool IsMonthAgo6(Tuple<DateTime?, DateTime?> pair)
    {
      return DateFiltersHelper.IsDateSpan(pair, DateFiltersHelper.Today.SixMonthsAgo(), DateFiltersHelper.Today.FiveMonthsAgo());
    }

    private static bool IsMonthAgo5(Tuple<DateTime?, DateTime?> pair)
    {
      return DateFiltersHelper.IsDateSpan(pair, DateFiltersHelper.Today.FiveMonthsAgo(), DateFiltersHelper.Today.FourMonthsAgo());
    }

    private static bool IsMonthAgo4(Tuple<DateTime?, DateTime?> pair)
    {
      return DateFiltersHelper.IsDateSpan(pair, DateFiltersHelper.Today.FourMonthsAgo(), DateFiltersHelper.Today.ThreeMonthsAgo());
    }

    private static bool IsMonthAgo3(Tuple<DateTime?, DateTime?> pair)
    {
      return DateFiltersHelper.IsDateSpan(pair, DateFiltersHelper.Today.ThreeMonthsAgo(), DateFiltersHelper.Today.TwoMonthsAgo());
    }

    private static bool IsMonthAgo2(Tuple<DateTime?, DateTime?> pair)
    {
      return DateFiltersHelper.IsDateSpan(pair, DateFiltersHelper.Today.TwoMonthsAgo(), DateFiltersHelper.Today.MonthAgo());
    }

    private static bool IsMonthAgo1(Tuple<DateTime?, DateTime?> pair)
    {
      return DateFiltersHelper.IsDateSpan(pair, DateFiltersHelper.Today.MonthAgo(), DateFiltersHelper.Today.BeginningOfMonth());
    }

    private static bool IsThisMonth(Tuple<DateTime?, DateTime?> pair)
    {
      return DateFiltersHelper.IsDateSpan(pair, DateFiltersHelper.Today.BeginningOfMonth(), DateFiltersHelper.Today.MonthAfter());
    }

    private static bool IsThisWeek(Tuple<DateTime?, DateTime?> pair)
    {
      return DateFiltersHelper.IsDateSpan(pair, DateFiltersHelper.Today.WeekStart(), DateFiltersHelper.Today.WeekAfter().WeekStart());
    }

    private static bool IsMonthAfter(Tuple<DateTime?, DateTime?> pair)
    {
      return DateFiltersHelper.IsDateSpan(pair, DateFiltersHelper.Today.MonthAfter(), DateFiltersHelper.Today.TwoMonthsAfter());
    }

    private static bool IsTwoMonthAfter(Tuple<DateTime?, DateTime?> pair)
    {
      return DateFiltersHelper.IsDateSpan(pair, DateFiltersHelper.Today.TwoMonthsAfter(), DateFiltersHelper.Today.ThreeMonthsAfter());
    }

    private static bool IsBeyond(DateTime date)
    {
      return date == DateFiltersHelper.Today.ThreeMonthsAfter();
    }

    private static bool IsDateSpan(Tuple<DateTime?, DateTime?> pair, DateTime start, DateTime end)
    {
      DateTime? nullable1 = pair.Item1;
      DateTime? nullable2 = pair.Item2;
      if (!nullable1.HasValue || !nullable2.HasValue)
        return false;
      DateTime? nullable3 = nullable1;
      DateTime dateTime1 = start;
      if ((!nullable3.HasValue ? 0 : (nullable3.GetValueOrDefault() == dateTime1 ? 1 : 0)) == 0)
        return false;
      DateTime? nullable4 = nullable2;
      DateTime dateTime2 = end;
      if (nullable4.HasValue)
        return nullable4.GetValueOrDefault() == dateTime2;
      return false;
    }

    private static FilterDateType[] ToNullFilter(this CriteriaOperator criteria, OperandProperty property)
    {
      GroupOperator groupOperator = criteria as GroupOperator;
      if (!object.ReferenceEquals((object) groupOperator, (object) null) && groupOperator.OperatorType == GroupOperatorType.Or)
        return groupOperator.Operands.SelectMany<CriteriaOperator, FilterDateType>((Func<CriteriaOperator, IEnumerable<FilterDateType>>) (x => (IEnumerable<FilterDateType>) x.ToNullFilterCore(property))).ToArray<FilterDateType>();
      return criteria.ToNullFilterCore(property);
    }

    private static FilterDateType[] ToNullFilterCore(this CriteriaOperator criteria, OperandProperty property)
    {
      if (!(criteria is NullOperator))
        return DateFiltersHelper.EmptyFilters;
      return new FilterDateType[1]{ FilterDateType.Empty };
    }

    public static DateTime[] ToDates(this CriteriaOperator criteria, string fieldName)
    {
      Guard.ArgumentNotNull((object) fieldName, "fieldName");
      return ((IEnumerable<Tuple<DateTime?, DateTime?>>) criteria.ToDateSpans(fieldName)).SelectMany<Tuple<DateTime?, DateTime?>, DateTime>((Func<Tuple<DateTime?, DateTime?>, IEnumerable<DateTime>>) (span => DateFiltersHelper.GetDaysInSpan(span))).ToArray<DateTime>();
    }

    private static IEnumerable<DateTime> GetDaysInSpan(Tuple<DateTime?, DateTime?> pair)
    {
      return DateFiltersHelper.GetDaysInSpan(pair.Item1.Value, pair.Item2.Value);
    }

    internal static IEnumerable<DateTime> GetDaysInSpan(DateTime start, DateTime end)
    {
      return Enumerable.Range(0, end.Subtract(start).Days).Select<int, DateTime>((Func<int, DateTime>) (d => start.AddDays((double) d)));
    }

    private static Tuple<DateTime?, DateTime?>[] ToDateSpans(this CriteriaOperator criteria, string fieldName)
    {
      OperandProperty property = new OperandProperty(fieldName);
      GroupOperator groupOperator = criteria as GroupOperator;
      if (!object.ReferenceEquals((object) groupOperator, (object) null) && groupOperator.OperatorType == GroupOperatorType.Or)
        return groupOperator.Operands.SelectMany<CriteriaOperator, Tuple<DateTime?, DateTime?>>((Func<CriteriaOperator, IEnumerable<Tuple<DateTime?, DateTime?>>>) (op => (IEnumerable<Tuple<DateTime?, DateTime?>>) DateFiltersHelper.ToDateSpansCore(op, property))).Where<Tuple<DateTime?, DateTime?>>((Func<Tuple<DateTime?, DateTime?>, bool>) (x =>
        {
          if (x.Item1.HasValue)
            return x.Item2.HasValue;
          return false;
        })).ToArray<Tuple<DateTime?, DateTime?>>();
      return DateFiltersHelper.ToDateSpansCore(criteria, property);
    }

    private static Tuple<DateTime?, DateTime?>[] ToDateSpansCore(CriteriaOperator criteria, OperandProperty property)
    {
      GroupOperator groupOperator = criteria as GroupOperator;
      if (object.ReferenceEquals((object) groupOperator, (object) null) || groupOperator.OperatorType != GroupOperatorType.And || groupOperator.Operands.Count != 2)
        return DateFiltersHelper.EmptyDates;
      DateTime? date1 = DateFiltersHelper.ExtractDate(groupOperator.Operands[0], BinaryOperatorType.GreaterOrEqual, property);
      DateTime? date2 = DateFiltersHelper.ExtractDate(groupOperator.Operands[1], BinaryOperatorType.Less, property);
      if (!(date1.HasValue & date2.HasValue))
        return DateFiltersHelper.EmptyDates;
      return new Tuple<DateTime?, DateTime?>[1]{ Tuple.Create<DateTime?, DateTime?>(date1, date2) };
    }

    private static DateTime? ExtractDate(CriteriaOperator criteria, BinaryOperatorType type, OperandProperty property)
    {
      BinaryOperator binaryOperator = criteria as BinaryOperator;
      if (object.ReferenceEquals((object) binaryOperator, (object) null) || binaryOperator.OperatorType != type || !object.Equals((object) binaryOperator.LeftOperand, (object) property))
        return new DateTime?();
      OperandValue operandValue = binaryOperator.RightOperand as OperandValue;
      if (object.ReferenceEquals((object) operandValue, (object) null) || !(operandValue.Value is DateTime))
        return new DateTime?();
      return new DateTime?((DateTime) operandValue.Value);
    }

    public static bool IsFilterValid(this FilterDateType filter)
    {
      if (filter == FilterDateType.None || !DateFiltersHelper.FilterToIntervalMap.ContainsKey(filter))
        return false;
      DateFiltersHelper.Interval interval = DateFiltersHelper.FilterToIntervalMap[filter];
      if (!interval.Start.HasValue || !interval.End.HasValue)
        return true;
      DateTime localDateTime = EvalHelpers.EvaluateLocalDateTime(interval.Start.Value);
      return EvalHelpers.EvaluateLocalDateTime(interval.End.Value) > localDateTime;
    }

    public static CriteriaOperator ExpandDates(this CriteriaOperator criteria)
    {
      return DateFiltersHelper.ActualDatesProcessor.Do(criteria);
    }

    public static FunctionOperatorType ToFunctionType(this FilterDateType filter)
    {
      if (filter == FilterDateType.None)
        return FunctionOperatorType.None;
      return DateFiltersHelper.FilterToIntervalMap[filter].Parts[0];
    }

    internal static DateTime SixMonthsAgo(this DateTime now)
    {
      return now.MonthsAgo(6);
    }

    internal static DateTime FiveMonthsAgo(this DateTime now)
    {
      return now.MonthsAgo(5);
    }

    internal static DateTime FourMonthsAgo(this DateTime now)
    {
      return now.MonthsAgo(4);
    }

    internal static DateTime ThreeMonthsAgo(this DateTime now)
    {
      return now.MonthsAgo(3);
    }

    internal static DateTime TwoMonthsAgo(this DateTime now)
    {
      return now.MonthsAgo(2);
    }

    internal static DateTime MonthAgo(this DateTime now)
    {
      return now.MonthsAgo(1);
    }

    internal static DateTime MonthAfter(this DateTime now)
    {
      return now.MonthsAfter(1);
    }

    internal static DateTime TwoMonthsAfter(this DateTime now)
    {
      return now.MonthsAfter(2);
    }

    internal static DateTime ThreeMonthsAfter(this DateTime now)
    {
      return now.MonthsAfter(3);
    }

    private static DateTime MonthsAgo(this DateTime now, int months)
    {
      return now.AddMonthsAndGetBeginningOfMonth(-months);
    }

    private static DateTime MonthsAfter(this DateTime now, int months)
    {
      return now.AddMonthsAndGetBeginningOfMonth(months);
    }

    private static DateTime AddMonthsAndGetBeginningOfMonth(this DateTime now, int months)
    {
      return now.AddMonths(months).BeginningOfMonth();
    }

    internal static DateTime BeginningOfMonth(this DateTime now)
    {
      return new DateTime(now.Year, now.Month, 1);
    }

    internal static DateTime WeekStart(this DateTime now)
    {
      return now.WeekStart(CultureInfo.CurrentCulture);
    }

    internal static DateTime WeekStart(this DateTime now, CultureInfo culture)
    {
      DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;
      int num = (now.DayOfWeek - firstDayOfWeek + 7) % 7;
      return now.AddDays((double) -num).Date;
    }

    internal static DateTime WeekAfter(this DateTime now)
    {
      return now.AddDays(7.0);
    }

    private static string GetFilterNameFromDate(DateTime date)
    {
      return date.ToString("yyyy, MMMM");
    }

    private class Interval : IEquatable<DateFiltersHelper.Interval>
    {
      internal readonly FunctionOperatorType? Start;
      internal readonly FunctionOperatorType? End;
      internal readonly FunctionOperatorType[] Parts;

      internal static DateFiltersHelper.Interval Empty
      {
        get
        {
          return new DateFiltersHelper.Interval(new FunctionOperatorType?(), new FunctionOperatorType?(), new FunctionOperatorType[0]);
        }
      }

      internal Interval(FunctionOperatorType? start, FunctionOperatorType? end, params FunctionOperatorType[] parts)
      {
        this.Start = start;
        this.End = end;
        this.Parts = parts ?? new FunctionOperatorType[0];
      }

      internal bool IsAdjacent(DateFiltersHelper.Interval other)
      {
        FunctionOperatorType? nullable1 = this.End;
        FunctionOperatorType? nullable2 = other.Start;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault())
          return nullable1.HasValue == nullable2.HasValue;
        return false;
      }

      internal DateFiltersHelper.Interval Merge(DateFiltersHelper.Interval other)
      {
        return new DateFiltersHelper.Interval(this.Start, other.End, ((IEnumerable<FunctionOperatorType>) this.Parts).Concat<FunctionOperatorType>((IEnumerable<FunctionOperatorType>) other.Parts).ToArray<FunctionOperatorType>());
      }

      public override bool Equals(object obj)
      {
        if (obj == null || this.GetType() != obj.GetType())
          return false;
        return base.Equals((object) (DateFiltersHelper.Interval) obj);
      }

      public bool Equals(DateFiltersHelper.Interval other)
      {
        FunctionOperatorType? nullable1 = this.Start;
        FunctionOperatorType? nullable2 = other.Start;
        if ((nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 0 : (nullable1.HasValue == nullable2.HasValue ? 1 : 0)) == 0)
          return false;
        FunctionOperatorType? nullable3 = this.End;
        FunctionOperatorType? nullable4 = other.End;
        if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault())
          return nullable3.HasValue == nullable4.HasValue;
        return false;
      }

      public override int GetHashCode()
      {
        return this.ToString().GetHashCode();
      }

      public override string ToString()
      {
        return string.Format("[{0} {1}]", (object) this.Start, (object) this.End);
      }
    }

    private class ActualDatesProcessor : IClientCriteriaVisitor<CriteriaOperator>, ICriteriaVisitor<CriteriaOperator>
    {
      public CriteriaOperator Visit(OperandProperty theOperand)
      {
        return (CriteriaOperator) theOperand;
      }

      public CriteriaOperator Visit(AggregateOperand theOperand)
      {
        return (CriteriaOperator) new AggregateOperand(theOperand.CollectionProperty, this.Process(theOperand.AggregatedExpression), theOperand.AggregateType, this.Process(theOperand.Condition));
      }

      public CriteriaOperator Visit(JoinOperand theOperand)
      {
        return (CriteriaOperator) new JoinOperand(theOperand.JoinTypeName, this.Process(theOperand.Condition), theOperand.AggregateType, this.Process(theOperand.AggregatedExpression));
      }

      public CriteriaOperator Visit(FunctionOperator theOperator)
      {
        switch (theOperator.OperatorType)
        {
          case FunctionOperatorType.LocalDateTimeThisYear:
          case FunctionOperatorType.LocalDateTimeThisMonth:
          case FunctionOperatorType.LocalDateTimeLastWeek:
          case FunctionOperatorType.LocalDateTimeThisWeek:
          case FunctionOperatorType.LocalDateTimeYesterday:
          case FunctionOperatorType.LocalDateTimeToday:
          case FunctionOperatorType.LocalDateTimeNow:
          case FunctionOperatorType.LocalDateTimeTomorrow:
          case FunctionOperatorType.LocalDateTimeDayAfterTomorrow:
          case FunctionOperatorType.LocalDateTimeNextWeek:
          case FunctionOperatorType.LocalDateTimeTwoWeeksAway:
          case FunctionOperatorType.LocalDateTimeNextMonth:
          case FunctionOperatorType.LocalDateTimeNextYear:
            return (CriteriaOperator) new OperandValue((object) EvalHelpers.EvaluateLocalDateTime(theOperator.OperatorType));
          case FunctionOperatorType.IsOutlookIntervalBeyondThisYear:
          case FunctionOperatorType.IsOutlookIntervalLaterThisYear:
          case FunctionOperatorType.IsOutlookIntervalLaterThisMonth:
          case FunctionOperatorType.IsOutlookIntervalNextWeek:
          case FunctionOperatorType.IsOutlookIntervalLaterThisWeek:
          case FunctionOperatorType.IsOutlookIntervalTomorrow:
          case FunctionOperatorType.IsOutlookIntervalToday:
          case FunctionOperatorType.IsOutlookIntervalYesterday:
          case FunctionOperatorType.IsOutlookIntervalEarlierThisWeek:
          case FunctionOperatorType.IsOutlookIntervalLastWeek:
          case FunctionOperatorType.IsOutlookIntervalEarlierThisMonth:
          case FunctionOperatorType.IsOutlookIntervalEarlierThisYear:
          case FunctionOperatorType.IsOutlookIntervalPriorThisYear:
            return this.Process(EvalHelpers.ExpandIsOutlookInterval(theOperator));
          default:
            return (CriteriaOperator) new FunctionOperator(theOperator.OperatorType, this.Process((ICollection<CriteriaOperator>) theOperator.Operands));
        }
      }

      public CriteriaOperator Visit(OperandValue theOperand)
      {
        return (CriteriaOperator) theOperand;
      }

      public CriteriaOperator Visit(GroupOperator theOperator)
      {
        return GroupOperator.Combine(theOperator.OperatorType, this.Process((ICollection<CriteriaOperator>) theOperator.Operands));
      }

      public CriteriaOperator Visit(InOperator theOperator)
      {
        return (CriteriaOperator) new InOperator(this.Process(theOperator.LeftOperand), this.Process((ICollection<CriteriaOperator>) theOperator.Operands));
      }

      public CriteriaOperator Visit(UnaryOperator theOperator)
      {
        return (CriteriaOperator) new UnaryOperator(theOperator.OperatorType, this.Process(theOperator.Operand));
      }

      public CriteriaOperator Visit(BinaryOperator theOperator)
      {
        return (CriteriaOperator) new BinaryOperator(this.Process(theOperator.LeftOperand), this.Process(theOperator.RightOperand), theOperator.OperatorType);
      }

      public CriteriaOperator Visit(BetweenOperator theOperator)
      {
        return (CriteriaOperator) new BetweenOperator(this.Process(theOperator.TestExpression), this.Process(theOperator.BeginExpression), this.Process(theOperator.EndExpression));
      }

      private CriteriaOperator Process(CriteriaOperator op)
      {
        if (object.ReferenceEquals((object) op, (object) null))
          return (CriteriaOperator) null;
        return op.Accept<CriteriaOperator>((ICriteriaVisitor<CriteriaOperator>) this);
      }

      private CriteriaOperator[] Process(ICollection<CriteriaOperator> ops)
      {
        return ops.Select<CriteriaOperator, CriteriaOperator>(new Func<CriteriaOperator, CriteriaOperator>(this.Process)).ToArray<CriteriaOperator>();
      }

      public static CriteriaOperator Do(CriteriaOperator op)
      {
        return new DateFiltersHelper.ActualDatesProcessor().Process(op);
      }
    }
  }
}
