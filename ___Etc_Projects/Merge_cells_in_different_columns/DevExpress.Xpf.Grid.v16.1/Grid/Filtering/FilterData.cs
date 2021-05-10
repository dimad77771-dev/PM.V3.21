// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Filtering.FilterData
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data.Filtering;
using System;

namespace DevExpress.Xpf.Grid.Filtering
{
  public class FilterData : IEquatable<FilterData>
  {
    public static FilterData Null
    {
      get
      {
        return new FilterData(string.Empty, (CriteriaOperator) null, string.Empty, FilterDateType.None);
      }
    }

    public string Caption { get; private set; }

    public CriteriaOperator Criteria { get; private set; }

    public string Tooltip { get; private set; }

    public FilterDateType FilterType { get; private set; }

    public FilterData(string caption, CriteriaOperator criteria, string tooltip, FilterDateType filterType)
    {
      this.Caption = caption;
      this.Criteria = criteria;
      this.Tooltip = tooltip;
      this.FilterType = filterType;
    }

    public override bool Equals(object obj)
    {
      FilterData other = obj as FilterData;
      if (other == null)
        return false;
      return this.Equals(other);
    }

    public bool Equals(FilterData other)
    {
      if (this.Caption == other.Caption && CriteriaOperator.CriterionEquals(this.Criteria, other.Criteria) && this.Tooltip == other.Tooltip)
        return this.FilterType == other.FilterType;
      return false;
    }

    public override int GetHashCode()
    {
      return this.FilterType.GetHashCode();
    }
  }
}
