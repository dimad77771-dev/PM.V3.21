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
	public class SupplierPayment
	{
		public SupplierPayment()
		{
		}


		public Guid RowId { get; set; }
		public Guid SupplierRowId { get; set; }
		public Decimal? Amount { get; set; }
		public DateTime? SupplierPaymentDate { get; set; }
		public string Notes { get; set; }
		public Nullable<System.DateTime> Created { get; set; }
		public string CreatedBy { get; set; }
		public Nullable<System.DateTime> Updated { get; set; }
		public string UpdatedBy { get; set; }
		public string SupplierPaymentType { get; set; }
		public string BankName { get; set; }
		public string ChequeNumber { get; set; }
		public string BrunchNumber { get; set; }
		public string TransitNumber { get; set; }
		public string AccountNumber { get; set; }
		public byte[] Image { get; set; }
		public string TransactionId { get; set; }
		public string FullDescription { get; set; }

		public string SupplierFullName { get; set; }

		public virtual List<OrderPayment> OrderPayments { get; set; } = new List<OrderPayment>();
		public virtual List<SupplierPaymentRefund> SupplierPaymentRefunds { get; set; } = new List<SupplierPaymentRefund>();
		public virtual Supplier Supplier { get; set; }

		public Decimal AmountInOrders { get; set; }
		public Decimal AmountInSupplierRefunds { get; set; }

		public Decimal SupplierPaymentBalance => (Amount ?? 0) - AmountInOrders - AmountInSupplierRefunds;
		public Boolean IsSupplierPaymentBalancePositive => (SupplierPaymentBalance > 0);


		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }
	}
}
