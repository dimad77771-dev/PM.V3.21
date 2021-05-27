using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class Patient
	{
        public Guid RowId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PreferredName { get; set; }
        public Guid FamilyHeadRowId { get; set; }
        public string FamilyMemberType { get; set; }
        public string RelationToFamilyHead { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Sex { get; set; }
        public string CardNo { get; set; }
		public DateTime? FirstSeen { get; set; }
		public Guid? SendInvoicesToFamilyMember { get; set; }
        public string Address1 { get; set; }
        public string Province1 { get; set; }
        public string City1 { get; set; }
        public string Postcode1 { get; set; }
        public string Address2 { get; set; }
        public string Province2 { get; set; }
        public string City2 { get; set; }
        public string Postcode2 { get; set; }
        public string AddressToUse { get; set; }
        public string HomePhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Occupation { get; set; }
        public string EmployerName { get; set; }
        public string WorkPhone { get; set; }
        public string Fax { get; set; }
        public string EmailAddress { get; set; }
        public string FamilyDoctor { get; set; }
        public string FamilyDoctorAddress { get; set; }
        public string FamilyDoctorPhoneNumber { get; set; }
		public byte[] Photo { get; set; }
		public bool HasHighBloodPressure { get; set; }
		public bool HasPacemaker { get; set; }
		public bool HasDiabetes { get; set; }
		public bool HasHepatitis { get; set; }
		public bool HasHeadaches { get; set; }
		public bool HasSurgeries { get; set; }
		public bool HasMetalImplants { get; set; }
		public bool HasFractures { get; set; }
		public bool HasNeckPain { get; set; }
		public bool HasBackPain { get; set; }
		public bool HasShoulderElbowHandPain { get; set; }
		public bool HasHipKneeFootPain { get; set; }
		public bool HasShoulderPain { get; set; }
		public bool HasElbowPain { get; set; }
		public bool HasHandPain { get; set; }
		public bool HasHipPain { get; set; }
		public bool HasKneePain { get; set; }
		public bool HasFootPain { get; set; }
		public string OtherMedicalConditions { get; set; }
		public string HealthHistoryNotes { get; set; }
		public Guid? ReferrerRowId { get; set; }
		public bool UseHeadAddress { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
		public decimal Rate { get; set; }
		public bool HasNoCoverage { get; set; }
		public bool IsNotRegistered { get; set; }

		public List<Patient> FamilyMembers { get; set; }
		public Patient FamilyHead { get; set; }
		public List<InsuranceCoverage> InsuranceCoverages { get; set; }
		public List<PatientNote> PatientNotes { get; set; }
		public List<PatientDocument> PatientDocuments { get; set; }
		public List<Appointment> AppointmentWithClinicalNotes { get; set; }
		public List<Appointment> AppointmentWithTreatmentNotes { get; set; }
		public Appointment PatientFormDocuments { get; set; }

		public ChangeFamilyMemberInfo ChangeFamilyMember { get; set; }
		public class ChangeFamilyMemberInfo
		{
			public ActionEnum Action { get; set; }
			public Guid NewFamilyHeadRowId { get; set; }
			public enum ActionEnum { RemoveFromFamily, MoveMemberToHeader, MoveToAnotherFamily }
		}


		public bool ChangeFamilyMembersAddress { get; set; }
		public bool IgnoreDuplicateLastFirstNameFlag { get; set; }

		public bool IsSelectHeadFamily { get; set; }
		public bool IsSelectUseHeadAddress { get; set; }
	}
}
