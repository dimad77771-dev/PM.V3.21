// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Filtering.FilterFactoryExtensions
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Grid.Filtering
{
  public static class FilterFactoryExtensions
  {
    public static FilterData[] CreateFilters(this FilterFactory factory, params FilterDateType[] filters)
    {
      return ((IEnumerable<FilterDateType>) filters).Select<FilterDateType, FilterData>((Func<FilterDateType, FilterData>) (x => factory.CreateFilter(x))).ToArray<FilterData>();
    }
  }
}
