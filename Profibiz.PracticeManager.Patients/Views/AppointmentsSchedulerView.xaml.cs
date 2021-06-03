using System.Linq;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DevExpress.XtraScheduler.Services;
using DevExpress.XtraScheduler.Drawing;

namespace Profibiz.PracticeManager.Patients.Views
{
    public partial class AppointmentsSchedulerView : UserControl
    {
        public AppointmentsSchedulerView()
        {
            InitializeComponent();

			//schedulerControl1.CustomizeVisualViewInfo
			//Grid a; 			a.ToolTipOpening

			var styleSelector = (StyleSelector)FindResource("VerticalAppointmentStyleSelector");
			schedulerControl1.DayView.VerticalAppointmentStyleSelector = styleSelector;
			schedulerControl1.WorkWeekView.VerticalAppointmentStyleSelector = styleSelector;

			DataTemplate template = (DataTemplate)this.FindResource("AppointmentTooltipContentTemplate");
			schedulerControl1.DayView.AppointmentToolTipContentTemplate = template;
			schedulerControl1.WorkWeekView.AppointmentToolTipContentTemplate = template;
			schedulerControl1.WeekView.AppointmentToolTipContentTemplate = template;
			schedulerControl1.MonthView.AppointmentToolTipContentTemplate = template;
			schedulerControl1.TimelineView.AppointmentToolTipContentTemplate = template;

			var styleSelector2 = (StyleSelector)this.FindResource("HorizontalAppointmentStyleSelector");
			schedulerControl1.DayView.HorizontalAppointmentStyleSelector = styleSelector2;
			schedulerControl1.WorkWeekView.HorizontalAppointmentStyleSelector = styleSelector2;
			schedulerControl1.WeekView.HorizontalAppointmentStyleSelector = styleSelector2;
			schedulerControl1.MonthView.HorizontalAppointmentStyleSelector = styleSelector2;
			schedulerControl1.TimelineView.HorizontalAppointmentStyleSelector = styleSelector2;

			var viewmodel = DataContext as ViewModels.AppointmentsSchedulerViewModel;
			viewmodel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(viewmodel.IsSmallRibbonHide) && viewmodel.IsSmallRibbonHide)
				{
					var grid = VisualTreeHelper.GetParent(smallRibbon) as Grid;
					grid.Children.Remove(smallRibbon);
				}
			};




			var oldService = schedulerControl1.GetService<IAppointmentFormatStringService>();
			if (oldService != null)
			{
				var  newService = new MyAppointmentFormatStringService(oldService);
				schedulerControl1.RemoveService(typeof(IAppointmentFormatStringService));
				schedulerControl1.AddService(typeof(IAppointmentFormatStringService), newService);
			}








			//var grid = VisualTreeHelper.GetParent(smallRibbon) as Grid;
			//grid.Children.Remove(smallRibbon);

			//var smallRibbonPartHeaderAndTabs = smallRibbon.FindName("PART_HeaderAndTabsLayout");
			//var smallRibbonPartHeaderAndTabs = smallRibbon.GetElementByName("PART_HeaderAndTabsLayout");
			//smallRibbonPartHeaderAndTabs.Visibility = Visibility.Collapsed;
			//var cnt = VisualTreeHelper.GetChildrenCount(smallRibbon);

			//var arr = LayoutTreeHelper.GetVisualChildren(smallRibbon).ToArray();
			//arr = arr;
			//var smallRibbonPartHeaderAndTabs = LayoutTreeHelper.GetVisualChildren(smallRibbon).OfType<FrameworkElement>().First(q => q.Name == "PART_HeaderAndTabsLayout");
			//smallRibbonPartHeaderAndTabs.Visibility = Visibility.Collapsed;

			//Dispatcher.InvokeAsync(async () =>
			//{
			//	await Task.Yield();
			//	var par = VisualTreeHelper.GetParent(smallRibbon);
			//	var cnt33 = VisualTreeHelper.GetChildrenCount(smallRibbon);
			//	cnt33 = cnt33;
			//});


			//PART_HeaderAndTabsLayout

			//if (!viewmodel.IsSmallRibbonShow)
			//{
			//	//var grid = VisualTreeHelper.GetParent(smallRibbon) as Grid;
			//	//grid.Children.Remove(smallRibbon);
			//}

