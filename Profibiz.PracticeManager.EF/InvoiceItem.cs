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
    
    public partial class InvoiceItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InvoiceItem()
        {
            this.Inventories = new HashSet<InventoryT>();
            this.ChargeoutItems = new HashSet<ChargeoutItem>();
        }
    
        public System.Guid RowId { get; set; }
        public System.Guid InvoiceRowId { get; set; }
        public Nullable<System.Guid> AppointmentRowId { get; set; }
        public Nullable<System.Guid> ServcieOrSupplyRowId { get; set; }
        public Nullable<decimal> Units { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> ItemDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
    
        public virtual AppointmentT Appointment_ { get; set; }
        public virtual MedicalServicesOrSupply MedicalServicesOrSupply { get; set; }
        public virtual InvoiceT Invoice { get; set; }
        public virtual AppointmentV Appointment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryT> Inventories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChargeoutItem> ChargeoutItems { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
        public virtual ServiceProvider ServiceProvider1 { get; set; }
    }
}
