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
	public class PickSupplierViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		#endregion
		public virtual ObservableCollection<Supplier> Entities { get; set; }
		public virtual ObservableCollection<Supplier> SelectedEntities { get; set; } = new ObservableCollection<Supplier>();
		public virtual Supplier SelectedEntity { get; set; }
		public virtual OpenParams OpenParam { get; set; }
		public virtual String WindowTitle { get; set; }
		public virtual String NewRowButtonText { get; set; }
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();


	


		public PickSupplierViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
			MessengerHelper.Register<MsgRowChange<Supplier>>(this, OnMsgRowChange);
		}

		
		public async void OnOpen(OpenParams param)
		{
			OpenParam = param;
			await LoadData();
		}


		public void Submit()
		{
			var ret = new ReturnParams
			{
				IsSuccess = true,
				Rows = SelectedEntities.ToList(),
				Row = SelectedEntity,
			};
			OpenParam.TaskSource.SetResult(ret);
			CloseInteractionRequest.Raise(null);
		}
		public bool CanSubmit()
		{
			if (OpenParam.PickMode == PickModeEnum.SingleSelect)
			{
				return SelectedEntity != null;
			}
			else if (OpenParam.PickMode == PickModeEnum.MultiSelect)
			{
				return SelectedEntities.Any();
			}
			else throw new ArgumentException();
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
					Param = "IsNew",
				});

			});
		}

		void OnMsgRowChange(MsgRowChange<Supplier> msg)
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

			var rows = LookupDataProvider.Instance.Suppliers;
			if (OpenParam.PickMode == PickModeEnum.SingleSelect)
			{
				WindowTitle = "Select Supplier";
				NewRowButtonText = "New Supplier";
			}
			else throw new ArgumentException();

			Entities = rows.OrderBy(q => q.Name).ToObservableCollection();
			SelectedEntities = Entities.Where(q => OpenParam.SelectedSupplierRowIds.Contains(q.RowId)).ToObservableCollection();

			ShowWaitIndicator.Hide();
		}


		public class OpenParams
		{
			public PickModeEnum PickMode { get; set; }
			public IEnumerable<Guid> SelectedSupplierRowIds { get; set; } = new List<Guid>();
			public TaskCompletionSource<ReturnParams> TaskSource { get; set; }
			public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; }
	}
		public enum PickModeEnum { SingleSelect, MultiSelect }


		public class ReturnParams
		{
			public Boolean IsSuccess { get; set; }
			public List<Supplier> Rows { get; set; }
			public Supplier Row { get; set; }
		}

		public static Task<ReturnParams> Pick(OpenParams parm)
		{
			var tcs = new TaskCompletionSource<ReturnParams>();
			parm.TaskSource = tcs;

			parm.ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.PickSupplierView,
				Param = parm,
			});

			return tcs.Task;
		}

	}
}	
