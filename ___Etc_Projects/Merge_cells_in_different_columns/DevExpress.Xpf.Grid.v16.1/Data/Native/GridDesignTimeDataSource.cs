// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.Native.GridDesignTimeDataSource
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using System;
using System.Collections.Generic;

namespace DevExpress.Xpf.Data.Native
{
  public class GridDesignTimeDataSource : DesignTimeDataSource
  {
    public GridDesignTimeDataSource(Type dataObjectType, int rowCount, bool useDistinctValues = false, object originalDataSource = null, IEnumerable<DesignTimePropertyInfo> defaultColumns = null)
      : base(dataObjectType, rowCount, useDistinctValues, originalDataSource, defaultColumns, (List<DesignTimePropertyInfo>) null)
    {
    }

    protected override DXGridDataController CreateDataController()
    {
      return (DXGridDataController) new StateGridDataController((IDataProviderOwner) new NullDataProviderOwner());
    }
  }
}
