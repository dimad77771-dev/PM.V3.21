using Profibiz.PracticeManager.Infrastructure;
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
	public class InsuranceCoverage
	{
		public InsuranceCoverage()
		{
			this.InsuranceCoverageHolders = new ObservableCollection<InsuranceCoverageHolder>();
			this.InsuranceCoverageServices = new ObservableCollection<InsuranceCoverageService>();
			this.InsuranceCoverageHolderServices = new ObservableCollection<InsuranceCoverageHolderService>();
		}

		public Guid RowId { get; set; }
		public Guid InsuranceProviderRowId { get; set; }
		public String PolicyNumber { get; set; }
		public DateTime CoverageStartDate { get; set; }
		public DateTime CoverageValidUntil { get; set; }
		public Boolean IsForAllListed { get; set; }
		public String PlanNumber { get; set; }
		public String DivisionNumber { get; set; }
		public String ID { get; set; }
		public String InsuranceCoverageYearType { get; set; }
		public String CreatedBy { get; set; }
		public DateTime? CreateDateTime { get; set; }
		public String UpdatedBy { get; set; }
		public DateTime? UpdatedDateTime { get; set; }

		public virtual ObservableCollection<InsuranceCoverageHolder> InsuranceCoverageHolders { get; set; }
		public virtual ObservableCollection<InsuranceCoverageService> InsuranceCoverageServices { get; set; }
		public virtual ObservableCollection<InsuranceCoverageHolderService> InsuranceCoverageHolderServices { get; set; }

		public List<InsuranceCoverageItem> InsuranceCoverageItems { get; set; } = new List<InsuranceCoverageItem>();
		public List<InsuranceCoverageItemCategory> InsuranceCoverageItemCategories { get; set; } = new List<InsuranceCoverageItemCategory>();
		public List<InsuranceCoverageItemHolder> InsuranceCoverageItemHolders { get; set; } = new List<InsuranceCoverageItemHolder>();


		public Patient PolicyOwner { get; set; }
		public Guid? PolicyOwnerRowId { get; set; }
		public String PolicyOwnerFullName { get; set; }

		public InsuranceProvider InsuranceProvider => LookupDataProvider.FindInsuranceProvider(InsuranceProviderRowId);
		public String InsuranceProviderCode => LookupDataProvider.FindInsuranceProvider(InsuranceProviderRowId)?.Code;

		public Boolean IsOnlyForOtherFamilyMember { get; set; }

		public String PolicyFullName => GetPolicyFullName(InsuranceProviderRowId, PolicyOwnerFullName, PolicyNumber, CoverageStartDate, CoverageValidUntil);



		public bool IsChanged { get; set; }

		public static String GetPolicyFullName(Guid? insuranceProviderRowId, string policyOwnerFullName, string policyNumber, DateTime CoverageStartDate, DateTime CoverageValidUntil)
		{
			return insuranceProviderRowId == null ? "" : 
				LookupDataProvider.Insurance2Code(insuranceProviderRowId) +  " - " + policyOwnerFullName + " - " + policyNumber 
					+ " (" + CoverageStartDate.FormatShortYYDate() + "-" + CoverageValidUntil.FormatShortYYDate() +  ")";
		}

		public static Boolean InCoverageIntarval(DateTime? date, DateTime coverageStartDate, DateTime coverageValidUntil)
		{
			if (date == null) return false;
			if (date == default(DateTime)) return false;

			if (date.Value >= coverageStartDate && date.Value < coverageValidUntil.AddDays(1))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static Boolean InCoverageIntarvalArray(IEnumerable<DateTime> dates, DateTime coverageStartDate, DateTime coverageValidUntil)
		{
			return dates.Any(q => InCoverageIntarval(q, coverageStartDate, coverageValidUntil));
		}
	}
}
