using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class InsurancePatientCategoryInfo
	{
		public List<Article> Articles { get; set; } = new List<Article>();

		public class Article
		{
			public bool CoversAllHolders { get; set; }
			public Guid? InsuranceCoverageItemRowId { get; set; }
			public Guid? InsuranceCoverageItemHolderRowId { get; set; }

			public Guid InsuranceCoverageRowId { get; set; }
			public List<Guid> PatientRowIds { get; set; }
			public List<Guid> CategoryRowIds { get; set; }

			public Decimal TotalAmont { get; set; }
			public Decimal TotalUnits { get; set; }

			public Decimal ApproveAmount { get; set; }
			public Decimal ApproveUnits { get; set; }
		}
	}
}
