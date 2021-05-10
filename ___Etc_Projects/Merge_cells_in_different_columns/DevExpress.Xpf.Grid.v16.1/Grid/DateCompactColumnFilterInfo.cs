// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DateCompactColumnFilterInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Filtering;

namespace DevExpress.Xpf.Grid
{
  public class DateCompactColumnFilterInfo : DateColumnFilterInfoBase
  {
    protected virtual FilterDateType[] Filters
    {
      get
      {
        return new FilterDateType[0];
      }
    }

    public DateCompactColumnFilterInfo(ColumnBase column)
      : base(column)
    {
    }

    protected override FilterData[] CreateUpperFilters()
    {
      FilterFactory filterFactory = new FilterFactory(this.Grid, this.FieldName);
      if (!this.Column.ShowEmptyDateFilter)
        return new FilterData[2]{ filterFactory.CreateNoneData(), filterFactory.CreateSpecificDateData() };
      return new FilterData[3]{ filterFactory.CreateNoneData(), filterFactory.CreateEmptyData(), filterFactory.CreateSpecificDateData() };
    }

    protected override FilterData[] CreateBottomFilters()
    {
      return new FilterFactory(this.Grid, this.FieldName).CreateFilters(this.Filters);
    }
  }
}
