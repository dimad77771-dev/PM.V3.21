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
    
    public partial class PayrollPaymentAllocationV
    {
        public System.Guid RowId { get; set; }
        public System.Guid PayrollPaymentRowId { get; set; }
        public System.DateTime PeriodStart { get; set; }
        public System.DateTime PeriodFinish { get; set; }
        public decimal Amount { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
    
        public virtual PayrollPaymentV PayrollPayment { get; set; }
    }
}
