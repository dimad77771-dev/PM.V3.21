using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
    public class PatientsListView
    {
        public Guid RowId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Sex { get; set; }
        public string FamilyMemberType { get; set; }
        public Guid FamilyHeadRowId { get; set; }
        public string EmailAddress { get; set; }
        public string HomePhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public byte[] Photo { get; set; }
        public string ReferrerName { get; set; }
        public string PrimaryPolicies { get; set; }
        public string SecondaryPolicies { get; set; }
		public string FamilyDoctor { get; set; }
		public decimal? InvoiceFamilyBalance { get; set; }
		public decimal Rate { get; set; }
		public bool HasNoCoverage { get; set; }
		public bool IsNotRegistered { get; set; }
		public int? spaCustomerNumber { get; set; }
	}
}
