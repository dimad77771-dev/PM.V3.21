using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Transactions;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using Profibiz.PracticeManager.BL;
using Profibiz.PracticeManager.DTO;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;

namespace Profibiz.PracticeManager.WebApi.Controllers
{
	public class PatientsController : ApiController
	{
        IWebApiRepository _repository;
        public PatientsController(IWebApiRepository repository)
	    {
	        _repository = repository;
	    }

		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			_repository.SetCurrentUserRowId(controllerContext);
		}

		public IHttpActionResult GetPatientsList(Guid? insuranceProviderRowId = null, Guid? insuranceProvidersViewGroupRowId = null, bool hasNoInsuranceProvider = false, bool includeAllFamilyMember = false, string restrictPatientList = "")
        {
            var patients = _repository.GetPatientsList(insuranceProviderRowId, insuranceProvidersViewGroupRowId, hasNoInsuranceProvider, includeAllFamilyMember, restrictPatientList);
			return Ok(patients);
        }


		[HttpGet]
		public IHttpActionResult PatientRowId2InsuranceProviders(Guid patientRowId)
		{
			var patients = _repository.PatientRowId2InsuranceProviders(patientRowId);
			return Ok(patients);
		}

		[HttpGet]
		public IHttpActionResult PatientRowId2InsuranceCoverages(Guid patientRowId)
		{
			var patients = _repository.PatientRowId2InsuranceCoverages(patientRowId);
			return Ok(patients);
		}

		[ResponseType(typeof(Patient))]
		public IHttpActionResult GetPatient(Guid id, bool isShortForm = false, bool isAddressOnly = false)
		{
			var patient = _repository.GetPatient(id, isShortForm, isAddressOnly);
			return Ok(patient);
		}


