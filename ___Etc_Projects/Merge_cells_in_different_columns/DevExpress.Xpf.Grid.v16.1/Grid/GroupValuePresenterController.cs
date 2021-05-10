// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupValuePresenterController
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid
{
  internal class GroupValuePresenterController : GroupValuePresenterControllerBase<GridGroupValueData>
  {
    private readonly GroupValuePresenter presenter;

    public GroupValuePresenterController(GroupValuePresenter presenter)
    {
      this.presenter = presenter;
    }

    protected override void UpdateText()
    {
      this.presenter.Text = this.ValueData != null ? this.ValueData.Text : (string) null;
      this.presenter.HighlightingProperties = this.ValueData != null ? this.ValueData.HighlightingProperties : (GroupTextHighlightingProperties) null;
    }

    protected override void UpdateColumnHeader()
    {
      this.presenter.ColumnHeader = this.ValueData != null ? this.ValueData.ColumnHeader : (string) null;
    }

    protected override void SetClient(IGroupValueClient client)
    {
      if (this.ValueData == null)
        return;
      this.ValueData.SetGroupValueClient(client);
    }
  }
}
