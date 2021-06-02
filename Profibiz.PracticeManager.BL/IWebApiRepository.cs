using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profibiz.PracticeManager.DTO;
using System.Data.Entity;
using Profibiz.PracticeManager.SharedCode;
using System.Web.Http.Controllers;

namespace Profibiz.PracticeManager.BL
{
    public interface IWebApiRepository
    {
		Guid? CurrentUserRowId { get; set; }
		void SetCurrentUserRowId(HttpControllerContext controllerContext);

		IEnumerable<PatientsListView> GetPatientsList(Guid? insuranceProviderRowId, Guid? insuranceProvidersViewGroupRowId, bool hasNoInsuranceProvider, bool includeAllFamilyMember);
		Guid[] PatientRowId2InsuranceProviders(Guid patientRowId);
		List<InsuranceCoverage> PatientRowId2InsuranceCoverages(Guid patientRowId);
		Patient GetPatient(Guid id, bool isShortForm, bool isAddressOnly);
		byte[] GetPatientPhoto(Guid id);
		void UpdatePatientCore(Patient entity, EntityState state);
		void PatientBuildFamily(List<Patient> patients);

		InsuranceCoverage GetInsuranceCoverage(Guid id);
		void UpdateInsuranceCoverageCore(InsuranceCoverage entity, EntityState state);
		ServerReturnCloneInsuranceCoverage CloneInsuranceCoverage(InsuranceCoverage id);
		void BatchCloneInsuranceCoverage();
		InsuranceCoverageInvoiceClaimsInfo[] GetInsuranceCoverageInvoiceClaimsInfo(String insuranceCoverageRowIds);
		InsurancePatientCategoryInfo GetInsurancePatientCategoryInfo(String insuranceCoverageRowIdsStr);
		InsuranceArticleInfo GetInsuranceArticleInfo(Guid insuranceCoverageRowId, Guid patientRowId, Guid categoryRowId, Boolean isShowAllYears, Boolean showProblemOnly);
		InsuranceArticleSummary GetInsuranceArticleSummary(string categoriesRowIds);



		IEnumerable<ServiceProvider> GetServiceProviderList(Guid? rowId, Guid? professionalAssociationRowId);
		void UpdateServiceProviderCore(ServiceProvider entity, EntityState state);

        IEnumerable<String> GetOntarioCities();
        IEnumerable<Category> GetCategories();
		IEnumerable<AppointmentStatus> GetAppointmentStatuses();
		IEnumerable<PatientNoteStatus> GetPatientNoteStatuses();
		IEnumerable<CalendarEventStatus> GetCalendarEventStatuses();
		IEnumerable<PublicHoliday> GetPublicHolidays();
		IEnumerable<InvoiceStatus> GetInvoiceStatuses();
		IEnumerable<ChargeoutStatus> GetChargeoutStatuses();
		IEnumerable<ChargeoutRecipient> GetChargeoutRecipientes();
		IEnumerable<Template> GetTemplates();
		IEnumerable<InsuranceProvider> GetInsuranceProviders();
		IEnumerable<MedicalServicesOrSupply> GetMedicalServicesOrSupplies();
		IEnumerable<ProfessionalAssociation> GetProfessionalAssociations();
		IEnumerable<ThirdPartyServiceProvider> GetThirdPartyServiceProviders();
		IEnumerable<Referrer> GetReferrers();
		IEnumerable<User> GetUsers();
		IEnumerable<Supplier> GetSuppliers();
		IEnumerable<AppointmentBook> GetAppointmentBooks();
		UserSetting GetUserSettings(string userCode);
		void PutMedicalServicesOrSupplies(IEnumerable<MedicalServicesOrSupply> entities);
		void DeleteMedicalServicesOrSupply(Guid id);
		void PutCategories(IEnumerable<Category> entities);
		void DeleteCategory(Guid id);
		void PutInsuranceProviders(IEnumerable<InsuranceProvider> entities);
		void DeleteInsuranceProvider(Guid id);
		void PutProfessionalAssociations(IEnumerable<ProfessionalAssociation> entities);
		void DeleteProfessionalAssociation(Guid id);
		void PutThirdPartyServiceProviders(IEnumerable<ThirdPartyServiceProvider> entities);
		void DeleteThirdPartyServiceProvider(Guid id);
		void PutReferrers(IEnumerable<Referrer> entities);
		void PutUsers(IEnumerable<User> entities);
		void DeleteReferrer(Guid id);
		void DeleteUser(Guid id);
		void PutSuppliers(IEnumerable<Supplier> entities);
		void DeleteSupplier(Guid id);
		void PutAppointmentBooks(IEnumerable<AppointmentBook> entities);
		void DeleteAppointmentBook(Guid id);
		void PutAppointmentStatuses(IEnumerable<AppointmentStatus> entities);
		void DeleteAppointmentStatus(Guid id);
		void PutPatientNoteStatuses(IEnumerable<PatientNoteStatus> entities);
		void DeletePatientNoteStatus(Guid id);
		void PutCalendarEventStatuses(IEnumerable<CalendarEventStatus> entities);
		void DeleteCalendarEventStatus(Guid id);
		void PutPublicHolidays(IEnumerable<PublicHoliday> entities);
		void DeletePublicHoliday(Guid id);
		void PutInvoiceStatuses(IEnumerable<InvoiceStatus> entities);
		void DeleteInvoiceStatus(Guid id);
		void PutChargeoutStatuses(IEnumerable<ChargeoutStatus> entities);
		void DeleteChargeoutStatus(Guid id);
		void PutChargeoutRecipientes(IEnumerable<ChargeoutRecipient> entities);
		void DeleteChargeoutRecipient(Guid id);


