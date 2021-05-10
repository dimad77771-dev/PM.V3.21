// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListSummaryCustomValue
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListSummaryCustomValue : TreeListSummaryValue
  {
    private TreeListView view;
    private object value;

    public override object Value
    {
      get
      {
        return this.value;
      }
    }

    protected TreeListView View
    {
      get
      {
        return this.view;
      }
    }

    public TreeListSummaryCustomValue(DevExpress.Xpf.Grid.SummaryItemBase summaryItem, TreeListView view)
      : base(summaryItem)
    {
      this.view = view;
    }

    public override void Calculate(TreeListSummaryValue val, bool summariesIgnoreNullValues)
    {
    }

    public override void Start(TreeListNode node)
    {
      this.RaiseEvent(node, CustomSummaryProcess.Start);
    }

    public override void Calculate(TreeListNode node, bool summariesIgnoreNullValues)
    {
      this.RaiseEvent(node, CustomSummaryProcess.Calculate);
    }

    public override void Finish(TreeListNode node)
    {
      this.RaiseEvent(node, CustomSummaryProcess.Finalize);
    }

    private void RaiseEvent(TreeListNode node, CustomSummaryProcess process)
    {
      TreeListCustomSummaryEventArgs e = new TreeListCustomSummaryEventArgs(node, this.SummaryItem, process, this.view.GetNodeValue(node, this.FieldName));
      e.TotalValue = this.Value;
      this.view.RaiseCustomSummary(e);
      this.value = e.TotalValue;
    }
  }
}
