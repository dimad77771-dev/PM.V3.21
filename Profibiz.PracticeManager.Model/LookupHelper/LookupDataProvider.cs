using Profibiz.PracticeManager.Model;
using PropertyChanged;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Profibiz.PracticeManager.Infrastructure;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class LookupDataProvider
	{
		public static LookupDataProvider Instance { get; private set; }

		public LookupDataProvider()
		{
			Instance = this;
		}

		public ObservableCollection<InsuranceProvider> InsuranceProviders { get; set; }
		public ObservableCollection<MedicalServicesOrSupply> MedicalServices { get; set; }
		public ObservableCollection<ProfessionalAssociation> ProfessionalAssociations { get; set; }
		public ObservableCollection<Setting> Settings { get; set; }
		public ObservableCollection<ThirdPartyServiceProvider> ThirdPartyServiceProviders { get; set; }
		public ObservableCollection<Referrer> Referrers { get; set; }
		public ObservableCollection<User> Users { get; set; }
		public ObservableCollection<Supplier> Suppliers { get; set; }
		public ObservableCollection<AppointmentBook> AppointmentBooks { get; set; }
		public ObservableCollection<ServiceProvider> ServiceProviders { get; set; }
		public ObservableCollection<ServiceProvider> ServiceProvidersEx { get; set; }
		public ObservableCollection<InsuranceProvidersViewGroup> InsuranceProvidersViewGroups { get; set; }
		public ObservableCollection<Category> Categories { get; set; }
		public ObservableCollection<AppointmentStatus> AppointmentStatuses { get; set; }
		public ObservableCollection<PatientNoteStatus> PatientNoteStatuses { get; set; }
		public ObservableCollection<CalendarEventStatus> CalendarEventStatuses { get; set; }
		public ObservableCollection<PublicHoliday> PublicHolidays { get; set; }
		public ObservableCollection<InvoiceStatus> InvoiceStatuses { get; set; }
		public ObservableCollection<ChargeoutStatus> ChargeoutStatuses { get; set; }
		public ObservableCollection<ChargeoutRecipient> ChargeoutRecipientes { get; set; }
		public ObservableCollection<Template> Templates { get; set; }


		public List<MedicalServicesOrSupply> MedicalServices_Service => MedicalServicesForItemType(TypeHelper.MedicalItemType.Service);
		public List<MedicalServicesOrSupply> MedicalServices_Supply => MedicalServicesForItemType(TypeHelper.MedicalItemType.Supply);
		public List<MedicalServicesOrSupply> MedicalServices_ThirdPartyService => MedicalServicesForItemType(TypeHelper.MedicalItemType.ThirdPartyService);


		public List<Template> Templates_Appointment => Templates.Where(q => q.InvoiceType == "Appointment").ToList();

		public List<ServiceProvider> ServiceProviders_ThirdParty => ServiceProvidersForItemType(TypeHelper.ServiceProviderServiceType.ThirdParty);

		List<MedicalServicesOrSupply> MedicalServicesForItemType(string itemType)
		{
			return MedicalServices == null ? null : MedicalServices.Where(q => q.ItemType == itemType).ToList();
		}

		List<ServiceProvider> ServiceProvidersForItemType(string serviceType)
		{
			return ServiceProviders == null ? null : ServiceProviders.Where(q => q.ServiceType == serviceType).ToList();
		}

		public void UpdateInsuranceProviders(IEnumerable<InsuranceProvider> newData)
		{
			InsuranceProviders = new ObservableCollection<InsuranceProvider>(newData.OrderBy(q => q.CompanyName));
		}

		public void UpdateMedicalServices(IEnumerable<MedicalServicesOrSupply> newData)
		{
			MedicalServices = new ObservableCollection<MedicalServicesOrSupply>(newData.OrderBy(q => q.Name));
		}

		public void UpdateProfessionalAssociations(IEnumerable<ProfessionalAssociation> newData)
		{
			ProfessionalAssociations = new ObservableCollection<ProfessionalAssociation>(newData.OrderBy(q => q.Name));
		}

		public void UpdateSettings(IEnumerable<Setting> newData)
		{
			Settings = new ObservableCollection<Setting>(newData.OrderBy(q => q.Name));
		}

		public void UpdateThirdPartyServiceProviders(IEnumerable<ThirdPartyServiceProvider> newData)
		{
			ThirdPartyServiceProviders = new ObservableCollection<ThirdPartyServiceProvider>(newData.OrderBy(q => q.Name));
		}

		public void UpdateReferrers(IEnumerable<Referrer> newData)
		{
			Referrers = new ObservableCollection<Referrer>(newData.OrderBy(q => q.Name));
		}

		public void UpdateUsers(IEnumerable<User> newData)
		{
			Users = new ObservableCollection<User>(newData.OrderBy(q => q.Name));
		}

		public void UpdateSuppliers(IEnumerable<Supplier> newData)
		{
			Suppliers = new ObservableCollection<Supplier>(newData.OrderBy(q => q.Name));
		}

		public void UpdateCategories(IEnumerable<Category> newData)
		{
			Categories = new ObservableCollection<Category>(newData.OrderBy(q => q.Name));
		}

		public void UpdateAppointmentStatuses(IEnumerable<AppointmentStatus> newData)
		{
			AppointmentStatuses = new ObservableCollection<AppointmentStatus>(newData.OrderBy(q => q.DisplayOrder));
		}

		public void UpdatePatientNoteStatuses(IEnumerable<PatientNoteStatus> newData)
		{
			PatientNoteStatuses = new ObservableCollection<PatientNoteStatus>(newData.OrderBy(q => q.DisplayOrder));
		}


		public void UpdateCalendarEventStatuses(IEnumerable<CalendarEventStatus> newData)
		{
			CalendarEventStatuses = new ObservableCollection<CalendarEventStatus>(newData.OrderBy(q => q.DisplayOrder));
		}


		public void UpdatePublicHolidays(IEnumerable<PublicHoliday> newData)
		{
			PublicHolidays = new ObservableCollection<PublicHoliday>(newData.OrderBy(q => q.HolidayDate));
		}



		public void UpdateInvoiceStatuses(IEnumerable<InvoiceStatus> newData)
		{
			InvoiceStatuses = new ObservableCollection<InvoiceStatus>(newData.OrderBy(q => q.DisplayOrder));
		}

		public void UpdateChargeoutStatuses(IEnumerable<ChargeoutStatus> newData)
		{
			ChargeoutStatuses = new ObservableCollection<ChargeoutStatus>(newData.OrderBy(q => q.DisplayOrder));
		}

		public void UpdateChargeoutRecipientes(IEnumerable<ChargeoutRecipient> newData)
		{
			ChargeoutRecipientes = new ObservableCollection<ChargeoutRecipient>(newData.OrderBy(q => q.DisplayOrder));
		}


		public void UpdateAppointmentBooks(IEnumerable<AppointmentBook> newData)
		{
			AppointmentBooks = new ObservableCollection<AppointmentBook>(newData.OrderBy(q => q.DisplayOrder));
		}

		public void UpdateServiceProviders(IEnumerable<ServiceProvider> newData)
		{
			ServiceProviders = new ObservableCollection<ServiceProvider>(newData.OrderBy(q => q.FullName));
			//ServiceProviders.ToList().ForEach(q => UpdateServiceProviderMedicalServicesOrSupplies(q));
			ServiceProvidersEx = new ObservableCollection<ServiceProvider>(ServiceProviders);
			ServiceProvidersEx.Add(new ServiceProvider
			{
				RowId = ServiceProvider.NO_DOCTOR,
				FirstName = ServiceProvider.NO_DOCTOR_NAME,
				AppointmentBackgroundColor = ServiceProvider.NO_DOCTOR_NAME_BACKGROUND_COLOR,
				AppointmentForegroundColor = ServiceProvider.NO_DOCTOR_NAME_FOREGROUND_COLOR,
			});
		}

		//void UpdateServiceProviderMedicalServicesOrSupplies(ServiceProvider row)
		//{
		//	var mrows = (row.ServicesPrivided ?? "").Split('/').Select(q => MedicalServices.FirstOrDefault(z => z.Code == q)).Where(q => q != null);
		//	row.MedicalServicesOrSupplies = new ObservableCollection<MedicalServicesOrSupply>(mrows);
		//}

		public void UpdateInsuranceProvidersViewGroups(IEnumerable<InsuranceProvidersViewGroup> newData)
		{
			InsuranceProvidersViewGroups = new ObservableCollection<InsuranceProvidersViewGroup>(newData.OrderBy(q => q.Name));
			InsuranceProvidersViewGroups.ToList().ForEach(
				q => q.InsuranceProvidersViewGroupMappings.ToList().ForEach(
					z => z.InsuranceProvider = FindInsuranceProvider(z.InsuranceProviderRowId)));
		}

		public void UpdateTemplates(IEnumerable<Template> newData)
		{
			Templates = new ObservableCollection<Template>(newData.OrderBy(q => q.Name));
		}


		public static ProfessionalAssociation FindProfessionalAssociation(Guid rowId)
		{
			return Instance.ProfessionalAssociations.SingleOrDefault(q => q.RowId == rowId);
		}

		public static Setting FindSetting(Guid rowId)
		{
			return Instance.Settings.SingleOrDefault(q => q.RowId == rowId);
		}


		public static ThirdPartyServiceProvider FindThirdPartyServiceProvider(Guid rowId)
		{
			return Instance.ThirdPartyServiceProviders.SingleOrDefault(q => q.RowId == rowId);
		}

		public static Referrer FindReferrer(Guid rowId)
		{
			return Instance.Referrers.SingleOrDefault(q => q.RowId == rowId);
		}

		public static User FindUser(Guid rowId)
		{
			return Instance.Users.SingleOrDefault(q => q.RowId == rowId);
		}

		public static Supplier FindSupplier(Guid rowId)
		{
			return Instance.Suppliers.SingleOrDefault(q => q.RowId == rowId);
		}

		public static AppointmentBook FindAppointmentBook(Guid rowId)
		{
			return Instance.AppointmentBooks.SingleOrDefault(q => q.RowId == rowId);
		}
        public static String AppointmentBook2Name(Guid rowId)
        {
            return Instance.AppointmentBooks.SingleOrDefault(q => q.RowId == rowId)?.Name;
        }

		public static IEnumerable<InsuranceProvider> InsuranceProvidersByRowIds(IEnumerable<Guid?> rowIds)
		{
			return Instance.InsuranceProviders.Where(q => rowIds.Contains((Guid?)q.RowId));
		}



		public static InsuranceProvider FindInsuranceProvider(Guid? rowId)
		{
			return Instance.InsuranceProviders.SingleOrDefault(q => q.RowId == rowId);
		}
		public static String Insurance2Code(Guid? rowId)
		{
			return Instance.InsuranceProviders.SingleOrDefault(q => q.RowId == rowId)?.Code;
		}

		public static MedicalServicesOrSupply FindMedicalService(Guid? rowId)
		{
			return Instance.MedicalServices.SingleOrDefault(q => q.RowId == rowId);
		}
        public static String MedicalService2Name(Guid? rowId)
        {
            return Instance.MedicalServices.SingleOrDefault(q => q.RowId == rowId)?.FullName;
        }
		public static String MedicalService2FullNameWithPrintLabel(Guid? rowId)
		{
			return Instance.MedicalServices.SingleOrDefault(q => q.RowId == rowId)?.FullNameWithPrintLabel;
		}
		public static String InvoiceStatus2Name(Guid? rowId)
		{
			return Instance.InvoiceStatuses.SingleOrDefault(q => q.RowId == rowId)?.Name;
		}
		public static String ChargeoutStatus2Name(Guid? rowId)
		{
			return Instance.ChargeoutStatuses.SingleOrDefault(q => q.RowId == rowId)?.Name;
		}
		public static String ChargeoutRecipient2Name(Guid? rowId)
		{
			return Instance.ChargeoutRecipientes.SingleOrDefault(q => q.RowId == rowId)?.Name;
		}
		public static Guid? MedicalService2CategoryRowId(Guid? rowId)
		{
			return Instance.MedicalServices.SingleOrDefault(q => q.RowId == rowId)?.CategoryRowId;
		}
		public static bool MedicalService2IsSupply(Guid? rowId)
		{
			var categoryRowId = Instance.MedicalServices.SingleOrDefault(q => q.RowId == rowId)?.CategoryRowId;
			return (Instance.Categories.SingleOrDefault(q => q.RowId == categoryRowId).CategoryType == TypeHelper.CategoryType.Supply);
		}

		public static Category FindCategory(Guid? rowId)
		{
			return Instance.Categories.SingleOrDefault(q => q.RowId == rowId);
		}

		public static Template FindTemplate(Guid? rowId)
		{
			return Instance.Templates.SingleOrDefault(q => q.RowId == rowId);
		}


		public static AppointmentStatus FindAppointmentStatus(Guid? rowId)
		{
			return Instance.AppointmentStatuses.SingleOrDefault(q => q.RowId == rowId);
		}

		public static PatientNoteStatus FindPatientNoteStatus(Guid? rowId)
		{
			return Instance.PatientNoteStatuses.SingleOrDefault(q => q.RowId == rowId);
		}

		public static CalendarEventStatus FindCalendarEventStatus(Guid? rowId)
		{
			return Instance.CalendarEventStatuses.SingleOrDefault(q => q.RowId == rowId);
		}

		public static InvoiceStatus FindInvoiceStatus(Guid? rowId)
		{
			return Instance.InvoiceStatuses.SingleOrDefault(q => q.RowId == rowId);
		}

		public static ChargeoutStatus FindChargeoutStatus(Guid? rowId)
		{
			return Instance.ChargeoutStatuses.SingleOrDefault(q => q.RowId == rowId);
		}

		public static ChargeoutRecipient FindChargeoutRecipient(Guid? rowId)
		{
			return Instance.ChargeoutRecipientes.SingleOrDefault(q => q.RowId == rowId);
		}

		public static PublicHoliday FindPublicHoliday(Guid? rowId)
		{
			return Instance.PublicHolidays.SingleOrDefault(q => q.RowId == rowId);
		}

		public static Boolean IsPublicHoliday(DateTime date)
		{
			return Instance.PublicHolidays.Any(q => q.HolidayDate == date);
		}
		public static PublicHoliday GetPublicHoliday(DateTime date)
		{
			return Instance.PublicHolidays.SingleOrDefault(q => q.HolidayDate == date);
		}


		public static ServiceProvider FindServiceProvider(Guid? rowId)
		{
			return Instance.ServiceProviders.SingleOrDefault(q => q.RowId == rowId);
		}
		public static ServiceProvider FindServiceProviderEx(Guid rowId)
		{
			return Instance.ServiceProvidersEx.SingleOrDefault(q => q.RowId == rowId);
		}
        public static String ServiceProvider2Name(Guid? rowId)
        {
			if (Instance.ServiceProviders == null) return "";
			return Instance.ServiceProviders.SingleOrDefault(q => q.RowId == rowId)?.FullName;
        }

		public static String Category2Name(Guid? rowId)
		{
			if (Instance.ServiceProviders == null) return "";
			return Instance.Categories.SingleOrDefault(q => q.RowId == rowId)?.FullName;
		}

		public static String Referrer2Name(Guid? rowId)
		{
			if (Instance.Referrers == null) return "";
			return Instance.Referrers.SingleOrDefault(q => q.RowId == rowId)?.Name;
		}

		public static String User2Name(Guid? rowId)
		{
			if (Instance.Users == null) return "";
			return Instance.Users.SingleOrDefault(q => q.RowId == rowId)?.Name;
		}

		public static String Referrer2Address(Guid? rowId)
		{
			if (Instance.Referrers == null) return "";
			return Instance.Referrers.SingleOrDefault(q => q.RowId == rowId)?.GetAddress();
		}

		public static String User2Address(Guid? rowId)
		{
			if (Instance.Users == null) return "";
			return Instance.Users.SingleOrDefault(q => q.RowId == rowId)?.GetAddress();
		}


		public static String Supplier2Name(Guid? rowId)
		{
			if (Instance.Suppliers == null) return "";
			return Instance.Suppliers.SingleOrDefault(q => q.RowId == rowId)?.Name;
		}

		public IEnumerable<String> Sexes
		{
			get
			{
				return new[] { TypeHelper.Sex.Male, TypeHelper.Sex.Female, TypeHelper.Sex.Other };
			}
		}
		public IEnumerable<String> AddressToUse
		{
			get
			{
				return new[] { TypeHelper.AddressToUse.FirstAddres, TypeHelper.AddressToUse.SecondAddress, TypeHelper.AddressToUse.Email };
			}
		}
		public IEnumerable<String> FamilyMemberType
		{
			get
			{
				return new[] { TypeHelper.FamilyMemberType.Head, TypeHelper.FamilyMemberType.Member };
			}
		}
		public IEnumerable<String> MedicalItemType
		{
			get
			{
				return new[] { TypeHelper.MedicalItemType.Service, TypeHelper.MedicalItemType.Supply, TypeHelper.MedicalItemType.ThirdPartyService };
			}
		}
		public IEnumerable<String> CategoryType
		{
			get
			{
				return new[] { TypeHelper.CategoryType.Service, TypeHelper.CategoryType.Supply };
			}
		}

		public IEnumerable<String> ChargeModels
		{
			get
			{
				return new[] { TypeHelper.ChargeModel.PerHour, TypeHelper.ChargeModel.PerVisit };
			}
		}
		public IEnumerable<String> InsuranceCoverageYearTypes
		{
			get
			{
				return new[] { TypeHelper.InsuranceCoverageYearType.CalendarYear, TypeHelper.InsuranceCoverageYearType.BeneficialYear, TypeHelper.InsuranceCoverageYearType.AcademicYear };
			}
		}
		public IEnumerable<String> ServiceProviderServiceTypes
		{
			get
			{
				return new[] { TypeHelper.ServiceProviderServiceType.InHouse, TypeHelper.ServiceProviderServiceType.ThirdParty };
			}
		}
		public IEnumerable<String> ServiceProviderEmploymentTypes
		{
			get
			{
				return new[] { TypeHelper.ServiceProviderEmploymentType.Service, TypeHelper.ServiceProviderEmploymentType.Payg};
			}
		}





		public static IEnumerable<String> RelationToFamilyHeadVariants
		{
			get
			{
				return new[] { "Mother", "Father", "Daughter", "Son", "Sister", "Brother", "Spouse", "Other" };
			}
		}

		public static IEnumerable<String> Provinces
		{
			get
			{
				return new[] { "AB", "BC", "MB", "NB", "NL", "NT", "NS", "NU", "ON", "PE", "QC", "SK", "YT" };
			}
		}
		public static string PROVINCE_ONTARIO => "ON";

		public static IEnumerable<String> PatientTitle
		{
			get
			{
				return new[] { "", "Dr", "Master", "Miss", "Mr", "Mrs", "Ms", "Prof" };
			}
		}

        public static IEnumerable<String> FormStatuses

        {

            get
            {
                return new[] { "", "SENT", "FILED", "OK" };
            }
        }

		
		public static IEnumerable<String> PatientDocumentTypes
		{
			get
			{
				return new[] { "Presciption", "Doctor's Letter", "Referral Letter", "Other" };
			}
		}


		public static IEnumerable<String> PaymentTypes
		{
			get
			{
				return new[] { "Cash", "Cheque", "American Express", "MasterCard", "Visa", "Direct Payment", "PayPal", "E-Transfer", "Interac Debit", "Other Credit Card" };
			}
		}
        private static IEnumerable<String> _citiesInOntario;
        public static IEnumerable<String> CitiesInOntario
		{
			get
			{
                /*
				return new[] 
				{
                    "Ajax", "Aurora", "Bolton", "Bowmanville", "Brampton", "Brampton", "Brock",    "Burlington",   "Caledon",
                    "Clarington",   "Concord",  "East Gwillimbury", "Etobicoke", "Georgina", "Gwillinbury", "Halton Hills",
                    "King", "Kitchener",  "Maple", "Markham", "Milton", "Mississauga", "Newmarket",
                    "North York", "Oakville", "Oshawa", "Pickering", "Richmond Hill",  "Roches Point", "Scarborough",
                    "Scugog", "Stouffville", "Thornhill", "Toronto", "Uxbridge", "Vaughan",  "Waterdown", "Whitby",
                    "Whitchurch-Stouffville", "Woodbridge",
                };
                */

                return _citiesInOntario;
            }
		}

        public void UpdateOntarioCities(IEnumerable<string> newData)
        {
            _citiesInOntario = newData;
        } 
		public static IEnumerable<String> Province2Cities(string province)
		{
			if (province == PROVINCE_ONTARIO)
			{
				return CitiesInOntario;
			}
			else
			{
				return new string[0];
			}
		}


		public IEnumerable<YesNoClass> YesNo
		{
			get
			{
				return new[] 
				{
					new YesNoClass { Value = true, Name = "Yes" },
					new YesNoClass { Value = false, Name = "No" },
				};
			}
		}
		public class YesNoClass
		{
			public bool Value { get; set; }
			public string Name { get; set; }
		}

		public IEnumerable<YesNoClass10> YesNo10
		{
			get
			{
				return new[]
				{
					new YesNoClass10 { Value = 1, Name = "1" },
					new YesNoClass10 { Value = 2, Name = "2" },
					new YesNoClass10 { Value = 3, Name = "3" },
					new YesNoClass10 { Value = 4, Name = "4" },
					new YesNoClass10 { Value = 5, Name = "5" },
					new YesNoClass10 { Value = 6, Name = "6" },
					new YesNoClass10 { Value = 7, Name = "7" },
					new YesNoClass10 { Value = 8, Name = "8" },
					new YesNoClass10 { Value = 9, Name = "9" },
					new YesNoClass10 { Value = 10, Name = "10" },
				};
			}
		}
		public class YesNoClass10
		{
			public int? Value { get; set; }
			public string Name { get; set; }
		}

		public IEnumerable<HeatIceClass> HeatIceEnum
		{
			get
			{
				return new[]
				{
					new HeatIceClass { Value = 1, Name = "Heat" },
					new HeatIceClass { Value = 2, Name = "Ice" },
				};
			}
		}
		public class HeatIceClass
		{
			public int? Value { get; set; }
			public string Name { get; set; }
		}


		public EventCalendarRemainderModel[] EventCalendarRemainderEnum => EventCalendarRemainderInfo.All;
		public EventCalendarSnoozedModel[] EventCalendarSnoozedEnum => EventCalendarSnoozedInfo.All;
		public AppointmentRemainderEnumModel[] AppointmentRemainderEnumAll => AppointmentRemainderEnumInfo.All;


	}
}