		IEnumerable<InsuranceProvidersViewGroup> GetInsuranceProvidersViewGroups();
		void PutInsuranceProvidersViewGroups(IEnumerable<InsuranceProvidersViewGroup> entities);


		
		IEnumerable<Appointment> GetAppointmentList(Guid? appointmentBookRowId, Guid? patientRowId, Guid? insuranceProvidersViewGroupRowId, Guid? rowId, DateTime? startFrom, DateTime? startTo, Boolean? completed, Boolean? inInvoice, Boolean? forChargeout, Boolean? calcAppointmentPaid, string rowIds);
		void UpdateAppointmentCore(List<Appointment> entity, EntityState state);
		IEnumerable<AppointmentInsuranceProviderDayInfo> GetAppointmentInsuranceProviderDayInfo(DateTime dat, Guid serviceProviderRowId);

		IEnumerable<CalendarEvent> GetCalendarEventList(Guid? patientRowId, Guid? serviceProviderRowId, Guid? rowId, DateTime? startFrom, DateTime? startTo, Boolean? completed, string rowIds, bool? forRemainder, bool? isVacation);
		void UpdateCalendarEventCore(List<CalendarEvent> entity, EntityState state);

		IEnumerable<Invoice> GetInvoiceList(Guid? rowId, Guid? patientRowId, bool? useFamilyHead, int? noPaidOnly, bool flagNoPaidOrNoApprovedAmount, bool negativeBalanceOnly, DateTime? invoiceDateFrom, DateTime? invoiceDateTo, bool includeInvoiceClaims, bool IsShowSentOnly, bool IsShowPaidOnly, Guid? ReferrerRowId, Guid? InsuranceProviderRowId, DateTime? createdDateFrom, DateTime? createdDateTo, bool isCoordinationProblemOnly);
		Invoice GetInvoice(Guid id, bool includeAppointment);
		ServerReturnUpdateInvoice UpdateInvoiceCore(Invoice entity);
		ServerReturnUpdateInvoice DeleteInvoiceCore(List<Guid> entity);

		IEnumerable<Payment> GetPaymentList(Guid? rowId, Guid? patientRowId, int? hasNoDistributedAmount, DateTime? paymentDateFrom, DateTime? paymentDateTo);
		Payment GetPayment(Guid id);
		void UpdatePaymentCore(Payment entity, EntityState state);

		IEnumerable<Paycharge> GetPaychargeList(Guid? rowId, Guid? patientRowId, int? hasNoDistributedAmount, DateTime? paychargeDateFrom, DateTime? paychargeDateTo);
		Paycharge GetPaycharge(Guid id);
		void UpdatePaychargeCore(Paycharge entity, EntityState state);


		IEnumerable<SupplierPayment> GetSupplierPaymentList(Guid? rowId, Guid? patientRowId, int? hasNoDistributedAmount, DateTime? paymentDateFrom, DateTime? paymentDateTo);
		SupplierPayment GetSupplierPayment(Guid id);
		void UpdateSupplierPaymentCore(SupplierPayment entity, EntityState state);


		IEnumerable<Refund> GetRefundList(Guid? rowId, Guid? patientRowId, int? hasNoDistributedAmount, DateTime? paymentDateFrom, DateTime? paymentDateTo);
		Refund GetRefund(Guid id);
		void UpdateRefundCore(Refund entity, EntityState state);

		IEnumerable<Refcharge> GetRefchargeList(Guid? rowId, Guid? chargeoutRecipientRowId, int? hasNoDistributedAmount, DateTime? paychargeDateFrom, DateTime? paychargeDateTo);
		Refcharge GetRefcharge(Guid id);
		void UpdateRefchargeCore(Refcharge entity, EntityState state);


		IEnumerable<SupplierRefund> GetSupplierRefundList(Guid? rowId, Guid? patientRowId, int? hasNoDistributedAmount, DateTime? paymentDateFrom, DateTime? paymentDateTo);
		SupplierRefund GetSupplierRefund(Guid id);
		void UpdateSupplierRefundCore(SupplierRefund entity, EntityState state);


		IEnumerable<PayrollInfoResult> GetPayrollInfo(DateTime periodStart, DateTime periodFinish, Guid? serviceProviderRowId);
		IEnumerable<InvoicePaymentByDoctorAndPeriodView> GetInvoicePaymentByDoctorAndPeriod(Guid? serviceProviderRowId);
		IEnumerable<PayrollPaymentByDoctorAndPeriodView> GetPayrollPaymentByDoctorAndPeriod(Guid? serviceProviderRowId);
		IEnumerable<InvoicePaymentByDoctors> GetPayrollDetail(DateTime periodStart, DateTime periodFinish, Guid serviceProviderRowId);
		IEnumerable<PayrollPayment> GetPayrollPaymentList(Guid? serviceProviderRowId, DateTime? paymentDateFrom, DateTime? paymentDateTo);
		PayrollPayment GetPayrollPayment(Guid id);
		void UpdatePayrollPaymentCore(PayrollPayment entity, EntityState state);

