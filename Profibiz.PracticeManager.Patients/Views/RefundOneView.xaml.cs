using DevExpress.Xpf.Grid;
using System.Windows;
using System.Windows.Controls;

namespace Profibiz.PracticeManager.Patients.Views
{
    public partial class RefundOneView
	{
        public RefundOneView()
        {
            InitializeComponent();

			((GridViewBase)gridControl1.View).ShowingEditor += PaymentOneView_ShowingEditor;
		}

		private void PaymentOneView_ShowingEditor(object sender, ShowingEditorEventArgs e)
		{
			if (e.Column.ReadOnly)
			{
				e.Cancel = true;
			}
		}
	}
}
