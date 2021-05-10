// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.IDataProviderOwner
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.GridData;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Threading;

namespace DevExpress.Xpf.Data
{
  public interface IDataProviderOwner
  {
    bool IsDesignTime { get; }

    Dispatcher Dispatcher { get; }

    GridSummaryItemCollection TotalSummary { get; }

    GridSummaryItemCollection GroupSummary { get; }

    bool? AllowLiveDataShaping { get; }

    bool OptimizeSummaryCalculation { get; }

    NewItemRowPosition NewItemRowPosition { get; }

    bool ShowGroupSummaryFooter { get; }

    Type ItemType { get; }

    bool IsSynchronizedWithCurrentItem { get; }

    List<IColumnInfo> GetColumns();

    IEnumerable<IColumnInfo> GetServiceUnboundColumns();

    IEnumerable<DevExpress.Xpf.Grid.SummaryItemBase> GetServiceSummaries();

    void OnCurrentIndexChanged();

    void OnCurrentIndexChanging(int newControllerRow);

    void OnCurrentRowChanged();

    void OnItemChanged(ListChangedEventArgs e);

    bool RequireSortCell(DataColumnInfo column);

    bool RequireDisplayText(DataColumnInfo column);

    string GetDisplayText(int listSourceIndex, DataColumnInfo column, object value, string columnName);

    bool HasCustomRowFilter();

    void OnListSourceChanged();

    void RaiseCurrentRowUpdated(ControllerRowEventArgs e);

    void RaiseCurrentRowCanceled(ControllerRowEventArgs e);

    void RaiseValidatingCurrentRow(ValidateControllerRowEventArgs e);

    void OnPostRowException(ControllerRowExceptionEventArgs e);

    void OnStartNewItemRow();

    void OnEndNewItemRow();

    void SynchronizeGroupSortInfo(IList<IColumnInfo> sortList, int groupCount);

    void OnSelectionChanged(SelectionChangedEventArgs e);

    bool CanSortColumn(string fieldName);

    void PopulateColumns();

    void RePopulateDataControllerColumns();

    void UpdateIsAsyncOperationInProgress();

    ColumnGroupInterval GetGroupInterval(string fieldName);

    void ValidateMasterDetailConsistency();

    string[] GetFindToColumnNames();

    void DoActionWithPostponedUpdateLayout(Action action);

    void RowChanged(ListChangedType changedType, int newHandle, int? oldRowHandle);
  }
}
