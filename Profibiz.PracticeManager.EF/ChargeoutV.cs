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
    
    public partial class ChargeoutV
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChargeoutV()
        {
            this.ChargeoutItems = new HashSet<ChargeoutItem>();
            this.ChargeoutPaycharges = new HashSet<ChargeoutPaycharge>();
            this.ChargeoutRefcharges = new HashSet<ChargeoutRefchargeV>();
        }
    
        public System.Guid RowId { get; set; }
        public string ChargeoutType { get; set; }
        public string ChargeoutNumber { get; set; }
        public Nullable<System.DateTime> ChargeoutDate { get; set; }
        public string BillTo { get; set; }
        public string BillToAddress1 { get; set; }
        public string BillToAddress2 { get; set; }
        public string BillToCity { get; set; }
        public string BillToProvince { get; set; }
        public string BillToPostCode { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdateBy { get; set; }
        public string PrintTemplate { get; set; }
        public Nullable<System.Guid> ThirdPartyServiceProviderRowId { get; set; }
        public Nullable<System.Guid> Status1RowId { get; set; }
        public Nullable<System.Guid> Status2RowId { get; set; }
        public decimal Rate { get; set; }
        public bool HasNoCoverage { get; set; }
        public Nullable<System.Guid> ServiceProviderRowId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> PaychargeAmount { get; set; }
        public Nullable<decimal> PaychargeTax { get; set; }
        public Nullable<decimal> RefchargeAmount { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> PaychargeTotal { get; set; }
        public Nullable<decimal> PaychargeRequest { get; set; }
        public string ServiceProvidersList { get; set; }
        public string MedicalServicesList { get; set; }
        public string CategoriesList { get; set; }
        public Nullable<System.DateTime> MaxChargeoutItemDate { get; set; }
        public Nullable<System.DateTime> MinChargeoutItemDate { get; set; }
        public System.Guid ChargeoutRecipientRowId { get; set; }
        public string ChargeoutRecipientName { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChargeoutItem> ChargeoutItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChargeoutPaycharge> ChargeoutPaycharges { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChargeoutRefchargeV> ChargeoutRefcharges { get; set; }
        public virtual ChargeoutRecipient ChargeoutRecipient { get; set; }
    }
}
