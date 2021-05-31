using Profibiz.PracticeManager.Patients.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Profibiz.PracticeManager.Patients.Views
{
    public partial class FormDocmodelView
	{
        public FormDocmodelView()
        {
            InitializeComponent();
			(this.DataContext as FormDocmodelViewModel).MainGrid = mainGrid;
			(this.DataContext as FormDocmodelViewModel).MainView = this;
		}
	}
}
