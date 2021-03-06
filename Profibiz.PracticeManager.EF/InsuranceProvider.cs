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
    
    public partial class InsuranceProvider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InsuranceProvider()
        {
            this.InsuranceCoverages = new HashSet<InsuranceCoverage>();
            this.InsuranceProvidersViewGroupMappings = new HashSet<InsuranceProvidersViewGroupMapping>();
            this.AppointmentInsuranceProviders = new HashSet<AppointmentInsuranceProvider>();
        }
    
        public System.Guid RowId { get; set; }
        public string Code { get; set; }
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Fax { get; set; }
        public string WebSite { get; set; }
        public string Notes { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InsuranceCoverage> InsuranceCoverages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InsuranceProvidersViewGroupMapping> InsuranceProvidersViewGroupMappings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentInsuranceProvider> AppointmentInsuranceProviders { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
        public virtual ServiceProvider ServiceProvider1 { get; set; }
    }
}
