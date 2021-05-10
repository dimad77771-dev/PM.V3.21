// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListSummaryValue
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;

namespace DevExpress.Xpf.Grid.TreeList
{
  public abstract class TreeListSummaryValue
  {
    protected static ValueComparer comparer = new ValueComparer();
    protected readonly object StartValue = new object();
    private DevExpress.Xpf.Grid.SummaryItemBase summaryItem;

    protected DevExpress.Xpf.Grid.SummaryItemBase SummaryItem
    {
      get
      {
        return this.summaryItem;
      }
    }

    protected string FieldName
    {
      get
      {
        return this.SummaryItem.FieldName;
      }
    }

    public abstract object Value { get; }

    public TreeListSummaryValue(DevExpress.Xpf.Grid.SummaryItemBase summaryItem)
    {
      this.summaryItem = summaryItem;
    }

    public virtual void Start(TreeListNode node)
    {
    }

    public virtual void Finish(TreeListNode node)
    {
    }

    public abstract void Calculate(TreeListNode node, bool summariesIgnoreNullValues);

    public abstract void Calculate(TreeListSummaryValue val, bool summariesIgnoreNullValues);

    protected object GetNodeValue(TreeListNode node)
    {
      return node.DataProvider.GetNodeValue(node, this.FieldName);
    }
  }
}
