using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class PayrollInfoResult
	{
		public Guid ServiceProviderRowId { get; set; }
		public string ServiceProviderFullName { get; set; }
		public string ServiceProviderType { get; set; }
		public string EmploymentType { get; set; }
		public DateTime PeriodStart { get; set; }
		public DateTime PeriodFinish { get; set; }
		public decimal SumPaidByPatientsThisPeriod { get; set; }
		public decimal SumDueToDoctorThisPeriod { get; set; }
		public decimal SumDueToDoctorPrevPeriod { get; set; }
		public decimal SumPayrollPayThisPeriod { get; set; }
		public decimal SumPayrollPayPrevPeriod { get; set; }
		public decimal InvoicesTotal { get; set; }
		public decimal InvoicesApproveAmont { get; set; }
		public decimal InvoicesDueByPatient { get; set; }
		public decimal OpeningBalance { get; set; }
		public decimal ClosingBalance { get; set; }

	}
}
