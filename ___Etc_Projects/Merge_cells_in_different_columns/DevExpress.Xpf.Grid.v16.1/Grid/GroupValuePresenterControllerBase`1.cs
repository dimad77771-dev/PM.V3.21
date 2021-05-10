// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupValuePresenterControllerBase`1
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid
{
  internal abstract class GroupValuePresenterControllerBase<T> : IGroupValueClient where T : GridColumnData
  {
    private T valueDataCore;

    protected internal T ValueData
    {
      get
      {
        return this.valueDataCore;
      }
      set
      {
        if ((object) this.valueDataCore == (object) value)
          return;
        this.SetClient((IGroupValueClient) null);
        this.valueDataCore = value;
        this.SetClient((IGroupValueClient) this);
        this.UpdateColumnHeader();
        this.UpdateText();
      }
    }

    protected abstract void UpdateText();

    protected abstract void UpdateColumnHeader();

    protected abstract void SetClient(IGroupValueClient client);

    void IGroupValueClient.UpdateText()
    {
      this.UpdateText();
    }

    void IGroupValueClient.UpdateColumnHeader()
    {
      this.UpdateColumnHeader();
    }
  }
}
