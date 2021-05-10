// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListSummarySumValue
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListSummarySumValue : TreeListSummaryValue
  {
    private Decimal sum;

    protected Decimal Sum
    {
      get
      {
        return this.sum;
      }
      set
      {
        this.sum = value;
      }
    }

    public override object Value
    {
      get
      {
        return (object) this.sum;
      }
    }

    public TreeListSummarySumValue(SummaryItemBase summaryItem)
      : base(summaryItem)
    {
    }

    public override void Start(TreeListNode node)
    {
      base.Start(node);
      this.sum = new Decimal(0);
    }

    public override void Calculate(TreeListNode node, bool summariesIgnoreNullValues)
    {
      object nodeValue = this.GetNodeValue(node);
      try
      {
        this.sum += Convert.ToDecimal(nodeValue);
      }
      catch
      {
      }
    }

    public override void Calculate(TreeListSummaryValue val, bool summariesIgnoreNullValues)
    {
      TreeListSummarySumValue listSummarySumValue = val as TreeListSummarySumValue;
      if (listSummarySumValue == null)
        return;
      this.sum += listSummarySumValue.sum;
    }
  }
}
