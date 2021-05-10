// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.RootTreeListNode
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.TreeList;

namespace DevExpress.Xpf.Grid
{
  internal class RootTreeListNode : TreeListNode
  {
    public override bool IsExpanded
    {
      get
      {
        return true;
      }
      set
      {
      }
    }

    public RootTreeListNode(TreeListDataProvider provider)
    {
      this.DataProvider = provider;
    }

    public static bool IsRootNode(TreeListNode node)
    {
      return node is RootTreeListNode;
    }
  }
}
