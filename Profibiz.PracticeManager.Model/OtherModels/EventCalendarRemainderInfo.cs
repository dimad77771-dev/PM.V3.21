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
	public static class EventCalendarRemainderInfo
	{
		public static EventCalendarRemainderModel[] All = new[]
		{
			new EventCalendarRemainderModel("None", -1),
			new EventCalendarRemainderModel("0 minutes", 0),
			new EventCalendarRemainderModel("5 minutes", 5),
			new EventCalendarRemainderModel("10 minutes", 10),
			new EventCalendarRemainderModel("15 minutes", 15),
			new EventCalendarRemainderModel("30 minutes", 30),
			new EventCalendarRemainderModel("1 hour", 1 * 60),
			new EventCalendarRemainderModel("2 hours", 2 * 60),
			new EventCalendarRemainderModel("3 hours", 3 * 60),
			new EventCalendarRemainderModel("4 hours", 4 * 60),
			new EventCalendarRemainderModel("5 hours", 5 * 60),
			new EventCalendarRemainderModel("6 hours", 6 * 60),
			new EventCalendarRemainderModel("7 hours", 7 * 60),
			new EventCalendarRemainderModel("8 hours", 8 * 60),
			new EventCalendarRemainderModel("9 hours", 9 * 60),
			new EventCalendarRemainderModel("10 hours", 10 * 60),
			new EventCalendarRemainderModel("11 hours", 11 * 60),
			new EventCalendarRemainderModel("12 hours", 12 * 60),
			new EventCalendarRemainderModel("18 hours", 18 * 60),
			new EventCalendarRemainderModel("1 day", 1 * 60 * 24),
			new EventCalendarRemainderModel("2 days", 2 * 60 * 24),
			new EventCalendarRemainderModel("3 days", 3 * 60 * 24),
			new EventCalendarRemainderModel("4 days", 4 * 60 * 24),
			new EventCalendarRemainderModel("1 week", 1 * 60 * 24 * 7),
			new EventCalendarRemainderModel("2 weeks", 2 * 60 * 24 * 7),
		};
	}


	[ImplementPropertyChanged]
	public class EventCalendarRemainderModel
	{
		public EventCalendarRemainderModel(string name, int value)
		{
			Name = name;
			Value = value;
		}

		public string Name { get; set; }
		public int Value { get; set; }
	}
}
