// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DateColumnFilterInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Filtering;

namespace DevExpress.Xpf.Grid
{
  public class DateColumnFilterInfo : DateCompactColumnFilterInfo
  {
    protected override FilterDateType[] Filters
    {
      get
      {
        return new FilterDateType[13]{ FilterDateType.BeyondThisYear, FilterDateType.LaterThisYear, FilterDateType.LaterThisMonth, FilterDateType.NextWeek, FilterDateType.LaterThisWeek, FilterDateType.Tomorrow, FilterDateType.Today, FilterDateType.Yesterday, FilterDateType.EarlierThisWeek, FilterDateType.LastWeek, FilterDateType.EarlierThisMonth, FilterDateType.EarlierThisYear, FilterDateType.PriorThisYear };
      }
    }

    public DateColumnFilterInfo(ColumnBase column)
      : base(column)
    {
    }
  }
}
