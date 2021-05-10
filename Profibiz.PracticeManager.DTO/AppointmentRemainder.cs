using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class AppointmentRemainder
	{
		public Guid RowId { get; set; }
		public Guid AppointmentRowId { get; set; }
		public int RemainderInMinutes { get; set; }
		public bool IsProcessedEmail { get; set; }
		public DateTime? ProcessedEmailTime { get; set; }
		public bool IsProcessedSms { get; set; }
		public DateTime? ProcessedSmsTime { get; set; }
	}
}
