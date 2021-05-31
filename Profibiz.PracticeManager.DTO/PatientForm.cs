using System;
using System.Collections.Generic;

namespace Profibiz.PracticeManager.DTO
{
    public partial class PatientForm
    {
        public PatientForm()
        {
        }
    
        public Guid RowId { get; set; }
        public Guid? PatientRowId { get; set; }
        public Guid? FormRowId { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsComplete { get; set; }
    }
}
