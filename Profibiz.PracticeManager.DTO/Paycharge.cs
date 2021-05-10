using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class Paycharge
	{
		public Guid RowId { get; set; }
		public Guid ChargeoutRecipientRowId { get; set; }
		public Nullable<decimal> Amount { get; set; }
		public Nullable<System.DateTime> PaychargeDate { get; set; }
		public string Notes { get; set; }
		public Nullable<System.DateTime> Created { get; set; }
		public string CreatedBy { get; set; }
		public Nullable<System.DateTime> Updated { get; set; }
		public string UpdatedBy { get; set; }
		public string PaychargeType { get; set; }
		public string BankName { get; set; }
		public string ChequeNumber { get; set; }
		public string BrunchNumber { get; set; }
		public string TransitNumber { get; set; }
		public string AccountNumber { get; set; }
		public byte[] Image { get; set; }
		public string TransactionId { get; set; }
		public string FullDescription { get; set; }
		public string ChargeoutRecipientName { get; set; }
		public Decimal AmountInChargeouts { get; set; }
		public Decimal AmountInRefcharges { get; set; }
		public Decimal PaychargeBalance { get; set; }

		public List<ChargeoutPaycharge> ChargeoutPaycharges { get; set; }
		public List<PaychargeRefcharge> PaychargeRefcharges { get; set; }
		public ChargeoutRecipient ChargeoutRecipient { get; set; }
	}
}
