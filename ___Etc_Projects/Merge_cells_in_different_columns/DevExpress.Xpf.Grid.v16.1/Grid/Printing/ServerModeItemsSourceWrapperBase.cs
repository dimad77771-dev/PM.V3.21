// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.ServerModeItemsSourceWrapperBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;

namespace DevExpress.Xpf.Grid.Printing
{
  public abstract class ServerModeItemsSourceWrapperBase : List<object>
  {
    private bool isFilled;

    public bool IsFilled
    {
      get
      {
        return this.isFilled;
      }
      set
      {
        this.isFilled = value;
      }
    }

    protected CriteriaOperator GetReportFilter(IServiceProvider servProvider)
    {
      string filterString = ((IFilterStringContainer) servProvider).FilterString;
      if (filterString == null)
        return (CriteriaOperator) null;
      CriteriaOperator criteriaOperator = (CriteriaOperator) null;
      try
      {
        criteriaOperator = CriteriaOperator.Parse(filterString);
      }
      catch
      {
      }
      return criteriaOperator;
    }
  }
}
