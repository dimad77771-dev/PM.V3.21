// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.StateGridDataController
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Helpers;
using DevExpress.Data.Selection;
using DevExpress.Mvvm.UI.Native.ViewGenerator;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using System.ComponentModel;
using System.Windows.Threading;

namespace DevExpress.Xpf.Data
{
  public class StateGridDataController : DXGridDataController
  {
    private readonly IDataProviderOwner owner;

    protected override Dispatcher Dispatcher
    {
      get
      {
        return this.owner.Dispatcher;
      }
    }

    public StateGridDataController(IDataProviderOwner owner)
    {
      this.owner = owner;
    }

    protected override SelectionController CreateSelectionController()
    {
      return (SelectionController) new RowStateController((DataController) this);
    }

    public override ListSourceRowsKeeper CreateControllerRowsKeeper()
    {
      return (ListSourceRowsKeeper) new RowStateKeeper((DataController) this, this.CreateSelectionKeeper());
    }

    public override void BeginCurrentRowEdit()
    {
      base.BeginCurrentRowEdit();
    }

    public override void CancelCurrentRowEdit()
    {
      base.CancelCurrentRowEdit();
      this.owner.RaiseCurrentRowCanceled(new ControllerRowEventArgs(this.CurrentControllerRow, this.GetRow(this.CurrentControllerRow)));
    }

    protected override CustomSummaryEventArgs CreateCustomSummaryEventArgs()
    {
      return (CustomSummaryEventArgs) new GridCustomSummaryEventArgs(this.owner as GridControl);
    }

    protected override CustomSummaryExistEventArgs CreateCustomSummaryExistEventArgs(GroupRowInfo groupRow, object item)
    {
      return (CustomSummaryExistEventArgs) new GridCustomSummaryExistEventArgs(this.owner as GridControl, groupRow, item);
    }

    protected override void OnCurrentControllerRowChanging(int oldControllerRow, int newControllerRow)
    {
      this.owner.OnCurrentIndexChanging(newControllerRow);
      base.OnCurrentControllerRowChanging(oldControllerRow, newControllerRow);
    }

    protected override void OnBindingListChanged(ListChangedEventArgs e)
    {
      if (e.ListChangedType == ListChangedType.ItemChanged && e.OldIndex != -1 && this.owner.AllowLiveDataShaping.HasValue && !this.owner.AllowLiveDataShaping.Value)
        return;
      base.OnBindingListChanged(e);
    }

    protected override TypeConverter GetActualTypeConverter(TypeConverter converter, PropertyDescriptor property)
    {
      return DataColumnAttributesExtensions.GetActualTypeConverter(property, this.owner.ItemType, converter);
    }

    protected override void OnItemMoved(ListChangedEventArgs e, DataControllerChangedItemCollection changedItems)
    {
      base.OnItemMoved(e, changedItems);
      if (e.OldIndex < 0 || e.NewIndex < 0 || this.SortInfo.Count <= 0)
        return;
      changedItems.AddItem(this.GetControllerRow(e.OldIndex), NotifyChangeType.ItemChanged, (GroupRowInfo) null);
      changedItems.AddItem(this.GetControllerRow(e.NewIndex), NotifyChangeType.ItemChanged, (GroupRowInfo) null);
    }

    protected override void OnBindingListChangedCore(ListChangedEventArgs e)
    {
      int controllerRow = this.GetControllerRow(e.NewIndex);
      base.OnBindingListChangedCore(e);
      this.owner.RowChanged(e.ListChangedType, this.GetControllerRow(e.NewIndex), new int?(controllerRow));
    }
  }
}
