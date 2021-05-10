using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class InsuranceArticleV
	{
		public Guid InsuranceCoverageRowId { get; set; }
		public bool CoversAllHolders { get; set; }
		public Guid InsuranceCoverageItemRowId { get; set; }
		public Guid InsuranceCoverageItemHolderRowId { get; set; }
		public decimal? AnnualAmountCovered { get; set; }
		public decimal? MaximumQuantity { get; set; }
		public string CategoryInfo { get; set; }
		public string PatientInfo { get; set; }
		public string ClaimInfo { get; set; }
		public Guid ViewRowId { get; set; }

		public InsuranceCoverage InsuranceCoverage { get; set; }
	}
}