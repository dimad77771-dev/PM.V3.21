// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.Native.TreeListDataIterator
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Data;

namespace DevExpress.Xpf.Grid.TreeList.Native
{
  public class TreeListDataIterator : DataIteratorBase
  {
    private TreeListView view
    {
      get
      {
        return (TreeListView) this.viewBase;
      }
    }

    public TreeListDataIterator(TreeListView view)
      : base((DataViewBase) view)
    {
    }

    protected internal override RowNode GetRowNodeForCurrentLevel(DataNodeContainer nodeContainer, int index, int startVisibleIndex, ref bool shouldBreak)
    {
      DataControllerValuesContainer valuesContainer = DataIteratorBase.CreateValuesContainer(nodeContainer.treeBuilder, index);
      return (RowNode) this.GetRowNode(nodeContainer.treeBuilder, startVisibleIndex, valuesContainer);
    }

    protected internal override RowNode GetSummaryNodeForCurrentNode(DataNodeContainer nodeContainer, RowHandle rowHandle, int index)
    {
      return (RowNode) null;
    }
  }
}
