using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class PaymentRefund
	{
		public Guid RowId { get; set; }
		public Guid PaymentRowId { get; set; }
		public Guid RefundRowId { get; set; }
		public Decimal Amount { get; set; }
		public Decimal Tax { get; set; }
		public DateTime AllocationDate { get; set; }
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public string UpdatedBy { get; set; }

		public virtual Refund Refund { get; set; }
		public virtual Payment Payment { get; set; }
	}
}
