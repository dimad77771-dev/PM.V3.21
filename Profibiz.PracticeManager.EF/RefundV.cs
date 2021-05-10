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
    
    public partial class RefundV
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RefundV()
        {
            this.InvoiceRefunds = new HashSet<InvoiceRefundV>();
            this.PaymentRefunds = new HashSet<PaymentRefundV>();
        }
    
        public System.Guid RowId { get; set; }
        public System.Guid PatientRowId { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string PaymentType { get; set; }
        public string BankName { get; set; }
        public string ChequeNumber { get; set; }
        public string BrunchNumber { get; set; }
        public string TransitNumber { get; set; }
        public string AccountNumber { get; set; }
        public byte[] Image { get; set; }
        public string TransactionId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string PatientFullName { get; set; }
        public string FullDescription { get; set; }
        public string RefundItemsType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceRefundV> InvoiceRefunds { get; set; }
        public virtual PatientV Patient { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentRefundV> PaymentRefunds { get; set; }
    }
}
