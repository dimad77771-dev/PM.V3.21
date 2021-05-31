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
    
    public partial class AppointmentT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AppointmentT()
        {
            this.AppointmentInsuranceProviders = new HashSet<AppointmentInsuranceProvider>();
            this.InvoiceItems = new HashSet<InvoiceItem>();
            this.AppointmentClinicalNotes = new HashSet<AppointmentClinicalNote>();
            this.AppointmentRemainders = new HashSet<AppointmentRemainder>();
            this.AppointmentTreatmentNotes = new HashSet<AppointmentTreatmentNote>();
            this.FormDocuments = new HashSet<FormDocument>();
            this.AppointmentForms = new HashSet<AppointmentForm>();
        }
    
        public System.Guid RowId { get; set; }
        public System.DateTime Start { get; set; }
        public System.DateTime Finish { get; set; }
        public string Notes { get; set; }
        public System.Guid PatientRowId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string RefNumber { get; set; }
        public string RefStatus { get; set; }
        public System.Guid AppointmentBookRowId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
        public Nullable<System.Guid> ServiceProviderRowId { get; set; }
        public bool Completed { get; set; }
        public Nullable<System.Guid> MedicalServicesOrSupplyRowId { get; set; }
        public Nullable<System.Guid> Status1RowId { get; set; }
        public Nullable<System.Guid> Status2RowId { get; set; }
        public Nullable<System.Guid> InsuranceCoverageRowId { get; set; }
        public bool HasNoCoverage { get; set; }
        public bool IsIgnoreForChargeout { get; set; }
        public bool IsRemainderEmail { get; set; }
        public bool IsRemainderSms { get; set; }
        public string GoogleCalendarEventId { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
        public bool IsEmailWhenRegistered { get; set; }
        public bool IsSmsWhenRegistered { get; set; }
    
        public virtual AppointmentBook AppointmentBook { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentInsuranceProvider> AppointmentInsuranceProviders { get; set; }
        public virtual MedicalServicesOrSupply MedicalServicesOrSupply { get; set; }
        public virtual Patient Patient { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
        public virtual AppointmentStatus AppointmentStatus { get; set; }
        public virtual AppointmentStatus AppointmentStatus1 { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
        public virtual InsuranceCoverage InsuranceCoverage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentClinicalNote> AppointmentClinicalNotes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentRemainder> AppointmentRemainders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentTreatmentNote> AppointmentTreatmentNotes { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FormDocument> FormDocuments { get; set; }
        public virtual ServiceProvider ServiceProvider1 { get; set; }
        public virtual ServiceProvider ServiceProvider2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentForm> AppointmentForms { get; set; }
    }
}
