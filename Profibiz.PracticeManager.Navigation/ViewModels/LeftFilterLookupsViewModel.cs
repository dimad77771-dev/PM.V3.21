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


namespace Profibiz.PracticeManager.Navigation.ViewModels
{
	[ImplementPropertyChanged]
	public class LeftFilterLookupsViewModel : ILeftPanelViewModel
	{
		public LeftFilterLookupsViewModel()
		{
			BuildAllFilters();
		}


		public void Init()
		{
			SelectedItem = InvoiceStatusesFilter;
		}


		void BuildAllFilters()
		{
			AllCategoriesItems.Add(InsuranceProvidersFilter);
			AllCategoriesItems.Add(MedicalServicesOrSuppliesFilter);
			AllCategoriesItems.Add(CategoriesFilter);
			AllCategoriesItems.Add(ProfessionalAssociationsFilter);
			AllCategoriesItems.Add(AppointmentBooksFilter);
			AllCategoriesItems.Add(AppointmentStatusesFilter);
			AllCategoriesItems.Add(CalendarEventStatusesFilter);
			AllCategoriesItems.Add(PatientNoteStatusesFilter);
			AllCategoriesItems.Add(InvoiceStatusesFilter);
			AllCategoriesItems.Add(ChargeoutStatusesFilter);
			AllCategoriesItems.Add(ChargeoutRecipientesFilter);
			AllCategoriesItems.Add(ThirdPartyServiceProvidersFilter);
			AllCategoriesItems.Add(ReferrersFilter);
			AllCategoriesItems.Add(UsersFilter);
			AllCategoriesItems.Add(SuppliersFilter);
			AllCategoriesItems.Add(PublicHolidaysFilter);
			StaticFilterItems = AllCategoriesItems.ToObservableCollection();
		}



		public ObservableCollection<FilterItem> AllCategoriesItems { get; set; } = new ObservableCollection<FilterItem>();
		public FilterItem InsuranceProvidersFilter { get; set; } = new FilterItem { Name = "Insurance Providers", LookupType = FilterItem.LookupTypeEnum.InsuranceProviders };
		public FilterItem MedicalServicesOrSuppliesFilter { get; set; } = new FilterItem { Name = "Medical Services/Supplies", LookupType = FilterItem.LookupTypeEnum.MedicalServices };
		public FilterItem CategoriesFilter { get; set; } = new FilterItem { Name = "Categories", LookupType = FilterItem.LookupTypeEnum.Categories };
		public FilterItem ProfessionalAssociationsFilter { get; set; } = new FilterItem { Name = "Professional Associations", LookupType = FilterItem.LookupTypeEnum.ProfessionalAssociations };
		public FilterItem ThirdPartyServiceProvidersFilter { get; set; } = new FilterItem { Name = "Service Providers (TP)", LookupType = FilterItem.LookupTypeEnum.ThirdPartyServiceProviders };
		public FilterItem ReferrersFilter { get; set; } = new FilterItem { Name = "Referrers", LookupType = FilterItem.LookupTypeEnum.Referrers };
		public FilterItem UsersFilter { get; set; } = new FilterItem { Name = "Users", LookupType = FilterItem.LookupTypeEnum.Users };
		public FilterItem SuppliersFilter { get; set; } = new FilterItem { Name = "Suppliers", LookupType = FilterItem.LookupTypeEnum.Suppliers };
		public FilterItem AppointmentBooksFilter { get; set; } = new FilterItem { Name = "Appointment Books", LookupType = FilterItem.LookupTypeEnum.AppointmentBooks };
		public FilterItem AppointmentStatusesFilter { get; set; } = new FilterItem { Name = "Appointment Statuses", LookupType = FilterItem.LookupTypeEnum.AppointmentStatusesFilter };
		public FilterItem CalendarEventStatusesFilter { get; set; } = new FilterItem { Name = "Calendar Event Statuses", LookupType = FilterItem.LookupTypeEnum.CalendarEventStatusesFilter };
		public FilterItem PatientNoteStatusesFilter { get; set; } = new FilterItem { Name = "Patient Note Statuses", LookupType = FilterItem.LookupTypeEnum.PatientNoteStatusesFilter };
		public FilterItem PublicHolidaysFilter { get; set; } = new FilterItem { Name = "Public Holidays", LookupType = FilterItem.LookupTypeEnum.PublicHolidaysFilter };
		public FilterItem InvoiceStatusesFilter { get; set; } = new FilterItem { Name = "Invoice Statuses", LookupType = FilterItem.LookupTypeEnum.InvoiceStatusesFilter };
		public FilterItem ChargeoutStatusesFilter { get; set; } = new FilterItem { Name = "Outgoing Invoice Statuses", LookupType = FilterItem.LookupTypeEnum.ChargeoutStatusesFilter };
		public FilterItem ChargeoutRecipientesFilter { get; set; } = new FilterItem { Name = "Outgoing Invoice Recipients", LookupType = FilterItem.LookupTypeEnum.ChargeoutRecipientesFilter };
		public FilterItem SelectedItem { get; set; }
		public ObservableCollection<FilterItem> StaticFilterItems { get; set; }

