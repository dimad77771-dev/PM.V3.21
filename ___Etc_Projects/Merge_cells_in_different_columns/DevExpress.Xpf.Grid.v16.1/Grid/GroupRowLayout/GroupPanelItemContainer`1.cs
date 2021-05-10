// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupRowLayout.GroupPanelItemContainer`1
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections;
using System.Collections.Generic;

namespace DevExpress.Xpf.Grid.GroupRowLayout
{
  public abstract class GroupPanelItemContainer<T> : IGroupPanelItem, IGroupPanelItemOwner, IEnumerable<T>, IEnumerable where T : IGroupPanelItem
  {
    private SortedList<int, T> children = new SortedList<int, T>();
    private IGroupPanelItemOwner ownerCore;

    public int Count
    {
      get
      {
        return this.children.Count;
      }
    }

    public IGroupPanelItemOwner Parent
    {
      get
      {
        return this.ownerCore;
      }
      set
      {
        if (this.ownerCore == value)
          return;
        IGroupPanelItemOwner oldValue = this.ownerCore;
        this.ownerCore = value;
        this.OnItemOwnerReplaced(oldValue);
      }
    }

    public int Index { get; set; }

    public void Add(T child, int index)
    {
      child.Index = index;
      this.children.Add(child.Index, child);
      child.Parent = (IGroupPanelItemOwner) this;
    }

    public void Remove(int index)
    {
      T obj = this.Get(index);
      if (this.children == null)
        return;
      obj.Index = 0;
      this.children.Remove(index);
      obj.Parent = (IGroupPanelItemOwner) null;
    }

    public T Get(int index)
    {
      T obj = default (T);
      this.children.TryGetValue(index, out obj);
      return obj;
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      return this.GetEnumeratorCore();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumeratorCore();
    }

    private IEnumerator<T> GetEnumeratorCore()
    {
      return this.children.Values.GetEnumerator();
    }

    public void OnItemOwnerReplaced(IGroupPanelItemOwner oldValue)
    {
      foreach (T obj in (IEnumerable<T>) this)
        obj.OnItemOwnerReplaced(oldValue);
    }

    void IGroupPanelItemOwner.Connect(Column column)
    {
      if (this.Parent == null)
        return;
      this.Parent.Connect(column);
    }

    void IGroupPanelItemOwner.Disconnect(Column column)
    {
      if (this.Parent == null)
        return;
      this.Parent.Disconnect(column);
    }
  }
}
