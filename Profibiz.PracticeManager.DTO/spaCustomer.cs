﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class spaCustomer
	{
		public int CustomerNumber { get; set; }
		public string MembershipId { get; set; }
		public string MembershipNumber { get; set; }
		public bool Undelete { get; set; }
		public bool Hide { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public string Phone1 { get; set; }
		public string Extention1 { get; set; }
		public string Phone2 { get; set; }
		public string Extention2 { get; set; }
		public string Phone3 { get; set; }
		public string PhoneType1 { get; set; }
		public string PhoneType2 { get; set; }
		public string PhoneType3 { get; set; }
		public Nullable<int> Visits { get; set; }
		public Nullable<System.DateTime> LastVisit { get; set; }
		public string Notes { get; set; }
		public string EMail { get; set; }
		public string User1 { get; set; }
		public string User2 { get; set; }
		public string User3 { get; set; }
		public string User4 { get; set; }
		public bool Account { get; set; }
		public Nullable<System.DateTime> Entered { get; set; }
		public Nullable<int> LastLocation { get; set; }
		public Nullable<short> BirthDate_Day { get; set; }
		public Nullable<short> BirthDate_Month { get; set; }
		public Nullable<short> BirthDate_Year { get; set; }
		public string LastStaff { get; set; }
		public string Referenced { get; set; }
		public Nullable<short> Gender { get; set; }
		public bool HideHistory { get; set; }
		public string Address { get; set; }
		public bool Tax1Exempt { get; set; }
		public bool Tax2Exempt { get; set; }
		public bool Tax3Exempt { get; set; }
		public bool Tax4Exempt { get; set; }
		public Nullable<bool> NotesPopUp { get; set; }
		public Nullable<int> PreferredStaff { get; set; }
		public string CustomerPicB { get; set; }
		public string CustomerPicA { get; set; }
		public string CreditCardNumber { get; set; }
		public string CCExpiryDate { get; set; }
		public string Extention3 { get; set; }
		public bool ALead { get; set; }
		public bool AConsultation { get; set; }
		public Nullable<int> NextMileStone { get; set; }
		public string SMSProvider { get; set; }
		public decimal CCType { get; set; }
		public bool BDInUse { get; set; }
		public string CustTag { get; set; }
		public string CreditCardAuth { get; set; }
		public string SMSNumber { get; set; }
		public Nullable<bool> SendText { get; set; }
		public Nullable<bool> SendEmail { get; set; }
		public Nullable<int> CreatedBy { get; set; }
		public Nullable<int> ModifiedBy { get; set; }
		public Nullable<System.DateTime> Modified { get; set; }
		public Nullable<int> Height { get; set; }
		public Nullable<int> Weight { get; set; }
		public string MiddleName { get; set; }
		public string Country { get; set; }
		public string Ethnicity { get; set; }
		public Nullable<int> MartialStatus { get; set; }
		public string SSN { get; set; }
		public Nullable<int> BloodType { get; set; }
		public string EmergencyContactName { get; set; }
		public string EmergencyContactPhone { get; set; }
		public string EmergencyContactRelationship { get; set; }
		public string EmploymentOccupation { get; set; }
		public string EmploymentOrganization { get; set; }
		public Nullable<int> EmploymentStatus { get; set; }
		public Nullable<System.Guid> EditSequence { get; set; }
		public Nullable<int> CreditCardTypeID { get; set; }
		public string MirrorClientID { get; set; }
		public string Password { get; set; }
		public int OnlineStatus { get; set; }
		public Nullable<System.Guid> OnlineActivationCode { get; set; }
		public string QBID { get; set; }
		public short SMSCall { get; set; }
		public short AutoCall { get; set; }
		public bool UseAutoCall { get; set; }
		public Nullable<System.Guid> PrimaryInsuranceId { get; set; }
		public Nullable<System.Guid> SecondaryInsuranceId { get; set; }
		public Nullable<System.Guid> DefaultPictureID { get; set; }
		public int DefaultLocationId { get; set; }
		public string FullName { get; set; }
	}
}
