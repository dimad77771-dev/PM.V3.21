using Autofac;
using Prism.Modularity;
using Prism.Regions;
using Profibiz.PracticeManager.Navigation.Views;
using Profibiz.PracticeManager.Infrastructure;
using Profibiz.PracticeManager.Navigation.ViewModels;

namespace Profibiz.PracticeManager.Navigation
{
    public class NavigationModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IContainer _container;

        public NavigationModule(IRegionManager regionManager, IContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
			var builder = new ContainerBuilder();
			builder.RegisterType<LeftNavigationPanel>().SingleInstance();
			builder.RegisterType<BottomNavigationPanel>().SingleInstance();
			builder.RegisterType<InsuranceProvidersViewGroupsViewModel>();
			builder.Update(_container);
			var view2 = _container.Resolve<LeftNavigationPanel>();
			var view3 = _container.Resolve<BottomNavigationPanel>();
			_regionManager.Regions["LeftNavigationPanelRegion"].Add(view2);
			_regionManager.Regions["BottomNavigationPanelRegion"].Add(view3);

			_container.RegisterView<LookupsEditView>();
			_container.RegisterView<LeftFilterLookupsView>();
			_container.RegisterView<InsuranceProvidersView>();
			_container.RegisterView<MedicalServicesView>();
			_container.RegisterView<CategoriesView>();
			_container.RegisterView<OneInsuranceProviderView>();
			_container.RegisterView<OneMedicalServiceView>();
			_container.RegisterView<OneCategoryView>();
			_container.RegisterView<OneProfessionalAssociationView>();
			_container.RegisterView<OneSettingView>();
			_container.RegisterView<OneThirdPartyServiceProviderView>();
			_container.RegisterView<OneAppointmentBookView>();
			_container.RegisterView<ProfessionalAssociationsView>();
			_container.RegisterView<SettingsView>();
			_container.RegisterView<ThirdPartyServiceProvidersView>();
			_container.RegisterView<AppointmentBooksView>();
			_container.RegisterView<RibbonInsuranceProvidersView>();
			_container.RegisterView<RibbonMedicalServicesView>();
			_container.RegisterView<RibbonCategoriesView>();
			_container.RegisterView<RibbonProfessionalAssociationsView>();
			_container.RegisterView<RibbonSettingsView>();
			_container.RegisterView<RibbonThirdPartyServiceProvidersView>();
			_container.RegisterView<RibbonAppointmentBooksView>();
			_container.RegisterView<OneAppointmentStatusView>();
			_container.RegisterView<AppointmentStatusesView>();
			_container.RegisterView<RibbonAppointmentStatusesView>();
			_container.RegisterView<OneReferrerView>();
			_container.RegisterView<ReferrersView>();
			_container.RegisterView<RibbonReferrersView>();
			_container.RegisterView<OneUserView>();
			_container.RegisterView<LoginView>();
			_container.RegisterView<UsersView>();
			_container.RegisterView<RibbonUsersView>();
			_container.RegisterView<OneSupplierView>();
			_container.RegisterView<SuppliersView>();
			_container.RegisterView<RibbonSuppliersView>();
			_container.RegisterView<OneInvoiceStatusView>();
			_container.RegisterView<OneChargeoutStatusView>();
			_container.RegisterView<OneChargeoutRecipientView>();
			_container.RegisterView<InvoiceStatusesView>();
			_container.RegisterView<ChargeoutStatusesView>();
			_container.RegisterView<ChargeoutRecipientesView>();
			_container.RegisterView<RibbonInvoiceStatusesView>();
			_container.RegisterView<RibbonChargeoutStatusesView>();
			_container.RegisterView<RibbonChargeoutRecipientesView>();
			_container.RegisterView<CalendarEventStatusesView>();
			_container.RegisterView<RibbonCalendarEventStatusesView>();
			_container.RegisterView<OneCalendarEventStatusView>();
			_container.RegisterView<PublicHolidaysView>();
			_container.RegisterView<RibbonPublicHolidaysView>();
			_container.RegisterView<OnePublicHolidayView>();
			_container.RegisterView<OnePatientNoteStatusView>();
			_container.RegisterView<PatientNoteStatusesView>();
			_container.RegisterView<RibbonPatientNoteStatusesView>();
		}
	}
}
