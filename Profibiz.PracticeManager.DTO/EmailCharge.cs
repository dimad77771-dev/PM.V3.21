using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class EmailCharge
	{
		public Guid RowId { get; set; }
		public String Body { get; set; }
		public String Subject { get; set; }
		public DateTime SendDate { get; set; }
		public Guid? ChargeoutRowId { get; set; }
		public bool IsSuccess { get; set; }
		public string ErrorMessage { get; set; }
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public string UpdateBy { get; set; }
		public string EmailChargeRecipientsList { get; set; }
		public string ChargeoutRecipientName { get; set; }
		public Guid? ChargeoutRecipientRowId { get; set; }
		public DateTime? ChargeoutDate { get; set; }
		public string ChargeoutNumber { get; set; }

		public List<EmailChargeRecipient> EmailChargeRecipients { get; set; }
		public List<EmailChargeAttachment> EmailChargeAttachments { get; set; }
	}
}