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
	public class InsuranceArticleInfo
	{
		public InsuranceArticleInfo()
		{
		}

		public Row[] Rows { get; set; }

		public class Row
		{
			public InvoiceClaimDetail InvoiceClaimDetail { get; set; }
			public InvoiceClaim InvoiceClaim { get; set; }
			public Invoice Invoice { get; set; }
			public InsuranceCoverage InsuranceCoverage { get; set; }
			public InvoiceItemsClass InvoiceItems { get; set; }

			public DelegateCommand OpenDetailCommand => new DelegateCommand(() =>
			{
				OnOpenDetail?.Invoke();
			});
			public Action OnOpenDetail;
		}

		public class InvoiceItemsClass
		{
			public Decimal? SumUnits { get; set; }
			public Decimal? SumAmount { get; set; }
		}


	}
}
