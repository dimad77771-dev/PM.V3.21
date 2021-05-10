// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListUtils
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;

namespace DevExpress.Xpf.Grid.TreeList
{
  public static class TreeListUtils
  {
    public static void CalcNodeIndents(TreeListNode node, List<TreeListIndentType> indents, bool showRootIndent)
    {
      indents.Clear();
      int actualLevel = node.ActualLevel;
      int num = showRootIndent ? 0 : 1;
      TreeListNode visibleParent = node.VisibleParent;
      for (int index = actualLevel; index >= num; --index)
      {
        TreeListIndentType treeListIndentType;
        if (index == actualLevel)
        {
          treeListIndentType = actualLevel != 0 || !node.IsFirstVisible || !node.IsLastVisible ? (actualLevel != 0 || !node.IsFirstVisible ? (!node.IsLastVisible ? TreeListIndentType.Middle : TreeListIndentType.Last) : TreeListIndentType.First) : TreeListIndentType.Root;
        }
        else
        {
          treeListIndentType = visibleParent.IsLastVisible ? TreeListIndentType.None : TreeListIndentType.Line;
          visibleParent = visibleParent.VisibleParent;
        }
        indents.Add(treeListIndentType);
      }
      indents.Reverse();
    }
  }
}
