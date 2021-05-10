using Profibiz.PracticeManager.Patients.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Profibiz.PracticeManager.Patients.Views
{
    public partial class InvoiceOneView
	{
        public InvoiceOneView()
        {
            InitializeComponent();
		}

		public bool HideCoordinations
		{
			get { return true; }
			set
			{
				if (value) layoutCoordinations.Visibility = Visibility.Collapsed;
			}
		}

		public bool HidePayments
		{
			get { return true; }
			set
			{
				if (value) layoutPayments.Visibility = Visibility.Collapsed;
			}
		}

		public bool HideRefunds
		{
			get { return true; }
			set
			{
				if (value) layoutRefunds.Visibility = Visibility.Collapsed;
			}
		}


		

	}
}
