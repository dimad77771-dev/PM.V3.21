// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridControlSerializationProvider
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Utils.Serializing;
using DevExpress.Utils.Serializing.Helpers;
using DevExpress.Xpf.Core.Serialization;

namespace DevExpress.Xpf.Grid
{
  internal class GridControlSerializationProvider : SerializationProvider
  {
    protected override object OnCreateCollectionItem(XtraCreateCollectionItemEventArgs e)
    {
      object collectionItem = base.OnCreateCollectionItem(e);
      if (collectionItem == null && e.Source is IXtraSupportDeserializeCollectionItem)
        collectionItem = ((IXtraSupportDeserializeCollectionItem) e.Source).CreateCollectionItem(e.CollectionName, new XtraItemEventArgs(e.Owner, e.Collection, e.Item));
      return collectionItem;
    }
  }
}
