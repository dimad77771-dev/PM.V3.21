using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Patients.BusinessService
{
	using Infrastructure;
	using Model;
	using Newtonsoft.Json;
	using System.Configuration;
	using System.Diagnostics;
	using static BusinessServiceHelper;

	public class LookupsBusinessService : ILookupsBusinessService
	{
		private string _baseUrl = ConfigurationManager.AppSettings["service.url"];
		//private const string _baseUrl = @"https://omega.profibiz.com/PM/";

		async public Task<UserSetting> GetUserSettings(String userCode)
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetUserSettings?userCode=" + userCode);
			var list = await response.Content.ReadAsAsync<UserSetting>();
			return list;
		}
		async public Task<List<Category>> GetCategories()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetCategories");
			var list = await response.Content.ReadAsAsync<IEnumerable<Category>>();
			return list.ToList();
		}
		async public Task<List<AppointmentStatus>> GetAppointmentStatuses()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetAppointmentStatuses");
			var list = await response.Content.ReadAsAsync<IEnumerable<AppointmentStatus>>();
			return list.ToList();
		}
		async public Task<List<PatientNoteStatus>> GetPatientNoteStatuses()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetPatientNoteStatuses");
			var list = await response.Content.ReadAsAsync<IEnumerable<PatientNoteStatus>>();
			return list.ToList();
		}
		async public Task<List<CalendarEventStatus>> GetCalendarEventStatuses()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetCalendarEventStatuses");
			var list = await response.Content.ReadAsAsync<IEnumerable<CalendarEventStatus>>();
			return list.ToList();
		}
		async public Task<List<PublicHoliday>> GetPublicHolidays()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetPublicHolidays");
			var list = await response.Content.ReadAsAsync<IEnumerable<PublicHoliday>>();
			return list.ToList();
		}
		async public Task<List<InvoiceStatus>> GetInvoiceStatuses()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetInvoiceStatuses");
			var list = await response.Content.ReadAsAsync<IEnumerable<InvoiceStatus>>();
			return list.ToList();
		}
		async public Task<List<ChargeoutStatus>> GetChargeoutStatuses()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetChargeoutStatuses");
			var list = await response.Content.ReadAsAsync<IEnumerable<ChargeoutStatus>>();
			return list.ToList();
		}
		async public Task<List<ChargeoutRecipient>> GetChargeoutRecipientes()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetChargeoutRecipientes");
			var list = await response.Content.ReadAsAsync<IEnumerable<ChargeoutRecipient>>();
			return list.ToList();
		}
		async public Task<List<Template>> GetTemplates()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetTemplates");
			var list = await response.Content.ReadAsAsync<IEnumerable<Template>>();
			return list.ToList();
		}
		async public Task<List<string>> GetOntarioCities()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetOntarioCities");
			var list = await response.Content.ReadAsAsync<IEnumerable<string>>();
			return list.ToList();
		}
		async public Task<List<InsuranceProvider>> GetInsuranceProviders()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetInsuranceProviders");
			var patients = await response.Content.ReadAsAsync<IEnumerable<InsuranceProvider>>();
			return patients.ToList();
		}
		async public Task<List<MedicalServicesOrSupply>> GetMedicalServicesOrSupplies()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetMedicalServicesOrSupplies");
			var patients = await response.Content.ReadAsAsync<IEnumerable<MedicalServicesOrSupply>>();
			return patients.ToList();
		}
		async public Task<List<ProfessionalAssociation>> GetProfessionalAssociations()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetProfessionalAssociations");
			var patients = await response.Content.ReadAsAsync<IEnumerable<ProfessionalAssociation>>();
			return patients.ToList();
		}
		async public Task<List<ThirdPartyServiceProvider>> GetThirdPartyServiceProviders()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetThirdPartyServiceProviders");
			var patients = await response.Content.ReadAsAsync<IEnumerable<ThirdPartyServiceProvider>>();
			return patients.ToList();
		}
		async public Task<List<Referrer>> GetReferrers()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetReferrers");
			var patients = await response.Content.ReadAsAsync<IEnumerable<Referrer>>();
			return patients.ToList();
		}
		async public Task<List<User>> GetUsers()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetUsers");
			var patients = await response.Content.ReadAsAsync<IEnumerable<User>>();
			return patients.ToList();
		}
		async public Task<List<Supplier>> GetSuppliers()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetSuppliers");
			var patients = await response.Content.ReadAsAsync<IEnumerable<Supplier>>();
			return patients.ToList();
		}
		async public Task<List<AppointmentBook>> GetAppointmentBooks()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetAppointmentBooks");
			var patients = await response.Content.ReadAsAsync<IEnumerable<AppointmentBook>>();
			return patients.ToList();
		}
		async public Task<List<ServiceProvider>> GetServiceProviders()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/patients/GetServiceProviderList");
			var patients = await response.Content.ReadAsAsync<IEnumerable<ServiceProvider>>();
			return patients.ToList();
		}


		async public Task<UpdateReturn> PutMedicalServicesOrSupplies(IEnumerable<MedicalServicesOrSupply> entities)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entities);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/lookups/PutMedicalServicesOrSupplies", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteMedicalServicesOrSupply(MedicalServicesOrSupply entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/lookups/DeleteMedicalServicesOrSupply/" + entity.RowId);
			return await response.ValidateResponse();
		}

		async public Task<UpdateReturn> PutCategories(IEnumerable<Category> entities)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entities);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/lookups/PutCategories", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteCategory(Category entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/lookups/DeleteCategory/" + entity.RowId);
			return await response.ValidateResponse();
		}



		async public Task<UpdateReturn> PutInsuranceProviders(IEnumerable<InsuranceProvider> entities)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entities);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/lookups/PutInsuranceProviders", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteInsuranceProvider(InsuranceProvider entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/lookups/DeleteInsuranceProvider/" + entity.RowId);
			return await response.ValidateResponse();
		}



		async public Task<UpdateReturn> PutProfessionalAssociations(IEnumerable<ProfessionalAssociation> entities)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entities);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/lookups/PutProfessionalAssociations", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteProfessionalAssociation(ProfessionalAssociation entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/lookups/DeleteProfessionalAssociation/" + entity.RowId);
			return await response.ValidateResponse();
		}

		async public Task<UpdateReturn> PutThirdPartyServiceProviders(IEnumerable<ThirdPartyServiceProvider> entities)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entities);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/lookups/PutThirdPartyServiceProviders", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteThirdPartyServiceProvider(ThirdPartyServiceProvider entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/lookups/DeleteThirdPartyServiceProvider/" + entity.RowId);
			return await response.ValidateResponse();
		}


		async public Task<UpdateReturn> PutReferrers(IEnumerable<Referrer> entities)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entities);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/lookups/PutReferrers", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteReferrer(Referrer entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/lookups/DeleteReferrer/" + entity.RowId);
			return await response.ValidateResponse();
		}

		async public Task<UpdateReturn> PutUsers(IEnumerable<User> entities)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entities);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/lookups/PutUsers", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteUser(User entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/lookups/DeleteUser/" + entity.RowId);
			return await response.ValidateResponse();
		}


		async public Task<UpdateReturn> PutSuppliers(IEnumerable<Supplier> entities)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entities);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/lookups/PutSuppliers", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteSupplier(Supplier entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/lookups/DeleteSupplier/" + entity.RowId);
			return await response.ValidateResponse();
		}


		async public Task<UpdateReturn> PutAppointmentBooks(IEnumerable<AppointmentBook> entities)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entities);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/lookups/PutAppointmentBooks", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteAppointmentBook(AppointmentBook entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/lookups/DeleteAppointmentBook/" + entity.RowId);
			return await response.ValidateResponse();
		}

		async public Task<UpdateReturn> PutAppointmentStatuses(IEnumerable<AppointmentStatus> entities)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entities);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/lookups/PutAppointmentStatuses", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteAppointmentStatus(AppointmentStatus entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/lookups/DeleteAppointmentStatus/" + entity.RowId);
			return await response.ValidateResponse();
		}


		async public Task<UpdateReturn> PutPatientNoteStatuses(IEnumerable<PatientNoteStatus> entities)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entities);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/lookups/PutPatientNoteStatuses", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeletePatientNoteStatus(PatientNoteStatus entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/lookups/DeletePatientNoteStatus/" + entity.RowId);
			return await response.ValidateResponse();
		}


		async public Task<UpdateReturn> PutCalendarEventStatuses(IEnumerable<CalendarEventStatus> entities)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entities);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/lookups/PutCalendarEventStatuses", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteCalendarEventStatus(CalendarEventStatus entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/lookups/DeleteCalendarEventStatus/" + entity.RowId);
			return await response.ValidateResponse();
		}


		async public Task<UpdateReturn> PutPublicHolidays(IEnumerable<PublicHoliday> entities)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entities);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/lookups/PutPublicHolidays", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeletePublicHoliday(PublicHoliday entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/lookups/DeletePublicHoliday/" + entity.RowId);
			return await response.ValidateResponse();
		}

		async public Task<UpdateReturn> PutInvoiceStatuses(IEnumerable<InvoiceStatus> entities)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entities);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/lookups/PutInvoiceStatuses", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteInvoiceStatus(InvoiceStatus entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/lookups/DeleteInvoiceStatus/" + entity.RowId);
			return await response.ValidateResponse();
		}

		async public Task<UpdateReturn> PutChargeoutStatuses(IEnumerable<ChargeoutStatus> entities)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entities);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/lookups/PutChargeoutStatuses", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteChargeoutStatus(ChargeoutStatus entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/lookups/DeleteChargeoutStatus/" + entity.RowId);
			return await response.ValidateResponse();
		}

		async public Task<UpdateReturn> PutChargeoutRecipientes(IEnumerable<ChargeoutRecipient> entities)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entities);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/lookups/PutChargeoutRecipientes", content);
			return await response.ValidateResponse();
		}
		async public Task<UpdateReturn> DeleteChargeoutRecipient(ChargeoutRecipient entity)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await _client.DeleteAsync("api/lookups/DeleteChargeoutRecipient/" + entity.RowId);
			return await response.ValidateResponse();
		}


		async public Task<List<InsuranceProvidersViewGroup>> GetInsuranceProvidersViewGroups()
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetInsuranceProvidersViewGroups");
			var patients = await response.Content.ReadAsAsync<IEnumerable<InsuranceProvidersViewGroup>>();
			return patients.ToList();
		}
		async public Task<UpdateReturn> PutInsuranceProvidersViewGroups(IEnumerable<InsuranceProvidersViewGroup> entities)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(entities);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync("api/lookups/PutInsuranceProvidersViewGroups", content);
			return await response.ValidateResponse();
		}


		async public Task<UpdateReturn> PostUserSettings(UserSetting userSetting)
		{
			var _client = new MyHttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(userSetting);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/lookups/PostUserSettings", content);
			return await response.ValidateResponse();
		}

		async public Task<LoginInfo> GetLoginInfo(string name, string password)
		{
			var client = new MyHttpClient();
			var response = await client.GetResponse(_baseUrl, "api/lookups/GetLoginInfo?name=" + Uri.EscapeUriString(name) + "&password=" + Uri.EscapeUriString(password));
			var result = await response.Content.ReadAsAsync<LoginInfo>();
			return result;
		}


		async public Task UpdateAllLookups()
		{
			var taskCategories = GetCategories();
			var taskAppointmentStatuses = GetAppointmentStatuses();
			var taskPatientNoteStatuses = GetPatientNoteStatuses();
			var taskCalendarEventStatuses = GetCalendarEventStatuses();
			var taskPublicHolidays = GetPublicHolidays();
			var taskInvoiceStatuses = GetInvoiceStatuses();
			var taskChargeoutStatuses = GetChargeoutStatuses();
			var taskChargeoutRecipientes = GetChargeoutRecipientes();
			var taskInsuranceProviders = GetInsuranceProviders();
			var taskMedicalServicesOrSupplies = GetMedicalServicesOrSupplies();
			var taskProfessionalAssociations = GetProfessionalAssociations();
			var taskAppointmentBooks = GetAppointmentBooks();
			var taskServiceProviders = GetServiceProviders();
			var taskInsuranceProvidersViewGroups = GetInsuranceProvidersViewGroups();
			var taskThirdPartyServiceProviders = GetThirdPartyServiceProviders();
			var taskReferrers = GetReferrers();
			var taskUsers = GetUsers();
			var taskSuppliers = GetSuppliers();
			var taskTemplates = GetTemplates();
			var taskOntarioCities = GetOntarioCities();
			var taskAll = Task.WhenAll(
					taskCategories, taskAppointmentStatuses, taskPatientNoteStatuses, taskCalendarEventStatuses, taskPublicHolidays, 
					taskInvoiceStatuses, taskChargeoutStatuses, taskChargeoutRecipientes,
					taskInsuranceProviders, taskMedicalServicesOrSupplies, 
					taskProfessionalAssociations, taskAppointmentBooks, taskTemplates, 
					taskServiceProviders, taskInsuranceProvidersViewGroups, taskThirdPartyServiceProviders, 
					taskReferrers, taskSuppliers, taskOntarioCities, taskUsers);
			await taskAll;

			var lookupDataProvider = LookupDataProvider.Instance;
			lookupDataProvider.UpdateCategories(taskCategories.Result);
			lookupDataProvider.UpdateAppointmentStatuses(taskAppointmentStatuses.Result);
			lookupDataProvider.UpdatePatientNoteStatuses(taskPatientNoteStatuses.Result);
			lookupDataProvider.UpdateCalendarEventStatuses(taskCalendarEventStatuses.Result);
			lookupDataProvider.UpdatePublicHolidays(taskPublicHolidays.Result);
			lookupDataProvider.UpdateInvoiceStatuses(taskInvoiceStatuses.Result);
			lookupDataProvider.UpdateChargeoutStatuses(taskChargeoutStatuses.Result);
			lookupDataProvider.UpdateChargeoutRecipientes(taskChargeoutRecipientes.Result);
			lookupDataProvider.UpdateInsuranceProviders(taskInsuranceProviders.Result);
			lookupDataProvider.UpdateMedicalServices(taskMedicalServicesOrSupplies.Result);
			lookupDataProvider.UpdateProfessionalAssociations(taskProfessionalAssociations.Result);
			lookupDataProvider.UpdateAppointmentBooks(taskAppointmentBooks.Result);
			lookupDataProvider.UpdateServiceProviders(taskServiceProviders.Result);
			lookupDataProvider.UpdateThirdPartyServiceProviders(taskThirdPartyServiceProviders.Result);
			lookupDataProvider.UpdateReferrers(taskReferrers.Result);
			lookupDataProvider.UpdateUsers(taskUsers.Result);
			lookupDataProvider.UpdateSuppliers(taskSuppliers.Result);
			lookupDataProvider.UpdateInsuranceProvidersViewGroups(taskInsuranceProvidersViewGroups.Result);
			lookupDataProvider.UpdateTemplates(taskTemplates.Result);
			lookupDataProvider.UpdateOntarioCities(taskOntarioCities.Result);
		}

		async public Task<T> RunTaskAndUpdateAllLookups<T>(Task<T> task)
		{
			await Task.WhenAll(task, UpdateAllLookups());
			return task.Result;
		}
	}
}
