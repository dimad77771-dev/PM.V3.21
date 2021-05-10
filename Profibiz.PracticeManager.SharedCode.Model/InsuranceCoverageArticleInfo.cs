using System;
using System.Linq;
using System.Collections.Generic;
#if DLL_BACKEND
	using Profibiz.PracticeManager.EF;
#elif DLL_FRONTEND
	using Profibiz.PracticeManager.Model;
#endif


namespace Profibiz.PracticeManager.SharedCode
{
	public class InsuranceCoverageArticleInfo
	{
		public InsuranceCoverageArticleInfo()
		{
		}

		//"координаты" статьи страхового полиса
		public Guid InsuranceCoverageRowId { get; set; }
		public Guid? InsuranceCoverageItemRowId { get; set; }
		public Guid? InsuranceCoverageItemHolderRowId { get; set; }
		public bool CoversAllHolders { get; set; }

		//"параметры" статьи страхового полиса
		public decimal? AnnualAmountCovered { get; set; }
		public decimal? PercentageCovered { get; set; }
		public decimal? HourlyRateCap { get; set; }
		public bool IsPrescriptionRequired { get; set; }
		public decimal? PerVisitCost { get; set; }
		public int? MaximumVisits { get; set; }
		public decimal? MaximumQuantity { get; set; }

		//"потраченные суммы и кол-ва"
		public decimal? ExpendedAmount { get; set; }
		public decimal? ExpendedQuantity { get; set; }

		//"остатки суммы и кол-ва"
		public decimal? RemainedAmount { get; set; }
		public decimal? RemainedQuantity { get; set; }


		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }
	}
}
