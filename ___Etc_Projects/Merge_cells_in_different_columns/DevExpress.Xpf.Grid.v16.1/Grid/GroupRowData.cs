// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupRowData
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Contains information about a group row.
  /// </para>
  ///             </summary>
  public class GroupRowData : RowData
  {
    private Locker UpdateAllItemsSelectedLocker = new Locker();
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupSummaryDataProperty = DependencyPropertyManager.Register("GroupSummaryData", typeof (IList<GridGroupSummaryData>), typeof (GroupRowData), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GroupRowData.GroupValue" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupValueProperty = DependencyPropertyManager.Register("GroupValue", typeof (GridGroupValueData), typeof (GroupRowData), new PropertyMetadata((PropertyChangedCallback) null));
    private static readonly DependencyPropertyKey GroupLevelPropertyKey = DependencyPropertyManager.RegisterReadOnly("GroupLevel", typeof (int), typeof (GroupRowData), new PropertyMetadata((object) 0));
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GroupRowData.GroupLevel" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupLevelProperty = GroupRowData.GroupLevelPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey FixedLeftGroupSummaryDataPropertyKey = DependencyPropertyManager.RegisterReadOnly("FixedLeftGroupSummaryData", typeof (IList<GridGroupSummaryColumnData>), typeof (GroupRowData), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FixedLeftGroupSummaryDataProperty = GroupRowData.FixedLeftGroupSummaryDataPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey FixedRightGroupSummaryDataPropertyKey = DependencyPropertyManager.RegisterReadOnly("FixedRightGroupSummaryData", typeof (IList<GridGroupSummaryColumnData>), typeof (GroupRowData), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FixedRightGroupSummaryDataProperty = GroupRowData.FixedRightGroupSummaryDataPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey FixedNoneGroupSummaryDataPropertyKey = DependencyPropertyManager.RegisterReadOnly("FixedNoneGroupSummaryData", typeof (IList<GridGroupSummaryColumnData>), typeof (GroupRowData), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FixedNoneGroupSummaryDataProperty = GroupRowData.FixedNoneGroupSummaryDataPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey IsLastVisibleElementRowPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsLastVisibleElementRow", typeof (bool), typeof (GroupRowData), new PropertyMetadata((object) false));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsLastVisibleElementRowProperty = GroupRowData.IsLastVisibleElementRowPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey IsLastHierarchicalRowPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsLastHierarchicalRow", typeof (bool), typeof (GroupRowData), new PropertyMetadata((object) false));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsLastHierarchicalRowProperty = GroupRowData.IsLastHierarchicalRowPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey IsPreviewExpandedPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsPreviewExpanded", typeof (bool), typeof (GroupRowData), new PropertyMetadata((object) false));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsPreviewExpandedProperty = GroupRowData.IsPreviewExpandedPropertyKey.DependencyProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllItemsSelectedProperty = DependencyProperty.Register("AllItemsSelected", typeof (bool?), typeof (GroupRowData), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GroupRowData) d).OnAllItemsSelectedChanged())));
    private readonly GroupRowData.UpdateGroupSummaryDataStrategy updateGroupSummaryDataStrategy;
    private IGroupRowStateClient groupRowStateClient;

    internal GroupRowData.UpdateGroupSummaryDataStrategy UpdateGroupSummaryDataStrategyInternal
    {
      get
      {
        return this.updateGroupSummaryDataStrategy;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the group row's value. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridGroupValueData" /> object that represents the group row's value.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GroupRowDataGroupValue")]
    public GridGroupValueData GroupValue
    {
      get
      {
        return (GridGroupValueData) this.GetValue(GroupRowData.GroupValueProperty);
      }
      set
      {
        this.SetValue(GroupRowData.GroupValueProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets a value that specifies at which nesting level the group row resides. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the nesting level at which the group row resides.</value>
    [DevExpressXpfGridLocalizedDescription("GroupRowDataGroupLevel")]
    public int GroupLevel
    {
      get
      {
        return (int) this.GetValue(GroupRowData.GroupLevelProperty);
      }
      private set
      {
        this.SetValue(GroupRowData.GroupLevelPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the list of objects that contain information on group summaries displayed within a group row. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>The list of <see cref="T:DevExpress.Xpf.Grid.GridGroupSummaryData" /> objects that contain information on group summaries displayed within a group row.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GroupRowDataGroupSummaryData")]
    public IList<GridGroupSummaryData> GroupSummaryData
    {
      get
      {
        return (IList<GridGroupSummaryData>) this.GetValue(GroupRowData.GroupSummaryDataProperty);
      }
      set
      {
        this.SetValue(GroupRowData.GroupSummaryDataProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GroupRowDataFixedLeftGroupSummaryData")]
    public IList<GridGroupSummaryColumnData> FixedLeftGroupSummaryData
    {
      get
      {
        return (IList<GridGroupSummaryColumnData>) this.GetValue(GroupRowData.FixedLeftGroupSummaryDataProperty);
      }
      private set
      {
        this.SetValue(GroupRowData.FixedLeftGroupSummaryDataPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GroupRowDataFixedRightGroupSummaryData")]
    public IList<GridGroupSummaryColumnData> FixedRightGroupSummaryData
    {
      get
      {
        return (IList<GridGroupSummaryColumnData>) this.GetValue(GroupRowData.FixedRightGroupSummaryDataProperty);
      }
      private set
      {
        this.SetValue(GroupRowData.FixedRightGroupSummaryDataPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GroupRowDataFixedNoneGroupSummaryData")]
    public IList<GridGroupSummaryColumnData> FixedNoneGroupSummaryData
    {
      get
      {
        return (IList<GridGroupSummaryColumnData>) this.GetValue(GroupRowData.FixedNoneGroupSummaryDataProperty);
      }
      private set
      {
        this.SetValue(GroupRowData.FixedNoneGroupSummaryDataPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    public bool IsLastVisibleElementRow
    {
      get
      {
        return (bool) this.GetValue(GroupRowData.IsLastVisibleElementRowProperty);
      }
      private set
      {
        this.SetValue(GroupRowData.IsLastVisibleElementRowPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets whether all items within the group are selected. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if all items within the group are selected; otherwise, <b>false</b>.
    /// </value>
    public bool? AllItemsSelected
    {
      get
      {
        return (bool?) this.GetValue(GroupRowData.AllItemsSelectedProperty);
      }
      set
      {
        this.SetValue(GroupRowData.AllItemsSelectedProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Indicates whether the group row is at the lowest hierarchical level. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, if the group row is at the lowest hierarchical level; otherwise, <b>false</b>.
    /// </value>
    public bool IsLastHierarchicalRow
    {
      get
      {
        return (bool) this.GetValue(GroupRowData.IsLastHierarchicalRowProperty);
      }
      private set
      {
        this.SetValue(GroupRowData.IsLastHierarchicalRowPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Indicates whether the parent group row is expanded. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the parent group row is expanded; otherwise, <b>false</b>.
    /// </value>
    public bool IsPreviewExpanded
    {
      get
      {
        return (bool) this.GetValue(GroupRowData.IsPreviewExpandedProperty);
      }
      private set
      {
        this.SetValue(GroupRowData.IsPreviewExpandedPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    public DataRowsContainer DataRowsContainer
    {
      get
      {
        return (DataRowsContainer) this.RowsContainer;
      }
    }

    protected GridViewBase GridView
    {
      get
      {
        return (GridViewBase) this.View;
      }
    }

    private GridControl Grid
    {
      get
      {
        return this.GridView.Grid;
      }
    }

    protected ITableView TableView
    {
      get
      {
        return this.View as ITableView;
      }
    }

    /// <summary>
    ///     <para> </para>
    /// </summary>
    /// <value> </value>
    public double Offset
    {
      get
      {
        if (this.TableView != null)
          return this.TableView.LeftGroupAreaIndent * (double) this.Level - (this.TableView.ActualShowDetailButtons ? this.TableView.ActualExpandDetailHeaderWidth : 0.0);
        if (this.View is CardView)
          return ((CardView) this.View).LeftGroupAreaIndent * (double) this.Level;
        return 0.0;
      }
    }

    internal override FrameworkElement RowElement
    {
      get
      {
        return ((IGroupRow) base.RowElement).RowElement;
      }
    }

    protected override bool UpdateImmediatelyCore
    {
      get
      {
        return !this.View.RootView.GetAllowGroupSummaryCascadeUpdate;
      }
    }

    protected virtual bool IsGroupFooter
    {
      get
      {
        return false;
      }
    }

    protected override bool IsItemsContainerCore
    {
      get
      {
        return true;
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the GroupRowData class.
    /// </para>
    ///             </summary>
    /// <param name="treeBuilder">
    /// 
    /// 
    /// </param>
    public GroupRowData(DataTreeBuilder treeBuilder)
      : base(treeBuilder, false, true)
    {
      this.updateGroupSummaryDataStrategy = new GroupRowData.UpdateGroupSummaryDataStrategy(this);
    }

    internal static string GetColumnDisplayFormat(ColumnBase column)
    {
      if (column == null)
        return string.Empty;
      return column.DisplayFormat;
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <param name="manager">
    /// 
    /// 
    /// </param>
    /// <returns>
    /// </returns>
    [Browsable(false)]
    public bool ShouldSerializeGroupSummaryData(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <param name="manager">
    /// 
    /// 
    /// </param>
    /// <returns>
    /// </returns>
    [Browsable(false)]
    public bool ShouldSerializeCellData(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <param name="manager">
    /// 
    /// 
    /// </param>
    /// <returns>
    /// </returns>
    [Browsable(false)]
    public bool ShouldSerializeFixedLeftCellData(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <param name="manager">
    /// 
    /// 
    /// </param>
    /// <returns>
    /// </returns>
    [Browsable(false)]
    public bool ShouldSerializeFixedRightCellData(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <param name="manager">
    /// 
    /// 
    /// </param>
    /// <returns>
    /// </returns>
    [Browsable(false)]
    public bool ShouldSerializeFixedNoneCellData(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <param name="manager">
    /// 
    /// </param>
    /// <returns> </returns>
    [Browsable(false)]
    public bool ShouldSerializeFixedLeftGroupSummaryData(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <param name="manager">
    /// 
    /// </param>
    /// <returns> </returns>
    [Browsable(false)]
    public bool ShouldSerializeFixedRightGroupSummaryData(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <param name="manager">
    /// 
    /// </param>
    /// <returns> </returns>
    [Browsable(false)]
    public bool ShouldSerializeFixedNoneGroupSummaryData(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    protected override FrameworkElement CreateRowElement()
    {
      return this.GridView.CreateGroupControl(this);
    }

    protected internal override void UpdateGroupSummaryData()
    {
      this.RefreshFixedNoneCellData(this.treeBuilder.SupportsHorizontalVirtualization, true);
      this.UpdateClientSummary();
    }

    private void UpdateGroupSummariesCore()
    {
      if (this.DataControl == null)
        return;
      if (this.GroupSummaryData == null)
        this.GroupSummaryData = (IList<GridGroupSummaryData>) new ObservableCollection<GridGroupSummaryData>();
      int num1 = 0;
      IList<SummaryItemBase> groupSummaries = this.treeBuilder.GetGroupSummaries();
      foreach (GridSummaryItem gridSummaryItem in (IEnumerable<SummaryItemBase>) groupSummaries)
      {
        object obj = (object) null;
        if (this.treeBuilder.TryGetGroupSummaryValue((RowData) this, (SummaryItemBase) gridSummaryItem, out obj) && this.CanUpdateSummary(gridSummaryItem))
          ++num1;
      }
      int num2 = num1 - this.GroupSummaryData.Count;
      if (num2 > 0)
      {
        for (int index = 0; index < num2; ++index)
          this.GroupSummaryData.Add(new GridGroupSummaryData((ColumnsRowDataBase) this));
      }
      else
      {
        for (int index = 0; index < -num2; ++index)
          this.GroupSummaryData.RemoveAt(this.GroupSummaryData.Count - 1);
      }
      int index1 = 0;
      for (int index2 = 0; index2 < groupSummaries.Count; ++index2)
      {
        GridSummaryItem summaryItem = (GridSummaryItem) groupSummaries[index2];
        GridColumn gridColumn = this.Grid.Columns[summaryItem.FieldName];
        object obj = (object) null;
        if (this.treeBuilder.TryGetGroupSummaryValue((RowData) this, (SummaryItemBase) summaryItem, out obj) && this.CanUpdateSummary(summaryItem))
        {
          GridGroupSummaryData groupSummaryData = this.GroupSummaryData[index1];
          groupSummaryData.Column = (ColumnBase) gridColumn;
          groupSummaryData.Text = this.GetGroupSummaryText(summaryItem, (ColumnBase) gridColumn, this.View as DevExpress.Xpf.Grid.TableView, obj, false);
          groupSummaryData.SummaryItem = summaryItem;
          groupSummaryData.Data = this.DataContext;
          groupSummaryData.Value = obj;
          groupSummaryData.SummaryValue = obj;
          groupSummaryData.IsLast = index1 == this.GroupSummaryData.Count - 1;
          groupSummaryData.IsFirst = index2 == 0;
          ++index1;
        }
      }
    }

    private bool CanUpdateSummary(GridSummaryItem item)
    {
      if (item.Visible)
        return this.CanExtractGridGroupSummaryItem(item);
      return false;
    }

    protected IList<GridSummaryItem> ExtractGridGroupSummaries(IList<SummaryItemBase> summaries)
    {
      List<GridSummaryItem> gridSummaryItemList = new List<GridSummaryItem>();
      foreach (GridSummaryItem summary in (IEnumerable<SummaryItemBase>) summaries)
      {
        if (this.CanExtractGridGroupSummaryItem(summary))
          gridSummaryItemList.Add(summary);
      }
      return (IList<GridSummaryItem>) gridSummaryItemList;
    }

    protected virtual bool CanExtractGridGroupSummaryItem(GridSummaryItem summary)
    {
      return string.IsNullOrEmpty(summary.ShowInGroupColumnFooter);
    }

    internal string GetGroupSummaryText(GridSummaryItem summaryItem, ColumnBase column, DevExpress.Xpf.Grid.TableView tableView, object value, bool isPrinting = false)
    {
      if (tableView == null || this.GetActualGroupSummaryDisplayMode(tableView, isPrinting) == GroupSummaryDisplayMode.Default)
        return summaryItem.GetGroupDisplayText((IFormatProvider) CultureInfo.CurrentCulture, ColumnBase.GetSummaryDisplayName(column, (SummaryItemBase) summaryItem), value, GroupRowData.GetColumnDisplayFormat(column));
      return summaryItem.GetGroupColumnDisplayText((IFormatProvider) CultureInfo.CurrentCulture, ColumnBase.GetSummaryDisplayName(column, (SummaryItemBase) summaryItem), value, GroupRowData.GetColumnDisplayFormat(column));
    }

    private GroupSummaryDisplayMode GetActualGroupSummaryDisplayMode(DevExpress.Xpf.Grid.TableView tableView, bool isPrinting)
    {
      if (!isPrinting)
        return tableView.GroupSummaryDisplayMode;
      return tableView.PrintGroupSummaryDisplayMode;
    }

    internal void InitGroupValue()
    {
      if (this.GroupValue != null)
        return;
      GridGroupValueData groupValueData = new GridGroupValueData((ColumnsRowDataBase) this);
      this.UpdateGroupValueData(groupValueData);
      this.GroupValue = groupValueData;
    }

    internal override void AssignFrom(RowsContainer parentRowsContainer, NodeContainer parentNodeContainer, RowNode rowNode, bool forceUpdate)
    {
      base.AssignFrom(parentRowsContainer, parentNodeContainer, rowNode, forceUpdate);
      this.InitGroupValue();
      this.GroupValue.Column = this.treeBuilder.GetGroupColumnByNode(this.DataRowNode);
      this.GroupValue.Value = this.treeBuilder.GetGroupValueByNode(this.DataRowNode);
      this.UpdateDisplayText();
      this.IsLastVisibleElementRow = this.GetIsLastVisibleElementRow(this.RowHandle.Value, true);
      this.IsLastHierarchicalRow = this.GetIsLastHierarchicalRow(this.RowHandle.Value);
      this.IsPreviewExpanded = this.GetIsPreviewExpanded(this.RowHandle.Value);
      if (this.groupRowStateClient != null)
        this.groupRowStateClient.UpdateIsPreviewExpanded();
      this.UpdateEditorHighlightingText();
      this.treeBuilder.UpdateGroupRowData((RowData) this);
    }

    protected override void SyncWithNode()
    {
      base.SyncWithNode();
      GroupNode groupNode = (GroupNode) this.DataRowNode;
      this.IsRowExpanded = groupNode.IsExpanded;
      this.IsExpanding = groupNode.IsExpanding;
      this.IsRowVisible = groupNode.IsRowVisible;
      this.GroupLevel = this.Level;
    }

    protected override void OnRowHandleChanged(RowHandle newValue)
    {
      base.OnRowHandleChanged(newValue);
      this.UpdateAllItemsSelected();
    }

    protected internal override void OnHeaderCaptionChanged()
    {
      this.UpdateDisplayText();
    }

    protected internal override void OnActualHeaderWidthChange()
    {
      foreach (GridGroupSummaryColumnData summaryColumnData in this.GetFixedGroupSummaryData())
        summaryColumnData.OnActualHeaderWidthChange();
    }

    private void UpdateDisplayText()
    {
      if (this.GroupValue == null)
        return;
      this.GroupValue.ColumnHeader = this.treeBuilder.GetGroupRowHeaderCaptionByNode(this.DataRowNode);
      this.GroupValue.Text = this.treeBuilder.GetGroupValueByNode(this.DataRowNode).ToString();
      this.GroupValue.HighlightingProperties = this.treeBuilder.GetGroupHighlightingPropertiesByNode(this.DataRowNode);
      this.UpdateClientGroupValue();
    }

    protected override void UpdateMasterDetailInfo(bool updateRowObjectIfRowExpanded, bool updateDetailRow)
    {
    }

    private bool GetIsLastVisibleElementRow(int rowHandle, bool checkParentRow)
    {
      if (!this.Grid.IsGroupRowExpanded(rowHandle) || this.Level > 0 && checkParentRow && this.GetLastRowHandle(this.Grid.GetParentRowHandle(rowHandle)) == rowHandle)
        return false;
      int lastRowHandle = this.GetLastRowHandle(rowHandle);
      if (!this.Grid.IsGroupRowHandle(lastRowHandle))
        return true;
      return this.GetIsLastVisibleElementRow(lastRowHandle, false);
    }

    private bool GetIsLastHierarchicalRow(int rowHandle)
    {
      return this.ControllerVisibleIndex == this.Grid.VisibleRowCount - 1;
    }

    private bool GetIsPreviewExpanded(int rowHandle)
    {
      if (this.RowPosition == RowPosition.Top || this.RowPosition == RowPosition.Single)
        return false;
      return this.Grid.IsGroupRowExpanded(this.Grid.GetRowHandleByVisibleIndex(this.ControllerVisibleIndex - 1));
    }

    private int GetLastRowHandle(int rowHandle)
    {
      return this.Grid.GridDataProvider.GetChildRowHandle(rowHandle, this.Grid.GridDataProvider.GetChildRowCount(rowHandle) - 1);
    }

    private void UpdateGroupColumnSummaries()
    {
      RowData.IterateNotNullDataCore<GridGroupSummaryColumnData>(this.updateGroupSummaryDataStrategy.DataCache, new Action<ColumnBase, GridGroupSummaryColumnData>(this.UpdateGridGroupSummaryData));
    }

    protected override void ValidateRowsContainer()
    {
      if (this.RowsContainer != null)
        return;
      this.RowsContainer = (RowsContainer) new DataRowsContainer(this.treeBuilder, this.Level + 1);
    }

    protected internal override GridColumnData CreateGridCellDataCore()
    {
      return (GridColumnData) new GridCellData((RowData) this);
    }

    protected internal override void UpdateEditorButtonVisibilities()
    {
    }

    internal override bool CanReuseCellData()
    {
      return false;
    }

    internal override IEnumerable<RowDataBase> GetCurrentViewChildItems()
    {
      if (this.RowsContainer == null)
        return base.GetCurrentViewChildItems();
      return (IEnumerable<RowDataBase>) this.RowsContainer.Items;
    }

    internal override void UpdateFixedLeftCellData()
    {
      base.UpdateFixedLeftCellData();
      this.GridView.PerformUpdateGroupSummaryDataAction((Action) (() => this.ReuseGroupSummaryDataNotVirtualized((Func<ColumnsRowDataBase, IList<GridGroupSummaryColumnData>>) (x => ((GroupRowData) x).FixedLeftGroupSummaryData), (Action<ColumnsRowDataBase, IList>) ((x, val) => ((GroupRowData) x).FixedLeftGroupSummaryData = (IList<GridGroupSummaryColumnData>) val), this.treeBuilder.GetFixedLeftColumns())));
      this.UpdateClientSummary();
    }

    internal override void UpdateFixedRightCellData()
    {
      base.UpdateFixedRightCellData();
      this.GridView.PerformUpdateGroupSummaryDataAction((Action) (() => this.ReuseGroupSummaryDataNotVirtualized((Func<ColumnsRowDataBase, IList<GridGroupSummaryColumnData>>) (x => ((GroupRowData) x).FixedRightGroupSummaryData), (Action<ColumnsRowDataBase, IList>) ((x, val) => ((GroupRowData) x).FixedRightGroupSummaryData = (IList<GridGroupSummaryColumnData>) val), this.treeBuilder.GetFixedRightColumns())));
      this.UpdateClientSummary();
    }

    protected override void UpdateFixedNoneCellDataCore(bool virtualized)
    {
      base.UpdateFixedNoneCellDataCore(virtualized);
      this.RefreshFixedNoneCellData(virtualized, false);
    }

    private void RefreshFixedNoneCellData(bool virtualized, bool updateGroupSummary)
    {
      this.GridView.PerformUpdateGroupSummaryDataAction((Action) (() =>
      {
        if (updateGroupSummary)
          this.UpdateGroupColumnSummaries();
        if (virtualized)
        {
          ITableView tableView = (ITableView) this.View;
          this.ReuseCellData<GridGroupSummaryColumnData>((Func<ColumnsRowDataBase, IList<GridGroupSummaryColumnData>>) (x => ((GroupRowData) x).FixedNoneGroupSummaryData), (Action<ColumnsRowDataBase, IList>) ((x, val) => ((GroupRowData) x).FixedNoneGroupSummaryData = (IList<GridGroupSummaryColumnData>) val), (UpdateCellDataStrategyBase<GridGroupSummaryColumnData>) this.updateGroupSummaryDataStrategy, tableView.ViewportVisibleColumns, tableView.TableViewBehavior.FixedNoneVisibleColumns.Count, 0);
          this.UpdateHasRightSibling(tableView.ViewportVisibleColumns ?? this.treeBuilder.GetFixedNoneColumns());
        }
        else
          this.ReuseGroupSummaryDataNotVirtualized((Func<ColumnsRowDataBase, IList<GridGroupSummaryColumnData>>) (x => ((GroupRowData) x).FixedNoneGroupSummaryData), (Action<ColumnsRowDataBase, IList>) ((x, val) => ((GroupRowData) x).FixedNoneGroupSummaryData = (IList<GridGroupSummaryColumnData>) val), this.treeBuilder.GetFixedNoneColumns());
      }));
      this.UpdateGroupSummariesCore();
    }

    internal void ReuseGroupSummaryDataNotVirtualized(Func<ColumnsRowDataBase, IList<GridGroupSummaryColumnData>> getter, Action<ColumnsRowDataBase, IList> setter, IList<ColumnBase> sourceColumns)
    {
      this.ReuseCellDataNotVirtualized<GridGroupSummaryColumnData>(getter, setter, (UpdateCellDataStrategyBase<GridGroupSummaryColumnData>) this.updateGroupSummaryDataStrategy, sourceColumns);
      this.UpdateHasRightSibling(sourceColumns);
    }

    private void UpdateHasRightSibling(IList<ColumnBase> sourceColumns)
    {
      if (sourceColumns == null)
        return;
      for (int index = 0; index < sourceColumns.Count; ++index)
      {
        ColumnBase column = sourceColumns[index];
        GridGroupSummaryColumnData summaryColumnData1 = this.SafeGetGroupSummaryColumnData(column);
        if (summaryColumnData1 != null)
        {
          bool flag1 = column.HasRightSibling;
          if (flag1)
          {
            bool flag2 = false;
            if (index < sourceColumns.Count - 1)
            {
              GridGroupSummaryColumnData summaryColumnData2 = this.SafeGetGroupSummaryColumnData(sourceColumns[index + 1]);
              if (summaryColumnData2 != null)
                flag2 = string.IsNullOrEmpty((string) summaryColumnData2.Value);
            }
            bool flag3 = string.IsNullOrEmpty((string) summaryColumnData1.Value);
            if (flag2 && flag3)
              flag1 = false;
          }
          summaryColumnData1.HasRightSibling = flag1;
        }
      }
    }

    protected internal GridGroupSummaryColumnData SafeGetGroupSummaryColumnData(ColumnBase column)
    {
      GridGroupSummaryColumnData summaryColumnData;
      this.updateGroupSummaryDataStrategy.DataCache.TryGetValue(column, out summaryColumnData);
      return summaryColumnData;
    }

    private GridGroupSummaryColumnData CreateNewGroupSummaryColumnData()
    {
      return new GridGroupSummaryColumnData(this);
    }

    protected internal virtual void UpdateGridGroupSummaryData(ColumnBase column, GridGroupSummaryColumnData cellData)
    {
      IList<GridSummaryItem> gridGroupSummaries = this.ExtractGridGroupSummaries(column.GroupSummariesCore);
      cellData.Column = column;
      cellData.HasSummary = gridGroupSummaries.Count > 0;
      cellData.SummaryTextInfo = this.GridView.GetGroupSummaryTextValues(column, this.RowHandle.Value, this.IsGroupFooter);
      cellData.OnActualHeaderWidthChange();
    }

    protected override bool ShouldUpdateCellDataCore(ColumnBase column, GridColumnData data)
    {
      return false;
    }

    protected internal void SetRowHandle(RowHandle rowHandle)
    {
      this.RowHandle = rowHandle;
    }

    protected override void OnDataContextChanged()
    {
      base.OnDataContextChanged();
      this.UpdateGroupValueData(this.GroupValue);
    }

    protected override void OnIsRowExpandedChanged()
    {
      base.OnIsRowExpandedChanged();
      if (this.groupRowStateClient == null)
        return;
      this.groupRowStateClient.UpdateIsRowExpanded();
    }

    protected override void OnIsRowVisibleChanged()
    {
      base.OnIsRowVisibleChanged();
      if (this.groupRowStateClient == null)
        return;
      this.groupRowStateClient.UpdateIsRowVisible();
    }

    private void UpdateGroupValueData(GridGroupValueData groupValueData)
    {
      if (groupValueData == null)
        return;
      groupValueData.Data = this.DataContext;
    }

    internal void SetAllItemsSelected(bool? allItemsSelected)
    {
      this.UpdateAllItemsSelectedLocker.DoLockedAction((Action) (() => this.AllItemsSelected = allItemsSelected));
    }

    internal void UpdateAllItemsSelected()
    {
      if (!this.GridView.ActualShowCheckBoxSelectorInGroupRow)
        return;
      this.SetAllItemsSelected(this.GridView.AreAllItemsSelected(this.RowHandle.Value));
    }

    private void OnAllItemsSelectedChanged()
    {
      if (!this.AllItemsSelected.HasValue)
        return;
      this.UpdateAllItemsSelectedLocker.DoIfNotLocked((Action) (() =>
      {
        if (this.AllItemsSelected.Value)
          this.GridView.SelectRowRecursively(this.RowHandle.Value);
        else
          this.GridView.UnselectRowRecursively(this.RowHandle.Value);
      }));
    }

    internal void SetGroupRowStateClient(IGroupRowStateClient rowStateClient)
    {
      if (this.groupRowStateClient != null)
        throw new InvalidOperationException();
      this.SetRowStateClient((IRowStateClient) rowStateClient);
      this.groupRowStateClient = rowStateClient;
      this.groupRowStateClient.UpdateGroupValue();
      this.groupRowStateClient.UpdateGroupRowStyle();
    }

    private void UpdateClientGroupValue()
    {
      if (this.groupRowStateClient == null)
        return;
      this.groupRowStateClient.UpdateGroupValue();
    }

    internal override void UpdateClientGroupValueTemplateSelector()
    {
      if (this.groupRowStateClient == null)
        return;
      this.groupRowStateClient.UpdateGroupValueTemplateSelector();
    }

    internal override void UpdateClientGroupRowTemplateSelector()
    {
      if (this.groupRowStateClient == null)
        return;
      this.groupRowStateClient.UpdateGroupRowTemplateSelector();
    }

    internal override void UpdateClientSummary()
    {
      if (this.groupRowStateClient == null)
        return;
      this.groupRowStateClient.UpdateSummary();
    }

    internal override void UpdateClientGroupRowStyle()
    {
      if (this.groupRowStateClient == null)
        return;
      this.groupRowStateClient.UpdateGroupValue();
      this.groupRowStateClient.UpdateGroupRowStyle();
    }

    internal override void UpdateClientCheckBoxSelector()
    {
      if (this.groupRowStateClient == null)
        return;
      this.groupRowStateClient.UpdateCheckBoxSelector();
    }

    internal override void OnIsReadyChanged()
    {
      if (this.groupRowStateClient != null)
        this.groupRowStateClient.UpdateIsReady();
      foreach (GridGroupSummaryColumnData summaryColumnData in this.GetFixedGroupSummaryData())
        summaryColumnData.UpdateSummaryIsReady();
    }

    internal override void UpdateClientFocusWithinState()
    {
      base.UpdateClientFocusWithinState();
      this.UpdateGroupSummaryClientFocusState();
    }

    internal override void UpdateClientIsFocused()
    {
      base.UpdateClientIsFocused();
      this.UpdateGroupSummaryClientFocusState();
    }

    private void UpdateGroupSummaryClientFocusState()
    {
      foreach (GridGroupSummaryColumnData summaryColumnData in this.GetFixedGroupSummaryData())
        summaryColumnData.UpdateSummaryClientFocusState();
    }

    private IEnumerable<GridGroupSummaryColumnData> GetFixedGroupSummaryData()
    {
      if (this.FixedNoneGroupSummaryData != null)
      {
        foreach (GridGroupSummaryColumnData summaryColumnData in (IEnumerable<GridGroupSummaryColumnData>) this.FixedNoneGroupSummaryData)
          yield return summaryColumnData;
      }
      if (this.FixedLeftGroupSummaryData != null)
      {
        foreach (GridGroupSummaryColumnData summaryColumnData in (IEnumerable<GridGroupSummaryColumnData>) this.FixedLeftGroupSummaryData)
          yield return summaryColumnData;
      }
      if (this.FixedRightGroupSummaryData != null)
      {
        foreach (GridGroupSummaryColumnData summaryColumnData in (IEnumerable<GridGroupSummaryColumnData>) this.FixedRightGroupSummaryData)
          yield return summaryColumnData;
      }
    }

    internal void UpdateCardLayout()
    {
      if (this.groupRowStateClient == null)
        return;
      this.groupRowStateClient.UpdateCardLayout();
    }

    protected internal override void UpdateEditorHighlightingText()
    {
      if (this.View == null || this.GroupValue == null || (this.GroupValue.Column == null || this.GroupValue.Column.ActualEditSettings == null))
        return;
      this.UpdateDisplayText();
    }

    protected internal override void UpdateEditorHighlightingText(TextHighlightingProperties textHighlightingProperties, string[] columns)
    {
      this.UpdateEditorHighlightingText();
    }

    internal class UpdateGroupSummaryDataStrategy : UpdateCellDataStrategyBase<GridGroupSummaryColumnData>
    {
      private readonly GroupRowData groupRowData;
      private readonly Dictionary<ColumnBase, GridGroupSummaryColumnData> groupSymmaryDataCache;

      public override bool CanReuseCellData
      {
        get
        {
          return true;
        }
      }

      public override Dictionary<ColumnBase, GridGroupSummaryColumnData> DataCache
      {
        get
        {
          return this.groupSymmaryDataCache;
        }
      }

      public UpdateGroupSummaryDataStrategy(GroupRowData groupRowData)
      {
        this.groupRowData = groupRowData;
        this.groupSymmaryDataCache = new Dictionary<ColumnBase, GridGroupSummaryColumnData>();
      }

      public override GridGroupSummaryColumnData CreateNewData()
      {
        return this.groupRowData.CreateNewGroupSummaryColumnData();
      }

      public override void UpdateData(ColumnBase column, GridGroupSummaryColumnData columnData)
      {
        this.groupRowData.UpdateGridGroupSummaryData(column, columnData);
      }
    }
  }
}
