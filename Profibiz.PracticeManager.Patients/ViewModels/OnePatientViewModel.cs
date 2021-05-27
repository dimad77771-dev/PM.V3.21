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
using Profibiz.PracticeManager.Patients.BusinessService;
using DevExpress.Xpf.Core;
using Profibiz.PracticeManager.SharedCode;
using System.Diagnostics;
using System.Windows.Input;
using DevExpress.Xpf.Utils;
using DevExpress.XtraRichEdit;
using DevExpress.Pdf;
using System.IO;
using System.Drawing.Printing;
using Microsoft.Win32;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class OnePatientViewModel : ViewModelBase 
	{
		#region Services
		IPatientsBusinessService businessService;
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion

		public OpenParams OpenParam { get; set; }
		public virtual Patient Entity { get; set; }
		public bool IsNew { get; set; }
		public InvoicesListViewModel InvoiceListModel { get; set; }
		public CalendarEventsSchedulerViewModel CalendarEventsModel { get; set; }
		public MedicalHistoryRecordViewModel MedicalHistoryModel { get; set; }
		public TreatmentPlanRecordViewModel TreatmentPlanModel { get; set; }
		public virtual UIElementManager UIManagerFamilyDoctor { get; set; } = new UIElementManager();
		public virtual bool IsVisibilityRate { get; set; }
		public virtual bool IsVisibilityInsuranceCoverage { get; set; }
		public virtual bool IsVisibilityPatientNote { get; set; } = true;
		public virtual bool IsVisibilityPatientDocument { get; set; } = true;
		public virtual bool IsVisibilityAppointmentClinicalNote { get; set; } = true;
		public virtual bool IsVisibilityAppointmentTreatmentNote { get; set; } = false;
		public virtual bool IsVisibilityTreatmentPlan { get; set; } = false;




		~OnePatientViewModel()
		{
			NLog.Trace("~OnePatientViewModel=" + this.GetHashCode());
		}

		public OnePatientViewModel(IPatientsBusinessService _businessService) : base()
		{
			NLog.vv(() => "OnePatientViewModel.create=" + this.GetHashCode());
			businessService = _businessService;
		}

		public void OnOpen(OpenParams param)
		{
			NLog.vv(() => param?.RowId + ";" + param?.IsNew + ";" + this.GetHashCode());
			OpenParam = param;
			DispatcherUIHelper.Run(async () =>
			{
				await LoadData();
			});
		}

		async Task LoadData()
		{
			NLog.vv(() => OpenParam?.RowId + ";" + OpenParam?.IsNew);

			isLoadInvoiceListModel = false;
			isLoadCalendarEventsModel = false;
			isLoadMedicalHistoryModel = false;
			isLoadTreatmentPlanModel = false;
			ShowWaitIndicator.Show();

			BehaviorGridConrolAppointmentClinicalNoteEntities.Control.Loaded += Control_Loaded;

			IsNew = OpenParam.IsNew;
			Patient entity;
			if (!IsNew)
			{
				entity = await businessService.GetPatient(OpenParam.RowId);
				entity.AppointmentWithClinicalNotes.ForEach(q => SubscribeClinicalAppointmentRow(q));
				entity.AppointmentWithTreatmentNotes.ForEach(q => SubscribeTreatmentAppointmentRow(q));
				entity.AppointmentWithClinicalNotes.SetOnClickButtonAppointmentForm(AppointmentClinicalNoteClick);
				entity.PatientFormDocuments.SetOnClickButtonAppointmentForm(AppointmentClinicalNoteClick);
			}
			else
			{
				NLog.vv();
				entity = new Patient();
				entity.RowId = Guid.NewGuid();
				entity.Rate = DEFAULT_RATE;
				entity.Province1 = LookupDataProvider.PROVINCE_ONTARIO;
				entity.Province2 = LookupDataProvider.PROVINCE_ONTARIO;


				if (OpenParam.CreateNewFamilyMemberFromPatient != null)
				{
					var frow = OpenParam.CreateNewFamilyMemberFromPatient;
					entity.FamilyHeadRowId = frow.FamilyHeadRowId;
					entity.FamilyMemberType = TypeHelper.FamilyMemberType.Member;

					var copyColumns = new string[]
					{
						//"Address1", "Suburb1", "Postcode1", "Province1",
						//"Address2", "Suburb2", "Postcode2", "Province2",
						//"AddressToUse", "MiddleName", "HomePhone", "Occupation", "Fax",
						//"FamilyDoctor", "FamilyDoctorAddress", "FamilyDoctorTelephoneNumber",
					};
					foreach (var col in copyColumns)
					{
						var prop = typeof(Patient).GetProperty(col);
						var val = prop.GetValue(frow);
						prop.SetValue(Entity, val);
					}
				}
				else
				{
					entity.FamilyHeadRowId = entity.RowId;
					entity.FamilyMemberType = TypeHelper.FamilyMemberType.Head;
				}
				NLog.vv();
			}
			entity.ReadOnlyFamilyMemberType = true;
			if (entity.PatientCoverage == null)
			{
				entity.PatientCoverage = new ObservableCollection<PatientCoverage>();
			}
			entity.PatientDocuments.ForEach(q => PatientDocumentSubscribeRow(q));
			Entity = entity;
			IsVisibilityRate = IsNew;
			BuildFamilyMembersEntities();
			SubscribeUseHeadAddressChange();
			SubscribeHasNoCoverageChange();
			await LoadInvoiceListModel();
			await LoadCalendarEventsModel();
			await LoadMedicalHistoryModel();
			await LoadTreatmentPlanModel();
			ResetHasChange();

			ShowWaitIndicator.Hide();
			RegisterMessenges();
		}

		private void Control_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			//BehaviorGridConrolAppointmentClinicalNoteEntities.Control.Ex
			var a = 10;
		}

		void ReloadData()
		{
			OpenParam = new OpenParams { RowId = Entity.RowId };
			DispatcherUIHelper.Run(async () =>
			{
				await LoadData();
			});
		}

		bool isRegisterMessenges;
		void RegisterMessenges()
		{
			if (!isRegisterMessenges)
			{
				MessengerHelper.Register<MsgRowChange<InsuranceCoverage>>(this, OnMsgRowChangeInsuranceCoverage);
				MessengerHelper.Register<MsgRowChange<PatientNote>>(this, OnMsgRowChangePatientNote);
				MessengerHelper.Register<MsgRowChange<AppointmentTreatmentNote>>(this, OnMsgRowChangeTreatmentNote);
				MessengerHelper.Register<MsgRowChange<FormDocument>>(this, OnMsgRowChangeFormDocument);
				isRegisterMessenges = true;
			}
		}

		bool isLoadInvoiceListModel;
		async Task LoadInvoiceListModel()
		{
			InvoiceListModel.ViewMode = InvoicesListViewModel.ViewModes.OnePatient;
			InvoiceListModel.OnePatientRowId = Entity.RowId;
			InvoiceListModel.FilterFrom = DateTimeHelper.MIN_DATE;
			InvoiceListModel.FilterTo = DateTimeHelper.MAX_DATE;

			if (IsNew) return;
			if (isLoadInvoiceListModel) return;
			if (CurrentPage != PageEnum.Invoices) return;

			ShowWaitIndicator.Show();
			await InvoiceListModel.LoadData();
			ShowWaitIndicator.Hide();
			isLoadInvoiceListModel = true;
		}



		bool isLoadCalendarEventsModel;
		async Task LoadCalendarEventsModel()
		{
			CalendarEventsModel.ViewMode = CalendarEventsSchedulerViewModel.ViewModeEnum.OnePatient;
			//CalendarEventsModel.OnePatientRowId = Entity.RowId;
			//CalendarEventsModel.FilterFrom = DateTimeHelper.MIN_DATE;
			//CalendarEventsModel.FilterTo = DateTimeHelper.MAX_DATE;

			if (IsNew) return;
			if (isLoadCalendarEventsModel) return;
			if (CurrentPage != PageEnum.CalendarEvents) return;

			ShowWaitIndicator.Show();
			await CalendarEventsModel.SetOnePatient(Entity);
			//await CalendarEventsModel.LoadData();
			ShowWaitIndicator.Hide();
			isLoadCalendarEventsModel = true;
		}



		bool isLoadMedicalHistoryModel;
		async Task LoadMedicalHistoryModel()
		{
			MedicalHistoryModel.PatientRowId = Entity.RowId;

			if (IsNew) return;
			if (isLoadMedicalHistoryModel) return;
			if (CurrentPage != PageEnum.MedicalHistory) return;

			ShowWaitIndicator.Show();
			await MedicalHistoryModel.LoadData();
			ShowWaitIndicator.Hide();
			isLoadMedicalHistoryModel = true;
		}


		bool isLoadTreatmentPlanModel;
		async Task LoadTreatmentPlanModel()
		{
			TreatmentPlanModel.PatientRowId = Entity.RowId;

			if (IsNew) return;
			if (isLoadTreatmentPlanModel) return;
			if (CurrentPage != PageEnum.TreatmentPlan) return;

			ShowWaitIndicator.Show();
			await TreatmentPlanModel.LoadData();
			ShowWaitIndicator.Hide();
			isLoadTreatmentPlanModel = true;
		}


		void OnMsgRowChangeInsuranceCoverage(MsgRowChange<InsuranceCoverage> msg)
		{
			MessengerHelper.RunAction(this, () =>
			{
				NLog.vv(() => msg.RowAction + " -- " + msg.Row.RowId + " -- " + Entity.RowId + " -----" + this.GetHashCode());
				if (InsuranceCoverageEntities == null)
				{
					NLog.vv(() => "OnePatientViewModel.OnMsgRowChange=PROBLEM!!!");
				}
				MessengerHelper.UpdateEntities(this, InsuranceCoverageEntities, msg.Row, msg.RowAction, (a, b) => a.RowId == b.RowId, () => InsuranceCoverageSelectedEntity);
			});
		}

		void OnMsgRowChangePatientNote(MsgRowChange<PatientNote> msg)
		{
			MessengerHelper.RunAction(this, () =>
			{
				MessengerHelper.UpdateEntities(this, PatientNoteEntities, msg.Row, msg.RowAction, (a, b) => a.RowId == b.RowId, () => PatientNoteSelectedEntity);
			});
		}

		void OnMsgRowChangeTreatmentNote(MsgRowChange<AppointmentTreatmentNote> msg)
		{
			var appointmentWithTreatmentNotes = Entity.AppointmentWithTreatmentNotes;
			var erow = appointmentWithTreatmentNotes.SingleOrDefault(q => q.RowId == msg.Row.AppointmentRowId);
			if (erow != null)
			{
				if (msg.RowAction == RowAction.Delete)
				{
					erow.AppointmentTreatmentNoteRowId = null;
				}
				else
				{
					erow.AppointmentTreatmentNoteRowId = msg.Row.RowId;
				}
			}
		}

		void OnMsgRowChangeFormDocument(MsgRowChange<FormDocument> msg)
		{
			var msgAppointmentRowId = msg.Row.AppointmentRowId ?? default(Guid);
			var appointmentWithClinicalNote = 
				Entity.AppointmentWithClinicalNotes.Union(new[] { Entity.PatientFormDocuments }).SingleOrDefault(q => q.RowId == msgAppointmentRowId);
			if (appointmentWithClinicalNote == null)
			{
				return;
			}

			var isChanged = Entity.IsChanged;

			if (msg.RowAction == RowAction.Delete)
			{
				var formDocument = appointmentWithClinicalNote.FormDocuments.SingleOrDefault(q => q.RowId == msg.Row.RowId);
				if (formDocument != null)
				{
					appointmentWithClinicalNote.FormDocuments.Remove(formDocument);
					BaseModelHelper.RaisePropertyChanged(appointmentWithClinicalNote, nameof(appointmentWithClinicalNote.ButtonsAppointmentForm));
				}
			}
			else if (msg.RowAction == RowAction.Insert)
			{
				appointmentWithClinicalNote.FormDocuments.Insert(0, msg.Row);
				BaseModelHelper.RaisePropertyChanged(appointmentWithClinicalNote, nameof(appointmentWithClinicalNote.ButtonsAppointmentForm));
			}

			Entity.IsChanged = isChanged;
		}



		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true)); 


		void BuildFamilyMembersEntities()
		{
			if (IsNew)
			{
				FamilyMembersEntities = new ObservableCollection<Patient>();
				FamilyMembersEntities.Add(Entity);
				if (OpenParam.CreateNewFamilyMemberFromPatientFamilyMembersEntities != null)
				{
					FamilyMembersEntities.AddRange(OpenParam.CreateNewFamilyMemberFromPatientFamilyMembersEntities);
				}
			}
			else
			{
				FamilyMembersEntities = new ObservableCollection<Patient>();
				FamilyMembersEntities.AddRange(Entity.FamilyMembers);
				FamilyMembersEntities.Add(Entity);
			}
			FamilyMembersEntities = FamilyMembersEntities.OrderBy(q => q.IsFamilyHead ? 0 : 1).ThenBy(q => q.FirstName).ToObservableCollection();
			RibbonFamilyMemberItems = FamilyMembersEntities.Select(q => new RibbonFamilyMemberItem
			{
				Entity = q,
				IsCurrent = (q == Entity),
				ParentModel = this,
			}).ToObservableCollection();
			RibbonFamilyMemberColumnCount = Math.Max((RibbonFamilyMemberItems.Count + 1) / 2, 1);
		}

		

		bool Validate()
		{
			List<string> errors = new List<string>();

			Entity.IsErrorInfoWork = true;
			var dataErrorInfo = (Entity as IDataErrorInfo);
			var cols = new[] { "FirstName", "LastName", "Rate" };
			errors.AddRange(cols.Select(q => dataErrorInfo[q]));
			errors.Add(dataErrorInfo.Error);
			errors = errors.Where(q => !string.IsNullOrEmpty(q)).ToList();

			if (Entity.PatientCoverage.Any(q => q.InsuranceProviderRowId == default(Guid)))
			{
				errors.Add("\"INSURANCE PROVIDER\" is empty");
			}
			if (Entity.PatientCoverage.Any(q => q.MedicalServiceOrSupplyRowId == default(Guid)))
			{
				errors.Add("\"MEDICAL SERVICE/SUPPLY\" is empty");
			}
			

			if (errors.Count > 0)
			{
				Entity.BaseRaisePropertyChanged("FirstName");

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

			if (!await MedicalHistoryModel.Save())
			{
				return false;
			}

			if (!await TreatmentPlanModel.Save())
			{
				return false;
			}

			//updateEntity
			var updateEntity = Entity.GetPocoClone();
			updateEntity.PatientCoverage = Entity.PatientCoverage.Select(q => q.GetPocoClone()).ToObservableCollection();

			//IsAddressChanged
			if (!IsNew && Entity.IsFamilyHead && Entity.IsAddressChanged)
			{
				var useHeadAddressFamilyMembers = Entity.FamilyMembers.Where(q => q.UseHeadAddress).ToList();
				if (useHeadAddressFamilyMembers.Count > 0)
				{
					foreach (var useHeadAddressFamilyMember in useHeadAddressFamilyMembers)
					{
						Patient.CopyAddressFields(Entity, useHeadAddressFamilyMember);
					}
					updateEntity.ChangeFamilyMembersAddress = true;
					updateEntity.FamilyMembers = useHeadAddressFamilyMembers.Select(q => q.GetPocoClone()).ToObservableCollection();
				}
			}


			//save
			var ret = await UpdateDatabaseCore(updateEntity);
			if (ret.UserErrorCode == UserErrorCodes.PatientNameDuplicate)
			{
				var messageBoxService = this.GetRequiredService<IMessageBoxService>();
				var errtext = "Patient Records for the " + updateEntity.LastName + " " + updateEntity.FirstName + " already exists. " +
								"Are you sure you want to " + (IsNew ? "create" : "update") + " this patient record?";
				var ret2 = messageBoxService.ShowMessage(errtext, CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret2 != MessageResult.Yes)
				{
					return false;
				}

				updateEntity.IgnoreDuplicateLastFirstNameFlag = true;
				ret = await UpdateDatabaseCore(updateEntity);
			}
			if (!ret.Validate(MessageBoxService))
			{
				return false;
			}
			MessengerHelper.SendMsgRowChange(Entity, IsNew, "NewActionBookmark=" + OpenParam.NewActionBookmark);
			IsNew = false;
			ResetHasChange();

			//close
			if (andClose)
			{
				CloseCore(force:true);
			}

			return true;
		}

		async Task<UpdateReturn> UpdateDatabaseCore(Patient updateEntity)
		{
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var ret = IsNew ?
				await businessService.PostPatient(updateEntity) :
				await businessService.PutPatient(updateEntity);
			ShowWaitIndicator.Hide();
			return ret;
		}

		public void Close() => CloseCore();

		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		bool forceClose;
		void CloseCore(bool force = false)
		{
			this.forceClose = force;
			CloseInteractionRequest.Raise(null);
		}

		bool HasChange()
		{
            if(Entity == null)
            {
                return false;
            }

			if (MedicalHistoryModel?.HasChange() == true)
			{
				return true;
			}

			if (TreatmentPlanModel?.HasChange() == true)
			{
				return true;
			}


			return (IsNew || Entity.IsChanged);
		}

		void ResetHasChange()
		{
			Entity.IsErrorInfoWork = false;
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
				if (ret == MessageResult.Cancel || ret == MessageResult.None)
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
				else throw new ArgumentException("ret=" + ret.ToString());
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


		#region InsuranceCoverage
		public virtual ObservableCollection<InsuranceCoverage> InsuranceCoverageEntities => Entity?.InsuranceCoverages;
		public virtual InsuranceCoverage InsuranceCoverageSelectedEntity { get; set; }
		public void InsuranceCoverageNew()
		{
			InsuranceCoverageAddEdit(null);
		}
		public void InsuranceCoverageEdit()
		{
			InsuranceCoverageAddEdit(InsuranceCoverageSelectedEntity);
		}
		void InsuranceCoverageAddEdit(InsuranceCoverage row)
		{
			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.InsuranceCoverage2WindowView,
				Param = new InsuranceCoverage2ViewModel.OpenParams
				{
					IsNew = (row == null),
					RowId = (row == null ? default(Guid) : row.RowId),
					FamilyMembers = this.FamilyMembersEntities,
				},
			});
		}
		public void InsuranceCoverageDelete()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var row = InsuranceCoverageSelectedEntity;
				var messageBoxService = this.GetRequiredService<IMessageBoxService>();
				var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "Insurance Coverage"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
					var uret = await businessService.DeleteInsuranceCoverage(row.RowId);
					ShowWaitIndicator.Hide();
					if (!uret.Validate(MessageBoxService)) return;
					InsuranceCoverageEntities.Remove(row);
				}
			});
		}
		public bool CanInsuranceCoverageNew() => (SelectedIndex == 3);
		public bool CanInsuranceCoverageEdit() => (SelectedIndex == 3 && InsuranceCoverageSelectedEntity != null);
		public bool CanInsuranceCoverageDelete() => (SelectedIndex == 3 && InsuranceCoverageSelectedEntity != null);
		public bool ShowRibbonInsuranceCoverage => (SelectedIndex == 3);
		#endregion



		#region PatientNote
		public virtual ObservableCollection<PatientNote> PatientNoteEntities => Entity?.PatientNotes;
		public virtual PatientNote PatientNoteSelectedEntity { get; set; }
		public void PatientNoteNew()
		{
			PatientNoteAddEdit(null);
		}
		public void PatientNoteEdit()
		{
			PatientNoteAddEdit(PatientNoteSelectedEntity);
		}
		void PatientNoteAddEdit(PatientNote row)
		{
			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.OnePatientNoteView,
				Param = new OnePatientNoteViewModel.OpenParams
				{
					IsNew = (row == null),
					RowId = (row == null ? default(Guid) : row.RowId),
					PatientRowId = Entity.RowId,
				},
			});
		}
		public void PatientNoteDelete()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var row = PatientNoteSelectedEntity;
				var messageBoxService = this.GetRequiredService<IMessageBoxService>();
				var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "Note"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
					var uret = await businessService.DeletePatientNote(row.RowId);
					ShowWaitIndicator.Hide();
					if (!uret.Validate(MessageBoxService)) return;
					PatientNoteEntities.Remove(row);
				}
			});
		}
		public bool CanPatientNoteNew() => (SelectedIndex == 6);
		public bool CanPatientNoteEdit() => (SelectedIndex == 6 && PatientNoteSelectedEntity != null);
		public bool CanPatientNoteDelete() => (SelectedIndex == 6 && PatientNoteSelectedEntity != null);
		public bool ShowRibbonPatientNote => (SelectedIndex == 6);
		#endregion


		#region PatientDocument
		public virtual ObservableCollection<PatientDocument> PatientDocumentEntities => Entity?.PatientDocuments;
		public virtual PatientDocument PatientDocumentSelectedEntity { get; set; }
		public void PatientDocumentNew() => PatientDocumentAddEdit(null);
		public void PatientDocumentPaste() => PatientDocumentAddEdit(null, isPaste: true);
		public void PatientDocumentEdit() => PatientDocumentAddEdit(PatientDocumentSelectedEntity);
		void PatientDocumentAddEdit(PatientDocument row, bool isPaste = false)
		{
			DispatcherUIHelper.Run(async () =>
			{
				var isNew = (row == null);
				if (isNew)
				{
					if (isPaste)
					{
						ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
						var fileinfo = ClipboardRemoteFileFunc.Run();
						ShowWaitIndicator.Hide();
						if (fileinfo == null)
						{
							MessageBoxService.ShowError("Clipboard has no files");
							return;
						}
						await PatientDocumentAddCore(fileinfo.Files, fileinfo.FileContentsBytes);
					}
					else
					{
						var openFileDialog = new OpenFileDialog();
						if (openFileDialog.ShowDialog() != true) return;

						var filename = openFileDialog.FileName;
						var bytes = File.ReadAllBytes(filename);
						await PatientDocumentAddCore(new[] { filename }, new[] { bytes });
					}
				}
				else
				{
					ShowWaitIndicator.Show();
					var row2 = (await businessService.GetPatientDocumentList("rowId=" + row.RowId.ToWebQuery())).Single();
					ShowWaitIndicator.Hide();

					var filename = row2.FileName;
					var filename2 = EmailSendsListViewModel.GetPdfFile(filename);
					File.WriteAllBytes(filename2, row2.BinaryDocument);
					Process.Start(filename2);
				}
			});


		}

		async Task PatientDocumentAddCore(string[] files, byte[][] fileBytes)
		{
			var param = new PatientDocumentViewModel.OpenParams
			{
				IsNew = true,
			};
			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.PatientDocumentView,
				Param = param,
			});
			var row = param.NewEntity;
			if (row == null) return;


			var mouseWaitCursor = new MouseWaitCursor();
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			try
			{
				for(var i = 0; i < files.Length; i++)
				{
					var file = files[i];
					var docbytes = fileBytes[i];
					var filetype = Path.GetExtension(file);
					if (filetype.Substring(0, 1) == ".")
					{
						filetype = filetype.Substring(1);
					}
					var filename = Path.GetFileNameWithoutExtension(file);


					row.PatientRowId = Entity.RowId;
					row.CreateDateTime = DateTimeHelper.Now;
					row.FileName = filename + "." + filetype;
					row.BinaryDocument = docbytes;
					PatientDocumentSubscribeRow(row);

					var uret = await businessService.PostPatientDocument(row);
					if (!uret.Validate(MessageBoxService)) return;
					PatientDocumentEntities.Insert(0, row);
					PatientDocumentSelectedEntity = row;
				}
			}
			finally
			{
				mouseWaitCursor.Dispose();
				ShowWaitIndicator.Hide();
			}
		}


		void PatientDocumentSubscribeRow(PatientDocument row)
		{
			row.OnOpenDetail += () =>
			{
				PatientDocumentAddEdit(row);
			};
		}

		public void PatientDocumentDelete()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var row = PatientDocumentSelectedEntity;
				var messageBoxService = this.GetRequiredService<IMessageBoxService>();
				var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "Document"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
					var uret = await businessService.DeletePatientDocument(row.RowId);
					ShowWaitIndicator.Hide();
					if (!uret.Validate(MessageBoxService)) return;
					PatientDocumentEntities.Remove(row);
				}
			});
		}
		public bool CanPatientDocumentNew() => (SelectedIndex == 10);
		public bool CanPatientDocumentEdit() => (SelectedIndex == 10 && PatientDocumentSelectedEntity != null);
		public bool CanPatientDocumentDelete() => (SelectedIndex == 10 && PatientDocumentSelectedEntity != null);
		public bool ShowRibbonPatientDocument => (SelectedIndex == 10);
		#endregion




		#region AppointmentClinicalNote
		public virtual ObservableCollection<Appointment> AppointmentClinicalNoteEntities => Entity?.AppointmentWithClinicalNotes;
		public virtual Appointment AppointmentClinicalNoteSelectedEntity { get; set; }
		public virtual ObservableCollection<Appointment> AppointmentClinicalNoteSelectedEntities { get; set; } = new ObservableCollection<Appointment>();
		public virtual GridControlBehaviorManager BehaviorGridConrolAppointmentClinicalNoteEntities { get; set; } = new GridControlBehaviorManager();
		public void AppointmentClinicalNoteEdit(Appointment row)
		{
			if (row == null) return;

			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.AppointmentClinicalNoteView,
				Param = new AppointmentClinicalNoteViewModel.OpenParams
				{
					IsNew = false,
					RowId = (Guid)row.AppointmentClinicalNoteRowId,
					IsReadOnly = true,
				},
			});
		}
		void SubscribeClinicalAppointmentRow(Appointment row)
		{
			row.OnOpenDetail = () =>
			{
				AppointmentClinicalNoteEdit(row);
			};
		}
		public void AppointmentClinicalNoteClick(Appointment appointment, FormDocument formDocument)
		{
			DispatcherUIHelper.Run(async () =>
			{
				FormDocumentViewModel.OpenParams parm;
				if (formDocument == null)
				{
					Guid? patientRowId = null;
					Guid? appointmentRowId = null;
					if (appointment.RowId == default(Guid))
					{
						patientRowId = Entity.RowId;
					}
					else
					{
						appointmentRowId = appointment.RowId;
					}

					var template = await FormDocumentHelper.PickTemplateName(appointment, MessageBoxService, ShowDXWindowsInteractionRequest);
					if (template == null)
					{
						return;
					}

					parm = new FormDocumentViewModel.OpenParams
					{
						IsNew = true,
						RowId = default(Guid),
						TemplatePath = template.TemplatePath,
						TemplateName = template.TemplateName,
						AppointmentRowId = appointmentRowId,
						PatientRowId = patientRowId,
					};
				}
				else
				{
					parm = new FormDocumentViewModel.OpenParams
					{
						IsNew = false,
						RowId = formDocument.RowId,
						AppointmentRowId = appointment.RowId,
					};
				}

				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.FormDocumentView,
					Param = parm,
				});
			});
		}

		public void AppointmentClinicalNotePrint() => AppointmentClinicalNotePdfCore(AppointmentClinicalNotePdfCoreMode.PrintPdf);
		public bool CanAppointmentClinicalNotePrint() => AppointmentClinicalNoteSelectedEntities.Any();

		public void AppointmentClinicalNotePdfBuild() => AppointmentClinicalNotePdfCore(AppointmentClinicalNotePdfCoreMode.OpenPdf);
		public bool CanAppointmentClinicalNotePdfBuild() => AppointmentClinicalNoteSelectedEntities.Any();

		enum AppointmentClinicalNotePdfCoreMode { OpenPdf, PrintPdf };
		void AppointmentClinicalNotePdfCore(AppointmentClinicalNotePdfCoreMode mode)
		{
			DispatcherUIHelper.Run(async () =>
			{
				ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Custom, "Build pdf...");
				var pdfDocumentProcessor = new PdfDocumentProcessor();
				pdfDocumentProcessor.CreateEmptyDocument();

				foreach (var arow in AppointmentClinicalNoteSelectedEntities)
				{
					var appointmentClinicalNote = (await businessService.GetAppointmentClinicalNoteList("rowid=" + arow.AppointmentClinicalNoteRowId)).First();
					var pdfbytes2 = RichTextFunc.ConvertDocxToPdf(appointmentClinicalNote.ClinicalNoteDocx);
					pdfDocumentProcessor.AppendDocument(new MemoryStream(pdfbytes2));
				}

				var pdfstream = new MemoryStream();
				pdfDocumentProcessor.SaveDocument(pdfstream);
				var pdfbytes = pdfstream.ToArray();

				if (mode == AppointmentClinicalNotePdfCoreMode.PrintPdf)
				{
					pdfDocumentProcessor.Print(new PdfPrinterSettings());
				}
				else if (mode == AppointmentClinicalNotePdfCoreMode.OpenPdf)
				{
					var pdffile = EmailSendsListViewModel.GetPdfFile("AppointmentClinicalNotes.pdf");
					File.WriteAllBytes(pdffile, pdfbytes);
					Process.Start(pdffile);
				}

				pdfstream.Dispose();
				pdfDocumentProcessor.Dispose();
				ShowWaitIndicator.Hide();
			});
		}


		#endregion


		#region AppointmentTreatmentNote
		public virtual ObservableCollection<Appointment> AppointmentTreatmentNoteEntities => Entity?.AppointmentWithTreatmentNotes;
		public virtual Appointment AppointmentTreatmentNoteSelectedEntity { get; set; }
		public virtual ObservableCollection<Appointment> AppointmentTreatmentNoteSelectedEntities { get; set; } = new ObservableCollection<Appointment>();
		public void AppointmentTreatmentNoteEdit(Appointment row)
		{
			if (row == null) return;

			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.AppointmentTreatmentNoteView,
				Param = new AppointmentTreatmentNoteViewModel.OpenParams
				{
					IsNew = row.AppointmentTreatmentNoteExists ? false : true,
					RowId = row.AppointmentTreatmentNoteExists ? (Guid)row.AppointmentTreatmentNoteRowId : default(Guid),
					AppointmentRowIds = new[] { row.RowId },
				},
			});
		}
		void SubscribeTreatmentAppointmentRow(Appointment row)
		{
			row.OnOpenDetail = () =>
			{
				AppointmentTreatmentNoteEdit(row);
			};
		}


		public void AppointmentTreatmentNoteAdd()
		{
			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.AppointmentTreatmentNoteView,
				Param = new AppointmentTreatmentNoteViewModel.OpenParams
				{
					IsNew = true,
					AppointmentRowIds = AppointmentTreatmentNoteSelectedEntities.Select(q => q.RowId).ToArray(),
				},
			});
		}
		public bool CanAppointmentTreatmentNoteAdd() => 
			AppointmentTreatmentNoteSelectedEntities.Any() && 
			!AppointmentTreatmentNoteSelectedEntities.Where(q => q.AppointmentTreatmentNoteExists).Any();




		#endregion

		public virtual Int32 SelectedIndex { get; set; }
		public PageEnum CurrentPage => GetPage(SelectedIndex);
		public PageEnum GetPage(int index) => EnumFunc.GetValues<PageEnum>()[index];
		public bool IsGroupPage => (CurrentPage == PageEnum.Insurances);
		public enum PageEnum { Property, MedicalHistory, TreatmentPlan, Insurances, Invoices, CalendarEvents, PatientNote, AppointmentClinicalNote, AppointmentTreatmentNote, Document }


		public virtual ObservableCollection<Patient> FamilyMembersEntities { get; set; }
		public virtual Patient FamilyMembersSelectedEntity { get; set; }
		public virtual Patient FamilyHeaderEntity => FamilyMembersEntities.SingleOrDefault(q => q.IsFamilyHead);


		public virtual ObservableCollection<RibbonFamilyMemberItem> RibbonFamilyMemberItems { get; set; }
		public int RibbonFamilyMemberColumnCount { get; set; }
		[ImplementPropertyChanged]
		public class RibbonFamilyMemberItem
		{
			public bool IsCurrent { get; set; }
			public Patient Entity { get; set; }
			public OnePatientViewModel ParentModel { get; set; }

			public DelegateCommand SubmitCommand => new DelegateCommand(async () =>
			{
				await ParentModel.OpenFamilyMember(Entity);
			});
		}

		public async Task OpenFamilyMember(Patient openEntity)
		{
			if (openEntity == Entity) return;
			if (!await OnClose()) return;

			OpenParam = new OpenParams { RowId = openEntity.RowId };
			await LoadData();
		}


		public void RefreshData()
		{
			DispatcherUIHelper.Run(async () =>
			{
				if (!await OnClose()) return;
				await LoadData();
			});
		}


		public async void FamilyMemberNew()
		{
			if (!await OnClose()) return;

			OpenParam = new OpenParams
			{
				IsNew = true,
				RowId = default(Guid),
				CreateNewFamilyMemberFromPatient = Entity,
				CreateNewFamilyMemberFromPatientFamilyMembersEntities = FamilyMembersEntities,
			};
			await LoadData();
		}

		public async void FamilyMemberDelete()
		{
			var ret = MessageBoxService.ShowMessage("Delete member \"" + Entity.FullName + "\" from family?", CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret != MessageResult.Yes) return;
			if (!await OnClose(showOKCancel: true)) return;

			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var updateEntity = Entity.GetPocoClone();
			updateEntity.FamilyHeadRowId = Entity.RowId;
			updateEntity.FamilyMemberType = TypeHelper.FamilyMemberType.Head;
			updateEntity.ChangeFamilyMember = new Patient.ChangeFamilyMemberInfo { Action = Patient.ChangeFamilyMemberInfo.ActionEnum.RemoveFromFamily };
			var uret = await businessService.PutPatient(updateEntity);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService)) return;
			MessengerHelper.SendMsgRowChange(updateEntity, IsNew);
			

			ReloadData();
		}
		public bool CanFamilyMemberDelete()
		{
			return (Entity != null && !Entity.IsFamilyHead && !Entity.UseHeadAddress);
		}


		public async void FamilyMemberMakeHeader()
		{
			var ret = MessageBoxService.ShowMessage("Make \"" + Entity.FullName + "\" head of the family?", CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret != MessageResult.Yes) return;
			if (!await OnClose(showOKCancel: true)) return;

			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);

			var updateEntity = Entity.GetPocoClone();
			var newFamilyHeadRowId = Entity.RowId;
			updateEntity.FamilyHeadRowId = newFamilyHeadRowId;
			updateEntity.FamilyMemberType = TypeHelper.FamilyMemberType.Head;
			updateEntity.ChangeFamilyMember = new Patient.ChangeFamilyMemberInfo { Action = Patient.ChangeFamilyMemberInfo.ActionEnum.MoveMemberToHeader };
			var uret = await businessService.PutPatient(updateEntity);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService)) return;

			foreach (var otherMember in FamilyMembersEntities.Where(q => q.RowId != Entity.RowId))
			{
				otherMember.FamilyHeadRowId = newFamilyHeadRowId;
				otherMember.FamilyMemberType = TypeHelper.FamilyMemberType.Member;
				MessengerHelper.SendMsgRowChange(otherMember, isNew: false);
			}
			MessengerHelper.SendMsgRowChange(updateEntity, isNew: false);

			

			ReloadData();
		}
		public bool CanFamilyMemberMakeHeader()
		{
			return (Entity != null && !Entity.IsFamilyHead && !Entity.UseHeadAddress);
		}

        
		public async void FamilyMemberMoveToAnotherFamily()
		{
			if (!await OnClose(showOKCancel: true)) return;

			var ret2 = await PickPatientViewModel.PickPatient(ShowDXWindowsInteractionRequest, PickPatientViewModel.PickModeEnum.PickFamily);
			if (!ret2.IsSuccess) return;
			var pickPatient = ret2.PickPatient;
			var newFamilyHeadRowId = pickPatient.FamilyHeadRowId;

			if (Entity.FamilyHeadRowId == newFamilyHeadRowId)
			{
				MessageBoxService.ShowMessage("\"" + Entity.FullName + "\" is already in family with \"" + pickPatient.FullName + "\"", CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
				return;
			}

			var ret = MessageBoxService.ShowMessage("Move \"" + Entity.FullName + "\" to the \"" + pickPatient.FullName + "\" family?", CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret != MessageResult.Yes) return;
			
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);

			var updateEntity = Entity.GetPocoClone();
			updateEntity.FamilyHeadRowId = newFamilyHeadRowId;
			updateEntity.FamilyMemberType = TypeHelper.FamilyMemberType.Member;
			updateEntity.ChangeFamilyMember = new Patient.ChangeFamilyMemberInfo
			{
				Action = Patient.ChangeFamilyMemberInfo.ActionEnum.MoveToAnotherFamily,
				NewFamilyHeadRowId = newFamilyHeadRowId,
			};
			var uret = await businessService.PutPatient(updateEntity);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService)) return;
			MessengerHelper.SendMsgRowChange(updateEntity, isNew: false);
		

			ReloadData();
		}
		public bool CanFamilyMemberMoveToAnotherFamily()
		{
			if (Entity != null && Entity.UseHeadAddress)
			{
				return false;
			}
			return 
			(
				(Entity != null && !Entity.IsFamilyHead) || 
				(FamilyMembersEntities != null && FamilyMembersEntities.Count == 1)
			);
		}

		void SubscribeUseHeadAddressChange()
		{
			(Entity as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(Entity.UseHeadAddress))
				{
					OnUseHeadAddressChanged();
				}
			};
		}

		void OnUseHeadAddressChanged()
		{
			if (Entity.UseHeadAddress)
			{
				Patient.CopyAddressFields(FamilyHeaderEntity, Entity);
			}
		}


		public async void PageSelectionChanging(TabControlSelectionChangingEventArgs arg)
		{
			arg.Cancel = true;
			if (!await OnClose(showOKCancel: true)) return;

			//if (GetPage(arg.NewSelectedIndex) == PageEnum.Insurances)
			//{
			//	ShowWaitIndicator.Show();
			//	await GroupsViewModel.LoadData();
			//	ShowWaitIndicator.Hide();
			//}
			SelectedIndex = arg.NewSelectedIndex;
			DispatcherUIHelper.Run(async () =>
			{
				await LoadInvoiceListModel();
				await LoadCalendarEventsModel();
				await LoadMedicalHistoryModel();
				await LoadTreatmentPlanModel();
			});
		}

		public void AddRowFromPopup(string column)
		{
			if (column == nameof(Entity.ReferrerRowId))
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneReferrerView,
					Param = "IsNew",
				});
			}
		}

		public string FamilyDoctorPrefix = "Dr. ";
		public void OnGotFocusFamilyDoctor()
		{
			if (string.IsNullOrEmpty(Entity.FamilyDoctor))
			{
				DispatcherUIHelper.Run(() =>
				{
					var isChanged = Entity.IsChanged;
					Entity.FamilyDoctor = FamilyDoctorPrefix;
					if (!isChanged)
					{
						Entity.IsChanged = false;
					}
					UIManagerFamilyDoctor.SelectionStart = FamilyDoctorPrefix.Length + 1;
					UIManagerFamilyDoctor.SelectionLength = 0;
				});
			}
		}
		public void OnLostFocusFamilyDoctor()
		{
			if (Entity.FamilyDoctor == FamilyDoctorPrefix)
			{
				var isChanged = Entity.IsChanged;
				Entity.FamilyDoctor = null;
				if (!isChanged)
				{
					Entity.IsChanged = false;
				}
			}
		}

		public void PreviewKeyDown(KeyEventArgs e)
		{
			if (e.Key == Key.R && KeyboardHelper.IsShiftPressed && KeyboardHelper.IsControlPressed && KeyboardHelper.IsAltPressed)
			{
				IsVisibilityRate = !IsVisibilityRate;
			}
		}


		void SubscribeHasNoCoverageChange()
		{
			UpdateIsVisibilityInsuranceCoverage();
			(Entity as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(Entity.HasNoCoverage))
				{
					UpdateIsVisibilityInsuranceCoverage();
					Entity.Rate = (Entity.HasNoCoverage ? 100M : DEFAULT_RATE);
					IsVisibilityRate = true;
				}
			};
		}
		void UpdateIsVisibilityInsuranceCoverage()
		{
			IsVisibilityInsuranceCoverage = (Entity?.HasNoCoverage != true);
		}




		public bool CanMedicalHistoryNew() => (SelectedIndex == 1);
		public bool CanMedicalHistoryEdit() => (SelectedIndex == 1);
		public bool CanMedicalHistoryDelete() => (SelectedIndex == 1);
		public bool ShowRibbonMedicalHistory => (SelectedIndex == 1);


		public bool CanTreatmentPlanNew() => (SelectedIndex == 2);
		public bool CanTreatmentPlanEdit() => (SelectedIndex == 2);
		public bool CanTreatmentPlanDelete() => (SelectedIndex == 2);
		public bool ShowRibbonTreatmentPlan => (SelectedIndex == 2);



		const decimal DEFAULT_RATE = 50M;





		public class OpenParams
		{
			public bool IsNew { get; set; }
			public Guid RowId { get; set; }
			public Patient CreateNewFamilyMemberFromPatient { get; set; }
			public IEnumerable<Patient> CreateNewFamilyMemberFromPatientFamilyMembersEntities { get; set; }
			public Guid NewActionBookmark { get; set; }
		}


	}

}
