using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profibiz.PracticeManager.Model;
using Profibiz.PracticeManager.SharedCode;

namespace Profibiz.PracticeManager.Patients.BusinessServiceInterface
{
    public interface IPatientsBusinessService
    {
		Task<List<PatientsListView>> GetPatientList(string query);
		Task<Patient> GetPatient(Guid rowId, bool isShortForm = false, bool isAddressOnly = false);
		Task<Guid[]> PatientRowId2InsuranceProviders(Guid patientRowId);
		Task<List<InsuranceCoverage>> PatientRowId2InsuranceCoverages(Guid patientRowId);
		Task<Byte[]> GetPatientPhoto(Guid rowId);
		Task<UpdateReturn> PutPatient(Patient patient);
		Task<UpdateReturn> PostPatient(Patient patient);
		Task<UpdateReturn> DeletePatient(Guid rowId);
		Task<UpdateReturn> PatientBuildFamily(List<Patient> patients);

		Task<InsuranceCoverage> GetInsuranceCoverage(Guid rowId);
		Task<UpdateReturn> PutInsuranceCoverage(InsuranceCoverage entity);
		Task<UpdateReturn> PostInsuranceCoverage(InsuranceCoverage entity);
		Task<UpdateReturn> DeleteInsuranceCoverage(Guid rowId);
		Task<UpdateReturn> CloneInsuranceCoverage(InsuranceCoverage entity);
		Task<InsuranceCoverageInvoiceClaimsInfo[]> GetInsuranceCoverageInvoiceClaimsInfo(String insuranceCoverageRowIdsStr);
		Task<InsurancePatientCategoryInfo> GetInsurancePatientCategoryInfo(String insuranceCoverageRowIds);
		Task<InsuranceArticleInfo> GetInsuranceArticleInfo(String qry);
		Task<InsuranceArticleSummary> GetInsuranceArticleSummary(String qry);

		Task<List<ServiceProvider>> GetServiceProviderList(string query);
		Task<UpdateReturn> PutServiceProvider(ServiceProvider entity);
		Task<UpdateReturn> PostServiceProvider(ServiceProvider entity);
		Task<UpdateReturn> DeleteServiceProvider(Guid rowId);

		Task<List<Appointment>> GetAppointmentList(Guid? appointmentBookRowId = null, Guid? patientRowId = null, Guid? insuranceProvidersViewGroupRowId = null, Guid? rowId = null, DateTime? startFrom = null, DateTime? startTo = null, Boolean? completed = null, Boolean? inInvoice = null, Boolean? inChargeout = null, Boolean? forChargeout = null, Guid[] rowIds = null);
		Task<UpdateReturn> PutAppointment(List<Appointment> entity);
		Task<UpdateReturn> PostAppointment(List<Appointment> entity);
		Task<UpdateReturn> DeleteAppointment(Guid rowId);
		Task<List<AppointmentInsuranceProviderDayInfo>> GetAppointmentInsuranceProviderDayInfo(string query);


		Task<List<CalendarEvent>> GetCalendarEventList(Guid? patientRowId = null, Guid? serviceProviderRowId = null, Guid? rowId = null, DateTime? startFrom = null, DateTime? startTo = null, Boolean? completed = null, Guid[] rowIds = null, bool forRemainder = false, Boolean? isVacation = null);
		Task<UpdateReturn> PutCalendarEvent(List<CalendarEvent> entity);
		Task<UpdateReturn> PostCalendarEvent(List<CalendarEvent> entity);
		Task<UpdateReturn> DeleteCalendarEvent(Guid rowId);


		Task<List<Invoice>> GetInvoiceList(string query);
		Task<Invoice> GetInvoice(Guid? id, bool includeAppointment = false);
		Task<UpdateReturn> PutInvoice(Invoice entity);
		Task<UpdateReturn> PostInvoice(Invoice entity);
		Task<UpdateReturn> DeleteInvoice(List<Guid> rowId);

