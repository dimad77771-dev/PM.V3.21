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
    
    public partial class PatientFormItem
    {
        public System.Guid RowId { get; set; }
        public Nullable<System.Guid> PatientFormRowId { get; set; }
        public Nullable<System.Guid> FormItemRowId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string ValueText { get; set; }
        public Nullable<System.DateTime> ValueDateTime { get; set; }
        public Nullable<bool> ValueBoolean { get; set; }
        public Nullable<decimal> ValueNumeric { get; set; }
        public Nullable<System.Guid> Created { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
        public string ValueDetails { get; set; }
    
        public virtual FormItem FormItem { get; set; }
        public virtual PatientForm PatientForm { get; set; }
    }
}
