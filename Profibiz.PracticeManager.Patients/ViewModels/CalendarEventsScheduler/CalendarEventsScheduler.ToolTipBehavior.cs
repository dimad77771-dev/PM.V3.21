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
//using System.Windows.Media;
using DevExpress.Mvvm.UI.Interactivity;
using System.Windows.Controls;
using System.Windows;
using DevExpress.Mvvm.UI;
using System.Windows.Media;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	public class CalendarEventsSchedulerToolTipBehavior : Behavior<ToolTip>
	{
		public object Source
		{
			get { return GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
		}
		public static readonly DependencyProperty SourceProperty =
			DependencyProperty.Register("Source", typeof(object), typeof(CalendarEventsSchedulerToolTipBehavior));


		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.Opened += OnOpened;
		}
		protected override void OnDetaching()
		{
			base.OnDetaching();
			AssociatedObject.Opened -= OnOpened;
		}

		private void OnOpened(object sender, RoutedEventArgs e)
		{
			var workTimeCell = AssociatedObject.DataContext as VisualTimeCellContent;
			if (workTimeCell == null)
			{
				AssociatedObject.Visibility = Visibility.Hidden;
				return;
			}
			var viewmodel = LayoutTreeHelper.GetVisualParents(AssociatedObject.PlacementTarget)
				.OfType<FrameworkElement>()
				.Select(q => q.DataContext as CalendarEventsSchedulerViewModel)
				.First(q => q != null);

			if (!viewmodel.IsOnePatient)
			{
				AssociatedObject.Visibility = Visibility.Hidden;
				return;
			}

			var iStart = workTimeCell.IntervalStart;
			var iFinish = workTimeCell.IntervalEnd;
			//var appointment = viewmodel.FindInIntervalAllCalendarEventInCalendarEventBook(iStart, iFinish).FirstOrDefault();

			//if (appointment != null)
			//{
			//	var model = new VisualAppointmentViewInfo
			//	{
			//		CustomViewInfo = viewmodel.CalendarEvent2HospitalCalendarEvent(appointment),
			//	};
			//	var a1 = VisualTreeHelper.GetChildrenCount(AssociatedObject);
			//	var a2 = VisualTreeHelper.GetChild(AssociatedObject, 0);
			//	var a3 = VisualTreeHelper.GetChildrenCount(a2);
			//	var a4 = VisualTreeHelper.GetChild(a2, 0);
			//	var allChilds = LayoutTreeHelper.GetVisualChildren(AssociatedObject).OfType<FrameworkElement>().ToArray();
			//	allChilds.ForEach(q => q.DataContext = model);
			//	var border = allChilds.Single(q => q.Name == "borderCalendarEventTooltip") as Border;
			//	border.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0));
			//	AssociatedObject.Visibility = Visibility.Visible;
			//}
			//else
			//{
				AssociatedObject.Visibility = Visibility.Hidden;
			//}
		}
	}
}
