// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListSummaryDateTimeAvarage
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using System;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListSummaryDateTimeAvarage : TreeListSummaryValue
  {
    private Tuple<Decimal, int> current;
    private object result;

    public override object Value
    {
      get
      {
        return this.result;
      }
    }

    public TreeListSummaryDateTimeAvarage(ServiceSummaryItem item)
      : base((SummaryItemBase) item)
    {
    }

    public override void Start(TreeListNode node)
    {
      this.current = new Tuple<Decimal, int>(new Decimal(0), 0);
    }

    public override void Calculate(TreeListNode node, bool summariesIgnoreNullValues)
    {
      this.current = new Tuple<Decimal, int>(this.current.Item1 + (Decimal) ((DateTime) this.GetNodeValue(node)).Ticks, this.current.Item2 + 1);
    }

    public override void Calculate(TreeListSummaryValue val, bool summariesIgnoreNullValues)
    {
    }

    public override void Finish(TreeListNode node)
    {
      Tuple<Decimal, int> tuple = this.current;
      this.result = (object) (tuple.Item1 / (Decimal) tuple.Item2);
    }
  }
}
