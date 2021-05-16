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
    
    public partial class CalendarEventV
    {
        public System.Guid RowId { get; set; }
        public System.DateTime Start { get; set; }
        public System.DateTime Finish { get; set; }
        public string Notes { get; set; }
        public Nullable<System.Guid> PatientRowId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string RefNumber { get; set; }
        public string RefStatus { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
        public bool Completed { get; set; }
        public Nullable<System.Guid> Status1RowId { get; set; }
        public Nullable<System.Guid> Status2RowId { get; set; }
        public string PatientFullName { get; set; }
        public bool AllDay { get; set; }
        public int RemainderInMinutes { get; set; }
        public bool IsDisabled { get; set; }
        public Nullable<System.DateTime> SnoozedTo { get; set; }
        public bool IsVacation { get; set; }
        public Nullable<System.Guid> ServiceProviderRowId { get; set; }
        public string ServiceProviderFullName { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
    
        public virtual PatientV Patient { get; set; }
    }
}
