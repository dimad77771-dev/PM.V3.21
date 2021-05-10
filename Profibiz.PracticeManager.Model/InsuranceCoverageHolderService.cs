using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public partial class InsuranceCoverageHolderService
	{
		public Guid RowId { get; set; }
		public Guid InsuranceCoverageHolderRowId { get; set; }
		public Guid InsuranceCoverageServiceRowId { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public Decimal? AnnualAmountCovered { get; set; }
		public Decimal? PercentageCovered { get; set; }
		public Decimal? HourlyRateCap { get; set; }
		public bool IsPrescriptionRequired { get; set; }
		public Decimal? PerVisitCost { get; set; }
		public string CreatedBy { get; set; }
		public System.DateTime? CreatedDateTime { get; set; }
		public string UpdatedBy { get; set; }
		public System.DateTime? UpdatedDateTime { get; set; }

		public virtual InsuranceCoverageHolder InsuranceCoverageHolders { get; set; }
		public virtual InsuranceCoverageService InsuranceCoverageServices { get; set; }
	}
}
