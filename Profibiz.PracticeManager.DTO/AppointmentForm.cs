using System;
using System.Collections.Generic;

namespace Profibiz.PracticeManager.DTO
{
    public class AppointmentForm
    {
        public AppointmentForm()
        {
        }
    
        public Guid RowId { get; set; }
        public Guid? FormRowId { get; set; }
        public DateTime? Date { get; set; }
        public Guid? AppointmentRowId { get; set; }
        public bool? IsComplete { get; set; }
    }
}
