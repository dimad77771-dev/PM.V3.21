// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupRowLayout.GroupContainer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm.Native;
using System;

namespace DevExpress.Xpf.Grid.GroupRowLayout
{
  public class GroupContainer : GroupPanelItemContainer<Group>
  {
    public Column Get(IndexDefinition index)
    {
      return this.GetLayer(index).Return<Layer, Column>((Func<Layer, Column>) (l => l.Get(index.Column)), (Func<Column>) (() => (Column) null));
    }

    public void Add(Column column, IndexDefinition index)
    {
      this.GetLayer(index).Do<Layer>((Action<Layer>) (l => l.Add(column, index.Column)));
    }

    public void Remove(IndexDefinition index)
    {
      this.GetLayer(index).Do<Layer>((Action<Layer>) (l => l.Remove(index.Column)));
    }

    private Layer GetLayer(IndexDefinition index)
    {
      return this.Get(index.Group).Return<Group, Layer>((Func<Group, Layer>) (g => g.Get(index.Layer)), (Func<Layer>) (() => (Layer) null));
    }
  }
}
