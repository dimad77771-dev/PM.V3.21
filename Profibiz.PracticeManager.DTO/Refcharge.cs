using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class Refcharge
	{
		public Guid RowId { get; set; }
		public Guid ChargeoutRecipientRowId { get; set; }
		public DateTime? PaychargeDate { get; set; }
		public Decimal Amount { get; set; }
		public string Notes { get; set; }
		public string PaychargeType { get; set; }
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
		public String ChargeoutRecipientName { get; set; }
		public string FullDescription { get; set; }
		public string RefchargeItemsType { get; set; }

		public virtual List<ChargeoutRefcharge> ChargeoutRefcharges { get; set; }
		public virtual List<PaychargeRefcharge> PaychargeRefcharges { get; set; }
		public virtual ChargeoutRecipient ChargeoutRecipient { get; set; }
	}
}
