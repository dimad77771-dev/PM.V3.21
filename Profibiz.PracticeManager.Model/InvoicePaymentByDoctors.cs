using DevExpress.Mvvm;
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
	public class InvoicePaymentByDoctors
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

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public DelegateCommand OpenDetailCommand => new DelegateCommand(() =>
		{
			OnOpenDetail?.Invoke();
		});
		public Action OnOpenDetail;
	}
}
