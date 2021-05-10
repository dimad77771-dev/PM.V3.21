// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListSummaryCountValue
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListSummaryCountValue : TreeListSummaryValue
  {
    private int count;

    public override object Value
    {
      get
      {
        return (object) this.count;
      }
    }

    public TreeListSummaryCountValue(SummaryItemBase summaryItem)
      : base(summaryItem)
    {
    }

    public override void Start(TreeListNode node)
    {
      this.count = 0;
    }

    public override void Calculate(TreeListNode node, bool summariesIgnoreNullValues)
    {
      ++this.count;
    }

    public override void Calculate(TreeListSummaryValue val, bool summariesIgnoreNullValues)
    {
      TreeListSummaryCountValue summaryCountValue = val as TreeListSummaryCountValue;
      if (summaryCountValue == null)
        return;
      this.count += summaryCountValue.count;
    }
  }
}
