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
	public class PickPaychargeViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
		#endregion
		public virtual ObservableCollection<Paycharge> Entities { get; set; }
		public virtual Paycharge SelectedEntity { get; set; }
		public virtual ObservableCollection<Paycharge> SelectedEntities { get; set; } = new ObservableCollection<Paycharge>();
		public virtual OpenParams OpenParam { get; set; }
		public virtual String WindowTitle { get; set; }
		public virtual String NewRowButtonText { get; set; }
		public virtual Boolean AutoExpandAllNodes { get; set; }
		public virtual Boolean IsMultiSelect { get; set; }





		public PickPaychargeViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
			MessengerHelper.Register<MsgRowChange<Paycharge>>(this, OnMsgRowChange);
		}


		public async void OnOpen(OpenParams param)
		{
			OpenParam = param;
			await LoadData();
		}

		public void Pick(Paycharge entity)
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

		void OnMsgRowChange(MsgRowChange<Paycharge> msg)
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

			var qry = "сhargeoutRecipientRowId=" + OpenParam.СhargeoutRecipientRowId;
			if (OpenParam.HasNoDistributedAmount)
			{
				qry += "&hasNoDistributedAmount=1";
			}
			var rows = await businessService.GetPaychargeList(qry);
			if (OpenParam.PickMode == PickModeEnum.Main)
			{
				WindowTitle = "Choose Paycharge" + (IsMultiSelect ? "s" : "");
				NewRowButtonText = "New Paycharge";
			}
			else throw new ArgumentException();

			if (OpenParam.ExcludePaycharges != null)
			{
				rows.RemoveAll(q => OpenParam.ExcludePaycharges.Contains(q.RowId));
			}
			Entities = new ObservableCollection<Paycharge>(rows.OrderByDescending(q => q.PaychargeDate));
			BehaviorGridConrol.FocuseSearchControl();

			ShowWaitIndicator.Hide();
		}


		public class OpenParams
		{
			public PickModeEnum PickMode { get; set; }
			public Guid? СhargeoutRecipientRowId { get; set; }
			public IEnumerable<Guid> ExcludePaycharges { get; set; }
			public Boolean HasNoDistributedAmount { get; set; }
			public Boolean IsMultiSelect { get; set; }
			public TaskCompletionSource<ReturnParams> TaskSource { get; set; }
		}
		public enum PickModeEnum { Main }


		public class ReturnParams
		{
			public Boolean IsSuccess { get; set; }
			public Paycharge PickRow { get; set; }
			public List<Paycharge> PickRows { get; set; }
		}

		public static Task<ReturnParams> PickRow(
			InteractionRequest<ShowDXWindowsActionParam> showDXWindowsInteractionRequest,
			PickModeEnum pickMode,
			Guid? сhargeoutRecipientRowId = null,
			IEnumerable<Guid> excludePaycharges = null,
			Boolean hasNoDistributedAmount = false,
			Boolean isMultiSelect = false)
		{
			var tcs = new TaskCompletionSource<ReturnParams>();

			showDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.PickPaychargeView,
				Param = new OpenParams
				{
					PickMode = pickMode,
					СhargeoutRecipientRowId = сhargeoutRecipientRowId,
					ExcludePaycharges = excludePaycharges,
					HasNoDistributedAmount = hasNoDistributedAmount,
					IsMultiSelect = isMultiSelect,
					TaskSource = tcs,
				},
			});


			return tcs.Task;
		}

	}
}
