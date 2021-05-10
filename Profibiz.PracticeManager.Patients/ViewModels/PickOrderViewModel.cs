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
	public class PickOrderViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
		#endregion
		public virtual ObservableCollection<Order> Entities { get; set; }
		public virtual Order SelectedEntity { get; set; }
		public virtual ObservableCollection<Order> SelectedEntities { get; set; } = new ObservableCollection<Order>();
		public virtual OpenParams OpenParam { get; set; }
		public virtual String WindowTitle { get; set; }
		public virtual String NewRowButtonText { get; set; }
		public virtual Boolean AutoExpandAllNodes { get; set; }
		public virtual Boolean IsMultiSelect { get; set; }
		




		public PickOrderViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
			MessengerHelper.Register<MsgRowChange<Order>>(this, OnMsgRowChange);
		}

		
		public async void OnOpen(OpenParams param)
		{
			OpenParam = param;
			await LoadData();
		}

		public void Pick(Order entity)
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

		void OnMsgRowChange(MsgRowChange<Order> msg)
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

			var qry = "SupplierRowId=" + OpenParam.SupplierRowId;
			if (OpenParam.NoPaidOnly)
			{
				qry += "&noPaidOnly=true";
			}
			var rows = await businessService.GetOrderList(qry);
			if (OpenParam.PickMode == PickModeEnum.Main)
			{
				WindowTitle = "Choose Order" + (IsMultiSelect ? "s" : "");
				NewRowButtonText = "New Order";
			}
			else throw new ArgumentException();

			if (OpenParam.ExcludeOrders != null)
			{
				rows.RemoveAll(q => OpenParam.ExcludeOrders.Contains(q.RowId));
			}
			Entities = new ObservableCollection<Order>(rows.OrderByDescending(q => q.OrderDate));
			BehaviorGridConrol.FocuseSearchControl();

			ShowWaitIndicator.Hide();
		}


		public class OpenParams
		{
			public PickModeEnum PickMode { get; set; }
			public Guid? SupplierRowId { get; set; }
			public IEnumerable<Guid> ExcludeOrders { get; set; }
			public Boolean NoPaidOnly { get; set; }
			public Boolean IsMultiSelect { get; set; }
			public String ShowMessageIfNotExists { get; set; }
			public IMessageBoxService MessageBoxService { get; set; }
			public ShowWaitIndicator ShowWaitIndicator { get; set; }
			public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; }
			public TaskCompletionSource<ReturnParams> TaskSource { get; set; }
		}
		public enum PickModeEnum { Main }


		public class ReturnParams
		{
			public Boolean IsSuccess { get; set; }
			public Order PickRow { get; set; }
			public List<Order> PickRows { get; set; }
		}

		public static Task<ReturnParams> PickRow(OpenParams param)
		{
			var tcs = new TaskCompletionSource<ReturnParams>();
			param.TaskSource = tcs;

			param.ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.PickOrderView,
				Param = param,
			});


			return tcs.Task;
		}

	}
}	
