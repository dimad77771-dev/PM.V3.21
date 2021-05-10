// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GridPrintingNodeBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using DevExpress.XtraPrinting.DataNodes;

namespace DevExpress.Xpf.Grid.Printing
{
  public abstract class GridPrintingNodeBase : PrintingNodeBase
  {
    protected internal PrintingDataTreeBuilder TreeBuilder
    {
      get
      {
        return base.TreeBuilder as PrintingDataTreeBuilder;
      }
    }

    protected GridPrintingNodeBase(PrintingDataTreeBuilderBase treeBuilder, IDataNode parent, int index)
      : base((DataTreeBuilder) treeBuilder, parent, index)
    {
    }
  }
}
