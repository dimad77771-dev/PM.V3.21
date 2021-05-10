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
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Newtonsoft.Json;
using System.Text;
using Profibiz.PracticeManager.BL;
using Profibiz.PracticeManager.DTO;

namespace Profibiz.PracticeManager.WebApi.Controllers
{
	public class LookupsController : ApiController
	{
		IWebApiRepository _repository;
		public LookupsController(IWebApiRepository repository)
		{
			_repository = repository;
		}

        public IHttpActionResult GetOntarioCities()
        {
            var result = _repository.GetOntarioCities();
            return Ok(result);

        }
        public IHttpActionResult GetUserSettings(string userCode)
		{
			var result = _repository.GetUserSettings(userCode);
			return Ok(result);
		}

		public IHttpActionResult GetCategories()
		{
            try
            {
                var list = _repository.GetCategories();
                return Ok(list); 
            }
            catch(Exception ex)
            {
                return Ok(ex);
            }
		}

		public IHttpActionResult GetAppointmentStatuses()
		{
			var list = _repository.GetAppointmentStatuses();
			return Ok(list);
		}

		public IHttpActionResult GetCalendarEventStatuses()
		{
			var list = _repository.GetCalendarEventStatuses();
			return Ok(list);
		}

		public IHttpActionResult GetPatientNoteStatuses()
		{
			var list = _repository.GetPatientNoteStatuses();
			return Ok(list);
		}

		public IHttpActionResult GetPublicHolidays()
		{
			var list = _repository.GetPublicHolidays();
			return Ok(list);
		}

		public IHttpActionResult GetInvoiceStatuses()
		{
			var list = _repository.GetInvoiceStatuses();
			return Ok(list);
		}

		public IHttpActionResult GetChargeoutStatuses()
		{
			var list = _repository.GetChargeoutStatuses();
			return Ok(list);
		}

		public IHttpActionResult GetChargeoutRecipientes()
		{
			var list = _repository.GetChargeoutRecipientes();
			return Ok(list);
		}

		public IHttpActionResult GetTemplates()
		{
			var list = _repository.GetTemplates();
			return Ok(list);
		}


