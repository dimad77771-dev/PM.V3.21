// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListColumnMenuInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Bars;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class TreeListColumnMenuInfo : ColumnMenuInfoBase
  {
    protected TreeListView View
    {
      get
      {
        return base.View as TreeListView;
      }
    }

    public TreeListColumnMenuInfo(TreeListPopupMenu menu)
      : base((DataControlPopupMenu) menu)
    {
    }

    protected override void CreateItemsCore()
    {
      this.CreateSortingItems();
      this.CreateColumnChooserItems();
      this.CreateBestFitItems();
      this.CreateExpressionEditorItems();
      this.CreateFilterControlItems();
      this.CreateFixedStyleItems();
      this.CreateSearchPanelItems();
      this.CreateConditionalFormattingMenuItems();
    }

    private void CreateBestFitItems()
    {
      BarButtonItem barButtonItem = this.CreateBarButtonItem("BestFit", GridControlStringId.MenuColumnBestFit, false, (ImageSource) ImageHelper.GetImage("BestFit"), this.View.TreeListCommands.BestFitColumn, (object) null);
      barButtonItem.CommandParameter = (object) this.Column;
      barButtonItem.IsVisible = this.View.ViewBehavior.CanBestFitColumnCore(this.Column) && this.View.IsColumnVisibleInHeaders((BaseColumn) this.Column);
      this.CreateBarButtonItem("BestFitColumns", GridControlStringId.MenuColumnBestFitColumns, false, (ImageSource) null, this.View.TreeListCommands.BestFitColumns, (object) null).IsVisible = this.View.ViewBehavior.CanBestFitAllColumns();
    }
  }
}
