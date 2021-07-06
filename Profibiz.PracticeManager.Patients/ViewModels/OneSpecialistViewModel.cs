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
using System.Diagnostics;
using System.Windows.Input;
using DevExpress.Xpf.Utils;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class OneSpecialistViewModel : ViewModelBase 
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> OpenWindowInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion
		public OpenParams OpenParam { get; set; }
		public virtual ServiceProvider Entity { get; set; }
		public bool IsNew { get; set; }
		public virtual ObservableCollection<ServiceProviderAssociation> ProfessionalAssociationEntities { get; set; }
		public virtual ServiceProviderAssociation ProfessionalAssociationSelectedEntity { get; set; }
		public virtual ObservableCollection<ServiceProviderService> MedicalServiceEntities { get; set; }
		public virtual ServiceProviderService MedicalServiceSelectedEntity { get; set; }
		public virtual bool IsVisibilityRate { get; set; }
		public virtual bool IsVisibleLabelYouMustSetupSchedule { get; set; }
		public virtual bool IsRestrictedAccess => OpenParam.IsRestrictedAccess;
		public virtual bool AllowEditingGridService => !IsRestrictedAccess;
		public virtual bool AllowEditingGridAssociations => !IsRestrictedAccess;
		public virtual bool IsReadOnlyPassword => (Entity?.RowId != UserManager.UserRowId && !IsNew);


		public OneSpecialistViewModel() : base()
		{
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
			ServiceProvider entity;
			Debug.WriteLine("LoadData=" + OpenParam.RowId);
			if (!IsNew)
			{
				var rows = await businessService.GetServiceProviderList("rowid=" + OpenParam.RowId);
				entity = rows.Single();
				entity.PasswordConfirm = entity.Password;
				var schedulerRecords = await businessService.GetSchedulerRecords(OpenParam.RowId);
				IsVisibleLabelYouMustSetupSchedule = !schedulerRecords.Any();
			}
			else
			{
				entity = new ServiceProvider();
				entity.RowId = Guid.NewGuid();
				entity.Province = LookupDataProvider.PROVINCE_ONTARIO;
				entity.ServiceType = TypeHelper.ServiceProviderServiceType.InHouse;
				entity.EmploymentType = TypeHelper.ServiceProviderEmploymentType.Service;
				entity.MaximumDayAppointments = 0;
				IsVisibleLabelYouMustSetupSchedule = true;
			}
			Entity = entity;
			Entity.ServiceProviderAssociations = Entity.ServiceProviderAssociations.OrderByDescending(q => q.IsPrimary).ThenBy(q => q.RegistrationDate).ToObservableCollection();
			ProfessionalAssociationEntities = Entity.ServiceProviderAssociations;
			ProfessionalAssociationEntities.ForEach(q => AlterProfessionalAssociationEntity(q));
			MedicalServiceEntities = Entity.ServiceProviderServices;
			MedicalServiceEntities.ForEach(q => AlterMedicalServiceEntity(q));
			ResetHasChange();
			ShowWaitIndicator.Hide();
		}

		void AlterProfessionalAssociationEntity(ServiceProviderAssociation erow)
		{
			(erow as INotifyPropertyChanged).PropertyChanged += (sender, e) =>
			{
				var row = (ServiceProviderAssociation)sender;
				if (row.IsPrimary)
				{
					ProfessionalAssociationEntities.Where(q => q != row).ForEach(q => q.IsPrimary = false);
				}
			};
			erow.OnAddRowFromPopup = (column) =>
			{
				OpenWindowInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneProfessionalAssociationView,
					Param = "IsNew",
				});
			};
		}


		void AlterMedicalServiceEntity(ServiceProviderService erow)
		{
			//(erow as INotifyPropertyChanged).PropertyChanged += (sender, e) =>
			//{
			//	var row = (ServiceProviderAssociation)sender;
			//	if (row.IsPrimary)
			//	{
			//		ProfessionalAssociationEntities.Where(q => q != row).ForEach(q => q.IsPrimary = false);
			//	}
			//};
			erow.OnAddRowFromPopup = (column) =>
			{
				OpenWindowInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneMedicalServiceView,
					Param = "IsNew;ItemType=" + TypeHelper.MedicalItemType.Service,
				});
			};
		}


		public void ProfessionalAssociationNew()
		{
			var row = new ServiceProviderAssociation()
			{
				RowId = Guid.NewGuid(),
				ServiceProviderRowId = Entity.RowId,
			};
			AlterProfessionalAssociationEntity(row);
			ProfessionalAssociationEntities.Add(row);
			ProfessionalAssociationSelectedEntity = row;
		}
		public void ProfessionalAssociationDelete()
		{
			var row = ProfessionalAssociationSelectedEntity;
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "row"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				ProfessionalAssociationEntities.Remove(row);
				Entity.IsChanged = true;
			}
		}
		public bool CanProfessionalAssociationDelete()
		{
			return (ProfessionalAssociationSelectedEntity != null);
		}




		public void MedicalServiceNew()
		{
			var row = new ServiceProviderService()
			{
				RowId = Guid.NewGuid(),
				ServiceProviderRowId = Entity.RowId,
			};
			AlterMedicalServiceEntity(row);
			MedicalServiceEntities.Add(row);
			MedicalServiceSelectedEntity = row;
		}
		public void MedicalServiceDelete()
		{
			var row = MedicalServiceSelectedEntity;
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "row"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				MedicalServiceEntities.Remove(row);
				Entity.IsChanged = true;
			}
		}
		public bool CanMedicalServiceDelete()
		{
			return (MedicalServiceSelectedEntity != null);
		}



		public void SchedulerRecordEdit()
		{
			DispatcherUIHelper.Run(() =>
			{
				if (IsNew)
				{
					MessageBoxService.ShowMessage("You must save row before editing scheduler", CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
					return;
				}

				var param = new SchedulerRecordViewModel.OpenParams
				{
					ServiceProviderRowId = Entity.RowId,
				};
				OpenWindowInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.SchedulerRecordView,
					Param = param,
				});

			});
		}

		public bool IsShowLoginHistoryView => !IsNew && UserManager.UserRowId == Entity?.RowId;
		public void LoginHistoryView()
		{
			DispatcherUIHelper.Run(() =>
			{
				var param = new LoginHistoryViewModel.OpenParams
				{
					ServiceProviderRowId = Entity.RowId,
				};
				OpenWindowInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.LoginHistoryView,
					Param = param,
				});
			});
		}


		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));



		bool Validate()
		{
			List<string> errors = new List<string>();

			ValidateHelper.Empty(Entity.FirstName, "First Name", errors);
			ValidateHelper.Empty(Entity.LastName, "Last Name", errors);
			ValidateHelper.Empty(Entity.DoctorRate, "Rate", errors);
			ValidateHelper.Empty(Entity.Position, "Position", errors);
			ValidateHelper.Empty(Entity.MaximumDayAppointments, "Maximum appointments per day", errors);
			ValidateHelper.Empty(Entity.AppointmentBackgroundColor, "Background", errors);
			ValidateHelper.Empty(Entity.AppointmentForegroundColor, "Foreground", errors);
			if (!string.IsNullOrEmpty(Entity.Username))
			{
				ValidateHelper.Empty(Entity.Password, "Password", errors);
			}

			ValidateHelper.EmptyEnumerable(this, ProfessionalAssociationEntities, 
				(q) => q.AssociationRowId, 
				"Professional Association", 
				() => ProfessionalAssociationSelectedEntity, errors);

			ValidateHelper.EmptyEnumerable(this, MedicalServiceEntities,
				(q) => q.MedicalServiceOrSupplyRowId,
				"Service", 
				() => MedicalServiceSelectedEntity, errors);
			ValidateHelper.EmptyEnumerable(this, MedicalServiceEntities,
				(q) => q.ChargeModel,
				"Charge Model",
				() => MedicalServiceSelectedEntity, errors);
			ValidateHelper.ValidateDuplicate(this, MedicalServiceEntities,
				(q) => q.MedicalServiceOrSupplyRowId, 
				(q) => "Service \"" + LookupDataProvider.MedicalService2Name(q.MedicalServiceOrSupplyRowId) + "\" " + ValidateHelper.IS_DUPLICATE, 
				() => MedicalServiceSelectedEntity, errors, ignoreEmptyValues: true);

			if ( (Entity.Password ?? "") != (Entity.PasswordConfirm ?? "") )
			{
				errors.Add("Password and confirmation is not matched");
			}

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

			//updateEntity
			var updateEntity = Entity.GetPocoClone();
			updateEntity.ServiceProviderAssociations = ProfessionalAssociationEntities.Select(q => q.GetPocoClone()).ToObservableCollection();
			updateEntity.ServiceProviderServices = MedicalServiceEntities.Select(q => q.GetPocoClone()).ToObservableCollection();

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = IsNew ?
				await businessService.PostServiceProvider(updateEntity) :
				await businessService.PutServiceProvider(updateEntity);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService))
			{
				return false;
			}

			UpdateListViewColumns(updateEntity);
			MessengerHelper.SendMsgRowChange(updateEntity, IsNew);
			IsNew = false;
			ResetHasChange();

			//close
			if (andClose)
			{
				CloseCore(force:true);
			}

			return true;
		}

		void UpdateListViewColumns(ServiceProvider entity)
		{
			var professionalAssociationEntity = ProfessionalAssociationEntities.FirstOrDefault(q => q.IsPrimary);
			if (professionalAssociationEntity == null)
			{
				professionalAssociationEntity = new ServiceProviderAssociation();
			}
			var professionalAssociation = LookupDataProvider.FindProfessionalAssociation(professionalAssociationEntity.AssociationRowId);

			entity.AssociationRowId = professionalAssociation?.RowId;
			entity.AssociationName = professionalAssociation?.Name;
			entity.AssociationCode = professionalAssociation?.Code;
			entity.RegistrationNumber = professionalAssociationEntity?.RegistrationNumber;
			entity.RegistrationDate = professionalAssociationEntity?.RegistrationDate;
			entity.RegistrationExpiryDate = professionalAssociationEntity?.RegistrationExpiryDate;
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
			return (IsNew || Entity.IsChanged || ProfessionalAssociationEntities.Any(q => q.IsChanged) || MedicalServiceEntities.Any(q => q.IsChanged));
		}

		void ResetHasChange()
		{
			Entity.IsChanged = false;
			ProfessionalAssociationEntities.ForEach(q => q.IsChanged = false);
			MedicalServiceEntities.ForEach(q => q.IsChanged = false);
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





		public void AddRowFromPopup(string column)
		{
			if (column == nameof(Entity.AppointmentBookRowId))
			{
				OpenWindowInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneAppointmentBookView,
					Param = "IsNew" + "&NewName=" + Entity.FullName + " (AB)",
				});
			}
		}






		public void PreviewKeyDown(KeyEventArgs e)
		{
			if (e.Key == Key.R && KeyboardHelper.IsShiftPressed && KeyboardHelper.IsControlPressed && KeyboardHelper.IsAltPressed)
			{
				IsVisibilityRate = !IsVisibilityRate;
			}
		}

















		public class OpenParams
		{
			public bool IsNew { get; set; }
			public Guid RowId { get; set; }
			public Boolean IsRestrictedAccess { get; set; }
		}


	}

}
