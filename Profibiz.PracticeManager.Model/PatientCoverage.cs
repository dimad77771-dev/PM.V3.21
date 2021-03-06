using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class PatientCoverage
	{
		public Guid RowId { get; set; }
		public Guid PatientRowId { get; set; }
		public Guid MedicalServiceOrSupplyRowId { get; set; }
		public Guid InsuranceProviderRowId { get; set; }
		public DateTime CoverageStartDate { get; set; }
		public DateTime CoverageValidUntil { get; set; }
		public decimal? PercentageCovered { get; set; }
		public decimal? HourlyRateCap { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedDateTime { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? UpdatedDateTime { get; set; }
		public Nullable<decimal> AnnualAmountCovered { get; set; }

		public virtual InsuranceProvider InsuranceProviders { get; set; }
		public virtual MedicalServicesOrSupply MedicalServicesOrSupplies { get; set; }
		public virtual Patient Patients { get; set; }


		public bool IsChanged { get; set; }
	}
}
