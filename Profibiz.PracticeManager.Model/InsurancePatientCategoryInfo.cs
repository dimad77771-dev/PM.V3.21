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


		public FindResult Find(Guid insuranceCoverageRowId, Guid patientRowId, Guid categoryRowId)
		{
			var rez = new FindResult { IsFind = false };
			//return rez;

			var article222 =
				Articles.Where(q => q.InsuranceCoverageRowId == insuranceCoverageRowId && q.PatientRowIds.Contains(patientRowId) && q.CategoryRowIds.Contains(categoryRowId))
				.ToArray();


			var article = 
				Articles.SingleOrDefault(q => q.InsuranceCoverageRowId == insuranceCoverageRowId && q.PatientRowIds.Contains(patientRowId) && q.CategoryRowIds.Contains(categoryRowId));


			if (article != null)
			{
				rez.IsFind = true;
				rez.TotalAmont = article.TotalAmont;
				rez.InsuranceCoverageRowId = article.InsuranceCoverageRowId;
				rez.CoversAllHolders = article.CoversAllHolders;
				rez.InsuranceCoverageItemRowId = article.InsuranceCoverageItemRowId;
				rez.InsuranceCoverageItemHolderRowId = article.InsuranceCoverageItemHolderRowId;
				rez.ApproveAmount = article.ApproveAmount;
				rez.ApproveUnits = article.ApproveUnits;
			}

			return rez;
		}

		public FindResult Find(Guid? insuranceCoverageRowId, Guid? patientRowId, Guid? categoryRowId)
		{
			return Find(insuranceCoverageRowId ?? default(Guid), patientRowId ?? default(Guid), categoryRowId ?? default(Guid));
		}

		public class FindResult
		{
			public Boolean IsFind { get; set; }

			public Guid InsuranceCoverageRowId { get; set; }
			public bool CoversAllHolders { get; set; }
			public Guid? InsuranceCoverageItemRowId { get; set; }
			public Guid? InsuranceCoverageItemHolderRowId { get; set; }


			public Decimal TotalAmont { get; set; }
			public Decimal TotalUnits { get; set; }

			public Decimal ApproveAmount { get; set; }
			public Decimal ApproveUnits { get; set; }
		}
	}

}