		Task<List<Payment>> GetPaymentList(string query);
		Task<Payment> GetPayment(Guid? id);
		Task<UpdateReturn> PutPayment(Payment entity);
		Task<UpdateReturn> PostPayment(Payment entity);
		Task<UpdateReturn> DeletePayment(Guid rowId);

		Task<List<SupplierPayment>> GetSupplierPaymentList(string query);
		Task<SupplierPayment> GetSupplierPayment(Guid? id);
		Task<UpdateReturn> PutSupplierPayment(SupplierPayment entity);
		Task<UpdateReturn> PostSupplierPayment(SupplierPayment entity);
		Task<UpdateReturn> DeleteSupplierPayment(Guid rowId);

		Task<List<Refund>> GetRefundList(string query);
		Task<Refund> GetRefund(Guid? id);
		Task<UpdateReturn> PutRefund(Refund entity);
		Task<UpdateReturn> PostRefund(Refund entity);
		Task<UpdateReturn> DeleteRefund(Guid rowId);

		Task<List<SupplierRefund>> GetSupplierRefundList(string query);
		Task<SupplierRefund> GetSupplierRefund(Guid? id);
		Task<UpdateReturn> PutSupplierRefund(SupplierRefund entity);
		Task<UpdateReturn> PostSupplierRefund(SupplierRefund entity);
		Task<UpdateReturn> DeleteSupplierRefund(Guid rowId);

		Task<List<PayrollInfoResult>> GetPayrollInfo(string query);
		Task<List<InvoicePaymentByDoctorAndPeriodView>> GetInvoicePaymentByDoctorAndPeriod(string query);
		Task<List<PayrollPaymentByDoctorAndPeriodView>> GetPayrollPaymentByDoctorAndPeriod(string query);
		Task<List<InvoicePaymentByDoctors>> GetPayrollDetail(string query);
		Task<List<PayrollPayment>> GetPayrollPaymentList(string query);
		Task<PayrollPayment> GetPayrollPayment(Guid? id);
		Task<UpdateReturn> PutPayrollPayment(PayrollPayment entity);
		Task<UpdateReturn> PostPayrollPayment(PayrollPayment entity);
		Task<UpdateReturn> DeletePayrollPayment(Guid rowId);


		Task<List<Inventory>> GetInventoryList(string query);
		Task<List<InventoryBalance>> GetInventoryBalanceList(string query);

		Task<Order> GetOrder(Guid? id);
		Task<List<Order>> GetOrderList(string query);
		Task<UpdateReturn> PutOrder(Order entity);
		Task<UpdateReturn> PostOrder(Order entity);
		Task<UpdateReturn> DeleteOrder(Guid rowId);
		Task<UpdateReturn> PostInventoryBalances(List<InventoryBalance> rows);
		Task<UpdateReturn> DeleteInventory(Guid rowId);

		Task<List<MedicalHistoryRecord>> GetMedicalHistoryRecordList(string query);
		Task<UpdateReturn> PutMedicalHistoryRecord(MedicalHistoryRecord entity);
		Task<UpdateReturn> PostMedicalHistoryRecord(MedicalHistoryRecord entity);
		Task<UpdateReturn> DeleteMedicalHistoryRecord(Guid rowId);

		Task<List<TreatmentPlanRecord>> GetTreatmentPlanRecordList(string query);
		Task<UpdateReturn> PutTreatmentPlanRecord(TreatmentPlanRecord entity);
		Task<UpdateReturn> PostTreatmentPlanRecord(TreatmentPlanRecord entity);
		Task<UpdateReturn> DeleteTreatmentPlanRecord(Guid rowId);

		Task<PrintdocInfo> GetPrintdocInfo(string query);
		Task<PrintDocument[]> GetPrintDocuments(string query);

