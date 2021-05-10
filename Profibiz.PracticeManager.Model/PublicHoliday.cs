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
	public partial class PublicHoliday
	{
		public PublicHoliday()
		{
		}

		public Guid RowId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public DateTime HolidayDate { get; set; }

		public int HolidayYear => HolidayDate.Year;

		public bool IsChanged { get; set; }

		public string Rowtype9 => "-";
	}


	public static class HolidaysFunc
	{
		public static bool IsHoliday(DateTime date)
		{
			return (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || LookupDataProvider.IsPublicHoliday(date));
		}
	}
}
