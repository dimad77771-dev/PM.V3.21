using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class EmailSendAttachment
	{
		public Guid RowId { get; set; }
		public Guid EmailSendRowId { get; set; }
		public String FileName { get; set; }
		public Byte[] FileBytes { get; set; }
	}
}