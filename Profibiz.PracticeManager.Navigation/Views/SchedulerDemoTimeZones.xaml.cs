using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Profibiz.PracticeManager.Navigation.Views
{
    public partial class SchedulerDemoTimeZones : UserControl
    {
        public SchedulerDemoTimeZones()
        {
			InitializeComponent();
			//InitializeScheduler();
		}

		//void InitializeScheduler()
		//{
		//	SchedulerDataHelper.DataBind(scheduler);
		//	InitializeSchedulerProperties(scheduler);
		//}

		//public static void DataBind(SchedulerStorage storage)
		//{
		//	InitCustomAppointmentStatuses(storage);
		//	FillStorageData(storage);
		//}



	}


	public class UsedAppointmentTypeToBoolConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			UsedAppointmentType type = (UsedAppointmentType)value;
			return type.Equals(UsedAppointmentType.All) ? true : false;
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? UsedAppointmentType.All : UsedAppointmentType.None;
		}
	}

	public class AppointmentConflictsModeToBoolConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			AppointmentConflictsMode mode = (AppointmentConflictsMode)value;
			return mode.Equals(AppointmentConflictsMode.Allowed) ? true : false;
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? AppointmentConflictsMode.Allowed : AppointmentConflictsMode.Forbidden;
		}
	}

}
