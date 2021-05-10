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
	public class InsuranceCoverageItem
	{
		public InsuranceCoverageItem()
		{
		}

		public Guid RowId { get; set; }
		public Guid InsuranceCoverageRowId { get; set; }
		public bool CoversAllHolders { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public decimal? AnnualAmountCovered { get; set; }
		public decimal? PercentageCovered { get; set; }
		public decimal? HourlyRateCap { get; set; }
		public bool IsPrescriptionRequired { get; set; }
		public decimal? PerVisitCost { get; set; }
		public int? MaximumVisits { get; set; }
		public decimal? MaximumQuantity { get; set; }
		public string AdditionalInformation { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedDateTime { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? UpdatedDateTime { get; set; }

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }
	}
}
