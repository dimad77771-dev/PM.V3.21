using System;
using System.Collections.Generic;

namespace Profibiz.PracticeManager.DTO
{
    public partial class PatientFormItem
    {
        public Guid RowId { get; set; }
        public Guid? PatientFormRowId { get; set; }
        public Guid? FormItemRowId { get; set; }
        public DateTime? Date { get; set; }
        public string ValueText { get; set; }
        public DateTime? ValueDateTime { get; set; }
        public bool? ValueBoolean { get; set; }
        public decimal? ValueNumeric { get; set; }
        public Guid? Created { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
