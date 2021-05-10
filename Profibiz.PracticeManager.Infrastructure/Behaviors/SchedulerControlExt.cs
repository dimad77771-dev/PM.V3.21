using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using DevExpress.DevAV.ViewModels;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Grid;
using System.Diagnostics;
using System.Threading;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Scheduler;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class SchedulerControlExt
	{
		public static readonly DependencyProperty WorkDaysProperty = DependencyProperty.RegisterAttached(
				"WorkDays", typeof(WeekDays), typeof(SchedulerControlExt), new PropertyMetadata(new PropertyChangedCallback(WorkDaysPropertyChanged)));
		public static void WorkDaysPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var schedulerControl = (SchedulerControl)d;
			var value = (WeekDays)e.NewValue;

			schedulerControl.WorkDays.BeginUpdate();
			schedulerControl.WorkDays.Clear();
			schedulerControl.WorkDays.Add(value);
			schedulerControl.WorkDays.EndUpdate();
		}
		public static void SetWorkDays(DependencyObject element, WeekDays value)
		{
			element.SetValue(WorkDaysProperty, value);
		}
		public static WeekDays GetWorkDays(DependencyObject element)
		{
			return (WeekDays)element.GetValue(WorkDaysProperty);
		}


		



		public class SetSelectionArgument
		{
			public TimeInterval Interval { get; set; }
			public Resource Resource { get; set; }
		}
		public static readonly DependencyProperty SetSelection = DependencyProperty.RegisterAttached(
				"SetSelection", typeof(SetSelectionArgument), typeof(SchedulerControlExt), new PropertyMetadata(new PropertyChangedCallback(SetSelectionPropertyChanged)));
		public static void SetSelectionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var schedulerControl = (SchedulerControl)d;
			var value = (SetSelectionArgument)e.NewValue;

			schedulerControl.ActiveView.SetSelection(value.Interval, value.Resource);
		}

	}
}
