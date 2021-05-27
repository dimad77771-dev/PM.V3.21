using System.Windows.Controls;

namespace Profibiz.PracticeManager.Patients.Views
{
	public partial class FormDocumentView
	{
		public FormDocumentView()
		{
			InitializeComponent();
			//var viewmodel = this.DataContext as ViewModels.FormDocumentViewModel;
			//richEdit.ReplaceService(viewmodel.userService);
			ribbonControl1.SelectedPage = pageHome;
		}
	}
}
