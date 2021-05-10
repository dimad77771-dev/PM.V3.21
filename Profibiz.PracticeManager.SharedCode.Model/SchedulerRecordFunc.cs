using System;
using System.Linq;
using System.Collections.Generic;
#if DLL_BACKEND
	using Profibiz.PracticeManager.EF;
#elif DLL_FRONTEND
	using Profibiz.PracticeManager.Model;
#endif


namespace Profibiz.PracticeManager.SharedCode
{
	public static class SchedulerRecordFunc
	{
		public static SchedulerRecord FindRecordForDate(List<SchedulerRecord> records, DateTime date)
		{
			date = date.Date;
			var isBaseSet = new[] { false, true };
			foreach (var isBase in isBaseSet)
			{
				var record = records.SingleOrDefault(q => q.IsBaseRecord == isBase && date >= q.StartPeriod && date <= q.FinishPeriod);
				if (record != null)
				{
					return record;
				}
			}
			return null;
		}

		public static SchedulerRecordItem FindItemForDate(List<SchedulerRecord> records, DateTime date)
		{
			var record = FindRecordForDate(records, date);
			if (record == null) return null;

			var dayOfWeek = (int)date.DayOfWeek;
			var item = record.SchedulerRecordItems.SingleOrDefault(q => q.DayWeek == dayOfWeek);
			if (item != null)
			{
				item.StartTime = date.Date + item.StartTime.TimeOfDay;
				item.FinishTime = date.Date + item.FinishTime.TimeOfDay;
			}
			return item;
		}

		public static bool IsWorkDate(List<SchedulerRecord> records, DateTime date)
		{
			var item = FindItemForDate(records, date);
			return (item != null);
		}
	}
}
