using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class InvoicePaymentByDoctors
	{
		public Guid InvoicePaymentRowId { get; set; }
		public Guid InvoiceRowId { get; set; }
		public Guid PaymentRowId { get; set; }
		public DateTime? PaymentDate { get; set; }
		public Decimal AllocateAmount { get; set; }
		public Decimal DueToDoctor { get; set; }
		public Guid ServiceProviderRowId { get; set; }

		public Payment Payment { get; set; }
		public Invoice Invoice { get; set; }
	}
}
