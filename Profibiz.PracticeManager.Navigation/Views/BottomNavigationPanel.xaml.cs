using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Practices.ServiceLocation;

namespace Profibiz.PracticeManager.Navigation.Views
{
	public partial class BottomNavigationPanel : UserControl
	{
		public BottomNavigationPanel()
		{
			InitializeComponent();

			var leftNavigationPanel = ServiceLocator.Current.GetInstance<LeftNavigationPanel>();
			officeNavigationBar.NavigationClient = leftNavigationPanel.navBarControl;
			officeNavigationBar.DataContext = leftNavigationPanel.DataContext;
		}
	}
}
