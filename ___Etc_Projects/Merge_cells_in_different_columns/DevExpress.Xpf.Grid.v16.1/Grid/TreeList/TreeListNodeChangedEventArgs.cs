// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListNodeChangedEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListNodeChangedEventArgs : EventArgs
  {
    public NodeChangeType ChangeType { get; protected set; }

    public TreeListNode Node { get; protected set; }

    public TreeListNodeChangedEventArgs(TreeListNode node, NodeChangeType changeType)
    {
      this.Node = node;
      this.ChangeType = changeType;
    }
  }
}
