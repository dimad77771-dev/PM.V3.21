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
    
    public partial class Form
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Form()
        {
            this.AppointmentForms = new HashSet<AppointmentForm>();
            this.FormItems = new HashSet<FormItem>();
            this.PatientForms = new HashSet<PatientForm>();
        }
    
        public System.Guid RowId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
        public Nullable<System.Guid> CategoryRowId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentForm> AppointmentForms { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FormItem> FormItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientForm> PatientForms { get; set; }
    }
}
