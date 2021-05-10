using AutoMapper;
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
	public class Refund
	{
		public Refund() { }

		public Guid RowId { get; set; }
		public Guid PatientRowId { get; set; }
		public DateTime? PaymentDate { get; set; }
		public Decimal Amount { get; set; }
		public string Notes { get; set; }
		public string PaymentType { get; set; }
		public string BankName { get; set; }
		public string ChequeNumber { get; set; }
		public string BrunchNumber { get; set; }
		public string TransitNumber { get; set; }
		public string AccountNumber { get; set; }
		public byte[] Image { get; set; }
		public string TransactionId { get; set; }
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public string UpdatedBy { get; set; }
		public string PatientFullName { get; set; }
		public string FullDescription { get; set; }
		public string RefundItemsType { get; set; }

		public virtual List<InvoiceRefund> InvoiceRefunds { get; set; } = new List<InvoiceRefund>();
		public virtual List<PaymentRefund> PaymentRefunds { get; set; } = new List<PaymentRefund>();
		public virtual Patient Patient { get; set; }


		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }
	}
}
