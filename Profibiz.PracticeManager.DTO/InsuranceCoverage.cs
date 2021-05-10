using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
    public class InsuranceCoverage
    {
        public System.Guid RowId { get; set; }
        public System.Guid InsuranceProviderRowId { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime CoverageStartDate { get; set; }
        public DateTime CoverageValidUntil { get; set; }
        public bool? IsForAllListed { get; set; }
		public string PlanNumber { get; set; }
		public string DivisionNumber { get; set; }
		public string ID { get; set; }
		public string InsuranceCoverageYearType { get; set; }
		public string CreatedBy { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }

		public InsuranceProvider InsuranceProvider { get; set; }
		public List<InsuranceCoverageHolder> InsuranceCoverageHolders { get; set; }

        public List<InsuranceCoverageService> InsuranceCoverageServices { get; set; }
		public List<InsuranceCoverageHolderService> InsuranceCoverageHolderServices { get; set; }

		public List<InsuranceCoverageItem> InsuranceCoverageItems { get; set; }
		public List<InsuranceCoverageItemCategory> InsuranceCoverageItemCategories { get; set; }
		public List<InsuranceCoverageItemHolder> InsuranceCoverageItemHolders { get; set; }

		public Patient PolicyOwner { get; set; }
		public Guid? PolicyOwnerRowId { get; set; }
		public String PolicyOwnerFullName { get; set; }

		public Boolean IsOnlyForOtherFamilyMember { get; set; }
	}
}
