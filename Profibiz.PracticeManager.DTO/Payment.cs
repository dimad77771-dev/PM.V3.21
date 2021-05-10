using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class Payment
	{
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
		public Decimal AmountInInvoices { get; set; }
		public Decimal AmountInRefunds { get; set; }
		public Decimal PaymentBalance { get; set; }
		

		public virtual List<InvoicePayment> InvoicePayments { get; set; }
		public virtual List<PaymentRefund> PaymentRefunds { get; set; }
		public virtual Patient Patient { get; set; }
	}
}
