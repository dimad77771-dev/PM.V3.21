// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListSummaryUniqueDuplicate
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.ConditionalFormatting;
using System.Collections.Generic;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListSummaryUniqueDuplicate : TreeListSummaryValue
  {
    private readonly UniqueDuplicateRule ruleCore;
    private HashSet<object> result;
    private UniqueDuplicateSummaryCalculator calculatorCore;

    private UniqueDuplicateSummaryCalculator Calculator
    {
      get
      {
        if (this.calculatorCore == null)
          this.calculatorCore = new UniqueDuplicateSummaryCalculator(this.Rule);
        return this.calculatorCore;
      }
    }

    internal UniqueDuplicateRule Rule
    {
      get
      {
        return this.ruleCore;
      }
    }

    public override object Value
    {
      get
      {
        return (object) this.result;
      }
    }

    public TreeListSummaryUniqueDuplicate(SummaryItemBase item, UniqueDuplicateRule rule)
      : base(item)
    {
      this.ruleCore = rule;
    }

    public override void Calculate(TreeListNode node, bool summariesIgnoreNullValues)
    {
      this.Calculator.Calculate(this.GetNodeValue(node));
    }

    public override void Calculate(TreeListSummaryValue val, bool summariesIgnoreNullValues)
    {
    }

    public override void Finish(TreeListNode node)
    {
      this.result = this.Calculator.Finish();
    }
  }
}
