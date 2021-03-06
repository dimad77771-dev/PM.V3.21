// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.PrintingVisualClient
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;

namespace DevExpress.Xpf.Grid.Printing
{
  public class PrintingVisualClient : IDataControllerVisualClient
  {
    private IDataControllerVisualClient realClient;

    public bool IsInitializing
    {
      get
      {
        return false;
      }
    }

    public int PageRowCount
    {
      get
      {
        return this.realClient.PageRowCount;
      }
    }

    public int TopRowIndex
    {
      get
      {
        return this.realClient.TopRowIndex;
      }
    }

    public int VisibleRowCount
    {
      get
      {
        return this.realClient.VisibleRowCount;
      }
    }

    public PrintingVisualClient(IDataControllerVisualClient realClient)
    {
      this.realClient = realClient;
    }

    public void ColumnsRenewed()
    {
    }

    public void RequestSynchronization()
    {
      this.realClient.RequestSynchronization();
    }

    public void RequireSynchronization(IDataSync dataSync)
    {
    }

    public void UpdateColumns()
    {
    }

    public void UpdateLayout()
    {
    }

    public void UpdateRow(int controllerRowHandle)
    {
    }

    public void UpdateRowIndexes(int newTopRowIndex)
    {
    }

    public void UpdateRows(int topRowIndexDelta)
    {
    }

    public void UpdateScrollBar()
    {
    }

    public void UpdateTotalSummary()
    {
    }
  }
}
