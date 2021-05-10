using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class PayrollPaymentAllocation
	{
		public Guid RowId { get; set; }
		public Guid PayrollPaymentRowId { get; set; }
		public DateTime PeriodStart { get; set; }
		public DateTime PeriodFinish { get; set; }
		public decimal Amount { get; set; }
	}
}