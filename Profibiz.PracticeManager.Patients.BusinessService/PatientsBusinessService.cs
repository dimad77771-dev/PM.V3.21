using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;


namespace Profibiz.PracticeManager.Patients.BusinessService
{
	using Infrastructure;
	using Model;
	using Newtonsoft.Json;
	using SharedCode;
	using System.Configuration;
	using static BusinessServiceHelper;

	public class PatientsBusinessService : IPatientsBusinessService
    {
		private string _baseUrl = ConfigurationManager.AppSettings["service.url"];
		//private const string _baseUrl = @"https://omega.profibiz.com/PM/";

		async public Task<List<PatientsListView>> GetPatientList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/getpatientslist?" + query);
			var patients = await response.Content.ReadAsAsync<IEnumerable<PatientsListView>>();
			return patients.ToList();
		}


		async public Task<Patient> GetPatient(Guid rowId, bool isShortForm = false, bool isAddressOnly = false)
        {
			var _client = new MyHttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var qry = "id=" + rowId.ToString();
			if (isShortForm)
			{
				qry += "&isShortForm=" + isShortForm;
			}
			if (isAddressOnly)
			{
				qry += "&isAddressOnly=" + isAddressOnly;
			}
			var response = await _client.GetResponse(_baseUrl, "api/patients/getpatient?" + qry);
			var patient = await response.Content.ReadAsAsync<Patient>();
            return patient;
        }

		async public Task<Guid[]> PatientRowId2InsuranceProviders(Guid patientRowId)
		{
			var _client = new MyHttpClient();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var response = await _client.GetResponse(_baseUrl, "api/patients/PatientRowId2InsuranceProviders?patientRowId=" + patientRowId.ToString());
			var result = await response.Content.ReadAsAsync<Guid[]>();
			return result;
		}

		async public Task<List<InsuranceCoverage>> PatientRowId2InsuranceCoverages(Guid patientRowId)
		{
			var _client = new MyHttpClient();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var response = await _client.GetResponse(_baseUrl, "api/patients/PatientRowId2InsuranceCoverages?patientRowId=" + patientRowId.ToString());
			var result = await response.Content.ReadAsAsync<List<InsuranceCoverage>>();
			return result;
		}


		async public Task<byte[]> GetPatientPhoto(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/png"));

			try
			{
				var response = await _client.GetResponse(_baseUrl, "api/patients/GetPatientPhoto/" + rowId.ToString());
				var photo = await response.Content.ReadAsByteArrayAsync();
				return photo;
			}
			catch (Exception ex)
			{
				var a = ex;
			}
			return null;
		}

		public List<PatientCoverage> GetpatientCoverage(Guid patientRowId)
        {
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = _client.GetAsync("api/patients/getpatientcoverages" + "/" + patientRowId.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                var patientsCoverages = response.Content.ReadAsAsync<IEnumerable<PatientCoverage>>().Result;
                return patientsCoverages.ToList();
            }

            return null;
        }

		async public Task<UpdateReturn> PutPatient(Patient patient)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(patient);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutPatient", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostPatient(Patient patient)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(patient);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostPatient", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeletePatient(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeletePatient/" + rowId);
			return await response.ValidateResponse();
		}

