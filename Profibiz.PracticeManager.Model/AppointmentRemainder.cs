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
	public partial class AppointmentRemainder
	{
		public AppointmentRemainder()
		{
		}

		public Guid RowId { get; set; }
		public Guid AppointmentRowId { get; set; }
		public int RemainderInMinutes { get; set; }
		public bool IsProcessedEmail { get; set; }
		public DateTime? ProcessedEmailTime { get; set; }
		public bool IsProcessedSms { get; set; }
		public DateTime? ProcessedSmsTime { get; set; }

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public string Rowtype9 => "-";

	}
}
