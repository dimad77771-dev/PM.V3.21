using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class InvoiceClaimDetail
	{
		public Guid RowId { get; set; }
		public Guid InvoiceClaimRowId { get; set; }
		public Guid ServcieOrSupplyRowId { get; set; }
		public Decimal? Units { get; set; }
		public Decimal Amount { get; set; }
		public string Description { get; set; }
	}
}