		async public Task<UpdateReturn> PatientBuildFamily(List<Patient> patients)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(patients);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PatientBuildFamily", content);
			return await response.ValidateResponse();
		}


		async public Task<InsuranceCoverage> GetInsuranceCoverage(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var response = await _client.GetResponse(_baseUrl, "api/patients/GetInsuranceCoverage/" + rowId.ToString());
			var entity = await response.Content.ReadAsAsync<InsuranceCoverage>();
			return entity;
		}
		async public Task<UpdateReturn> PutInsuranceCoverage(InsuranceCoverage entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutInsuranceCoverage", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostInsuranceCoverage(InsuranceCoverage entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostInsuranceCoverage", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> CloneInsuranceCoverage(InsuranceCoverage entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/CloneInsuranceCoverage", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteInsuranceCoverage(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeleteInsuranceCoverage/" + rowId);
			return await response.ValidateResponse();
		}
		async public Task<InsuranceCoverageInvoiceClaimsInfo[]> GetInsuranceCoverageInvoiceClaimsInfo(String insuranceCoverageRowIds)
		{
			var _client = new MyHttpClient();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var response = await _client.GetResponse(_baseUrl, "api/patients/GetInsuranceCoverageInvoiceClaimsInfo?insuranceCoverageRowIds=" + insuranceCoverageRowIds);
			var entity = await response.Content.ReadAsAsync<InsuranceCoverageInvoiceClaimsInfo[]>();
			return entity;
		}
		async public Task<InsurancePatientCategoryInfo> GetInsurancePatientCategoryInfo(String insuranceCoverageRowIds)
		{
			if (string.IsNullOrEmpty(insuranceCoverageRowIds))
			{
				return new InsurancePatientCategoryInfo();
			}

			var _client = new MyHttpClient();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var response = await _client.GetResponse(_baseUrl, "api/patients/GetInsurancePatientCategoryInfo?insuranceCoverageRowIds=" + insuranceCoverageRowIds);
			var entity = await response.Content.ReadAsAsync<InsurancePatientCategoryInfo>();
			return entity;
		}
		async public Task<InsuranceArticleInfo> GetInsuranceArticleInfo(string qry)
		{
			var _client = new MyHttpClient();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var response = await _client.GetResponse(_baseUrl, "api/patients/GetInsuranceArticleInfo?" + qry);
			var entity = await response.Content.ReadAsAsync<InsuranceArticleInfo>();
			return entity;
		}
		async public Task<InsuranceArticleSummary> GetInsuranceArticleSummary(string qry)
		{
			var _client = new MyHttpClient();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var response = await _client.GetResponse(_baseUrl, "api/patients/GetInsuranceArticleSummary?" + qry);
			var entity = await response.Content.ReadAsAsync<InsuranceArticleSummary>();
			return entity;
		}


		async public Task<InsuranceCoverage> GetInsuranceCoverage(Guid[] insuranceCoverageRowId)
		{
			var _client = new MyHttpClient();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var response = await _client.GetResponse(_baseUrl, "api/patients/GetInsuranceCoverage?insuranceCoverageRowIds=" + insuranceCoverageRowId.ToWebQuery());
			var entity = await response.Content.ReadAsAsync<InsuranceCoverage>();
			return entity;
		}



		async public Task<List<ServiceProvider>> GetServiceProviderList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetServiceProviderList?" + query);
			var patients = await response.Content.ReadAsAsync<IEnumerable<ServiceProvider>>();
			return patients.ToList();
		}
		async public Task<ServiceProvider> GetServiceProvider(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var response = await _client.GetResponse(_baseUrl, "api/patients/GetServiceProvider/" + rowId.ToString());
			var entity = await response.Content.ReadAsAsync<ServiceProvider>();
			return entity;
		}
		async public Task<UpdateReturn> PutServiceProvider(ServiceProvider entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutServiceProvider", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostServiceProvider(ServiceProvider entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostServiceProvider", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteServiceProvider(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeleteServiceProvider/" + rowId);
			return await response.ValidateResponse();
		}



		async public Task<List<Appointment>> GetAppointmentList(Guid? appointmentBookRowId = null, Guid? patientRowId = null, Guid? insuranceProvidersViewGroupRowId = null, Guid? rowId = null, DateTime? startFrom = null, DateTime? startTo = null, Boolean? completed = null, Boolean? inInvoice = null, Boolean? inChargeout = null, Boolean? forChargeout = null, Guid[] rowIds = null)
		{
			var _client = new MyHttpClient();
			var query = "";
			if (appointmentBookRowId != null)
			{
				query += "appointmentBookRowId=" + appointmentBookRowId + "&";
			}
			if (patientRowId != null)
			{
				query += "patientRowId=" + patientRowId + "&";
			}
			if (insuranceProvidersViewGroupRowId != null)
			{
				query += "insuranceProvidersViewGroupRowId=" + insuranceProvidersViewGroupRowId + "&";
			}
			if (rowId != null)
			{
				query += "rowId=" + rowId + "&";
			}
			if (rowIds != null)
			{
				query += "rowIds=" + rowIds.ToWebQuery() + "&";
			}
			if (startFrom != null)
            {
                query += "startFrom=" + startFrom.ToWebQuery() + "&";
            }
            if (startTo != null)
            {
                query += "startTo=" + startTo.ToWebQuery() + "&";
            }
            if (completed != null)
            {
                query += "completed=" + completed.ToWebQuery() + "&";
            }
			if (inInvoice != null)
			{
				query += "inInvoice=" + inInvoice.ToWebQuery() + "&";
			}
			if (forChargeout != null)
			{
				query += "forChargeout=" + forChargeout.ToWebQuery() + "&";
			}
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetAppointmentList?" + query);
			var patients = await response.Content.ReadAsAsync<IEnumerable<Appointment>>();
			return patients.ToList();
		}
		async public Task<UpdateReturn> PutAppointment(List<Appointment> entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutAppointment", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostAppointment(List<Appointment> entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostAppointment", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteAppointment(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeleteAppointment/" + rowId);
			return await response.ValidateResponse();
		}
		async public Task<List<AppointmentInsuranceProviderDayInfo>> GetAppointmentInsuranceProviderDayInfo(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetAppointmentInsuranceProviderDayInfo?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<AppointmentInsuranceProviderDayInfo>>();
			return list.ToList();
		}


		async public Task<List<CalendarEvent>> GetCalendarEventList(Guid? patientRowId = null, Guid? serviceProviderRowId = null, Guid ? rowId = null, DateTime? startFrom = null, DateTime? startTo = null, Boolean? completed = null, Guid[] rowIds = null, bool forRemainder = false, Boolean? isVacation = null)
		{
			var _client = new MyHttpClient();
			var query = "";
			if (patientRowId != null)
			{
				query += "patientRowId=" + patientRowId.ToWebQuery() + "&";
			}
			if (serviceProviderRowId != null)
			{
				query += "serviceProviderRowId=" + serviceProviderRowId.ToWebQuery() + "&";
			}
			if (rowId != null)
			{
				query += "rowId=" + rowId.ToWebQuery() + "&";
			}
			if (rowIds != null)
			{
				query += "rowIds=" + rowIds.ToWebQuery() + "&";
			}
			if (startFrom != null)
			{
				query += "startFrom=" + startFrom.ToWebQuery() + "&";
			}
			if (startTo != null)
			{
				query += "startTo=" + startTo.ToWebQuery() + "&";
			}
			if (completed != null)
			{
				query += "completed=" + completed.ToWebQuery() + "&";
			}
			if (forRemainder)
			{
				query += "forRemainder=" + forRemainder.ToWebQuery() + "&";
			}
			if (isVacation != null)
			{
				query += "isVacation=" + isVacation.ToWebQuery() + "&";
			}
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetCalendarEventList?" + query);
			var patients = await response.Content.ReadAsAsync<IEnumerable<CalendarEvent>>();
			return patients.ToList();
		}
		async public Task<UpdateReturn> PutCalendarEvent(List<CalendarEvent> entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutCalendarEvent", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostCalendarEvent(List<CalendarEvent> entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostCalendarEvent", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteCalendarEvent(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeleteCalendarEvent/" + rowId);
			return await response.ValidateResponse();
		}

		async public Task<List<Invoice>> GetInvoiceList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetInvoiceList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<Invoice>>();
			return list.ToList();
		}
		async public Task<Invoice> GetInvoice(Guid? rowId, bool includeAppointment = false)
		{
			var _client = new MyHttpClient();
			var query = "id=" + rowId;
			if (includeAppointment)
			{
				query += "&includeAppointment=true";
			}
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetInvoice?" + query);
			var rez = await response.Content.ReadAsAsync<Invoice>();
			return rez;
		}
		async public Task<UpdateReturn> PutInvoice(Invoice entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutInvoice", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostInvoice(Invoice entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostInvoice", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteInvoice(List<Guid> rowIds)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(rowIds);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/DeleteInvoice", content);
			return await response.ValidateResponse();
		}




		async public Task<List<Payment>> GetPaymentList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetPaymentList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<Payment>>();
			return list.ToList();
		}
		async public Task<Payment> GetPayment(Guid? rowId)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetPayment?id=" + rowId);
			var rez = await response.Content.ReadAsAsync<Payment>();
			return rez;
		}
		async public Task<UpdateReturn> PutPayment(Payment entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutPayment", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostPayment(Payment entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostPayment", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeletePayment(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeletePayment/" + rowId);
			return await response.ValidateResponse();
		}


		async public Task<List<SupplierPayment>> GetSupplierPaymentList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetSupplierPaymentList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<SupplierPayment>>();
			return list.ToList();
		}
		async public Task<SupplierPayment> GetSupplierPayment(Guid? rowId)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetSupplierPayment?id=" + rowId);
			var rez = await response.Content.ReadAsAsync<SupplierPayment>();
			return rez;
		}
		async public Task<UpdateReturn> PutSupplierPayment(SupplierPayment entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutSupplierPayment", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostSupplierPayment(SupplierPayment entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostSupplierPayment", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteSupplierPayment(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeleteSupplierPayment/" + rowId);
			return await response.ValidateResponse();
		}


		async public Task<List<Refund>> GetRefundList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetRefundList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<Refund>>();
			return list.ToList();
		}
		async public Task<Refund> GetRefund(Guid? rowId)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetRefund?id=" + rowId);
			var rez = await response.Content.ReadAsAsync<Refund>();
			return rez;
		}
		async public Task<UpdateReturn> PutRefund(Refund entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutRefund", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostRefund(Refund entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostRefund", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteRefund(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeleteRefund/" + rowId);
			return await response.ValidateResponse();
		}


		async public Task<List<SupplierRefund>> GetSupplierRefundList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetSupplierRefundList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<SupplierRefund>>();
			return list.ToList();
		}
		async public Task<SupplierRefund> GetSupplierRefund(Guid? rowId)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetSupplierRefund?id=" + rowId);
			var rez = await response.Content.ReadAsAsync<SupplierRefund>();
			return rez;
		}
		async public Task<UpdateReturn> PutSupplierRefund(SupplierRefund entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutSupplierRefund", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostSupplierRefund(SupplierRefund entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostSupplierRefund", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteSupplierRefund(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeleteSupplierRefund/" + rowId);
			return await response.ValidateResponse();
		}


		async public Task<List<PayrollInfoResult>> GetPayrollInfo(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetPayrollInfo?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<PayrollInfoResult>>();
			return list.ToList();
		}
		async public Task<List<InvoicePaymentByDoctorAndPeriodView>> GetInvoicePaymentByDoctorAndPeriod(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetInvoicePaymentByDoctorAndPeriod?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<InvoicePaymentByDoctorAndPeriodView>>();
			return list.ToList();
		}
		async public Task<List<PayrollPaymentByDoctorAndPeriodView>> GetPayrollPaymentByDoctorAndPeriod(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetPayrollPaymentByDoctorAndPeriod?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<PayrollPaymentByDoctorAndPeriodView>>();
			return list.ToList();
		}
		async public Task<List<InvoicePaymentByDoctors>> GetPayrollDetail(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetPayrollDetail?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<InvoicePaymentByDoctors>>();
			return list.ToList();
		}
		async public Task<List<PayrollPayment>> GetPayrollPaymentList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetPayrollPaymentList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<PayrollPayment>>();
			return list.ToList();
		}
		async public Task<PayrollPayment> GetPayrollPayment(Guid? rowId)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetPayrollPayment?id=" + rowId);
			var rez = await response.Content.ReadAsAsync<PayrollPayment>();
			return rez;
		}
		async public Task<UpdateReturn> PutPayrollPayment(PayrollPayment entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutPayrollPayment", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostPayrollPayment(PayrollPayment entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostPayrollPayment", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeletePayrollPayment(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeletePayrollPayment/" + rowId);
			return await response.ValidateResponse();
		}


		async public Task<List<Inventory>> GetInventoryList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetInventoryList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<Inventory>>();
			return list.ToList();
		}

		async public Task<List<InventoryBalance>> GetInventoryBalanceList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetInventoryBalanceList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<InventoryBalance>>();
			return list.ToList();
		}


		async public Task<Order> GetOrder(Guid? rowId)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetOrder?id=" + rowId);
			var rez = await response.Content.ReadAsAsync<Order>();
			return rez;
		}
		async public Task<List<Order>> GetOrderList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetOrderList?" + query);
			var rez = await response.Content.ReadAsAsync<List<Order>>();
			return rez;
		}
		async public Task<UpdateReturn> PutOrder(Order entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutOrder", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostOrder(Order entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostOrder", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteOrder(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeleteOrder/" + rowId);
			return await response.ValidateResponse();
		}

		async public Task<UpdateReturn> PostInventoryBalances(List<InventoryBalance> rows)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(rows);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostInventoryBalances", content);
			return await response.ValidateResponse();
		}

		async public Task<UpdateReturn> DeleteInventory(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeleteInventory/" + rowId);
			return await response.ValidateResponse();
		}


		async public Task<List<MedicalHistoryRecord>> GetMedicalHistoryRecordList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetMedicalHistoryRecordList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<MedicalHistoryRecord>>();
			return list.ToList();
		}
		async public Task<UpdateReturn> PutMedicalHistoryRecord(MedicalHistoryRecord entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutMedicalHistoryRecord", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostMedicalHistoryRecord(MedicalHistoryRecord entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostMedicalHistoryRecord", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteMedicalHistoryRecord(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeleteMedicalHistoryRecord/" + rowId);
			return await response.ValidateResponse();
		}

		async public Task<List<TreatmentPlanRecord>> GetTreatmentPlanRecordList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetTreatmentPlanRecordList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<TreatmentPlanRecord>>();
			return list.ToList();
		}
		async public Task<UpdateReturn> PutTreatmentPlanRecord(TreatmentPlanRecord entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutTreatmentPlanRecord", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostTreatmentPlanRecord(TreatmentPlanRecord entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostTreatmentPlanRecord", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteTreatmentPlanRecord(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeleteTreatmentPlanRecord/" + rowId);
			return await response.ValidateResponse();
		}


		async public Task<PrintdocInfo> GetPrintdocInfo(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetPrintdocInfo?" + query);
			var list = await response.Content.ReadAsAsync<PrintdocInfo>();
			return list;
		}

		async public Task<PrintDocument[]> GetPrintDocuments(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetPrintDocuments?" + query);
			var list = await response.Content.ReadAsAsync<PrintDocument[]>();
			return list;
		}


		async public Task<List<PatientNote>> GetPatientNoteList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetPatientNoteList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<PatientNote>>();
			return list.ToList();
		}
		async public Task<UpdateReturn> PutPatientNote(PatientNote entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutPatientNote", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostPatientNote(PatientNote entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostPatientNote", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeletePatientNote(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeletePatientNote/" + rowId);
			return await response.ValidateResponse();
		}



		async public Task<List<PatientDocument>> GetPatientDocumentList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetPatientDocumentList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<PatientDocument>>();
			return list.ToList();
		}
		async public Task<UpdateReturn> PutPatientDocument(PatientDocument entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutPatientDocument", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostPatientDocument(PatientDocument entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostPatientDocument", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeletePatientDocument(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeletePatientDocument/" + rowId);
			return await response.ValidateResponse();
		}

		async public Task<List<SchedulerRecord>> GetSchedulerRecords(Guid serviceProviderRowId)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetSchedulerRecords?serviceProviderRowId=" + serviceProviderRowId);
			var rez = await response.Content.ReadAsAsync<List<SchedulerRecord>>();
			return rez;
		}
		async public Task<UpdateReturn> PutSchedulerRecords(List<SchedulerRecord> entities)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entities);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutSchedulerRecords", content);
			return await response.ValidateResponse();
		}
		async public Task<List<CalculateAppointmentStartFinishResult>> CalculateAppointmentStartFinish(Guid serviceProviderRowId, DateTime[] dates)
		{
			var _client = new MyHttpClient();
			var url = "api/patients/CalculateAppointmentStartFinish?" + "serviceProviderRowId=" + serviceProviderRowId;
			url += string.Join("", dates.Select(q => "&date=" + q.ToWebQuery()));
			var response = await _client.GetResponse(_baseUrl, url);
			var rez = await response.Content.ReadAsAsync<List<CalculateAppointmentStartFinishResult>>();
			return rez;
		}


		async public Task<UpdateReturn> SendEmail(EmailSend entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/SendEmail", content);
			return await response.ValidateResponse();
		}
		async public Task<List<EmailSend>> GetEmailSendList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetEmailSendList?" + query);
			var rez = await response.Content.ReadAsAsync<List<EmailSend>>();
			return rez;
		}
		async public Task<List<EmailSendAttachment>> GetEmailSendAttachmentList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetEmailSendAttachmentList?" + query);
			var rez = await response.Content.ReadAsAsync<List<EmailSendAttachment>>();
			return rez;
		}

		async public Task<UpdateReturn> SendChargeEmail(EmailCharge entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/SendChargeEmail", content);
			return await response.ValidateResponse();
		}
		async public Task<List<EmailCharge>> GetEmailChargeList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetEmailChargeList?" + query);
			var rez = await response.Content.ReadAsAsync<List<EmailCharge>>();
			return rez;
		}
		async public Task<List<EmailChargeAttachment>> GetEmailChargeAttachmentList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetEmailChargeAttachmentList?" + query);
			var rez = await response.Content.ReadAsAsync<List<EmailChargeAttachment>>();
			return rez;
		}


		async public Task<List<AppointmentClinicalNote>> GetAppointmentClinicalNoteList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetAppointmentClinicalNoteList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<AppointmentClinicalNote>>();
			return list.ToList();
		}
		async public Task<UpdateReturn> PutAppointmentClinicalNote(AppointmentClinicalNote entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutAppointmentClinicalNote", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostAppointmentClinicalNote(AppointmentClinicalNote entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostAppointmentClinicalNote", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteAppointmentClinicalNote(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeleteAppointmentClinicalNote/" + rowId);
			return await response.ValidateResponse();
		}


		async public Task<List<FormDocument>> GetFormDocumentList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetFormDocumentList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<FormDocument>>();
			return list.ToList();
		}
		async public Task<UpdateReturn> PutFormDocument(FormDocument entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutFormDocument", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostFormDocument(FormDocument entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostFormDocument", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteFormDocument(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeleteFormDocument/" + rowId);
			return await response.ValidateResponse();
		}

		async public Task<List<FormDocmodel>> GetFormDocmodelList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetFormDocmodelList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<FormDocmodel>>();
			return list.ToList();
		}
		async public Task<UpdateReturn> PutFormDocmodel(FormDocmodel entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutFormDocmodel", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostFormDocmodel(FormDocmodel entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostFormDocmodel", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteFormDocmodel(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeleteFormDocmodel/" + rowId);
			return await response.ValidateResponse();
		}


		async public Task<List<AppointmentTreatmentNote>> GetAppointmentTreatmentNoteList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetAppointmentTreatmentNoteList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<AppointmentTreatmentNote>>();
			return list.ToList();
		}
		async public Task<UpdateReturn> PutAppointmentTreatmentNote(AppointmentTreatmentNote[] entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutAppointmentTreatmentNote", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostAppointmentTreatmentNote(AppointmentTreatmentNote[] entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostAppointmentTreatmentNote", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteAppointmentTreatmentNote(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeleteAppointmentTreatmentNote/" + rowId);
			return await response.ValidateResponse();
		}


		async public Task<List<Chargeout>> GetChargeoutList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetChargeoutList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<Chargeout>>();
			return list.ToList();
		}
		async public Task<Chargeout> GetChargeout(Guid? rowId)
		{
			var _client = new MyHttpClient();
			var query = "id=" + rowId;
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetChargeout?" + query);
			var rez = await response.Content.ReadAsAsync<Chargeout>();
			return rez;
		}
		async public Task<UpdateReturn> PutChargeout(Chargeout entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutChargeout", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostChargeout(Chargeout entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostChargeout", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteChargeout(List<Guid> rowIds)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(rowIds);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/DeleteChargeout", content);
			return await response.ValidateResponse();
		}


		async public Task<List<Paycharge>> GetPaychargeList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetPaychargeList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<Paycharge>>();
			return list.ToList();
		}
		async public Task<Paycharge> GetPaycharge(Guid? rowId)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetPaycharge?id=" + rowId);
			var rez = await response.Content.ReadAsAsync<Paycharge>();
			return rez;
		}
		async public Task<UpdateReturn> PutPaycharge(Paycharge entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutPaycharge", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostPaycharge(Paycharge entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostPaycharge", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeletePaycharge(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeletePaycharge/" + rowId);
			return await response.ValidateResponse();
		}


		async public Task<List<Refcharge>> GetRefchargeList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetRefchargeList?" + query);
			var list = await response.Content.ReadAsAsync<IEnumerable<Refcharge>>();
			return list.ToList();
		}
		async public Task<Refcharge> GetRefcharge(Guid? rowId)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetRefcharge?id=" + rowId);
			var rez = await response.Content.ReadAsAsync<Refcharge>();
			return rez;
		}
		async public Task<UpdateReturn> PutRefcharge(Refcharge entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutRefcharge", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostRefcharge(Refcharge entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostRefcharge", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteRefcharge(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeleteRefcharge/" + rowId);
			return await response.ValidateResponse();
		}


		async public Task<WorkInout> GetWorkInout(Guid? rowId)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetWorkInout?id=" + rowId);
			var rez = await response.Content.ReadAsAsync<WorkInout>();
			return rez;
		}
		async public Task<List<WorkInout>> GetWorkInoutList(string query)
		{
			var _client = new MyHttpClient();
			var response = await _client.GetResponse(_baseUrl, "api/patients/GetWorkInoutList?" + query);
			var rez = await response.Content.ReadAsAsync<List<WorkInout>>();
			return rez;
		}
		async public Task<UpdateReturn> PutWorkInout(WorkInout entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/patients/PutWorkInout", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> PostWorkInout(WorkInout entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entity);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/patients/PostWorkInout", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteWorkInout(Guid rowId)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/patients/DeleteWorkInout/" + rowId);
			return await response.ValidateResponse();
		}


	}
}
