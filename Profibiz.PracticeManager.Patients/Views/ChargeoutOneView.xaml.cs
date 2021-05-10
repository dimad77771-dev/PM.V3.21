using Profibiz.PracticeManager.Patients.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Profibiz.PracticeManager.Patients.Views
{
	public partial class ChargeoutOneView
	{
		public ChargeoutOneView()
		{
			InitializeComponent();
		}


		public bool HidePaycharges
		{
			get { return true; }
			set
			{
				if (value) layoutPaycharges.Visibility = Visibility.Collapsed;
			}
		}


	}
}
