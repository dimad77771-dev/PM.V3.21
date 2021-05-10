// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.GridGroupSummarySortInfoDeserializable
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Utils.Serializing;
using System.ComponentModel;

namespace DevExpress.Xpf.Grid.Native
{
  public class GridGroupSummarySortInfoDeserializable : GridGroupSummarySortInfo
  {
    [XtraSerializableProperty]
    public new string FieldName
    {
      get
      {
        return base.FieldName;
      }
      set
      {
        base.FieldName = value;
      }
    }

    [XtraSerializableProperty]
    public new ListSortDirection SortOrder
    {
      get
      {
        return base.SortOrder;
      }
      set
      {
        base.SortOrder = value;
      }
    }

    [XtraSerializableProperty]
    public new int SummaryItemIndex
    {
      get
      {
        return base.SummaryItemIndex;
      }
      set
      {
        base.SummaryItemIndex = value;
      }
    }
  }
}
