// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.PrintSelectedRowsHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Xpf.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DevExpress.Xpf.Grid.Printing
{
  internal class PrintSelectedRowsHelper
  {
    private Dictionary<int, object> results = new Dictionary<int, object>();
    private IList allRows = (IList) new List<object>();
    private DataProviderBase DataProvider;
    private DataViewBase View;
    private PrintSelectedRowsInfo PrintSelectedRowsInfo;

    private DataControlBase RootDataControl
    {
      get
      {
        return this.View.DataControl;
      }
    }

    protected DataController DataController
    {
      get
      {
        return (DataController) this.DataProvider.DataController;
      }
    }

    private PrintSelectedRowsHelper(DataProviderBase dataProvider, DataViewBase view, IList allRows)
    {
      this.DataProvider = dataProvider;
      this.View = view;
      this.allRows = allRows;
      this.PrintSelectedRowsInfo = new PrintSelectedRowsInfo();
    }

    public static IList GetSelectedRows(DataProviderBase dataProvider, DataViewBase view, out PrintSelectedRowsInfo printSelectedRowsInfo, IList allRows)
    {
      return new PrintSelectedRowsHelper(dataProvider, view, allRows).GetRows(out printSelectedRowsInfo);
    }

    private IList GetRows(out PrintSelectedRowsInfo printSelectedRowsInfo)
    {
      printSelectedRowsInfo = this.PrintSelectedRowsInfo;
      if (!this.View.IsMultiSelection)
      {
        this.AddRow(this.View.FocusedRowHandle);
        return this.ExtractListSource((IList) this.results.Values.ToList<object>());
      }
      foreach (int detailSelectedRow in this.GetMasterDetailSelectedRows(this.View.DataControl))
        this.AddRow(detailSelectedRow);
      if (this.allRows != null)
        return this.ExtractListSource(this.GetServerModeRows());
      return this.ExtractListSource((IList) this.results.Values.ToList<object>());
    }

    private IList ExtractListSource(IList source)
    {
      if (!(this.DataController.ListSource is ITypedList))
        return source;
      return (IList) new PrintSelectedRowsHelper.TypedListWrapper(source, (ITypedList) this.DataController.ListSource);
    }

    private int[] GetMasterDetailSelectedRows(DataControlBase dataControl)
    {
      DataProviderBase dataProviderBase = dataControl.DataProviderBase;
      List<int> list = ((IEnumerable<int>) this.GetMasterRows(dataControl)).ToList<int>();
      int[] normalizedSelectedRowsEx1 = dataProviderBase.DataController.Selection.GetNormalizedSelectedRowsEx();
      for (int index = 0; index < normalizedSelectedRowsEx1.Length; ++index)
      {
        if (list.Contains(normalizedSelectedRowsEx1[index]))
          list.Remove(normalizedSelectedRowsEx1[index]);
      }
      dataProviderBase.DataController.Selection.BeginSelection();
      foreach (int controllerRow in list)
        dataProviderBase.DataController.Selection.SetSelected(controllerRow, true);
      int[] normalizedSelectedRowsEx2 = dataProviderBase.DataController.Selection.GetNormalizedSelectedRowsEx();
      foreach (int controllerRow in list)
        dataProviderBase.DataController.Selection.SetSelected(controllerRow, false);
      dataProviderBase.DataController.Selection.CancelSelection();
      return normalizedSelectedRowsEx2;
    }

    private int[] GetMasterRows(DataControlBase dataControl)
    {
      List<int> result = new List<int>();
      dataControl.UpdateAllDetailDataControls((Action<DataControlBase>) (dc => this.UpadteDetailContainsSelectedElements(dataControl, dc, ref result)), (Action<DataControlBase>) (dc => this.UpadteDetailContainsSelectedElements(dataControl, dc, ref result)));
      return result.Distinct<int>().ToArray<int>();
    }

    private void UpadteDetailContainsSelectedElements(DataControlBase masterGrid, DataControlBase detailGrid, ref List<int> result)
    {
      if (detailGrid.GetMasterGridCore() != masterGrid || this.GetMasterDetailSelectedRows(detailGrid).Length == 0)
        return;
      int masterRowHandle = ((GridControl) detailGrid).GetMasterRowHandle();
      result.Add(masterRowHandle);
    }

    private IList GetServerModeRows()
    {
      this.allRows.Clear();
      this.PrintSelectedRowsInfo.OriginalRowHandles.Clear();
      foreach (KeyValuePair<int, object> result in this.results)
      {
        this.PrintSelectedRowsInfo.OriginalRowHandles.Add(this.allRows.Count, result.Key);
        this.allRows.Add(result.Value);
      }
      return this.allRows;
    }

    private void AddRow(int rowHandle)
    {
      if (!this.View.DataControl.IsValidRowHandleCore(rowHandle))
        return;
      if (!this.View.DataControl.IsGroupRowHandleCore(rowHandle))
      {
        object row = this.allRows == null || rowHandle > this.allRows.Count - 1 ? this.DataController.GetListSourceRow(rowHandle) : this.allRows[rowHandle];
        this.AddRowToPrintList(rowHandle, row);
      }
      else
        this.AddGroupRowData(rowHandle);
    }

    private void AddRowToPrintList(int rowHandle, object row)
    {
      if (this.results.ContainsKey(rowHandle))
        return;
      this.PrintSelectedRowsInfo.OriginalRowHandles.Add(this.results.Count, rowHandle);
      this.results.Add(rowHandle, row);
    }

    private void AddGroupRowData(int rowHandle)
    {
      GroupRowInfo groupRowInfoByHandle = this.DataController.GroupInfo.GetGroupRowInfoByHandle(rowHandle);
      int childCount = this.DataController.GroupInfo.GetChildCount(groupRowInfoByHandle);
      for (int childIndex = 0; childIndex < childCount; ++childIndex)
      {
        int childRow = this.DataController.GroupInfo.GetChildRow(groupRowInfoByHandle, childIndex);
        if (this.View.DataControl.IsGroupRowHandleCore(childRow))
        {
          this.AddGroupRowData(childRow);
        }
        else
        {
          object row = this.allRows == null || childRow > this.allRows.Count - 1 ? this.DataController.GetListSourceRow(childRow) : this.allRows[childRow];
          if (row != null)
            this.AddRowToPrintList(childRow, row);
        }
      }
    }

    private class TypedListWrapper : IList, ICollection, IEnumerable, ITypedList
    {
      private IList SourceList { get; set; }

      private ITypedList TypedList { get; set; }

      bool ICollection.IsSynchronized
      {
        get
        {
          return this.SourceList.IsSynchronized;
        }
      }

      int ICollection.Count
      {
        get
        {
          return this.SourceList.Count;
        }
      }

      object ICollection.SyncRoot
      {
        get
        {
          return this.SourceList.SyncRoot;
        }
      }

      object IList.this[int index]
      {
        get
        {
          return this.SourceList[index];
        }
        set
        {
          this.SourceList[index] = value;
        }
      }

      bool IList.IsFixedSize
      {
        get
        {
          return this.SourceList.IsFixedSize;
        }
      }

      bool IList.IsReadOnly
      {
        get
        {
          return this.SourceList.IsReadOnly;
        }
      }

      public TypedListWrapper(IList sourceList, ITypedList sourceTypedList)
      {
        this.SourceList = sourceList;
        this.TypedList = sourceTypedList;
      }

      int IList.Add(object value)
      {
        return this.SourceList.Add(value);
      }

      void IList.Insert(int index, object value)
      {
        this.SourceList.Insert(index, value);
      }

      void IList.Remove(object value)
      {
        this.SourceList.Remove(value);
      }

      void IList.RemoveAt(int index)
      {
        this.SourceList.RemoveAt(index);
      }

      void IList.Clear()
      {
        this.SourceList.Clear();
      }

      int IList.IndexOf(object value)
      {
        return this.SourceList.IndexOf(value);
      }

      bool IList.Contains(object value)
      {
        return this.SourceList.Contains(value);
      }

      void ICollection.CopyTo(Array array, int index)
      {
        this.SourceList.CopyTo(array, index);
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return this.SourceList.GetEnumerator();
      }

      PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
      {
        return this.TypedList.GetItemProperties(listAccessors);
      }

      string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
      {
        return this.TypedList.GetListName(listAccessors);
      }
    }
  }
}
