using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Profibiz.PracticeManager.Infrastructure;
using Profibiz.PracticeManager.Patients.BusinessService;
using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class AppointmentsSchedulerPanelViewModel : ViewModelBase, ILeftPanelViewModel
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		#endregion

		public AppointmentsSchedulerViewModel SubViewModel1 { get; set; }
		public AppointmentsSchedulerViewModel SubViewModel2 { get; set; }
		public Int32 SelectedIndex { get; set; }
		


		public AppointmentsSchedulerPanelViewModel() : base()
		{
		}

		public void Init() { }
	

		public void OnOpen(string parm)
		{
			var openParms = QueryHelper.ParseString(parm);
			DispatcherUIHelper.Run(async () =>
			{
				await LoadData();
			});
		}
		


		async Task LoadData()
		{
			await OnSelectedIndexChangedCore();

			//SubViewModel1.IsMainRibbonShow = true;
			//SubViewModel2.IsMainRibbonShow = false;

			//await SubViewModel1.OnOpen("ViewMode=AppointmentBooks");
			//await SubViewModel2.OnOpen("ViewMode=InsuranceGroups");


			//ShowWaitIndicator.Show();

			//ShowWaitIndicator.Hide();

			DXSplashScreenHelper.Hide();
		}

		bool isInit1, isInit2;
		async Task OnSelectedIndexChangedCore()
		{
			if (SelectedIndex == 0 && !isInit1)
			{
				await SubViewModel1.OnOpen("ViewMode=AppointmentBooks");
				isInit1 = true;
			}

			if (SelectedIndex == 1 && !isInit2)
			{
				await SubViewModel2.OnOpen("ViewMode=InsuranceGroups");
				isInit2 = true;
			}

			SubViewModel1.IsMainRibbonShow = (SelectedIndex == 0);
			SubViewModel2.IsMainRibbonShow = (SelectedIndex == 1);
		}

		protected void OnSelectedIndexChanged()
		{
			DispatcherUIHelper.Run(async () =>
			{
				await OnSelectedIndexChangedCore();
			});
		}
	}
}	
