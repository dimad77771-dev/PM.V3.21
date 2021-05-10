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
	public class Refcharge
	{
		public Refcharge() { }

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
		public string ChargeoutRecipientName { get; set; }
		public string FullDescription { get; set; }
		public string RefchargeItemsType { get; set; }

		public virtual List<ChargeoutRefcharge> ChargeoutRefcharges { get; set; } = new List<ChargeoutRefcharge>();
		public virtual List<PaychargeRefcharge> PaychargeRefcharges { get; set; } = new List<PaychargeRefcharge>();
		public virtual ChargeoutRecipient ChargeoutRecipient { get; set; }


		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }
	}
}
