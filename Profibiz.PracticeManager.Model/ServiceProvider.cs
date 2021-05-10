using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class ServiceProvider
	{
		public Guid RowId { get; set; }
		public string Title { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime? BirthDate { get; set; }
		public string AddressLine { get; set; }
		public string Province { get; set; }
		public string City { get; set; }
		public string Postcode { get; set; }
		public string PhoneNumber { get; set; }
		public string MobilePhoneNumber { get; set; }
		public string Qualifications { get; set; }
		public string EmailAddress { get; set; }
		public Guid? AppointmentBookRowId { get; set; }

		public Guid? AssociationRowId { get; set; }
		public string RegistrationNumber { get; set; }
		public DateTime? RegistrationDate { get; set; }
		public DateTime? RegistrationExpiryDate { get; set; }
		public string AssociationCode { get; set; }
		public string AssociationName { get; set; }

		public string AppointmentBackgroundColor { get; set; }
		public string AppointmentForegroundColor { get; set; }
		public string Position { get; set; }

        public string FooterText { get; set; }
        public string HeaderText { get; set; }

		public decimal? Rate { get; set; }
		public decimal? TaxRate { get; set; }
        public string AssosiationsList { get; set; }
		public int MaximumDayAppointments { get; set; }

		public decimal DoctorRate { get; set; }
		public string ServiceType { get; set; }
		public string EmploymentType { get; set; }

		


		public virtual ObservableCollection<ServiceProviderAssociation> ServiceProviderAssociations { get; set; } = new ObservableCollection<ServiceProviderAssociation>();
		public virtual ObservableCollection<ServiceProviderService> ServiceProviderServices { get; set; } = new ObservableCollection<ServiceProviderService>();

		public string FullName
		{
			get
			{
				var ret = (FirstName + " " + LastName)?.Trim();
				if (string.IsNullOrEmpty(ret))
				{
					ret = "<Empty>";
				}
				return ret;
			}
		}

		public string ListAllServicesProvided => string.Join(", ", ServiceProviderServices.Select(q => q?.MedicalService?.FullName));
		public string ListAllCategories => string.Join(", ", ServiceProviderServices.Select(q => q?.Category?.FullName).Distinct().OrderBy(q => q));


		public string Rowtype9 => "-";
		public bool IsChanged { get; set; }


		public static readonly char DAYOFWEEKS_SPLITTER = '/';
		public static readonly string[] DayOfWeekNames = new string[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
		public static readonly DayOfWeek[] DayOfWeeks = new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };


		public static readonly Guid NO_DOCTOR = default(Guid);
		public static readonly String NO_DOCTOR_NAME = "<Doctor not assigned>";
		public static readonly String NO_DOCTOR_NAME_BACKGROUND_COLOR = "#C6EFCE";
		public static readonly String NO_DOCTOR_NAME_FOREGROUND_COLOR = "#006100";
	}
}
