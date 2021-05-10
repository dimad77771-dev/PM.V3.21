// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.SummaryItemExportWrapper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Summary;
using DevExpress.XtraExport.Helpers;
using System;

namespace DevExpress.Xpf.Grid.Printing
{
  internal class SummaryItemExportWrapper : ISummaryItemEx, ISummaryItem
  {
    private readonly Func<int, object> getSummaryValueByGroupRowHandle;

    public string DisplayFormat { get; set; }

    public string FieldName { get; private set; }

    public SummaryItemType SummaryType { get; private set; }

    public object SummaryValue { get; private set; }

    public string ShowInColumnFooterName { get; private set; }

    public SummaryItemExportWrapper(string fieldName, string shownInColumn, SummaryItemType summaryType, string displayFormat, object summaryValue, Func<int, object> getSummaryValueByGroupRowHandle)
    {
      this.FieldName = fieldName;
      this.SummaryType = summaryType;
      this.DisplayFormat = displayFormat;
      this.SummaryValue = summaryValue;
      this.ShowInColumnFooterName = shownInColumn;
      this.getSummaryValueByGroupRowHandle = getSummaryValueByGroupRowHandle;
    }

    public object GetSummaryValueByGroupId(int groupId)
    {
      return this.getSummaryValueByGroupRowHandle(groupId);
    }
  }
}
