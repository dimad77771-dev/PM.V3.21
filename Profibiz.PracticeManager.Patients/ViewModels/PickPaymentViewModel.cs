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
	public class PickPaymentViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
		#endregion
		public virtual ObservableCollection<Payment> Entities { get; set; }
		public virtual Payment SelectedEntity { get; set; }
		public virtual ObservableCollection<Payment> SelectedEntities { get; set; } = new ObservableCollection<Payment>();
		public virtual OpenParams OpenParam { get; set; }
		public virtual String WindowTitle { get; set; }
		public virtual String NewRowButtonText { get; set; }
		public virtual Boolean AutoExpandAllNodes { get; set; }
		public virtual Boolean IsMultiSelect { get; set; }
		




		public PickPaymentViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
			MessengerHelper.Register<MsgRowChange<Payment>>(this, OnMsgRowChange);
		}

		
		public async void OnOpen(OpenParams param)
		{
			OpenParam = param;
			await LoadData();
		}

		public void Pick(Payment entity)
		{
			SelectedEntity = entity;
			Submit();
		}

		public void Submit()
		{
			if (IsMultiSelect)
			{
				var ret = new ReturnParams { IsSuccess = true, PickRows = SelectedEntities.ToList() };
				OpenParam.TaskSource.SetResult(ret);
			}
			else
			{
				var ret = new ReturnParams { IsSuccess = true, PickRow = SelectedEntity };
				OpenParam.TaskSource.SetResult(ret);
			}
			CloseInteractionRequest.Raise(null);
		}
		public Boolean CanSubmit() => (IsMultiSelect ? SelectedEntities.Any() : SelectedEntity != null);

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
					Param = "IsNew",
				});

			});
		}

		void OnMsgRowChange(MsgRowChange<Payment> msg)
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

			IsMultiSelect = OpenParam.IsMultiSelect;

			var qry = "PatientRowId=" + OpenParam.PatientRowId;
			if (OpenParam.HasNoDistributedAmount)
			{
				qry += "&hasNoDistributedAmount=1";
			}
			var rows = await businessService.GetPaymentList(qry);
			if (OpenParam.PickMode == PickModeEnum.Main)
			{
				WindowTitle = "Choose Payment" + (IsMultiSelect ? "s" : "");
				NewRowButtonText = "New Payment";
			}
			else throw new ArgumentException();

			if (OpenParam.ExcludePayments != null)
			{
				rows.RemoveAll(q => OpenParam.ExcludePayments.Contains(q.RowId));
			}
			Entities = new ObservableCollection<Payment>(rows.OrderByDescending(q => q.PaymentDate));
			BehaviorGridConrol.FocuseSearchControl();

			ShowWaitIndicator.Hide();
		}


		public class OpenParams
		{
			public PickModeEnum PickMode { get; set; }
			public Guid? PatientRowId { get; set; }
			public IEnumerable<Guid> ExcludePayments { get; set; }
			public Boolean HasNoDistributedAmount { get; set; }
			public Boolean IsMultiSelect { get; set; }
			public TaskCompletionSource<ReturnParams> TaskSource { get; set; }
		}
		public enum PickModeEnum { Main }


		public class ReturnParams
		{
			public Boolean IsSuccess { get; set; }
			public Payment PickRow { get; set; }
			public List<Payment> PickRows { get; set; }
		}

		public static Task<ReturnParams> PickRow(
			InteractionRequest<ShowDXWindowsActionParam> showDXWindowsInteractionRequest,
			PickModeEnum pickMode,
            Guid? patientRowId = null,
			IEnumerable<Guid> excludePayments = null,
			Boolean hasNoDistributedAmount = false,
			Boolean isMultiSelect = false)
		{
			var tcs = new TaskCompletionSource<ReturnParams>();

			showDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.PickPaymentView,
				Param = new OpenParams
				{
					PickMode = pickMode,
                    PatientRowId = patientRowId,
					ExcludePayments = excludePayments,
					HasNoDistributedAmount = hasNoDistributedAmount,
					IsMultiSelect = isMultiSelect,
					TaskSource = tcs,
				},
			});


			return tcs.Task;
		}

	}
}	
