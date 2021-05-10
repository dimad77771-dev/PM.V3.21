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
	public class InvoicePaymentByDoctorAndPeriodView
	{
		public Guid ServiceProviderRowId { get; set; }
		public DateTime PaymentPeriod { get; set; }
		public Decimal SumDueToDoctor { get; set; }
	}
}