		[ResponseType(typeof(void))]
		public IHttpActionResult PutPatient([FromBody]Patient patient)
		{
			_repository.UpdatePatientCore(patient, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}


		[ResponseType(typeof(void))]
		public IHttpActionResult PostPatient([FromBody]Patient patient)
		{
			_repository.UpdatePatientCore(patient, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}

		[ResponseType(typeof(Patient))]
		public IHttpActionResult DeletePatient(Guid id)
		{
			_repository.UpdatePatientCore(new Patient { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}


		[HttpPost]
		public IHttpActionResult PatientBuildFamily(List<Patient> patients)
		{
			_repository.PatientBuildFamily(patients);
			return StatusCode(HttpStatusCode.NoContent);
		}



		[ResponseType(typeof(InsuranceCoverage))]
		public IHttpActionResult GetInsuranceCoverage(Guid id)
		{
			var entity = _repository.GetInsuranceCoverage(id);
			return Ok(entity);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PutInsuranceCoverage([FromBody]InsuranceCoverage entity)
		{
			_repository.UpdateInsuranceCoverageCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PostInsuranceCoverage([FromBody]InsuranceCoverage entity)
		{
			_repository.UpdateInsuranceCoverageCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		[ResponseType(typeof(void))]
		[HttpPost]
		public IHttpActionResult CloneInsuranceCoverage([FromBody]InsuranceCoverage entity)
		{
			var ret = _repository.CloneInsuranceCoverage(entity);
			return Ok(ret);
		}
		[ResponseType(typeof(void))]
		[HttpGet]
		public IHttpActionResult BatchCloneInsuranceCoverage()
		{
			_repository.BatchCloneInsuranceCoverage();
			return Ok();
		}
		[ResponseType(typeof(Patient))]
		public IHttpActionResult DeleteInsuranceCoverage(Guid id)
		{
			_repository.UpdateInsuranceCoverageCore(new InsuranceCoverage { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult GetInsuranceCoverageInvoiceClaimsInfo(String insuranceCoverageRowIds)
		{
			var entity = _repository.GetInsuranceCoverageInvoiceClaimsInfo(insuranceCoverageRowIds);
			return Ok(entity);
		}
		public IHttpActionResult GetInsurancePatientCategoryInfo(String insuranceCoverageRowIds)
		{
			var entity = _repository.GetInsurancePatientCategoryInfo(insuranceCoverageRowIds);
			return Ok(entity);
		}
		public IHttpActionResult GetInsuranceArticleInfo(Guid insuranceCoverageRowId, Guid patientRowId, Guid categoryRowId, Boolean isShowAllYears = false, Boolean showProblemOnly = false)
		{
			var entity = _repository.GetInsuranceArticleInfo(insuranceCoverageRowId, patientRowId, categoryRowId, isShowAllYears, showProblemOnly);
			return Ok(entity);
		}
		public IHttpActionResult GetInsuranceArticleSummary(string categoriesRowIds)
		{
			var entity = _repository.GetInsuranceArticleSummary(categoriesRowIds);
			return Ok(entity);
		}

		public IHttpActionResult GetServiceProviderList(Guid? rowId = null, Guid? professionalAssociationRowId = null)
		{
			var patients = _repository.GetServiceProviderList(rowId, professionalAssociationRowId);
			return Ok(patients);
		}
		public IHttpActionResult PutServiceProvider([FromBody]ServiceProvider entity)
		{
			_repository.UpdateServiceProviderCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult PostServiceProvider([FromBody]ServiceProvider entity)
		{
			_repository.UpdateServiceProviderCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteServiceProvider(Guid id)
		{
			_repository.UpdateServiceProviderCore(new ServiceProvider { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}


		
	
		public HttpResponseMessage GetPatientPhoto(Guid id)
		{
			DateTime dt1 = DateTimeHelper.Now;
			HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
			var photo = _repository.GetPatientPhoto(id);
			result.Content = new ByteArrayContent(photo);
			result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
			DateTime dt2 = DateTimeHelper.Now;
			return result;
		}


		
		public IHttpActionResult GetAppointmentList(Guid? appointmentBookRowId = null, Guid? patientRowId = null, Guid? insuranceProvidersViewGroupRowId = null, Guid? rowId = null, DateTime? startFrom = null, DateTime? startTo = null, Boolean? completed = null, Boolean? inInvoice = null, Boolean? forChargeout = null, Boolean? calcAppointmentPaid = null, string rowIds = null, string hideStatuses2 = null)
		{
			var entities = _repository.GetAppointmentList(appointmentBookRowId, patientRowId, insuranceProvidersViewGroupRowId, rowId, startFrom, startTo, completed, inInvoice, forChargeout, calcAppointmentPaid, rowIds, hideStatuses2);
			return Ok(entities);
		}
		public IHttpActionResult PutAppointment([FromBody]List<Appointment> entities)
		{
			_repository.UpdateAppointmentCore(entities, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult PostAppointment([FromBody]List<Appointment> entities)
		{
			_repository.UpdateAppointmentCore(entities, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteAppointment(Guid id)
		{
			_repository.UpdateAppointmentCore(new List<Appointment>() { new Appointment { RowId = id } }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult GetAppointmentInsuranceProviderDayInfo(DateTime dat, Guid serviceProviderRowId)
		{
			var entities = _repository.GetAppointmentInsuranceProviderDayInfo(dat, serviceProviderRowId);
			return Ok(entities);
		}





		public IHttpActionResult GetCalendarEventList(Guid? patientRowId = null, Guid? serviceProviderRowId = null, Guid? rowId = null, DateTime? startFrom = null, DateTime? startTo = null, Boolean? completed = null, string rowIds = null, bool? forRemainder = null, bool? isVacation = null)
		{
			var entities = _repository.GetCalendarEventList(patientRowId, serviceProviderRowId, rowId, startFrom, startTo, completed, rowIds, forRemainder, isVacation);
			return Ok(entities);
		}
		public IHttpActionResult PutCalendarEvent([FromBody]List<CalendarEvent> entities)
		{
			_repository.UpdateCalendarEventCore(entities, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult PostCalendarEvent([FromBody]List<CalendarEvent> entities)
		{
			_repository.UpdateCalendarEventCore(entities, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteCalendarEvent(Guid id)
		{
			_repository.UpdateCalendarEventCore(new List<CalendarEvent>() { new CalendarEvent { RowId = id } }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}




		#region Invoice
		public IHttpActionResult GetInvoiceList(Guid? rowId = null, Guid? patientRowId = null, Boolean? useFamilyHead = null, int? noPaidOnly = null, bool flagNoPaidOrNoApprovedAmount = false, bool negativeBalanceOnly = false, DateTime? invoiceDateFrom = null, DateTime? invoiceDateTo = null, bool includeInvoiceClaims = false, bool IsShowSentOnly = false, bool IsShowPaidOnly = false, Guid? ReferrerRowId = null, Guid? InsuranceProviderRowId = null, DateTime? createdDateFrom = null, DateTime? createdDateTo = null, bool isCoordinationProblemOnly = false)
		{
			var rezult = _repository.GetInvoiceList(rowId, patientRowId, useFamilyHead, noPaidOnly, flagNoPaidOrNoApprovedAmount, negativeBalanceOnly, invoiceDateFrom, invoiceDateTo, includeInvoiceClaims, IsShowSentOnly, IsShowPaidOnly, ReferrerRowId, InsuranceProviderRowId, createdDateFrom, createdDateTo, isCoordinationProblemOnly);
			return Ok(rezult);
		}
		public IHttpActionResult GetInvoice(Guid id, bool includeAppointment = false)
		{
			var rezult = _repository.GetInvoice(id, includeAppointment);
			return Ok(rezult);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PutInvoice([FromBody]Invoice entity)
		{
			var result = _repository.UpdateInvoiceCore(entity);
			return Ok(result);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PostInvoice([FromBody]Invoice entity)
		{
			var result = _repository.UpdateInvoiceCore(entity);
			return Ok(result);
		}
		[HttpPost]
		public IHttpActionResult DeleteInvoice([FromBody]List<Guid> ids)
		{
			var result = _repository.DeleteInvoiceCore(ids);
			return Ok(result);
		}
		#endregion


		#region Payment
		public IHttpActionResult GetPaymentList(Guid? rowId = null, Guid? patientRowId = null, int? hasNoDistributedAmount = null, DateTime? paymentDateFrom = null, DateTime? paymentDateTo = null)
		{
			var rezult = _repository.GetPaymentList(rowId, patientRowId, hasNoDistributedAmount, paymentDateFrom, paymentDateTo);
			return Ok(rezult);
		}
		public IHttpActionResult GetPayment(Guid id)
		{
			var rezult = _repository.GetPayment(id);
			return Ok(rezult);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PutPayment([FromBody]Payment entity)
		{
			_repository.UpdatePaymentCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PostPayment([FromBody]Payment entity)
		{
			_repository.UpdatePaymentCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		[ResponseType(typeof(Patient))]
		public IHttpActionResult DeletePayment(Guid id)
		{
			_repository.UpdatePaymentCore(new Payment { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		#endregion

		#region Paycharge
		public IHttpActionResult GetPaychargeList(Guid? rowId = null, Guid? patientRowId = null, int? hasNoDistributedAmount = null, DateTime? paychargeDateFrom = null, DateTime? paychargeDateTo = null)
		{
			var rezult = _repository.GetPaychargeList(rowId, patientRowId, hasNoDistributedAmount, paychargeDateFrom, paychargeDateTo);
			return Ok(rezult);
		}
		public IHttpActionResult GetPaycharge(Guid id)
		{
			var rezult = _repository.GetPaycharge(id);
			return Ok(rezult);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PutPaycharge([FromBody]Paycharge entity)
		{
			_repository.UpdatePaychargeCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PostPaycharge([FromBody]Paycharge entity)
		{
			_repository.UpdatePaychargeCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		[ResponseType(typeof(Patient))]
		public IHttpActionResult DeletePaycharge(Guid id)
		{
			_repository.UpdatePaychargeCore(new Paycharge { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		#endregion


		#region SupplierPayment
		public IHttpActionResult GetSupplierPaymentList(Guid? rowId = null, Guid? patientRowId = null, int? hasNoDistributedAmount = null, DateTime? paymentDateFrom = null, DateTime? paymentDateTo = null)
		{
			var rezult = _repository.GetSupplierPaymentList(rowId, patientRowId, hasNoDistributedAmount, paymentDateFrom, paymentDateTo);
			return Ok(rezult);
		}
		public IHttpActionResult GetSupplierPayment(Guid id)
		{
			var rezult = _repository.GetSupplierPayment(id);
			return Ok(rezult);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PutSupplierPayment([FromBody]SupplierPayment entity)
		{
			_repository.UpdateSupplierPaymentCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PostSupplierPayment([FromBody]SupplierPayment entity)
		{
			_repository.UpdateSupplierPaymentCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		[ResponseType(typeof(Patient))]
		public IHttpActionResult DeleteSupplierPayment(Guid id)
		{
			_repository.UpdateSupplierPaymentCore(new SupplierPayment { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		#endregion


		#region Refund
		public IHttpActionResult GetRefundList(Guid? rowId = null, Guid? patientRowId = null, int? hasNoDistributedAmount = null, DateTime? paymentDateFrom = null, DateTime? paymentDateTo = null)
		{
			var rezult = _repository.GetRefundList(rowId, patientRowId, hasNoDistributedAmount, paymentDateFrom, paymentDateTo);
			return Ok(rezult);
		}
		public IHttpActionResult GetRefund(Guid id)
		{
			var rezult = _repository.GetRefund(id);
			return Ok(rezult);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PutRefund([FromBody]Refund entity)
		{
			_repository.UpdateRefundCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PostRefund([FromBody]Refund entity)
		{
			_repository.UpdateRefundCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		[ResponseType(typeof(Patient))]
		public IHttpActionResult DeleteRefund(Guid id)
		{
			_repository.UpdateRefundCore(new Refund { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		#endregion


		#region Refcharge
		public IHttpActionResult GetRefchargeList(Guid? rowId = null, Guid? chargeoutRecipientRowId = null, int? hasNoDistributedAmount = null, DateTime? paychargeDateFrom = null, DateTime? paychargeDateTo = null)
		{
			var rezult = _repository.GetRefchargeList(rowId, chargeoutRecipientRowId, hasNoDistributedAmount, paychargeDateFrom, paychargeDateTo);
			return Ok(rezult);
		}
		public IHttpActionResult GetRefcharge(Guid id)
		{
			var rezult = _repository.GetRefcharge(id);
			return Ok(rezult);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PutRefcharge([FromBody]Refcharge entity)
		{
			_repository.UpdateRefchargeCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PostRefcharge([FromBody]Refcharge entity)
		{
			_repository.UpdateRefchargeCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		[ResponseType(typeof(Patient))]
		public IHttpActionResult DeleteRefcharge(Guid id)
		{
			_repository.UpdateRefchargeCore(new Refcharge { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		#endregion

		#region SupplierRefund
		public IHttpActionResult GetSupplierRefundList(Guid? rowId = null, Guid? patientRowId = null, int? hasNoDistributedAmount = null, DateTime? paymentDateFrom = null, DateTime? paymentDateTo = null)
		{
			var rezult = _repository.GetSupplierRefundList(rowId, patientRowId, hasNoDistributedAmount, paymentDateFrom, paymentDateTo);
			return Ok(rezult);
		}
		public IHttpActionResult GetSupplierRefund(Guid id)
		{
			var rezult = _repository.GetSupplierRefund(id);
			return Ok(rezult);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PutSupplierRefund([FromBody]SupplierRefund entity)
		{
			_repository.UpdateSupplierRefundCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PostSupplierRefund([FromBody]SupplierRefund entity)
		{
			_repository.UpdateSupplierRefundCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		[ResponseType(typeof(Patient))]
		public IHttpActionResult DeleteSupplierRefund(Guid id)
		{
			_repository.UpdateSupplierRefundCore(new SupplierRefund { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		#endregion

		#region Payroll
		public IHttpActionResult GetPayrollInfo(DateTime periodStart, DateTime periodFinish, Guid? serviceProviderRowId = null)
		{
			var rezult = _repository.GetPayrollInfo(periodStart, periodFinish, serviceProviderRowId);
			return Ok(rezult);
		}
		public IHttpActionResult GetInvoicePaymentByDoctorAndPeriod(Guid? serviceProviderRowId = null)
		{
			var rezult = _repository.GetInvoicePaymentByDoctorAndPeriod(serviceProviderRowId);
			return Ok(rezult);
		}
		public IHttpActionResult GetPayrollPaymentByDoctorAndPeriod(Guid? serviceProviderRowId = null)
		{
			var rezult = _repository.GetPayrollPaymentByDoctorAndPeriod(serviceProviderRowId);
			return Ok(rezult);
		}
		public IHttpActionResult GetPayrollDetail(DateTime periodStart, DateTime periodFinish, Guid serviceProviderRowId)
		{
			var rezult = _repository.GetPayrollDetail(periodStart, periodFinish, serviceProviderRowId);
			return Ok(rezult);
		}
		public IHttpActionResult GetPayrollPaymentList(Guid? serviceProviderRowId = null, DateTime? paymentDateFrom = null, DateTime? paymentDateTo = null)
		{
			var rezult = _repository.GetPayrollPaymentList(serviceProviderRowId, paymentDateFrom, paymentDateTo);
			return Ok(rezult);
		}
		public IHttpActionResult GetPayrollPayment(Guid id)
		{
			var rezult = _repository.GetPayrollPayment(id);
			return Ok(rezult);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PutPayrollPayment([FromBody]PayrollPayment entity)
		{
			_repository.UpdatePayrollPaymentCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PostPayrollPayment([FromBody]PayrollPayment entity)
		{
			_repository.UpdatePayrollPaymentCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		[ResponseType(typeof(Patient))]
		public IHttpActionResult DeletePayrollPayment(Guid id)
		{
			_repository.UpdatePayrollPaymentCore(new PayrollPayment { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		#endregion


		#region Inventory
		public IHttpActionResult GetInventoryList(Guid? rowId = null, Guid? orderRowId = null, Guid? invoiceRowId = null, DateTime? transactionDateFrom = null, DateTime? transactionDateTo = null)
		{
			var rezult = _repository.GetInventoryList(rowId, orderRowId, invoiceRowId, transactionDateFrom, transactionDateTo);
			return Ok(rezult);
		}

		public IHttpActionResult GetInventoryBalanceList(Guid? rowId = null)
		{
			var rezult = _repository.GetInventoryBalanceList(rowId);
			return Ok(rezult);
		}

		#endregion


		#region Order
		public IHttpActionResult GetOrder(Guid id)
		{
			var rezult = _repository.GetOrder(id);
			return Ok(rezult);
		}
		public IHttpActionResult GetOrderList(Guid? rowId = null, Guid? supplierRowId = null, DateTime? orderDateFrom = null, DateTime? orderDateTo = null, Boolean? noPaidOnly = null)
		{
			var rezult = _repository.GetOrderList(rowId, supplierRowId, orderDateFrom, orderDateTo, noPaidOnly);
			return Ok(rezult);
		}
		public IHttpActionResult PutOrder([FromBody]Order entity)
		{
			_repository.UpdateOrderCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult PostOrder([FromBody]Order entity)
		{
			_repository.UpdateOrderCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteOrder(Guid id)
		{
			_repository.UpdateOrderCore(new Order { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}

		public IHttpActionResult PostInventoryBalances([FromBody]List<InventoryBalance> rows)
		{
			_repository.PostInventoryBalances(rows);
			return StatusCode(HttpStatusCode.NoContent);
		}

		public IHttpActionResult DeleteInventory(Guid id)
		{
			_repository.UpdateInventoryCore(new Inventory { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		#endregion




		#region MedicalHistoryRecord
		public IHttpActionResult GetMedicalHistoryRecordList(Guid? rowId = null, Guid? patientRowId = null)
		{
			var rezult = _repository.GetMedicalHistoryRecordList(rowId, patientRowId);
			return Ok(rezult);
		}

		public IHttpActionResult PutMedicalHistoryRecord([FromBody]MedicalHistoryRecord entity)
		{
			_repository.UpdateMedicalHistoryRecordCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult PostMedicalHistoryRecord([FromBody]MedicalHistoryRecord entity)
		{
			_repository.UpdateMedicalHistoryRecordCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteMedicalHistoryRecord(Guid id)
		{
			_repository.UpdateMedicalHistoryRecordCore(new MedicalHistoryRecord { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		#endregion

		#region TreatmentPlanRecord
		public IHttpActionResult GetTreatmentPlanRecordList(Guid? rowId = null, Guid? patientRowId = null)
		{
			var rezult = _repository.GetTreatmentPlanRecordList(rowId, patientRowId);
			return Ok(rezult);
		}

		public IHttpActionResult PutTreatmentPlanRecord([FromBody]TreatmentPlanRecord entity)
		{
			_repository.UpdateTreatmentPlanRecordCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult PostTreatmentPlanRecord([FromBody]TreatmentPlanRecord entity)
		{
			_repository.UpdateTreatmentPlanRecordCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteTreatmentPlanRecord(Guid id)
		{
			_repository.UpdateTreatmentPlanRecordCore(new TreatmentPlanRecord { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		#endregion


		#region PrintdocInfo
		public IHttpActionResult GetPrintdocInfo(Guid invoiceRowId)
		{
			var rezult = _repository.GetPrintdocInfo(invoiceRowId);
			return Ok(rezult);
		}

		public IHttpActionResult GetPrintDocuments()
		{
			var rezult = _repository.GetPrintDocuments();
			return Ok(rezult);
		}


		#endregion


		#region PatientNote
		public IHttpActionResult GetPatientNoteList(Guid? rowId = null, Guid? patientRowId = null)
		{
			var rezult = _repository.GetPatientNoteList(rowId, patientRowId);
			return Ok(rezult);
		}

		public IHttpActionResult PutPatientNote([FromBody]PatientNote entity)
		{
			_repository.UpdatePatientNoteCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult PostPatientNote([FromBody]PatientNote entity)
		{
			_repository.UpdatePatientNoteCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeletePatientNote(Guid id)
		{
			_repository.UpdatePatientNoteCore(new PatientNote { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		#endregion

		#region PatientDocument
		public IHttpActionResult GetPatientDocumentList(Guid? rowId = null, Guid? patientRowId = null)
		{
			var rezult = _repository.GetPatientDocumentList(rowId, patientRowId);
			return Ok(rezult);
		}

		public IHttpActionResult PutPatientDocument([FromBody]PatientDocument entity)
		{
			_repository.UpdatePatientDocumentCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult PostPatientDocument([FromBody]PatientDocument entity)
		{
			_repository.UpdatePatientDocumentCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeletePatientDocument(Guid id)
		{
			_repository.UpdatePatientDocumentCore(new PatientDocument { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		#endregion


		#region SchedulerRecords
		public IHttpActionResult GetSchedulerRecords(Guid serviceProviderRowId)
		{
			var rezult = _repository.GetSchedulerRecords(serviceProviderRowId);
			return Ok(rezult);
		}
		public IHttpActionResult PutSchedulerRecords([FromBody]List<SchedulerRecord> rows)
		{
			_repository.PutSchedulerRecords(rows);
			return StatusCode(HttpStatusCode.NoContent);
		}
		[HttpGet]
		public IHttpActionResult CalculateAppointmentStartFinish(Guid serviceProviderRowId, [FromUri] DateTime[] date)
		{
			var rezult = _repository.CalculateAppointmentStartFinish(serviceProviderRowId, date);
			return Ok(rezult);
		}
		#endregion

		#region EMail
		[HttpPost]
		public IHttpActionResult SendEmail(EmailSend parm)
		{
			_repository.SendEmail(parm);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult GetEmailSendList(DateTime? sendDateFrom = null, DateTime? sendDateTo = null)
		{
			var rezult = _repository.GetEmailSendList(sendDateFrom, sendDateTo);
			return Ok(rezult);
		}
		public IHttpActionResult GetEmailSendAttachmentList(Guid emailSendRowId)
		{
			var rezult = _repository.GetEmailSendAttachmentList(emailSendRowId);
			return Ok(rezult);
		}
		#endregion

		#region EMailCharge
		[HttpPost]
		public IHttpActionResult SendChargeEmail(EmailCharge parm)
		{
			_repository.SendChargeEmail(parm);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult GetEmailChargeList(DateTime? sendDateFrom = null, DateTime? sendDateTo = null)
		{
			var rezult = _repository.GetEmailChargeList(sendDateFrom, sendDateTo);
			return Ok(rezult);
		}
		public IHttpActionResult GetEmailChargeAttachmentList(Guid emailSendRowId)
		{
			var rezult = _repository.GetEmailChargeAttachmentList(emailSendRowId);
			return Ok(rezult);
		}
		#endregion

		#region AppointmentClinicalNote
		public IHttpActionResult GetAppointmentClinicalNoteList(Guid? rowId = null, Guid? appointmentRowId = null)
		{
			var rezult = _repository.GetAppointmentClinicalNoteList(rowId, appointmentRowId);
			return Ok(rezult);
		}

		public IHttpActionResult PutAppointmentClinicalNote([FromBody]AppointmentClinicalNote entity)
		{
			_repository.UpdateAppointmentClinicalNoteCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult PostAppointmentClinicalNote([FromBody]AppointmentClinicalNote entity)
		{
			_repository.UpdateAppointmentClinicalNoteCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteAppointmentClinicalNote(Guid id)
		{
			_repository.UpdateAppointmentClinicalNoteCore(new AppointmentClinicalNote { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		#endregion

		#region FormDocument
		public IHttpActionResult GetFormDocumentList(Guid? rowId = null)
		{
			var rezult = _repository.GetFormDocumentList(rowId);
			return Ok(rezult);
		}

		public IHttpActionResult PutFormDocument([FromBody] FormDocument entity)
		{
			_repository.UpdateFormDocumentCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult PostFormDocument([FromBody] FormDocument entity)
		{
			_repository.UpdateFormDocumentCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteFormDocument(Guid id)
		{
			_repository.UpdateFormDocumentCore(new FormDocument { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		#endregion

		#region FormDocmodel
		public IHttpActionResult GetFormDocmodelList(int? formDictionary = null, Guid? patientRowId = null, Guid? formRowId = null)
		{
			var rezult = _repository.GetFormDocmodelList(formDictionary, patientRowId, formRowId);
			return Ok(rezult);
		}

		public IHttpActionResult PutFormDocmodel([FromBody] FormDocmodel entity)
		{
			_repository.UpdateFormDocmodelCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult PostFormDocmodel([FromBody] FormDocmodel entity)
		{
			_repository.UpdateFormDocmodelCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteFormDocmodel(Guid id)
		{
			_repository.UpdateFormDocmodelCore(new FormDocmodel { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		#endregion

		#region AppointmentTreatmentNote
		public IHttpActionResult GetAppointmentTreatmentNoteList(Guid? rowId = null, Guid? appointmentRowId = null)
		{
			var rezult = _repository.GetAppointmentTreatmentNoteList(rowId, appointmentRowId);
			return Ok(rezult);
		}

		public IHttpActionResult PutAppointmentTreatmentNote([FromBody]AppointmentTreatmentNote[] entity)
		{
			_repository.UpdateAppointmentTreatmentNoteCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult PostAppointmentTreatmentNote([FromBody]AppointmentTreatmentNote[] entity)
		{
			_repository.UpdateAppointmentTreatmentNoteCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteAppointmentTreatmentNote(Guid id)
		{
			_repository.UpdateAppointmentTreatmentNoteCore(new[] { new AppointmentTreatmentNote { RowId = id } }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		#endregion

		#region Chargeout
		public IHttpActionResult GetChargeoutList(Guid? rowId = null, Guid? chargeoutRecipientRowId = null, int? noPaidOnly = null, bool flagNoPaidOrNoApprovedAmount = false, bool negativeBalanceOnly = false, DateTime? chargeoutDateFrom = null, DateTime? chargeoutDateTo = null, bool isShowSentOnly = false, bool isShowPaidOnly = false, DateTime? createdDateFrom = null, DateTime? createdDateTo = null)
		{
			var rezult = _repository.GetChargeoutList(rowId, chargeoutRecipientRowId, noPaidOnly, flagNoPaidOrNoApprovedAmount, negativeBalanceOnly, chargeoutDateFrom, chargeoutDateTo, isShowSentOnly, isShowPaidOnly, createdDateFrom, createdDateTo);
			return Ok(rezult);
		}
		public IHttpActionResult GetChargeout(Guid id)
		{
			var rezult = _repository.GetChargeout(id);
			return Ok(rezult);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PutChargeout([FromBody]Chargeout entity)
		{
			var result = _repository.UpdateChargeoutCore(entity);
			return Ok(result);
		}
		[ResponseType(typeof(void))]
		public IHttpActionResult PostChargeout([FromBody]Chargeout entity)
		{
			var result = _repository.UpdateChargeoutCore(entity);
			return Ok(result);
		}
		[HttpPost]
		public IHttpActionResult DeleteChargeout([FromBody]List<Guid> ids)
		{
			var result = _repository.DeleteChargeoutCore(ids);
			return Ok(result);
		}
		#endregion

		#region WorkInout
		public IHttpActionResult GetWorkInout(Guid id)
		{
			var rezult = _repository.GetWorkInout(id);
			return Ok(rezult);
		}
		public IHttpActionResult GetWorkInoutList(Guid? rowId = null, DateTime? workInoutDateFrom = null, DateTime? workInoutDateTo = null)
		{
			var rezult = _repository.GetWorkInoutList(rowId, workInoutDateFrom, workInoutDateTo);
			return Ok(rezult);
		}
		public IHttpActionResult PutWorkInout([FromBody] WorkInout entity)
		{
			_repository.UpdateWorkInoutCore(entity, EntityState.Modified);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult PostWorkInout([FromBody] WorkInout entity)
		{
			_repository.UpdateWorkInoutCore(entity, EntityState.Added);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteWorkInout(Guid id)
		{
			_repository.UpdateWorkInoutCore(new WorkInout { RowId = id }, EntityState.Deleted);
			return StatusCode(HttpStatusCode.NoContent);
		}
		#endregion


	}
}