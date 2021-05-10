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
	public class LeftFilterChargeoutsViewModel : ILeftPanelViewModel
	{
		public LeftFilterChargeoutsViewModel()
		{
			BuildAllFilters();
		}
		


		void BuildAllFilters()
		{
			AllCategoriesItems.Add(InvoicesFilter);
			AllCategoriesItems.Add(PaymentsFilter);
			AllCategoriesItems.Add(RefundsFilter);
			AllCategoriesItems.Add(EmailSendListFilter);
			StaticFilterItems = AllCategoriesItems.ToObservableCollection();
		}

		public void Init()
		{
			SelectedItem = InvoicesFilter;
			if (RuntimeHelper.IsMachineD)
			{
				//SelectedItem = InvoicesFilter;
				//SelectedItem = PaymentsFilter;
				SelectedItem = RefundsFilter;
				//SelectedItem = EmailSendListFilter;
			}
		}




		public ObservableCollection<FilterItem> AllCategoriesItems { get; set; } = new ObservableCollection<FilterItem>();
		public FilterItem InvoicesFilter { get; set; } = new FilterItem { Name = "Invoices", FilterItemType = FilterItem.FilterItemTypeEnum.ChargeOuts };
		public FilterItem PaymentsFilter { get; set; } = new FilterItem { Name = "Payments", FilterItemType = FilterItem.FilterItemTypeEnum.Payments };
		public FilterItem RefundsFilter { get; set; } = new FilterItem { Name = "Refunds", FilterItemType = FilterItem.FilterItemTypeEnum.Refunds };
		public FilterItem EmailSendListFilter { get; set; } = new FilterItem { Name = "Send Emails", FilterItemType = FilterItem.FilterItemTypeEnum.EmailSendList };
		public FilterItem SelectedItem { get; set; }
		public ObservableCollection<FilterItem> StaticFilterItems { get; set; }

		bool ignoreOnSelectedItemChanged = false;
		FilterItem CurrentFilterItem;
		public void OnSelectedItemChanged()
		{
			if (ignoreOnSelectedItemChanged) return;

			DispatcherUIHelper.Run(() =>
			{
				if (SelectedItem != null && !FilterItem.IsEqual(CurrentFilterItem, SelectedItem))
				{
					if (!RegionHelper.OnCloseRegion())
					{
						ignoreOnSelectedItemChanged = true;
						SelectedItem = CurrentFilterItem;
						ignoreOnSelectedItemChanged = false;
						return;
					}


					RegionHelper.CloseLeftNavigationPopUp();
					CurrentFilterItem = SelectedItem;
					if (CurrentFilterItem.FilterItemType == FilterItem.FilterItemTypeEnum.ChargeOuts)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.ChargeoutsListView, null);
					}
					else if (CurrentFilterItem.FilterItemType == FilterItem.FilterItemTypeEnum.Payments)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.PaychargesListView, null);
					}
					else if (CurrentFilterItem.FilterItemType == FilterItem.FilterItemTypeEnum.Refunds)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.RefchargesListView, null);
					}
					else if (CurrentFilterItem.FilterItemType == FilterItem.FilterItemTypeEnum.EmailSendList)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.EmailChargesListView, null);
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
			public enum FilterItemTypeEnum { ChargeOuts, Payments, Refunds, EmailSendList }

			public static bool IsEqual(FilterItem item1, FilterItem item2)
			{
				return (item1 != null && item2 != null && item1.FilterItemType == item2.FilterItemType);
			}
		}
	}
}
