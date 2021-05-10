using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Profibiz.PracticeManager.Patients.Views
{
	public partial class ChargeoutWindowView
	{
		public static ChargeoutWindowView Instance;
		public ChargeoutWindowView()
		{
			Instance = this;
			InitializeComponent();
		}
	}
}
