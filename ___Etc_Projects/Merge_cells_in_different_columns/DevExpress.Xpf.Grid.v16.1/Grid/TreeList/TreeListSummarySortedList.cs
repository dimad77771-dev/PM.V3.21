// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListSummarySortedList
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.ConditionalFormatting.Native;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListSummarySortedList : TreeListSummaryValue
  {
    private List<TreeListNode> nodes;
    private SortedIndices result;

    public override object Value
    {
      get
      {
        return (object) this.result;
      }
    }

    public TreeListSummarySortedList(ServiceSummaryItem item)
      : base((SummaryItemBase) item)
    {
      this.nodes = new List<TreeListNode>();
    }

    public override void Calculate(TreeListNode node, bool summariesIgnoreNullValues)
    {
      object nodeValue = this.GetNodeValue(node);
      if (summariesIgnoreNullValues && nodeValue == null)
        return;
      this.nodes.Add(node);
    }

    public override void Calculate(TreeListSummaryValue val, bool summariesIgnoreNullValues)
    {
    }

    public override void Finish(TreeListNode node)
    {
      this.result = new SortedIndices(this.nodes.OrderBy<TreeListNode, object>((Func<TreeListNode, object>) (n => this.GetNodeValue(n))).Select<TreeListNode, int>((Func<TreeListNode, int>) (n => n.RowHandle)).ToArray<int>());
    }
  }
}
