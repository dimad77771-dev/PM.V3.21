﻿// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListNodeObject
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListNodeObject
  {
    public TreeListNode Node { get; private set; }

    public int Level { get; internal set; }

    public List<TreeListNodeObject> Nodes { get; private set; }

    public TreeListNodeObject(TreeListNode node)
    {
      this.Node = node;
      this.Nodes = new List<TreeListNodeObject>();
    }
  }
}
