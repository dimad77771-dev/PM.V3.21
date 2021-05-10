// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupSummaryRowNode
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using System.Collections.Generic;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class GroupSummaryRowNode : GroupNode
  {
    internal GroupSummaryRowData CurrentRowData { get; set; }

    protected GroupSummaryRowKey MatchKeyCore { get; private set; }

    public override object MatchKey
    {
      get
      {
        return (object) this.MatchKeyCore;
      }
    }

    public GroupSummaryRowNode(DataTreeBuilder treeBuilder, DataControllerValuesContainer controllerValues)
      : base(treeBuilder, controllerValues)
    {
      this.MatchKeyCore = new GroupSummaryRowKey(controllerValues.RowHandle, this.Level);
    }

    internal override LinkedList<FreeRowDataInfo> GetFreeRowDataQueue(SynchronizationQueues synchronizationQueues)
    {
      return synchronizationQueues.FreeGroupSummaryRowDataQueue;
    }

    internal override RowDataBase CreateRowData()
    {
      return (RowDataBase) new GroupSummaryRowData(this.treeBuilder, this.RowHandle);
    }

    internal override RowDataBase GetRowData()
    {
      return (RowDataBase) this.CurrentRowData;
    }

    internal override FrameworkElement GetRowElement()
    {
      return this.CurrentRowData.RowElement;
    }

    public override int SkipChildNodes(int index)
    {
      return index;
    }
  }
}
