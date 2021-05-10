using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class PayrollPaymentByDoctorAndPeriodView
	{
		public Guid ServiceProviderRowId { get; set; }
		public DateTime PeriodStart { get; set; }
		public Decimal SumPayrollAmount { get; set; }
	}
}
