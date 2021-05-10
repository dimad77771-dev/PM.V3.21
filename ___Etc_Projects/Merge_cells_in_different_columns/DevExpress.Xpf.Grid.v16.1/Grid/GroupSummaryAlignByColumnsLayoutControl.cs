// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupSummaryAlignByColumnsLayoutControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class GroupSummaryAlignByColumnsLayoutControl : Control
  {
    private BandedViewContentSelector selector;
    private double offset;

    public GroupSummaryAlignByColumnsLayoutControl()
    {
      this.SetDefaultStyleKey(typeof (GroupSummaryAlignByColumnsLayoutControl));
    }

    internal void SetContentOffset(double newOffset)
    {
      GroupRowData groupRowData = this.DataContext as GroupRowData;
      ITableView tableView = (ITableView) groupRowData.View;
      newOffset -= groupRowData != null ? (double) groupRowData.Level * tableView.LeftGroupAreaIndent : 0.0;
      newOffset += tableView.ActualShowDetailButtons ? tableView.ActualExpandDetailHeaderWidth : 0.0;
      newOffset -= tableView.ShowIndicator ? 0.0 : tableView.LeftDataAreaIndent;
      if (this.offset == newOffset)
        return;
      this.offset = newOffset;
      this.UpdateSelector();
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.selector = this.GetTemplateChild("PART_BandedViewContentSelector") as BandedViewContentSelector;
    }

    private void UpdateSelector()
    {
      if (this.selector == null)
        return;
      this.selector.Margin = new Thickness(this.offset, 0.0, 0.0, 0.0);
    }
  }
}
