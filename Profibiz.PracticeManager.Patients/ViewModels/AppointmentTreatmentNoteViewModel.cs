using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Profibiz.PracticeManager.Model;
using Profibiz.PracticeManager.Infrastructure;
using Prism.Interactivity.InteractionRequest;
using System.Collections.ObjectModel;
using DevExpress.DevAV.Common;
using DevExpress.Mvvm.POCO;
using System.ComponentModel;
using AutoMapper;
using Newtonsoft.Json;
using PropertyChanged;
using Profibiz.PracticeManager.InfrastructureExt.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.Practices.ServiceLocation;
using DevExpress.Xpf.Core;
using System.Diagnostics;
using Profibiz.PracticeManager.Patients.BusinessService;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class AppointmentTreatmentNoteViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> OpenWindowInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion
		public OpenParams OpenParam { get; set; }
		public virtual AppointmentTreatmentNote Entity { get; set; }
		public bool IsReadOnly { get; set; }
		public bool IsNew { get; set; }
		public bool ShowDeleteButton => !IsNew && !IsReadOnly;
		public bool IsHideSaveButton => IsNew && OpenParam.AppointmentRowIds.Length > 1;

		public AppointmentTreatmentNoteViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
		}

		public void OnOpen(OpenParams param)
		{
			OpenParam = param;
			DispatcherUIHelper.Run(async () =>
			{
				await LoadData();
			});
		}

		async Task LoadData()
		{
			ShowWaitIndicator.Show();

			IsNew = OpenParam.IsNew;
			IsReadOnly = OpenParam.IsReadOnly;
			AppointmentTreatmentNote entity;
			if (!IsNew)
			{
				entity = (await businessService.GetAppointmentTreatmentNoteList("rowid=" + OpenParam.RowId)).First();
			}
			else
			{
				entity = new AppointmentTreatmentNote();
				entity.RowId = Guid.NewGuid();
				entity.AppointmentRowId = OpenParam.AppointmentRowIds[0];
			}
			Entity = entity;
			EntitySubscribeRow(Entity);
			ResetHasChange();


			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		void EntitySubscribeRow(AppointmentTreatmentNote row)
		{
			var cols = new[] { nameof(Entity.TechniquesDeepTissue), nameof(Entity.TechniquesModeratePressure), nameof(Entity.TechniquesLightPressure) };
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				var prop = e.PropertyName;
				if (prop == nameof(Entity.TechniquesDeepTissue) && Entity.TechniquesDeepTissue)
				{
					Entity.TechniquesModeratePressure = false;
					Entity.TechniquesLightPressure = false;
				}
				else if (prop == nameof(Entity.TechniquesModeratePressure) && Entity.TechniquesModeratePressure)
				{
					Entity.TechniquesDeepTissue = false;
					Entity.TechniquesLightPressure = false;
				}
				else if (prop == nameof(Entity.TechniquesLightPressure) && Entity.TechniquesLightPressure)
				{
					Entity.TechniquesDeepTissue = false;
					Entity.TechniquesModeratePressure = false;
				}
			};
		}




		public void Close() => CloseCore();
		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));



		bool Validate()
		{
			List<string> errors = new List<string>();


			if (errors.Count > 0)
			{
				var err = string.Join("\n", errors.ToArray());
				MessageBoxService.ShowMessage(err, CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
				return false;
			}

			return true;
		}

		async Task<bool> SaveCore(bool andClose)
		{
			//validate
			if (!Validate())
			{
				return false;
			}

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			AppointmentTreatmentNote[] updateEntities;
			if (IsNew)
			{
				updateEntities = OpenParam.AppointmentRowIds.Select(appointmentRowId => 
				{ 
					var ret = Entity.GetPocoClone(); 
					ret.AppointmentRowId = appointmentRowId;
					//ret.RowId = Guid.NewGuid();
					return ret; 
				}).ToArray();
			}
			else
			{
				updateEntities = new[] { Entity.GetPocoClone() };
			}
			var uret = await businessService.PutAppointmentTreatmentNote(updateEntities);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService))
			{
				return false;
			}

			updateEntities.ForEach(q => MessengerHelper.SendMsgRowChange(q, IsNew));
			IsNew = false;
			ResetHasChange();

			//close
			if (andClose)
			{
				CloseCore(force: true);
			}

			return true;
		}






		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		bool forceClose;
		void CloseCore(bool force = false)
		{
			this.forceClose = force;
			CloseInteractionRequest.Raise(null);
		}

		bool HasChange()
		{
			return (IsNew || Entity.IsChanged);
		}

		void ResetHasChange()
		{
			//RichEditConrolManager.Modified = false;
			Entity.IsChanged = false;
		}




		async public Task<bool> OnClose(bool showOKCancel = false)
		{
			if (HasChange())
			{
				var ret = MessageBoxService.ShowMessage(
					(showOKCancel ? CommonResources.Confirmation_Save_And_Continue : CommonResources.Confirmation_Save),
					CommonResources.Confirmation_Caption,
					(showOKCancel ? MessageButton.OKCancel : MessageButton.YesNoCancel),
					MessageIcon.Question);
				if (ret == MessageResult.Cancel)
				{
					return false;
				}
				else if (ret == MessageResult.No)
				{
					return true;
				}
				else if (ret == MessageResult.Yes || ret == MessageResult.OK)
				{
					return await SaveCore(andClose: false);
				}
				else throw new ArgumentException();
			}
			else
			{
				return true;
			}
		}
		public async void ClosingEvent(CancelEventArgs arg)
		{
			if (forceClose)
			{
				return;
			}
			if (!await OnClose())
			{
				arg.Cancel = true;
			}
		}


		public void Delete()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var msg = "Do you want to delete Treatment Notes?";
				var ret = MessageBoxService.ShowMessage(msg, CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
					var uret = await businessService.DeleteAppointmentTreatmentNote(Entity.RowId);
					ShowWaitIndicator.Hide();
					if (!uret.Validate(MessageBoxService)) return;

					MessengerHelper.SendMsgRowChange(Entity, RowAction.Delete);
					CloseInteractionRequest.Raise(null);
				}
			});
		}


		public void Print()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var ret = await OnClose(showOKCancel: true);
				if (!ret) return;

				ShowWaitIndicator.Show();
				var appointmentRowId = Entity.AppointmentRowId;
				var appointments = await businessService.GetAppointmentList(rowId: appointmentRowId);
				var appointment = appointments[0];
				var report = new AppointmentTreatmentNoteReport
				{
					Row = Entity,
					Appointment = appointment,
					Doctor = LookupDataProvider.FindServiceProvider(appointment.ServiceProviderRowId),
					Service = LookupDataProvider.FindMedicalService(appointment.MedicalServicesOrSupplyRowId),
					Patient = appointment.Patient,
				};
				report.Run();
				ShowWaitIndicator.Hide();
			});
		}




		public class OpenParams
		{
			public bool IsNew { get; set; }
			public Guid RowId { get; set; }
			public Guid[] AppointmentRowIds { get; set; }
			public bool IsReadOnly { get; set; }
		}


	}

}
