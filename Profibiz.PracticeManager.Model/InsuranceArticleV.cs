using DevExpress.Mvvm;
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
	public class InsuranceArticleV
	{
		public Guid InsuranceCoverageRowId { get; set; }
		public bool CoversAllHolders { get; set; }
		public Guid InsuranceCoverageItemRowId { get; set; }
		public Guid InsuranceCoverageItemHolderRowId { get; set; }
		public Decimal? AnnualAmountCovered { get; set; }
		public Decimal? MaximumQuantity { get; set; }
		public String CategoryInfo { get; set; }
		public String PatientInfo { get; set; }
		public String ClaimInfo { get; set; }
		public Guid ViewRowId { get; set; }

		public InsuranceCoverage InsuranceCoverage { get; set; }

		public string PatientsName { get; set; }
		public string PatientReferrerName { get; set; }
		public string CategoriesName { get; set; }

		public Guid PatientRowId0 { get; set; }
		public Guid[] CategoryRowIds { get; set; }
		public Guid CategoryRowId0 { get; set; }

		public Decimal? ApproveAmount { get; set; }
		public Decimal? ApproveUnits { get; set; }

		public Decimal? RemaindedAmount { get; set; }
		public Decimal? RemaindedUnits { get; set; }


		public void CalculateColumns()
		{
			var arrPatientInfo = XDocumentFunc.ParseArray(PatientInfo);
			var names = arrPatientInfo.Select(q => (string)q.Element("PatientFullName")).ToArray();
			PatientsName = string.Join("\n", names);

			var referrerNames = arrPatientInfo.Select(q => (string)q.Element("PatientReferrerName")).Where(q => !string.IsNullOrEmpty(q)).Distinct().ToArray();
			PatientReferrerName = string.Join("\n", referrerNames) ?? "";


			PatientRowId0 = arrPatientInfo.Select(q => (Guid)q.Element("PatientRowId")).FirstOrDefault();

			var categories = XDocumentFunc.ParseArray(CategoryInfo).Select(q => (string)q.Element("CategoryName")).ToArray();
			CategoriesName = string.Join("\n", categories);
			
			CategoryRowIds = XDocumentFunc.ParseArray(CategoryInfo).Select(q => (Guid)q.Element("CategoryRowId")).ToArray();
			CategoryRowId0 = CategoryRowIds.FirstOrDefault();

			ApproveAmount = XDocumentFunc.ParseArray(ClaimInfo).Sum(q => (decimal?)q.Element("Amount")) ?? 0;

			ApproveUnits = XDocumentFunc.ParseArray(ClaimInfo).Sum(q => (decimal?)q.Element("Unit")) ?? 0;
			if (MaximumQuantity == null && ApproveUnits == 0) ApproveUnits = null;

			RemaindedAmount = AnnualAmountCovered - ApproveAmount;
			RemaindedUnits = MaximumQuantity - RemaindedUnits;
		}


		public DelegateCommand OpenDetailCommand => new DelegateCommand(() =>
		{
			OnOpenDetail?.Invoke();
		});
		public Action OnOpenDetail;

		public DelegateCommand OpenDetail2Command => new DelegateCommand(() =>
		{
			OnOpenDetail2?.Invoke();
		});
		public Action OnOpenDetail2;
	}
}
