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
using DevExpress.Mvvm.POCO;
using System.ComponentModel;
using Profibiz.PracticeManager.InfrastructureExt.Common;
using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using System.Diagnostics;
using System.Windows.Threading;
using Profibiz.PracticeManager.Patients.BusinessService;
using AutoMapper;
using Newtonsoft.Json;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class OneCalendarEventViewModel : ViewModelBase 
	{
		#region Services
		IPatientsBusinessService businessService = ServiceHelper.GetInstance<IPatientsBusinessService>();
		ILookupsBusinessService lookupsService = ServiceHelper.GetInstance<ILookupsBusinessService>();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion
		public OpenParams OpenParam { get; set; }
		public virtual CalendarEvent Entity { get; set; }
		public virtual bool IsNew { get; set; }
		public virtual bool IsReadOnly { get; set; }
		public virtual bool IsEnabledStartDate { get; set; } = true;
		public virtual bool IsButtonSaveEnabled { get; set; } = true;
		public virtual bool IsEnabledStartEndTime { get; set; }
		public virtual ObservableCollection<ServiceProvider> ServiceProviders { get; set; }

		public enum ShowModes { Patient, ServiceProvider }
		public virtual ShowModes ShowMode { get; set; }
		public virtual bool IsShowPatient => ShowMode == ShowModes.Patient;
		public virtual bool IsShowServiceProvider => ShowMode == ShowModes.ServiceProvider;

		public virtual UIElementManager UIManagerStartTime { get; set; } = new UIElementManager();
		public virtual UIElementManager UIManagerFinishTime { get; set; } = new UIElementManager();
		public virtual UIElementManager UIManagerWindow { get; set; } = new UIElementManager();
		



		public OneCalendarEventViewModel() : base()
		{
		}

		public void OnOpen(OpenParams param)
		{
			OpenParam = param;
			DispatcherUIHelper.Run2(LoadData());
		}

		async Task LoadData()
		{
			ShowWaitIndicator.Show();

			IsNew = OpenParam.IsNew;
			var task1 = (!IsNew ?
					businessService.GetCalendarEventList(rowId: OpenParam.RowId) :
					Task.FromResult(new List<CalendarEvent>(new[] { new CalendarEvent() })));
			await Task.WhenAll(task1);
			Entity = task1.Result.Single();

			if (IsNew)
			{
				Entity.RowId = Guid.NewGuid();
				Entity.RemainderInMinutes = 30;
				ShowMode = OpenParam.NewShowMode;
				if (OpenParam.NewStart != null)
				{
					Entity.Start = (DateTime)OpenParam.NewStart;
					Entity.Finish = (DateTime)OpenParam.NewFinish;
				}
				if (OpenParam.NewPatient != null)
				{
					Entity.Patient = OpenParam.NewPatient;
					Entity.PatientRowId = Entity.Patient.RowId;
				}
			}
			else
			{
				ShowMode = (Entity.PatientRowId != null ? ShowModes.Patient : ShowModes.ServiceProvider);
			}
			IsReadOnly = OpenParam.IsReadOnly;
			Entity.DateTimePropsBuild();
			UpdateStartEndTime(afterAllDayChanged: false);
			BuildServiceProviders(true);

			RegisterMessenges();

			(Entity as INotifyPropertyChanged).PropertyChanged += EntityPropertyChanged;

			ResetHasChange();
			ShowWaitIndicator.Hide();

			if (IsNew && GuidHelper.IsNullOrEmpty(Entity.PatientRowId) && ShowMode == ShowModes.Patient)
			{
				FindPatient();
			}
		}


		bool isRegisterMessenges;
		void RegisterMessenges()
		{
			if (!isRegisterMessenges)
			{
				MessengerHelper.Register<MsgRowChange<Patient>>(this, OnMsgRowChange);
				isRegisterMessenges = true;
			}
		}


		void EntityPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(Entity.AllDay))
			{
				UpdateStartEndTime(afterAllDayChanged: true);
			}
			if (e.PropertyName == nameof(Entity.RemainderInMinutes))
			{
				Entity.SnoozedTo = null;
			}
		}

		void OnMsgRowChange(MsgRowChange<Patient> msg)
		{
			MessengerHelper.RunAction(this, () =>
			{
				if (msg.RowAction == RowAction.Update && Entity.PatientRowId == msg.Row.RowId)
				{
					Entity.Patient = msg.Row;
				}
				else if (msg.RowAction == RowAction.Insert && msg.Options == "NewActionBookmark=" + lastNewActionBookmark)
				{
					Entity.PatientRowId = msg.Row.RowId;
					Entity.Patient = msg.Row;
				}
			});
		}


		void BuildServiceProviders(bool isOpenWindow)
		{
			ServiceProviders = LookupDataProvider.Instance.ServiceProviders.OrderBy(q => q.FullName).ToObservableCollection();
		}



		bool Validate()
		{
			var errors = new List<String>();

			var err2 = Entity.DateTimePropsUpdate();
			errors.AddRange(err2);
			if (ShowMode == ShowModes.Patient)
			{
				ValidateHelper.Empty(Entity.PatientRowId, "Patient", errors);
			}
			else if (ShowMode == ShowModes.ServiceProvider)
			{
				ValidateHelper.Empty(Entity.ServiceProviderRowId, "Service Provider", errors);
			}


			if (errors.Count > 0)
			{
				var err = string.Join("\n\n", errors.ToArray());
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

			if (Entity.IsVacation || Entity.IsBusyEvent)
			{
				Entity.RemainderInMinutes = -1;
			}
			if (Entity.IsVacation)
			{
				Entity.IsBusyEvent = false;
			}

			//updateEntity
			var updateEntity = Entity.GetPocoClone();
			var updateEntities = new List<CalendarEvent>() { updateEntity };

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = IsNew ?
				await businessService.PostCalendarEvent(updateEntities) :
				await businessService.PutCalendarEvent(updateEntities);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService))
			{
				return false;
			}

			MessengerHelper.SendMsgRowChange(Entity, IsNew);
			IsNew = false;
			ResetHasChange();

			//close
			if (andClose)
			{
				CloseCore(force:true);
			}

			return true;
		}



		public void OpenPatient() => NewOpenPatient(false);
		public bool CanOpenPatient() => (!GuidHelper.IsNullOrEmpty(Entity?.PatientRowId));
		public void NewPatient() => NewOpenPatient(true);
		public bool CanNewPatient() => !OpenParam.IsLockPatient;

		Guid lastNewActionBookmark;
		public void NewOpenPatient(bool isnew)
		{
			lastNewActionBookmark = (isnew ? Guid.NewGuid() : default(Guid));
			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.OnePatientView,
				Param = new OnePatientViewModel.OpenParams
				{
					IsNew = isnew,
					RowId = (isnew ? default(Guid) : (Guid)Entity.PatientRowId),
					NewActionBookmark = lastNewActionBookmark,
				},
			});
		}
		


		public void FindPatient()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var ret2 = await PickPatientViewModel.PickPatient(
					ShowDXWindowsInteractionRequest, 
					PickPatientViewModel.PickModeEnum.PickPatient);
				if (!ret2.IsSuccess) return;

				Entity.PatientRowId = ret2.PickPatient.RowId;
				Entity.Patient = ret2.PickPatient;
			});
		}
		public bool CanFindPatient() => !OpenParam.IsLockPatient;


		public void SpinStartTime(DevExpress.Xpf.Editors.SpinEventArgs e) => SpinCore("StartTime", e);
		public void SpinFinishTime(DevExpress.Xpf.Editors.SpinEventArgs e) => SpinCore("FinishTime", e);
		public void SpinCore(String column, DevExpress.Xpf.Editors.SpinEventArgs e)
		{
			e.Handled = true;

			Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>
			{
				var interval = 15;
				var timeSpan = new TimeSpan(0, (e.IsSpinUp ? 1 : -1) * interval, 0);
				if (column == "StartTime")
				{
					Entity.StartTime += timeSpan; 
				}
				else if (column == "FinishTime")
				{
					Entity.FinishTime += timeSpan;
				}
				UIManagerWindow.ClearFocus();
			}));
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
			if (IsReadOnly) return false;
			return (IsNew || Entity.IsChanged);
		}
		void ResetHasChange()
		{
			Entity.IsChanged = false;
		}


		async Task<bool> OnClose(bool showOKCancel = false)
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
		//public async void ClosingEvent(CancelEventArgs arg)
		//{
		//	if (forceClose)
		//	{
		//		return;
		//	}
		//	if (!await OnClose())
		//	{
		//		arg.Cancel = true;
		//	}
		//}
		public async void ClosingEvent(CancelEventArgs arg)
		{
			if (forceClose)
			{
				return;
			}
			arg.Cancel = true;
			if (await OnClose())
			{
				DispatcherUIHelper.Run(() => CloseCore(force: true));
			}
		}
		public void Close() => CloseCore();
		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));


		public void AddRowFromPopup(string column)
		{
			if (column == nameof(Entity.Status1RowId) || column == nameof(Entity.Status2RowId))
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneCalendarEventStatusView,
					Param = "IsNew",
				});
			}
		}


		void UpdateStartEndTime(bool afterAllDayChanged)
		{
			IsEnabledStartEndTime = !Entity.AllDay;

			if (afterAllDayChanged)
			{
				if (Entity.AllDay)
				{
					var zero = new DateTime(1901, 1, 1, 0, 0, 0);
					Entity.StartTime = zero;
					Entity.FinishTime = zero;
				}
			}
		}
		


		public class OpenParams
		{
			public bool IsNew { get; set; }
			public Guid RowId { get; set; }

			public ShowModes NewShowMode { get; set; }

			public DateTime? NewStart { get; set; }
			public DateTime? NewFinish { get; set; }
			public Patient NewPatient { get; set; }
			public Boolean IsLockPatient { get; set; }
			public Boolean IsReadOnly { get; set; }
		}


	}

}
