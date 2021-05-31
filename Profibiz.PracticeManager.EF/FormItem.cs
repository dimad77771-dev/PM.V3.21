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
    
    public partial class FormItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FormItem()
        {
            this.AppointmentFormItems = new HashSet<AppointmentFormItem>();
            this.PatientFormItems = new HashSet<PatientFormItem>();
        }
    
        public System.Guid RowId { get; set; }
        public Nullable<System.Guid> FormRowId { get; set; }
        public string Code { get; set; }
        public Nullable<int> SectionOrder { get; set; }
        public string SectionName { get; set; }
        public string Name { get; set; }
        public Nullable<int> OrderInSection { get; set; }
        public Nullable<bool> IsDetailsRequired { get; set; }
        public string DetailsLabel { get; set; }
        public Nullable<bool> IsBoolean { get; set; }
        public Nullable<bool> IsDateTime { get; set; }
        public Nullable<bool> IsText { get; set; }
        public Nullable<bool> IsNumeric { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentFormItem> AppointmentFormItems { get; set; }
        public virtual Form Form { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientFormItem> PatientFormItems { get; set; }
    }
}
