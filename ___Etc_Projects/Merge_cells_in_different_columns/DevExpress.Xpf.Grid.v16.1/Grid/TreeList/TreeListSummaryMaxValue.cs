// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListSummaryMaxValue
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListSummaryMaxValue : TreeListSummaryValue
  {
    private object max;

    public override object Value
    {
      get
      {
        if (this.max != this.StartValue)
          return this.max;
        return (object) null;
      }
    }

    public TreeListSummaryMaxValue(SummaryItemBase summaryItem)
      : base(summaryItem)
    {
      this.max = this.StartValue;
    }

    public override void Calculate(TreeListNode node, bool summariesIgnoreNullValues)
    {
      object nodeValue = this.GetNodeValue(node);
      if (summariesIgnoreNullValues && nodeValue == null || this.max != this.StartValue && node.DataProvider.ValueComparer.Compare(nodeValue, this.Value) <= 0)
        return;
      this.max = nodeValue;
    }

    public override void Calculate(TreeListSummaryValue val, bool summariesIgnoreNullValues)
    {
      TreeListSummaryMaxValue listSummaryMaxValue = val as TreeListSummaryMaxValue;
      if (listSummaryMaxValue == null || summariesIgnoreNullValues && listSummaryMaxValue.max == null || this.max != this.StartValue && TreeListSummaryValue.comparer.Compare(listSummaryMaxValue.Value, this.Value) <= 0)
        return;
      this.max = listSummaryMaxValue.max;
    }
  }
}
