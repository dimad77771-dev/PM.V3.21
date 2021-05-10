// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.VisibleIndicesProvider
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Data
{
  public class VisibleIndicesProvider
  {
    private Dictionary<int, object> visibleIndexToScrollIndexMap = new Dictionary<int, object>();
    private Dictionary<int, int> groupRowHandleToSummaryRowCountMap = new Dictionary<int, int>();
    private int cachedVisibleGroupSummaryRowCount = -1;
    private const int DefaultMaxCachedRowCount = 50;
    private const int DefaultCachedRowsIncrement = 20;
    private bool isValid;
    private int visibleIndex;
    private int groupInfoIndex;
    private int index;
    private int currentSummaryRowCount;
    protected int maxCachedRowCount;

    protected GridDataProvider DataProvider { get; private set; }

    protected bool IsBottomNewItemRowVisible
    {
      get
      {
        return this.DataProvider.Owner.NewItemRowPosition == NewItemRowPosition.Bottom;
      }
    }

    public bool IsCacheEmpty
    {
      get
      {
        return this.CachedRowCount == 0;
      }
    }

    public int CachedRowCount
    {
      get
      {
        return this.visibleIndexToScrollIndexMap.Count;
      }
    }

    public bool ShowGroupSummaryFooter
    {
      get
      {
        if (this.DataProvider.Owner.ShowGroupSummaryFooter)
          return this.ShowGroupSummaryFooterInternal;
        return false;
      }
    }

    public int VisibleGroupSummaryRowCount
    {
      get
      {
        if (this.DataProvider.IsServerMode || this.DataProvider.IsAsyncServerMode)
          return this.currentSummaryRowCount;
        this.ValidateVisibleGroupSummaryRowCountCache();
        return this.cachedVisibleGroupSummaryRowCount;
      }
    }

    internal bool ShowGroupSummaryFooterInternal { get; set; }

    public VisibleIndicesProvider(GridDataProvider dataProvider)
    {
      this.DataProvider = dataProvider;
      this.ShowGroupSummaryFooterInternal = true;
    }

    public void InvalidateCache()
    {
      this.isValid = false;
      this.cachedVisibleGroupSummaryRowCount = -1;
      this.visibleIndexToScrollIndexMap.Clear();
      this.groupRowHandleToSummaryRowCountMap.Clear();
    }

    protected void ValidateVisibleGroupSummaryRowCountCache()
    {
      if (this.cachedVisibleGroupSummaryRowCount >= 0)
        return;
      this.cachedVisibleGroupSummaryRowCount = this.DataProvider.DataController.GroupInfo.Count<GroupRowInfo>((Func<GroupRowInfo, bool>) (info =>
      {
        if (info.IsVisible && (!this.DataProvider.DataController.AllowPartialGrouping || this.DataProvider.GetChildRowCount(info.Handle) > 1))
          return this.DataProvider.OnShowingGroupFooter(info.Handle, (int) info.Level);
        return false;
      }));
    }

    public virtual object GetVisibleIndexByScrollIndex(int scrollIndex)
    {
      if (!this.ShowGroupSummaryFooter)
        return (object) scrollIndex;
      this.ValidateCache();
      if (this.IsCacheEmpty)
        return (object) scrollIndex;
      this.UpdateMaxCacheRowCountIfNeeded(scrollIndex);
      if (scrollIndex >= this.CachedRowCount)
        return (object) scrollIndex;
      return this.visibleIndexToScrollIndexMap[scrollIndex];
    }

    public virtual int GetVisibleIndexByScrollIndexSafe(int scrollIndex)
    {
      if (!this.ShowGroupSummaryFooter)
        return scrollIndex;
      this.ValidateCache();
      if (this.IsCacheEmpty)
        return scrollIndex;
      this.UpdateMaxCacheRowCountIfNeeded(scrollIndex);
      if (scrollIndex >= this.CachedRowCount)
        return scrollIndex;
      for (int index = scrollIndex; index >= 0; --index)
      {
        object obj = this.visibleIndexToScrollIndexMap[index];
        if (obj is int)
          return (int) obj;
      }
      return scrollIndex;
    }

    protected void ValidateCache()
    {
      if (this.isValid)
        return;
      this.visibleIndex = this.index = 0;
      this.groupInfoIndex = 0;
      this.currentSummaryRowCount = 0;
      this.maxCachedRowCount = 50;
      this.BuildCache();
      this.isValid = true;
    }

    protected void UpdateMaxCacheRowCountIfNeeded(int requestedScrollIndex)
    {
      if (requestedScrollIndex <= this.maxCachedRowCount)
        return;
      this.maxCachedRowCount = requestedScrollIndex + 20;
      this.BuildCache();
    }

    private bool IsSingleRow(int rowHandle)
    {
      if (!this.DataProvider.DataController.AllowPartialGrouping)
        return false;
      return this.DataProvider.GetChildRowCount(this.DataProvider.GetParentRowHandle(rowHandle)) == 1;
    }

    protected virtual void BuildCache()
    {
      GroupRowInfoCollection groupInfo1 = this.DataProvider.DataController.GroupInfo;
      while (this.groupInfoIndex < groupInfo1.Count)
      {
        GroupRowInfo groupInfo2 = groupInfo1[this.groupInfoIndex];
        bool flag = this.DataProvider.IsGroupVisible(groupInfo2);
        if (flag)
        {
          this.visibleIndexToScrollIndexMap[this.index] = (object) this.visibleIndex;
          ++this.visibleIndex;
          ++this.index;
          this.groupRowHandleToSummaryRowCountMap[groupInfo2.Handle] = this.currentSummaryRowCount;
        }
        if (groupInfo2.Expanded || !flag)
        {
          if ((int) groupInfo2.Level == groupInfo1.LevelCount - 1)
          {
            for (int index = 0; index < groupInfo2.ChildControllerRowCount; ++index)
            {
              this.visibleIndexToScrollIndexMap[this.index] = (object) this.visibleIndex;
              ++this.visibleIndex;
              ++this.index;
              if (!flag)
                this.groupRowHandleToSummaryRowCountMap[groupInfo2.ChildControllerRow] = this.currentSummaryRowCount;
            }
          }
          ++this.groupInfoIndex;
        }
        else
          this.groupInfoIndex = GridDataProvider.GetLevelNextIndex(this.groupInfoIndex, groupInfo1);
        int num = this.groupInfoIndex < groupInfo1.Count ? (int) groupInfo1[this.groupInfoIndex].Level : 0;
        for (int level = (int) groupInfo2.Level; level >= num; --level)
        {
          if (this.DataProvider.IsGroupVisible(groupInfo2) && this.DataProvider.OnShowingGroupFooter(groupInfo2.Handle, level))
          {
            this.visibleIndexToScrollIndexMap[this.index] = (object) new GroupSummaryRowKey(new RowHandle(groupInfo2.Handle), level);
            ++this.currentSummaryRowCount;
            ++this.index;
          }
          groupInfo2 = groupInfo2.ParentGroup;
        }
        if (this.CachedRowCount > this.maxCachedRowCount)
          break;
      }
      if (!this.IsBottomNewItemRowVisible || this.visibleIndex != this.DataProvider.VisibleCount)
        return;
      this.visibleIndexToScrollIndexMap[this.index] = (object) this.visibleIndex;
      this.groupRowHandleToSummaryRowCountMap[-2147483647] = this.currentSummaryRowCount;
      ++this.visibleIndex;
    }

    protected internal int CalcVisibleSummaryRowsCountBeforeRow(int visibleIndex, bool allowFixedGroups)
    {
      if (!this.ShowGroupSummaryFooter)
        return 0;
      this.ValidateCache();
      if (this.IsCacheEmpty)
        return 0;
      while (visibleIndex > this.visibleIndex - 1)
      {
        this.maxCachedRowCount += visibleIndex - this.visibleIndex - 1 + 20;
        this.BuildCache();
      }
      if (this.IsBottomNewItemRowVisible && visibleIndex == this.DataProvider.VisibleCount)
        return this.groupRowHandleToSummaryRowCountMap[-2147483647];
      if (allowFixedGroups)
      {
        int[] scrollableIndexes = this.DataProvider.DataController.GetVisibleIndexes().ScrollableIndexes;
        int val2 = visibleIndex;
        for (int index = 0; index < scrollableIndexes.Length; ++index)
        {
          int val1 = scrollableIndexes[index];
          val2 = Math.Max(val1, val2);
          if (val1 == visibleIndex)
            break;
        }
        visibleIndex = val2;
      }
      return this.GetSummaryRowCountBeforeRow(visibleIndex);
    }

    private int GetSummaryRowCountBeforeRow(int visibleIndex)
    {
      int controllerRow = this.DataProvider.GetControllerRow(visibleIndex);
      if (this.DataProvider.IsGroupRowHandle(controllerRow) || this.IsSingleRow(controllerRow))
        return this.GetCachedSummaryRowCountBeforeRow(controllerRow);
      return this.GetCachedSummaryRowCountBeforeRow(this.DataProvider.GetParentRowHandle(controllerRow));
    }

    internal int GetCachedSummaryRowCountBeforeRow(int rowHandle)
    {
      this.ValidateCache();
      int num = 0;
      if (rowHandle == int.MinValue)
        return num;
      while (!this.groupRowHandleToSummaryRowCountMap.TryGetValue(rowHandle, out num))
      {
        this.maxCachedRowCount += 20;
        this.BuildCache();
        if (this.visibleIndex >= this.DataProvider.VisibleCount)
          return num;
      }
      return num;
    }
  }
}
