// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CellItemsControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class CellItemsControl : CellItemsControlBase
  {
    private TableViewBehavior TableViewBehavior
    {
      get
      {
        if (this.View == null)
          return (TableViewBehavior) null;
        return this.View.ViewBehavior as TableViewBehavior;
      }
    }

    protected override FrameworkElement CreateChildCore(GridCellData cellData)
    {
      return (FrameworkElement) new GridCellContentPresenter(cellData);
    }

    protected override void ValidateElementCore(FrameworkElement element, GridCellData cellData)
    {
      GridCellContentPresenter contentPresenter = (GridCellContentPresenter) element;
      if (!contentPresenter.ShouldSyncProperties)
        return;
      contentPresenter.SyncProperties(cellData);
    }

    protected override void OnCurrentViewChanged()
    {
      base.OnCurrentViewChanged();
      if (this.TableViewBehavior == null)
        return;
      this.TableViewBehavior.OnCellItemsControlLoaded();
    }
  }
}
