// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Filtering.DateFilterCache
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DevExpress.Xpf.Grid.Filtering
{
  public class DateFilterCache
  {
    private readonly DataControlBase grid;
    private readonly ColumnBase column;
    private object[] values;

    public DateFilterCache(DataControlBase grid, ColumnBase column)
    {
      this.grid = grid;
      this.column = column;
    }

    public bool IsFilterVisibleForColumn(FilterData filter)
    {
      if (this.values == null)
        this.values = this.grid.GetUniqueColumnValues(this.column, true, false);
      if (this.values == null || this.values.Length == 0 || AsyncServerModeDataController.IsNoValue(this.values[0]))
        return true;
      Func<DateTime, bool> predicate = this.CompileToPredicate(filter);
      return ((IEnumerable<object>) this.values).AsParallel<object>().Any<object>((Func<object, bool>) (x =>
      {
        if (x is DateTime)
          return predicate((DateTime) x);
        return false;
      }));
    }

    public void UpdateValuesForColumn(object[] values)
    {
      this.values = values;
    }

    public void Clear()
    {
      this.values = (object[]) null;
    }

    private Func<DateTime, bool> CompileToPredicate(FilterData filter)
    {
      if (filter == null)
        return (Func<DateTime, bool>) null;
      return CriteriaCompiler.ToPredicate<DateTime>(filter.Criteria, (CriteriaCompilerDescriptor) new DateFilterCache.DTDescriptor(this.column.FieldName));
    }

    private class DTDescriptor : CriteriaCompilerDescriptor
    {
      private readonly string PropertyName;

      public override Type ObjectType
      {
        get
        {
          return typeof (DateTime);
        }
      }

      public DTDescriptor(string propertyName)
      {
        this.PropertyName = propertyName;
      }

      public override Expression MakePropertyAccess(Expression baseExpression, string propertyPath)
      {
        if (propertyPath == this.PropertyName)
          return baseExpression;
        return (Expression) Expression.Constant((object) null);
      }
    }
  }
}
