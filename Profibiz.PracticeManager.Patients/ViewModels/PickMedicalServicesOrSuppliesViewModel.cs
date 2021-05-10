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
	public class PickMedicalServicesOrSuppliesViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		#endregion
		public virtual ObservableCollection<MedicalServicesOrSupply> Entities { get; set; }
		public virtual MedicalServicesOrSupply SelectedEntity { get; set; }
		public virtual OpenParams OpenParam { get; set; }
		public virtual String WindowTitle { get; set; }
		public virtual String NewRowButtonText { get; set; }
		public virtual Boolean AutoExpandAllNodes { get; set; }
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();


	


		public PickMedicalServicesOrSuppliesViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
			MessengerHelper.Register<MsgRowChange<MedicalServicesOrSupply>>(this, OnMsgRowChange);
		}

		
		public async void OnOpen(OpenParams param)
		{
			OpenParam = param;
			await LoadData();
		}

		public void Pick(MedicalServicesOrSupply entity)
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
			DispatcherUIHelper.Run(() =>
			{
                ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
                {
                    ViewCode = ViewCodes.OneMedicalServiceView,
                    Param = OpenParam.PickMode == PickModeEnum.PickSupply ? "IsNew;ItemType=" + TypeHelper.MedicalItemType.Supply :
                            OpenParam.PickMode == PickModeEnum.PickService ? "IsNew;ItemType=" + TypeHelper.MedicalItemType.Service :
                            "IsNew",
                });

			});
		}

		void OnMsgRowChange(MsgRowChange<MedicalServicesOrSupply> msg)
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

			IEnumerable< MedicalServicesOrSupply> rows = LookupDataProvider.Instance.MedicalServices;
			if (OpenParam.PickMode == PickModeEnum.PickService)
			{
				WindowTitle = "Choose Service";
				NewRowButtonText = "New Service";
				//AutoExpandAllNodes = false;
				rows = rows.Where(q => q.ItemType == TypeHelper.MedicalItemType.Service).OrderBy(q => q.Name).ThenBy(q => q.Model).ThenBy(q => q.Size);
			}
			else if (OpenParam.PickMode == PickModeEnum.PickSupply)
			{
				WindowTitle = "Choose Supply";
				NewRowButtonText = "New Supply";
				//AutoExpandAllNodes = true;
				rows = rows.Where(q => q.ItemType == TypeHelper.MedicalItemType.Supply);
			}
			else throw new ArgumentException();

			Entities = Mapper.Map<ObservableCollection<MedicalServicesOrSupply>>(rows);
			BehaviorGridConrol.FocuseSearchControl();

			ShowWaitIndicator.Hide();
		}


		public class OpenParams
		{
			public PickModeEnum PickMode { get; set; }
			public TaskCompletionSource<ReturnParams> TaskSource { get; set; }
		}
		public enum PickModeEnum { PickService, PickSupply }


		public class ReturnParams
		{
			public Boolean IsSuccess { get; set; }
			public MedicalServicesOrSupply PickRow { get; set; }
		}

		public static Task<ReturnParams> PickRow(
			InteractionRequest<ShowDXWindowsActionParam> showDXWindowsInteractionRequest,
			PickModeEnum pickMode)
		{
			var tcs = new TaskCompletionSource<ReturnParams>();

			showDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.PickMedicalServicesOrSuppliesView,
				Param = new OpenParams
				{
					PickMode = pickMode,
					TaskSource = tcs,
				},
			});


			return tcs.Task;
		}

	}
}	
