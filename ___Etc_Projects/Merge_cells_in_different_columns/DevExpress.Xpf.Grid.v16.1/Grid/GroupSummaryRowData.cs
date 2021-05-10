// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupSummaryRowData
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Data;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Utils;
using System;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class GroupSummaryRowData : GroupRowData
  {
    private static readonly DependencyPropertyKey FooterPositionPropertyKey = DependencyPropertyManager.RegisterReadOnly("FooterPosition", typeof (RowPosition), typeof (GroupSummaryRowData), (PropertyMetadata) new FrameworkPropertyMetadata((object) RowPosition.Single));
    public static readonly DependencyProperty FooterPositionProperty = GroupSummaryRowData.FooterPositionPropertyKey.DependencyProperty;

    public RowPosition FooterPosition
    {
      get
      {
        return (RowPosition) this.GetValue(GroupSummaryRowData.FooterPositionProperty);
      }
      protected set
      {
        this.SetValue(GroupSummaryRowData.FooterPositionPropertyKey, (object) value);
      }
    }

    protected internal GroupSummaryRowKey MatchKeyCore { get; private set; }

    internal override object MatchKey
    {
      get
      {
        return (object) this.MatchKeyCore;
      }
    }

    internal override FrameworkElement RowElement
    {
      get
      {
        return ((GroupFooterRowControl) this.WholeRowElement).GroupFooterContentPresenter;
      }
    }

    protected override bool IsGroupFooter
    {
      get
      {
        return true;
      }
    }

    public GroupSummaryRowData(DataTreeBuilder treeBuilder, RowHandle rowHandle)
      : base(treeBuilder)
    {
      this.RowHandle = rowHandle;
      this.MatchKeyCore = new GroupSummaryRowKey(this.RowHandle, this.Level);
    }

    protected override RowDataBase.NotImplementedRowDataReusingStrategy CreateReusingStrategy(Func<FrameworkElement> createRowElementDelegate)
    {
      return (RowDataBase.NotImplementedRowDataReusingStrategy) new GroupSummaryRowData.GroupSummaryRowDataReusingStrategy(this);
    }

    protected override void CacheRowData()
    {
      this.VisualDataTreeBuilder.CacheGroupSummaryRowData((RowData) this);
    }

    internal override void UpdateRow()
    {
    }

    protected internal override void EnsureRowLoaded()
    {
    }

    protected override FrameworkElement CreateRowElement()
    {
      return (FrameworkElement) new GroupFooterRowControl();
    }

    protected internal override double GetFixedNoneContentWidth(double totalWidth)
    {
      return base.GetFixedNoneContentWidth(totalWidth) + this.GetExpandDetailHeaderWidth();
    }

    internal override void AssignFrom(RowsContainer parentRowsContainer, NodeContainer parentNodeContainer, RowNode rowNode, bool forceUpdate)
    {
      base.AssignFrom(parentRowsContainer, parentNodeContainer, rowNode, forceUpdate);
      if (this.MatchKeyCore == null || this.MatchKeyCore.RowHandle.Equals((object) this.RowHandle))
        return;
      this.MatchKeyCore = new GroupSummaryRowKey(this.RowHandle, this.Level);
    }

    protected double GetExpandDetailHeaderWidth()
    {
      if (!this.TableView.ActualShowDetailButtons)
        return 0.0;
      return this.TableView.ActualExpandDetailHeaderWidth;
    }

    internal override void UpdateLineLevel()
    {
      bool flag1 = this.View.DataProviderBase.IsGroupRowExpanded(this.RowHandle.Value);
      bool flag2 = false;
      bool flag3 = false;
      int num = this.parentNodeContainer.Items.IndexOf(this.node);
      if (this.parentNodeContainer == this.View.RootNodeContainer && num == this.parentNodeContainer.Items.Count - 1)
      {
        this.FooterPosition = flag1 ? (this.FooterPosition = RowPosition.Bottom) : RowPosition.Single;
      }
      else
      {
        if (num > -1)
        {
          if (num < this.parentNodeContainer.Items.Count - 1 && !(this.parentNodeContainer.Items[num + 1] is GroupSummaryRowNode))
            flag2 = true;
          if (num > 0 && this.parentNodeContainer.Items[num - 1] is GroupNode)
            flag3 = true;
        }
        if (!flag1 || this.Level == this.View.DataProviderBase.DataController.GroupInfo.LevelCount - 1)
          this.FooterPosition = !flag2 || !flag3 ? RowPosition.Top : RowPosition.Single;
        else if (flag2 && flag1)
          this.FooterPosition = RowPosition.Bottom;
        else
          this.FooterPosition = RowPosition.Middle;
      }
    }

    protected override bool CanExtractGridGroupSummaryItem(GridSummaryItem summary)
    {
      return !string.IsNullOrEmpty(summary.ShowInGroupColumnFooter);
    }

    protected class GroupSummaryRowDataReusingStrategy : RowData.RowDataReusingStrategy
    {
      protected GroupSummaryRowData CurrentRowData
      {
        get
        {
          return this.rowData as GroupSummaryRowData;
        }
      }

      public GroupSummaryRowDataReusingStrategy(GroupSummaryRowData rowData)
        : base((RowData) rowData)
      {
      }

      internal override void CacheRowData()
      {
        base.CacheRowData();
        ((GroupSummaryRowNode) this.CurrentRowData.node).CurrentRowData = this.CurrentRowData;
      }

      internal override FrameworkElement CreateRowElement()
      {
        return this.CurrentRowData.CreateRowElement();
      }
    }
  }
}
