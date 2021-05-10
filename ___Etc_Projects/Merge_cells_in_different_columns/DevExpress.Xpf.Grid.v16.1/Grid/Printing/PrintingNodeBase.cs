// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.PrintingNodeBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting.DataNodes;
using System.Windows;

namespace DevExpress.Xpf.Grid.Printing
{
  public abstract class PrintingNodeBase : IDataNode
  {
    private readonly IDataNode parent;
    protected readonly int fIndex;
    private readonly DataTreeBuilder _treeBuilder;

    protected internal DataTreeBuilder TreeBuilder
    {
      get
      {
        return this._treeBuilder;
      }
    }

    int IDataNode.Index
    {
      get
      {
        return this.fIndex;
      }
    }

    IDataNode IDataNode.Parent
    {
      get
      {
        return this.parent;
      }
    }

    bool IDataNode.IsDetailContainer
    {
      get
      {
        return this.IsDetailContainerCore;
      }
    }

    bool IDataNode.PageBreakBefore
    {
      get
      {
        return false;
      }
    }

    bool IDataNode.PageBreakAfter
    {
      get
      {
        return false;
      }
    }

    protected abstract bool IsDetailContainerCore { get; }

    protected PrintingNodeBase(DataTreeBuilder treeBuilder, IDataNode parent, int index)
    {
      this._treeBuilder = treeBuilder;
      this.parent = parent;
      this.fIndex = index;
    }

    bool IDataNode.CanGetChild(int index)
    {
      return this.CanGetChildCore(index);
    }

    IDataNode IDataNode.GetChild(int index)
    {
      return this.GetChildCore(index);
    }

    protected abstract IDataNode GetChildCore(int index);

    protected abstract bool CanGetChildCore(int index);

    protected RowViewInfo CreateRowElement(RowData rowData, DataTemplate template)
    {
      if (template == null)
        return (RowViewInfo) null;
      return new RowViewInfo(template, (object) rowData);
    }
  }
}
