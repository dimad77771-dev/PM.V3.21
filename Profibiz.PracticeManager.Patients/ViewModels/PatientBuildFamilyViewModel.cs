using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using Profibiz.PracticeManager.Model;
using DevExpress.DevAV.Common;
using System.Collections.ObjectModel;
using Prism.Interactivity.InteractionRequest;
using Profibiz.PracticeManager.Infrastructure;
using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using AutoMapper;
using Prism.Regions;
using Autofac;
using System.Collections.Specialized;
using Profibiz.PracticeManager.InfrastructureExt.Common;
using Microsoft.Practices.ServiceLocation;
using System.ComponentModel;
using System.Collections;
using Profibiz.PracticeManager.Patients.BusinessService;
using System.Threading;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class PatientBuildFamilyViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		#endregion
		public virtual ObservableCollection<Patient> Entities { get; set; }
		public virtual Patient SelectedEntity { get; set; }
		public virtual ObservableCollection<Patient> SelectedEntities { get; set; } = new ObservableCollection<Patient>();
		public virtual OpenParams OpenParam { get; set; }
		public virtual String WindowTitle { get; set; }
	


		public PatientBuildFamilyViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
		}

		
		public async void OnOpen(OpenParams param)
		{
			OpenParam = param;
			await LoadData();
		}

		public void Pick(Patient entity)
		{
			SelectedEntity = entity;
			Submit();
		}

		public void Submit()
		{
			if (SelectedEntity == null) return;

			var ret = new ReturnParams
            {
                IsSuccess = true,
                PickRow = SelectedEntity,
                PickRows = SelectedEntities.ToList(),
            };
			OpenParam.TaskSource.SetResult(ret);
			CloseInteractionRequest.Raise(null);
		}
		public bool CanSubmit() => CanSubmitFlag;

		public void Cancel()
		{
			CloseInteractionRequest.Raise(null);
		}
		public void ClosingEvent(CancelEventArgs arg)
		{
			if (OpenParam.TaskSource.Task.Status != TaskStatus.RanToCompletion)
			{
				var ret = new ReturnParams { IsSuccess = false };
				OpenParam.TaskSource.SetResult(ret);
			}
		}


		async Task LoadData()
		{
			await Task.Yield();
			ShowWaitIndicator.Show();

			WindowTitle = "Build New Family";
			Entities = new ObservableCollection<Patient>(OpenParam.ListRows);
			Entities.ForEach(q => SubscribeIsSelectHeadFamilyChange(q));
			UpdateCanSubmit();

			ShowWaitIndicator.Hide();
		}

		void SubscribeIsSelectHeadFamilyChange(Patient row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(row.IsSelectHeadFamily))
				{
					if (row.IsSelectHeadFamily)
					{
						Entities.Where(q => q != row).ForEach(q => q.IsSelectHeadFamily = false);
						row.IsSelectUseHeadAddress = false;
					}
					UpdateCanSubmit();
				}
			};

		}

		bool CanSubmitFlag { get; set; }
		void UpdateCanSubmit()
		{
			CanSubmitFlag = (Entities.Count(q => q.IsSelectHeadFamily) == 1);
		}

		public class OpenParams
		{
			public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest;
			public TaskCompletionSource<ReturnParams> TaskSource { get; set; }
			public List<Patient> ListRows { get; set; }
		}


		public class ReturnParams
		{
			public Boolean IsSuccess { get; set; }
			public Patient PickRow { get; set; }
            public List<Patient> PickRows { get; set; }
        }


		public static Task<ReturnParams> Run(OpenParams parms)
		{
			var tcs = new TaskCompletionSource<ReturnParams>();
			parms.TaskSource = tcs;

			parms.ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.PatientBuildFamilyView,
				Param = parms,
			});


			return tcs.Task;
		}

	}
}	
