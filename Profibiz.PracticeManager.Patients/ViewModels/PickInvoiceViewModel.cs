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
	public class PickInvoiceViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		//public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		#endregion
		//public virtual ObservableCollection<Invoice> Entities { get; set; }
		//public virtual Invoice SelectedEntity { get; set; }
		public virtual ObservableCollection<Invoice> SelectedEntities => InvoiceListModel.SelectedInvoices;
		public virtual OpenParams OpenParam { get; set; }
		public virtual String WindowTitle { get; set; }
		public virtual String NewRowButtonText { get; set; }
		public virtual Boolean AutoExpandAllNodes { get; set; }
		//public virtual Boolean MultipleSelectionMode => OpenParam.MultipleSelectionMode;
		public virtual InvoicesListViewModel InvoiceListModel { get; set; }
		public virtual Boolean IsVisibleInvoicesListView { get; set; }
		


		public PickInvoiceViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
			MessengerHelper.Register<MsgRowChange<Invoice>>(this, OnMsgRowChange);
		}

		
		public async void OnOpen(OpenParams param)
		{
			OpenParam = param;
			await LoadData();
		}

		//public void Pick(Invoice entity)
		//{
		//	SelectedEntity = entity;
		//	Submit();
		//}

		public void Submit()
		{
			//if (SelectedEntity == null) return;

			var ret = new ReturnParams
            {
                IsSuccess = true,
                //PickRow = SelectedEntity,
                PickRows = SelectedEntities.ToList(),
            };
			OpenParam.TaskSource.SetResult(ret);
			CloseInteractionRequest.Raise(null);
		}
		//public bool CanSubmit() => (MultipleSelectionMode ? SelectedEntities.Count > 0 : Entities != null);
		public bool CanSubmit() => (SelectedEntities.Any());

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

		void OnMsgRowChange(MsgRowChange<Invoice> msg)
		{
			MessengerHelper.RunAction(this, async () =>
			{
				await LoadData();
			});
		}


		async Task LoadData()
		{
			await Task.Yield();
			InvoiceListModel.ViewMode = InvoicesListViewModel.ViewModes.PickRows;
			ShowWaitIndicator.Show();

			var rows = OpenParam.ListRows ?? await GetInvoiceList(OpenParam);
			if (OpenParam.PickMode == PickModeEnum.Main)
			{
				WindowTitle = "Choose Invoice";
				NewRowButtonText = "New Invoice";
			}
			else throw new ArgumentException();


			InvoiceListModel.PreloadedRows = rows;
			await InvoiceListModel.LoadData();

			//Entities = new ObservableCollection<Invoice>(rows.OrderByDescending(q => q.InvoiceDate));
			//BehaviorGridConrol.FocuseSearchControl();

			ShowWaitIndicator.Hide();
			IsVisibleInvoicesListView = true;
		}

		public void MouseDoubleClick(Invoice row)
		{
			DispatcherUIHelper.Run(() =>
			{
				if (row == null) return;

				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.InvoiceWindowView,
					Param = new InvoiceWindowViewModel.OpenParams
					{
						IsNew = false,
						RowId = row.RowId,
						ReadOnly = true,
					},
				});
			});
		}

		//public void OpenRow() => MouseDoubleClick(SelectedEntity);
		//public bool CanOpenRow() => SelectedEntity != null;

		public async static Task<List<Invoice>> GetInvoiceList(OpenParams openParam)
		{
			var businessService = BusinessServiceHelper.GetPatientsBusinessService();
			var qrys = new List<string>();
			if (openParam.PatientRowId != null)
			{
				qrys.Add("patientRowId=" + openParam.PatientRowId);
			}
			if (openParam.UseFamilyHead != null)
			{
				qrys.Add("useFamilyHead=" + openParam.UseFamilyHead.ToWebQuery());
			}
			if (openParam.NoPaidOnly)
			{
				qrys.Add("noPaidOnly=1");
			}
			if (openParam.FlagNoPaidOrNoApprovedAmount)
			{
				qrys.Add("flagNoPaidOrNoApprovedAmount=true");
			}
			if (openParam.NegativeBalanceOnly)
			{
				qrys.Add("negativeBalanceOnly=true");
			}
			qrys.Add("includeInvoiceClaims=true");
			var qry = QueryHelper.CreateQuery(qrys);
			var rows = await businessService.GetInvoiceList(qry);
			rows = rows.OrderBy(q => q.InvoiceDate).ToList();
			return rows;
		}


		public class OpenParams
		{
			public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest;
			public IMessageBoxService MessageBoxService;
			public ShowWaitIndicator ShowWaitIndicator;
			public TaskCompletionSource<ReturnParams> TaskSource { get; set; }
			public PickModeEnum PickMode { get; set; }
			public Guid? PatientRowId { get; set; }
			public Boolean? UseFamilyHead { get; set; }
			public IEnumerable<Guid> ExcludeInvoices { get; set; }
			public Boolean NoPaidOnly { get; set; }
			public Boolean FlagNoPaidOrNoApprovedAmount { get; set; }
			public Boolean NegativeBalanceOnly { get; set; }
			//public Boolean MultipleSelectionMode { get; set; }
			public String ShowMessageIfNotExists { get; set; }
			public List<Invoice> ListRows { get; set; }
		}
		public enum PickModeEnum { Main }


		public class ReturnParams
		{
			public Boolean IsSuccess { get; set; }
			//public Invoice PickRow { get; set; }
            public List<Invoice> PickRows { get; set; }
        }

		public async static Task<ReturnParams> PickRow(OpenParams parms)
		{
			var task = await PickRowCore(parms);
			var ret = await task;
			return ret;
		}

		private async static Task<Task<ReturnParams>> PickRowCore(OpenParams parms)
		{
			var tcs = new TaskCompletionSource<ReturnParams>();
			parms.TaskSource = tcs;

			var flagShowMessageIfNotExists = !string.IsNullOrEmpty(parms.ShowMessageIfNotExists);
			if (flagShowMessageIfNotExists)
			{
				bool notExists = false;
				parms.ShowWaitIndicator?.Show();
				var rows = await GetInvoiceList(parms);
				parms.ShowWaitIndicator?.Hide();

				if (parms.ExcludeInvoices != null)
				{
					rows.RemoveAll(q => parms.ExcludeInvoices.Contains(q.RowId));
				}

				if (rows.Count == 0)
				{
					parms.MessageBoxService.ShowWarning(parms.ShowMessageIfNotExists);
					notExists = true;
					tcs.SetResult(new ReturnParams { IsSuccess = false });
				}
				if (notExists)
				{
					return tcs.Task;
				}
				else
				{
					parms.ListRows = rows;
				}
			}


			parms.ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.PickInvoiceView,
				Param = parms,
			});


			return tcs.Task;
		}

	}
}	
