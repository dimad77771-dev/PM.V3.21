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
    
    public partial class PaychargeRefchargeV
    {
        public System.Guid RowId { get; set; }
        public System.Guid PaychargeRowId { get; set; }
        public System.Guid RefchargeRowId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public System.DateTime AllocationDate { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
    
        public virtual RefchargeV Refcharge { get; set; }
        public virtual PaychargeV Paycharge { get; set; }
    }
}
