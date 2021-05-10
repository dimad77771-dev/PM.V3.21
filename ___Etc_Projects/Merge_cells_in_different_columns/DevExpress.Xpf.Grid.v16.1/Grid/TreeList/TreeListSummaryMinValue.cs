// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListSummaryMinValue
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListSummaryMinValue : TreeListSummaryValue
  {
    private object min;

    public override object Value
    {
      get
      {
        if (this.min != this.StartValue)
          return this.min;
        return (object) null;
      }
    }

    public TreeListSummaryMinValue(SummaryItemBase summaryItem)
      : base(summaryItem)
    {
      this.min = this.StartValue;
    }

    public override void Calculate(TreeListNode node, bool summariesIgnoreNullValues)
    {
      object nodeValue = this.GetNodeValue(node);
      if (summariesIgnoreNullValues && nodeValue == null || this.min != this.StartValue && node.DataProvider.ValueComparer.Compare(nodeValue, this.Value) >= 0)
        return;
      this.min = nodeValue;
    }

    public override void Calculate(TreeListSummaryValue val, bool summariesIgnoreNullValues)
    {
      TreeListSummaryMinValue listSummaryMinValue = val as TreeListSummaryMinValue;
      if (listSummaryMinValue == null || summariesIgnoreNullValues && listSummaryMinValue.min == null || this.min != this.StartValue && TreeListSummaryValue.comparer.Compare(listSummaryMinValue.Value, this.Value) >= 0)
        return;
      this.min = listSummaryMinValue.min;
    }
  }
}
