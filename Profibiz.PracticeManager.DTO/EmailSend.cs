using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class EmailSend
	{
		public Guid RowId { get; set; }
		public String Body { get; set; }
		public String Subject { get; set; }
		public DateTime SendDate { get; set; }
		public Guid? InvoiceRowId { get; set; }
		public bool IsSuccess { get; set; }
		public string ErrorMessage { get; set; }
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public string UpdateBy { get; set; }
		public string EmailSendRecipientsList { get; set; }
		public string InvoicePatientName { get; set; }
		public Guid? InvoicePatientRowId { get; set; }
		public DateTime? InvoiceDate { get; set; }
		public string InvoiceNumber { get; set; }


		public List<EmailSendRecipient> EmailSendRecipients { get; set; }
		public List<EmailSendAttachment> EmailSendAttachments { get; set; }
	}
}