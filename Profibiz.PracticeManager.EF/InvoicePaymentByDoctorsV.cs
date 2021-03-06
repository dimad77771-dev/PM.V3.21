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
    
    public partial class InvoicePaymentByDoctorsV
    {
        public System.Guid InvoicePaymentRowId { get; set; }
        public System.Guid InvoiceRowId { get; set; }
        public System.Guid PaymentRowId { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public Nullable<decimal> AllocateAmount { get; set; }
        public Nullable<decimal> DueToDoctor { get; set; }
        public Nullable<System.Guid> ServiceProviderRowId { get; set; }
    
        public virtual InvoiceV Invoice { get; set; }
        public virtual PaymentV Payment { get; set; }
    }
}