		Task<List<PatientNote>> GetPatientNoteList(string query);
		Task<UpdateReturn> PutPatientNote(PatientNote entity);
		Task<UpdateReturn> PostPatientNote(PatientNote entity);
		Task<UpdateReturn> DeletePatientNote(Guid rowId);

		Task<List<PatientDocument>> GetPatientDocumentList(string query);
		Task<UpdateReturn> PutPatientDocument(PatientDocument entity);
		Task<UpdateReturn> PostPatientDocument(PatientDocument entity);
		Task<UpdateReturn> DeletePatientDocument(Guid rowId);

		Task<List<SchedulerRecord>> GetSchedulerRecords(Guid serviceProviderRowId);
		Task<UpdateReturn> PutSchedulerRecords(List<SchedulerRecord> entities);
		Task<List<CalculateAppointmentStartFinishResult>> CalculateAppointmentStartFinish(Guid serviceProviderRowId, DateTime[] dates);

		Task<UpdateReturn> SendEmail(EmailSend entity);
		Task<List<EmailSend>> GetEmailSendList(string query);
		Task<List<EmailSendAttachment>> GetEmailSendAttachmentList(string query);

		Task<UpdateReturn> SendChargeEmail(EmailCharge entity);
		Task<List<EmailCharge>> GetEmailChargeList(string query);
		Task<List<EmailChargeAttachment>> GetEmailChargeAttachmentList(string query);

		Task<List<AppointmentClinicalNote>> GetAppointmentClinicalNoteList(string query);
		Task<UpdateReturn> PutAppointmentClinicalNote(AppointmentClinicalNote entity);
		Task<UpdateReturn> PostAppointmentClinicalNote(AppointmentClinicalNote entity);
		Task<UpdateReturn> DeleteAppointmentClinicalNote(Guid rowId);

		Task<List<FormDocument>> GetFormDocumentList(string query);
		Task<UpdateReturn> PutFormDocument(FormDocument entity);
		Task<UpdateReturn> PostFormDocument(FormDocument entity);
		Task<UpdateReturn> DeleteFormDocument(Guid rowId);

		Task<List<AppointmentTreatmentNote>> GetAppointmentTreatmentNoteList(string query);
		Task<UpdateReturn> PutAppointmentTreatmentNote(AppointmentTreatmentNote[] entity);
		Task<UpdateReturn> PostAppointmentTreatmentNote(AppointmentTreatmentNote[] entity);
		Task<UpdateReturn> DeleteAppointmentTreatmentNote(Guid rowId);

		Task<List<Chargeout>> GetChargeoutList(string query);
		Task<Chargeout> GetChargeout(Guid? id);
		Task<UpdateReturn> PutChargeout(Chargeout entity);
		Task<UpdateReturn> PostChargeout(Chargeout entity);
		Task<UpdateReturn> DeleteChargeout(List<Guid> rowId);

		Task<List<Paycharge>> GetPaychargeList(string query);
		Task<Paycharge> GetPaycharge(Guid? id);
		Task<UpdateReturn> PutPaycharge(Paycharge entity);
		Task<UpdateReturn> PostPaycharge(Paycharge entity);
		Task<UpdateReturn> DeletePaycharge(Guid rowId);

		Task<List<Refcharge>> GetRefchargeList(string query);
		Task<Refcharge> GetRefcharge(Guid? id);
		Task<UpdateReturn> PutRefcharge(Refcharge entity);
		Task<UpdateReturn> PostRefcharge(Refcharge entity);
		Task<UpdateReturn> DeleteRefcharge(Guid rowId);

		Task<WorkInout> GetWorkInout(Guid? id);
		Task<List<WorkInout>> GetWorkInoutList(string query);
		Task<UpdateReturn> PutWorkInout(WorkInout entity);
		Task<UpdateReturn> PostWorkInout(WorkInout entity);
		Task<UpdateReturn> DeleteWorkInout(Guid rowId);
	}
}
