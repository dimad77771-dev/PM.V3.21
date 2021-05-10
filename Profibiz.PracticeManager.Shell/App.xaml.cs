using DevExpress.Xpf.Core;
using Profibiz.PracticeManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Profibiz.PracticeManager.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		

		protected override void OnStartup(StartupEventArgs e)
        {
			//decimal x = 12345678;
			////var b = x.ToString("0.#");
			//var b = x.ToString("#,##0.##");


			base.OnStartup(e);
			CustimizeCultureInfo();
			//if (!System.Diagnostics.Debugger.IsAttached)
			{
				this.DispatcherUnhandledException += App_DispatcherUnhandledException;
				AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			}
			ApplicationThemeHelper.ApplicationThemeName = Theme.Office2013DarkGray.Name;

			NLog.Init();
			NLog.Trace("Start App...");
			
			//NLog.Trace("TryStartNoGCRegion=" + GC.TryStartNoGCRegion(100000));
			//DispatcherUIHelper.Run(async () => await UnhandledExceptionProccesing.SendErrorToServer("eeeeeeeeeeee1111111"));

			XamlSpyServiceStart();
			if (!System.Diagnostics.Debugger.IsAttached)
			{
                DXSplashScreen.Show<SplashScreenWindow>();
			}
			//MainWindow.Visibility = Visibility.Hidden;
			//MainWindow.WindowState = WindowState.Minimized;

			//DispatcherUIHelper.Run(() =>
			//{
			//	MainWindow.Visibility = Visibility.Hidden;
			//});
			//var z1 = System.Globalization.DateTimeFormatInfo.InvariantInfo.FirstDayOfWeek;
			//var z2 = System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek;
			//var z19 = System.Globalization.DateTimeFormatInfo.InvariantInfo.CalendarWeekRule;
			//var z20 = System.Globalization.DateTimeFormatInfo.CurrentInfo.CalendarWeekRule;

			//Patients.ViewModels.PrintdocMultiRowsWriter a = new Patients.ViewModels.PrintdocMultiRowsWriter();
			//a.Text = @"  234567    012-345  89  012  ";
			//a.Run();


			//сразу же - явное создание некоторых ресурсов
			this.FindResource("lookupDataProvider");
			//var zz = this.FindResource("popupExtentedContentTemplate");
			//var zz2 = this.FindResource("comboBoxProfessionalAssociations");

			var bs = new Bootstrapper();
            bs.Run();
			MainWindow.Visibility = Visibility.Hidden;
			MainWindow.WindowState = WindowState.Minimized;

		
			NLog.vv();
		}



		//private bool in_App_DispatcherUnhandledException;
		//private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		//{
		//	//if (in_App_DispatcherUnhandledException)
		//	//{
		//	//	DispatcherUIHelper.Run(() =>
		//	//	{
		//	//		throw new AggregateException(e.Exception);
		//	//	});
		//	//}
		//	e.Handled = true;
		//	in_App_DispatcherUnhandledException = true;
		//	throw e.Exception;
		//}

		bool isProcessException = false;
		object lockProcessException = new object();
		private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			lock (lockProcessException)
			{
				if (isProcessException) return;
				isProcessException = true;
				ExceptionLogger.ProcessException(e.Exception);
			}
		}
		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			lock (lockProcessException)
			{
				if (isProcessException) return;
				isProcessException = true;
				ExceptionLogger.ProcessException(e.ExceptionObject);
			}
		}




		private object xamlSpyService;
		private void XamlSpyServiceStart()
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				var assemblyFile = @"C:\Program Files (x86)\First Floor Software\XAML Spy\Libraries\WPF\XamlSpy.WPF4.dll";
				if (File.Exists(assemblyFile))
				{
					var assembly = Assembly.LoadFrom(assemblyFile);
					var type = assembly.GetType("FirstFloor.XamlSpy.XamlSpyService");
					var prop = type.GetProperty("Password");
					xamlSpyService = Activator.CreateInstance(type);
					prop.SetValue(xamlSpyService, "21551");
					var method = type.GetMethod("StartService");
					method.Invoke(xamlSpyService, null);
					//this.service = new FirstFloor.XamlSpy.XamlSpyService()
					//{
					//	Password = "01345",
					//};
					//this.service.StartService();
				}
			}
		}


		private void CustimizeCultureInfo()
		{
			var culture = (System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture.Clone();
			culture.DateTimeFormat.ShortDatePattern = @"MM\/dd\/yyyy";
			culture.DateTimeFormat.ShortTimePattern = @"HH:mm";
			culture.NumberFormat.NumberDecimalSeparator = @".";
			culture.NumberFormat.NumberGroupSeparator = @",";
			System.Globalization.CultureInfo.CurrentCulture = culture;
		}
	}
}

