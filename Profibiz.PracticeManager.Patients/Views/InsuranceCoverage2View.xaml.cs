using System.Linq;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DevExpress.Xpf.Grid;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Xpf.Utils;
using Profibiz.PracticeManager.Infrastructure;
using System.Diagnostics;

namespace Profibiz.PracticeManager.Patients.Views
{
    public partial class InsuranceCoverage2View
	{
        public InsuranceCoverage2View()
        {
			InitializeComponent();

			tableViewGridControl.PreviewKeyDown += TableViewGridControl_PreviewKeyDown;

			//DispatcherUIHelper.Run(async () =>
			//{
			//	await Task.Delay(8000);
			//	Debug.WriteLine("AAAAAAAAA");
			//	var bb = Keyboard.FocusedElement;
			//	Debug.WriteLine("DDDDDDDD=" + bb.GetType().FullName);
			//	var s1 = string.Join("\n", LayoutTreeHelper.GetVisualParents(bb as DependencyObject).Select(q => q.GetType().FullName + ";" + (q as FrameworkElement)?.Name).ToArray());
			//	Debug.WriteLine("E=" + s1);
			//});
		}

		private void TableViewGridControl_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.Tab)
			{
				var request = new TraversalRequest(KeyboardHelper.IsShiftPressed ? FocusNavigationDirection.Previous : FocusNavigationDirection.Next);
				var elementWithFocus = Keyboard.FocusedElement as UIElement;
				if (elementWithFocus != null)
				{
					elementWithFocus.MoveFocus(request);
				}
			}
		}
	}
}