		public IHttpActionResult GetInsuranceProviders()
		{
            try
            { 
			var list = _repository.GetInsuranceProviders();
			return Ok(list);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }

		public IHttpActionResult GetMedicalServicesOrSupplies()
		{
			var list = _repository.GetMedicalServicesOrSupplies();
			return Ok(list);
		}

		public IHttpActionResult GetProfessionalAssociations()
		{
			var list = _repository.GetProfessionalAssociations();
			return Ok(list);
		}

		public IHttpActionResult GetThirdPartyServiceProviders()
		{
			var list = _repository.GetThirdPartyServiceProviders();
			return Ok(list);
		}

		public IHttpActionResult GetReferrers()
		{
			var list = _repository.GetReferrers();
			return Ok(list);
		}

		public IHttpActionResult GetSuppliers()
		{
			var list = _repository.GetSuppliers();
			return Ok(list);
		}

		public IHttpActionResult GetAppointmentBooks()
		{
			var entities = _repository.GetAppointmentBooks();
			return Ok(entities);
		}


		public IHttpActionResult PutMedicalServicesOrSupplies([FromBody]IEnumerable<MedicalServicesOrSupply> entities)
		{
			_repository.PutMedicalServicesOrSupplies(entities);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteMedicalServicesOrSupply(Guid id)
		{
			_repository.DeleteMedicalServicesOrSupply(id);
			return StatusCode(HttpStatusCode.NoContent);
		}

		public IHttpActionResult PutCategories([FromBody]IEnumerable<Category> entities)
		{
			_repository.PutCategories(entities);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteCategory(Guid id)
		{
			_repository.DeleteCategory(id);
			return StatusCode(HttpStatusCode.NoContent);
		}


		public IHttpActionResult PutInsuranceProviders([FromBody]IEnumerable<InsuranceProvider> entities)
		{
			_repository.PutInsuranceProviders(entities);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteInsuranceProvider(Guid id)
		{
			_repository.DeleteInsuranceProvider(id);
			return StatusCode(HttpStatusCode.NoContent);
		}

		public IHttpActionResult PutProfessionalAssociations([FromBody]IEnumerable<ProfessionalAssociation> entities)
		{
			_repository.PutProfessionalAssociations(entities);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteProfessionalAssociation(Guid id)
		{
			_repository.DeleteProfessionalAssociation(id);
			return StatusCode(HttpStatusCode.NoContent);
		}


		public IHttpActionResult PutThirdPartyServiceProviders([FromBody]IEnumerable<ThirdPartyServiceProvider> entities)
		{
			_repository.PutThirdPartyServiceProviders(entities);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteThirdPartyServiceProvider(Guid id)
		{
			_repository.DeleteThirdPartyServiceProvider(id);
			return StatusCode(HttpStatusCode.NoContent);
		}


		public IHttpActionResult PutReferrers([FromBody]IEnumerable<Referrer> entities)
		{
			_repository.PutReferrers(entities);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteReferrer(Guid id)
		{
			_repository.DeleteReferrer(id);
			return StatusCode(HttpStatusCode.NoContent);
		}

		public IHttpActionResult PutSuppliers([FromBody]IEnumerable<Supplier> entities)
		{
			_repository.PutSuppliers(entities);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteSupplier(Guid id)
		{
			_repository.DeleteSupplier(id);
			return StatusCode(HttpStatusCode.NoContent);
		}

		public IHttpActionResult PutAppointmentBooks([FromBody]IEnumerable<AppointmentBook> entities)
		{
			_repository.PutAppointmentBooks(entities);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteAppointmentBook(Guid id)
		{
			_repository.DeleteAppointmentBook(id);
			return StatusCode(HttpStatusCode.NoContent);
		}


		public IHttpActionResult PutAppointmentStatuses([FromBody]IEnumerable<AppointmentStatus> entities)
		{
			_repository.PutAppointmentStatuses(entities);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteAppointmentStatus(Guid id)
		{
			_repository.DeleteAppointmentStatus(id);
			return StatusCode(HttpStatusCode.NoContent);
		}



		public IHttpActionResult PutCalendarEventStatuses([FromBody]IEnumerable<CalendarEventStatus> entities)
		{
			_repository.PutCalendarEventStatuses(entities);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteCalendarEventStatus(Guid id)
		{
			_repository.DeleteCalendarEventStatus(id);
			return StatusCode(HttpStatusCode.NoContent);
		}


		public IHttpActionResult PutPatientNoteStatuses([FromBody]IEnumerable<PatientNoteStatus> entities)
		{
			_repository.PutPatientNoteStatuses(entities);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeletePatientNoteStatus(Guid id)
		{
			_repository.DeletePatientNoteStatus(id);
			return StatusCode(HttpStatusCode.NoContent);
		}

		public IHttpActionResult PutPublicHolidays([FromBody]IEnumerable<PublicHoliday> entities)
		{
			_repository.PutPublicHolidays(entities);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeletePublicHoliday(Guid id)
		{
			_repository.DeletePublicHoliday(id);
			return StatusCode(HttpStatusCode.NoContent);
		}

		public IHttpActionResult PutInvoiceStatuses([FromBody]IEnumerable<InvoiceStatus> entities)
		{
			_repository.PutInvoiceStatuses(entities);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteInvoiceStatus(Guid id)
		{
			_repository.DeleteInvoiceStatus(id);
			return StatusCode(HttpStatusCode.NoContent);
		}

		public IHttpActionResult PutChargeoutStatuses([FromBody]IEnumerable<ChargeoutStatus> entities)
		{
			_repository.PutChargeoutStatuses(entities);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteChargeoutStatus(Guid id)
		{
			_repository.DeleteChargeoutStatus(id);
			return StatusCode(HttpStatusCode.NoContent);
		}

		public IHttpActionResult PutChargeoutRecipientes([FromBody]IEnumerable<ChargeoutRecipient> entities)
		{
			_repository.PutChargeoutRecipientes(entities);
			return StatusCode(HttpStatusCode.NoContent);
		}
		public IHttpActionResult DeleteChargeoutRecipient(Guid id)
		{
			_repository.DeleteChargeoutRecipient(id);
			return StatusCode(HttpStatusCode.NoContent);
		}

		public IHttpActionResult GetInsuranceProvidersViewGroups()
		{
			var patient = _repository.GetInsuranceProvidersViewGroups();
			return Ok(patient);
		}
		public IHttpActionResult PutInsuranceProvidersViewGroups([FromBody]IEnumerable<InsuranceProvidersViewGroup> entities)
		{
			_repository.PutInsuranceProvidersViewGroups(entities);
			return StatusCode(HttpStatusCode.NoContent);
		}

		public IHttpActionResult PostErrorToServer([FromBody]ClientError errorInfo)
		{
			_repository.PostErrorToServer(errorInfo);
			return StatusCode(HttpStatusCode.NoContent);
		}

		public IHttpActionResult PostNLogItem([FromBody]NLogItem nlogItem)
		{
			_repository.PostNLogItem(nlogItem);
			return StatusCode(HttpStatusCode.NoContent);
		}

		public IHttpActionResult PostUserSettings([FromBody]UserSetting userSetting)
		{
			_repository.PostUserSettings(userSetting);
			return StatusCode(HttpStatusCode.NoContent);
		}


	}
}