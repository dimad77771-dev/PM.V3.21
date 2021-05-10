using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using Profibiz.PracticeManager.Model;
using DevExpress.DevAV.Common;
using System.Collections.ObjectModel;
using Prism.Interactivity.InteractionRequest;
using Profibiz.PracticeManager.Infrastructure;
using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using AutoMapper;
using Prism.Regions;
using Autofac;
using System.Collections.Specialized;
using Profibiz.PracticeManager.InfrastructureExt.Common;
using Microsoft.Practices.ServiceLocation;
using System.ComponentModel;
using System.Collections;
using DevExpress.Mvvm.UI;
using System.Diagnostics;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;
using DevExpress.Xpf.Editors.DateNavigator.Controls;
using System.Windows;
using System.Windows.Controls;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class PickAppointmentMultiDateViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		#endregion
		public virtual ObservableCollection<DateTime> SelectedDates { get; set; } = new ObservableCollection<DateTime>();
		//public virtual ObservableCollection<DateTime> VacationDates { get; set; } = new ObservableCollection<DateTime>();
		public virtual Guid? ServiceProviderRowId { get; set; }
		public virtual AppointmentsSchedulerViewModel.DaysInfoClass DaysInfo { get; set; }
		public virtual OpenParams OpenParam { get; set; }
		public virtual String WindowTitle { get; set; }



		public PickAppointmentMultiDateViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
		}

		
		public async void OnOpen(OpenParams param)
		{
			OpenParam = param;
			await LoadData();
		}


		public void Submit()
		{
			var ret = new ReturnParams
			{
				IsSuccess = true,
				SelectedDates = SelectedDates.ToList(),
			};
			OpenParam.TaskSource.SetResult(ret);
			CloseInteractionRequest.Raise(null);
		}
		public bool CanSubmit()
		{
			return SelectedDates.Any();
		}

		public void Cancel()
		{
			CloseInteractionRequest.Raise(null);
		}
		public void ClosingEvent(CancelEventArgs arg)
		{
			if (OpenParam.TaskSource.Task.Status != TaskStatus.RanToCompletion)
			{
				var ret = new ReturnParams { IsSuccess = false };
				OpenParam.TaskSource.SetResult(ret);
			}
		}





		async Task LoadData()
		{
			ShowWaitIndicator.Show();

			if (OpenParam.PickMode == PickModeEnum.Main)
			{
				WindowTitle = "Select Appointment Dates";
			}
			else throw new ArgumentException();

			ServiceProviderRowId = OpenParam.ServiceProviderRowId;
			DaysInfo = OpenParam.DaysInfo;
			SelectedDates = OpenParam.SelectedDates.ToObservableCollection();
			//if (ServiceProviderRowId != null)
			//{
			//	var vrows = await businessService.GetCalendarEventList(serviceProviderRowId: ServiceProviderRowId, isVacation: true);
			//	VacationDates = vrows.Select(q => q.Start).Distinct().ToObservableCollection();
			//}


			ShowWaitIndicator.Hide();
		}

		public void ToolTipOpening(RoutedEventArgs e)
		{
			var grid = (Grid)e.OriginalSource;
			var dateNavigatorCalendarCellButton = LayoutTreeHelper.GetVisualParents(grid).OfType<DateNavigatorCalendarCellButton>().FirstOrDefault();
			var dat = DateNavigatorCalendar.GetDateTime(dateNavigatorCalendarCellButton);
			grid.ToolTip = "Waiting...";
			DispatcherUIHelper.Run(async () =>
			{
				//await Task.Delay(2000);
				var arows = await businessService.GetAppointmentInsuranceProviderDayInfo("dat=" + dat.ToWebQuery() + "&serviceProviderRowId=" + ServiceProviderRowId.ToWebQuery());
				var lines = arows.OrderBy(q => q.InsuranceProviderCode).Select(q => q.InsuranceProviderCode + ": " + q.Count).ToArray();
				var str = lines.Any() ? String.Join("\n", lines) : "-";
				grid.ToolTip = str;
			});

			//return;
			//var zz = DateNavigatorCalendar.GetDateTime((Grid)e.OriginalSource);

			//var grid = (((System.Windows.RoutedEventArgs)e).OriginalSource as System.Windows.Controls.Grid);
			//if (grid != null)
			//{
			//	var templatedParent = grid.TemplatedParent;
			//	int day;
			//	var dateNavigatorCalendar = LayoutTreeHelper.GetVisualParents(grid).OfType<DevExpress.Xpf.Editors.DateNavigator.Controls.DateNavigatorCalendar>().FirstOrDefault();
			//	if (dateNavigatorCalendar != null)
			//	{
			//		if (Int32.TryParse((templatedParent as System.Windows.Controls.ContentControl).Content.ToString(), out day))
			//		{
			//			var dt = dateNavigatorCalendar.DateTime;
			//			dt = dt.AddDays(day - 1);
			//			//grid.ToolTip = dt.ToLongDateString();
			//			grid.ToolTip = "Waiting...";
			//			DispatcherUIHelper.Run(async () =>
			//			{
			//				//await Task.Delay(2000);
			//				var arows = await businessService.GetAppointmentInsuranceProviderDayInfo("dat=" + dt.ToWebQuery());
			//				var lines = arows.OrderBy(q => q.InsuranceProviderCode).Select(q => q.InsuranceProviderCode + ": " + q.Count).ToArray();
			//				var str = lines.Any() ? String.Join("\n", lines) : "-";
			//				grid.ToolTip = str;
			//			});
			//		}
			//	}
			//}
		}


		public class OpenParams
		{
			public PickModeEnum PickMode { get; set; }
			public IEnumerable<DateTime> SelectedDates { get; set; }
			public AppointmentsSchedulerViewModel.DaysInfoClass DaysInfo { get; set; }
			public Guid? ServiceProviderRowId { get; set; }
			public TaskCompletionSource<ReturnParams> TaskSource { get; set; }
			public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; }
	}
		public enum PickModeEnum { Main }


		public class ReturnParams
		{
			public Boolean IsSuccess { get; set; }
			public List<DateTime> SelectedDates { get; set; }
		}

		public static Task<ReturnParams> Pick(OpenParams parm)
		{
			var tcs = new TaskCompletionSource<ReturnParams>();
			parm.TaskSource = tcs;

			parm.ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.PickAppointmentMultiDateView,
				Param = parm,
			});

			return tcs.Task;
		}
	}

	//public class PickAppointmentMultiDateViewModel_TextStyleConverter : BaseViewModelConverter
	//{
	//	public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	//	{
	//		var a = value;
	//		//return value;
	//		var dt = ((DateTime)value).Date;
	//		return dt.Day % 2 == 0 ? Brushes.Yellow : Brushes.Red;
	//	}
	//}

	public class PickAppointmentMultiDateViewModel_TextStyleConverter2 : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var date = ((DateTime)values[0]).Date;
			var daysInfo = (AppointmentsSchedulerViewModel.DaysInfoClass)values[1];
			var serviceProviderRowId = (Guid)values[2];
			var serviceProvider = LookupDataProvider.FindServiceProvider(serviceProviderRowId);

			var dayStatus = daysInfo.GetDayStatus(serviceProviderRowId, date);
			var isMaximum = daysInfo.GetAppointmentCountIsMaximum(serviceProviderRowId, date);

			var foreground =
				dayStatus == AppointmentsSchedulerViewModel.DayStatus.Holiday ? ConvertFunc.ToBrush("#CC3333") :
				dayStatus == AppointmentsSchedulerViewModel.DayStatus.Work ? ConvertFunc.ToBrush("#007F0E") :
				dayStatus == AppointmentsSchedulerViewModel.DayStatus.Nowork ? ConvertFunc.ToBrush("#262626") :
				LogicalException.Throw<Brush>();
			var fontWeight = 
				dayStatus == AppointmentsSchedulerViewModel.DayStatus.Work ? FontWeights.Bold : 
				FontWeights.Normal;
			var background =
				isMaximum ? ConvertFunc.ToBrush("#80FFD800") :
				Brushes.Transparent;


			if (parameter.ToString() == "Foreground")
			{
				return foreground;
			}
			else if (parameter.ToString() == "FontWeight")
			{
				return fontWeight;
			}
			else if (parameter.ToString() == "Background")
			{
				return background;
			}
			else throw new ArgumentException();

			//Brush foreground = ConvertFunc.ToBrush("#262626");
			//Brush background = Brushes.Transparent;
			//FontWeight fontWeight = FontWeights.Normal;
			//if (vacationDates.Contains(dt) || LookupDataProvider.IsPublicHoliday(dt))
			//{
			//	foreground = ConvertFunc.ToBrush("#CC3333");
			//}
			//else if (serviceProvider != null && serviceProvider.GetWorkWeekDays().Contains(dt.DayOfWeek))
			//{
			//	fontWeight = FontWeights.Bold;
			//}
			//else if (HolidaysFunc.IsHoliday(dt))
			//{
			//	foreground = ConvertFunc.ToBrush("#CC3333");
			//}

			//if (dt.Day % 3 == 0)
			//{
			//	background = Brushes.Yellow;
			//}


			//if (parameter.ToString() == "Foreground")
			//{
			//	return foreground;
			//}
			//else if (parameter.ToString() == "FontWeight")
			//{
			//	return fontWeight;
			//}
			//else if (parameter.ToString() == "Background")
			//{
			//	return background;
			//}
			//else throw new ArgumentException();
		}

		public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new System.NotImplementedException();
		}
	}
}	
