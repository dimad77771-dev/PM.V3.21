// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.StubNode
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting.DataNodes;

namespace DevExpress.Xpf.Grid.Printing
{
  public class StubNode : PrintingNodeBase, IVisualDetailNode, IDataNode
  {
    protected override bool IsDetailContainerCore
    {
      get
      {
        return false;
      }
    }

    public StubNode(DataTreeBuilder treeBuilder, IDataNode parent, int index)
      : base(treeBuilder, parent, index)
    {
    }

    protected override IDataNode GetChildCore(int index)
    {
      return (IDataNode) null;
    }

    protected override bool CanGetChildCore(int index)
    {
      return false;
    }

    public RowViewInfo GetDetail(bool allowContentReuse)
    {
      return (RowViewInfo) null;
    }
  }
}
