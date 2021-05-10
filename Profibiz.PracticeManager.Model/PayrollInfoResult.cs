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
	public class PayrollInfoResult
	{
		public PayrollInfoResult() {}

		public Guid ServiceProviderRowId { get; set; }
		public String ServiceProviderFullName { get; set; }
		public String ServiceProviderType { get; set; }
		public String EmploymentType { get; set; }
		public DateTime PeriodStart { get; set; }
		public DateTime PeriodFinish { get; set; }
		public Decimal SumPaidByPatientsThisPeriod { get; set; }
		public Decimal SumDueToDoctorThisPeriod { get; set; }
		public Decimal SumDueToDoctorPrevPeriod { get; set; }
		public Decimal SumPayrollPayThisPeriod { get; set; }
		public Decimal SumPayrollPayPrevPeriod { get; set; }
		public Decimal InvoicesTotal { get; set; }
		public Decimal InvoicesApproveAmont { get; set; }
		public Decimal InvoicesDueByPatient { get; set; }
		public Decimal OpeningBalance { get; set; }
		public Decimal ClosingBalance { get; set; }

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public decimal SumDueToDoctorAll => SumDueToDoctorThisPeriod + OpeningBalance;
		public string PeriodStartFinishText
		{
			get
			{
				var s1 = PeriodStart.FormatMonthYear();
				var s2 = PeriodFinish.FormatMonthYear();
				return (s1 == s2 ? s1 : s1 + " - " + s2);
			}
		}
		
	}
}
