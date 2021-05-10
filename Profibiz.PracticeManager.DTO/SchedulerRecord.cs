using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class SchedulerRecord
	{
		public Guid RowId { get; set; }
		public Guid ServiceProviderRowId { get; set; }
		public DateTime StartPeriod { get; set; }
		public DateTime FinishPeriod { get; set; }
		public bool IsBaseRecord { get; set; }

		public DateTime StartPeriodDate => ((DateTime)StartPeriod).Date;
		public DateTime FinishPeriodDate => ((DateTime)FinishPeriod).Date;


		public List<SchedulerRecordItem> SchedulerRecordItems { get; set; }
	}
}
