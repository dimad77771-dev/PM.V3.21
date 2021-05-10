// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DateSmartColumnFilterInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Grid
{
  public class DateSmartColumnFilterInfo : DateColumnFilterInfo
  {
    private readonly DateFilterCache cache;

    public DateSmartColumnFilterInfo(ColumnBase column)
      : base(column)
    {
      this.cache = new DateFilterCache(this.Grid, column);
    }

    protected override void UpdatePopupData(PopupBaseEdit popup)
    {
      base.UpdatePopupData(popup);
      this.cache.Clear();
      this.UpdateBottomFilters();
    }

    protected override void UpdatePopupData(PopupBaseEdit popup, object[] values)
    {
      this.cache.UpdateValuesForColumn(values);
      this.UpdateBottomFilters();
      this.CalendarFilter.RebuildFilters();
    }

    protected override FilterData[] CreateBottomFilters()
    {
      return ((IEnumerable<FilterData>) base.CreateBottomFilters()).Where<FilterData>((Func<FilterData, bool>) (x => this.cache.IsFilterVisibleForColumn(x))).ToArray<FilterData>();
    }
  }
}
