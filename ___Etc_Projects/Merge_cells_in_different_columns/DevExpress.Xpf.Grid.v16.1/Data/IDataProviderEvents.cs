// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.IDataProviderEvents
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Helpers;
using System;

namespace DevExpress.Xpf.Data
{
  public interface IDataProviderEvents
  {
    object GetUnboundData(int listSourceRowIndex, string fieldName, object value);

    void SetUnboundData(int listSourceRowIndex, string fieldName, object value);

    void OnCustomSummaryExists(object sender, CustomSummaryExistEventArgs e);

    void OnCustomSummary(object sender, CustomSummaryEventArgs e);

    int? OnCompareSortValues(int listSourceRowIndex1, int listSourceRowIndex2, object value1, object value2, DataColumnInfo column, ColumnSortOrder sortOrder);

    int? OnCompareGroupValues(int listSourceRowIndex1, int listSourceRowIndex2, object value1, object value2, DataColumnInfo column);

    ExpressiveSortInfo.Cell GetSortCellMethodInfo(DataColumnInfo dataColumnInfo, Type baseExtractorType, ColumnSortOrder order);

    ExpressiveSortInfo.Cell GetSortGroupCellMethodInfo(DataColumnInfo dataColumnInfo, Type baseExtractorType);

    bool? OnCustomRowFilter(int listSourceRowIndex, bool fit);

    bool OnShowingGroupFooter(int rowHandle, int level);

    void OnBeforeSorting();

    void OnAfterSorting();

    void OnBeforeGrouping();

    void OnAfterGrouping();

    void SubstituteFilter(SubstituteFilterEventArgs args);

    void SubstituteSortInfo(SubstituteSortInfoEventArgs args);
  }
}
