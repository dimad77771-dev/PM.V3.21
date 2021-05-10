using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
    public class MedicalCondition
    {
        public Guid RowId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

    }
}
