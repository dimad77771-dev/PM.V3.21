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
	public static class AppointmentRemainderEnumInfo
	{
		public static AppointmentRemainderEnumModel[] All = new[]
		{
			new AppointmentRemainderEnumModel("15 mins", 15),
			new AppointmentRemainderEnumModel("30 mins", 30),
			new AppointmentRemainderEnumModel("1 horus", 1 * 60),
			new AppointmentRemainderEnumModel("3 hours", 3 * 60),
			new AppointmentRemainderEnumModel("1 day", 1 * 60 * 24),
		};

		public static AppointmentRemainderEnumModel GetByMinutes(int mins)
		{
			return All.SingleOrDefault(q => q.Minutes == mins);
		}
	}


	[ImplementPropertyChanged]
	public class AppointmentRemainderEnumModel
	{
		public AppointmentRemainderEnumModel(string name, int minutes)
		{
			Name = name;
			Minutes = minutes;
		}

		public string Name { get; set; }
		public int Minutes { get; set; }
	}
}
