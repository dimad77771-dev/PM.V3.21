// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridGroupSummarySortInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Utils.Serializing;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Grid.Native;
using System.Collections;
using System.ComponentModel;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Represents an element in the <see cref="T:DevExpress.Xpf.Grid.GridGroupSummarySortInfoCollection" />.
  /// </para>
  ///             </summary>
  public class GridGroupSummarySortInfo : IDetailElement<GridGroupSummarySortInfo>
  {
    internal GridGroupSummarySortInfoCollection Owner;

    /// <summary>
    ///                 <para>Gets or sets the field name of a grouping column.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.String" /> value that specifies the field name of a column that defines the nesting level of the group whose data rows are sorted by summary values.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridGroupSummarySortInfoFieldName")]
    [XtraSerializableProperty]
    public string FieldName { get; internal set; }

    /// <summary>
    ///                 <para>Gets or sets the order in which group rows are sorted.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.ComponentModel.ListSortDirection" /> enumeration value that specifies the sort order.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridGroupSummarySortInfoSortOrder")]
    [XtraSerializableProperty]
    public ListSortDirection SortOrder { get; internal set; }

    /// <summary>
    ///                 <para>Gets the summary item.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridSummaryItem" /> object that represents the summary item.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridGroupSummarySortInfoSummaryItem")]
    public GridSummaryItem SummaryItem { get; private set; }

    /// <summary>
    ///                 <para>Gets the summary item's position within the <see cref="P:DevExpress.Xpf.Grid.GridControl.GroupSummary" /> collection.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the item's position within the <see cref="P:DevExpress.Xpf.Grid.GridControl.GroupSummary" /> collection.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [XtraSerializableProperty]
    public int SummaryItemIndex
    {
      get
      {
        return this.GroupSummary.IndexOf(this.SummaryItem);
      }
      internal set
      {
        if (0 > value || value >= this.GroupSummary.Count)
          return;
        this.SummaryItem = this.GroupSummary[value];
      }
    }

    private GridSummaryItemCollection GroupSummary
    {
      get
      {
        return this.Owner.Owner.GroupSummary;
      }
    }

    protected GridGroupSummarySortInfo()
    {
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the GridGroupSummarySortInfo class with the specified summary item and group column.
    /// </para>
    ///             </summary>
    /// <param name="summaryItem">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridSummaryItem" /> object, representing a summary item used to calculate summary values for groups of rows. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.GridGroupSummarySortInfo.SummaryItem" /> property.
    /// 
    ///           </param>
    /// <param name="fieldName">
    /// A <see cref="T:System.String" /> value that specifies the field name of a column that defines the nesting level of the group whose data rows are sorted by summary values. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.GridGroupSummarySortInfo.FieldName" /> property.
    /// 
    ///           </param>
    public GridGroupSummarySortInfo(GridSummaryItem summaryItem, string fieldName)
      : this(summaryItem, fieldName, ListSortDirection.Ascending)
    {
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the GridGroupSummarySortInfo class with the specified summary item, group column and sort order.
    /// </para>
    ///             </summary>
    /// <param name="summaryItem">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridSummaryItem" /> object, representing a summary item used to calculate summary values for groups of rows. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.GridGroupSummarySortInfo.SummaryItem" /> property.
    /// 
    ///           </param>
    /// <param name="fieldName">
    /// A <see cref="T:System.String" /> value that specifies the field name of a column that defines the nesting level of the group whose data rows are sorted by summary values. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.GridGroupSummarySortInfo.FieldName" /> property.
    /// 
    ///           </param>
    /// <param name="sortOrder">
    /// A <see cref="T:System.ComponentModel.ListSortDirection" /> enumeration value that specifies the sort order. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.GridGroupSummarySortInfo.SortOrder" /> property.
    /// 
    ///           </param>
    public GridGroupSummarySortInfo(GridSummaryItem summaryItem, string fieldName, ListSortDirection sortOrder)
    {
      this.FieldName = fieldName;
      this.SummaryItem = summaryItem;
      this.SortOrder = sortOrder;
    }

    internal ColumnSortOrder GetSortOrder()
    {
      return this.SortOrder != ListSortDirection.Ascending ? ColumnSortOrder.Descending : ColumnSortOrder.Ascending;
    }

    GridGroupSummarySortInfo IDetailElement<GridGroupSummarySortInfo>.CreateNewInstance(params object[] args)
    {
      return new GridGroupSummarySortInfo(CloneDetailHelper.SafeGetDependentCollectionItem<GridSummaryItem>(this.SummaryItem, (ISupportGetCachedIndex<GridSummaryItem>) ((GridControl) ((GridDataProvider) this.Owner.Owner).Owner).GroupSummary, (IList) ((GridControl) args[0]).GroupSummary), this.FieldName, this.SortOrder);
    }
  }
}
