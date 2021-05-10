using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Profibiz.PracticeManager.Infrastructure;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Markup;
using System.Globalization;
using PropertyChanged;


namespace Profibiz.PracticeManager.Model
{
	public static class EventCalendarSnoozedInfo
	{
		public static EventCalendarSnoozedModel[] All = new[]
		{
			new EventCalendarSnoozedModel("15 hours before start", 15, true),
			new EventCalendarSnoozedModel("10 hours before start", 10, true),
			new EventCalendarSnoozedModel("5 hours before start", 5, true),
			new EventCalendarSnoozedModel("0 hours before start", 0, true),

			new EventCalendarSnoozedModel("0 minutes", 0),
			new EventCalendarSnoozedModel("5 minutes", 5, isDefault: true),
			new EventCalendarSnoozedModel("10 minutes", 10),
			new EventCalendarSnoozedModel("15 minutes", 15),
			new EventCalendarSnoozedModel("30 minutes", 30),
			new EventCalendarSnoozedModel("1 hour", 1 * 60),
			new EventCalendarSnoozedModel("2 hours", 2 * 60),
			new EventCalendarSnoozedModel("3 hours", 3 * 60),
			new EventCalendarSnoozedModel("4 hours", 4 * 60),
			new EventCalendarSnoozedModel("5 hours", 5 * 60),
			new EventCalendarSnoozedModel("6 hours", 6 * 60),
			new EventCalendarSnoozedModel("7 hours", 7 * 60),
			new EventCalendarSnoozedModel("8 hours", 8 * 60),
			new EventCalendarSnoozedModel("9 hours", 9 * 60),
			new EventCalendarSnoozedModel("10 hours", 10 * 60),
			new EventCalendarSnoozedModel("11 hours", 11 * 60),
			new EventCalendarSnoozedModel("12 hours", 12 * 60),
			new EventCalendarSnoozedModel("18 hours", 18 * 60),
			new EventCalendarSnoozedModel("1 day", 1 * 60 * 24),
			new EventCalendarSnoozedModel("2 days", 2 * 60 * 24),
			new EventCalendarSnoozedModel("3 days", 3 * 60 * 24),
			new EventCalendarSnoozedModel("4 days", 4 * 60 * 24),
			new EventCalendarSnoozedModel("1 week", 1 * 60 * 24 * 7),
			new EventCalendarSnoozedModel("2 weeks", 2 * 60 * 24 * 7),
		};

		public static EventCalendarSnoozedModel Default => All.First(q => q.IsDefault);
	}


	[ImplementPropertyChanged]
	public class EventCalendarSnoozedModel
	{
		public EventCalendarSnoozedModel(string name, int minutes, bool beforeStartMode = false, bool isDefault = false)
		{
			Name = name;
			Minutes = minutes;
			BeforeStartMode = beforeStartMode;
			IsDefault = isDefault;
		}

		public string Name { get; set; }
		public int Minutes { get; set; }
		public bool BeforeStartMode { get; set; }
		public bool IsDefault { get; set; }
	}
}
