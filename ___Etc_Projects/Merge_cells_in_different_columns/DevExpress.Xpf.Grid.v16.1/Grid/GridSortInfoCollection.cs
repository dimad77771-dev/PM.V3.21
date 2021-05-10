// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridSortInfoCollection
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using System;
using System.ComponentModel;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Represents a collection of <see cref="T:DevExpress.Xpf.Grid.GridSortInfo" /> objects.
  /// </para>
  ///             </summary>
  public class GridSortInfoCollection : SortInfoCollectionBase
  {
    /// <summary>
    ///                 <para>Gets or sets how many columns are used to group data.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies how many columns are used to group data.</value>
    [DefaultValue(0)]
    [DevExpressXpfGridLocalizedDescription("GridSortInfoCollectionGroupCount")]
    public int GroupCount
    {
      get
      {
        return this.GroupCountInternal;
      }
      set
      {
        this.GroupCountInternal = value;
      }
    }

    /// <summary>
    ///                 <para>Clears the collection, then adds the specified objects to it and sets how many columns are used to group data.
    /// </para>
    ///             </summary>
    /// <param name="groupCount">
    /// An integer value that specifies how many columns are used to group data.
    /// 
    ///           </param>
    /// <param name="items">
    /// An array of <see cref="T:DevExpress.Xpf.Grid.GridSortInfo" /> objects to add to the collection.
    /// 
    ///           </param>
    public virtual void ClearAndAddRange(int groupCount, params GridSortInfo[] items)
    {
      this.ClearAndAddRangeCore(groupCount, items);
    }

    /// <summary>
    ///                 <para>Groups data by the values of the specified column.
    /// </para>
    ///             </summary>
    /// <param name="fieldName">
    /// A System.String value that specifies the column's field name.
    /// 
    ///           </param>
    public void GroupByColumn(string fieldName)
    {
      this.GroupByColumn(fieldName, this.GroupCount, ColumnSortOrder.Ascending);
    }

    /// <summary>
    ///                 <para>Groups data by the values of the specified column with the specified sort order. If several columns are involved in grouping, the specified column will reside at the specified grouping level.
    /// </para>
    ///             </summary>
    /// <param name="fieldName">
    /// A <see cref="T:System.String" /> value that specifies the field name of the column by whose values data is grouped.
    /// 
    ///           </param>
    /// <param name="index">
    /// A zero-based integer value that specifies the grouping level. If negative, an exception is raised.
    /// 
    ///           </param>
    /// <param name="sortOrder">
    /// A <see cref="T:DevExpress.Data.ColumnSortOrder" /> enumeration value that specifies the column's sort order.
    /// 
    ///           </param>
    public void GroupByColumn(string fieldName, int index, ColumnSortOrder sortOrder = ColumnSortOrder.Ascending)
    {
      if (string.IsNullOrEmpty(fieldName))
        return;
      if (index < 0 || sortOrder == ColumnSortOrder.None)
      {
        this.UngroupByColumn(fieldName);
      }
      else
      {
        GridSortInfo gridSortInfo = this[fieldName];
        int groupCount = this.GroupCount;
        if (gridSortInfo == null || this.IndexOf(gridSortInfo) >= this.GroupCount)
          ++groupCount;
        this.BeginUpdate();
        try
        {
          ListSortDirection directionBySortOrder = GridSortInfo.GetSortDirectionBySortOrder(sortOrder);
          if (gridSortInfo != null)
          {
            if (index > this.IndexOf(gridSortInfo))
              --index;
            this.LockVerifying = true;
            this.Remove(gridSortInfo);
            gridSortInfo.SortOrder = directionBySortOrder;
            this.LockVerifying = false;
          }
          else
            gridSortInfo = new GridSortInfo(fieldName, directionBySortOrder);
          index = Math.Min(groupCount - 1, index);
          this.Insert(index, gridSortInfo);
          this.fGroupCount = groupCount;
        }
        finally
        {
          this.EndUpdate();
        }
      }
    }

    /// <summary>
    ///                 <para>Ungroups data by the values of the specified column.
    /// </para>
    ///             </summary>
    /// <param name="fieldName">
    /// A <see cref="T:System.String" /> value that speicifies the field name of the column by whose values data grouping is canceled.
    /// 
    ///           </param>
    public void UngroupByColumn(string fieldName)
    {
      if (string.IsNullOrEmpty(fieldName))
        return;
      GridSortInfo gridSortInfo = this[fieldName];
      if (gridSortInfo == null || this.IndexOf(gridSortInfo) >= this.GroupCount)
        return;
      this.BeginUpdate();
      try
      {
        this.fGroupCount = Math.Max(this.GroupCount - 1, 0);
        this.Remove(gridSortInfo);
      }
      finally
      {
        this.EndUpdate();
      }
    }

    protected internal void OnGroupColumnMove(string name, int index, bool fromGroup, bool toGroup)
    {
      if (!fromGroup && !toGroup)
        return;
      if (fromGroup && !toGroup)
      {
        this.UngroupByColumn(name);
      }
      else
      {
        ColumnSortOrder sortOrder = ColumnSortOrder.Ascending;
        if (this[name] != null)
          sortOrder = this[name].GetSortOrder();
        if (!fromGroup && toGroup)
        {
          this.GroupByColumn(name, index, sortOrder);
        }
        else
        {
          if (!fromGroup || !toGroup)
            return;
          this.GroupByColumn(name, index, sortOrder);
        }
      }
    }
  }
}
