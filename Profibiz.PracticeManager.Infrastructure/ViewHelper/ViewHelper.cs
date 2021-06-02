using Autofac;
using Autofac.Core;
using DevExpress.Mvvm;
using Microsoft.Practices.ServiceLocation;
using Prism;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;


namespace Profibiz.PracticeManager.Infrastructure
{
	public static class ViewHelper
	{
		public static void RegisterView<T>(this IContainer _container, object key = null)
		{
			if (key == null)
			{
				key = typeof(T).Name;
			}

			var builder = new ContainerBuilder();
			builder.RegisterType<T>().Keyed<object>(key.ToString());
			builder.Update(_container);
		}


	}


	public enum ViewCodes
	{
		PatientsView,
		OnePatientView,
		InsuranceCoverageWindowView,
		InsuranceCoverage2WindowView,
		RibbonPatientsView,
		SpecialistsView,
		RibbonSpecialistsView,
		OneSpecialistView,
		PickPatientView,
		LookupsEditView,
		InsuranceProvidersView,
		OneInsuranceProviderView,
		MedicalServicesView,
		CategoriesView,
		OneMedicalServiceView,
		OneCategoryView,
		ProfessionalAssociationsView,
		SettingsView,
		ThirdPartyServiceProvidersView,
		AppointmentBooksView,
		OneProfessionalAssociationView,
		OneSettingView,
		AppointmentsSchedulerView,
		OneAppointmentView,
		InvoiceClaimDetailsView,
		InsuranceArticleInfoView,
		OneCalendarEventView,
		OneAppointmentBookView,
		AppointmentsSchedulerPanelView,
		CalendarEventsSchedulerView,
		InvoicesListView,
		InventoryListView,
		InventoryBalanceView,
		PickMedicalServicesOrSuppliesView,
		PickChargeoutRecipientsView,
		PickPaymentView,
		InvoicesBuilderView,
		PaymentOneView,
		PaymentsListView,
		RefundsListView,
		PickInvoiceView,
        InvoiceWindowView,
        PaymentWindowView,
		RefundWindowView,
		PickAppointmentView,
		InvoicePrintView,
		OneThirdPartyServiceProviderView,
		OneAppointmentStatusView,
		AppointmentStatusesView,
		CalendarEventStatusesView,
		ReferrersView,
		UsersView,
		SuppliersView,
		OneReferrerView,
		OneUserView,
		PatientBuildFamilyView,
		OneInvoiceStatusView,
		OneChargeoutStatusView,
		OneChargeoutRecipientView,
		InvoiceStatusesView,
		ChargeoutStatusesView,
		ChargeoutRecipientesView,
		PickInsuranceCoverageView,
		PickCategoryView,
		PickTemplateNameView,
		PickAppointmentMultiDateView,
		PayrollAllDoctorsView,
		PayrollDetailWindowView,
		PayrollPaymentOneWindowView,
		PayrollPaymentListWindowView,
		PayrollPaymentListView,
		PayrollPaymentListXWindowView,
		OneCalendarEventStatusView,
		CalendarEventsRemindersView,
		OnePublicHolidayView,
		PublicHolidaysView,
		OneSupplierView,
		OneOrderView,
		OneWorkInoutView,
		OnePatientNoteView,
		OnePatientNoteStatusView,
		PatientNoteStatusesView,
		SchedulerRecordView,
		SupplierPaymentsListView,
		SupplierRefundWindowView,
		SupplierPaymentWindowView,
		PickOrderView,
		PickSupplierPaymentView,
		PickSupplierView,
		OrdersListView,
		WorkInoutsListView,
		SupplierRefundsListView,
		EmailSendRunView,
		EmailSendsListView,
		AppointmentClinicalNoteView,
		AppointmentTreatmentNoteView,
		FormDocumentView,
		FormDocmodelView,
		InsuranceArticleSummaryView,
		PatientDocumentView,
		ChargeoutsListView,
		ChargeoutWindowView,
		PaychargeWindowView,
		PickPaychargeView,
		RefchargeWindowView,
		PickChargeoutView,
		ChargeoutPrintView,
		EmailChargeRunView,
		EmailChargesListView,
		PaychargesListView,
		RefchargesListView,
		PickPaychargeAppointmentsView,
	}
}
