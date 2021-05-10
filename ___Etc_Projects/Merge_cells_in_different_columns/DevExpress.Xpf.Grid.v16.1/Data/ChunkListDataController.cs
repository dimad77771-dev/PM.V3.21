// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.ChunkListDataController
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Helpers;
using DevExpress.Utils;
using DevExpress.Xpf.ChunkList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DevExpress.Xpf.Data
{
  public class ChunkListDataController : StateGridDataController
  {
    private static int MinMaxValueCacheCount = 10;
    private Dictionary<SummaryItem, object> totalSummaryCache = new Dictionary<SummaryItem, object>();
    private Dictionary<GroupRowInfo, Dictionary<SummaryItem, object>> groupSummaryCache = new Dictionary<GroupRowInfo, Dictionary<SummaryItem, object>>();
    private List<Tuple<GroupRowInfo, SummaryItem>> recalcSummaryCache = new List<Tuple<GroupRowInfo, SummaryItem>>();
    internal const string CrossThreadExceptionMessage = "Cross-thread property changing operation detected. This operation is not supported in optimized summary calculation mode.";

    private IListChanging ListChanging
    {
      get
      {
        return this.DataSource as IListChanging;
      }
    }

    public ChunkListDataController(IDataProviderOwner owner)
      : base(owner)
    {
    }

    protected override void OnDataSourceChanged()
    {
      base.OnDataSourceChanged();
      this.ListChanging.ListChanging += new ListChangingEventHandler(this.ListChanging_ListChanging);
    }

    public override void Dispose()
    {
      this.ListChanging.ListChanging -= new ListChangingEventHandler(this.ListChanging_ListChanging);
      base.Dispose();
    }

    protected override object CalcSummaryValue(SummaryItem summaryItem, SummaryItemType summaryType, bool ignoreNullValues, Type valueType, IEnumerable valuesEnumerable, Func<string[]> exceptionAuxInfoGetter, GroupRowInfo groupRowInfo)
    {
      Dictionary<SummaryItem, object> summaryCache = this.GetSummaryCache(groupRowInfo, true);
      if (summaryItem.ColumnInfo.Unbound)
        return base.CalcSummaryValue(summaryItem, summaryType, ignoreNullValues, valueType, valuesEnumerable, exceptionAuxInfoGetter, groupRowInfo);
      switch (summaryItem.SummaryType)
      {
        case SummaryItemType.Sum:
          object obj = base.CalcSummaryValue(summaryItem, SummaryItemType.Sum, ignoreNullValues, valueType, valuesEnumerable, exceptionAuxInfoGetter, groupRowInfo);
          summaryCache[summaryItem] = obj;
          return obj;
        case SummaryItemType.Min:
        case SummaryItemType.Max:
          return this.CalcMinMax(valuesEnumerable, summaryItem.SummaryType == SummaryItemType.Max, this.GetOrCreateMinMaxValuesCache(summaryItem, summaryCache), ignoreNullValues);
        case SummaryItemType.Average:
          object total;
          int nullCount;
          if (!this.TryCalcAssociativeAverage(valuesEnumerable, out total, out nullCount))
          {
            total = base.CalcSummaryValue(summaryItem, SummaryItemType.Sum, ignoreNullValues, valueType, valuesEnumerable, exceptionAuxInfoGetter, groupRowInfo);
            nullCount = ignoreNullValues ? this.GetNullCount(valuesEnumerable) : 0;
          }
          summaryCache[summaryItem] = (object) new SummaryCache<object>()
          {
            Value = total,
            NullCount = nullCount
          };
          int num = groupRowInfo == null ? this.VisibleListSourceRowCount : groupRowInfo.ChildControllerRowCount;
          return this.CalcAverage(total, num - nullCount);
        default:
          return base.CalcSummaryValue(summaryItem, summaryType, ignoreNullValues, valueType, valuesEnumerable, exceptionAuxInfoGetter, groupRowInfo);
      }
    }

    private bool TryCalcAssociativeAverage(IEnumerable values, out object value, out int nullCount)
    {
      value = (object) 0;
      nullCount = 0;
      foreach (object obj in values)
      {
        SummaryCache<object> summaryCache = obj as SummaryCache<object>;
        if (summaryCache == null)
          return false;
        value = this.CalcSumOnAdded(summaryCache.Value, value);
        nullCount += summaryCache.NullCount;
      }
      return true;
    }

    private int GetNullCount(IEnumerable values)
    {
      int num = 0;
      foreach (object obj in values)
      {
        if (obj == null)
          ++num;
      }
      return num;
    }

    protected override bool IsAssociativeSummary(SummaryItemType summaryType)
    {
      if (summaryType != SummaryItemType.Sum)
        return summaryType == SummaryItemType.Average;
      return true;
    }

    protected override object GetSummaryShortcut(GroupRowInfo groupRowInfo, SummaryItem summaryItem, out bool isValid)
    {
      if (summaryItem.SummaryType != SummaryItemType.Average)
        return base.GetSummaryShortcut(groupRowInfo, summaryItem, out isValid);
      Dictionary<SummaryItem, object> summaryCache = this.GetSummaryCache(groupRowInfo, false);
      object obj;
      if (summaryCache == null || !summaryCache.TryGetValue(summaryItem, out obj))
      {
        isValid = false;
        return (object) null;
      }
      isValid = true;
      return obj;
    }

    public override void UpdateTotalSummary(List<SummaryItem> changedItems)
    {
      base.UpdateTotalSummary(changedItems);
      List<SummaryItem> list = this.totalSummaryCache.Keys.ToList<SummaryItem>();
      foreach (SummaryItem summaryItem in (CollectionBase) this.TotalSummary)
      {
        int index = list.IndexOf(summaryItem);
        if (index != -1)
          list.RemoveAt(index);
      }
      foreach (SummaryItem key in list)
        this.totalSummaryCache.Remove(key);
    }

    public override void UpdateGroupSummary(List<SummaryItem> changedItems)
    {
      this.groupSummaryCache.Clear();
      base.UpdateGroupSummary(changedItems);
    }

    protected override void OnPreRefresh(bool useRowsKeeper)
    {
      this.groupSummaryCache.Clear();
      base.OnPreRefresh(useRowsKeeper);
    }

    protected override void OnGroupDeleted(GroupRowInfo groupInfo)
    {
      this.groupSummaryCache.Remove(groupInfo);
      base.OnGroupDeleted(groupInfo);
    }

    private Dictionary<SummaryItem, object> GetSummaryCache(GroupRowInfo groupRowInfo, bool allowCreate = false)
    {
      if (groupRowInfo == null)
        return this.totalSummaryCache;
      Dictionary<SummaryItem, object> dictionary;
      if (!this.groupSummaryCache.TryGetValue(groupRowInfo, out dictionary) && allowCreate)
      {
        dictionary = new Dictionary<SummaryItem, object>();
        this.groupSummaryCache[groupRowInfo] = dictionary;
      }
      return dictionary;
    }

    private SummaryCache<SortedList<object, int>> GetOrCreateMinMaxValuesCache(SummaryItem summaryItem, Dictionary<SummaryItem, object> summaryCache)
    {
      object obj = (object) null;
      if (!summaryCache.TryGetValue(summaryItem, out obj))
        return this.CreateMinMaxValueCache(summaryItem, summaryCache);
      return (SummaryCache<SortedList<object, int>>) obj;
    }

    private SummaryCache<SortedList<object, int>> CreateMinMaxValueCache(SummaryItem summaryItem, Dictionary<SummaryItem, object> summaryCache)
    {
      SummaryCache<SortedList<object, int>> summaryCache1 = new SummaryCache<SortedList<object, int>>() { Value = new SortedList<object, int>(ChunkListDataController.MinMaxValueCacheCount) };
      summaryCache[summaryItem] = (object) summaryCache1;
      return summaryCache1;
    }

    protected override void UpdateTotalSummaryOnItemDeleted(int controllerRow)
    {
    }

    protected override void UpdateTotalSummaryOnItemFilteredOut(int listSourceRow)
    {
      this.ProcessItemDeleted(this.GetRowByListSourceIndex(listSourceRow), string.Empty, listSourceRow, true, true);
    }

    private void ThrowCrossThreadException()
    {
      throw new InvalidOperationException("Cross-thread property changing operation detected. This operation is not supported in optimized summary calculation mode.");
    }

    private void ListChanging_ListChanging(object sender, ListChangingEventArgs e)
    {
      if (this.Dispatcher != null && !this.Dispatcher.CheckAccess())
      {
        this.Dispatcher.BeginInvoke((Delegate) new Action(this.ThrowCrossThreadException));
        this.ThrowCrossThreadException();
      }
      object byListSourceIndex = this.GetRowByListSourceIndex(e.Index);
      if (this.IsCurrentRowEditing && byListSourceIndex == this.CurrentControllerRowObject)
        return;
      this.ProcessItemDeleted(byListSourceIndex, e.PropertyDescriptor != null ? e.PropertyDescriptor.Name : string.Empty, e.Index, false, true);
    }

    public override void BeginCurrentRowEdit()
    {
      if (!this.IsCurrentRowEditing)
        this.ProcessItemDeleted(this.CurrentControllerRowObject, string.Empty, this.CurrentListSourceIndex, false, false);
      base.BeginCurrentRowEdit();
    }

    public override bool EndCurrentRowEdit(bool force)
    {
      if (this.IsCurrentRowEditing)
        this.OnEndCurrentRowEdit();
      return base.EndCurrentRowEdit(force);
    }

    private void OnEndCurrentRowEdit()
    {
      int num = this.CurrentControllerRow;
label_1:
      GroupRowInfo groupRowInfoByHandle;
      do
      {
        num = this.GetParentRowHandle(num);
        if (num == int.MinValue)
          return;
        groupRowInfoByHandle = this.GroupInfo.GetGroupRowInfoByHandle(num);
      }
      while (groupRowInfoByHandle.ChildControllerRowCount == 1);
      Dictionary<SummaryItem, object> summaryCache1 = this.GetSummaryCache(groupRowInfoByHandle, false);
      IEnumerator enumerator = this.GroupSummary.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          SummaryItem index = (SummaryItem) enumerator.Current;
          if (index.ColumnInfo.Unbound || index.IsCustomSummary)
          {
            this.recalcSummaryCache.Add(new Tuple<GroupRowInfo, SummaryItem>(groupRowInfoByHandle, index));
          }
          else
          {
            object obj;
            switch (index.SummaryType)
            {
              case SummaryItemType.Sum:
                obj = summaryCache1[index];
                break;
              case SummaryItemType.Min:
              case SummaryItemType.Max:
                obj = this.GetSummaryValue(index.SummaryType == SummaryItemType.Max, (SummaryCache<SortedList<object, int>>) summaryCache1[index]);
                break;
              case SummaryItemType.Count:
                obj = (object) (groupRowInfoByHandle.ChildControllerRowCount - 1);
                break;
              case SummaryItemType.Average:
                SummaryCache<object> summaryCache2 = (SummaryCache<object>) summaryCache1[index];
                obj = this.CalcAverage(summaryCache2.Value, groupRowInfoByHandle.ChildControllerRowCount - 1 - summaryCache2.NullCount);
                break;
              default:
                throw new NotSupportedException();
            }
            groupRowInfoByHandle.SetSummaryValue((SummaryItemBase) index, obj);
          }
        }
        goto label_1;
      }
      finally
      {
        IDisposable disposable = enumerator as IDisposable;
        if (disposable != null)
          disposable.Dispose();
      }
    }

    public override void UpdateGroupSummary(GroupRowInfo groupRowInfo, DataControllerChangedItemCollection changedItems)
    {
      for (; groupRowInfo != null; groupRowInfo = groupRowInfo.ParentGroup)
      {
        if (changedItems != null)
          changedItems.AddItem(groupRowInfo.Handle, NotifyChangeType.ItemChanged, groupRowInfo.ParentGroup);
      }
    }

    protected override void UpdateTotalSummaryOnItemAdded(int listSourceRow)
    {
      this.ProcessItemAdded(listSourceRow, string.Empty, true);
    }

    private void ProcessItemAdded(int listSourceRow, string propertyName, bool updateSummary)
    {
      if (this.IsGrouped)
      {
        int num = this.GetControllerRow(listSourceRow);
        while (true)
        {
          num = this.GetParentRowHandle(num);
          if (num != int.MinValue)
          {
            GroupRowInfo groupRowInfoByHandle = this.GroupInfo.GetGroupRowInfoByHandle(num);
            Dictionary<SummaryItem, object> summaryCache = this.GetSummaryCache(groupRowInfoByHandle, true);
            this.ProcessItemAdded(listSourceRow, this.IsGroupedColumn(propertyName) || summaryCache.Count == 0 ? string.Empty : propertyName, this.GroupSummary, summaryCache, groupRowInfoByHandle, updateSummary);
          }
          else
            break;
        }
      }
      this.ProcessItemAdded(listSourceRow, propertyName, (SummaryItemCollection) this.TotalSummary, this.totalSummaryCache, (GroupRowInfo) null, updateSummary);
    }

    private bool IsGroupedColumn(string propertyName)
    {
      for (int index = 0; index < this.SortInfo.GroupCount; ++index)
      {
        if (this.SortInfo[index].ColumnInfo.Name == propertyName)
          return true;
      }
      return false;
    }

    private void ProcessItemAdded(int listSourceRow, string propertyName, SummaryItemCollection summaryCollection, Dictionary<SummaryItem, object> summaryCache, GroupRowInfo groupRowInfo, bool updateSummary)
    {
      foreach (SummaryItem summary in (CollectionBase) summaryCollection)
      {
        if (!(propertyName != string.Empty) || !(summary.FieldName != propertyName) || (summary.ColumnInfo == null || summary.ColumnInfo.Unbound))
        {
          object obj = this.CalcSummaryValueOnAdded(listSourceRow, summaryCache, groupRowInfo, summary);
          if (updateSummary)
          {
            if (groupRowInfo == null)
              summary.SummaryValue = obj;
            else
              groupRowInfo.SetSummaryValue((SummaryItemBase) summary, obj);
          }
        }
      }
    }

    private object CalcSummaryValueOnAdded(int listSourceRow, Dictionary<SummaryItem, object> summaryCache, GroupRowInfo groupRowInfo, SummaryItem summaryItem)
    {
      if (summaryItem.ColumnInfo != null && summaryItem.ColumnInfo.Unbound)
        return this.CalcSummaryValueCore(groupRowInfo, summaryItem);
      if (summaryItem.SummaryType == SummaryItemType.Count)
        return (object) (groupRowInfo == null ? this.VisibleListSourceRowCount : groupRowInfo.ChildControllerRowCount);
      object obj;
      summaryCache.TryGetValue(summaryItem, out obj);
      object rowValue = this.GetRowValue(this.GetControllerRow(listSourceRow), summaryItem.ColumnInfo);
      switch (summaryItem.SummaryType)
      {
        case SummaryItemType.Sum:
          obj = this.CalcSumOnAdded(obj ?? (object) 0, rowValue);
          summaryCache[summaryItem] = obj;
          break;
        case SummaryItemType.Min:
        case SummaryItemType.Max:
          if (obj == null)
            obj = (object) this.CreateMinMaxValueCache(summaryItem, summaryCache);
          obj = this.GetMinMaxValueOnAdded(summaryItem, rowValue, (SummaryCache<SortedList<object, int>>) obj, groupRowInfo);
          break;
        case SummaryItemType.Average:
          SummaryCache<object> summaryCache1 = (SummaryCache<object>) obj;
          if (summaryCache1 == null)
          {
            summaryCache1 = new SummaryCache<object>()
            {
              Value = (object) 0
            };
            summaryCache[summaryItem] = (object) summaryCache1;
          }
          if (rowValue == null && this.SummariesIgnoreNullValues)
            ++summaryCache1.NullCount;
          object total = this.CalcSumOnAdded(summaryCache1.Value, rowValue);
          obj = this.CalcAverage(total, (groupRowInfo == null ? this.VisibleListSourceRowCount : groupRowInfo.ChildControllerRowCount) - summaryCache1.NullCount);
          summaryCache1.Value = total;
          break;
        default:
          obj = this.CalcSummaryValueCore(groupRowInfo, summaryItem);
          break;
      }
      return obj;
    }

    private object CalcSummaryValueCore(GroupRowInfo groupRow, SummaryItem summaryItem)
    {
      bool validResult = false;
      return this.CalcSummaryInfo(groupRow, summaryItem, ref validResult);
    }

    protected override void UpdateTotalSummaryOnItemChanged(int listSourceRow, string propertyName)
    {
      bool flag = this.IsCurrentRowEditing && this.CurrentListSourceIndex != listSourceRow;
      if (flag)
        this.ProcessItemAdded(this.CurrentListSourceIndex, string.Empty, false);
      this.ProcessItemAdded(listSourceRow, propertyName, true);
      if (!flag)
        return;
      this.ProcessItemDeleted(this.CurrentControllerRowObject, string.Empty, this.CurrentListSourceIndex, false, false);
    }

    protected override void OnBindingListChangedCore(ListChangedEventArgs e)
    {
      ChunkListChangedEventArgs changedEventArgs = e as ChunkListChangedEventArgs;
      if (changedEventArgs != null)
        this.ProcessItemDeleted(changedEventArgs.OldItem, string.Empty, e.NewIndex, false, true);
      base.OnBindingListChangedCore(e);
    }

    protected override void OnVisibleIndexesUpdated()
    {
      foreach (Tuple<GroupRowInfo, SummaryItem> tuple in this.recalcSummaryCache)
      {
        GroupRowInfo groupRow = tuple.Item1;
        SummaryItem summaryItem = tuple.Item2;
        if (!this.IsCurrentRowEditing || !groupRow.ContainsControllerRow(this.CurrentControllerRow))
        {
          object obj = this.CalcSummaryValueCore(groupRow, summaryItem);
          if (groupRow == null)
            summaryItem.SummaryValue = obj;
          else
            groupRow.SetSummaryValue((SummaryItemBase) summaryItem, obj);
        }
      }
      this.recalcSummaryCache.Clear();
    }

    private void ProcessItemDeleted(object row, string propertyName, int listSourceRow, bool allowRecalculate, bool updateSummary)
    {
      if (this.IsGrouped)
      {
        int num = this.GetControllerRow(listSourceRow);
        while (true)
        {
          num = this.GetParentRowHandle(num);
          if (num != int.MinValue)
          {
            GroupRowInfo groupRowInfoByHandle = this.GroupInfo.GetGroupRowInfoByHandle(num);
            if (groupRowInfoByHandle.ChildControllerRowCount == 1)
              this.groupSummaryCache.Remove(groupRowInfoByHandle);
            else
              this.ProcessItemDeleted(row, this.IsGroupedColumn(propertyName) ? string.Empty : propertyName, this.GroupSummary, this.GetSummaryCache(groupRowInfoByHandle, false), groupRowInfoByHandle, allowRecalculate, updateSummary);
          }
          else
            break;
        }
      }
      this.ProcessItemDeleted(row, propertyName, (SummaryItemCollection) this.TotalSummary, this.totalSummaryCache, (GroupRowInfo) null, allowRecalculate, updateSummary);
    }

    private void ProcessItemDeleted(object row, string propertyName, SummaryItemCollection summaryCollection, Dictionary<SummaryItem, object> summaryCache, GroupRowInfo groupRowInfo, bool allowRecalculate, bool updateSummary)
    {
      foreach (SummaryItem summary in (CollectionBase) summaryCollection)
      {
        if (!(propertyName != string.Empty) || !(summary.FieldName != propertyName) || (summary.ColumnInfo == null || summary.ColumnInfo.Unbound))
        {
          if (summary.ColumnInfo != null && summary.ColumnInfo.Unbound || summary.SummaryType == SummaryItemType.Custom)
          {
            this.recalcSummaryCache.Add(new Tuple<GroupRowInfo, SummaryItem>(groupRowInfo, summary));
          }
          else
          {
            object obj = this.CalcSummaryValueOnDeleted(row, summaryCache, groupRowInfo, allowRecalculate, summary);
            if (updateSummary)
            {
              if (groupRowInfo == null)
                summary.SummaryValue = obj;
              else
                groupRowInfo.SetSummaryValue((SummaryItemBase) summary, obj);
            }
          }
        }
      }
    }

    private object CalcSummaryValueOnDeleted(object row, Dictionary<SummaryItem, object> summaryCache, GroupRowInfo groupRowInfo, bool allowRecalculate, SummaryItem summaryItem)
    {
      int num;
      if (groupRowInfo == null)
      {
        num = this.VisibleListSourceRowCount;
        if (this.IsGrouped)
          --num;
      }
      else
        num = groupRowInfo.ChildControllerRowCount - 1;
      if (summaryItem.SummaryType == SummaryItemType.Count)
        return (object) (num > 0 ? num : 0);
      object obj = summaryItem.ColumnInfo.PropertyDescriptor.GetValue(row);
      switch (summaryItem.SummaryType)
      {
        case SummaryItemType.Sum:
          summaryCache[summaryItem] = this.CalcSumOnDeleted(summaryCache[summaryItem], obj);
          return summaryCache[summaryItem];
        case SummaryItemType.Min:
        case SummaryItemType.Max:
          return this.GetMinMaxValueOnDeleted(summaryItem, obj, (SummaryCache<SortedList<object, int>>) summaryCache[summaryItem], allowRecalculate, groupRowInfo);
        case SummaryItemType.Average:
          SummaryCache<object> summaryCache1 = (SummaryCache<object>) summaryCache[summaryItem];
          if (obj == null && this.SummariesIgnoreNullValues)
            --summaryCache1.NullCount;
          object total = this.CalcSumOnDeleted(summaryCache1.Value, obj);
          summaryCache1.Value = total;
          return this.CalcAverage(total, num - summaryCache1.NullCount);
        default:
          return this.CalcSummaryValueCore(groupRowInfo, summaryItem);
      }
    }

    private object CalcMinMax(IEnumerable valuesEnumerable, bool isMax, SummaryCache<SortedList<object, int>> summaryCache, bool ignoreNullValues)
    {
      summaryCache.Value.Clear();
      summaryCache.NullCount = 0;
      foreach (object values in valuesEnumerable)
      {
        if (!ignoreNullValues || values != null)
          this.ProcessMinMaxValue(summaryCache, values, isMax, 1, true);
      }
      return this.GetSummaryValue(isMax, summaryCache);
    }

    private void ProcessMinMaxValue(SummaryCache<SortedList<object, int>> summaryCache, object item, bool isMax, int increment, bool force)
    {
      if (item == null)
      {
        if (this.SummariesIgnoreNullValues)
          return;
        ++summaryCache.NullCount;
      }
      else if (summaryCache.Value.ContainsKey(item))
      {
        SortedList<object, int> sortedList;
        object index;
        (sortedList = summaryCache.Value)[index = item] = sortedList[index] + increment;
      }
      else
      {
        int index = isMax ? 0 : summaryCache.Value.Count - 1;
        if ((!force || summaryCache.Value.Count >= summaryCache.Value.Capacity) && (summaryCache.Value.Count <= 0 || !(Comparer.Default.Compare(summaryCache.Value.Keys[index], item) > 0 ^ isMax)))
          return;
        if (summaryCache.Value.Count == summaryCache.Value.Capacity)
          summaryCache.Value.RemoveAt(index);
        summaryCache.Value[item] = increment;
      }
    }

    private object GetMinMaxValueOnDeleted(SummaryItem summaryItem, object value, SummaryCache<SortedList<object, int>> summaryCache, bool allowRecalculate, GroupRowInfo groupRowInfo)
    {
      if (value == null)
      {
        if (!this.SummariesIgnoreNullValues)
          --summaryCache.NullCount;
      }
      else if (summaryCache.Value.ContainsKey(value))
      {
        if (summaryCache.Value[value] == 1)
        {
          summaryCache.Value.Remove(value);
        }
        else
        {
          SortedList<object, int> sortedList;
          object index;
          (sortedList = summaryCache.Value)[index = value] = sortedList[index] - 1;
        }
      }
      bool isMax = summaryItem.SummaryType == SummaryItemType.Max;
      if (summaryCache.Value.Count == 0 && (isMax || summaryCache.NullCount == 0))
      {
        if (allowRecalculate)
          this.CalcSummaryValue(summaryItem, groupRowInfo);
        else
          this.recalcSummaryCache.Add(new Tuple<GroupRowInfo, SummaryItem>(groupRowInfo, summaryItem));
      }
      return this.GetSummaryValue(isMax, summaryCache);
    }

    private object GetMinMaxValueOnAdded(SummaryItem summaryItem, object value, SummaryCache<SortedList<object, int>> summaryCache, GroupRowInfo groupRowInfo)
    {
      bool isMax = summaryItem.SummaryType == SummaryItemType.Max;
      this.ProcessMinMaxValue(summaryCache, value, isMax, 1, false);
      if (summaryCache.Value.Count == 0)
      {
        this.CalcSummaryValue(summaryItem, groupRowInfo);
        Tuple<GroupRowInfo, SummaryItem> tuple = new Tuple<GroupRowInfo, SummaryItem>(groupRowInfo, summaryItem);
        if (this.recalcSummaryCache.Contains(tuple))
          this.recalcSummaryCache.Remove(tuple);
      }
      return this.GetSummaryValue(isMax, summaryCache);
    }

    private object GetSummaryValue(bool isMax, SummaryCache<SortedList<object, int>> summaryCache)
    {
      if (!isMax && summaryCache.NullCount > 0 && !this.SummariesIgnoreNullValues)
        return (object) null;
      if (summaryCache.Value.Count == 0)
        return (object) null;
      return summaryCache.Value.Keys[isMax ? summaryCache.Value.Count - 1 : 0];
    }

    private object CalcSumOnAdded(object summaryValue, object value)
    {
      if (value == null)
        return summaryValue;
      Type type = summaryValue.GetType();
      switch (DXTypeExtensions.GetTypeCode(type))
      {
        case TypeCode.Object:
          if (type == typeof (TimeSpan))
            return (object) TimeSpan.FromMilliseconds(((TimeSpan) summaryValue).TotalMilliseconds + ((TimeSpan) value).TotalMilliseconds);
          break;
        case TypeCode.Int32:
        case TypeCode.Int64:
          long num1 = Convert.ToInt64(summaryValue) + Convert.ToInt64(value);
          return (object) (num1 > (long) int.MaxValue || num1 < (long) int.MinValue ? num1 : (long) (int) num1);
        case TypeCode.UInt32:
        case TypeCode.UInt64:
          ulong num2 = Convert.ToUInt64(summaryValue) + Convert.ToUInt64(value);
          return (object) (ulong) (num2 > (ulong) uint.MaxValue ? (long) num2 : (long) (uint) num2);
        case TypeCode.Single:
          return (object) (float) ((double) Convert.ToSingle(summaryValue) + (double) Convert.ToSingle(value));
        case TypeCode.Double:
          return (object) (Convert.ToDouble(summaryValue) + Convert.ToDouble(value));
        case TypeCode.Decimal:
          return (object) (Convert.ToDecimal(summaryValue) + Convert.ToDecimal(value));
      }
      throw new NotSupportedException();
    }

    private object CalcSumOnDeleted(object summaryValue, object value)
    {
      if (value == null)
        return summaryValue;
      Type type = summaryValue.GetType();
      switch (DXTypeExtensions.GetTypeCode(type))
      {
        case TypeCode.Object:
          if (type == typeof (TimeSpan))
            return (object) TimeSpan.FromMilliseconds(((TimeSpan) summaryValue).TotalMilliseconds - ((TimeSpan) value).TotalMilliseconds);
          break;
        case TypeCode.Int32:
        case TypeCode.Int64:
          long num1 = Convert.ToInt64(summaryValue) - Convert.ToInt64(value);
          return (object) (num1 > (long) int.MaxValue || num1 < (long) int.MinValue ? num1 : (long) (int) num1);
        case TypeCode.UInt32:
        case TypeCode.UInt64:
          ulong num2 = Convert.ToUInt64(summaryValue) - Convert.ToUInt64(value);
          return (object) (ulong) (num2 > (ulong) uint.MaxValue ? (long) num2 : (long) (uint) num2);
        case TypeCode.Single:
          return (object) (float) ((double) Convert.ToSingle(summaryValue) - (double) Convert.ToSingle(value));
        case TypeCode.Double:
          return (object) (Convert.ToDouble(summaryValue) - Convert.ToDouble(value));
        case TypeCode.Decimal:
          return (object) (Convert.ToDecimal(summaryValue) - Convert.ToDecimal(value));
      }
      throw new NotSupportedException();
    }

    private object CalcAverage(object total, int count)
    {
      if (count <= 0)
        return (object) null;
      Type type = total.GetType();
      switch (DXTypeExtensions.GetTypeCode(type))
      {
        case TypeCode.Object:
          if (type == typeof (TimeSpan))
            return (object) TimeSpan.FromMilliseconds(((TimeSpan) total).TotalMilliseconds / (double) count);
          break;
        case TypeCode.Int32:
        case TypeCode.UInt32:
        case TypeCode.Int64:
        case TypeCode.UInt64:
        case TypeCode.Decimal:
          return (object) (Convert.ToDecimal(total) / (Decimal) count);
        case TypeCode.Single:
          return (object) (Convert.ToDecimal(total) / (Decimal) count);
        case TypeCode.Double:
          return (object) (Convert.ToDecimal(total) / (Decimal) count);
      }
      throw new NotSupportedException();
    }
  }
}
