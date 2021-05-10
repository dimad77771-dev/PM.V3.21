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

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class PickInsuranceCoverageViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		#endregion
		public virtual ObservableCollection<InsuranceCoverage> Entities { get; set; }
		public virtual InsuranceCoverage SelectedEntity { get; set; }
		public virtual OpenParams OpenParam { get; set; }
		public virtual String WindowTitle { get; set; }
		public virtual String NewRowButtonText { get; set; }
		public virtual Boolean AutoExpandAllNodes { get; set; }
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();


	


		public PickInsuranceCoverageViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
			MessengerHelper.Register<MsgRowChange<InsuranceCoverage>>(this, OnMsgRowChange);
		}

		
		public async void OnOpen(OpenParams param)
		{
			OpenParam = param;
			await LoadData();
		}

		public void Pick(InsuranceCoverage entity)
		{
			SelectedEntity = entity;
			Submit();
		}

		public void Submit()
		{
			if (SelectedEntity == null) return;

			var ret = new ReturnParams { IsSuccess = true, PickRow = SelectedEntity };
			OpenParam.TaskSource.SetResult(ret);
			CloseInteractionRequest.Raise(null);
		}

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


		public void NewRow()
		{
			//DispatcherUIHelper.Run(() =>
			//{
   //             ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
   //             {
   //                 ViewCode = ViewCodes.OneMedicalServiceView,
   //                 Param = OpenParam.PickMode == PickModeEnum.PickSupply ? "IsNew;ItemType=" + TypeHelper.MedicalItemType.Supply :
   //                         OpenParam.PickMode == PickModeEnum.PickService ? "IsNew;ItemType=" + TypeHelper.MedicalItemType.Service :
   //                         "IsNew",
   //             });

			//});
		}

		void OnMsgRowChange(MsgRowChange<InsuranceCoverage> msg)
		{
			MessengerHelper.RunAction(this, async () =>
			{
				await LoadData();
			});
		}


		async Task LoadData()
		{
			await Task.Yield();
			ShowWaitIndicator.Show();

			IEnumerable<InsuranceCoverage> rows = await businessService.PatientRowId2InsuranceCoverages(OpenParam.PatientRowId);
			if (OpenParam.PickMode == PickModeEnum.ForPatient)
			{
				WindowTitle = "Choose Insurance Coverage";
				//NewRowButtonText = "New Service";
			}
			else throw new ArgumentException();

			if (OpenParam.CoverageDates != null)
			{
				rows = rows.Where(q => InsuranceCoverage.InCoverageIntarvalArray(OpenParam.CoverageDates, q.CoverageStartDate, q.CoverageValidUntil));
			}

			Entities = Mapper.Map<ObservableCollection<InsuranceCoverage>>(rows);
			BehaviorGridConrol.FocuseSearchControl();

			ShowWaitIndicator.Hide();
		}


		public class OpenParams
		{
			public PickModeEnum PickMode { get; set; }
			public Guid PatientRowId { get; set; }
			public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; }
			public TaskCompletionSource<ReturnParams> TaskSource { get; set; }
			public DateTime[] CoverageDates { get; set; }
		}
		public enum PickModeEnum { ForPatient }


		public class ReturnParams
		{
			public Boolean IsSuccess { get; set; }
			public InsuranceCoverage PickRow { get; set; }
		}

		public static Task<ReturnParams> PickRow(OpenParams parm)
		{
			var tcs = new TaskCompletionSource<ReturnParams>();
			parm.TaskSource = tcs;

			parm.ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.PickInsuranceCoverageView,
				Param = parm,
			});


			return tcs.Task;
		}

	}
}	
