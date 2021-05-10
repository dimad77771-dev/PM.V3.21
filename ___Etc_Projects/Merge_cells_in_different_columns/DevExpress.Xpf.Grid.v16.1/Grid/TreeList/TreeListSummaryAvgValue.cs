// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListSummaryAvgValue
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListSummaryAvgValue : TreeListSummarySumValue
  {
    private int count;

    public TreeListSummaryAvgValue(SummaryItemBase summaryItem)
      : base(summaryItem)
    {
    }

    public override void Start(TreeListNode node)
    {
      base.Start(node);
      this.count = 0;
    }

    public override void Calculate(TreeListNode node, bool summariesIgnoreNullValues)
    {
      base.Calculate(node, summariesIgnoreNullValues);
      ++this.count;
    }

    public override void Calculate(TreeListSummaryValue val, bool summariesIgnoreNullValues)
    {
      base.Calculate(val, summariesIgnoreNullValues);
      TreeListSummaryAvgValue listSummaryAvgValue = val as TreeListSummaryAvgValue;
      if (listSummaryAvgValue == null)
        return;
      this.count += listSummaryAvgValue.count;
    }

    public override void Finish(TreeListNode node)
    {
      this.Sum = this.count == 0 ? new Decimal(0) : this.Sum / (Decimal) this.count;
    }
  }
}
