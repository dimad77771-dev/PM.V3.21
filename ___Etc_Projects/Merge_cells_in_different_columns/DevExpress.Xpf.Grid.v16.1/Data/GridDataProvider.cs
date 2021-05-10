// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.GridDataProvider
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Async;
using DevExpress.Data.Filtering;
using DevExpress.Data.Helpers;
using DevExpress.Data.Linq;
using DevExpress.Xpf.ChunkList;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Grid.Printing;
using DevExpress.Xpf.GridData;
using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Data
{
  public class GridDataProvider : GridDataProviderBase, IDataControllerCurrentSupport, IDataControllerData2, IDataControllerData, IDataControllerSort, IDataControllerValidationSupport, IWeakEventListener
  {
    private static readonly string[] DummyPropertyDescriptors = new string[5]{ "SimpleListPropertyDescriptor", "NoQueryablePropertyDescriptor", "NoEnumerablePropertyDescriptor", "NoSourcePropertyDescriptor", "NoQueryPropertyDescriptor" };
    private IDataProviderOwner owner;
    private DataSourceProvider provider;
    private BaseGridController dataController;
    private IList list;
    private IDataControllerVisualClient visualClient;
    private GridSelectionController gridSelection;
    private bool isDisplayMemberBindingInitialized;
    private bool autoPopulateColumns;
    private bool isRepopulateColumnsNeeded;
    private bool isDummySource;
    private bool isAsyncOperationInProgress;
    private static NullDataProviderOwner nullOwner;
    private int lockInit;

    protected static NullDataProviderOwner NullOwner
    {
      get
      {
        if (GridDataProvider.nullOwner == null)
          GridDataProvider.nullOwner = new NullDataProviderOwner();
        return GridDataProvider.nullOwner;
      }
    }

    internal IDataProviderOwner Owner
    {
      get
      {
        if (this.owner == null)
          return (IDataProviderOwner) GridDataProvider.NullOwner;
        return this.owner;
      }
    }

    protected internal IDataProviderEvents Events
    {
      get
      {
        return this.Owner as IDataProviderEvents;
      }
    }

    public VisibleIndicesProvider VisibleIndicesProvider { get; private set; }

    public override ISelectionController Selection
    {
      get
      {
        return (ISelectionController) this.gridSelection;
      }
    }

    protected internal override BaseGridController DataController
    {
      get
      {
        if (this.dataController == null)
        {
          this.dataController = this.CreateDataController();
          this.gridSelection = new GridSelectionController((DevExpress.Data.DataController) this.dataController);
          this.dataController.ListSourceChanged += new EventHandler(this.OnListSourceChanged);
          this.dataController.CustomSummary += new CustomSummaryEventHandler(this.OnCustomSummary);
          this.dataController.CustomSummaryExists += new CustomSummaryExistEventHandler(this.OnCustomSummaryExists);
          this.dataController.SelectionChanged += new SelectionChangedEventHandler(this.OnSelectionChanged);
          this.dataController.VisibleRowCountChanged += new EventHandler(this.OnVisibleRowCountChanged);
          this.dataController.VisualClient = this.VisualClient;
          this.dataController.DataClient = (IDataControllerData) this;
          this.dataController.CurrentClient = (IDataControllerCurrentSupport) this;
          this.dataController.ValidationClient = (IDataControllerValidationSupport) this;
          this.dataController.GetVisibleIndexes().ExpandedGroupsIncludedInScrollableIndexes = true;
          if (this.Events != null)
            this.dataController.SortClient = (IDataControllerSort) this;
        }
        return this.dataController;
      }
    }

    private bool IsIPagedCollectionView
    {
      get
      {
        return false;
      }
    }

    protected override Type ItemTypeCore
    {
      get
      {
        return DataProviderBase.GetListItemPropertyType(this.DataController.ListSource, (ICollectionView) null);
      }
    }

    public override ISummaryItemOwner TotalSummaryCore
    {
      get
      {
        return (ISummaryItemOwner) this.Owner.TotalSummary;
      }
    }

    public override ISummaryItemOwner GroupSummaryCore
    {
      get
      {
        return (ISummaryItemOwner) this.GroupSummary;
      }
    }

    public override GridSummaryItemCollection GroupSummary
    {
      get
      {
        return this.Owner.GroupSummary;
      }
    }

    public override ValueComparer ValueComparer
    {
      get
      {
        return this.DataController.ValueComparer;
      }
    }

    public override CriteriaOperator FilterCriteria
    {
      get
      {
        return this.DataController.FilterCriteria;
      }
      set
      {
        if (object.ReferenceEquals((object) this.FilterCriteria, (object) value))
          return;
        this.DataController.FilterCriteria = value;
      }
    }

    private FilterHelper FilterHelper
    {
      get
      {
        return this.DataController.FilterHelper;
      }
    }

    internal override bool IsServerMode
    {
      get
      {
        return this.List is IListServer;
      }
    }

    internal override bool IsICollectionView
    {
      get
      {
        return this.List is ICollectionViewHelper;
      }
    }

    internal override bool IsAsyncServerMode
    {
      get
      {
        return this.List is IAsyncListServer;
      }
    }

    internal override bool IsAsyncOperationInProgress
    {
      get
      {
        return this.isAsyncOperationInProgress;
      }
      set
      {
        this.isAsyncOperationInProgress = value;
        this.Owner.UpdateIsAsyncOperationInProgress();
      }
    }

    internal override bool IsRefreshInProgress
    {
      get
      {
        StateServerModeDataController modeDataController = this.dataController as StateServerModeDataController;
        if (modeDataController != null)
          return modeDataController.IsInRefresh;
        return false;
      }
    }

    protected internal override bool AllowUpdateFocusedRowData
    {
      get
      {
        return !this.IsAsyncServerMode;
      }
    }

    public override bool IsUpdateLocked
    {
      get
      {
        return this.DataController.IsUpdateLocked;
      }
    }

    internal RowStateController RowStateController
    {
      get
      {
        return (RowStateController) this.DataController.Selection;
      }
    }

    protected IList List
    {
      get
      {
        return this.list;
      }
    }

    private bool IsRealBindingList
    {
      get
      {
        if (this.List is IBindingList)
          return !(this.List is IListWrapper);
        return false;
      }
    }

    internal override IRefreshable RefreshableSource
    {
      get
      {
        return this.list as IRefreshable;
      }
    }

    internal override BindingListAdapterBase BindingListAdapter
    {
      get
      {
        return this.list as BindingListAdapterBase;
      }
    }

    internal override bool SubscribeRowChangedForVisibleRows
    {
      get
      {
        if (this.IsServerMode || this.IsAsyncServerMode)
          return true;
        if (this.Owner.AllowLiveDataShaping.HasValue)
          return !this.Owner.AllowLiveDataShaping.Value;
        return !this.IsRealBindingList;
      }
    }

    internal override bool IsSelfManagedItemsSource
    {
      get
      {
        if (!this.IsServerMode)
          return this.IsAsyncServerMode;
        return true;
      }
    }

    protected DataSourceProvider Provider
    {
      get
      {
        return this.provider;
      }
      set
      {
        if (this.Provider == value)
          return;
        if (this.Provider != null)
          DataChangedEventManager.RemoveListener(this.Provider, (IWeakEventListener) this);
        this.provider = value;
        if (this.Provider == null)
          return;
        DataChangedEventManager.AddListener(this.Provider, (IWeakEventListener) this);
      }
    }

    private bool IsLockInit
    {
      get
      {
        return this.lockInit != 0;
      }
    }

    public IDataControllerVisualClient VisualClient
    {
      get
      {
        return this.visualClient;
      }
      private set
      {
        this.visualClient = value;
        if (this.DataController.VisualClient == value)
          return;
        this.DataController.VisualClient = value;
      }
    }

    private IDispalyMemberBindingClient DisplayMemberBindingClient
    {
      get
      {
        return this.VisualClient as IDispalyMemberBindingClient;
      }
    }

    public override int GroupedColumnCount
    {
      get
      {
        return this.DataController.GroupedColumnCount;
      }
    }

    public override int VisibleCount
    {
      get
      {
        return this.DataController.VisibleCount;
      }
    }

    public override int DataRowCount
    {
      get
      {
        return this.DataController.VisibleListSourceRowCount;
      }
    }

    public override DataColumnInfoCollection Columns
    {
      get
      {
        return this.DataController.Columns;
      }
    }

    public override DataColumnInfoCollection DetailColumns
    {
      get
      {
        return this.DataController.DetailColumns;
      }
    }

    public override bool IsReady
    {
      get
      {
        return this.DataController.IsReady;
      }
    }

    public override bool IsCurrentRowEditing
    {
      get
      {
        return this.DataController.IsCurrentRowEditing;
      }
    }

    public override bool ShowGroupSummaryFooter
    {
      get
      {
        return this.VisibleIndicesProvider.ShowGroupSummaryFooter;
      }
    }

    public override bool AutoExpandAllGroups
    {
      get
      {
        return this.DataController.AutoExpandAllGroups;
      }
      set
      {
        this.DataController.AutoExpandAllGroups = value;
      }
    }

    public override int CurrentControllerRow
    {
      get
      {
        return this.DataController.CurrentControllerRow;
      }
      set
      {
        this.DataController.CurrentControllerRow = value;
      }
    }

    public override int CurrentIndex
    {
      get
      {
        return this.GetRowVisibleIndexByHandle(this.CurrentControllerRow);
      }
    }

    bool IDataControllerData2.CanUseFastProperties
    {
      get
      {
        return !this.Owner.IsDesignTime;
      }
    }

    bool IDataControllerData2.HasUserFilter
    {
      get
      {
        return this.Owner.HasCustomRowFilter();
      }
    }

    public override bool AllowEdit
    {
      get
      {
        return this.DataController.AllowEdit;
      }
    }

    IBoundControl IDataControllerValidationSupport.BoundControl
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public bool IsNewItemRowEditing
    {
      get
      {
        return this.DataController.IsNewItemRowEditing;
      }
    }

    private IEditableCollectionView EditableView
    {
      get
      {
        if (!(this.List is ISupportEditableCollectionView))
          return this.List as IEditableCollectionView;
        if (!((ISupportEditableCollectionView) this.List).IsSupportEditableCollectionView)
          return (IEditableCollectionView) null;
        return (IEditableCollectionView) this.List;
      }
    }

    public override bool? AllowLiveDataShaping
    {
      get
      {
        return this.Owner.AllowLiveDataShaping;
      }
    }

    internal override bool CanFilter
    {
      get
      {
        return this.DataController.CanFilter;
      }
    }

    public GridDataProvider(IDataProviderOwner owner)
    {
      this.owner = owner;
      this.VisibleIndicesProvider = new VisibleIndicesProvider(this);
      this.VisualClient = (IDataControllerVisualClient) owner;
    }

    internal static DataColumnInfo GetActualColumnInfo(DevExpress.Data.DataController dataController, string fieldName)
    {
      return dataController.Columns[fieldName] ?? dataController.DetailColumns[fieldName];
    }

    internal static object GetRowValueByListIndex(DevExpress.Data.DataController dataController, int listIndex, string fieldName)
    {
      DataColumnInfo actualColumnInfo = GridDataProvider.GetActualColumnInfo(dataController, fieldName);
      return GridDataProvider.GetRowValueByListIndex(dataController, listIndex, actualColumnInfo);
    }

    internal static object GetRowValueByListIndex(DevExpress.Data.DataController dataController, int listIndex, DataColumnInfo info)
    {
      if (info == null)
        return (object) null;
      if (dataController.DetailColumns[info.Name] != null)
        return dataController.Helper.GetRowValueDetail(listIndex, info);
      return dataController.GetListSourceRowValue(listIndex, info.Index);
    }

    internal static object GetRowByListIndex(DevExpress.Data.DataController dataController, int listSourceRowIndex)
    {
      return dataController.GetRowByListSourceIndex(listSourceRowIndex);
    }

    internal static int GetLevelNextIndex(int index, GroupRowInfoCollection groupInfo)
    {
      int num = (int) groupInfo[index].Level;
      do
      {
        ++index;
      }
      while (index != groupInfo.Count && (int) groupInfo[index].Level > num);
      return index;
    }

    internal GridServerModeDataControllerPrintInfo SubstituteDataControllerForPrinting(IList printingSource, bool expandAllGroups)
    {
      BaseGridController defaultDataController = this.CreateDefaultDataController();
      defaultDataController.ByPassFilter = this.IsServerMode || this.IsAsyncServerMode;
      defaultDataController.DataClient = (IDataControllerData) new PrintingDataClient((IDataControllerData2) this);
      defaultDataController.CustomSummary += new CustomSummaryEventHandler(this.OnCustomSummary);
      defaultDataController.CustomSummaryExists += new CustomSummaryExistEventHandler(this.OnCustomSummaryExists);
      defaultDataController.VisualClient = (IDataControllerVisualClient) new PrintingVisualClient(this.VisualClient);
      defaultDataController.SortClient = (IDataControllerSort) this;
      BaseGridController dataController = this.DataController;
      this.dataController = defaultDataController;
      bool flag = printingSource.Count != 0;
      if (flag)
        defaultDataController.DataSource = (object) printingSource;
      ((DataControlBase) this.Owner).UpdateTotalSummary();
      Dictionary<ColumnBase, string> dictionary = ((DataControlBase) this.Owner).ColumnsCore.Cast<ColumnBase>().ToDictionary<ColumnBase, ColumnBase, string>((Func<ColumnBase, ColumnBase>) (c => c), (Func<ColumnBase, string>) (c => c.TotalSummaryText));
      string summariesLeftString = ((DataControlBase) this.Owner).viewCore.GetFixedSummariesLeftString();
      string summariesRightString = ((DataControlBase) this.Owner).viewCore.GetFixedSummariesRightString();
      if (flag)
        defaultDataController.VisualClient.RequestSynchronization();
      if (expandAllGroups)
        this.ExpandAll();
      else
        this.LoadGroupStatesToPrintingController(dataController);
      return new GridServerModeDataControllerPrintInfo(defaultDataController, dictionary, summariesLeftString, summariesRightString);
    }

    internal void ClearPrintingControllerEvents(BaseGridController printingController)
    {
      printingController.CustomSummary -= new CustomSummaryEventHandler(this.OnCustomSummary);
      printingController.CustomSummaryExists -= new CustomSummaryExistEventHandler(this.OnCustomSummaryExists);
    }

    private void LoadGroupStatesToPrintingController(BaseGridController oldDataController)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        oldDataController.SaveRowState((Stream) memoryStream);
        memoryStream.Seek(0L, SeekOrigin.Begin);
        this.DataController.RestoreRowState((Stream) memoryStream);
      }
    }

    public override void InvalidateVisibleIndicesCache()
    {
      this.VisibleIndicesProvider.InvalidateCache();
    }

    private void OnVisibleRowCountChanged(object sender, EventArgs e)
    {
      DataControlBase dataControlBase = this.owner as DataControlBase;
      if (dataControlBase == null)
        return;
      dataControlBase.RaiseVisibleRowCountChanged();
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      this.Owner.OnSelectionChanged(e);
    }

    protected internal override void ForceClearData()
    {
      if (this.dataController == null)
        return;
      this.dataController.ListSourceChanged -= new EventHandler(this.OnListSourceChanged);
      this.dataController.CustomSummary -= new CustomSummaryEventHandler(this.OnCustomSummary);
      this.dataController.CustomSummaryExists -= new CustomSummaryExistEventHandler(this.OnCustomSummaryExists);
      this.dataController.SelectionChanged -= new SelectionChangedEventHandler(this.OnSelectionChanged);
      this.dataController.VisibleRowCountChanged -= new EventHandler(this.OnVisibleRowCountChanged);
      this.dataController.Dispose();
      this.dataController = (BaseGridController) null;
    }

    protected virtual void OnListSourceChanged(object sender, EventArgs e)
    {
      this.Owner.OnListSourceChanged();
    }

    private void OnCustomSummaryExists(object sender, CustomSummaryExistEventArgs e)
    {
      if (this.Events == null)
        return;
      this.Events.OnCustomSummaryExists(sender, e);
    }

    private void OnCustomSummary(object sender, CustomSummaryEventArgs e)
    {
      if (this.Events == null)
        return;
      this.Events.OnCustomSummary(sender, e);
    }

    private BaseGridController CreateDefaultDataController()
    {
      return (BaseGridController) new StateGridDataController(this.Owner);
    }

    protected virtual BaseGridController CreateDataController()
    {
      if (this.IsAsyncServerMode)
      {
        AsyncServerModeGridDataController gridDataController = new AsyncServerModeGridDataController(this.owner);
        gridDataController.ThreadClient = (IDataControllerThreadClient) new GridDataControllerThreadClient(this);
        return (BaseGridController) gridDataController;
      }
      if (this.IsServerMode)
      {
        if (this.IsICollectionView && !this.IsIPagedCollectionView)
          return (BaseGridController) new CollectionViewDataController(this.owner);
        return (BaseGridController) new StateServerModeDataController(this.Owner);
      }
      if (this.Owner.OptimizeSummaryCalculation && this.List is IListChanging)
        return (BaseGridController) new ChunkListDataController(this.Owner);
      return this.CreateDefaultDataController();
    }

    internal void SetDataController(BaseGridController dataController)
    {
      this.dataController = dataController;
    }

    internal override void CancelAllGetRows()
    {
      if (!this.IsAsyncServerMode)
        return;
      this.DataController.ScrollingCancelAllGetRows();
    }

    internal override void EnsureAllRowsLoaded(int firstRowIndex, int rowsCount)
    {
      if (!this.IsAsyncServerMode)
        return;
      for (int scrollIndex = firstRowIndex; scrollIndex < firstRowIndex + rowsCount; ++scrollIndex)
      {
        object indexByScrollIndex = this.VisibleIndicesProvider.GetVisibleIndexByScrollIndex(scrollIndex);
        if (!(indexByScrollIndex is GroupSummaryRowKey))
          this.DataController.ScrollingCheckRowLoaded(this.DataController.GetControllerRowHandle((int) indexByScrollIndex));
      }
    }

    protected internal override void OnDataSourceChanged()
    {
      this.list = this.ExtractDataSource(this.DataSource);
      bool isUpdateLocked = this.DataController.IsUpdateLocked;
      this.ForceClearData();
      if (isUpdateLocked)
        this.DataController.BeginUpdate();
      if (this.VisualClient != null && this.List == null && !this.IsAsyncServerMode)
        this.VisualClient.RequestSynchronization();
      this.DataController.SetDataSource((object) this.List);
      this.Owner.ValidateMasterDetailConsistency();
      this.ScheduleRepopulateColumnsIfNeeded();
      this.ResetValidationAttributes();
    }

    private void RefreshDataProvider()
    {
      object dataSource = this.DataSource;
      this.DataSource = (object) null;
      this.DataSource = dataSource;
    }

    protected virtual IList ExtractDataSource(object dataSource)
    {
      if (this.IsLockInit)
        return (IList) null;
      ++this.lockInit;
      try
      {
        this.Provider = dataSource as DataSourceProvider;
        DevExpress.Xpf.Core.Native.ItemPropertyNotificationMode itemPropertyNotificationMode = DevExpress.Xpf.Core.Native.ItemPropertyNotificationMode.None;
        if (!this.SubscribeRowChangedForVisibleRows)
        {
          itemPropertyNotificationMode |= DevExpress.Xpf.Core.Native.ItemPropertyNotificationMode.PropertyChanged;
          if (this.Owner.OptimizeSummaryCalculation)
            itemPropertyNotificationMode |= DevExpress.Xpf.Core.Native.ItemPropertyNotificationMode.PropertyChanging;
        }
        return DataBindingHelper.ExtractDataSource(dataSource, itemPropertyNotificationMode, false);
      }
      finally
      {
        --this.lockInit;
      }
    }

    public virtual bool CanColumnSort(string fieldName)
    {
      return this.Owner.CanSortColumn(fieldName);
    }

    public override bool CanColumnSortCore(string fieldName)
    {
      return this.DataController.CanSortColumn(fieldName);
    }

    public override void Synchronize(IList<GridSortInfo> sortList, int groupCount, CriteriaOperator filterCriteria)
    {
      System.Collections.Generic.List<DataColumnSortInfo> dataColumnSortInfoList = new System.Collections.Generic.List<DataColumnSortInfo>();
      foreach (IColumnInfo sort in (IEnumerable<GridSortInfo>) sortList)
      {
        if (this.CanColumnSort(sort.FieldName))
        {
          DataColumnInfo columnInfo = this.Columns[sort.FieldName];
          if (columnInfo != null)
            dataColumnSortInfoList.Add(new DataColumnSortInfo(columnInfo, sort.SortOrder, this.Owner.GetGroupInterval(sort.FieldName)));
        }
      }
      this.DataController.BeginUpdate();
      try
      {
        try
        {
          this.FilterCriteria = filterCriteria;
        }
        catch
        {
        }
        this.DataController.SortInfo.ClearAndAddRange(dataColumnSortInfoList.ToArray(), groupCount);
        this.GroupSummarySortInfo.Sync(sortList, groupCount);
        this.SynchronizeGroupSortInfo();
        this.SynchronizeSummary();
        this.DataController.SummarySortInfo.ClearAndAddRange(this.GetSummarySortInfo());
      }
      finally
      {
        this.DataController.EndUpdate();
      }
    }

    private void SynchronizeGroupSortInfo()
    {
      int groupCount;
      this.Owner.SynchronizeGroupSortInfo((IList<IColumnInfo>) this.GetSortInfo(out groupCount), groupCount);
    }

    public override void SynchronizeSummary()
    {
      System.Collections.Generic.List<SummaryItem> changedGroupSummaries = this.SynchronizeSummary(this.DataController.GroupSummary, (IEnumerable<DevExpress.Xpf.Grid.SummaryItemBase>) this.GroupSummary);
      System.Collections.Generic.List<SummaryItem> changedTotalSummaries = this.SynchronizeSummary((SummaryItemCollection) this.DataController.TotalSummary, this.TotalSummaryCore.Concat<DevExpress.Xpf.Grid.SummaryItemBase>(this.Owner.GetServiceSummaries()));
      this.UpdateGroupSummary(changedGroupSummaries);
      this.UpdateTotalSummary(changedTotalSummaries);
    }

    private void UpdateGroupSummary(System.Collections.Generic.List<SummaryItem> changedGroupSummaries = null)
    {
      this.DataController.UpdateGroupSummary(changedGroupSummaries);
    }

    private void UpdateTotalSummary(System.Collections.Generic.List<SummaryItem> changedTotalSummaries = null)
    {
      this.DataController.UpdateTotalSummary(changedTotalSummaries);
    }

    public override void UpdateGroupSummary()
    {
      this.DataController.UpdateGroupSummary((System.Collections.Generic.List<SummaryItem>) null);
    }

    public override void UpdateTotalSummary()
    {
      this.DataController.UpdateTotalSummary((System.Collections.Generic.List<SummaryItem>) null);
    }

    public System.Collections.Generic.List<IColumnInfo> GetSortInfo(out int groupCount)
    {
      DataControlBase dataControlBase = this.owner as DataControlBase;
      System.Collections.Generic.List<IColumnInfo> columnInfoList = new System.Collections.Generic.List<IColumnInfo>();
      if (dataControlBase != null && dataControlBase.ActualItemsSource == null)
      {
        groupCount = dataControlBase.SortInfoCore.GroupCountCore;
        foreach (GridSortInfo gridSortInfo in (Collection<GridSortInfo>) dataControlBase.SortInfoCore)
          columnInfoList.Add((IColumnInfo) new DummyColumnInfo(((IColumnInfo) gridSortInfo).FieldName, ((IColumnInfo) gridSortInfo).SortOrder));
      }
      else
      {
        groupCount = this.DataController.SortInfo.GroupCount;
        foreach (DataColumnSortInfo dataColumnSortInfo in (CollectionBase) this.DataController.SortInfo)
          columnInfoList.Add((IColumnInfo) new DummyColumnInfo(dataColumnSortInfo.ColumnInfo.Name, dataColumnSortInfo.SortOrder));
      }
      return columnInfoList;
    }

    protected SummarySortInfo[] GetSummarySortInfo()
    {
      System.Collections.Generic.List<SummarySortInfo> summarySortInfoList = new System.Collections.Generic.List<SummarySortInfo>();
      foreach (GridGroupSummarySortInfo groupSummarySortInfo in (Collection<GridGroupSummarySortInfo>) this.GroupSummarySortInfo)
      {
        if (groupSummarySortInfo.SummaryItem != null && groupSummarySortInfo.SummaryItem.SummaryType != SummaryItemType.None)
        {
          int index = this.GroupSummary.IndexOf(groupSummarySortInfo.SummaryItem);
          if (index >= 0 && index < this.DataController.GroupSummary.Count)
          {
            SummaryItem summaryItem = this.DataController.GroupSummary[index];
            int groupIndex = this.GetGroupIndex(groupSummarySortInfo.FieldName);
            if (groupIndex >= 0)
              summarySortInfoList.Add(new SummarySortInfo(summaryItem, groupIndex, groupSummarySortInfo.GetSortOrder()));
          }
        }
      }
      return summarySortInfoList.ToArray();
    }

    public override void BeginUpdate()
    {
      this.DataController.BeginUpdate();
    }

    public override void EndUpdate()
    {
      this.DataController.EndUpdate();
      this.UpdateDisplayMemberBindingColumnsIfNeeded();
    }

    public override void CancelCurrentRowEdit()
    {
      this.DataController.CancelCurrentRowEdit();
    }

    public override void BeginCurrentRowEdit()
    {
      this.DataController.BeginCurrentRowEdit();
    }

    public override bool EndCurrentRowEdit()
    {
      if (this.IsCurrentRowEditing)
        return this.DataController.EndCurrentRowEdit();
      return true;
    }

    public override ErrorInfo GetErrorInfo(RowHandle rowHandle)
    {
      return this.DataController.GetErrorInfo(rowHandle.Value);
    }

    public override void ExpandAll()
    {
      this.DataController.ExpandAll();
    }

    public override void CollapseAll()
    {
      this.DataController.CollapseAll();
    }

    public override int GetListIndexByRowHandle(int rowHandle)
    {
      return this.DataController.GetListSourceRowIndex(rowHandle);
    }

    public override RowDetailContainer GetRowDetailContainer(int rowHandle, Func<RowDetailContainer> createContainerDelegate, bool createNewIfNotExist)
    {
      if (!this.IsGroupRowHandle(rowHandle))
        return this.RowStateController.GetRowDetailInfo(rowHandle, createContainerDelegate, createNewIfNotExist);
      return (RowDetailContainer) null;
    }

    public override IEnumerable<int> GetRowListIndicesWithExpandedDetails()
    {
      return this.RowStateController.GetRowListIndicesWithExpandedDetails();
    }

    public override void ClearDetailInfo()
    {
      this.RowStateController.ClearDetailInfo();
    }

    public override DependencyObject GetRowState(int controllerRow, bool createNewIfNotExist)
    {
      return this.RowStateController.GetRowState(controllerRow, createNewIfNotExist);
    }

    public override int GetRowHandleByListIndex(int listIndex)
    {
      return this.DataController.GetControllerRow(listIndex);
    }

    public override int GetControllerRow(int visibleIndex)
    {
      return this.DataController.GetControllerRowHandle(visibleIndex);
    }

    public override bool IsGroupRowHandle(int rowHandle)
    {
      if (rowHandle == -999997)
        return false;
      return this.DataController.IsGroupRowHandle(rowHandle);
    }

    public override bool IsValidRowHandle(int rowHandle)
    {
      return this.DataController.IsValidControllerRowHandle(rowHandle);
    }

    public override bool IsRowVisible(int rowHandle)
    {
      if (this.Owner.NewItemRowPosition == NewItemRowPosition.Bottom && rowHandle == -2147483647)
        return true;
      return this.DataController.IsRowVisible(rowHandle);
    }

    public override int GetParentRowHandle(int rowHandle)
    {
      return this.DataController.GetParentRowHandle(rowHandle);
    }

    public override int GetChildRowCount(int rowHandle)
    {
      return this.DataController.GroupInfo.GetChildCount(rowHandle);
    }

    public override int GetChildRowHandle(int rowHandle, int childIndex)
    {
      return this.DataController.GroupInfo.GetChildRow(rowHandle, childIndex);
    }

    public object GetWpfRowByListIndex(int listIndex)
    {
      return (object) new RowTypeDescriptorListIndex(this.selfReference, listIndex);
    }

    public override bool IsGroupRow(int visibleIndex)
    {
      return this.IsGroupRowHandle(this.GetControllerRow(visibleIndex));
    }

    public override object GetGroupRowValue(int rowHandle)
    {
      return this.DataController.GetGroupRowValue(rowHandle);
    }

    public override object GetGroupRowValue(int rowHandle, ColumnBase column)
    {
      if (column == null)
        return this.GetGroupRowValue(rowHandle);
      DataColumnInfo column1 = this.DataController.Columns[column.FieldName];
      return this.DataController.GetGroupRowValue(rowHandle, column1);
    }

    public override bool IsGroupRowExpanded(int controllerRow)
    {
      return this.DataController.IsRowExpanded(controllerRow);
    }

    public override int GetRowLevelByControllerRow(int rowHandle)
    {
      return this.DataController.GetRowLevel(rowHandle);
    }

    public override int GetActualRowLevel(int rowHandle, int level)
    {
      if (!this.DataController.AllowPartialGrouping || this.IsGroupRowHandle(rowHandle))
        return level;
      for (int parentRowHandle = this.GetParentRowHandle(rowHandle); parentRowHandle != int.MinValue && this.GetChildRowCount(parentRowHandle) == 1; parentRowHandle = this.GetParentRowHandle(parentRowHandle))
        --level;
      return level;
    }

    public override bool IsGroupVisible(GroupRowInfo groupInfo)
    {
      if (this.DataController.AllowPartialGrouping)
        return groupInfo.ChildControllerRowCount > 1;
      return true;
    }

    public override bool OnShowingGroupFooter(int rowHandle, int level)
    {
      return this.Events.OnShowingGroupFooter(rowHandle, level);
    }

    public override void ChangeGroupExpanded(int controllerRow, bool recursive)
    {
      if (this.IsGroupRowExpanded(controllerRow))
        this.DataController.CollapseRow(controllerRow, recursive);
      else
        this.DataController.ExpandRow(controllerRow, recursive);
    }

    public override void MakeRowVisible(int rowHandle)
    {
      this.DataController.MakeRowVisible(rowHandle);
    }

    public override object GetTotalSummaryValue(DevExpress.Xpf.Grid.SummaryItemBase item)
    {
      if (item == null)
        return (object) null;
      SummaryItem summaryItemByKey = this.DataController.TotalSummary.GetSummaryItemByKey((object) item);
      if (summaryItemByKey == null)
        return (object) null;
      return summaryItemByKey.SummaryValue;
    }

    public override bool TryGetGroupSummaryValue(int rowHandle, DevExpress.Xpf.Grid.SummaryItemBase item, out object value)
    {
      value = (object) null;
      if (item == null)
        return false;
      IDictionary dictionary = (IDictionary) this.DataController.GetGroupSummary(rowHandle);
      if (dictionary == null || !dictionary.Contains((object) item))
        return false;
      value = dictionary[(object) item];
      return true;
    }

    public override object GetRowByListIndex(int listSourceRowIndex)
    {
      return GridDataProvider.GetRowByListIndex((DevExpress.Data.DataController) this.DataController, listSourceRowIndex);
    }

    public override object GetCellValueByListIndex(int listSourceRowIndex, string fieldName)
    {
      return this.GetRowValueByListIndex(listSourceRowIndex, fieldName);
    }

    public object GetListSourceRowValue(int listSourceRowIndex, string fieldName)
    {
      return this.DataController.GetListSourceRowValue(listSourceRowIndex, fieldName);
    }

    private int GetColumnHandle(ColumnBase column)
    {
      DataColumnInfo dataColumnInfo = this.Columns[column.FieldName];
      if (dataColumnInfo != null)
        return dataColumnInfo.Index;
      return -1;
    }

    public override object[] GetUniqueColumnValues(ColumnBase column, bool includeFilteredOut, bool roundDataTime, bool implyNullLikeEmptyStringWhenFiltering)
    {
      return this.GetUniqueColumnValues(column, includeFilteredOut, roundDataTime, (OperationCompleted) null, implyNullLikeEmptyStringWhenFiltering);
    }

    public object[] GetUniqueColumnValues(ColumnBase column, bool includeFilteredOut, bool roundDataTime, OperationCompleted asyncCompleted, bool implyNullLikeEmptyStringWhenFiltering)
    {
      if (this.IsAsyncServerMode && asyncCompleted == null)
        asyncCompleted = (OperationCompleted) (valuesObject => this.OnAsyncGetColumnValuesCompleted(column, valuesObject as object[]));
      return this.FilterHelper.GetUniqueColumnValues(this.GetColumnHandle(column), -1, includeFilteredOut, roundDataTime, asyncCompleted, implyNullLikeEmptyStringWhenFiltering);
    }

    private void OnAsyncGetColumnValuesCompleted(ColumnBase column, object[] values)
    {
      column.ColumnFilterInfo.UpdateCurrentPopupData(values);
    }

    public override CriteriaOperator CalcColumnFilterCriteriaByValue(ColumnBase column, object columnValue)
    {
      return this.FilterHelper.CalcColumnFilterCriteriaByValue(this.GetColumnHandle(column), columnValue, true, column.RoundDateTimeForColumnFilter, (IFormatProvider) null);
    }

    public override object CorrectFilterValueType(Type columnFilteredType, object filteredValue, IFormatProvider provider = null)
    {
      return FilterHelper.CorrectFilterValueType(columnFilteredType, filteredValue, provider);
    }

    protected virtual void OnCurrentChanged()
    {
      this.Owner.OnCurrentIndexChanged();
    }

    protected virtual void OnCurrentRowChanged()
    {
      this.Owner.OnCurrentRowChanged();
    }

    public override int GetControllerRowByGroupRow(int groupRowHandle)
    {
      return this.dataController.GetControllerRowByGroupRow(groupRowHandle);
    }

    public override int GetRowVisibleIndexByHandle(int controllerRow)
    {
      if (this.Owner.NewItemRowPosition == NewItemRowPosition.Bottom && controllerRow == -2147483647)
        return this.VisibleCount;
      return this.DataController.GetVisibleIndexChecked(controllerRow);
    }

    public int GetLastVisibleChild(int visibleIndex)
    {
      int controllerRow = this.GetControllerRow(visibleIndex);
      return visibleIndex + this.GetVisibleRowCount(controllerRow);
    }

    public override void DoRefresh()
    {
      this.DataController.DoRefresh();
    }

    public override void RefreshRow(int rowHandle)
    {
      this.DataController.RefreshRow(rowHandle);
    }

    public override int FindRowByRowValue(object value)
    {
      return this.DataController.FindRowByRowValue(value, -1);
    }

    public override int FindRowByValue(string fieldName, object value)
    {
      return this.DataController.FindRowByValue(fieldName, value);
    }

    internal override int GetGroupIndex(string fieldName)
    {
      return this.DataController.SortInfo.GetGroupIndex(this.Columns[fieldName]);
    }

    internal override void EnsureRowLoaded(int rowHandle)
    {
      if (!this.IsAsyncServerMode)
        return;
      this.DataController.ScrollingCheckRowLoaded(rowHandle);
    }

    private int GetVisibleRowCount(int groupRowHandle)
    {
      int num = 0;
      GroupRowInfo groupRowInfoByHandle = this.DataController.GroupInfo.GetGroupRowInfoByHandle(groupRowHandle);
      if (groupRowInfoByHandle == null || !groupRowInfoByHandle.Expanded)
        return 0;
      if (this.DataController.GroupInfo.IsLastLevel(groupRowInfoByHandle))
        return groupRowInfoByHandle.ChildControllerRowCount;
      int childrenGroupCount = this.DataController.GroupInfo.GetTotalChildrenGroupCount(groupRowInfoByHandle);
      for (int index = 0; index < childrenGroupCount; ++index)
      {
        GroupRowInfo groupRow = this.DataController.GroupInfo[groupRowInfoByHandle.Index + index + 1];
        if (groupRow.IsVisible)
          ++num;
        if (groupRow.Expanded && this.DataController.GroupInfo.IsLastLevel(groupRow))
          num += groupRow.ChildControllerRowCount;
      }
      return num;
    }

    private System.Collections.Generic.List<SummaryItem> SynchronizeSummary(SummaryItemCollection controllerSummaries, IEnumerable<DevExpress.Xpf.Grid.SummaryItemBase> gridSummaries)
    {
      if (gridSummaries == null)
        return Enumerable.Empty<SummaryItem>().ToList<SummaryItem>();
      controllerSummaries.BeginUpdate();
      try
      {
        this.RemoveNonVisibleSummariesFromDataController(controllerSummaries);
        this.RemoveDeletedGridSummariesFromDataController(gridSummaries, controllerSummaries);
        System.Collections.Generic.List<SummaryItem> summaryItemList1 = new System.Collections.Generic.List<SummaryItem>();
        DevExpress.Xpf.Grid.SummaryItemBase[] array1 = gridSummaries.Where<DevExpress.Xpf.Grid.SummaryItemBase>((Func<DevExpress.Xpf.Grid.SummaryItemBase, bool>) (x => x.Visible)).ToArray<DevExpress.Xpf.Grid.SummaryItemBase>();
        System.Collections.Generic.List<SummaryItem> summaryItemList2 = this.UpdateControllerSummaryItems(((IEnumerable<DevExpress.Xpf.Grid.SummaryItemBase>) array1).Where<DevExpress.Xpf.Grid.SummaryItemBase>((Func<DevExpress.Xpf.Grid.SummaryItemBase, bool>) (x =>
        {
          if (x.IsInDataController(controllerSummaries) && x.SummaryType != SummaryItemType.None)
            return !x.EqualsToControllerSummaryItem(controllerSummaries.GetSummaryItemByTag((object) x));
          return false;
        })), controllerSummaries);
        summaryItemList1.AddRange((IEnumerable<SummaryItem>) summaryItemList2);
        SummaryItem[] array2 = ((IEnumerable<DevExpress.Xpf.Grid.SummaryItemBase>) array1).Where<DevExpress.Xpf.Grid.SummaryItemBase>((Func<DevExpress.Xpf.Grid.SummaryItemBase, bool>) (x => !x.IsInDataController(controllerSummaries))).Select<DevExpress.Xpf.Grid.SummaryItemBase, SummaryItem>((Func<DevExpress.Xpf.Grid.SummaryItemBase, SummaryItem>) (x => new SummaryItem(!string.IsNullOrEmpty(x.FieldName) ? this.Columns[x.FieldName] : (DataColumnInfo) null, x.SummaryType, (object) x, x.IgnoreNullValues))).ToArray<SummaryItem>();
        controllerSummaries.AddRange(array2);
        summaryItemList1.AddRange((IEnumerable<SummaryItem>) array2);
        return summaryItemList1;
      }
      finally
      {
        controllerSummaries.CancelUpdate();
      }
    }

    private void RemoveNonVisibleSummariesFromDataController(SummaryItemCollection controllerSummaries)
    {
      for (int index = controllerSummaries.Count - 1; index >= 0; --index)
      {
        if (!(controllerSummaries[index].Tag as DevExpress.Xpf.Grid.SummaryItemBase).Visible)
          controllerSummaries.RemoveAt(index);
      }
    }

    private void RemoveDeletedGridSummariesFromDataController(IEnumerable<DevExpress.Xpf.Grid.SummaryItemBase> gridSummaries, SummaryItemCollection controllerSummaries)
    {
      for (int i = controllerSummaries.Count - 1; i >= 0; --i)
      {
        if (!gridSummaries.Any<DevExpress.Xpf.Grid.SummaryItemBase>((Func<DevExpress.Xpf.Grid.SummaryItemBase, bool>) (gridSummaryItem => gridSummaryItem == controllerSummaries[i].Tag)))
          controllerSummaries.RemoveAt(i);
      }
    }

    private System.Collections.Generic.List<SummaryItem> UpdateControllerSummaryItems(IEnumerable<DevExpress.Xpf.Grid.SummaryItemBase> gridSummaries, SummaryItemCollection controllerSummaries)
    {
      System.Collections.Generic.List<SummaryItem> summaryItemList = new System.Collections.Generic.List<SummaryItem>();
      foreach (DevExpress.Xpf.Grid.SummaryItemBase gridSummary in gridSummaries)
      {
        SummaryItem summaryItemByTag = controllerSummaries.GetSummaryItemByTag((object) gridSummary);
        this.UpdateControllerSummaryItem(summaryItemByTag, gridSummary);
        summaryItemList.Add(summaryItemByTag);
      }
      return summaryItemList;
    }

    private void UpdateControllerSummaryItem(SummaryItem controllerSummaryItem, DevExpress.Xpf.Grid.SummaryItemBase gridSummaryItem)
    {
      controllerSummaryItem.FieldName = gridSummaryItem.FieldName;
      controllerSummaryItem.SummaryType = gridSummaryItem.SummaryType;
    }

    internal void DisplayMemberBindingInitialize()
    {
      if (this.DisplayMemberBindingClient != null)
        this.DisplayMemberBindingClient.UpdateSimpleBinding();
      if (this.DataRowCount > 0)
      {
        if (this.DisplayMemberBindingClient == null)
          return;
        this.DisplayMemberBindingClient.UpdateColumns();
      }
      else
        this.isDisplayMemberBindingInitialized = false;
    }

    void IDataControllerCurrentSupport.OnCurrentControllerRowChanged(CurrentRowEventArgs e)
    {
      this.OnCurrentChanged();
    }

    void IDataControllerCurrentSupport.OnCurrentControllerRowObjectChanged(CurrentRowChangedEventArgs e)
    {
      this.OnCurrentRowChanged();
    }

    bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
    {
      if (!(managerType == typeof (DataChangedEventManager)))
        return false;
      this.RefreshDataProvider();
      return true;
    }

    UnboundColumnInfoCollection IDataControllerData.GetUnboundColumns()
    {
      return this.GetUnboundColumnsCore(this.Owner.GetColumns().Concat<IColumnInfo>(this.Owner.GetServiceUnboundColumns()));
    }

    object IDataControllerData.GetUnboundData(int listSourceRow, DataColumnInfo column, object value)
    {
      if (this.Events == null)
        return value;
      return this.Events.GetUnboundData(listSourceRow, column.Name, value);
    }

    void IDataControllerData.SetUnboundData(int listSourceRow, DataColumnInfo column, object value)
    {
      if (this.Events == null)
        return;
      this.Events.SetUnboundData(listSourceRow, column.Name, value);
    }

    void IDataControllerData2.SubstituteFilter(SubstituteFilterEventArgs args)
    {
      this.Events.SubstituteFilter(args);
    }

    bool? IDataControllerData2.IsRowFit(int listSourceRow, bool fit)
    {
      return this.Events.OnCustomRowFilter(listSourceRow, fit);
    }

    PropertyDescriptorCollection IDataControllerData2.PatchPropertyDescriptorCollection(PropertyDescriptorCollection collection)
    {
      System.Collections.Generic.List<IColumnInfo> columns = this.Owner.GetColumns();
      if (columns == null || collection == null)
        return collection;
      foreach (IColumnInfo columnInfo in columns)
      {
        if (collection.Find(columnInfo.FieldName, false) == null)
          collection.Find(columnInfo.FieldName, true);
      }
      return collection;
    }

    ComplexColumnInfoCollection IDataControllerData2.GetComplexColumns()
    {
      ComplexColumnInfoCollection columnInfoCollection = new ComplexColumnInfoCollection();
      foreach (IColumnInfo column in this.Owner.GetColumns())
      {
        if (column.UnboundType == UnboundColumnType.Bound && column.FieldName.Contains(".") && this.DataController.Columns[column.FieldName] == null)
          columnInfoCollection.Add(column.FieldName);
      }
      return columnInfoCollection;
    }

    string[] IDataControllerSort.GetFindByPropertyNames()
    {
      if (this.IsServerMode || this.Owner == null)
        return new string[0];
      return this.Owner.GetFindToColumnNames();
    }

    string IDataControllerSort.GetDisplayText(int listSourceIndex, DataColumnInfo column, object value, string columnName)
    {
      return this.Owner.GetDisplayText(listSourceIndex, column, value, columnName);
    }

    bool IDataControllerSort.RequireDisplayText(DataColumnInfo column)
    {
      return this.Owner.RequireDisplayText(column);
    }

    ExpressiveSortInfo.Row IDataControllerSort.GetCompareRowsMethodInfo()
    {
      return (ExpressiveSortInfo.Row) null;
    }

    bool? IDataControllerSort.IsEqualGroupValues(int listSourceRow1, int listSourceRow2, object value1, object value2, DataColumnInfo column)
    {
      if (!this.Owner.RequireSortCell(column))
        return new bool?();
      int? nullable = this.Events.OnCompareGroupValues(listSourceRow1, listSourceRow2, value1, value2, column);
      if (nullable.HasValue)
        return new bool?(nullable.Value == 0);
      return new bool?();
    }

    ExpressiveSortInfo.Cell IDataControllerSort.GetSortGroupCellMethodInfo(DataColumnInfo dataColumnInfo, Type baseExtractorType)
    {
      if (!this.Owner.RequireSortCell(dataColumnInfo))
        return (ExpressiveSortInfo.Cell) null;
      return this.Events.GetSortGroupCellMethodInfo(dataColumnInfo, baseExtractorType);
    }

    ExpressiveSortInfo.Cell IDataControllerSort.GetSortCellMethodInfo(DataColumnInfo dataColumnInfo, Type baseExtractorType, ColumnSortOrder order)
    {
      if (!this.Owner.RequireSortCell(dataColumnInfo))
        return (ExpressiveSortInfo.Cell) null;
      return this.Events.GetSortCellMethodInfo(dataColumnInfo, baseExtractorType, order);
    }

    void IDataControllerSort.BeforeGrouping()
    {
      this.Events.OnBeforeGrouping();
    }

    void IDataControllerSort.AfterGrouping()
    {
      this.Events.OnAfterGrouping();
    }

    void IDataControllerSort.BeforeSorting()
    {
      this.Events.OnBeforeSorting();
    }

    void IDataControllerSort.AfterSorting()
    {
      this.Events.OnAfterSorting();
    }

    void IDataControllerSort.SubstituteSortInfo(SubstituteSortInfoEventArgs args)
    {
      this.Events.SubstituteSortInfo(args);
    }

    internal override DataColumnInfo GetActualColumnInfo(string fieldName)
    {
      return GridDataProvider.GetActualColumnInfo((DevExpress.Data.DataController) this.DataController, fieldName);
    }

    public object GetRowValueByListIndex(int listIndex, string fieldName)
    {
      return GridDataProvider.GetRowValueByListIndex((DevExpress.Data.DataController) this.DataController, listIndex, fieldName);
    }

    public object GetRowValueByListIndex(int listIndex, DataColumnInfo info)
    {
      return GridDataProvider.GetRowValueByListIndex((DevExpress.Data.DataController) this.DataController, listIndex, info);
    }

    public override object GetRowValue(int rowHandle)
    {
      return this.DataController.GetRow(rowHandle);
    }

    public override object GetRowValue(int rowHandle, string fieldName)
    {
      return this.GetRowValue(rowHandle, this.GetActualColumnInfo(fieldName));
    }

    public override object GetRowValue(int rowHandle, DataColumnInfo info)
    {
      if (!this.IsGroupRowHandle(rowHandle))
        return this.DataController.GetRowValue(rowHandle, info);
      return this.DataController.GetGroupRowValue(rowHandle, info);
    }

    public override void SetRowValue(RowHandle rowHandle, DataColumnInfo info, object value)
    {
      this.DataController.SetRowValue(rowHandle.Value, info, value);
    }

    void IDataControllerValidationSupport.OnEndNewItemRow()
    {
      this.Owner.OnEndNewItemRow();
    }

    void IDataControllerValidationSupport.OnStartNewItemRow()
    {
      if (!this.DataController.Columns.HasUnboundColumns)
        this.TryRePopulateColumns();
      this.Owner.OnStartNewItemRow();
    }

    void IDataControllerValidationSupport.OnBeginCurrentRowEdit()
    {
    }

    void IDataControllerValidationSupport.OnControllerItemChanged(ListChangedEventArgs e)
    {
      this.UpdateDisplayMemberBindingColumnsIfNeeded();
      this.TryRePopulateColumns();
      this.Owner.OnItemChanged(e);
    }

    private void UpdateDisplayMemberBindingColumnsIfNeeded()
    {
      if (this.isDisplayMemberBindingInitialized || this.DisplayMemberBindingClient == null || this.DataRowCount <= 0)
        return;
      this.DisplayMemberBindingClient.UpdateColumns();
      this.isDisplayMemberBindingInitialized = true;
    }

    void IDataControllerValidationSupport.OnCurrentRowUpdated(ControllerRowEventArgs e)
    {
      this.Owner.RaiseCurrentRowUpdated(e);
    }

    void IDataControllerValidationSupport.OnPostCellException(ControllerRowCellExceptionEventArgs e)
    {
      throw e.Exception;
    }

    void IDataControllerValidationSupport.OnPostRowException(ControllerRowExceptionEventArgs e)
    {
      this.Owner.OnPostRowException(e);
    }

    void IDataControllerValidationSupport.OnValidatingCurrentRow(ValidateControllerRowEventArgs e)
    {
      this.Owner.RaiseValidatingCurrentRow(e);
    }

    public virtual RowPosition GetRowPosition(int visibleIndex)
    {
      RowPosition rowPosition;
      if (this.GetRowLevelByVisibleIndex(visibleIndex) == 0)
      {
        rowPosition = this.GetRowPosition(visibleIndex, 0, this.VisibleCount - 1);
      }
      else
      {
        GroupRowInfo parentGroupRow = this.DataController.GetParentGroupRow(this.GetControllerRow(visibleIndex));
        int visibleIndex1 = this.DataController.GetVisibleIndex(parentGroupRow.ChildControllerRow);
        rowPosition = this.GetRowPosition(visibleIndex, visibleIndex1, visibleIndex1 + parentGroupRow.ChildControllerRowCount - 1);
      }
      return this.GetRowPositionByCheckingPrevRow(visibleIndex, rowPosition);
    }

    protected RowPosition GetRowPositionByCheckingPrevRow(int visibleIndex, RowPosition rowPosition)
    {
      int levelByVisibleIndex = this.GetRowLevelByVisibleIndex(visibleIndex);
      if (rowPosition == RowPosition.Middle && levelByVisibleIndex < this.DataController.GroupInfo.LevelCount && this.GetRowLevelByVisibleIndex(visibleIndex - 1) > levelByVisibleIndex)
        return RowPosition.Top;
      return rowPosition;
    }

    protected RowPosition GetRowPosition(int visibleIndex, int startLevelIndex, int endLevelIndex)
    {
      if (startLevelIndex == endLevelIndex)
        return RowPosition.Single;
      if (visibleIndex == startLevelIndex)
        return RowPosition.Top;
      return visibleIndex == endLevelIndex ? RowPosition.Bottom : RowPosition.Middle;
    }

    public virtual void AddNewRow()
    {
      this.DataController.AddNewRow();
      if (this.List == null || this.List.Count != 0 || this.CollectionViewSource == null)
        return;
      this.RePopulateColumns();
    }

    public override void DeleteRow(RowHandle rowHandle)
    {
      this.DataController.DeleteRow(rowHandle.Value);
    }

    internal override void ScheduleAutoPopulateColumns()
    {
      this.autoPopulateColumns = true;
    }

    private void ScheduleRepopulateColumnsIfNeeded()
    {
      if (!this.IsDataSourceEmpty())
        return;
      this.SetRepopulateColumnsConditions(true);
    }

    private void TryRePopulateColumns()
    {
      if (!this.isRepopulateColumnsNeeded || this.IsDataSourceEmpty() && !this.IsAsyncServerMode)
        return;
      bool flag = this.autoPopulateColumns && this.ShouldTryRepopulateColumns();
      this.Owner.RePopulateDataControllerColumns();
      if (flag)
        this.Owner.PopulateColumns();
      this.SetRepopulateColumnsConditions(false);
    }

    private bool IsDataSourceEmpty()
    {
      if (this.List != null && this.List.Count == 0 && (this.EditableView != null && this.EditableView.IsAddingNew) || this.List == null)
        return false;
      return this.List.Count == 0;
    }

    private void SetRepopulateColumnsConditions(bool value)
    {
      if (value)
      {
        this.isRepopulateColumnsNeeded = true;
        this.isDummySource = false;
        if (this.Columns.Count != 1)
          return;
        Type type = this.Columns[0].PropertyDescriptor.GetType();
        foreach (string propertyDescriptor in GridDataProvider.DummyPropertyDescriptors)
        {
          if (type.Name == propertyDescriptor)
          {
            this.isDummySource = true;
            break;
          }
        }
      }
      else
      {
        this.isRepopulateColumnsNeeded = false;
        this.isDummySource = false;
      }
    }

    private bool ShouldTryRepopulateColumns()
    {
      if (this.Owner.GetColumns().Count != 0)
        return this.isDummySource;
      return true;
    }

    internal override int ConvertVisibleIndexToScrollIndex(int visibleIndex, bool allowFixedGroups)
    {
      return (this.Owner.NewItemRowPosition != NewItemRowPosition.Bottom || visibleIndex != this.VisibleCount ? this.DataController.GetVisibleIndexes().ConvertIndexToScrollIndex(visibleIndex, allowFixedGroups) : visibleIndex) + this.VisibleIndicesProvider.CalcVisibleSummaryRowsCountBeforeRow(visibleIndex, allowFixedGroups);
    }

    internal override int ConvertScrollIndexToVisibleIndex(int scrollIndex, bool allowFixedGroups)
    {
      scrollIndex = this.VisibleIndicesProvider.GetVisibleIndexByScrollIndexSafe(scrollIndex);
      return this.DataController.GetVisibleIndexes().ConvertScrollIndexToIndex(scrollIndex, allowFixedGroups);
    }

    public override object GetVisibleIndexByScrollIndex(int scrollIndex)
    {
      return this.VisibleIndicesProvider.GetVisibleIndexByScrollIndex(scrollIndex);
    }

    internal override int GetSummaryRowCountBeforeRow(int rowHandle)
    {
      return this.VisibleIndicesProvider.GetCachedSummaryRowCountBeforeRow(rowHandle);
    }
  }
}
