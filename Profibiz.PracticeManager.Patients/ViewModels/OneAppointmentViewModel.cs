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
using Profibiz.PracticeManager.SharedCode;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class OneAppointmentViewModel : ViewModelBase 
	{
		#region Services
		IPatientsBusinessService businessService = ServiceHelper.GetInstance<IPatientsBusinessService>();
		ILookupsBusinessService lookupsService = ServiceHelper.GetInstance<ILookupsBusinessService>();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion
		public OpenParams OpenParam { get; set; }
		public virtual Appointment Entity { get; set; }
		public virtual AppointmentBook AppointmentBook { get; set; }
		public virtual ObservableCollection<ServiceProvider> ServiceProviders { get; set; }
		public virtual ObservableCollection<MedicalServicesOrSupply> MedicalServicesOrSupplies { get; set; }
		public virtual ObservableCollection<AppointmentBook> AllAppointmentBooks { get; set; }
		public virtual ObservableCollection<InsuranceCoverage> GoodInsuranceCoverages { get; set; }
		public virtual InsurancePatientCategoryInfo InsurancePatientCategoryInfo { get; set; } = new InsurancePatientCategoryInfo();
		public virtual bool IsNew { get; set; }
		public virtual bool IsReadOnlyAppointmentBookRowId { get; set; }
		public virtual bool IsReadOnly { get; set; }
		public virtual bool IsMultiDateVisibile => IsNew;
		public virtual bool IsNotRegisteredMode => OpenParam.IsNotRegisteredMode;
		public virtual string VisibilityIsNotRegisteredMode => IsNotRegisteredMode ? "Collapsed" : "Visible";
		public virtual string RowHeightIsNotRegisteredMode => IsNotRegisteredMode ? "0" : "Auto";
		public virtual string RowHeightIsNotRegisteredModeReverse => IsNotRegisteredMode ? "Auto" : "0";
		public virtual bool IsEnabledStartDate { get; set; } = true;
		public virtual bool IsButtonSaveEnabled { get; set; } = true;
		public virtual bool IsButtonSaveHide => IsNotRegisteredMode;
		public virtual bool AppointmentClinicalNoteExists => (Entity?.AppointmentClinicalNoteRowId != null);
		public virtual string AddOpenClinicalNoteButtonText => (AppointmentClinicalNoteExists ? "Edit Clinical Notes..." : "Add Clinical Notes...");
		public virtual bool AppointmentTreatmentNoteExists => (Entity?.AppointmentTreatmentNoteRowId != null);
		public virtual string AddOpenTreatmentNoteButtonText => (AppointmentTreatmentNoteExists ? "Edit Treatment Notes..." : "Add Treatment Notes...");


		public virtual UIElementManager UIManagerStartTime { get; set; } = new UIElementManager();
		public virtual UIElementManager UIManagerFinishTime { get; set; } = new UIElementManager();
		public virtual UIElementManager UIManagerWindow { get; set; } = new UIElementManager();

		public virtual List<object> RemainderInMinutesSet { get; set; } = new List<object>();


		public OneAppointmentViewModel() : base()
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
					businessService.GetAppointmentList(rowId: OpenParam.RowId) :
					Task.FromResult(new List<Appointment>(new[] { new Appointment() })));
			await Task.WhenAll(task1);
			Entity = task1.Result.Single();

			AppointmentBook = LookupDataProvider.FindAppointmentBook(OpenParam.AppointmentBookRowId);
			if (IsNew)
			{
				Entity.RowId = Guid.NewGuid();
				if (AppointmentBook != null)
				{
					Entity.AppointmentBookRowId = AppointmentBook.RowId;
				}
				if (OpenParam.NewStart != null)
				{
					Entity.Start = (DateTime)OpenParam.NewStart;
					Entity.Finish = (DateTime)OpenParam.NewFinish;
				}
				if (OpenParam.NewServiceProviderRowId != null)
				{
					Entity.ServiceProviderRowId = OpenParam.NewServiceProviderRowId;
				}
				if (OpenParam.NewPatient != null)
				{
					Entity.Patient = OpenParam.NewPatient;
					Entity.PatientRowId = Entity.Patient.RowId;
				}
				Entity.IsEmailWhenRegistered = true;
				Entity.IsSmsWhenRegistered = true;
			}
			IsReadOnlyAppointmentBookRowId = (Entity.AppointmentBookRowId != default(Guid));
			IsReadOnly = (Entity.InInvoice || OpenParam.IsReadOnly);
			BuildAppointmentBooks();
			BuildInsuranceProviders(true);
			BuildServiceProviders(true);
			BuildMedicalServicesOrSupplies();
			await SetupStartFinishForNewRow();
			Entity.DateTimePropsBuild();
			Entity.UpdateMultiDatesText();
			LoadAppointmentRemainders();

			RegisterMessenges();

			(Entity as INotifyPropertyChanged).PropertyChanged += EntityPropertyChanged;
			Entity.OnOpenInvoiceDetail = OnOpenInvoiceDetail;

			ResetHasChange();
			//DispatcherUIHelper.Run(() => ResetHasChange());
			ShowWaitIndicator.Hide();

			if (IsNew && GuidHelper.IsNullOrEmpty(Entity.PatientRowId))
			{
				if (IsNotRegisteredMode)
				{
					Entity.Patient = new Patient 
					{
						RowId = Guid.NewGuid(),
						IsNotRegistered = true
					};
					Entity.PatientRowId = Entity.Patient.RowId;
				}
				else
				{
					FindPatient();
				}
			}
		}


		bool isRegisterMessenges;
		void RegisterMessenges()
		{
			if (!isRegisterMessenges)
			{
				MessengerHelper.Register<MsgRowChange<Patient>>(this, OnMsgRowChange);
				MessengerHelper.Register<MsgRowChange<InsuranceCoverage>>(this, OnMsgRowChangeInsuranceCoverage);
				MessengerHelper.Register<MsgRowChange<AppointmentClinicalNote>>(this, OnMsgRowChangeAppointmentClinicalNote);
				MessengerHelper.Register<MsgRowChange<AppointmentTreatmentNote>>(this, OnMsgRowChangeAppointmentTreatmentNote);
				isRegisterMessenges = true;
			}
		}

		async Task SetupStartFinishForNewRow()
		{
			if (!IsNew) return;

			if (Entity.ServiceProviderRowId != null && OpenParam.NewStart != null)
			{
				var date = OpenParam.NewStart.Value.Date;
				var calcinfos = await businessService.CalculateAppointmentStartFinish(Entity.ServiceProviderRowId.Value, new[] { date });
				var calcinfo = calcinfos[0];
				if (!calcinfo.HasError())
				{
					Entity.Start = calcinfo.Start;
					Entity.Finish = calcinfo.Finish;
				}
			}
		}


		void EntityPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(Entity.AppointmentBookRowId))
			{
				BuildServiceProviders(false);
			}

			if (e.PropertyName == nameof(Entity.ServiceProviderRowId))
			{
				BuildMedicalServicesOrSupplies();
			}

			if (e.PropertyName == nameof(Entity.InsuranceCoverageRowId))
			{
				OnInsuranceCoverageRowIdChanged();
			}

			if (e.PropertyName == nameof(Entity.InsuranceCoverageRowId) || e.PropertyName == nameof(Entity.MedicalServicesOrSupplyRowId))
			{
				RecalcInsuranceCoverageInvoiceClaimsInfoFIelds();
			}

			if (e.PropertyName == nameof(Entity.StartDate))
			{
				BuildInsuranceProviders(false);
				//var aaa = Entity.StartDate;
				//Debug.WriteLine("Entity.StartDate=" + aaa);
			}

			if (e.PropertyName == nameof(Entity.StartTime) || e.PropertyName == nameof(Entity.MedicalServicesOrSupplyRowId))
			{
				CalcFinishTime();
			}


		}

		void CalcFinishTime()
		{
			if (!Entity.HasMultiDates)
			{
				var qty = LookupDataProvider.FindMedicalService(Entity.MedicalServicesOrSupplyRowId)?.Qty;
				if (qty != null && Entity.StartTime != null)
				{
					Entity.FinishTime = new DateTime(2000, 1, 1) + Entity.StartTime.Value.TimeOfDay.Add(TimeSpan.FromMinutes(qty.Value));
				}
			}
				
		}

		void OnInsuranceCoverageRowIdChanged()
		{
			if (GoodInsuranceCoverages == null) return;

			var insuranceCoverage = GoodInsuranceCoverages.SingleOrDefault(q => q.RowId == Entity.InsuranceCoverageRowId);
			if (Entity.InsuranceCoverageRowId != null && insuranceCoverage == null) throw new LogicalException();

			Entity.InsuranceProviderRowId = insuranceCoverage?.InsuranceProviderRowId;
			Entity.PolicyNumber = insuranceCoverage?.PolicyNumber;
			Entity.PolicyOwnerRowId = insuranceCoverage?.PolicyOwnerRowId;
			Entity.PolicyOwnerFullName = insuranceCoverage?.PolicyOwnerFullName;
		}

		void BuildServiceProviders(bool isOpenWindow)
		{
			var appointmentBook = LookupDataProvider.FindAppointmentBook(Entity.AppointmentBookRowId);
			ServiceProviders = (appointmentBook != null ? appointmentBook.ServiceProviders.OrderBy(q => q.FullName) : Enumerable.Empty<ServiceProvider>()).ToObservableCollection();
			if (Entity.ServiceProviderRowId != null && !ServiceProviders.Select(q => q.RowId).Contains((Guid)Entity.ServiceProviderRowId))
			{
				Entity.ServiceProviderRowId = null;
			}

			if (  (!isOpenWindow) || (isOpenWindow && IsNew)  )
			{
				if (ServiceProviders.Count == 1)
				{
					Entity.ServiceProviderRowId = ServiceProviders[0].RowId;
				}
			}
		}

		void BuildMedicalServicesOrSupplies()
		{
			var serviceProvider = LookupDataProvider.FindServiceProvider(Entity.ServiceProviderRowId);
			MedicalServicesOrSupplies = 
				LookupDataProvider.Instance.MedicalServices
				.Where(q => serviceProvider == null ? true : serviceProvider.ServiceProviderServices.Any(z => z.MedicalServiceOrSupplyRowId == q.RowId))
				.ToObservableCollection();
			foreach(var mrow in MedicalServicesOrSupplies)
			{
				ServiceProviderService serviceProviderService = null;
				if (serviceProvider != null)
				{
					serviceProviderService = serviceProvider.ServiceProviderServices.FirstOrDefault(z => z.MedicalServiceOrSupplyRowId == mrow.RowId);
				}
				mrow.SetPriceInfoFromServiceProvider(serviceProviderService);
			}
            if (Entity.MedicalServicesOrSupplyRowId != null && !MedicalServicesOrSupplies.Select(q => q.RowId).Contains((Guid)Entity.MedicalServicesOrSupplyRowId))
			{
				Entity.MedicalServicesOrSupplyRowId = null;
			}
		}


		void BuildAppointmentBooks()
		{
			AllAppointmentBooks = LookupDataProvider.Instance.AppointmentBooks.ToObservableCollection();
		}

		void BuildInsuranceProviders(bool isOpenWindow)
		{
			DispatcherUIHelper.Run(async () =>
			{
				Entity.HasNoCoverage = Entity?.Patient?.HasNoCoverage ?? false;

				var allInsuranceCoverages = new ObservableCollection<InsuranceCoverage>();
				var patientRowId = Entity.PatientRowId;
				if (patientRowId == default(Guid))
				{
					allInsuranceCoverages = new ObservableCollection<InsuranceCoverage>();
				}
				else
				{
					ShowWaitIndicator.Show();
					allInsuranceCoverages = (await businessService.PatientRowId2InsuranceCoverages(patientRowId)).ToObservableCollection();
					ShowWaitIndicator.Hide();
				}

				GoodInsuranceCoverages = 
					allInsuranceCoverages
					.Where
					(q => Entity.HasMultiDates ?
						InsuranceCoverage.InCoverageIntarvalArray(Entity.MultiDates.Select(z => z.Date), q.CoverageStartDate, q.CoverageValidUntil) :
						InsuranceCoverage.InCoverageIntarval(Entity.StartDate, q.CoverageStartDate, q.CoverageValidUntil)
					)
					.ToObservableCollection();
				if (!GoodInsuranceCoverages.Select(q => (Guid?)q.RowId).Contains(Entity.InsuranceCoverageRowId))
				{
					Entity.InsuranceCoverageRowId = null;
				}
				if (!isOpenWindow && GoodInsuranceCoverages.Count == 1)
				{
					Entity.InsuranceCoverageRowId = GoodInsuranceCoverages[0].RowId;
				}

				var insuranceCoverageRowIds = GoodInsuranceCoverages.Select(q => q.RowId).ToArray();
				InsurancePatientCategoryInfo = await businessService.GetInsurancePatientCategoryInfo(insuranceCoverageRowIds.ToWebQuery());
				RecalcInsuranceCoverageInvoiceClaimsInfoFIelds();

				if (isOpenWindow)
				{
					ResetHasChange();
				}
			});
		}

		void RecalcInsuranceCoverageInvoiceClaimsInfoFIelds()
		{
			var isExistInsuranceCoverageInfo = false;
			var categoryRowId = LookupDataProvider.MedicalService2CategoryRowId(Entity.MedicalServicesOrSupplyRowId);
			var info = InsurancePatientCategoryInfo.Find(Entity.InsuranceCoverageRowId, Entity.PatientRowId, categoryRowId);
			if (info.IsFind)
			{
				isExistInsuranceCoverageInfo = true;
				Entity.InsuranceCoverageInfoTotalAmount = info.TotalAmont;
				Entity.InsuranceCoverageInfoApproveAmount = info.ApproveAmount;
			}
			Entity.IsExistInsuranceCoverageInfo = isExistInsuranceCoverageInfo;
			if (!isExistInsuranceCoverageInfo)
			{
				Entity.InsuranceCoverageInfoTotalAmount = 0;
				Entity.InsuranceCoverageInfoApproveAmount = 0;
			}
		}

		void OnMsgRowChange(MsgRowChange<Patient> msg)
		{
			MessengerHelper.RunAction(this, () =>
			{
				if (msg.RowAction == RowAction.Update && Entity.PatientRowId == msg.Row.RowId)
				{
					Entity.Patient = msg.Row;
					BuildInsuranceProviders(false);
				}
				else if (msg.RowAction == RowAction.Insert && msg.Options == "NewActionBookmark=" + lastNewActionBookmark)
				{
					Entity.PatientRowId = msg.Row.RowId;
					Entity.Patient = msg.Row;
					BuildInsuranceProviders(false);
				}
			});
		}

		void OnMsgRowChangeInsuranceCoverage(MsgRowChange<InsuranceCoverage> msg)
		{
			MessengerHelper.RunAction(this, () =>
			{
				BuildInsuranceProviders(false);
			});
		}

		void OnMsgRowChangeAppointmentClinicalNote(MsgRowChange<AppointmentClinicalNote> msg)
		{
			if (msg.Row.AppointmentRowId == Entity.RowId)
			{
				var isChanged = Entity.IsChanged;

				if (msg.RowAction == RowAction.Delete)
				{
					Entity.AppointmentClinicalNoteRowId = null;
				}
				else
				{
					Entity.AppointmentClinicalNoteRowId = msg.Row.RowId;
				}
				this.RaisePropertyChanged(nameof(AddOpenClinicalNoteButtonText));
				this.RaisePropertyChanged(nameof(AddOpenTreatmentNoteButtonText));

				Entity.IsChanged = isChanged;
			}
		}

		void OnMsgRowChangeAppointmentTreatmentNote(MsgRowChange<AppointmentTreatmentNote> msg)
		{
			if (msg.Row.AppointmentRowId == Entity.RowId)
			{
				var isChanged = Entity.IsChanged;

				if (msg.RowAction == RowAction.Delete)
				{
					Entity.AppointmentTreatmentNoteRowId = null;
				}
				else
				{
					Entity.AppointmentTreatmentNoteRowId = msg.Row.RowId;
				}
				this.RaisePropertyChanged(nameof(AddOpenClinicalNoteButtonText));
				this.RaisePropertyChanged(nameof(AddOpenTreatmentNoteButtonText));

				Entity.IsChanged = isChanged;
			}
		}

		void ShowErrors(IEnumerable<string> errors)
		{
			var err = string.Join("\n\n", errors.ToArray());
			MessageBoxService.ShowMessage(err, CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
		}

		bool DateTimePropsUpdate()
		{
			var errors = Entity.DateTimePropsUpdate();
			if (errors.Count > 0)
			{
				ShowErrors(errors);
				return false;
			}

			return true;
		}


		bool Validate()
		{
			var errors = new List<String>();

			ValidateHelper.Empty(Entity.PatientRowId, "Patient", errors);
			ValidateGetWorkWeekDays(errors);

			if (IsNotRegisteredMode)
			{
				ValidateHelper.Empty(Entity.ServiceProviderRowId, "Specialist", errors);
				ValidateHelper.Empty(Entity.MedicalServicesOrSupplyRowId, "Service", errors);
				if (ValidateHelper.IsEmpty(Entity.Patient.EmailAddress) && ValidateHelper.IsEmpty(Entity.Patient.MobileNumber))
				{
					errors.Add("You must specify Email or Phone");
				}
			}
			else if (Entity.Completed)
			{
				if (!Entity.HasNoCoverage)
				{
					ValidateHelper.Empty(Entity.InsuranceCoverageRowId, "Insurance Coverage", errors);
				}
				ValidateHelper.Empty(Entity.ServiceProviderRowId, "Specialist", errors);
				ValidateHelper.Empty(Entity.MedicalServicesOrSupplyRowId, "Service", errors);
			}

			if (errors.Count > 0)
			{
				ShowErrors(errors);
				return false;
			}

			if (!ValidatePublicHolidays())
			{
				return false;
			}

			return true;
		}

		void ValidateGetWorkWeekDays(List<string> errors)
		{
			var serviceProvider = LookupDataProvider.FindServiceProvider(Entity.ServiceProviderRowId);
			if (serviceProvider != null)
			{
				var cnt = (HasMultiDates ? Entity.MultiDates.Count : 1);
				for (int i = 0; i < cnt; i++)
				{
					var startdate = (HasMultiDates ? Entity.MultiDates[i].Start : Entity.StartDate);
					if (startdate != null)
					{
						var sdate = (DateTime)startdate;
						var daysInfo = OpenParam.DaysInfo.DoctorInfo[(Guid)Entity.ServiceProviderRowId];
						if (!SchedulerRecordFunc.IsWorkDate(daysInfo.SchedulerRecords, sdate))
						{
							errors.Add("Specialist \"" + serviceProvider.FullName + "\" is not available on " + startdate.FormatLongDate());
						}
					}
				}
			}
		}

		bool ValidatePublicHolidays()
		{
			var cnt = (HasMultiDates ? Entity.MultiDates.Count : 1);
			for (int i = 0; i < cnt; i++)
			{
				var startdate = (HasMultiDates ? Entity.MultiDates[i].Start : Entity.StartDate);
				if (startdate != null)
				{
					if (LookupDataProvider.IsPublicHoliday(startdate.Value))
					{
						var holidayName = LookupDataProvider.GetPublicHoliday(startdate.Value).Name;
						var txt = startdate.FormatShortDate() + " is a Public Holiday" + (string.IsNullOrEmpty(holidayName) ? "" : " (" + holidayName + ")") + ". Do you want to continue?"; 
						var ret = MessageBoxService.ShowMessage(txt,  CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
						if (ret != MessageResult.Yes)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		bool ValidateEmptyServiceProvider()
		{
			var errors = new List<string>();
			ValidateHelper.Empty(Entity.ServiceProviderRowId, "Specialist", errors);
			if (errors.Count > 0)
			{
				ShowErrors(errors);
				return false;
			}
			return true;
		}

		async Task<bool> UpdateMultiDates()
		{
			if (HasMultiDates)
			{
				if (Entity.IsAutoAllocate)
				{
					if (!ValidateEmptyServiceProvider()) return false;

					var dates = Entity.MultiDates.Select(q => q.Date).ToArray();
					var calcInfoSet = await businessService.CalculateAppointmentStartFinish((Guid)Entity.ServiceProviderRowId, dates);

					var errors = calcInfoSet.Where(q => q.HasError()).Select(q => q.Date.FormatShortDate() + ":" + "\n" + "  " + q.ErrorInfo);
					if (errors.Any())
					{
						ShowErrors(errors);
						return false;
					}

					foreach (var calcInfo in calcInfoSet)
					{
						var mrow = Entity.MultiDates.Single(q => q.Date == calcInfo.Date);
						mrow.Start = calcInfo.Start;
						mrow.Finish = calcInfo.Finish;
					}
				}
				else
				{
					Entity.UpdateMultiDates();
				}
			}
			return true;
		}

		async Task<bool> SaveCore(bool andClose)
		{
			if (!DateTimePropsUpdate()) return false;
			if (!await UpdateMultiDates()) return false;
			if (!Validate()) return false;
			SaveAppointmentRemainders();

			//updateEntity
			var updateEntity = Entity.GetPocoClone();
			var updateEntities = new List<Appointment>() { updateEntity };
			updateEntities[0].AppointmentRemainders = Entity.AppointmentRemainders;
			if (IsNotRegisteredMode)
			{
				var updatePatient = Entity.Patient.GetPocoClone();
				updateEntity.Patient = updatePatient;
			}

			//MultiDates
			if (HasMultiDates)
			{
				updateEntities.Clear();
				foreach (var date in Entity.MultiDates)
				{
					var uentity = Entity.GetPocoClone();
					uentity.RowId = Guid.NewGuid();
					uentity.Start = date.Start;
					uentity.Finish = date.Finish;
					updateEntities.Add(uentity);
				}
			}

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = IsNew ?
				await businessService.PostAppointment(updateEntities) :
				await businessService.PutAppointment(updateEntities);
			ShowWaitIndicator.Hide();
			if (!ValidateAppointmentUpdateErrror(uret, HasMultiDates, MessageBoxService))
			{
				return false;
			}

			if (HasMultiDates)
			{
				foreach (var uent in updateEntities)
				{
					uent.Patient = Entity.Patient;
					MessengerHelper.SendMsgRowChange(uent, IsNew);
				}
			}
			else
			{
				MessengerHelper.SendMsgRowChange(Entity, IsNew);
			}
			IsNew = false;
			ResetHasChange();

			//close
			if (andClose)
			{
				CloseCore(force:true);
			}

			return true;
		}

		static public bool ValidateAppointmentUpdateErrror(UpdateReturn uret, Boolean hasMultiDates, IMessageBoxService messageBoxService)
		{
			if (uret.UserErrorCode == UserErrorCodes.AppointmentIntersection)
			{
				var intersectionAppointments = JsonConvert.DeserializeObject<Appointment[]>(uret.UserErrorInfoObjectJson);
				var errtext = "This appointment overlaps with the following appointment of " +
					LookupDataProvider.ServiceProvider2Name(intersectionAppointments[0].ServiceProviderRowId) + ":\n" +
					string.Join("\n", intersectionAppointments.Select(q => "\t" + (hasMultiDates ? q.StartEndDateString : q.StartEndTimeString) + " - " + q.PatientFullName).ToArray());
				messageBoxService.ShowError(errtext);
				return false;
			}

			if (uret.UserErrorCode == UserErrorCodes.AppointmentPatientVacation)
			{
				var vacations = JsonConvert.DeserializeObject<CalendarEvent[]>(uret.UserErrorInfoObjectJson);
				var errtext = "This appointment overlaps with the following Patient Vacation:\n" +
					string.Join("\n", vacations.Select(q => q.StartEndDateVacationString + " - " + q.PatientFullName).ToArray());
				messageBoxService.ShowError(errtext);
				return false;
			}

			if (uret.UserErrorCode == SharedCode.UserErrorCodes.AppointmentServiceProviderVacation)
			{
				var vacations = JsonConvert.DeserializeObject<CalendarEvent[]>(uret.UserErrorInfoObjectJson);
				var errtext = "This appointment overlaps with the following Service Provider Vacation:\n" +
					string.Join("\n", vacations.Select(q => q.StartEndDateVacationString + " - " + "(*)" + q.ServiceProviderFullName).ToArray());
				messageBoxService.ShowError(errtext);
				return false;
				//DevExpress.Xpf.Editors.DateNavigator.DateNavigator
			}


			if (!uret.Validate(messageBoxService))
			{
				return false;
			}
			return true;
		}

		
		public void LoadAppointmentRemainders()
		{
			if (Entity.AppointmentRemainders != null)
			{
				RemainderInMinutesSet = Entity.AppointmentRemainders
					.Select(q => AppointmentRemainderEnumInfo.GetByMinutes(q.RemainderInMinutes))
					.Where(q => q != null)
					.Cast<object>()
					.ToList();
			}
		}
		public void SaveAppointmentRemainders()
		{
			Entity.AppointmentRemainders = RemainderInMinutesSet == null ? new AppointmentRemainder[0] :
				RemainderInMinutesSet
				.Select(q => new AppointmentRemainder { RemainderInMinutes = ((AppointmentRemainderEnumModel)q).Minutes })
				.ToArray();
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
					RowId = (isnew ? default(Guid) : Entity.PatientRowId),
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
					PickPatientViewModel.PickModeEnum.PickPatient,
					insuranceProvidersViewGroupRowId: OpenParam.InsuranceProvidersViewGroupRowId);
				if (!ret2.IsSuccess) return;

				Entity.PatientRowId = ret2.PickPatient.RowId;
				Entity.Patient = ret2.PickPatient;
				BuildInsuranceProviders(false);
			});
		}
		public bool CanFindPatient() => !OpenParam.IsLockPatient;


		public void SpinStartTime(DevExpress.Xpf.Editors.SpinEventArgs e) => SpinCore("StartTime", e);
		public void SpinFinishTime(DevExpress.Xpf.Editors.SpinEventArgs e) => SpinCore("FinishTime", e);
		public void SpinCore(String column, DevExpress.Xpf.Editors.SpinEventArgs e)
		{
			e.Handled = true;

			//StartTimeBaseEditConrol.DoValidate();
			//var bb = Entity.StartTime;
			//Debug.WriteLine("Entity.StartTime=" + bb);
			//Entity.StartTime += new TimeSpan(0, AppointmentBook.Interval, 0);
			//StartTimeBaseEditConrol.SelectNone();

			Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>
			{
				var interval = AppointmentBook?.Interval ?? 15;
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
				//StartTimeBaseEditConrol.SelectNone();
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



		void OnOpenInvoiceDetail()
		{
			DispatcherUIHelper.Run(() =>
			{
				if (Entity.InvoiceRowId != null)
				{
					ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
					{
						ViewCode = ViewCodes.InvoiceWindowView,
						Param = new InvoiceWindowViewModel.OpenParams
						{
							IsNew = false,
							RowId = (Guid)Entity.InvoiceRowId,
						},
					});
				}
			});
		}


		public void OpenInsuranceCoverage()
		{
			DispatcherUIHelper.Run(() =>
			{
				if (Entity.InsuranceCoverageRowId != null)
				{
					ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
					{
						ViewCode = ViewCodes.InsuranceCoverage2WindowView,
						Param = new InsuranceCoverage2ViewModel.OpenParams
						{
							IsNew = false,
							RowId = (Guid)Entity.InsuranceCoverageRowId,
							FamilyMembers = new List<Patient>(),
							HighlightPatientRowId = Entity.PatientRowId,
							HighlightCategoryRowId = LookupDataProvider.MedicalService2CategoryRowId(Entity.MedicalServicesOrSupplyRowId),
						},
					});
				}
			});
		}


		public void AddRowFromPopup(string column)
		{
			if (column == nameof(Entity.Status1RowId) || column == nameof(Entity.Status2RowId))
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneAppointmentStatusView,
					Param = "IsNew",
				});
			}
		}


		public void MultiDateOpen()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var ret2 = await PickAppointmentMultiDateViewModel.Pick(new PickAppointmentMultiDateViewModel.OpenParams
				{
					ShowDXWindowsInteractionRequest = ShowDXWindowsInteractionRequest,
					PickMode = PickAppointmentMultiDateViewModel.PickModeEnum.Main,
					SelectedDates = Entity.MultiDates.Select(q => q.Date),
					ServiceProviderRowId = Entity.ServiceProviderRowId,
					DaysInfo = OpenParam.DaysInfo,
				});
				if (!ret2.IsSuccess) return;

				Entity.MultiDates = ret2.SelectedDates.Select(q => new Appointment.MultiDateInfo { Date = q }).ToObservableCollection();
				Entity.UpdateMultiDatesText();
				DisableStartDate();
				BuildInsuranceProviders(false);
			});
		}


		public void DisableStartDate()
		{
			IsEnabledStartDate = false;
			IsButtonSaveEnabled = false;
			Entity.StartDate = null;
		}

		bool HasMultiDates => IsNew && Entity.MultiDates.Count > 0;

		public void AddOpenClinicalNote()
		{
			DispatcherUIHelper.Run(() =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.AppointmentClinicalNoteView,
					Param = new AppointmentClinicalNoteViewModel.OpenParams
					{
						IsNew = (Entity.AppointmentClinicalNoteRowId == null),
						RowId = (Entity.AppointmentClinicalNoteRowId == null ? default(Guid) : (Guid)Entity.AppointmentClinicalNoteRowId),
						AppointmentRowId = Entity.RowId,
					},
				});
			});
		}
		public bool CanAddOpenClinicalNote() => !IsNew;


		public void AddOpenTreatmentNote()
		{
			DispatcherUIHelper.Run(() =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.AppointmentTreatmentNoteView,
					Param = new AppointmentTreatmentNoteViewModel.OpenParams
					{
						IsNew = (Entity.AppointmentTreatmentNoteRowId == null),
						RowId = (Entity.AppointmentTreatmentNoteRowId == null ? default(Guid) : (Guid)Entity.AppointmentTreatmentNoteRowId),
						AppointmentRowIds = new[] { Entity.RowId },
					},
				});
			});
		}
		public bool CanAddOpenTreatmentNote() => !IsNew;



		public class OpenParams
		{
			public bool IsNew { get; set; }
			public Guid RowId { get; set; }

			public Guid AppointmentBookRowId { get; set; }
			public AppointmentsSchedulerViewModel.DaysInfoClass DaysInfo { get; set; }

			public DateTime? NewStart { get; set; }
			public DateTime? NewFinish { get; set; }
			public Guid? NewServiceProviderRowId { get; set; }
			public Patient NewPatient { get; set; }
			public Boolean IsLockPatient { get; set; }
			public Boolean IsReadOnly { get; set; }

			public Boolean IsNotRegisteredMode { get; set; }

			public Guid? InsuranceProvidersViewGroupRowId { get; set; }
		}


	}

}
