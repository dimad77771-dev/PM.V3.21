// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListFilterColumn
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data.Filtering.Helpers;

namespace DevExpress.Xpf.Grid
{
  internal class TreeListFilterColumn : GridFilterColumn
  {
    public override FilterColumnClauseClass ClauseClass
    {
      get
      {
        return FilterColumnClauseClass.String;
      }
    }

    public TreeListFilterColumn(ColumnBase column, bool useDomainDataSourceRestrictions, bool useWcfSource)
      : base(column, useDomainDataSourceRestrictions, useWcfSource)
    {
    }
  }
}
