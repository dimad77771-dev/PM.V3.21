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
	public class PickPaychargeAppointmentsViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		#endregion
		public virtual ObservableCollection<Appointment> Entities { get; set; }
		public virtual Appointment SelectedEntity { get; set; }
		public virtual ObservableCollection<Appointment> SelectedEntities { get; set; } = new ObservableCollection<Appointment>();
		public virtual OpenParams OpenParam { get; set; }
		public virtual String WindowTitle { get; set; }
		public virtual String NewRowButtonText { get; set; }
		public virtual Boolean AutoExpandAllNodes { get; set; }
		public virtual Boolean MultipleSelectionMode => OpenParam.MultipleSelectionMode;




		public PickPaychargeAppointmentsViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
		}


		public async void OnOpen(OpenParams param)
		{
			OpenParam = param;
			await LoadData();
		}

		public void Pick(Appointment entity)
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
		public bool CanSubmit() => (MultipleSelectionMode ? SelectedEntities.Count > 0 : Entities != null);

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


		async Task LoadData()
		{
			await Task.Yield();
			ShowWaitIndicator.Show();

			var rows = await businessService.GetAppointmentList(
				patientRowId: OpenParam.PatientRowId,
				completed: OpenParam.Completed,
				inInvoice: OpenParam.InInvoice,
				forChargeout: OpenParam.ForChargeout);
			if (OpenParam.PickMode == PickModeEnum.Main)
			{
				WindowTitle = "Choose Appointment";
				NewRowButtonText = "New Appointment";
			}
			else throw new ArgumentException();

			if (OpenParam.ExcludeAppointments != null)
			{
				rows.RemoveAll(q => OpenParam.ExcludeAppointments.Contains(q.RowId));
			}
			if (OpenParam.ExcludeInvoiceItems != null)
			{
				rows.RemoveAll(q => OpenParam.ExcludeInvoiceItems.Contains(q.InvoiceItem.RowId));
			}
			Entities = new ObservableCollection<Appointment>(rows.OrderBy(q => q.Start));
			BuildChargeoutItems();
			BehaviorGridConrol.FocuseSearchControl();

			ShowWaitIndicator.Hide();
		}

		void BuildChargeoutItems()
		{
			foreach(var appointment in Entities)
			{
				appointment.BuildingChargeoutItem = ChargeoutItem.CreateFromAppointment(appointment, appointment.InvoiceItem);
			}
		}


		public class OpenParams
		{
			public PickModeEnum PickMode { get; set; }
			public Guid? PatientRowId { get; set; }
			public Guid? ChargeoutRecipientRowId { get; set; }
			public IEnumerable<Guid> ExcludeAppointments { get; set; }
			public IEnumerable<Guid> ExcludeInvoiceItems { get; set; }
			public Boolean? InInvoice { get; set; }
			public Boolean? Completed { get; set; }
			public Boolean? ForChargeout { get; set; }
			public Boolean MultipleSelectionMode { get; set; }
			public TaskCompletionSource<ReturnParams> TaskSource { get; set; }
		}
		public enum PickModeEnum { Main }


		public class ReturnParams
		{
			public Boolean IsSuccess { get; set; }
			public Appointment PickRow { get; set; }
			public List<Appointment> PickRows { get; set; }
		}

		public static Task<ReturnParams> PickRow(
			InteractionRequest<ShowDXWindowsActionParam> showDXWindowsInteractionRequest,
			PickModeEnum pickMode,
			Guid? patientRowId = null,
			Guid? chargeoutRecipientRowId = null,
			IEnumerable<Guid> excludeAppointments = null,
			IEnumerable<Guid> excludeInvoiceItems = null,
			Boolean? completed = null,
			Boolean? inInvoice = null,
			Boolean? forChargeout = null,
			Boolean multipleSelectionMode = false)
		{
			var tcs = new TaskCompletionSource<ReturnParams>();

			showDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.PickPaychargeAppointmentsView,
				Param = new OpenParams
				{
					PickMode = pickMode,
					PatientRowId = patientRowId,
					ExcludeAppointments = excludeAppointments,
					ExcludeInvoiceItems = excludeInvoiceItems,
					Completed = completed,
					InInvoice = (forChargeout == true ? (bool?)null : false),
					ForChargeout = forChargeout,
					MultipleSelectionMode = multipleSelectionMode,
					TaskSource = tcs,
				},
			});


			return tcs.Task;
		}

	}
}
