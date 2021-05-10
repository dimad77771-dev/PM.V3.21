using Profibiz.PracticeManager.Navigation.Views;
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

namespace Profibiz.PracticeManager.Navigation.Views
{
    public partial class LeftNavigationPanel : UserControl
    {
        public LeftNavigationPanel()
        {
			InitializeComponent();
			//navBarControl.View.ItemSelecting += View_ItemSelecting;
			//navBarControl.View.ItemSelected += View_ItemSelected;
			//navBarControl.View.ActiveGroupChanging += View_ActiveGroupChanging;
		}

		//private void View_ActiveGroupChanging(object sender, DevExpress.Xpf.NavBar.NavBarActiveGroupChangingEventArgs e)
		//{
		//	//e.Cancel = true;
		//}

		//private void View_ItemSelected(object sender, DevExpress.Xpf.NavBar.NavBarItemSelectedEventArgs e)
		//{
		//	//throw new NotImplementedException();
		//}

		//private void View_ItemSelecting(object sender, DevExpress.Xpf.NavBar.NavBarItemSelectingEventArgs e)
		//{
		//	if (e.Source == null)
		//	{
		//		e.Cancel = true;
		//	}
		//	//throw new NotImplementedException();
		//}
	}
}
