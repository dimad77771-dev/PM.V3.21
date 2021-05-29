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
    
    public partial class AppointmentRemainder
    {
        public System.Guid RowId { get; set; }
        public System.Guid AppointmentRowId { get; set; }
        public int RemainderInMinutes { get; set; }
        public bool IsProcessedEmail { get; set; }
        public Nullable<System.DateTime> ProcessedEmailTime { get; set; }
        public bool IsProcessedSms { get; set; }
        public Nullable<System.DateTime> ProcessedSmsTime { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
    
        public virtual AppointmentT AppointmentT { get; set; }
        public virtual AppointmentV AppointmentV { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
        public virtual ServiceProvider ServiceProvider1 { get; set; }
    }
}
