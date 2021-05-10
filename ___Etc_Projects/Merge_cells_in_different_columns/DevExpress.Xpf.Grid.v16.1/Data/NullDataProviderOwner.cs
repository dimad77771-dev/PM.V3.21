// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.NullDataProviderOwner
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
  public class NullDataProviderOwner : IDataProviderOwner
  {
    bool IDataProviderOwner.IsDesignTime
    {
      get
      {
        return false;
      }
    }

    Dispatcher IDataProviderOwner.Dispatcher
    {
      get
      {
        return (Dispatcher) null;
      }
    }

    GridSummaryItemCollection IDataProviderOwner.TotalSummary
    {
      get
      {
        return new GridSummaryItemCollection((DataControlBase) null, SummaryItemCollectionType.Total);
      }
    }

    GridSummaryItemCollection IDataProviderOwner.GroupSummary
    {
      get
      {
        return new GridSummaryItemCollection((DataControlBase) null, SummaryItemCollectionType.Group);
      }
    }

    bool? IDataProviderOwner.AllowLiveDataShaping
    {
      get
      {
        return new bool?(false);
      }
    }

    bool IDataProviderOwner.OptimizeSummaryCalculation
    {
      get
      {
        return false;
      }
    }

    NewItemRowPosition IDataProviderOwner.NewItemRowPosition
    {
      get
      {
        return NewItemRowPosition.None;
      }
    }

    bool IDataProviderOwner.ShowGroupSummaryFooter
    {
      get
      {
        return false;
      }
    }

    Type IDataProviderOwner.ItemType
    {
      get
      {
        return (Type) null;
      }
    }

    bool IDataProviderOwner.IsSynchronizedWithCurrentItem
    {
      get
      {
        return false;
      }
    }

    List<IColumnInfo> IDataProviderOwner.GetColumns()
    {
      return new List<IColumnInfo>();
    }

    IEnumerable<IColumnInfo> IDataProviderOwner.GetServiceUnboundColumns()
    {
      yield break;
    }

    IEnumerable<DevExpress.Xpf.Grid.SummaryItemBase> IDataProviderOwner.GetServiceSummaries()
    {
      yield break;
    }

    void IDataProviderOwner.OnCurrentIndexChanged()
    {
    }

    void IDataProviderOwner.OnCurrentIndexChanging(int newControllerRow)
    {
    }

    void IDataProviderOwner.OnCurrentRowChanged()
    {
    }

    void IDataProviderOwner.OnItemChanged(ListChangedEventArgs e)
    {
    }

    bool IDataProviderOwner.HasCustomRowFilter()
    {
      return false;
    }

    bool IDataProviderOwner.RequireSortCell(DataColumnInfo column)
    {
      return false;
    }

    bool IDataProviderOwner.RequireDisplayText(DataColumnInfo column)
    {
      return false;
    }

    string IDataProviderOwner.GetDisplayText(int listSourceIndex, DataColumnInfo column, object value, string columnName)
    {
      return string.Empty;
    }

    void IDataProviderOwner.OnListSourceChanged()
    {
    }

    void IDataProviderOwner.RaiseCurrentRowUpdated(ControllerRowEventArgs e)
    {
    }

    void IDataProviderOwner.RaiseCurrentRowCanceled(ControllerRowEventArgs e)
    {
    }

    void IDataProviderOwner.RaiseValidatingCurrentRow(ValidateControllerRowEventArgs e)
    {
    }

    void IDataProviderOwner.OnPostRowException(ControllerRowExceptionEventArgs e)
    {
    }

    void IDataProviderOwner.OnStartNewItemRow()
    {
    }

    void IDataProviderOwner.OnEndNewItemRow()
    {
    }

    void IDataProviderOwner.SynchronizeGroupSortInfo(IList<IColumnInfo> sortList, int groupCount)
    {
    }

    void IDataProviderOwner.OnSelectionChanged(SelectionChangedEventArgs e)
    {
    }

    bool IDataProviderOwner.CanSortColumn(string fieldName)
    {
      return false;
    }

    void IDataProviderOwner.PopulateColumns()
    {
    }

    void IDataProviderOwner.RePopulateDataControllerColumns()
    {
    }

    void IDataProviderOwner.UpdateIsAsyncOperationInProgress()
    {
    }

    ColumnGroupInterval IDataProviderOwner.GetGroupInterval(string fieldName)
    {
      return ColumnGroupInterval.Default;
    }

    void IDataProviderOwner.ValidateMasterDetailConsistency()
    {
    }

    string[] IDataProviderOwner.GetFindToColumnNames()
    {
      return (string[]) null;
    }

    void IDataProviderOwner.DoActionWithPostponedUpdateLayout(Action action)
    {
    }

    void IDataProviderOwner.RowChanged(ListChangedType changedType, int newHandle, int? oldRowHandle)
    {
    }
  }
}
