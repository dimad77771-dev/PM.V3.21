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
	public class InvoiceClaimDetail
	{
		public InvoiceClaimDetail() { }

		public Guid RowId { get; set; }
		public Guid InvoiceClaimRowId { get; set; }
		public Guid ServcieOrSupplyRowId { get; set; }
		public Decimal? Units { get; set; }
		public Decimal Amount { get; set; }
		public string Description { get; set; }

		public Decimal InvoiceItemsUnits { get; set; }
		public Decimal InvoiceItemsAmount { get; set; }


		//public Decimal InsuranceAmount { get; set; }
		

		public bool IsNew { get; set; }
		public bool IsChanged { get; set; }

		public String MedicalServiceOrSupplyName => LookupDataProvider.MedicalService2Name(ServcieOrSupplyRowId);


		public InsurancePatientCategoryInfo.FindResult InsuranceInfo { get; set; } = new InsurancePatientCategoryInfo.FindResult();

		public DelegateCommand InsuranceOpenCommand => new DelegateCommand(() =>
		{
			OnInsuranceOpen?.Invoke();
		});
		public Action OnInsuranceOpen;
	}
}
