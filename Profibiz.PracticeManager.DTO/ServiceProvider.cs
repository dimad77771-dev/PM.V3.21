using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
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
		public Guid? AppointmentBookRowId { get; set; }

		public Guid? AssociationRowId { get; set; }
		public string RegistrationNumber { get; set; }
		public DateTime? RegistrationDate { get; set; }
		public DateTime? RegistrationExpiryDate { get; set; }
		public string AssociationCode { get; set; }
		public string AssociationName { get; set; }
		public string EmailAddress { get; set; }
		public string AppointmentBackgroundColor { get; set; }
		public string AppointmentForegroundColor { get; set; }
		public string Position { get; set; }
        public string FooterText { get; set; }
        public string HeaderText { get; set; }

        public Nullable<decimal> Rate { get; set; }
		public Nullable<decimal> TaxRate { get; set; }
		public string AssosiationsList { get; set; }
		public int MaximumDayAppointments { get; set; }

		public decimal DoctorRate { get; set; }
		public string ServiceType { get; set; }
		public string EmploymentType { get; set; }
		public string FullName { get; set; }

		public string Username { get; set; }
		public string Password { get; set; }
		public Guid? RoleRowId { get; set; }
		public bool IsOfficeEmployee { get; set; }
		public byte[] Signature { get; set; }

		public List<ServiceProviderAssociation> ServiceProviderAssociations { get; set; }
		public List<ServiceProviderService> ServiceProviderServices { get; set; }
	}
}
