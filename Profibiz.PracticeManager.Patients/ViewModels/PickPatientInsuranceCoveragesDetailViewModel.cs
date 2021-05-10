using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Profibiz.PracticeManager.Model;
using Profibiz.PracticeManager.Infrastructure;
using Prism.Interactivity.InteractionRequest;
using System.Collections.ObjectModel;
using DevExpress.DevAV.Common;
using DevExpress.Mvvm.POCO;
using System.ComponentModel;
using AutoMapper;
using Newtonsoft.Json;
using PropertyChanged;
using Profibiz.PracticeManager.InfrastructureExt.Common;
using Profibiz.PracticeManager.Patients.BusinessService;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class PickPatientInsuranceCoveragesDetailViewModel : ViewModelBase 
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion

		public virtual Patient PatientEntity { get; set; }
		public virtual ObservableCollection<RowModel> InsuranceCoverageEntities { get; set; }
		public virtual RowModel InsuranceCoverageSelectedEntity { get; set; }

		public class RowModel
		{
			public InsuranceCoverage Entity { get; set; }
			public InsuranceCoverageViewModel ViewModel9999 { get; set; }
		}

		public PickPatientInsuranceCoveragesDetailViewModel() : base()
		{
		}


		public async Task LoadData(Guid patientRowId)
		{
			ShowWaitIndicator.Show();

			PatientEntity = await businessService.GetPatient(patientRowId);
			InsuranceCoverageEntities = PatientEntity.InsuranceCoverages.Select(q => new RowModel { Entity = q }).ToObservableCollection();

			var tasks = new List<Task>();
			foreach(var row in InsuranceCoverageEntities)
			{
				var vm = row.ViewModel9999 = new InsuranceCoverageViewModel();
				vm.ViewMode = InsuranceCoverageViewModel.ViewModeEnum.Short;
				vm.OpenParam = new InsuranceCoverageViewModel.OpenParams
				{
					RowId = row.Entity.RowId
				};
				tasks.Add(vm.LoadData());
			}
			await Task.WhenAll(tasks.ToArray());

			ShowWaitIndicator.Hide();
		}



	}

}
