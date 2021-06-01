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
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Scheduler.Drawing;
using System.Windows.Media;
using System.Windows;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	public class DoctorToColorConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var appointment = (AppointmentsSchedulerViewModel.HospitalAppointment)value;
			if ((string)parameter == "Foreground")
			{
				if (appointment?.Patient?.IsNotRegistered == true)
				{
					return "Blue";
				}
				else
				{
					return appointment.Doctor.ForegroundColor;
				}
			}
			else if ((string)parameter == "Background")
			{
				return appointment.Doctor.BackgroundColor;
			}
			else throw new ArgumentException();
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

	public class AppointmentStatusToColorConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var statusRowId = (Guid?)value;
			var returnType = (string)parameter;

			var row = LookupDataProvider.FindAppointmentStatus(statusRowId);
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

	public class AppointmentStatusToColorConverterExtension : MarkupExtension
	{
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new AppointmentStatusToColorConverter();
		}
	}

	public class AppointmentBorderColorConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var isSelected = (bool)values[0];
			var isDoctorSelected = (bool)values[1];
			var isInsuranceProviderSelected = (values[2] is bool ? (bool)values[2] : false);

			var color = 
				isSelected ? "Brown" :
				isDoctorSelected ? "Red" :
				isInsuranceProviderSelected ? "Red" :
				"Black";

			var mcolor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(color);
			return new SolidColorBrush(mcolor);
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class AppointmentItemTextConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var appointment = (AppointmentsSchedulerViewModel.HospitalAppointment)value;
			if (appointment.ParentViewModel.ViewMode == AppointmentsSchedulerViewModel.ViewModeEnum.OnePatient)
			{
				return appointment.Doctor.Entity.FullName;
			}
			else
			{
				var medicalServiceName = LookupDataProvider.MedicalService2Name(appointment.MedicalServicesOrSupplyRowId);
				var text = appointment.Patient.FullNameForAppointment;
				if (!string.IsNullOrEmpty(medicalServiceName))
				{
					text += " (" + medicalServiceName + ")";
				}
				return text;
			}
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

	public class AppointmentItemInsuranceInfoConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var appointment = (AppointmentsSchedulerViewModel.HospitalAppointment)value;
			return LookupDataProvider.FindInsuranceProvider(appointment.Entity.InsuranceProviderRowId)?.Code;
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

	public class AppointmentItemInsuranceInfoVisibleConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var appointment = (AppointmentsSchedulerViewModel.HospitalAppointment)value;
			return appointment.Entity.InsuranceProviderRowId == null ? "Hidden" : "Visible";
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}


	public class AppointmentItemTextDecorationConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var appointment = (AppointmentsSchedulerViewModel.HospitalAppointment)value;
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

	public class AppointmentItemTextFontStyleConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var appointment = (AppointmentsSchedulerViewModel.HospitalAppointment)value;
			//if (appointment.Entity.Completed)
			if (appointment.Entity.InInvoice)
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

	public class AppointmentDayInfoConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				if (values[0] == DependencyProperty.UnsetValue) return null;
				var date = (DateTime)values[0];
				var resourceId = values[1];
				//var daysInfo = (AppointmentsSchedulerViewModel.DaysInfoClass)values[2];
				var daysInfo = values[2] as AppointmentsSchedulerViewModel.DaysInfoClass;
				if (daysInfo == null) return null;

				//if (resourceId is Guid && (Guid)resourceId == default(Guid))
				//{
				//	var a = resourceId;
				//}

				var serviceProviderRowIds =
					(resourceId == Xtra.EmptyResourceId.Id || resourceId.Equals(default(Guid))) ?
					daysInfo.DoctorInfo.Select(q => q.Key).ToArray() :
					new[] { (Guid)resourceId };

				if (parameter.Equals("AppointmentDayText"))
				{
					var vals = serviceProviderRowIds.Select(q => daysInfo.GetAppointmentCountText(q, date)).ToArray();
					var text = string.Join(";", vals);
					return text;
				}
				else if (parameter.Equals("AppointmentDayForeground"))
				{
					var isMaximum = serviceProviderRowIds.Where(q => daysInfo.GetAppointmentCountIsMaximum(q, date)).Any();
					return isMaximum ? ConvertFunc.ToBrush("Red") : ConvertFunc.ToBrush("Black");
				}
				else if (parameter.Equals("Background"))
				{
					var serviceProviderRowId = serviceProviderRowIds[0];
					var dayStatus = daysInfo.GetDayStatus(serviceProviderRowId, date);
					Brush background = Brushes.Transparent;
					if (dayStatus == AppointmentsSchedulerViewModel.DayStatus.Holiday)
					{
						return Application.Current.FindResource("AppointmentDayBackground_Holiday");
					}
					else if (dayStatus == AppointmentsSchedulerViewModel.DayStatus.Work)
					{
						return Application.Current.FindResource("AppointmentDayBackground_Work");
					}
					else if (dayStatus == AppointmentsSchedulerViewModel.DayStatus.Nowork)
					{
						return Application.Current.FindResource("AppointmentDayBackground_Nowork");
					}
					else throw new ArgumentException();
				}
				else throw new ArgumentException();
			}
			catch(Exception ex)
			{
				//var txt = (values[3] as System.Windows.Controls.TextBlock);
				//var pars = DevExpress.Mvvm.UI.LayoutTreeHelper.GetVisualParents(txt).ToArray();
				//LogicalTreeHelper.GetParent(txt);
				//VisualTreeHelper.GetParent
				throw new AggregateException(ex);
				//return null;
			}
		}

		public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new System.NotImplementedException();
		}
	}




}
