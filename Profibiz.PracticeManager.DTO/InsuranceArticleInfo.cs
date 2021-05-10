using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
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
		}

		public class InvoiceItemsClass
		{
			public Decimal? SumUnits { get; set; }
			public Decimal? SumAmount { get; set; }
		}
	}
}
