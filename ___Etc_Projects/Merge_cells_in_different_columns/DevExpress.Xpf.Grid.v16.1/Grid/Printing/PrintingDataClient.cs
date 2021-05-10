// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.PrintingDataClient
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using System.ComponentModel;

namespace DevExpress.Xpf.Grid.Printing
{
  public class PrintingDataClient : IDataControllerData2, IDataControllerData
  {
    private IDataControllerData2 realClient;

    public bool CanUseFastProperties
    {
      get
      {
        return this.realClient.CanUseFastProperties;
      }
    }

    public bool HasUserFilter
    {
      get
      {
        return this.realClient.HasUserFilter;
      }
    }

    public PrintingDataClient(IDataControllerData2 realClient)
    {
      this.realClient = realClient;
    }

    public UnboundColumnInfoCollection GetUnboundColumns()
    {
      return this.realClient.GetUnboundColumns();
    }

    public object GetUnboundData(int listSourceRow1, DataColumnInfo column, object value)
    {
      return this.realClient.GetUnboundData(listSourceRow1, column, value);
    }

    public void SetUnboundData(int listSourceRow1, DataColumnInfo column, object value)
    {
    }

    public ComplexColumnInfoCollection GetComplexColumns()
    {
      return this.realClient.GetComplexColumns();
    }

    public void SubstituteFilter(SubstituteFilterEventArgs args)
    {
    }

    public bool? IsRowFit(int listSourceRow, bool fit)
    {
      return this.realClient.IsRowFit(listSourceRow, fit);
    }

    public PropertyDescriptorCollection PatchPropertyDescriptorCollection(PropertyDescriptorCollection collection)
    {
      return this.realClient.PatchPropertyDescriptorCollection(collection);
    }
  }
}
