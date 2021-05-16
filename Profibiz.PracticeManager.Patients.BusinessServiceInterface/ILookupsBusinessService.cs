using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profibiz.PracticeManager.Model;

namespace Profibiz.PracticeManager.Patients.BusinessServiceInterface
{
    public interface ILookupsBusinessService
	{
		Task<List<InsuranceProvider>> GetInsuranceProviders();
		Task<List<MedicalServicesOrSupply>> GetMedicalServicesOrSupplies();
		Task<List<ProfessionalAssociation>> GetProfessionalAssociations();
		Task<List<AppointmentBook>> GetAppointmentBooks();
		Task<List<ThirdPartyServiceProvider>> GetThirdPartyServiceProviders();
		Task UpdateAllLookups();
		Task<T> RunTaskAndUpdateAllLookups<T>(Task<T> task);

		Task<UpdateReturn> PutMedicalServicesOrSupplies(IEnumerable<MedicalServicesOrSupply> entities);
		Task<UpdateReturn> DeleteMedicalServicesOrSupply(MedicalServicesOrSupply entity);

		Task<UpdateReturn> PutCategories(IEnumerable<Category> entities);
		Task<UpdateReturn> DeleteCategory(Category entity);

		Task<UpdateReturn> PutInsuranceProviders(IEnumerable<InsuranceProvider> entities);
		Task<UpdateReturn> DeleteInsuranceProvider(InsuranceProvider entity);

		Task<UpdateReturn> PutProfessionalAssociations(IEnumerable<ProfessionalAssociation> entities);
		Task<UpdateReturn> DeleteProfessionalAssociation(ProfessionalAssociation entity);

		Task<UpdateReturn> PutAppointmentBooks(IEnumerable<AppointmentBook> entities);
		Task<UpdateReturn> DeleteAppointmentBook(AppointmentBook entity);

		Task<UpdateReturn> PutAppointmentStatuses(IEnumerable<AppointmentStatus> entities);
		Task<UpdateReturn> DeleteAppointmentStatus(AppointmentStatus entity);

		Task<UpdateReturn> PutPatientNoteStatuses(IEnumerable<PatientNoteStatus> entities);
		Task<UpdateReturn> DeletePatientNoteStatus(PatientNoteStatus entity);

		Task<UpdateReturn> PutCalendarEventStatuses(IEnumerable<CalendarEventStatus> entities);
		Task<UpdateReturn> DeleteCalendarEventStatus(CalendarEventStatus entity);

		Task<UpdateReturn> PutPublicHolidays(IEnumerable<PublicHoliday> entities);
		Task<UpdateReturn> DeletePublicHoliday(PublicHoliday entity);

		Task<UpdateReturn> PutInvoiceStatuses(IEnumerable<InvoiceStatus> entities);
		Task<UpdateReturn> DeleteInvoiceStatus(InvoiceStatus entity);

		Task<UpdateReturn> PutChargeoutStatuses(IEnumerable<ChargeoutStatus> entities);
		Task<UpdateReturn> DeleteChargeoutStatus(ChargeoutStatus entity);

		Task<UpdateReturn> PutChargeoutRecipientes(IEnumerable<ChargeoutRecipient> entities);
		Task<UpdateReturn> DeleteChargeoutRecipient(ChargeoutRecipient entity);

		Task<UpdateReturn> PutThirdPartyServiceProviders(IEnumerable<ThirdPartyServiceProvider> entities);
		Task<UpdateReturn> DeleteThirdPartyServiceProvider(ThirdPartyServiceProvider entity);

		Task<UpdateReturn> PutReferrers(IEnumerable<Referrer> entities);
		Task<UpdateReturn> DeleteReferrer(Referrer entity);

		Task<UpdateReturn> PutUsers(IEnumerable<User> entities);
		Task<UpdateReturn> DeleteUser(User entity);

		Task<UpdateReturn> PutSuppliers(IEnumerable<Supplier> entities);
		Task<UpdateReturn> DeleteSupplier(Supplier entity);

		Task<List<InsuranceProvidersViewGroup>> GetInsuranceProvidersViewGroups();
		Task<UpdateReturn> PutInsuranceProvidersViewGroups(IEnumerable<InsuranceProvidersViewGroup> entities);

		Task<UserSetting> GetUserSettings(String userCode);
		Task<UpdateReturn> PostUserSettings(UserSetting userSetting);

		Task<LoginInfo> GetLoginInfo(string name, string password);
	}
}
