using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class SupplierPaymentRefund
	{
		public Guid RowId { get; set; }
		public Guid SupplierPaymentRowId { get; set; }
		public Guid SupplierRefundRowId { get; set; }
		public Decimal Amount { get; set; }
		public Decimal Tax { get; set; }
		public DateTime AllocationDate { get; set; }
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public string UpdatedBy { get; set; }

		public virtual SupplierRefund SupplierRefund { get; set; }
		public virtual SupplierPayment SupplierPayment { get; set; }
	}
}
