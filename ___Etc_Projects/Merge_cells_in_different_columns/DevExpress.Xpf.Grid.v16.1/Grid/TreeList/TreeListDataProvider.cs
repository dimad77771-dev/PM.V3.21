// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListDataProvider
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Access;
using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Exceptions;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.Data.Helpers;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.GridData;
using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListDataProvider : GridDataProviderBase, IEvaluatorDataAccess
  {
    private int currentControllerRow = int.MinValue;
    private Locker dataUpdateLocker = new Locker();
    private int maxLevelCore = -1;
    private Dictionary<int, TreeListRowState> rowStates = new Dictionary<int, TreeListRowState>();
    private Locker recursiveCheckLocker = new Locker();
    internal const string CheckBoxExceptionMessage = "CheckBoxFieldName should correspond to a NullableBoolean or Boolean field in a data source.";
    private readonly TreeListView view;
    protected DataColumnInfoCollection columnsCollection;
    private bool nodesCountIsActual;
    private int nodesCount;
    private CriteriaOperator filterCriteria;
    private ISelectionController selectionController;
    private readonly ValueComparer valueComparer;
    protected bool allowCumulativeSummaryCalculation;
    private bool currentRowEditing;
    internal Func<object, bool> filterFitPredicate;
    private bool filterCriteriaChanged;
    private bool shouldUpdateColumnsUnboundType;
    private bool addingFirstNode;
    private bool autoPopulateColumns;
    private bool isRepopulateColumnsNeeded;

    public TreeListNodeCollection Nodes
    {
      get
      {
        return this.RootNode.Nodes;
      }
    }

    public TreeListView View
    {
      get
      {
        return this.view;
      }
    }

    public override int CurrentControllerRow
    {
      get
      {
        return this.currentControllerRow;
      }
      set
      {
        if (!this.IsValidRowHandle(value) && value != int.MinValue || this.CurrentControllerRow == value)
          return;
        this.currentControllerRow = value;
        this.OnCurrentControllerRowChanged();
      }
    }

    public TreeListNode CurrentNode
    {
      get
      {
        return this.GetNodeByRowHandle(this.CurrentControllerRow);
      }
    }

    public override int CurrentIndex
    {
      get
      {
        return this.GetRowVisibleIndexByHandle(this.CurrentControllerRow);
      }
    }

    public override ValueComparer ValueComparer
    {
      get
      {
        return this.valueComparer;
      }
    }

    internal TreeListNode RootNode { get; set; }

    protected override Type ItemTypeCore
    {
      get
      {
        return this.DataHelper.ItemType;
      }
    }

    protected internal TreeListDataHelperBase DataHelper { get; private set; }

    protected TreeListFilterHelper FilterHelper { get; private set; }

    protected internal TreeListNodesInfo NodesInfo { get; private set; }

    protected TreeListNodeComparer NodesComparer { get; private set; }

    protected Dictionary<TreeListNode, TreeListDataProvider.SummaryItem> SummaryData { get; private set; }

    public virtual int TotalNodesCount
    {
      get
      {
        if (!this.nodesCountIsActual)
        {
          this.nodesCount = this.GetTotalNodesCount();
          this.nodesCountIsActual = true;
        }
        return this.nodesCount;
      }
    }

    public override bool IsUpdateLocked
    {
      get
      {
        return this.dataUpdateLocker.IsLocked;
      }
    }

    public override ISummaryItemOwner TotalSummaryCore
    {
      get
      {
        if (this.View.DataControl == null)
          return (ISummaryItemOwner) null;
        return this.View.DataControl.TotalSummaryCore;
      }
    }

    public override int VisibleCount
    {
      get
      {
        return this.NodesInfo.TotalVisibleNodesCount;
      }
    }

    public override int DataRowCount
    {
      get
      {
        return this.NodesInfo.TotalNodesCount;
      }
    }

    public int MaxVisibleLevel { get; protected set; }

    public int MaxLevel
    {
      get
      {
        if (this.maxLevelCore == -1)
          this.maxLevelCore = this.CalcMaxNodeLevel(false);
        return this.maxLevelCore;
      }
    }

    public bool CanUseFastPropertyDescriptors
    {
      get
      {
        if (this.View.TreeDerivationMode == TreeDerivationMode.Selfreference)
          return !this.View.IsDesignTime;
        return false;
      }
    }

    protected bool IsSorted { get; private set; }

    protected bool IsFiltered { get; private set; }

    private IValidationAttributeOwner ValidationOwner
    {
      get
      {
        return (IValidationAttributeOwner) this.View.DataControl;
      }
    }

    public override bool IsCurrentRowEditing
    {
      get
      {
        return this.currentRowEditing;
      }
    }

    public override bool AllowEdit
    {
      get
      {
        return this.DataHelper.AllowEdit;
      }
    }

    public bool AllowRemove
    {
      get
      {
        return this.DataHelper.AllowRemove;
      }
    }

    public override bool AutoExpandAllGroups { get; set; }

    protected internal bool IsRecursiveNodesUpdateLocked { get; private set; }

    public override ISelectionController Selection
    {
      get
      {
        return this.selectionController;
      }
    }

    public TreeListSelectionController TreeListSelection
    {
      get
      {
        return (TreeListSelectionController) this.Selection;
      }
    }

    public override bool IsReady
    {
      get
      {
        return this.DataHelper.IsReady;
      }
    }

    public override CriteriaOperator FilterCriteria
    {
      get
      {
        return this.filterCriteria;
      }
      set
      {
        if (object.Equals((object) this.FilterCriteria, (object) value))
          return;
        CriteriaOperator filterCriteria = this.FilterCriteria;
        try
        {
          this.filterCriteria = CriteriaOperator.Clone(value);
          this.OnFilterExpressionChanged();
        }
        catch
        {
          this.filterCriteria = filterCriteria;
          this.OnFilterExpressionChanged();
        }
      }
    }

    protected Func<object, bool> FilterFitPredicate
    {
      get
      {
        if (this.filterFitPredicate == null && !object.ReferenceEquals((object) this.FilterCriteria, (object) null))
          this.filterFitPredicate = this.CreateFilterFitPredicate();
        return this.filterFitPredicate;
      }
    }

    public override DataColumnInfoCollection Columns
    {
      get
      {
        return this.columnsCollection;
      }
    }

    public override DataColumnInfoCollection DetailColumns
    {
      get
      {
        return new DataColumnInfoCollection();
      }
    }

    public bool IsUnboundMode
    {
      get
      {
        return this.DataHelper.IsUnboundMode;
      }
    }

    protected internal override BaseGridController DataController
    {
      get
      {
        return (BaseGridController) null;
      }
    }

    protected TreeListNodesState NodesState { get; private set; }

    protected internal bool HasSortInfo
    {
      get
      {
        if (this.View.DataControl != null)
          return this.View.DataControl.SortInfoCore.Count > 0;
        return false;
      }
    }

    private bool HasFilter
    {
      get
      {
        if (object.ReferenceEquals((object) this.FilterCriteria, (object) null))
          return this.View.IsCustomNodeFilterAssigned;
        return true;
      }
    }

    internal override bool IsServerMode
    {
      get
      {
        return false;
      }
    }

    internal override bool IsICollectionView
    {
      get
      {
        return false;
      }
    }

    internal override bool IsAsyncServerMode
    {
      get
      {
        return false;
      }
    }

    internal override bool IsAsyncOperationInProgress
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    internal override bool IsRefreshInProgress
    {
      get
      {
        return false;
      }
    }

    public override GridSummaryItemCollection GroupSummary
    {
      get
      {
        if (this.View.DataControl == null)
          return (GridSummaryItemCollection) null;
        return (GridSummaryItemCollection) this.View.DataControl.GroupSummaryCore;
      }
    }

    public override int GroupedColumnCount
    {
      get
      {
        return 0;
      }
    }

    public override ISummaryItemOwner GroupSummaryCore
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    internal bool IsRecursiveCheckInProgress
    {
      get
      {
        return this.recursiveCheckLocker.IsLocked;
      }
    }

    internal TreeListNode CurrentlyCheckingNode { get; private set; }

    public TreeListDataProvider(TreeListView view)
    {
      this.view = view;
      this.selectionController = (ISelectionController) new TreeListSelectionController(this);
      this.RootNode = (TreeListNode) new RootTreeListNode(this);
      this.SummaryData = new Dictionary<TreeListNode, TreeListDataProvider.SummaryItem>();
      this.FilterHelper = this.CreateFilterHelper();
      this.valueComparer = this.CreateValueComparer();
      this.NodesComparer = this.CreateNodesComparer();
      this.columnsCollection = new DataColumnInfoCollection();
      this.NodesInfo = new TreeListNodesInfo(this);
      this.NodesState = new TreeListNodesState(this);
      this.UpdateDataHelper();
    }

    private int GetTotalNodesCount()
    {
      int num = 0;
      foreach (TreeListNode treeListNode in (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.RootNode.Nodes))
        ++num;
      return num;
    }

    public override int GetRowLevelByControllerRow(int rowHandle)
    {
      if (!this.IsValidRowHandle(rowHandle))
        return -1;
      return this.GetRowLevelByControllerRowCore(rowHandle);
    }

    public override int GetActualRowLevel(int rowHandle, int level)
    {
      return level;
    }

    public override bool IsGroupVisible(GroupRowInfo groupInfo)
    {
      return true;
    }

    protected internal int GetRowLevelByControllerRowCore(int rowHandle)
    {
      return this.GetRowLevelByControllerRowCore(rowHandle, true);
    }

    protected internal int GetRowLevelByControllerRowCore(int rowHandle, bool actualLevel)
    {
      TreeListNode nodeByRowHandle = this.NodesInfo.GetNodeByRowHandle(rowHandle);
      if (nodeByRowHandle == null)
        return -1;
      if (!actualLevel)
        return nodeByRowHandle.Level;
      return nodeByRowHandle.ActualLevel;
    }

    public override int GetRowVisibleIndexByHandle(int rowHandle)
    {
      if (!this.IsValidRowHandle(rowHandle))
        return -1;
      return this.NodesInfo.GetVisibleIndexByNode(this.NodesInfo.GetNodeByRowHandle(rowHandle));
    }

    public override bool IsValidRowHandle(int rowHandle)
    {
      if (0 <= rowHandle)
        return rowHandle < this.NodesInfo.TotalNodesCount;
      return false;
    }

    internal bool IsValidVisibleIndex(int visibleIndex)
    {
      if (0 <= visibleIndex)
        return visibleIndex < this.NodesInfo.TotalVisibleNodesCount;
      return false;
    }

    public override object GetRowValue(int rowHandle)
    {
      TreeListNode nodeByRowHandle = this.GetNodeByRowHandle(rowHandle);
      if (nodeByRowHandle != null)
        return nodeByRowHandle.Content;
      return (object) null;
    }

    public TreeListNode GetNodeByRowHandle(int rowHandle)
    {
      if (!this.IsValidRowHandle(rowHandle))
        return (TreeListNode) null;
      return this.NodesInfo.GetNodeByRowHandle(rowHandle);
    }

    public TreeListNode GetNodeByVisibleIndex(int visibleIndex)
    {
      if (!this.IsValidVisibleIndex(visibleIndex))
        return (TreeListNode) null;
      return this.NodesInfo.GetNodeByVisibleIndex(visibleIndex);
    }

    public int GetVisibleIndexByNode(TreeListNode node)
    {
      if (node == null)
        return -1;
      return this.NodesInfo.GetVisibleIndexByNode(node);
    }

    public override object GetNodeIdentifier(int rowHandle)
    {
      return (object) this.GetNodeByRowHandle(rowHandle);
    }

    public int GetRowHandleByNode(TreeListNode node)
    {
      if (node == null)
        return int.MinValue;
      return this.NodesInfo.GetRowHandleByNode(node);
    }

    public override object GetRowValue(int rowHandle, string fieldName)
    {
      TreeListNode nodeByRowHandle = this.GetNodeByRowHandle(rowHandle);
      if (nodeByRowHandle != null)
        return this.DataHelper.GetValue(nodeByRowHandle, fieldName);
      return (object) null;
    }

    public virtual object GetNodeValue(TreeListNode node, string fieldName)
    {
      return this.DataHelper.GetValue(node, fieldName);
    }

    public override object GetRowValue(int rowHandle, DataColumnInfo info)
    {
      return this.DataHelper.GetValue(this.GetNodeByRowHandle(rowHandle), info);
    }

    public override int GetControllerRow(int visibleIndex)
    {
      if (!this.IsValidVisibleIndex(visibleIndex))
        return int.MinValue;
      return this.NodesInfo.GetRowHandleByNode(this.NodesInfo.GetNodeByVisibleIndex(visibleIndex));
    }

    public override bool IsRowVisible(int rowHandle)
    {
      if (!this.IsValidRowHandle(rowHandle))
        return false;
      return this.NodesInfo.GetVisibleIndexByNode(this.NodesInfo.GetNodeByRowHandle(rowHandle)) > -1;
    }

    public override int GetChildRowCount(int rowHandle)
    {
      if (!this.IsValidRowHandle(rowHandle))
        return -1;
      return this.NodesInfo.GetNodeByRowHandle(rowHandle).Nodes.Count;
    }

    public override int GetChildRowHandle(int rowHandle, int childIndex)
    {
      if (!this.IsValidRowHandle(rowHandle))
        return int.MinValue;
      TreeListNode nodeByRowHandle = this.NodesInfo.GetNodeByRowHandle(rowHandle);
      if (childIndex < 0 || childIndex >= nodeByRowHandle.Nodes.Count)
        return int.MinValue;
      return this.NodesInfo.GetRowHandleByNode(nodeByRowHandle.Nodes[childIndex]);
    }

    public override void Synchronize(IList<GridSortInfo> sortList, int groupCount, CriteriaOperator filterCriteria)
    {
      if (this.View.DataControl == null)
        return;
      this.BeginUpdateCore();
      try
      {
        this.FilterCriteria = filterCriteria;
      }
      catch
      {
      }
      finally
      {
        this.EndUpdateCore();
      }
    }

    public virtual void ResetCurrentPosition()
    {
      if (this.View.FocusedRowHandleChangedLocker.IsLocked || this.View.DataControl != null && this.View.DataControl.CurrentItemChangedLocker.IsLocked || this.IsUpdateLocked)
        return;
      if (this.CurrentControllerRow == int.MinValue)
        this.CurrentControllerRow = this.Nodes.Count > 0 ? 0 : int.MinValue;
      if (!this.view.IsMultiSelection || !this.view.IsMultiRowSelection || (this.CurrentControllerRow == int.MinValue || !this.View.DataControl.AllowUpdateSelectedItems()) || (this.TreeListSelection.Count != 0 || !this.View.DataControl.IsSelectionInitialized && this.View.DataControl.SelectedItems != null))
        return;
      this.TreeListSelection.BeginSelection();
      try
      {
        this.TreeListSelection.SetSelected(this.CurrentControllerRow, true);
      }
      finally
      {
        if (this.TreeListSelection.GetSelected(this.CurrentControllerRow))
          this.TreeListSelection.EndSelection();
        else
          this.TreeListSelection.CancelSelection();
      }
    }

    protected virtual void DoSortNodes(IList<GridSortInfo> sortList, TreeListNode parentNode)
    {
      if (!this.DataHelper.IsReady)
        return;
      if (this.IsUpdateLocked)
        return;
      try
      {
        if (!this.View.AutoScrollOnSorting)
          this.View.ScrollIntoViewLocker.Lock();
        TreeListNode currentNode = this.CurrentNode;
        if (!this.IsSorted && sortList.Count == 0)
          return;
        this.NodesComparer.SetSortInfo(sortList);
        this.View.DataControl.SynchronizeSortInfo((IList<IColumnInfo>) this.GetSortInfo(sortList), 0);
        this.view.OnStartSorting();
        try
        {
          this.SortNodes(parentNode, true);
        }
        finally
        {
          this.view.OnEndSorting();
        }
        this.IsSorted = sortList.Count > 0;
        if (currentNode == null || this.NodesState.FocusedRow != null || this.View.FocusedRowHandleChangedLocker.IsLocked)
          return;
        this.CurrentControllerRow = this.GetRowHandleByNode(currentNode);
      }
      finally
      {
        if (!this.View.AutoScrollOnSorting)
          this.View.ScrollIntoViewLocker.Unlock();
      }
    }

    private List<IColumnInfo> GetSortInfo(IList<GridSortInfo> sortList)
    {
      List<IColumnInfo> columnInfoList = new List<IColumnInfo>();
      foreach (GridSortInfo sort in (IEnumerable<GridSortInfo>) sortList)
        columnInfoList.Add((IColumnInfo) new DummyColumnInfo(sort.FieldName, sort.GetSortOrder()));
      return columnInfoList;
    }

    protected internal virtual void SortNodes(TreeListNode parentNode, bool recursive = true)
    {
      this.SortNodes(parentNode.Nodes, recursive);
    }

    protected internal void SortNodes(TreeListNodeCollection nodes, bool recursive)
    {
      this.NodesInfo.SetDirty();
      nodes.DoSort((IComparer<TreeListNode>) this.NodesComparer);
      if (!recursive)
        return;
      foreach (TreeListNode node in (Collection<TreeListNode>) nodes)
        this.SortNodes(node.Nodes, recursive);
    }

    public override void SynchronizeSummary()
    {
      if (this.IsUpdateLocked)
        return;
      this.UpdateTotalSummary();
    }

    public override void UpdateTotalSummary()
    {
      if (this.TotalSummaryCore == null)
        return;
      this.CalcSummary(this.TotalSummaryCore.Concat<DevExpress.Xpf.Grid.SummaryItemBase>((IEnumerable<DevExpress.Xpf.Grid.SummaryItemBase>) this.View.ViewBehavior.GetServiceSummaries()));
    }

    protected internal virtual bool CanCalculateSummary(IEnumerable<DevExpress.Xpf.Grid.SummaryItemBase> summary)
    {
      if (!summary.Any<DevExpress.Xpf.Grid.SummaryItemBase>())
        return false;
      bool flag = false;
      foreach (DevExpress.Xpf.Grid.SummaryItemBase summaryItemBase in summary)
      {
        flag = summaryItemBase.SummaryType != SummaryItemType.Custom ? summaryItemBase.SummaryType != SummaryItemType.None : this.View.HasCustomSummary || summaryItemBase is ServiceSummaryItem;
        if (flag)
          break;
      }
      return flag;
    }

    protected virtual void CalcSummary(IEnumerable<DevExpress.Xpf.Grid.SummaryItemBase> summary)
    {
      this.CalcSummaryCore(summary, this.SummaryData, false, (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.RootNode));
    }

    protected virtual void ClearSummaryData(Dictionary<TreeListNode, TreeListDataProvider.SummaryItem> summaryData)
    {
      summaryData.Clear();
    }

    protected internal virtual void CalcSummaryCore(IEnumerable<DevExpress.Xpf.Grid.SummaryItemBase> summary, Dictionary<TreeListNode, TreeListDataProvider.SummaryItem> summaryData, bool calcOnlySelectedItems, IEnumerable<TreeListNode> nodes)
    {
      this.ClearSummaryData(summaryData);
      if (!this.RootNode.HasChildren || !this.CanCalculateSummary(summary))
        return;
      foreach (TreeListNode node in nodes)
      {
        if (node.IsVisible && (!calcOnlySelectedItems || ((IEnumerable<int>) this.View.GetSelectedRowHandlesCore()).Contains<int>(node.RowHandle)))
        {
          foreach (DevExpress.Xpf.Grid.SummaryItemBase summaryItemBase in summary)
          {
            if (this.CanCalculateSummaryItem(summaryItemBase))
              this.UpdateSummaryValue(node, summaryData, summaryItemBase, node);
          }
        }
      }
      foreach (TreeListNode key in summaryData.Keys)
      {
        foreach (TreeListSummaryValue listSummaryValue in summaryData[key].Values)
          listSummaryValue.Finish(RootTreeListNode.IsRootNode(key) ? (TreeListNode) null : key);
      }
    }

    protected virtual bool CanCalculateSummaryItem(DevExpress.Xpf.Grid.SummaryItemBase item)
    {
      if (item.SummaryType == SummaryItemType.None)
        return false;
      ServiceSummaryItem serviceSummaryItem = item as ServiceSummaryItem;
      if (serviceSummaryItem != null)
      {
        CustomServiceSummaryItemType? serviceSummaryItemType = serviceSummaryItem.CustomServiceSummaryItemType;
        if ((serviceSummaryItemType.GetValueOrDefault() != CustomServiceSummaryItemType.SortedList ? 0 : (serviceSummaryItemType.HasValue ? 1 : 0)) != 0)
        {
          DataColumnInfo ci = this.Columns[serviceSummaryItem.FieldName];
          if (ci == null)
            return false;
          if (!typeof (IComparable).IsAssignableFrom(ci.GetDataType()))
            return this.IsUnboundWithExpression(ci);
          return true;
        }
      }
      return true;
    }

    protected bool IsUnboundWithExpression(DataColumnInfo ci)
    {
      TreeListUnboundPropertyDescriptor propertyDescriptor = ci.PropertyDescriptor as TreeListUnboundPropertyDescriptor;
      if (propertyDescriptor == null)
        return false;
      return !string.IsNullOrEmpty(propertyDescriptor.UnboundInfo.Expression);
    }

    protected void UpdateSummaryValue(TreeListNode summaryOwner, Dictionary<TreeListNode, TreeListDataProvider.SummaryItem> summaryData, DevExpress.Xpf.Grid.SummaryItemBase item, TreeListNode node)
    {
      bool flag = RootTreeListNode.IsRootNode(summaryOwner);
      TreeListNode index = flag ? summaryOwner : summaryOwner.parentNodeCore;
      if (index == null)
        return;
      TreeListDataProvider.SummaryItem summaryItem1;
      if (!summaryData.TryGetValue(index, out summaryItem1))
      {
        summaryItem1 = new TreeListDataProvider.SummaryItem();
        summaryData[index] = summaryItem1;
      }
      TreeListSummaryValue summaryValue;
      if (!summaryItem1.TryGetValue(item, out summaryValue))
      {
        summaryValue = this.CreateSummaryValue(item);
        summaryValue.Start(flag ? (TreeListNode) null : index);
        summaryItem1[item] = summaryValue;
      }
      bool valueOrDefault = item.IgnoreNullValues.GetValueOrDefault(this.View.SummariesIgnoreNullValues);
      if (!flag)
      {
        summaryValue.Calculate(node, valueOrDefault);
        if (this.allowCumulativeSummaryCalculation)
        {
          TreeListDataProvider.SummaryItem summaryItem2 = (TreeListDataProvider.SummaryItem) null;
          summaryData.TryGetValue(node, out summaryItem2);
          TreeListSummaryValue val = (TreeListSummaryValue) null;
          if (summaryItem2 != null)
            summaryItem2.TryGetValue(item, out val);
          if (val != null)
            summaryValue.Calculate(val, valueOrDefault);
        }
      }
      if (!TreeListSummarySettings.GetIsRecursive((DependencyObject) item) || flag)
        return;
      this.UpdateSummaryValue(index, summaryData, item, node);
    }

    public override object GetTotalSummaryValue(DevExpress.Xpf.Grid.SummaryItemBase item)
    {
      return this.GetSummaryValue(this.RootNode, item);
    }

    protected internal virtual object GetSummaryValueCore(TreeListNode node, DevExpress.Xpf.Grid.SummaryItemBase item, Dictionary<TreeListNode, TreeListDataProvider.SummaryItem> summaryData)
    {
      if (!summaryData.ContainsKey(node))
        return (object) null;
      if (!summaryData[node].ContainsKey(item))
        return (object) null;
      return summaryData[node][item].Value;
    }

    public object GetSummaryValue(TreeListNode node, DevExpress.Xpf.Grid.SummaryItemBase item)
    {
      return this.GetSummaryValueCore(node, item, this.SummaryData);
    }

    internal TreeListDataProvider.SummaryItem GetRootSummaryItem()
    {
      TreeListDataProvider.SummaryItem summaryItem = (TreeListDataProvider.SummaryItem) null;
      this.SummaryData.TryGetValue(this.RootNode, out summaryItem);
      return summaryItem;
    }

    protected TreeListSummaryValue CreateSummaryValue(DevExpress.Xpf.Grid.SummaryItemBase item)
    {
      switch (item.SummaryType)
      {
        case SummaryItemType.Sum:
          return (TreeListSummaryValue) new TreeListSummarySumValue(item);
        case SummaryItemType.Min:
          return (TreeListSummaryValue) new TreeListSummaryMinValue(item);
        case SummaryItemType.Max:
          return (TreeListSummaryValue) new TreeListSummaryMaxValue(item);
        case SummaryItemType.Count:
          return (TreeListSummaryValue) new TreeListSummaryCountValue(item);
        case SummaryItemType.Average:
          return (TreeListSummaryValue) new TreeListSummaryAvgValue(item);
        case SummaryItemType.Custom:
          return this.CreateCustomSummaryValue(item);
        default:
          throw new ArgumentException();
      }
    }

    private TreeListSummaryValue CreateCustomSummaryValue(DevExpress.Xpf.Grid.SummaryItemBase item)
    {
      ServiceSummaryItem serviceSummaryItem = item as ServiceSummaryItem;
      if (serviceSummaryItem == null)
        return (TreeListSummaryValue) new TreeListSummaryCustomValue(item, this.View);
      CustomServiceSummaryItemType? serviceSummaryItemType = serviceSummaryItem.CustomServiceSummaryItemType;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      CustomServiceSummaryItemType?& local = @serviceSummaryItemType;
      // ISSUE: explicit reference operation
      CustomServiceSummaryItemType valueOrDefault = (^local).GetValueOrDefault();
      // ISSUE: explicit reference operation
      if ((^local).HasValue)
      {
        switch (valueOrDefault)
        {
          case CustomServiceSummaryItemType.DateTimeAverage:
            return (TreeListSummaryValue) new TreeListSummaryDateTimeAvarage(serviceSummaryItem);
          case CustomServiceSummaryItemType.SortedList:
            return (TreeListSummaryValue) new TreeListSummarySortedList(serviceSummaryItem);
          case CustomServiceSummaryItemType.Unique:
            return (TreeListSummaryValue) new TreeListSummaryUniqueDuplicate((DevExpress.Xpf.Grid.SummaryItemBase) serviceSummaryItem, UniqueDuplicateRule.Unique);
          case CustomServiceSummaryItemType.Duplicate:
            return (TreeListSummaryValue) new TreeListSummaryUniqueDuplicate((DevExpress.Xpf.Grid.SummaryItemBase) serviceSummaryItem, UniqueDuplicateRule.Duplicate);
        }
      }
      throw new ArgumentException();
    }

    public override bool EndCurrentRowEdit()
    {
      if (!this.IsReady || !this.IsCurrentRowEditing)
        return true;
      TreeListNode currentNode = this.CurrentNode;
      if (currentNode == null)
        return true;
      try
      {
        if (!this.View.RaiseValidateNode(this.CurrentControllerRow, currentNode.Content))
          return false;
        this.EndDataRowEdit(currentNode);
      }
      catch (Exception ex)
      {
        ControllerRowExceptionEventArgs args = new ControllerRowExceptionEventArgs(this.CurrentControllerRow, currentNode.Content, ex);
        this.View.RaiseInvalidNodeException(currentNode, args);
        if (args.Action != ExceptionAction.CancelAction)
          return false;
        this.CancelDataRowEdit(this.CurrentNode);
        return true;
      }
      this.StopCurrentRowEdit();
      this.OnEndCurrentRowEdit();
      return true;
    }

    public override void BeginCurrentRowEdit()
    {
      if (!this.IsReady || this.CurrentNode == null || this.IsCurrentRowEditing)
        return;
      this.BeginDataRowEdit(this.CurrentNode);
      this.currentRowEditing = true;
    }

    public override void CancelCurrentRowEdit()
    {
      if (!this.IsReady || this.CurrentNode == null || !this.IsCurrentRowEditing)
        return;
      this.CancelDataRowEdit(this.CurrentNode);
      this.StopCurrentRowEdit();
      this.OnEndCurrentRowEdit();
    }

    protected virtual void OnEndCurrentRowEdit()
    {
      if (this.IsCurrentRowEditing || this.IsUpdateLocked)
        return;
      TreeListNode nodeByRowHandle = this.GetNodeByRowHandle(this.CurrentControllerRow);
      if (nodeByRowHandle == null)
        return;
      this.OnNodeCollectionChanged(nodeByRowHandle, NodeChangeType.Content, false, (string) null);
    }

    public override void SetRowValue(RowHandle rowHandle, DataColumnInfo info, object value)
    {
      if (rowHandle.Value == this.CurrentControllerRow)
        this.BeginCurrentRowEdit();
      TreeListNode nodeByRowHandle = this.GetNodeByRowHandle(rowHandle.Value);
      this.DataHelper.SetValue(nodeByRowHandle, info.Name, value);
      if (this.DataHelper.SupportNotifications)
        return;
      this.OnNodeCollectionChanged(nodeByRowHandle, NodeChangeType.Content, true, info.Name);
    }

    public virtual void SetNodeValue(TreeListNode node, string fieldName, object value)
    {
      if (node == this.CurrentNode)
        this.BeginCurrentRowEdit();
      this.DataHelper.SetValue(node, fieldName, value);
    }

    protected void BeginDataRowEdit(TreeListNode node)
    {
      IEditableObject editableObject = this.GetEditableObject(node);
      if (editableObject == null)
        return;
      editableObject.BeginEdit();
    }

    protected void CancelDataRowEdit(TreeListNode node)
    {
      IEditableObject editableObject = this.GetEditableObject(node);
      if (editableObject == null)
        return;
      editableObject.CancelEdit();
    }

    protected void EndDataRowEdit(TreeListNode node)
    {
      IEditableObject editableObject = this.GetEditableObject(node);
      if (editableObject == null)
        return;
      editableObject.EndEdit();
    }

    protected virtual void StopCurrentRowEdit()
    {
      this.currentRowEditing = false;
    }

    protected IEditableObject GetEditableObject(TreeListNode node)
    {
      if (node != null)
        return node.Content as IEditableObject;
      return (IEditableObject) null;
    }

    protected virtual void OnCurrentControllerRowChanged()
    {
      this.View.OnCurrentIndexChanged();
    }

    public override void RefreshRow(int rowHandle)
    {
      if (!this.IsValidRowHandle(rowHandle))
        return;
      this.View.UpdateRowDataByRowHandle(rowHandle, (UpdateRowDataDelegate) (rowData => rowData.UpdateData()));
    }

    public void ReloadChildNodes(TreeListNode node)
    {
      if (node == null || !object.ReferenceEquals((object) node.DataProvider, (object) this))
        return;
      this.DataHelper.ReloadChildNodes(node, (IEnumerable) null);
    }

    public override DependencyObject GetRowState(int controllerRow, bool createNewIfNotExist)
    {
      if (this.rowStates.ContainsKey(controllerRow))
        return (DependencyObject) this.rowStates[controllerRow];
      if (!createNewIfNotExist)
        return (DependencyObject) null;
      this.rowStates[controllerRow] = new TreeListRowState();
      return (DependencyObject) this.rowStates[controllerRow];
    }

    public override RowDetailContainer GetRowDetailContainer(int controllerRow, Func<RowDetailContainer> createContainerDelegate, bool createNewIfNotExist)
    {
      throw new NotSupportedException();
    }

    public override ErrorInfo GetErrorInfo(RowHandle rowHandle, string fieldName)
    {
      ErrorInfo info = new ErrorInfo();
      DataColumnInfo columnInfo = this.Columns[fieldName];
      if (columnInfo == null || columnInfo.PropertyDescriptor is TreeListUnboundPropertyDescriptor)
        return info;
      TreeListNode nodeByRowHandle = this.GetNodeByRowHandle(rowHandle.Value);
      if (nodeByRowHandle == null)
        return info;
      IDataErrorInfo rowErrorInfo = this.GetRowErrorInfo(nodeByRowHandle.Content);
      IDXDataErrorInfo rowDxErrorInfo = this.GetRowDXErrorInfo(nodeByRowHandle.Content);
      if (rowErrorInfo != null && rowDxErrorInfo == null)
      {
        string str = rowErrorInfo[columnInfo.Name];
        if (!string.IsNullOrEmpty(str))
          info.ErrorText = str;
      }
      if (rowDxErrorInfo != null)
        rowDxErrorInfo.GetPropertyError(columnInfo.Name, info);
      if (this.ValidationOwner != null && !this.ValidationOwner.CalculateValidationAttribute(columnInfo.Name, rowHandle.Value))
        return info;
      string attributesErrorText = this.GetValidationAttributesErrorText(rowHandle.Value, columnInfo);
      if (!string.IsNullOrEmpty(attributesErrorText))
        info.ErrorText = attributesErrorText;
      return info;
    }

    protected internal override object GetRowErrorInfoController(int rowHandle)
    {
      TreeListNode nodeByRowHandle = this.GetNodeByRowHandle(rowHandle);
      if (nodeByRowHandle == null)
        return (object) null;
      return nodeByRowHandle.Content;
    }

    protected internal override ErrorInfo GetErrorInfo(int rowHandle, DataColumnInfo ci, object row)
    {
      ErrorInfo info = new ErrorInfo();
      if (ci == null || ci.PropertyDescriptor is TreeListUnboundPropertyDescriptor)
        return info;
      IDataErrorInfo rowErrorInfo = this.GetRowErrorInfo(row);
      IDXDataErrorInfo rowDxErrorInfo = this.GetRowDXErrorInfo(row);
      if (rowErrorInfo != null && rowDxErrorInfo == null)
      {
        string str = rowErrorInfo[ci.Name];
        if (!string.IsNullOrEmpty(str))
          info.ErrorText = str;
      }
      if (rowDxErrorInfo != null)
        rowDxErrorInfo.GetPropertyError(ci.Name, info);
      if (this.ValidationOwner != null && !this.ValidationOwner.CalculateValidationAttribute(ci.Name, rowHandle))
        return info;
      string attributesErrorText = this.GetValidationAttributesErrorText(rowHandle, ci);
      if (!string.IsNullOrEmpty(attributesErrorText))
        info.ErrorText = attributesErrorText;
      return info;
    }

    protected override object GetRowValueForValidationAttribute(int controllerRow, string columnName)
    {
      return this.GetRowValue(controllerRow, columnName);
    }

    public override ErrorInfo GetErrorInfo(RowHandle rowHandle)
    {
      ErrorInfo info = new ErrorInfo();
      TreeListNode nodeByRowHandle = this.GetNodeByRowHandle(rowHandle.Value);
      if (nodeByRowHandle == null)
        return info;
      IDataErrorInfo rowErrorInfo = this.GetRowErrorInfo(nodeByRowHandle.Content);
      IDXDataErrorInfo rowDxErrorInfo = this.GetRowDXErrorInfo(nodeByRowHandle.Content);
      if (rowErrorInfo != null && rowDxErrorInfo == null)
      {
        string error = rowErrorInfo.Error;
        if (!string.IsNullOrEmpty(error))
          info.ErrorText = error;
      }
      if (rowDxErrorInfo != null)
        rowDxErrorInfo.GetError(info);
      return info;
    }

    protected IDataErrorInfo GetRowErrorInfo(object row)
    {
      return row as IDataErrorInfo;
    }

    protected IDXDataErrorInfo GetRowDXErrorInfo(object row)
    {
      return row as IDXDataErrorInfo;
    }

    protected internal void LockRecursiveNodesUpdate()
    {
      this.IsRecursiveNodesUpdateLocked = true;
    }

    protected internal void UnlockRecursiveNodesUpdate()
    {
      this.IsRecursiveNodesUpdateLocked = false;
    }

    protected virtual Func<object, bool> CreateFilterFitPredicate()
    {
      try
      {
        return CriteriaCompiler.ToUntypedPredicate(this.FilterCriteria, CriteriaCompilerDescriptor.Get(this.GetFilterDescriptorCollection()));
      }
      catch
      {
        return (Func<object, bool>) null;
      }
    }

    public virtual Func<object, bool> CreateFilterFitPredicate(CriteriaOperator criteria)
    {
      try
      {
        return CriteriaCompiler.ToUntypedPredicate(criteria, CriteriaCompilerDescriptor.Get(this.GetFilterDescriptorCollection()));
      }
      catch
      {
        return (Func<object, bool>) null;
      }
    }

    public override bool CanColumnSortCore(string fieldName)
    {
      return this.Columns[fieldName] != null;
    }

    public override bool IsGroupRowHandle(int rowHandle)
    {
      return false;
    }

    public override void MakeRowVisible(int rowHandle)
    {
      TreeListNode nodeByRowHandle = this.GetNodeByRowHandle(rowHandle);
      if (nodeByRowHandle == null)
        return;
      for (TreeListNode parentNode = nodeByRowHandle.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
        parentNode.IsExpanded = true;
    }

    public override void BeginUpdate()
    {
      this.Selection.BeginSelection();
      this.SaveNodesState(false);
      this.BeginUpdateCore();
    }

    internal void SaveNodesState(bool supressLocker = false)
    {
      this.NodesState.SaveNodesState(supressLocker);
      this.NodesState.SaveCurrentFocus(supressLocker);
    }

    public override void EndUpdate()
    {
      if (this.DataHelper.RequiresReloadDataOnEndUpdate)
        this.DataHelper.LoadData();
      this.UpdateColumnsUnboundTypeIfNeeded();
      this.RestoreNodesState(false);
      this.EndUpdateCore();
      this.Selection.EndSelection();
    }

    internal void RestoreNodesState(bool supressLocker = false)
    {
      this.NodesState.RestoreNodesState(supressLocker);
      this.NodesState.RestoreCurrentFocus();
    }

    public void CancelUpdate()
    {
      this.dataUpdateLocker.Unlock();
    }

    public override void RePopulateColumns()
    {
      this.Columns.Clear();
      this.ResetValidationAttributes();
      this.DataHelper.PopulateColumns();
    }

    public virtual ExpressionEvaluator CreateExpressionEvaluator(CriteriaOperator criteriaOperator, out Exception e)
    {
      e = (Exception) null;
      if (this.IsReady)
      {
        if (this.Columns.Count != 0)
        {
          try
          {
            return new ExpressionEvaluator(this.GetFilterDescriptorCollection(), criteriaOperator, false) { DataAccess = (IEvaluatorDataAccess) this };
          }
          catch (Exception ex)
          {
            e = ex;
            return (ExpressionEvaluator) null;
          }
        }
      }
      return (ExpressionEvaluator) null;
    }

    public override void DoRefresh()
    {
      this.DoRefresh(true);
    }

    protected internal void DoRefresh(bool keepNodesState)
    {
      if (!this.IsReady)
        return;
      if (keepNodesState)
        this.SaveNodesState(false);
      this.BeginUpdateCore();
      try
      {
        this.DataHelper.LoadData();
      }
      finally
      {
        if (keepNodesState)
          this.RestoreNodesState(false);
        this.EndUpdateCore();
      }
    }

    public override int GetParentRowHandle(int rowHandle)
    {
      TreeListNode nodeByRowHandle = this.GetNodeByRowHandle(rowHandle);
      if (nodeByRowHandle != null)
      {
        TreeListNode visibleParent = nodeByRowHandle.VisibleParent;
        if (visibleParent != null)
          return this.NodesInfo.GetRowHandleByNode(visibleParent);
      }
      return int.MinValue;
    }

    public override object[] GetUniqueColumnValues(ColumnBase column, bool includeFilteredOut, bool roundDataTime, bool implyNullLikeEmptyStringWhenFiltering)
    {
      DataColumnInfo column1 = this.Columns[column.FieldName];
      if (column1 != null)
        return this.FilterHelper.GetUniqueColumnValuesCore(column1, includeFilteredOut, roundDataTime, column.ColumnFilterMode == ColumnFilterMode.DisplayText);
      return (object[]) null;
    }

    public override CriteriaOperator CalcColumnFilterCriteriaByValue(ColumnBase column, object columnValue)
    {
      DataColumnInfo columnInfo = this.Columns[column.FieldName];
      if (columnInfo == null)
        return (CriteriaOperator) null;
      bool roundDateTime = column.ActualEditSettings is DateEditSettings || column.RoundDateTimeForColumnFilter;
      return this.FilterHelper.CalcColumnFilterCriteriaByValue(columnInfo, columnValue, roundDateTime, column.ColumnFilterMode == ColumnFilterMode.DisplayText);
    }

    public override object CorrectFilterValueType(Type columnFilteredType, object filteredValue, IFormatProvider provider = null)
    {
      return TreeListFilterHelper.CorrectFilterValueType(columnFilteredType, filteredValue);
    }

    public virtual TreeListNode FindNodeByValue(object value)
    {
      if (!this.IsReady)
        return (TreeListNode) null;
      foreach (TreeListNode treeListNode in (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.Nodes))
      {
        if (object.Equals(value, treeListNode.Content))
          return treeListNode;
      }
      return (TreeListNode) null;
    }

    public virtual IList<TreeListNode> FindNodesByValue(object value)
    {
      List<TreeListNode> treeListNodeList = new List<TreeListNode>();
      if (this.IsReady)
      {
        foreach (TreeListNode treeListNode in (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.Nodes))
        {
          if (object.Equals(value, treeListNode.Content))
            treeListNodeList.Add(treeListNode);
        }
      }
      return (IList<TreeListNode>) treeListNodeList;
    }

    public TreeListNode FindNodeById(int id)
    {
      if (!this.IsReady)
        return (TreeListNode) null;
      foreach (TreeListNode treeListNode in (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.Nodes))
      {
        if (treeListNode.Id == id)
          return treeListNode;
      }
      return (TreeListNode) null;
    }

    public virtual TreeListNode FindNodeByValue(string fieldName, object value)
    {
      if (!this.IsReady)
        return (TreeListNode) null;
      DataColumnInfo columnInfo = this.Columns[fieldName];
      if (columnInfo == null)
        return (TreeListNode) null;
      try
      {
        value = columnInfo.ConvertValue(value);
      }
      catch
      {
        return (TreeListNode) null;
      }
      foreach (TreeListNode node in (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.Nodes))
      {
        if (object.Equals(this.DataHelper.GetValue(node, columnInfo), value))
          return node;
      }
      return (TreeListNode) null;
    }

    public TreeListNode FindVisibleNode(int rowHandle)
    {
      if (!this.IsReady)
        return (TreeListNode) null;
      return this.NodesInfo.FindVisibleNode(rowHandle);
    }

    public void CloseActiveEditor()
    {
      this.View.CloseEditor();
    }

    public void DoSortNodes()
    {
      this.DoSortNodes(this.RootNode);
    }

    public void DoSortNodes(TreeListNode parentNode)
    {
      if (this.View.DataControl == null)
        return;
      this.DoSortNodes((IList<GridSortInfo>) this.View.DataControl.SortInfoCore, parentNode);
    }

    public virtual TreeListFilterHelper CreateFilterHelper()
    {
      return new TreeListFilterHelper(this);
    }

    public override void DeleteRow(RowHandle rowHandle)
    {
      this.DeleteNode(this.GetNodeByRowHandle(rowHandle.Value), true);
    }

    public void DeleteNode(TreeListNode node, bool deleteChildren)
    {
      if (node == null || !this.AllowRemove)
        return;
      this.DataHelper.DeleteNode(node, deleteChildren, true);
    }

    protected internal virtual bool OnNodeExpandingOrCollapsing(TreeListNode treeListNode)
    {
      bool flag = !treeListNode.IsExpanded ? this.view.RaiseNodeExpanding(treeListNode) : this.view.RaiseNodeCollapsing(treeListNode);
      if (flag)
        this.DataHelper.NodeExpandingCollapsing(treeListNode);
      return flag;
    }

    protected internal virtual void OnNodeExpandedOrCollapsed(TreeListNode treeListNode)
    {
      if (treeListNode.IsExpanded)
      {
        this.view.CheckFocusedNodeOnExpand(treeListNode);
        this.view.RaiseNodeExpanded(treeListNode);
      }
      else
      {
        this.view.CheckFocusedNodeOnCollapse(treeListNode);
        this.view.RaiseNodeCollapsed(treeListNode);
      }
    }

    protected internal virtual void OnNodeCheckStateChanged(TreeListNode treeListNode)
    {
      this.view.RaiseNodeCheckStateChanged(treeListNode);
    }

    protected internal virtual void OnNodeChanged(TreeListNode treeListNode, NodeChangeType changeType)
    {
      this.view.RaiseNodeChanged(treeListNode, changeType);
    }

    protected internal virtual TreeListNode CreateNode(object content)
    {
      return new TreeListNode(content);
    }

    protected virtual TreeListNodeComparer CreateNodesComparer()
    {
      return new TreeListNodeComparer(this);
    }

    protected virtual ValueComparer CreateValueComparer()
    {
      return new ValueComparer();
    }

    protected virtual TreeListDataHelperBase CreateDataHelper()
    {
      if (this.DataSource == null)
        return (TreeListDataHelperBase) new TreeListUnboundDataHelper(this);
      switch (this.View.TreeDerivationMode)
      {
        case TreeDerivationMode.ChildNodesSelector:
          return (TreeListDataHelperBase) new TreeListHierarchicalDataHelper(this, this.DataSource);
        case TreeDerivationMode.HierarchicalDataTemplate:
          return (TreeListDataHelperBase) new TreeListHierarchicalDataTemplateHelper(this, this.DataSource);
        default:
          return (TreeListDataHelperBase) new TreeListSelfReferenceDataHelper(this, this.DataSource);
      }
    }

    protected virtual void OnFilterExpressionChanged()
    {
      this.ResetFilterFitPredicate();
      this.filterCriteriaChanged = true;
      this.DoRefreshCore(false);
    }

    private void ResetFilterFitPredicate()
    {
      this.filterFitPredicate = (Func<object, bool>) null;
    }

    protected internal void UpdateDataHelper()
    {
      if (this.DataHelper != null)
        this.DataHelper.Dispose();
      this.DataHelper = this.CreateDataHelper();
      this.shouldUpdateColumnsUnboundType = true;
    }

    private void UpdateColumnsUnboundTypeIfNeeded()
    {
      if (!this.shouldUpdateColumnsUnboundType || !this.DataHelper.IsLoaded || this.View.DisplayMemberBindingClient == null)
        return;
      this.shouldUpdateColumnsUnboundType = false;
      bool reloadDataOnEndUpdate = this.DataHelper.RequiresReloadDataOnEndUpdate;
      this.DataHelper.RequiresReloadDataOnEndUpdate = false;
      try
      {
        this.View.DisplayMemberBindingClient.UpdateColumns();
        this.View.DisplayMemberBindingClient.UpdateSimpleBinding();
      }
      finally
      {
        this.DataHelper.RequiresReloadDataOnEndUpdate = reloadDataOnEndUpdate;
      }
    }

    protected internal override void OnDataSourceChanged()
    {
      this.BeginUpdateCore();
      try
      {
        this.NodesState.LockSaveNodesState();
        this.ResetFilterFitPredicate();
        this.UpdateDataHelper();
        this.RePopulateColumns();
        this.currentControllerRow = int.MinValue;
        this.DoRefresh(false);
        this.View.OnDataSourceChanged();
      }
      finally
      {
        this.UpdateColumnsUnboundTypeIfNeeded();
        this.NodesState.UnlockSaveNodesState();
        this.EndUpdateCore();
        this.ResetCurrentPosition();
      }
      this.ScheduleRepopulateColumnsIfNeeded();
    }

    private void ScheduleRepopulateColumnsIfNeeded()
    {
      if (this.DataHelper.IsLoaded)
        return;
      this.isRepopulateColumnsNeeded = true;
    }

    protected virtual void DoRefreshCore(bool forceResort = true)
    {
      if (this.IsUpdateLocked)
        return;
      if (forceResort)
        this.DoSortNodes();
      this.DoFilterNodes();
      this.NodesInfo.SetDirty();
      this.View.UpdateFocusedNode();
      this.SynchronizeSummary();
      this.UpdateRows();
    }

    protected internal void DoFilterNodes()
    {
      this.DoFilterNodes(this.RootNode);
      TreeListNode currentNode = this.CurrentNode;
      if (currentNode != null && !currentNode.IsVisible)
        this.CurrentControllerRow = 0;
      bool flag = false;
      this.TreeListSelection.BeginSelection();
      try
      {
        foreach (TreeListNode selectedNode in this.TreeListSelection.GetSelectedNodes())
        {
          if (!selectedNode.IsVisible)
          {
            this.TreeListSelection.SetSelected(selectedNode.RowHandle, false);
            flag = true;
          }
        }
      }
      finally
      {
        if (flag)
          this.TreeListSelection.EndSelection();
        else
          this.TreeListSelection.CancelSelection();
      }
    }

    protected internal void DoFilterNodes(TreeListNode parentNode)
    {
      if (this.IsUpdateLocked || !this.IsFiltered && !this.HasFilter)
        return;
      this.DoFilterNodesCore(parentNode);
      this.IsFiltered = this.HasFilter;
      this.filterCriteriaChanged = false;
    }

    protected virtual void DoFilterNodesCore(TreeListNode parent)
    {
      foreach (TreeListNode node in (Collection<TreeListNode>) parent.Nodes)
      {
        if (!this.DoFilterNode(node) || this.View.FilterMode != TreeListFilterMode.Standard)
          this.DoFilterNodesCore(node);
      }
    }

    protected virtual bool DoFilterNode(TreeListNode node)
    {
      try
      {
        node.IsVisible = this.CalcNodeVisibility(node);
        node.IsExpandedSetInternally = false;
        if (this.View.FilterMode == TreeListFilterMode.Standard && !node.IsVisible)
          node.ProcessNodeAndDescendantsAction((Func<TreeListNode, bool>) (n =>
          {
            n.IsVisible = false;
            return true;
          }));
        if (this.View.FilterMode == TreeListFilterMode.Extended && node.IsVisible)
          this.UpdateParentVisibilityStateOnFilter(node);
        if (node.IsVisible && this.HasFilter && (this.filterCriteriaChanged && this.View.ExpandNodesOnFiltering))
          this.UpdateParentExpandStateOnFilter(node);
        return !node.IsVisible;
      }
      catch
      {
        return true;
      }
    }

    protected virtual void UpdateParentVisibilityStateOnFilter(TreeListNode node)
    {
      for (TreeListNode parentNode = node.ParentNode; parentNode != null && !parentNode.IsVisible; parentNode = parentNode.ParentNode)
        parentNode.IsVisible = true;
    }

    protected virtual void UpdateParentExpandStateOnFilter(TreeListNode node)
    {
      this.BeginUpdateCore();
      try
      {
        for (TreeListNode parentNode = node.ParentNode; parentNode != null && !parentNode.IsExpandedSetInternally; parentNode = parentNode.ParentNode)
        {
          parentNode.IsExpanded = true;
          parentNode.IsExpandedSetInternally = parentNode.IsExpanded;
        }
      }
      finally
      {
        this.CancelUpdate();
      }
    }

    protected virtual bool CalcNodeVisibility(TreeListNode node)
    {
      bool? nullable = this.IsNodeUserFit(node);
      if (nullable.HasValue)
        return nullable.Value;
      if (this.FilterFitPredicate == null)
        return true;
      return this.FilterFitPredicate((object) node);
    }

    protected virtual bool? IsNodeUserFit(TreeListNode node)
    {
      return this.View.RaiseCustomNodeFilter(node);
    }

    internal void OnNodeCollectionChanging(TreeListNode node, NodeChangeType changeType)
    {
      this.addingFirstNode = false;
      if (changeType == NodeChangeType.Remove)
        this.UnselectRemovedNode(node);
      if (changeType != NodeChangeType.Add || this.Nodes.Count != 0)
        return;
      this.addingFirstNode = true;
    }

    protected internal virtual void OnNodeCollectionChanged(TreeListNode node, NodeChangeType changeType, bool raiseNodeChangedEvent = true, string propertyName = null)
    {
      if (this.IsRecursiveNodesUpdateLocked)
        return;
      if (raiseNodeChangedEvent)
        this.OnNodeChanged(node, changeType);
      if (changeType == NodeChangeType.Add && !this.DataHelper.IsLoading)
        node.ForceUpdateExpandState();
      if (changeType == NodeChangeType.ExpandButtonVisibility || changeType == NodeChangeType.Image || changeType == NodeChangeType.IsCheckBoxEnabled)
        this.UpdateRow(node);
      else if (changeType == NodeChangeType.CheckBox)
      {
        if (this.IsRecursiveCheckingAllowed(node))
        {
          node.CheckBoxUpdateLocker.DoLockedActionIfNotLocked((Action) (() => this.UpdateRows()));
        }
        else
        {
          node.CheckBoxUpdateLocker.DoIfNotLocked((Action) (() => this.UpdateRow(node)));
          node.isCheckStateInitialized = true;
        }
      }
      else
      {
        bool flag = this.View.NeedWatchRowChanged();
        int? nullable = new int?();
        if (flag)
          nullable = new int?(node.RowHandle);
        if (changeType == NodeChangeType.Remove || changeType == NodeChangeType.Add)
        {
          this.nodesCountIsActual = false;
          this.rowStates.Clear();
          this.NodesInfo.SetDirty();
        }
        if (changeType == NodeChangeType.Add)
        {
          this.OnNodeAdded(node);
          if (flag)
            this.View.CurrentRowChanged(ListChangedType.ItemAdded, node.RowHandle, new int?(nullable.Value));
        }
        if (changeType == NodeChangeType.Remove)
        {
          if (flag)
          {
            int rowHandle = node.RowHandle;
            if (rowHandle != int.MinValue)
              this.View.CurrentRowChanged(ListChangedType.ItemDeleted, rowHandle, new int?(rowHandle));
          }
          this.OnNodeRemoved(node);
        }
        if (changeType == NodeChangeType.Content)
        {
          this.OnNodeContentChanged(node, propertyName);
          if (flag)
            this.View.CurrentRowChanged(ListChangedType.ItemChanged, node.RowHandle, new int?(nullable.Value));
        }
        if (changeType != NodeChangeType.Expand)
          return;
        this.NodesInfo.SetDirty(true);
        this.UpdateRows();
      }
    }

    protected internal virtual void UpdateNodeInternal(TreeListNode node, bool filterChildren, bool sortNodes)
    {
      if (this.IsUpdateLocked || this.IsCurrentRowEditing)
        return;
      this.UpdateNodeVisiblility(node, filterChildren);
      if (sortNodes)
        this.DoSortInternal(node);
      this.UpdateNodeSummary(node);
      this.View.UpdateColumnsTotalSummary();
    }

    protected virtual void UpdateNodeSummary(TreeListNode node)
    {
      this.SynchronizeSummary();
    }

    private void OnNodeContentChanged(TreeListNode node, string propertyName)
    {
      if (node.CheckBoxUpdateLocker.IsLocked)
      {
        this.View.EnqueueImmediateAction((Action) (() => this.OnNodeContentChanged(node, propertyName)));
      }
      else
      {
        bool sortNodes = !this.HasSortInfo || string.IsNullOrEmpty(propertyName) || this.View.DataControl.SortInfoCore.FindIndex<GridSortInfo>((Predicate<GridSortInfo>) (s => s.FieldName == propertyName)) >= 0;
        this.UpdateNodeInternal(node, false, sortNodes);
        this.DoRefreshRow(node);
        if (node != this.CurrentNode || this.View.DataControl == null)
          return;
        this.View.DataControl.UpdateCurrentCellValue();
      }
    }

    private void DoRefreshRow(TreeListNode node)
    {
      if (this.IsSorted || this.IsFiltered || this.View.ViewBehavior.GetServiceSummaries().Any<ServiceSummaryItem>())
        this.UpdateRows();
      else
        this.UpdateRow(node);
    }

    private void UpdateNodeVisiblility(TreeListNode node, bool filterChildren)
    {
      if (this.View.FilterMode == TreeListFilterMode.Extended)
      {
        TreeListNode rootNode = node.RootNode;
        this.DoFilterNode(rootNode);
        this.DoFilterNodes(rootNode);
      }
      else
      {
        this.DoFilterNode(node);
        if (filterChildren)
          this.DoFilterNodes(node);
      }
      if (!this.HasFilter)
        return;
      this.NodesInfo.SetDirty(true);
    }

    private void OnNodeRemoved(TreeListNode node)
    {
      if (this.IsUpdateLocked)
        return;
      this.SynchronizeSummary();
      bool flag = this.View.FocusedNode != null;
      if (this.View.FocusedNode == node && this.NodesState.FocusedRow == null)
        this.View.FocusedNode = this.FindVisibleNode(this.View.FocusedRowHandle);
      this.UpdateRows();
      if (!flag)
        return;
      this.ResetCurrentPosition();
    }

    private void OnNodeAdded(TreeListNode node)
    {
      if (this.IsUpdateLocked)
        return;
      this.UpdateNodeInternal(node, true, true);
      this.View.UpdateFocusedNode();
      this.UpdateColumnsUnboundTypeIfNeeded();
      this.DataHelper.RecalcNodeIdsIfNeeded();
      this.UpdateRows();
      if (!this.addingFirstNode)
        return;
      this.ResetCurrentPosition();
    }

    private void DoSortInternal(TreeListNode node)
    {
      if (this.IsCurrentRowEditing)
        return;
      this.DoSortNodes(node.parentNodeCore);
    }

    private void UnselectRemovedNode(TreeListNode node)
    {
      if (!this.View.IsMultiSelection || this.View.DataControl == null || !this.View.DataControl.AllowUpdateSelectedItems())
        return;
      this.Selection.BeginSelection();
      this.SelectNodesRecursive(node, false);
      this.Selection.EndSelection();
    }

    protected void SelectNodesRecursive(TreeListNode node, bool selected)
    {
      if (node == null)
        return;
      this.TreeListSelection.SetSelected(node, selected);
      foreach (TreeListNode node1 in (Collection<TreeListNode>) node.Nodes)
        this.SelectNodesRecursive(node1, selected);
    }

    protected internal virtual void UpdateRows()
    {
      if (this.IsUpdateLocked)
        return;
      this.maxLevelCore = -1;
      this.MaxVisibleLevel = this.CalcMaxNodeLevel(true);
      this.view.UpdateRows();
    }

    protected internal virtual void UpdateRow(TreeListNode node)
    {
      if (this.IsUpdateLocked || node == null)
        return;
      this.RefreshRow(node.RowHandle);
    }

    protected internal virtual bool UseFirstRowTypeWhenPopulatingColumns(Type itemType)
    {
      return itemType.FullName == ListDataControllerHelper.UseFirstRowTypeWhenPopulatingColumnsTypeName;
    }

    protected internal override void ForceClearData()
    {
      if (this.DataHelper != null)
        this.DataHelper.Dispose();
      this.NodesInfo.SetDirty();
    }

    protected internal virtual UnboundColumnInfoCollection GetUnboundColumns()
    {
      return this.GetUnboundColumnsCore(this.View.GetColumns().Concat<IColumnInfo>(this.View.ViewBehavior.GetServiceUnboundColumns()));
    }

    protected internal virtual ComplexColumnInfoCollection GetComplexColumns()
    {
      ComplexColumnInfoCollection columnInfoCollection = new ComplexColumnInfoCollection();
      foreach (IColumnInfo column in (IEnumerable<IColumnInfo>) this.View.GetColumns())
      {
        if (column.UnboundType == UnboundColumnType.Bound && column.FieldName.Contains(".") && this.Columns[column.FieldName] == null)
          columnInfoCollection.Add(column.FieldName);
      }
      return columnInfoCollection;
    }

    protected internal virtual PropertyDescriptorCollection GetFilterDescriptorCollection()
    {
      PropertyDescriptorCollection descriptorCollection = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
      foreach (DataColumnInfo column in (CollectionBase) this.Columns)
      {
        if (this.ShouldFilterByDisplayText(column))
          descriptorCollection.Add((PropertyDescriptor) new TreeListDisplayTextPropertyDescriptor(this, column.Name));
        else
          descriptorCollection.Add((PropertyDescriptor) new TreeListValuePropertyDescriptor(this, column.PropertyDescriptor));
        descriptorCollection.Add((PropertyDescriptor) new TreeListSearchDisplayTextPropertyDescriptor(this, column.Name));
      }
      return descriptorCollection;
    }

    protected virtual bool ShouldFilterByDisplayText(DataColumnInfo dataColumn)
    {
      ColumnBase columnBase = this.View.ColumnsCore[dataColumn.Name];
      if (columnBase != null)
        return columnBase.ColumnFilterMode == ColumnFilterMode.DisplayText;
      return false;
    }

    protected internal void ToggleExpandedAllNodes(bool expand)
    {
      this.ToggleExpandedAllChildNodes(this.RootNode, expand);
    }

    protected internal void ExpandToLevel(int maxLevel)
    {
      this.BeginUpdateCore();
      this.SaveFocusState();
      try
      {
        this.RootNode.ProcessNodeAndDescendantsAction((Func<TreeListNode, bool>) (node =>
        {
          if (maxLevel >= 0 && node.Level > maxLevel)
            return false;
          node.IsExpanded = true;
          return true;
        }));
      }
      finally
      {
        this.RestoreFocusState();
        this.EndUpdateCore();
      }
    }

    protected internal void ToggleExpandedAllChildNodes(TreeListNode parent, bool expand)
    {
      if (parent == null)
        return;
      this.BeginUpdateCore();
      if (expand)
        this.SaveFocusState();
      try
      {
        parent.ToggleExpandedAllChildrenCore(expand);
      }
      finally
      {
        if (expand)
          this.RestoreFocusState();
        this.CancelUpdate();
        if (!this.IsUpdateLocked)
        {
          if (expand)
          {
            this.DoSortNodes(parent);
            this.DoFilterNodes(parent);
            this.SynchronizeSummary();
          }
          this.NodesInfo.SetDirty();
          this.View.UpdateFocusedNode();
          this.UpdateRows();
          this.View.UpdateScrollBarAnnotations();
        }
      }
    }

    protected internal virtual ExpressionEvaluator CreateExpressionEvaluator(string criteria, out Exception e)
    {
      e = (Exception) null;
      if (string.IsNullOrEmpty(criteria))
        return (ExpressionEvaluator) null;
      CriteriaOperator criteriaOperator;
      try
      {
        criteriaOperator = CriteriaOperator.Parse(criteria, (object[]) null);
      }
      catch (CriteriaParserException ex)
      {
        criteriaOperator = CriteriaOperator.Parse(string.Empty, (object[]) null);
      }
      return this.CreateExpressionEvaluator(criteriaOperator, out e);
    }

    protected internal virtual object GetUnboundData(object p, string propName, object value)
    {
      ColumnBase columnBase = this.View.ColumnsCore[propName];
      if (columnBase != null && columnBase.Binding != null)
      {
        int rowHandleByNode = this.GetRowHandleByNode(p as TreeListNode);
        if (rowHandleByNode != int.MinValue)
          return columnBase.DisplayMemberBindingCalculator.GetValue(rowHandleByNode, -1);
      }
      return this.View.RaiseCustomUnboundColumnData(p, propName, value, true);
    }

    private object GetRow(TreeListNode node)
    {
      if (node == null)
        return (object) null;
      return node.Content;
    }

    protected internal virtual void SetUnboundData(object p, string propName, object value)
    {
      ColumnBase columnBase = this.View.ColumnsCore[propName];
      if (columnBase != null && columnBase.Binding != null)
      {
        int rowHandleByNode = this.GetRowHandleByNode(p as TreeListNode);
        if (rowHandleByNode != int.MinValue)
        {
          columnBase.DisplayMemberBindingCalculator.SetValue(rowHandleByNode, value);
          return;
        }
      }
      this.View.RaiseCustomUnboundColumnData(p, propName, value, false);
    }

    internal override DataColumnInfo GetActualColumnInfo(string fieldName)
    {
      return this.Columns[fieldName] ?? this.DetailColumns[fieldName];
    }

    protected internal IList<TreeListNode> GetAllNodes()
    {
      return this.GetAllNodesCore((Func<TreeListNode, bool>) (node => true), (Func<TreeListNode, bool>) (node => true));
    }

    protected internal IList<TreeListNode> GetAllVisibleNodes()
    {
      return this.GetAllNodesCore((Func<TreeListNode, bool>) (node => node.IsExpanded), (Func<TreeListNode, bool>) (node => node.IsVisible));
    }

    private IList<TreeListNode> GetAllNodesCore(Func<TreeListNode, bool> shouldCollectChildren, Func<TreeListNode, bool> shouldProcessNode)
    {
      List<TreeListNode> treeListNodeList = new List<TreeListNode>();
      this.CollectNodes(this.Nodes, (IList<TreeListNode>) treeListNodeList, shouldCollectChildren, shouldProcessNode);
      return (IList<TreeListNode>) treeListNodeList;
    }

    internal override void EnsureRowLoaded(int rowHandle)
    {
    }

    private void CollectNodes(TreeListNodeCollection nodes, IList<TreeListNode> list, Func<TreeListNode, bool> shouldCollectChildren, Func<TreeListNode, bool> shouldProcessNode)
    {
      foreach (TreeListNode node in (Collection<TreeListNode>) nodes)
      {
        if (shouldProcessNode(node))
          list.Add(node);
        if (shouldCollectChildren(node))
          this.CollectNodes(node.Nodes, list, shouldCollectChildren, shouldProcessNode);
      }
    }

    protected internal void SaveExpansionState()
    {
      this.NodesState.SaveNodesState(false);
    }

    protected internal void SaveFocusState()
    {
      this.NodesState.SaveCurrentFocus(false);
    }

    protected internal void BeginUpdateCore()
    {
      this.dataUpdateLocker.Lock();
    }

    protected internal void EndUpdateCore()
    {
      this.dataUpdateLocker.Unlock();
      this.DoRefreshCore(true);
    }

    protected internal void RestoreFocusState()
    {
      this.NodesState.RestoreCurrentFocus();
    }

    private int CalcMaxNodeLevel(bool visibleOnly)
    {
      return Math.Max(0, this.GetMaxNodeLevel(this.RootNode, -1, visibleOnly));
    }

    private int GetMaxNodeLevel(TreeListNode rootNode, int level, bool visibleOnly)
    {
      int val2 = level++;
      if (!visibleOnly || rootNode.IsExpanded)
      {
        foreach (TreeListNode node in (Collection<TreeListNode>) rootNode.Nodes)
        {
          if (node.IsVisible)
            val2 = Math.Max(this.GetMaxNodeLevel(node, level, visibleOnly), val2);
          else if (this.View.FilterMode == TreeListFilterMode.Smart && node.HasVisibleChildren)
            val2 = Math.Max(this.GetMaxNodeLevel(node, level - 1, visibleOnly), val2);
        }
      }
      return val2;
    }

    protected internal virtual void OnSelectionChanged(SelectionChangedEventArgs e)
    {
      this.View.OnSelectionChanged(e);
    }

    object IEvaluatorDataAccess.GetValue(PropertyDescriptor descriptor, object theObject)
    {
      return this.DataHelper.GetValue(theObject as TreeListNode, descriptor);
    }

    public override int FindRowByRowValue(object value)
    {
      TreeListNode nodeByValue = this.FindNodeByValue(value);
      if (nodeByValue == null)
        return int.MinValue;
      return this.GetRowHandleByNode(nodeByValue);
    }

    public override int FindRowByValue(string fieldName, object value)
    {
      TreeListNode nodeByValue = this.FindNodeByValue(fieldName, value);
      if (nodeByValue == null)
        return int.MinValue;
      return this.GetRowHandleByNode(nodeByValue);
    }

    public override object GetRowByListIndex(int listIndex)
    {
      return this.DataHelper.GetDataRowByListIndex(listIndex);
    }

    public override object GetCellValueByListIndex(int listSourceRowIndex, string fieldName)
    {
      return this.DataHelper.GetCellValueByListIndex(listSourceRowIndex, fieldName);
    }

    public override int GetRowHandleByListIndex(int listIndex)
    {
      object rowByListIndex = this.GetRowByListIndex(listIndex);
      if (rowByListIndex == null)
        return int.MinValue;
      return this.GetRowHandleByNode(this.FindNodeByValue(rowByListIndex));
    }

    public override int GetListIndexByRowHandle(int rowHandle)
    {
      TreeListNode nodeByRowHandle = this.GetNodeByRowHandle(rowHandle);
      if (nodeByRowHandle != null)
        return this.DataHelper.GetListIndexByDataRow(nodeByRowHandle.Content);
      return -1;
    }

    internal override void ScheduleAutoPopulateColumns()
    {
      this.autoPopulateColumns = true;
    }

    internal override void CancelAllGetRows()
    {
    }

    internal override void EnsureAllRowsLoaded(int firstRowIndex, int rowsCount)
    {
    }

    public override void ExpandAll()
    {
      throw new NotImplementedException();
    }

    public override void CollapseAll()
    {
      throw new NotImplementedException();
    }

    public override void ChangeGroupExpanded(int controllerRow, bool recursive)
    {
      throw new NotImplementedException();
    }

    internal override int GetGroupIndex(string fieldName)
    {
      throw new NotImplementedException();
    }

    public override bool IsGroupRow(int visibleIndex)
    {
      throw new NotImplementedException();
    }

    public override bool IsGroupRowExpanded(int controllerRow)
    {
      throw new NotImplementedException();
    }

    public override void UpdateGroupSummary()
    {
      throw new NotImplementedException();
    }

    public override int GetControllerRowByGroupRow(int groupRowHandle)
    {
      throw new NotImplementedException();
    }

    public override object GetGroupRowValue(int rowHandle, ColumnBase column)
    {
      throw new NotImplementedException();
    }

    public override object GetGroupRowValue(int rowHandle)
    {
      throw new NotImplementedException();
    }

    public override bool TryGetGroupSummaryValue(int rowHandle, DevExpress.Xpf.Grid.SummaryItemBase item, out object value)
    {
      throw new NotImplementedException();
    }

    internal void CustomNodeSort(TreeListCustomColumnSortEventArgs e)
    {
      this.View.RaiseCustomColumnSort(e);
    }

    internal void UpdateNodeId(TreeListNode node)
    {
      this.DataHelper.UpdateNodeId(node);
    }

    internal void TryRepopulateColumns()
    {
      if (!this.isRepopulateColumnsNeeded || !this.DataHelper.IsLoaded)
        return;
      bool flag = this.autoPopulateColumns && this.ShouldTryRepopulateColumns();
      this.DataHelper.PopulateColumns();
      if (flag && this.View.DataControl != null)
        this.View.DataControl.PopulateColumns();
      this.isRepopulateColumnsNeeded = false;
    }

    private bool ShouldTryRepopulateColumns()
    {
      if (this.View.GetColumns().Count == 0)
        return true;
      if (this.Columns.Count == 1)
        return this.Columns[0].PropertyDescriptor is SimpleListPropertyDescriptor;
      return false;
    }

    protected internal void OnFilterModeChanged()
    {
      this.DoRefreshCore(false);
    }

    internal void DoRecursiveCheckAction(Action action)
    {
      this.recursiveCheckLocker.DoLockedAction(action);
    }

    protected internal bool? GetObjectIsChecked(TreeListNode node)
    {
      if (!string.IsNullOrEmpty(this.View.CheckBoxFieldName))
      {
        try
        {
          object nodeValue = this.View.GetNodeValue(node, this.View.CheckBoxFieldName);
          if (this.View.CheckBoxValueConverter != null && this.Columns[this.View.CheckBoxFieldName] != null)
            return (bool?) this.View.CheckBoxValueConverter.Convert(nodeValue, typeof (bool?), (object) null, (CultureInfo) null);
          return (bool?) nodeValue;
        }
        catch (Exception ex)
        {
          if (this.View.IsDesignTime)
            return new bool?();
          if (ex is InvalidCastException)
            throw new InvalidCastException("CheckBoxFieldName should correspond to a NullableBoolean or Boolean field in a data source.");
          throw ex;
        }
      }
      else
      {
        if (node != null)
          return node.IsChecked;
        return new bool?();
      }
    }

    protected internal void SetObjectIsChecked(TreeListNode node, bool? checkStatus)
    {
      try
      {
        if (string.IsNullOrEmpty(this.View.CheckBoxFieldName))
          return;
        this.CloseActiveEditor();
        object obj = (object) checkStatus;
        if (this.View.CheckBoxValueConverter != null && this.Columns[this.View.CheckBoxFieldName] != null)
        {
          Type targetType = this.GetTargetType(node, this.Columns[this.View.CheckBoxFieldName]);
          obj = this.View.CheckBoxValueConverter.ConvertBack(obj, targetType, (object) null, (CultureInfo) null);
        }
        this.CurrentlyCheckingNode = node;
        this.SetNodeValue(node, this.View.CheckBoxFieldName, obj);
      }
      finally
      {
        this.CurrentlyCheckingNode = (TreeListNode) null;
      }
    }

    private Type GetTargetType(TreeListNode node, DataColumnInfo columnInfo)
    {
      PropertyDescriptor propertyDescriptor1 = columnInfo.PropertyDescriptor;
      UnitypeDataPropertyDescriptor propertyDescriptor2 = propertyDescriptor1 as UnitypeDataPropertyDescriptor;
      if (propertyDescriptor2 != null)
      {
        PropertyDescriptor properyDescriptor = propertyDescriptor2.GetProperyDescriptor(node.Content);
        if (properyDescriptor != null)
          propertyDescriptor1 = properyDescriptor;
      }
      return propertyDescriptor1.PropertyType;
    }

    protected internal bool IsRecursiveCheckingAllowed(TreeListNode node)
    {
      if (this.View.AllowRecursiveNodeChecking)
        return node.isCheckStateInitialized;
      return false;
    }

    internal void InitNodesIsChecked()
    {
      foreach (TreeListNode treeListNode in (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.Nodes))
        treeListNode.InitIsChecked();
    }

    public static bool IsUnitypeColumn(DataColumnInfo info)
    {
      if (!(info.PropertyDescriptor is UnitypeDataPropertyDescriptor))
        return info.PropertyDescriptor is UnitypeComplexPropertyDescriptor;
      return true;
    }

    protected internal virtual void OnColumnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      if (this.View.TreeDerivationMode == TreeDerivationMode.Selfreference || e.Action != NotifyCollectionChangedAction.Add && e.Action != NotifyCollectionChangedAction.Reset)
        return;
      foreach (IDataColumnInfo dataColumnInfo in (IEnumerable) this.View.ColumnsCore)
      {
        if (this.Columns[dataColumnInfo.FieldName] == null)
        {
          this.RePopulateColumns();
          break;
        }
      }
    }

    protected internal void UpdateNodesExpandState(TreeListNodeCollection nodes, bool updateEvaluator = true)
    {
      foreach (TreeListNode treeListNode in (IEnumerable<TreeListNode>) new TreeListNodeIterator(nodes))
      {
        if (updateEvaluator)
          treeListNode.UpdateExpandStateBindingEvaluator();
        treeListNode.ForceUpdateExpandState();
      }
    }

    protected internal override object GetFormatInfoCellValueByListIndex(int listIndex, string fieldName)
    {
      return this.GetNodeValue(this.GetNodeByRowHandle(listIndex), fieldName);
    }

    internal override bool IsUnboundColumnInfo(DataColumnInfo info)
    {
      if (info != null && info.PropertyDescriptor is TreeListUnboundPropertyDescriptor)
        return true;
      return base.IsUnboundColumnInfo(info);
    }

    internal override bool IsFilteredByRowHandle(int rowHandle)
    {
      TreeListNode nodeByRowHandle = this.GetNodeByRowHandle(rowHandle);
      if (nodeByRowHandle == null)
        return false;
      return nodeByRowHandle.IsFiltered;
    }

    protected internal class SummaryItem : Dictionary<DevExpress.Xpf.Grid.SummaryItemBase, TreeListSummaryValue>
    {
    }
  }
}
