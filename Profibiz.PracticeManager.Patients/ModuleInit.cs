using Autofac;
using Profibiz.PracticeManager.Patients.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Profibiz.PracticeManager.Infrastructure;
using AutoMapper;
using Profibiz.PracticeManager.Model;
using Profibiz.PracticeManager.Patients.ViewModels;

namespace Profibiz.PracticeManager.Patients
{
    public class ModuleInit : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IContainer _container;

        public ModuleInit(IRegionManager regionManager, IContainer container) 
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
			var builder = new ContainerBuilder();
			builder.Update(_container);
			_container.RegisterView<OnePatientView>();
			_container.RegisterView<PatientsView>();
			_container.RegisterView<RibbonPatientsView>();
			_container.RegisterView<LeftFilterPatientsView>();
			_container.RegisterView<LeftFilterFinancesView>();
			_container.RegisterView<LeftFilterFinancesView>();
			_container.RegisterView<LeftFilterInventoryView>();
			_container.RegisterView<LeftFilterChargeoutsView>();
			_container.RegisterView<InsuranceCoverageView>();
			_container.RegisterView<InsuranceCoverageWindowView>();
			_container.RegisterView<InsuranceCoverage2View>();
			_container.RegisterView<InsuranceCoverage2WindowView>();
			_container.RegisterView<LeftFilterSpecialistsView>();
			_container.RegisterView<SpecialistsView>();
			_container.RegisterView<RibbonSpecialistsView>();
			_container.RegisterView<OneSpecialistView>();
			_container.RegisterView<PickPatientView>();
			_container.RegisterView<AppointmentsSchedulerView>();
			_container.RegisterView<RibbonAppointmentsSchedulerView>();
			_container.RegisterView<OneAppointmentView>();
			_container.RegisterView<AppointmentsSchedulerPanelView>();
			_container.RegisterView<CalendarEventsSchedulerView>();
			_container.RegisterView<RibbonCalendarEventsSchedulerView>();
			_container.RegisterView<OneCalendarEventView>();
			_container.RegisterView<InvoicesListView>();
			_container.RegisterView<ChargeoutsListView>();
			_container.RegisterView<RibbonInvoicesListView>();
			_container.RegisterView<RibbonChargeoutsListView>();
			_container.RegisterView<PickMedicalServicesOrSuppliesView>();
			_container.RegisterView<PickChargeoutRecipientsView>();
			_container.RegisterView<PickPaymentView>();
			_container.RegisterView<PickSupplierPaymentView>();
			_container.RegisterView<InvoicesBuilderView>();
			_container.RegisterView<RibbonInvoicesBuilderView>();
			_container.RegisterView<PaymentOneView>();
			_container.RegisterView<SupplierPaymentOneView>();
			_container.RegisterView<RefundOneView>();
			_container.RegisterView<PaymentsListView>();
			_container.RegisterView<PaychargesListView>();
			_container.RegisterView<SupplierPaymentsListView>();
			_container.RegisterView<RefundsListView>();
			_container.RegisterView<RibbonPaymentsListView>();
			_container.RegisterView<RibbonPaychargesListView>();
			_container.RegisterView<RibbonSupplierPaymentsListView>();
			_container.RegisterView<RibbonRefundsListView>();
			_container.RegisterView<PickInvoiceView>();
            _container.RegisterView<InvoiceWindowView>();
            _container.RegisterView<PaymentWindowView>();
			_container.RegisterView<SupplierPaymentWindowView>();
			_container.RegisterView<RefundWindowView>();
			_container.RegisterView<PickAppointmentView>();
			_container.RegisterView<InvoicePrintView>();
			_container.RegisterView<ChargeoutPrintView>();
			_container.RegisterView<PatientBuildFamilyView>();
			_container.RegisterView<PickInsuranceCoverageView>();
			_container.RegisterView<PickCategoryView>();
			_container.RegisterView<PickTemplateNameView>();
			_container.RegisterView<PickAppointmentMultiDateView>();
			_container.RegisterView<PayrollAllDoctorsView>();
			_container.RegisterView<RibbonPayrollAllDoctorsView>();
			_container.RegisterView<PayrollDetailView>();
			_container.RegisterView<PayrollDetailWindowView>();
			_container.RegisterView<PayrollPaymentOneView>();
			_container.RegisterView<PayrollPaymentOneWindowView>();
			_container.RegisterView<PayrollPaymentListView>();
			_container.RegisterView<PayrollPaymentListWindowView>();
			_container.RegisterView<RibbonPayrollPaymentListView>();
			_container.RegisterView<PayrollPaymentListXWindowView>();
			_container.RegisterView<CalendarEventsRemindersView>();
			_container.RegisterView<CalendarEventsRemindersService>();
			_container.RegisterView<InventoryListView>();
			_container.RegisterView<RibbonInventoryListView>();
			_container.RegisterView<OneOrderView>();
			_container.RegisterView<InventoryBalanceView>();
			_container.RegisterView<RibbonInventoryBalanceView>();
			_container.RegisterView<OnePatientNoteView>();
			_container.RegisterView<SchedulerRecordView>();
			_container.RegisterView<PickSupplierView>();
			_container.RegisterView<OrdersListView>();
			_container.RegisterView<RibbonOrdersView>();
			_container.RegisterView<PickOrderView>();
			_container.RegisterView<SupplierRefundsListView>();
			_container.RegisterView<RibbonSupplierRefundsListView>();
			_container.RegisterView<SupplierRefundWindowView>();
			_container.RegisterView<EmailSendRunView>();
			_container.RegisterView<EmailChargeRunView>();
			_container.RegisterView<EmailSendsListView>();
			_container.RegisterView<EmailChargesListView>();
			_container.RegisterView<RibbonEmailSendsView>();
			_container.RegisterView<RibbonEmailChargesView>();
			_container.RegisterView<AppointmentClinicalNoteView>();
			_container.RegisterView<FormDocumentView>();
			_container.RegisterView<AppointmentTreatmentNoteView>();
			_container.RegisterView<InvoiceClaimDetailsView>();
			_container.RegisterView<InsuranceArticleInfoView>();
			_container.RegisterView<RibbonInsuranceArticleSummaryView>();
			_container.RegisterView<InsuranceArticleSummaryView>();
			_container.RegisterView<PatientDocumentView>();
			_container.RegisterView<ChargeoutWindowView>();
			_container.RegisterView<PickChargeoutView>();
			_container.RegisterView<RefchargeWindowView>();
			_container.RegisterView<PaychargeWindowView>();
			_container.RegisterView<RefchargesListView>();
			_container.RegisterView<RibbonRefchargesListView>();
			_container.RegisterView<PickPaychargeView>();
			_container.RegisterView<PickPaychargeAppointmentsView>();

			AutoMapperInit();
		}


		void AutoMapperInit()
		{
			Mapper.Initialize(cfg => cfg.CreateMap<PatientsListView, Patient>());
		}
    }
}