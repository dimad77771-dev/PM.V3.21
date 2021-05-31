using System;
using System.Collections.Generic;

namespace Profibiz.PracticeManager.DTO
{
    public partial class AppointmentFormItem
    {
        public Guid RowId { get; set; }
        public Guid? AppointmentFormRowId { get; set; }
        public Guid? FormItemRowId { get; set; }
        public DateTime? Date { get; set; }
        public string ValueText { get; set; }
        public DateTime? ValueDateTime { get; set; }
        public bool? ValueBoolean { get; set; }
        public decimal? ValueNumeric { get; set; }
    }
}
