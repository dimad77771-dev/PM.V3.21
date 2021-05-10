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
	public partial class InsuranceCoverageService
	{
		public InsuranceCoverageService()
		{
			this.InsuranceCoverageHolderServices = new ObservableCollection<InsuranceCoverageHolderService>();
		}

		public Guid RowId { get; set; }
		public Guid InsuranceCoverageRowId { get; set; }
		public Guid CategoryRowId { get; set; }
		public Boolean CoversAllHolders { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public Decimal? AnnualAmountCovered { get; set; }
		public Decimal? PercentageCovered { get; set; }
		public Decimal? HourlyRateCap { get; set; }
		public bool IsPrescriptionRequired { get; set; }
		public Decimal? PerVisitCost { get; set; }
		public String CreatedBy { get; set; }
		public DateTime? CreatedDateTime { get; set; }
		public String UpdatedBy { get; set; }
		public DateTime? UpdatedDateTime { get; set; }

		public virtual ObservableCollection<InsuranceCoverageHolderService> InsuranceCoverageHolderServices { get; set; }
		public virtual InsuranceCoverage InsuranceCoverages { get; set; }
		public virtual MedicalServicesOrSupply MedicalServicesOrSupplies { get; set; }


		public const decimal NO_LIMIT = -1M; 
	}
}
