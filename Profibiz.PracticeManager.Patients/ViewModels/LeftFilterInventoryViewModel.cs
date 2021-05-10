using DevExpress.DevAV.ViewModels;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Profibiz.PracticeManager.Infrastructure;
using Profibiz.PracticeManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm.POCO;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PropertyChanged;
using System.ComponentModel;
using System.Web;
using Profibiz.PracticeManager.Patients.BusinessService;


namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[ImplementPropertyChanged]
	public class LeftFilterInventoryViewModel : ILeftPanelViewModel
	{
		public LeftFilterInventoryViewModel()
		{
			BuildAllFilters();
		}
		


		void BuildAllFilters()
		{
			AllCategoriesItems.Add(InventoryFilter);
			AllCategoriesItems.Add(BalanceFilter);
			AllCategoriesItems.Add(OrdersFilter);
			AllCategoriesItems.Add(PaymentsFilter);
			AllCategoriesItems.Add(RefundsFilter);
			StaticFilterItems = AllCategoriesItems.ToObservableCollection();
		}

		public void Init()
		{
			SelectedItem = InventoryFilter;
			if (RuntimeHelper.IsMachineD)
			{
				//SelectedItem = InventoryFilter;
				//SelectedItem = BalanceFilter;
				SelectedItem = OrdersFilter;
				//SelectedItem = PaymentsFilter;
				//SelectedItem = RefundsFilter;
			}
		}




		public ObservableCollection<FilterItem> AllCategoriesItems { get; set; } = new ObservableCollection<FilterItem>();
		public FilterItem InventoryFilter { get; set; } = new FilterItem { Name = "Inventory", FilterItemType = FilterItem.FilterItemTypeEnum.Inventory };
		public FilterItem BalanceFilter { get; set; } = new FilterItem { Name = "Balance", FilterItemType = FilterItem.FilterItemTypeEnum.Balance };
		public FilterItem OrdersFilter { get; set; } = new FilterItem { Name = "Orders", FilterItemType = FilterItem.FilterItemTypeEnum.Orders };
		public FilterItem PaymentsFilter { get; set; } = new FilterItem { Name = "Payments", FilterItemType = FilterItem.FilterItemTypeEnum.Payments };
		public FilterItem RefundsFilter { get; set; } = new FilterItem { Name = "Refunds", FilterItemType = FilterItem.FilterItemTypeEnum.Refunds };
		public FilterItem SelectedItem { get; set; }
		public ObservableCollection<FilterItem> StaticFilterItems { get; set; }


		FilterItem CurrentFilterItem;
		public void OnSelectedItemChanged()
		{
			DispatcherUIHelper.Run(() =>
			{
				if (SelectedItem != null && !FilterItem.IsEqual(CurrentFilterItem, SelectedItem))
				{
					var aa = this.GetHashCode();
					RegionHelper.CloseLeftNavigationPopUp();

					CurrentFilterItem = SelectedItem;
					if (CurrentFilterItem.FilterItemType == FilterItem.FilterItemTypeEnum.Inventory)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.InventoryListView, null);
					}
					else if (CurrentFilterItem.FilterItemType == FilterItem.FilterItemTypeEnum.Balance)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.InventoryBalanceView, null);
					}
					else if (CurrentFilterItem.FilterItemType == FilterItem.FilterItemTypeEnum.Orders)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.OrdersListView, null);
					}
					else if (CurrentFilterItem.FilterItemType == FilterItem.FilterItemTypeEnum.Payments)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.SupplierPaymentsListView, null);
					}
					else if (CurrentFilterItem.FilterItemType == FilterItem.FilterItemTypeEnum.Refunds)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.SupplierRefundsListView, null);
					}
				}
			});
		}

		


		[ImplementPropertyChanged]
		public class FilterItem : IFilterItem
		{
			public virtual string Name { get; set; }

			public virtual string DisplayText
			{
				get
				{
					return Name;
				}
			}

			public FilterItemTypeEnum FilterItemType { get; set; }
			public enum FilterItemTypeEnum { Inventory, Balance, Orders, Payments, Refunds }

			public static bool IsEqual(FilterItem item1, FilterItem item2)
			{
				return (item1 != null && item2 != null && item1.FilterItemType == item2.FilterItemType);
			}
		}
	}
}
