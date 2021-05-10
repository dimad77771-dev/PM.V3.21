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
	public class LeftFilterFinancesViewModel : ILeftPanelViewModel
	{
		public LeftFilterFinancesViewModel()
		{
			BuildAllFilters();
		}

		

		void BuildAllFilters()
		{
			AllCategoriesItems.Add(InvoicesFilter);
			AllCategoriesItems.Add(PaymentsFilter);
			AllCategoriesItems.Add(RefundsFilter);
			AllCategoriesItems.Add(InvoiceBuilderFilter);
			AllCategoriesItems.Add(PayrollAllDoctorsFilter);
			AllCategoriesItems.Add(PayrollPaymentListFilter);
			AllCategoriesItems.Add(EmailSendListFilter);
			AllCategoriesItems.Add(InsuranceArticleSummaryFilter);
			StaticFilterItems = AllCategoriesItems.ToObservableCollection();
		}

		public void Init()
		{
			SelectedItem = InvoicesFilter;
			if (RuntimeHelper.IsMachineD)
			{
				//SelectedItem = InvoicesFilter;
				SelectedItem = PaymentsFilter;
				//SelectedItem = RefundsFilter;
				//SelectedItem = InvoiceBuilderFilter;
				//SelectedItem = PayrollAllDoctorsFilter;
				//SelectedItem = PayrollPaymentListFilter;
				//SelectedItem = EmailSendListFilter;
				//SelectedItem = InsuranceArticleSummaryFilter;
			}
		}




		public ObservableCollection<FilterItem> AllCategoriesItems { get; set; } = new ObservableCollection<FilterItem>();
		public FilterItem InvoicesFilter { get; set; } = new FilterItem { Name = "Invoices", FilterItemType = FilterItem.FilterItemTypeEnum.Invoices };
		public FilterItem InvoiceBuilderFilter { get; set; } = new FilterItem { Name = "Invoice Builder", FilterItemType = FilterItem.FilterItemTypeEnum.InvoiceBuilder };
		public FilterItem PaymentsFilter { get; set; } = new FilterItem { Name = "Payments", FilterItemType = FilterItem.FilterItemTypeEnum.Payments };
		public FilterItem RefundsFilter { get; set; } = new FilterItem { Name = "Refunds", FilterItemType = FilterItem.FilterItemTypeEnum.Refunds };
		public FilterItem PayrollAllDoctorsFilter { get; set; } = new FilterItem { Name = "Payrolls", FilterItemType = FilterItem.FilterItemTypeEnum.PayrollAllDoctors };
		public FilterItem PayrollPaymentListFilter { get; set; } = new FilterItem { Name = "Payrolls Payments", FilterItemType = FilterItem.FilterItemTypeEnum.PayrollPaymentList };
		public FilterItem EmailSendListFilter { get; set; } = new FilterItem { Name = "Send Emails", FilterItemType = FilterItem.FilterItemTypeEnum.EmailSendList };
		public FilterItem InsuranceArticleSummaryFilter { get; set; } = new FilterItem { Name = "Insurance Coverages Summary", FilterItemType = FilterItem.FilterItemTypeEnum.InsuranceArticleSummary };
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
					if (CurrentFilterItem.FilterItemType == FilterItem.FilterItemTypeEnum.Invoices)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.InvoicesListView, null);
					}
					else if (CurrentFilterItem.FilterItemType == FilterItem.FilterItemTypeEnum.InvoiceBuilder)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.InvoicesBuilderView, null);
					}
					else if (CurrentFilterItem.FilterItemType == FilterItem.FilterItemTypeEnum.Payments)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.PaymentsListView, null);
					}
					else if (CurrentFilterItem.FilterItemType == FilterItem.FilterItemTypeEnum.Refunds)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.RefundsListView, null);
					}
					else if (CurrentFilterItem.FilterItemType == FilterItem.FilterItemTypeEnum.PayrollAllDoctors)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.PayrollAllDoctorsView, null);
					}
					else if (CurrentFilterItem.FilterItemType == FilterItem.FilterItemTypeEnum.PayrollPaymentList)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.PayrollPaymentListXWindowView, null);
					}
					else if (CurrentFilterItem.FilterItemType == FilterItem.FilterItemTypeEnum.EmailSendList)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.EmailSendsListView, null);
					}
					else if (CurrentFilterItem.FilterItemType == FilterItem.FilterItemTypeEnum.InsuranceArticleSummary)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.InsuranceArticleSummaryView, null);
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
			public enum FilterItemTypeEnum { Invoices, InvoiceBuilder, Payments, Refunds, PayrollAllDoctors, PayrollPaymentList, EmailSendList, InsuranceArticleSummary }

			public static bool IsEqual(FilterItem item1, FilterItem item2)
			{
				return (item1 != null && item2 != null && item1.FilterItemType == item2.FilterItemType);
			}
		}
	}
}
