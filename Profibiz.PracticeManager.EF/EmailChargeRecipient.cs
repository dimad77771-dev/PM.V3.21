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
    
    public partial class EmailChargeRecipient
    {
        public System.Guid RowId { get; set; }
        public System.Guid EmailChargeRowId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Nullable<System.Guid> ServiceProviderRowId { get; set; }
        public Nullable<System.Guid> ChargeoutRecipientRowId { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
    
        public virtual EmailChargeT EmailCharge { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
        public virtual ChargeoutRecipient ChargeoutRecipient { get; set; }
    }
}
