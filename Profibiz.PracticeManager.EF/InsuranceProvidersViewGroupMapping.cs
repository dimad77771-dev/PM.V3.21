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
    
    public partial class InsuranceProvidersViewGroupMapping
    {
        public System.Guid RowId { get; set; }
        public System.Guid InsuranceProvidersViewGroupRowId { get; set; }
        public System.Guid InsuranceProviderRowId { get; set; }
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
    
        public virtual InsuranceProvider InsuranceProvider { get; set; }
        public virtual InsuranceProvidersViewGroup InsuranceProvidersViewGroup { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
