using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
    public class InsuranceCoverageHolder
    {
        public System.Guid RowId { get; set; }
        public System.Guid InsuranceCoverageRowId { get; set; }
        public System.Guid PolicyHolderRowId { get; set; }
        public string PolicyHolderType { get; set; }

        public InsuranceCoverage InsuranceCoverage { get; set; }
        public Patient Patient { get; set; }
        public IEnumerable<InsuranceCoverageHolderService> InsuranceCoverageHolderServices { get; set; }
    }
}
