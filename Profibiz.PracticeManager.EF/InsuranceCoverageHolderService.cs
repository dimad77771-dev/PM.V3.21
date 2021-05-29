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
    
    public partial class InsuranceCoverageHolderService
    {
        public System.Guid RowId { get; set; }
        public System.Guid InsuranceCoverageHolderRowId { get; set; }
        public System.Guid InsuranceCoverageServiceRowId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<decimal> AnnualAmountCovered { get; set; }
        public Nullable<decimal> PercentageCovered { get; set; }
        public Nullable<decimal> HourlyRateCap { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
        public bool IsPrescriptionRequired { get; set; }
        public Nullable<decimal> PerVisitCost { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
    
        public virtual InsuranceCoverageHolder InsuranceCoverageHolder { get; set; }
        public virtual InsuranceCoverageService InsuranceCoverageService { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
        public virtual ServiceProvider ServiceProvider1 { get; set; }
    }
}
