using System.Windows;
using DevExpress.Xpf.Core;
using System.Diagnostics;
using System;

namespace Profibiz.PracticeManager.Shell
{
	public partial class SplashScreenWindow : Window, ISplashScreen
	{
		public SplashScreenWindow()
		{
			InitializeComponent();
            //System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            //Version.Text = DateTime.Today.ToString("yyyyMMdd") + "." + fvi.FileVersion + ".4.6.2";

            //this.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Hidden : Visibility.Visible;
        }

        #region ISplashScreen
        void ISplashScreen.Progress(double value)
		{
			progressBar.Value = value;
		}
		void ISplashScreen.CloseSplashScreen()
		{
			this.Close();
		}
		void ISplashScreen.SetProgressState(bool isIndeterminate)
		{
		}
		#endregion
	}
}
