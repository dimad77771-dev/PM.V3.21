// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.GridDataControllerThreadClient
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;

namespace DevExpress.Xpf.Data
{
  internal class GridDataControllerThreadClient : IDataControllerThreadClient
  {
    private GridDataProvider dataProvider;

    public GridDataControllerThreadClient(GridDataProvider dataProvider)
    {
      this.dataProvider = dataProvider;
    }

    public void OnAsyncBegin()
    {
      this.dataProvider.IsAsyncOperationInProgress = true;
    }

    public void OnAsyncEnd()
    {
      this.dataProvider.IsAsyncOperationInProgress = false;
    }

    public void OnRowLoaded(int controllerRowHandle)
    {
      this.IncrementLoadedRowsCount();
    }

    private void IncrementLoadedRowsCount()
    {
    }

    public void OnTotalsReceived()
    {
      this.dataProvider.OnAsyncTotalsReceived();
    }
  }
}
