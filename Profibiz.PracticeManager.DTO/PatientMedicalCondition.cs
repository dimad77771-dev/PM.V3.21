using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Profibiz.PracticeManager.DTO
{
    public class PatientMedicalCondition
    {
        public System.Guid RowId { get; set; }
        public System.Guid MedicalConditionRowId { get; set; }
        public System.Guid PatientRowId { get; set; }
        public bool? Value { get; set; }
        public string Note { get; set; }
        public MedicalCondition MedicalCondition { get; set; }
    }
}


