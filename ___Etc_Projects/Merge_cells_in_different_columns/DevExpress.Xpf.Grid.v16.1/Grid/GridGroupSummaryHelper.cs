// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridGroupSummaryHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Summary;
using DevExpress.Xpf.Core;

namespace DevExpress.Xpf.Grid
{
  public class GridGroupSummaryHelper : GridSummaryHelper
  {
    protected override ISummaryItemOwner SummaryItems
    {
      get
      {
        return (ISummaryItemOwner) ((GridControl) this.view.DataControl).GroupSummary;
      }
    }

    public GridGroupSummaryHelper(DataViewBase view)
      : base(view)
    {
    }

    protected override string GetEditorTitle()
    {
      return this.view.GetLocalizedString(GridControlStringId.GroupSummaryEditorFormCaption);
    }

    protected internal override bool CanUseSummaryItem(ISummaryItem item)
    {
      IGroupFooterSummaryItem footerSummaryItem = item as IGroupFooterSummaryItem;
      if (footerSummaryItem != null && !string.IsNullOrEmpty(footerSummaryItem.ShowInGroupColumnFooter))
        return false;
      if (!(this.view is IGroupSummaryDisplayMode) || item.SummaryType != SummaryItemType.Count)
        return base.CanUseSummaryItem(item);
      if ((this.view as IGroupSummaryDisplayMode).GroupSummaryDisplayMode == GroupSummaryDisplayMode.Default)
        return item.FieldName == "";
      return base.CanUseSummaryItem(item);
    }

    public override FloatingContainer ShowSummaryEditor()
    {
      return this.ShowSummaryEditor(SummaryEditorType.GroupSummary);
    }
  }
}
