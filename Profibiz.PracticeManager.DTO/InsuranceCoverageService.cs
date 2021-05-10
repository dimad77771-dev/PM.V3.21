using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
    public class InsuranceCoverageService
    {
        public Guid RowId { get; set; }
        public Guid InsuranceCoverageRowId { get; set; }
		public Guid CategoryRowId { get; set; }
		public bool CoversAllHolders { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? AnnualAmountCovered { get; set; }
        public decimal? PercentageCovered { get; set; }
        public decimal? HourlyRateCap { get; set; }
		public bool IsPrescriptionRequired { get; set; }
		public decimal? PerVisitCost { get; set; }
		public string CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }

        public IEnumerable<InsuranceCoverageHolderService> InsuranceCoverageHolderServices { get; set; }
        public InsuranceCoverage InsuranceCoverage { get; set; }
        public MedicalServicesOrSupply MedicalServicesOrSupply { get; set; }
    }
}
