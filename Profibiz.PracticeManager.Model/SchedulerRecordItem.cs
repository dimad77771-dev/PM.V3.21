using DevExpress.Mvvm;
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
	public partial class SchedulerRecordItem
	{
		public SchedulerRecordItem()
		{
		}

		public Guid RowId { get; set; }
		public Guid SchedulerRecordRowId { get; set; }
		public int DayWeek { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime FinishTime { get; set; }

		public List<SchedulerRecordItem> SchedulerRecordItems { get; set; } = new List<SchedulerRecordItem>();


		//public string FullName => Name;
		//public string Rowtype9 => "Medical" + CategoryType;

		public bool IsChanged { get; set; }
	}
}
