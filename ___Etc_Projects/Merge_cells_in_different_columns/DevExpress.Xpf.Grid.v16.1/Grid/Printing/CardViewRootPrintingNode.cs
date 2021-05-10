// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.CardViewRootPrintingNode
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using DevExpress.XtraPrinting.DataNodes;
using System;
using System.Windows;

namespace DevExpress.Xpf.Grid.Printing
{
  public class CardViewRootPrintingNode : CardViewContainerPrintingNodeBase, IRootDataNode, IDataNode, IDisposable
  {
    protected override bool IsDetailContainerCore
    {
      get
      {
        return false;
      }
    }

    public CardViewRootPrintingNode(CardViewPrintingDataTreeBuilder treeBuilder, Size pageSize)
      : base((DataNodeContainer) null, treeBuilder, (IDataNode) null, -1, pageSize)
    {
    }

    protected override bool CanGetChildCore(int index)
    {
      return index < 1;
    }

    protected override IDataNode GetChildCore(int index)
    {
      return (IDataNode) new CardViewGroupRootPrintingNode(this.TreeBuilder, (IDataNode) this, this.pageSize);
    }

    int IRootDataNode.GetTotalDetailCount()
    {
      return this.GetMasterRowsCount();
    }

    void IDisposable.Dispose()
    {
      this.TreeBuilder.OnRootNodeDispose();
    }

    private int GetMasterRowsCount()
    {
      VirtualItemsEnumerator virtualItemsEnumerator = new VirtualItemsEnumerator((NodeContainer) this.TreeBuilder.RootNodeContainer);
      int num = 0;
      while (virtualItemsEnumerator.MoveNext())
        ++num;
      return num;
    }
  }
}
