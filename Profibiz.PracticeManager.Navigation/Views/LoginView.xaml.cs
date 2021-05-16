using Profibiz.PracticeManager.Navigation.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Profibiz.PracticeManager.Navigation.Views
{
	public partial class LoginView
	{
		public LoginView()
		{
			InitializeComponent();
			this.Closed += LoginView_Closed;
		}

		private void LoginView_Closed(object sender, System.EventArgs e)
		{
			if (!ViewModel.IsConnect)
			{
				Application.Current.Shutdown();
				Environment.Exit(0);
			}
		}

		async private void connectButton_Click(object sender, RoutedEventArgs e)
		{
			await ViewModel.OnConnect();
		}

		private void closeButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}


		LoginViewModel ViewModel => DataContext as LoginViewModel;
	}
}
