//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Profibiz.PracticeManager.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class PatientV
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PatientV()
        {
            this.Invoices = new HashSet<InvoiceV>();
            this.Payments = new HashSet<PaymentV>();
            this.CalendarEvents = new HashSet<CalendarEventV>();
        }
    
        public System.Guid RowId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PreferredName { get; set; }
        public System.Guid FamilyHeadRowId { get; set; }
        public string FamilyMemberType { get; set; }
        public string RelationToFamilyHead { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string Sex { get; set; }
        public string CardNo { get; set; }
        public Nullable<System.DateTime> FirstSeen { get; set; }
        public Nullable<System.Guid> SendInvoicesToFamilyMember { get; set; }
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
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
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
        public string OtherMedicalConditions { get; set; }
        public string HealthHistoryNotes { get; set; }
        public string FullName { get; set; }
        public Nullable<System.Guid> ReferrerRowId { get; set; }
        public bool UseHeadAddress { get; set; }
        public string Source { get; set; }
        public string SourceFullName { get; set; }
        public decimal Rate { get; set; }
        public bool HasShoulderPain { get; set; }
        public bool HasElbowPain { get; set; }
        public bool HasHandPain { get; set; }
        public bool HasHipPain { get; set; }
        public bool HasKneePain { get; set; }
        public bool HasFootPain { get; set; }
        public bool HasNoCoverage { get; set; }
        public int PatientId { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
        public bool IsNotRegistered { get; set; }
        public byte[] Signature { get; set; }
        public string FamilyDoctorEmail { get; set; }
        public string SexIdentifyAs { get; set; }
        public string InsuranceCompany { get; set; }
        public string InsurancePlanMember { get; set; }
        public string InsurancePlanMemberOtherValue { get; set; }
        public string InsurancePolicyNumber { get; set; }
        public string InsuranceCertificateId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceV> Invoices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentV> Payments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CalendarEventV> CalendarEvents { get; set; }
    }
}
