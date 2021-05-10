// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.DummyColumnInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Xpf.GridData;

namespace DevExpress.Xpf.Data
{
  public class DummyColumnInfo : IColumnInfo
  {
    private string fieldName;
    private ColumnSortOrder sortOrder;

    string IColumnInfo.FieldName
    {
      get
      {
        return this.fieldName;
      }
    }

    ColumnSortOrder IColumnInfo.SortOrder
    {
      get
      {
        return this.sortOrder;
      }
    }

    UnboundColumnType IColumnInfo.UnboundType
    {
      get
      {
        return UnboundColumnType.Bound;
      }
    }

    string IColumnInfo.UnboundExpression
    {
      get
      {
        return string.Empty;
      }
    }

    bool IColumnInfo.ReadOnly
    {
      get
      {
        return false;
      }
    }

    public DummyColumnInfo(string fieldName, ColumnSortOrder sortOrder)
    {
      this.fieldName = fieldName;
      this.sortOrder = sortOrder;
    }
  }
}
