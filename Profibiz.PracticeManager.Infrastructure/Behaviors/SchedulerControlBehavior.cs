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
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Native;

namespace Profibiz.PracticeManager.Infrastructure
{
    public class SchedulerControlBehavior : Behavior<SchedulerControl>
	{
        public SchedulerControl Source
		{
            get { return (SchedulerControl)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
		public static readonly DependencyProperty SourceProperty =
			DependencyProperty.Register("Source", typeof(SchedulerControl), typeof(SchedulerControlBehavior));

		public SchedulerControlManager Manager
		{
			get { return (SchedulerControlManager)GetValue(ManagerProperty); }
			set { SetValue(ManagerProperty, value); }
		}
		public static readonly DependencyProperty ManagerProperty =
			DependencyProperty.Register("Manager", typeof(SchedulerControlManager), typeof(SchedulerControlBehavior), new PropertyMetadata(OnManagerChange));


		protected override void OnAttached()
		{
            base.OnAttached();
			Source = AssociatedObject;
		}

        protected override void OnDetaching()
		{
            base.OnDetaching();
        }

		public static void OnManagerChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((SchedulerControlManager)(e.NewValue)).Behavior = (SchedulerControlBehavior)d;
		}
	}

	public class SchedulerControlManager
	{
		public SchedulerControlBehavior Behavior { get; set; }
		SchedulerControl Control => (SchedulerControl)Behavior.AssociatedObject;

		public void SetSelection(DateTime start, DateTime finish, Object resourceId)
		{
			var interval = new TimeInterval(start, finish);
			var resource = Control.Storage.ResourceStorage.GetResourceById(resourceId);
			Control.ActiveView.SetSelection(interval, resource);
		}

		public void ZoomIn()
		{
			Control.ActiveView.ZoomIn();
		}

		public void ZoomOut()
		{
			Control.ActiveView.ZoomOut();
		}


		public void ddd(DevExpress.Xpf.Bars.ItemClickEventArgs e)
		{
			//var hitInfo = SchedulerHitInfo.CreateSchedulerHitInfo(Control, e.GetPosition(Control));
			//viewInfo = hitInfo.ViewInfo as DevExpress.Xpf.Scheduler.Drawing.VisualAppointmentViewInfo;
		}

		public TimeInterval SelectedInterval => Control.SelectedInterval;
		public Resource SelectedResource => Control.SelectedResource;
		public Object SelectedResourceId => Control.SelectedResource?.Id;

		public void LayoutChanged()
		{
			Control.ActiveView.LayoutChanged();
			Control.InvalidateVisual();
		}

	}
}
