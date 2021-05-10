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
    
    public partial class AppointmentBook
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AppointmentBook()
        {
            this.Appointments = new HashSet<AppointmentT>();
            this.ServiceProviders = new HashSet<ServiceProvider>();
        }
    
        public System.Guid RowId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public System.DateTime StartAt { get; set; }
        public System.DateTime FinishAt { get; set; }
        public int Interval { get; set; }
        public string AppointmentBackgroundColor { get; set; }
        public string AppointmentForegroundColor { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentT> Appointments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceProvider> ServiceProviders { get; set; }
    }
}
