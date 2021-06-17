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
using Profibiz.PracticeManager.Patients.EF;
using Newtonsoft.Json;
using Profibiz.PracticeManager.Patients.BusinessService;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class PickBodyrevivalsalonspaPatientViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		#endregion
		public virtual ObservableCollection<Customer> Entities { get; set; }
		public virtual Customer SelectedEntity { get; set; }
		public virtual ObservableCollection<Customer> SelectedEntities { get; set; } = new ObservableCollection<Customer>();
		public virtual OpenParams OpenParam { get; set; }
		public virtual String WindowTitle { get; set; }
		public virtual String NewRowButtonText { get; set; }
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();





		public PickBodyrevivalsalonspaPatientViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
			MessengerHelper.Register<MsgRowChange<Category>>(this, OnMsgRowChange);
		}


		public async void OnOpen(OpenParams param)
		{
			OpenParam = param;
			await LoadData();
		}


		public void Submit()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var customers = SelectedEntities.ToArray();

				ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
				var json = JsonConvert.SerializeObject(customers);
				var rez = await OpenParam.BusinessService.PostPatientsFromBodyrevivalsalonspa(json);
				ShowWaitIndicator.Hide();
				if (!rez.Validate(OpenParam.MessageBoxService))
				{
					return;
				}

				var ret = new ReturnParams
				{
					IsSuccess = true,
					Rows = SelectedEntities.ToArray(),
				};
				OpenParam.TaskSource.SetResult(ret);
				CloseInteractionRequest.Raise(null);
			});
		}
		public bool CanSubmit()
		{
			return (SelectedEntities != null && SelectedEntities.Any() && SelectedEntities.All(q => !q.isImported));
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

		void OnMsgRowChange(MsgRowChange<Category> msg)
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

			WindowTitle = "Select Patients from MD Ware";

			var sdb = new md_bodyrevivalsalonspaEntities();
			var customers = sdb.Customer.ToArray();
			var patients = await businessService.GetPatientList("");
			var joins = 
				from patient in patients
				join customer in customers on patient.spaCustomerNumber equals customer.CustomerNumber
				select new { patient, customer };
			foreach(var join in joins)
			{
				join.customer.isImported = true;
			}

			Entities = customers.ToObservableCollection();

			ShowWaitIndicator.Hide();
		}


		public class OpenParams
		{
			public PickModeEnum PickMode { get; set; }
			public TemplateNameModel[] TemplateFiles { get; set; }
			//public String TemplateFolder { get; set; }
			public IEnumerable<Guid> SelectedCategoryRowIds { get; set; }
			public TaskCompletionSource<ReturnParams> TaskSource { get; set; }
			public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; }
			public IMessageBoxService MessageBoxService { get; set; }
			public IPatientsBusinessService BusinessService { get; set; }
		}
		public enum PickModeEnum { Main }


		public class ReturnParams
		{
			public Boolean IsSuccess { get; set; }
			public Customer[] Rows { get; set; }
		}

		public static Task<ReturnParams> Pick(OpenParams parm)
		{
			var tcs = new TaskCompletionSource<ReturnParams>();
			parm.TaskSource = tcs;

			parm.ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.PickBodyrevivalsalonspaPatientView,
				Param = parm,
			});

			return tcs.Task;
		}

	}
}