		FilterItem CurrentFilterItem;
		public void OnSelectedItemChanged()
		{
			DispatcherUIHelper.Run(() =>
			{
				if (!FilterItem.IsEqual(CurrentFilterItem, SelectedItem))
				{
					RegionHelper.CloseLeftNavigationPopUp();
					CurrentFilterItem = SelectedItem;

					var lookupType = CurrentFilterItem?.LookupType;
					if (lookupType == FilterItem.LookupTypeEnum.InsuranceProviders)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.InsuranceProvidersView, null);
					}
					else if (lookupType == FilterItem.LookupTypeEnum.MedicalServices)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.MedicalServicesView, null);
					}
					else if (lookupType == FilterItem.LookupTypeEnum.Categories)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.CategoriesView, null);
					}
					else if (lookupType == FilterItem.LookupTypeEnum.ProfessionalAssociations)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.ProfessionalAssociationsView, null);
					}
					else if (lookupType == FilterItem.LookupTypeEnum.ThirdPartyServiceProviders)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.ThirdPartyServiceProvidersView, null);
					}
					else if (lookupType == FilterItem.LookupTypeEnum.Referrers)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.ReferrersView, null);
					}
					else if (lookupType == FilterItem.LookupTypeEnum.Users)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.UsersView, null);
					}
					else if (lookupType == FilterItem.LookupTypeEnum.Suppliers)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.SuppliersView, null);
					}
					else if (lookupType == FilterItem.LookupTypeEnum.AppointmentBooks)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.AppointmentBooksView, null);
					}
					else if (lookupType == FilterItem.LookupTypeEnum.AppointmentStatusesFilter)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.AppointmentStatusesView, null);
					}
					else if (lookupType == FilterItem.LookupTypeEnum.PatientNoteStatusesFilter)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.PatientNoteStatusesView, null);
					}
					else if (lookupType == FilterItem.LookupTypeEnum.CalendarEventStatusesFilter)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.CalendarEventStatusesView, null);
					}
					else if (lookupType == FilterItem.LookupTypeEnum.PublicHolidaysFilter)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.PublicHolidaysView, null);
					}
					else if (lookupType == FilterItem.LookupTypeEnum.InvoiceStatusesFilter)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.InvoiceStatusesView, null);
					}
					else if (lookupType == FilterItem.LookupTypeEnum.ChargeoutStatusesFilter)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.ChargeoutStatusesView, null);
					}
					else if (lookupType == FilterItem.LookupTypeEnum.ChargeoutRecipientesFilter)
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.ChargeoutRecipientesView, null);
					}
				}
			});
		}


		[ImplementPropertyChanged]
		public class FilterItem : IFilterItem
		{
			public virtual string Name { get; set; }
			public virtual LookupTypeEnum LookupType { get; set; }

			public virtual string DisplayText
			{
				get
				{
					return Name;
				}
			}
			public enum LookupTypeEnum
			{
				InsuranceProviders, MedicalServices, Categories, ProfessionalAssociations, ThirdPartyServiceProviders,
				AppointmentBooks, AppointmentStatusesFilter, PatientNoteStatusesFilter, CalendarEventStatusesFilter, PublicHolidaysFilter,
				InvoiceStatusesFilter, ChargeoutStatusesFilter, ChargeoutRecipientesFilter,
				Referrers, Suppliers, Users,
			}

			public static bool IsEqual(FilterItem item1, FilterItem item2)
			{
				return (item1 != null && item2 != null && item1.LookupType == item2.LookupType);
			}
		}
	}
}
