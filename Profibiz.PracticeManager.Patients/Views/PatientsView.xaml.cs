using System.Diagnostics;
using System.Windows.Controls;

namespace Profibiz.PracticeManager.Patients.Views
{
    public partial class PatientsView : UserControl
    {
        public PatientsView()
        {
            InitializeComponent();
			//tableViewGridControl.FilterChanged += TableViewGridControl_FilterChanged;
		}

		//private void TableViewGridControl_FilterChanged(object sender, System.Windows.RoutedEventArgs e)
		//{
		//	var Control = tableViewGridControl;
		//	var a1 = Control.FilterCriteria;
		//	var a2 = Control.FilteredComponent;
		//	var a3 = Control.FilterString;

		//	Debug.WriteLine("===========================================================");
		//	var tv = ((DevExpress.Xpf.Grid.TreeListView)Control.View);
		//	DevExpress.Xpf.Grid.TreeListNodeIterator nodeIterator = new DevExpress.Xpf.Grid.TreeListNodeIterator(tv.Nodes, false);
		//	foreach (var node in nodeIterator)
		//	{
		//		var a = node.Content.ToString();
		//		Debug.WriteLine("Z=" + a + "=" + node.IsFiltered);
		//		var b = node.IsFiltered;
		//	}
		//	Debug.WriteLine("===========================================================");

		//	Infrastructure.DispatcherUIHelper.Run(() => tableViewGridControl.RefreshData());
		//}
	}
}
