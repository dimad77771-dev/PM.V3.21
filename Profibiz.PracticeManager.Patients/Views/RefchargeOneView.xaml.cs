using DevExpress.Xpf.Grid;
using System.Windows;
using System.Windows.Controls;

namespace Profibiz.PracticeManager.Patients.Views
{
	public partial class RefchargeOneView
	{
		public RefchargeOneView()
		{
			InitializeComponent();

			((GridViewBase)gridControl1.View).ShowingEditor += PaychargeOneView_ShowingEditor;
		}

		private void PaychargeOneView_ShowingEditor(object sender, ShowingEditorEventArgs e)
		{
			if (e.Column.ReadOnly)
			{
				e.Cancel = true;
			}
		}
	}
}
