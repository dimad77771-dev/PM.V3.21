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
    
    public partial class InsuranceCoverage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InsuranceCoverage()
        {
            this.InsuranceCoverageHolders = new HashSet<InsuranceCoverageHolder>();
            this.InsuranceCoverageServices = new HashSet<InsuranceCoverageService>();
            this.Appointments = new HashSet<AppointmentT>();
            this.InsuranceCoverageItems = new HashSet<InsuranceCoverageItem>();
        }
    
        public System.Guid RowId { get; set; }
        public System.Guid InsuranceProviderRowId { get; set; }
        public string PolicyNumber { get; set; }
        public System.DateTime CoverageStartDate { get; set; }
        public System.DateTime CoverageValidUntil { get; set; }
        public Nullable<bool> IsForAllListed { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
        public string PlanNumber { get; set; }
        public string DivisionNumber { get; set; }
        public string ID { get; set; }
        public string InsuranceCoverageYearType { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InsuranceCoverageHolder> InsuranceCoverageHolders { get; set; }
        public virtual InsuranceProvider InsuranceProvider { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InsuranceCoverageService> InsuranceCoverageServices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentT> Appointments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InsuranceCoverageItem> InsuranceCoverageItems { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
