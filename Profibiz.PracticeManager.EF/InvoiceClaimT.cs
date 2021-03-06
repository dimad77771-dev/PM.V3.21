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
    
    public partial class InvoiceClaimT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InvoiceClaimT()
        {
            this.InvoiceClaimDetails = new HashSet<InvoiceClaimDetail>();
        }
    
        public System.Guid RowId { get; set; }
        public System.Guid InvoiceRowId { get; set; }
        public Nullable<System.Guid> InsuranceCoverageRowId { get; set; }
        public System.DateTime SentDate { get; set; }
        public decimal SentAmont { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
        public Nullable<decimal> ApproveAmont { get; set; }
        public Nullable<System.Guid> Status1RowId { get; set; }
        public Nullable<System.Guid> Status2RowId { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string Forms { get; set; }
        public bool HasNoCoverage { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceClaimDetail> InvoiceClaimDetails { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
        public virtual ServiceProvider ServiceProvider1 { get; set; }
    }
}
