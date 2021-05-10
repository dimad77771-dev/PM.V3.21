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
	public partial class SchedulerRecord
	{
		public SchedulerRecord()
		{
		}

		public Guid RowId { get; set; }
		public Guid ServiceProviderRowId { get; set; }
		public DateTime? StartPeriod { get; set; }
		public DateTime? FinishPeriod { get; set; }
		public bool IsBaseRecord { get; set; }


		public List<SchedulerRecordItem> SchedulerRecordItems { get; set; } = new List<SchedulerRecordItem>();


		//public string FullName => Name;
		//public string Rowtype9 => "Medical" + CategoryType;

		public bool IsChanged { get; set; }
	}



	//public static class SchedulerRecordFunc
	//{
	//	public static SchedulerRecord FindRecordForDate(List<SchedulerRecord> records, DateTime date)
	//	{
	//		date = date.Date;
	//		var isBaseSet = new[] { false, true };
	//		foreach (var isBase in isBaseSet)
	//		{
	//			var record = records.SingleOrDefault(q => q.IsBaseRecord == isBase && date >= q.StartPeriodDate && date <= q.FinishPeriodDate);
	//			if (record != null)
	//			{
	//				return record;
	//			}
	//		}
	//		return null;
	//	}

	//	public static SchedulerRecordItem FindItemForDate(List<SchedulerRecord> records, DateTime date)
	//	{
	//		var record = FindRecordForDate(records, date);
	//		if (record == null) return null;

	//		var dayOfWeek = (int)date.DayOfWeek;
	//		var item = record.SchedulerRecordItems.SingleOrDefault(q => q.DayWeek == dayOfWeek);
	//		return item;
	//	}

	//	public static bool IsWorkDate(List<SchedulerRecord> records, DateTime date)
	//	{
	//		var item = FindItemForDate(records, date);
	//		return (item != null);
	//	}
	//}
}
