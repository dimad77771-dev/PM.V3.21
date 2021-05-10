using DevExpress.Mvvm.POCO;
using Profibiz.PracticeManager.Infrastructure;
using Profibiz.PracticeManager.Patients.BusinessService;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Profibiz.PracticeManager.Shell
{
    public partial class Shell 
    {
        public Shell()
        {
			GlobalSettings.ReadSettingsFromServer2();
			InitializeComponent();

			//var textBlock = new TextBlock { Text = "ABCDEFGH", TextWrapping = TextWrapping.NoWrap };
			//textBlock.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
			//textBlock.Arrange(new Rect(textBlock.DesiredSize));
			//var bb = textBlock.ActualWidth;
			//var hh = SystemParameters.WorkArea.Height;
			//var iii = SystemParameters.WorkArea;

			var viwemodel = ViewModelSource.Create<ShellViewModel>();
			this.DataContext = viwemodel;
			viwemodel.ToolbarRegionContentControl = ribbonControl;
			NLog.vv();
		}
    }
}
