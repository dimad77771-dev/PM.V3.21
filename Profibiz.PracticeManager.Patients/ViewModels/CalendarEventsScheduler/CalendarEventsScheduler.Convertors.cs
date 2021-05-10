using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Profibiz.PracticeManager.Infrastructure;
using Prism.Interactivity.InteractionRequest;
using System.Collections.ObjectModel;
using DevExpress.DevAV.Common;
using DevExpress.Mvvm.POCO;
using System.ComponentModel;
using AutoMapper;
using Newtonsoft.Json;
using PropertyChanged;
using Profibiz.PracticeManager.InfrastructureExt.Common;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using XtraAppointment = DevExpress.XtraScheduler.Appointment;
using TimeOfDayInterval = DevExpress.XtraScheduler.TimeOfDayInterval;
using Xtra = DevExpress.XtraScheduler;
using Profibiz.PracticeManager.Model;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Markup;
using System.Drawing;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Scheduler.Drawing;
using System.Windows.Media;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	public class CalendarEventToColorConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var appointment = (CalendarEventsSchedulerViewModel.HospitalCalendarEvent)value;
			var status1RowId = appointment.Entity.Status1RowId;
			var status = LookupDataProvider.FindCalendarEventStatus(status1RowId);
			if ((string)parameter == "Foreground")
			{
				return 
					status != null ? status.ForegroundColor : 
					appointment.Entity.IsVacation ? "#474747" : "#1F497D";
			}
			else if ((string)parameter == "Background")
			{
				return 
					status != null ? status.BackgroundColor :
					appointment.Entity.IsVacation ? "#CEE0FF" : "#FFE0CC";
			}
			else throw new ArgumentException();
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}


	public class CalendarEventStatusToColorConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var statusRowId = (Guid?)value;
			var returnType = (string)parameter;

			var row = LookupDataProvider.FindCalendarEventStatus(statusRowId);
			if (row == null)
			{
				if (returnType == "Visibility")
				{
					return "Collapsed";
				}
				else if (returnType == "Background")
				{
					return "#FFFFFF";
				}
				else if (returnType == "Foreground")
				{
					return "#FFFFFF";
				}
				else if (returnType == "Text")
				{
					return "";
				}
			}
			else
			{
				if (returnType == "Visibility")
				{
					return "Visible";
				}
				else if (returnType == "Background")
				{
					return row.BackgroundColor;
				}
				else if (returnType == "Foreground")
				{
					return row.ForegroundColor;
				}
				else if (returnType == "Text")
				{
					return row.Name;
				}
			}
			throw new ArgumentException();
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

	public class CalendarEventStatusToColorConverterExtension : MarkupExtension
	{
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new CalendarEventStatusToColorConverter();
		}
	}

	public class CalendarEventBorderColorConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var isSelected = (bool)values[0];
			//var isDoctorSelected = (bool)values[1];
			//var isInsuranceProviderSelected = (values[2] is bool ? (bool)values[2] : false);

			var color =
				isSelected ? "Brown" :
				//isDoctorSelected ? "Red" :
				//isInsuranceProviderSelected ? "Red" :
				"Black";

			var mcolor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(color);
			return new SolidColorBrush(mcolor);
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class CalendarEventItemTextConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var appointment = (CalendarEventsSchedulerViewModel.HospitalCalendarEvent)value;
			return appointment.Subject;
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

	public class CalendarEventItemTextDecorationConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var appointment = (CalendarEventsSchedulerViewModel.HospitalCalendarEvent)value;
			if (appointment.Entity.Completed)
			{
				return "Strikethrough";
			}
			else
			{
				return null;
			}
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

	public class CalendarEventItemTextFontStyleConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var appointment = (CalendarEventsSchedulerViewModel.HospitalCalendarEvent)value;
			if (appointment.Entity.Completed)
			{
				return "Italic";
			}
			else
			{
				return "Normal";
			}
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}


}
