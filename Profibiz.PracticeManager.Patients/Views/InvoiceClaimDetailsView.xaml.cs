using Profibiz.PracticeManager.Infrastructure;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Profibiz.PracticeManager.Patients.Views
{
	public partial class InvoiceClaimDetailsView
	{
		public InvoiceClaimDetailsView()
		{
			InitializeComponent();
			WindowInfo.CenterWindowVertical(this);
		}
	}
}
