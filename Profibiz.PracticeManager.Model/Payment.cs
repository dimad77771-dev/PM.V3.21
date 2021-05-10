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
	public class Payment
	{
		public Payment()
		{
		}


        public System.Guid RowId { get; set; }
        public System.Guid PatientRowId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string PaymentType { get; set; }
        public string BankName { get; set; }
        public string ChequeNumber { get; set; }
        public string BrunchNumber { get; set; }
        public string TransitNumber { get; set; }
        public string AccountNumber { get; set; }
        public byte[] Image { get; set; }
        public string TransactionId { get; set; }
        public string FullDescription { get; set; }

		public string PatientFullName { get; set; }

		public virtual List<InvoicePayment> InvoicePayments { get; set; } = new List<InvoicePayment>();
		public virtual List<PaymentRefund> PaymentRefunds { get; set; } = new List<PaymentRefund>();
		public virtual Patient Patient { get; set; }

		public Decimal AmountInInvoices { get; set; }
		public Decimal AmountInRefunds { get; set; }

		public Decimal PaymentBalance => (Amount ?? 0) - AmountInInvoices - AmountInRefunds;
		public Boolean IsPaymentBalancePositive => (PaymentBalance > 0);


		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }
	}
}
