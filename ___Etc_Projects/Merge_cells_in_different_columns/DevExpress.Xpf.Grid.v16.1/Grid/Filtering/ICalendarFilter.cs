// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Filtering.ICalendarFilter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Collections.Generic;

namespace DevExpress.Xpf.Grid.Filtering
{
  public interface ICalendarFilter : IDisposable
  {
    DateTime FocusedDate { get; }

    IList<DateTime> SelectedDates { get; set; }

    bool HasCheckedFilters { get; }

    bool HasSelectedDates { get; }

    FilterData[] CheckedFilters { get; }

    bool IsShowAllChecked { get; }

    void CheckShowAll();

    void UncheckShowAll();

    void CheckFilterBySpecificDate();

    void UncheckFilterBySpecificDate();

    void CheckFilters(params FilterData[] filters);

    void UncheckFilters(params FilterData[] filters);

    void UncheckAllFilters();

    void RebuildFilters();
  }
}
