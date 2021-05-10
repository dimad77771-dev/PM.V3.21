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
	public class PickChargeoutRecipientsViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		#endregion
		public virtual ObservableCollection<ChargeoutRecipient> Entities { get; set; }
		public virtual ChargeoutRecipient SelectedEntity { get; set; }
		public virtual OpenParams OpenParam { get; set; }
		public virtual String WindowTitle { get; set; }
		public virtual String NewRowButtonText { get; set; }
		public virtual Boolean AutoExpandAllNodes { get; set; }
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();





		public PickChargeoutRecipientsViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
			MessengerHelper.Register<MsgRowChange<ChargeoutRecipient>>(this, OnMsgRowChange);
		}


		public async void OnOpen(OpenParams param)
		{
			OpenParam = param;
			await LoadData();
		}

		public void Pick(ChargeoutRecipient entity)
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
					ViewCode = ViewCodes.OneChargeoutRecipientView,
					Param = "IsNew",
				});

			});
		}

		void OnMsgRowChange(MsgRowChange<ChargeoutRecipient> msg)
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

			var rows = LookupDataProvider.Instance.ChargeoutRecipientes;
			WindowTitle = "Choose Outgoing Invoice Recipient";
			NewRowButtonText = "New Outgoing Invoice Recipient";
			rows = rows.OrderBy(q => q.Name).ToObservableCollection();

			Entities = Mapper.Map<ObservableCollection<ChargeoutRecipient>>(rows);
			BehaviorGridConrol.FocuseSearchControl();

			ShowWaitIndicator.Hide();
		}


		public class OpenParams
		{
			public TaskCompletionSource<ReturnParams> TaskSource { get; set; }
		}


		public class ReturnParams
		{
			public Boolean IsSuccess { get; set; }
			public ChargeoutRecipient PickRow { get; set; }
		}

		public static Task<ReturnParams> PickRow(
			InteractionRequest<ShowDXWindowsActionParam> showDXWindowsInteractionRequest)
		{
			var tcs = new TaskCompletionSource<ReturnParams>();

			showDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.PickChargeoutRecipientsView,
				Param = new OpenParams
				{
					TaskSource = tcs,
				},
			});


			return tcs.Task;
		}

	}
}
