// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListNodeValuesCache
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using System;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListNodeValuesCache
  {
    private TreeListDataProvider provider;
    private object[] values;

    protected virtual TreeListNode CurrentNode
    {
      get
      {
        return this.Provider.CurrentNode;
      }
    }

    protected bool CanSave
    {
      get
      {
        return this.CurrentNode != null;
      }
    }

    protected bool CanRestore
    {
      get
      {
        if (this.values != null)
          return this.CanSave;
        return false;
      }
    }

    protected object[] Values
    {
      get
      {
        return this.values;
      }
    }

    protected TreeListDataProvider Provider
    {
      get
      {
        return this.provider;
      }
    }

    public TreeListNodeValuesCache(TreeListDataProvider provider)
    {
      this.provider = provider;
    }

    public void Clear()
    {
      this.values = (object[]) null;
    }

    public void SaveValues()
    {
      this.Clear();
      if (!this.CanSave)
        return;
      this.values = new object[this.Provider.Columns.Count];
      for (int index = 0; index < this.Provider.Columns.Count; ++index)
      {
        DataColumnInfo columnInfo = this.Provider.Columns[index];
        if (!columnInfo.ReadOnly)
          this.Values[index] = this.Provider.DataHelper.GetValue(this.CurrentNode, columnInfo);
      }
    }

    public void RestoreValues()
    {
      try
      {
        if (!this.CanRestore)
          return;
        for (int index = 0; index < this.Provider.Columns.Count; ++index)
        {
          DataColumnInfo columnInfo = this.Provider.Columns[index];
          if (!columnInfo.ReadOnly)
          {
            object x = this.Provider.DataHelper.GetValue(this.CurrentNode, columnInfo);
            object y = this.Values[index];
            bool flag;
            try
            {
              if (!(x is IComparable))
                x = (object) null;
              if (!(y is IComparable))
                y = (object) null;
              flag = this.Provider.ValueComparer.Compare(x, y) != 0;
            }
            catch
            {
              continue;
            }
            if (flag)
              this.Provider.DataHelper.SetValue(this.CurrentNode, columnInfo, this.Values[index]);
          }
        }
      }
      catch
      {
      }
      this.Clear();
    }
  }
}
