// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.StateServerModeDataController
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Helpers;
using DevExpress.Data.Linq;
using DevExpress.Data.Selection;
using DevExpress.Xpf.Editors.Helpers;
using System;

namespace DevExpress.Xpf.Data
{
  public class StateServerModeDataController : ServerModeDataController
  {
    protected readonly IDataProviderOwner owner;

    private IListServerRefreshable ListServerRefreshable
    {
      get
      {
        return this.DataSource as IListServerRefreshable;
      }
    }

    internal bool IsInRefresh
    {
      get
      {
        return this.IsRefreshInProgress;
      }
    }

    public StateServerModeDataController(IDataProviderOwner owner)
    {
      this.owner = owner;
    }

    protected override void OnDataSourceChanged()
    {
      base.OnDataSourceChanged();
      if (this.ListServerRefreshable == null)
        return;
      this.ListServerRefreshable.Refresh += new EventHandler(this.OnListServerRefresh);
    }

    private void OnListServerRefresh(object sender, EventArgs e)
    {
      if (this.IsRefreshInProgress)
        return;
      this.DoRefresh();
    }

    public override void Dispose()
    {
      if (this.ListServerRefreshable != null)
        this.ListServerRefreshable.Refresh -= new EventHandler(this.OnListServerRefresh);
      base.Dispose();
    }

    protected override SelectionController CreateSelectionController()
    {
      return (SelectionController) new RowStateController((DevExpress.Data.DataController) this);
    }

    protected override bool UseFirstRowTypeWhenPopulatingColumns(Type rowType)
    {
      return rowType.FullName == ListDataControllerHelper.UseFirstRowTypeWhenPopulatingColumnsTypeName;
    }

    public override void CancelCurrentRowEdit()
    {
      base.CancelCurrentRowEdit();
      this.owner.RaiseCurrentRowCanceled(new ControllerRowEventArgs(this.CurrentControllerRow, this.GetRow(this.CurrentControllerRow)));
    }

    protected override bool CanSortColumnCore(DataColumnInfo column)
    {
      if (this.ListSourceEx2 == null)
        return base.CanSortColumnCore(column);
      return this.ListSourceEx2.CanSort;
    }

    protected override void OnDataSync_FilterSortGroupInfoChanged(object sender, CollectionViewFilterSortGroupInfoChangedEventArgs e)
    {
      int count = this.Columns.Count;
      base.OnDataSync_FilterSortGroupInfoChanged(sender, e);
      if (!(this.DataSync is ICollectionViewHelper) || this.Columns.Count <= count)
        return;
      this.UpdateTotalSummary();
    }
  }
}
