// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridGroupSummarySortInfoCollection
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Data;
using System.Collections.Generic;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Represents a collection which contains the information required to sort the group rows by summary values.
  /// </para>
  ///             </summary>
  public class GridGroupSummarySortInfoCollection : ObservableCollectionCore<GridGroupSummarySortInfo>
  {
    internal GridDataProviderBase Owner { get; private set; }

    /// <summary>
    ///                 <para>Initializes a new instance of the GridGroupSummarySortInfoCollection class.
    /// </para>
    ///             </summary>
    /// <param name="owner">The collection owner.</param>
    public GridGroupSummarySortInfoCollection(GridDataProviderBase owner)
    {
      this.Owner = owner;
    }

    /// <summary>
    ///                 <para>Clears the current collection and adds the specified <see cref="T:DevExpress.Xpf.Grid.GridGroupSummarySortInfo" /> objects.
    /// </para>
    ///             </summary>
    /// <param name="sortInfos">
    /// An array of <see cref="T:DevExpress.Xpf.Grid.GridGroupSummarySortInfo" /> objects.
    /// 
    ///           </param>
    public void ClearAndAddRange(params GridGroupSummarySortInfo[] sortInfos)
    {
      this.BeginUpdate();
      try
      {
        this.Clear();
        this.AddRange(sortInfos);
      }
      finally
      {
        this.EndUpdate();
      }
    }

    /// <summary>
    ///                 <para>Adds an array of <see cref="T:DevExpress.Xpf.Grid.GridGroupSummarySortInfo" /> objects to the current collection.
    /// </para>
    ///             </summary>
    /// <param name="items">
    /// An array of <see cref="T:DevExpress.Xpf.Grid.GridGroupSummarySortInfo" /> objects.
    /// 
    ///           </param>
    public void AddRange(params GridGroupSummarySortInfo[] items)
    {
      this.BeginUpdate();
      try
      {
        foreach (GridGroupSummarySortInfo groupSummarySortInfo in items)
          this.Add(groupSummarySortInfo);
      }
      finally
      {
        this.EndUpdate();
      }
    }

    protected override void InsertItem(int index, GridGroupSummarySortInfo item)
    {
      item.Owner = this;
      this.InsertItem(index, item);
    }

    protected override void RemoveItem(int index)
    {
      this[index].Owner = (GridGroupSummarySortInfoCollection) null;
      base.RemoveItem(index);
    }

    internal void Sync(IList<GridSortInfo> sortList, int groupCount)
    {
      this.BeginUpdate();
      try
      {
        for (int index = this.Count - 1; index >= 0; --index)
        {
          GridGroupSummarySortInfo groupSummarySortInfo = this[index];
          if (this.Owner.GroupSummary.IndexOf(groupSummarySortInfo.SummaryItem) == -1 || !this.IsGroupedColumn(groupSummarySortInfo.FieldName, sortList, groupCount))
            this.RemoveSafe(groupSummarySortInfo);
        }
      }
      finally
      {
        this.CancelUpdate();
      }
    }

    private bool IsGroupedColumn(string fieldName, IList<GridSortInfo> sortList, int groupCount)
    {
      for (int index = 0; index < groupCount; ++index)
      {
        if (fieldName == sortList[index].FieldName)
          return true;
      }
      return false;
    }

    internal void ClearCore()
    {
      this.BeginUpdate();
      try
      {
        this.Clear();
      }
      finally
      {
        this.CancelUpdate();
      }
    }
  }
}
