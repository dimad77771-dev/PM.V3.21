using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class SchedulerRecordItem
	{
		public Guid RowId { get; set; }
		public Guid SchedulerRecordRowId { get; set; }
		public int DayWeek { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime FinishTime { get; set; }
	}
}