		IEnumerable<Inventory> GetInventoryList(Guid? rowId, Guid? orderRowId, Guid? invoiceRowId, DateTime? transactionDateFrom, DateTime? transactionDateTo);
		IEnumerable<InventoryBalance> GetInventoryBalanceList(Guid? rowId);
		Order GetOrder(Guid id);
		IEnumerable<Order> GetOrderList(Guid? rowId, Guid? supplierRowId, DateTime? orderDateFrom, DateTime? orderDateTo, Boolean? noPaidOnly);
		void UpdateOrderCore(Order entity, EntityState state);
		void PostInventoryBalances(List<InventoryBalance> rows);
		void UpdateInventoryCore(Inventory entity, EntityState state);


		void PostErrorToServer(ClientError errorInfo);
		void PostNLogItem(NLogItem nlogItem);
		void PostUserSettings(UserSetting userSetting);

		IEnumerable<MedicalHistoryRecord> GetMedicalHistoryRecordList(Guid? rowId, Guid? patientRowId);
		void UpdateMedicalHistoryRecordCore(MedicalHistoryRecord entity, EntityState state);

		IEnumerable<TreatmentPlanRecord> GetTreatmentPlanRecordList(Guid? rowId, Guid? patientRowId);
		void UpdateTreatmentPlanRecordCore(TreatmentPlanRecord entity, EntityState state);

		PrintdocInfo GetPrintdocInfo(Guid invoiceRowId);
		PrintDocument[] GetPrintDocuments();

		IEnumerable<PatientNote> GetPatientNoteList(Guid? rowId, Guid? patientRowId);
		void UpdatePatientNoteCore(PatientNote entity, EntityState state);

		IEnumerable<PatientDocument> GetPatientDocumentList(Guid? rowId, Guid? patientRowId);
		void UpdatePatientDocumentCore(PatientDocument entity, EntityState state);

		IEnumerable<SchedulerRecord> GetSchedulerRecords(Guid serviceProviderRowId);
		void PutSchedulerRecords(List<SchedulerRecord> rows);
		IEnumerable<CalculateAppointmentStartFinishResult> CalculateAppointmentStartFinish(Guid serviceProviderRowId, DateTime[] dates);


		void SendEmail(EmailSend parm);
		IEnumerable<EmailSend> GetEmailSendList(DateTime? sendDateFrom, DateTime? sendDateTo);
		IEnumerable<EmailSendAttachment> GetEmailSendAttachmentList(Guid emailSendRowId);

		void SendChargeEmail(EmailCharge parm);
		IEnumerable<EmailCharge> GetEmailChargeList(DateTime? sendDateFrom, DateTime? sendDateTo);
		IEnumerable<EmailChargeAttachment> GetEmailChargeAttachmentList(Guid emailSendRowId);

		IEnumerable<AppointmentClinicalNote> GetAppointmentClinicalNoteList(Guid? rowId, Guid? patientRowId);
		void UpdateAppointmentClinicalNoteCore(AppointmentClinicalNote entity, EntityState state);

		IEnumerable<FormDocument> GetFormDocumentList(Guid? rowId);
		void UpdateFormDocumentCore(FormDocument entity, EntityState state);

		IEnumerable<FormDocmodel> GetFormDocmodelList(int? formDictionary, Guid? patientRowId, Guid? formRowId);
		void UpdateFormDocmodelCore(FormDocmodel entity, EntityState state);

		IEnumerable<AppointmentTreatmentNote> GetAppointmentTreatmentNoteList(Guid? rowId, Guid? patientRowId);
		void UpdateAppointmentTreatmentNoteCore(AppointmentTreatmentNote[] entity, EntityState state);

		WorkInout GetWorkInout(Guid id);
		IEnumerable<WorkInout> GetWorkInoutList(Guid? rowId, DateTime? workInoutDateFrom, DateTime? workInoutDateTo);
		void UpdateWorkInoutCore(WorkInout entity, EntityState state);

		IEnumerable<Chargeout> GetChargeoutList(Guid? rowId, Guid? chargeoutRecipientRowId, int? noPaidOnly, bool flagNoPaidOrNoApprovedAmount, bool negativeBalanceOnly, DateTime? chargeoutDateFrom, DateTime? chargeoutDateTo, bool isShowSentOnly, bool isShowPaidOnly, DateTime? createdDateFrom, DateTime? createdDateTo);
		Chargeout GetChargeout(Guid id);
		ServerReturnUpdateChargeout UpdateChargeoutCore(Chargeout entity);
		ServerReturnUpdateChargeout DeleteChargeoutCore(List<Guid> entity);

		LoginInfo GetLoginInfo(string name, string password);
	}
}
