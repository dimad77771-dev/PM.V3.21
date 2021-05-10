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
    
    public partial class InvoiceClaimDetail
    {
        public System.Guid RowId { get; set; }
        public System.Guid InvoiceClaimRowId { get; set; }
        public System.Guid ServcieOrSupplyRowId { get; set; }
        public Nullable<decimal> Units { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
    
        public virtual InvoiceClaimT InvoiceClaim { get; set; }
        public virtual MedicalServicesOrSupply MedicalServicesOrSupply { get; set; }
    }
}
