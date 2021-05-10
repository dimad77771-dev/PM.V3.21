using DevExpress.Xpf.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class DXSplashScreenHelper
	{
		public static void Hide()
		{
			if (DXSplashScreen.IsActive)
			{
				DXSplashScreen.Close();
			}

			var mainWindow = System.Windows.Application.Current.MainWindow;
			if (mainWindow.Visibility != System.Windows.Visibility.Visible)
			{
				mainWindow.Visibility = System.Windows.Visibility.Visible;
				mainWindow.WindowState = System.Windows.WindowState.Maximized;
				mainWindow.Activate();

				DispatcherUIHelper.Run(async () =>
				{
					await Task.Yield();
					await Task.Yield();
					var toolbarRegionContentControl = ShellViewModel.Instance.ToolbarRegionContentControl;
					toolbarRegionContentControl.Height = toolbarRegionContentControl.ActualHeight;
					Debug.WriteLine("toolbarRegionContentControl.ActualHeight2=" + toolbarRegionContentControl.Height);
					Debug.WriteLine("toolbarRegionContentControl.ActualHeight2=" + toolbarRegionContentControl.ActualHeight);


					GlobalServiceHelper.Start(GlobalServiceCodes.CalendarEventsRemindersService);
					//await Task.Delay(1000);
					//CalendarEventsRemindersService
				});
			}
		}
	}
}
