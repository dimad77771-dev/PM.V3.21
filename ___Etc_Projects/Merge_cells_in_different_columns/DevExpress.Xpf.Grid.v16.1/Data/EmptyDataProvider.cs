// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.EmptyDataProvider
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Native;
using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DevExpress.Xpf.Data
{
  public class EmptyDataProvider : DataProviderBase
  {
    public static ValueComparer DefaultValueComparer = new ValueComparer();
    private GridSummaryItemCollection totalSummary = new GridSummaryItemCollection((DataControlBase) null, SummaryItemCollectionType.Total);
    private DataColumnInfoCollection columns = new DataColumnInfoCollection();
    private DataColumnInfoCollection detailColumns = new DataColumnInfoCollection();

    public override bool AutoExpandAllGroups
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    public override DataColumnInfoCollection Columns
    {
      get
      {
        return this.columns;
      }
    }

    public override int CurrentControllerRow
    {
      get
      {
        return int.MinValue;
      }
      set
      {
      }
    }

    public override int CurrentIndex
    {
      get
      {
        return 0;
      }
    }

    protected internal override BaseGridController DataController
    {
      get
      {
        return (BaseGridController) null;
      }
    }

    public override int DataRowCount
    {
      get
      {
        return 0;
      }
    }

    public override DataColumnInfoCollection DetailColumns
    {
      get
      {
        return this.detailColumns;
      }
    }

    public override CriteriaOperator FilterCriteria
    {
      get
      {
        return (CriteriaOperator) null;
      }
      set
      {
      }
    }

    public override int GroupedColumnCount
    {
      get
      {
        return 0;
      }
    }

    public override bool AllowEdit
    {
      get
      {
        return false;
      }
    }

    public override bool IsCurrentRowEditing
    {
      get
      {
        return false;
      }
    }

    public override bool IsReady
    {
      get
      {
        return false;
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

    public override bool IsUpdateLocked
    {
      get
      {
        return false;
      }
    }

    public override ISelectionController Selection
    {
      get
      {
        return (ISelectionController) null;
      }
    }

    public override ISummaryItemOwner TotalSummaryCore
    {
      get
      {
        return (ISummaryItemOwner) this.totalSummary;
      }
    }

    public override ISummaryItemOwner GroupSummaryCore
    {
      get
      {
        return (ISummaryItemOwner) null;
      }
    }

    public override int VisibleCount
    {
      get
      {
        return 0;
      }
    }

    protected override Type ItemTypeCore
    {
      get
      {
        return (Type) null;
      }
    }

    public override ValueComparer ValueComparer
    {
      get
      {
        return EmptyDataProvider.DefaultValueComparer;
      }
    }

    public override void BeginCurrentRowEdit()
    {
    }

    public override void BeginUpdate()
    {
    }

    public override CriteriaOperator CalcColumnFilterCriteriaByValue(ColumnBase column, object columnValue)
    {
      return (CriteriaOperator) null;
    }

    public override object CorrectFilterValueType(Type columnFilteredType, object filteredValue, IFormatProvider provider = null)
    {
      return (object) null;
    }

    public override bool CanColumnSortCore(string fieldName)
    {
      return false;
    }

    internal override void CancelAllGetRows()
    {
    }

    public override void CancelCurrentRowEdit()
    {
    }

    public override void ChangeGroupExpanded(int controllerRow, bool recursive)
    {
    }

    public override void CollapseAll()
    {
    }

    protected internal override void OnDataSourceChanged()
    {
    }

    public override void DeleteRow(RowHandle rowHandle)
    {
    }

    public override void DoRefresh()
    {
    }

    public override bool EndCurrentRowEdit()
    {
      return true;
    }

    public override void EndUpdate()
    {
    }

    internal override void EnsureAllRowsLoaded(int firstRowIndex, int rowsCount)
    {
    }

    public override void ExpandAll()
    {
    }

    public override int FindRowByRowValue(object value)
    {
      return -1;
    }

    public override int FindRowByValue(string fieldName, object value)
    {
      return -1;
    }

    protected internal override void ForceClearData()
    {
    }

    internal override DataColumnInfo GetActualColumnInfo(string fieldName)
    {
      return (DataColumnInfo) null;
    }

    public override int GetChildRowCount(int rowHandle)
    {
      return 0;
    }

    public override int GetChildRowHandle(int rowHandle, int childIndex)
    {
      return int.MinValue;
    }

    public override int GetControllerRow(int visibleIndex)
    {
      return int.MinValue;
    }

    public override int GetControllerRowByGroupRow(int groupRowHandle)
    {
      return int.MinValue;
    }

    public override ErrorInfo GetErrorInfo(RowHandle rowHandle)
    {
      return (ErrorInfo) null;
    }

    internal override int GetGroupIndex(string fieldName)
    {
      return 0;
    }

    public override object GetGroupRowValue(int rowHandle, ColumnBase column)
    {
      return (object) null;
    }

    public override object GetGroupRowValue(int rowHandle)
    {
      return (object) null;
    }

    public override bool TryGetGroupSummaryValue(int rowHandle, DevExpress.Xpf.Grid.SummaryItemBase item, out object value)
    {
      value = (object) null;
      return false;
    }

    public override int GetParentRowHandle(int rowHandle)
    {
      return int.MinValue;
    }

    public override object GetRowByListIndex(int listSourceRowIndex)
    {
      return (object) null;
    }

    public override object GetCellValueByListIndex(int listSourceRowIndex, string fieldName)
    {
      return (object) null;
    }

    public override int GetRowHandleByListIndex(int listIndex)
    {
      return int.MinValue;
    }

    public override int GetRowLevelByControllerRow(int rowHandle)
    {
      return 0;
    }

    public override int GetActualRowLevel(int rowHandle, int level)
    {
      return level;
    }

    public override bool IsGroupVisible(GroupRowInfo groupInfo)
    {
      return true;
    }

    public override int GetListIndexByRowHandle(int rowHandle)
    {
      return 0;
    }

    public override DependencyObject GetRowState(int controllerRow, bool createNewIfNotExist)
    {
      return (DependencyObject) null;
    }

    public override object GetRowValue(int rowHandle)
    {
      return (object) null;
    }

    public override object GetRowValue(int rowHandle, DataColumnInfo info)
    {
      return (object) null;
    }

    public override object GetRowValue(int rowHandle, string fieldName)
    {
      return (object) null;
    }

    public override int GetRowVisibleIndexByHandle(int rowHandle)
    {
      return 0;
    }

    public override object GetTotalSummaryValue(DevExpress.Xpf.Grid.SummaryItemBase item)
    {
      return (object) null;
    }

    public override object[] GetUniqueColumnValues(ColumnBase column, bool includeFilteredOut, bool roundDataTime, bool implyNullLikeEmptyStringWhenFiltering)
    {
      return (object[]) null;
    }

    public override bool IsGroupRow(int visibleIndex)
    {
      return false;
    }

    public override bool IsGroupRowExpanded(int controllerRow)
    {
      return false;
    }

    public override bool IsGroupRowHandle(int rowHandle)
    {
      return false;
    }

    public override bool IsRowVisible(int rowHandle)
    {
      return false;
    }

    public override bool IsValidRowHandle(int rowHandle)
    {
      return false;
    }

    public override void MakeRowVisible(int rowHandle)
    {
    }

    public override void RefreshRow(int rowHandle)
    {
    }

    internal override void ScheduleAutoPopulateColumns()
    {
    }

    public override void SetRowValue(RowHandle rowHandle, DataColumnInfo info, object value)
    {
    }

    public override void SynchronizeSummary()
    {
    }

    public override void Synchronize(IList<GridSortInfo> sortList, int groupCount, CriteriaOperator filterCriteria)
    {
    }

    public override void UpdateGroupSummary()
    {
    }

    public override void UpdateTotalSummary()
    {
    }

    internal override void EnsureRowLoaded(int rowHandle)
    {
    }

    public override RowDetailContainer GetRowDetailContainer(int controllerRow, Func<RowDetailContainer> createContainerDelegate, bool createNewIfNotExist)
    {
      throw new NotImplementedException();
    }

    public override void RePopulateColumns()
    {
    }
  }
}
