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
    
    public partial class ChargeoutPaycharge
    {
        public System.Guid RowId { get; set; }
        public System.Guid ChargeoutRowId { get; set; }
        public System.Guid PaychargeRowId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public System.DateTime AllocationDate { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
    
        public virtual PaychargeV Paycharge { get; set; }
        public virtual ChargeoutV Chargeout { get; set; }
        public virtual PaychargeT PaychargeT { get; set; }
        public virtual ChargeoutT ChargeoutT { get; set; }
    }
}