			//Dispatcher.InvokeAsync(async () =>
			//{
			//	await Task.Delay(10000);
			//	var a = (DependencyObject)System.Windows.Input.Mouse.DirectlyOver;
			//	while (true)
			//	{
			//		System.Diagnostics.Debug.WriteLine("aa=" + a.GetType().FullName);
			//		if (a.GetType().FullName == "DevExpress.Xpf.Scheduler.Drawing.VisualResourceHeader")
			//		{
			//			var b = (DevExpress.Xpf.Scheduler.Drawing.VisualResourceHeader)a;
			//			var c = b.Style;


			//			var c0 = (c.Setters[0] as Setter);
			//			string xaml0 = System.Windows.Markup.XamlWriter.Save(c0.Value);
			//			var c1 = (c.Setters[1] as Setter);
			//			string xaml1 = System.Windows.Markup.XamlWriter.Save(c1.Value);

			//			object defaultStyleKey = b.GetValue(FrameworkElement.DefaultStyleKeyProperty);
			//			var r2 = b.FindResource(defaultStyleKey);
			//			var rr = Application.Current.FindResource(defaultStyleKey);
			//			Style style = (Style)Application.Current.FindResource(defaultStyleKey);
			//			string xaml = System.Windows.Markup.XamlWriter.Save(style);
			//		}


			//		a = VisualTreeHelper.GetParent(a);
			//		if (a == null) break;
			//	}
			//});



			//schedulerControl1.SelectionChanged += SchedulerControl1_SelectionChanged;

			//schedulerControl1.ActiveView.SetSelection(

			//schedulerControl1.MouseDoubleClick += SchedulerControl1_MouseDoubleClick;
			//schedulerControl1.AppointmentResized += SchedulerControl1_AppointmentResized;
			//schedulerControl1.AppointmentDrop += SchedulerControl1_AppointmentDrop;
			//schedulerControl1.PopupMenuShowing += SchedulerControl1_PopupMenuShowing;


			//var k = new DevExpress.Xpf.Scheduler.ThemeKeys.SchedulerViewThemeKeyExtension();
			////k.ResourceKey = DevExpress.Xpf.Scheduler.ThemeKeys.SchedulerViewThemeKeys.VerticalAppointmentTemplate;
			//k.ResourceKey = DevExpress.Xpf.Scheduler.ThemeKeys.SchedulerViewThemeKeys.HorizontalAppointmentTemplate;
			//k.ThemeName = Theme.Office2013DarkGray.Name;
			//var v = this.FindResource(k);
			//string xaml = System.Windows.Markup.XamlWriter.Save(v);

			//var v2 = Application.Current.FindResource(k);
			//this.Resources.Remove(k);
			//this.Resources.Add(k, v2);
		}

		//private void SchedulerControl1_SelectionChanged(object sender, System.EventArgs e)
		//{
		//	var a = e;
		//}

		public class MyAppointmentFormatStringService : AppointmentFormatStringServiceWrapper
		{
			//static string TIME_FORMAT = "{0:t}";
			//static string TIME_FORMAT2 = "{0:\\-t}";
			static string TIME_FORMAT = "{0:h:mm tt}";

			public MyAppointmentFormatStringService(IAppointmentFormatStringService service) : base(service) { }
			//public override string GetVerticalAppointmentStartFormat(IAppointmentViewInfo aptViewInfo)
			//{
			//	return TIME_FORMAT;
			//}
			//public override string GetVerticalAppointmentEndFormat(IAppointmentViewInfo aptViewInfo)
			//{
			//	return TIME_FORMAT2;
			//}

			public override string GetHorizontalAppointmentEndFormat(IAppointmentViewInfo aptViewInfo)
			{
				return TIME_FORMAT;
			}
			public override string GetHorizontalAppointmentStartFormat(IAppointmentViewInfo aptViewInfo)
			{
				return TIME_FORMAT;
			}

			//public override string GetContinueItemStartFormat(IAppointmentViewInfo aptViewInfo)
			//{
			//	return TIME_FORMAT;
			//}
			//public override string GetContinueItemEndFormat(IAppointmentViewInfo aptViewInfo)
			//{
			//	return TIME_FORMAT;
			//}
		}
	}
}
