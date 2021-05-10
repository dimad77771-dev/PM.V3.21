// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupRowDefaultSummaryItemController
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid
{
  internal class GroupRowDefaultSummaryItemController : GroupValuePresenterControllerBase<GridGroupSummaryData>
  {
    private readonly GroupRowDefaultSummaryItemControl summaryItem;

    public GroupRowDefaultSummaryItemController(GroupRowDefaultSummaryItemControl summaryItem)
    {
      this.summaryItem = summaryItem;
    }

    protected override void UpdateText()
    {
      this.summaryItem.Text = this.CalcValueDataText();
    }

    protected override void UpdateColumnHeader()
    {
    }

    private string CalcValueDataText()
    {
      if (this.ValueData == null)
        return (string) null;
      string text = this.ValueData.Text;
      if (this.ValueData.IsLast)
        return text;
      return text + ", ";
    }

    protected override void SetClient(IGroupValueClient client)
    {
      if (this.ValueData == null)
        return;
      this.ValueData.SetGroupValueClient(client);
    }
  }
}
