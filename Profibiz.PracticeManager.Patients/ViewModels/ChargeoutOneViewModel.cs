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
using Profibiz.PracticeManager.SharedCode;
using System.Windows.Input;
using DevExpress.Xpf.Utils;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class ChargeoutOneViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; }
		public GridControlBehaviorManager BehaviorGridConrolChargeoutItem { get; set; } = new GridControlBehaviorManager();
		public GridControlBehaviorManager BehaviorGridConrolChargeoutPaycharge { get; set; } = new GridControlBehaviorManager();
		#endregion
		public virtual Chargeout Entity { get; set; }
		public virtual Boolean IsNew { get; set; }
		public virtual Guid? RowId { get; set; }
		public virtual ObservableCollection<ChargeoutItem> ChargeoutItemEntities { get; set; }
		public virtual ChargeoutItem ChargeoutItemSelectedEntity { get; set; }
		public virtual ObservableCollection<ChargeoutPaycharge> ChargeoutPaychargeEntities { get; set; }
		public virtual ChargeoutPaycharge ChargeoutPaychargeSelectedEntity { get; set; }
		public virtual ObservableCollection<ChargeoutRefcharge> ChargeoutRefchargeEntities { get; set; }
		public virtual ChargeoutRefcharge ChargeoutRefchargeSelectedEntity { get; set; }
		public virtual Boolean ReadOnly { get; set; }
		public virtual Boolean NotReadOnly => !ReadOnly;
		public virtual Boolean FlagAlwaysIsNewHasChanges { get; set; } = true;
		public virtual Boolean IsShowColorRowBackground { get; set; }
		public virtual Int32 ColorRowBackground => (!IsShowColorRowBackground ? 0 : IsNew ? 1 : 2);
		public virtual Boolean IsShowCommandPanel => IsWindowMode;
		public virtual Boolean ShowThirdPartyServiceProvider => (Entity?.ChargeoutType == TypeHelper.ChargeoutType.ThirdParty);
		public virtual Boolean ShowChargeoutItemDate => (Entity?.ChargeoutType != TypeHelper.ChargeoutType.Supply);
		public virtual Boolean ShowChargeoutItemDescription => (Entity?.ChargeoutType != TypeHelper.ChargeoutType.ThirdParty);
		public virtual Boolean ShowChargeoutItemServcieOrSupplyRowId => (Entity?.ChargeoutType == TypeHelper.ChargeoutType.ThirdParty);
		public virtual Boolean ShowOpenDetailColumn => (Entity?.ChargeoutType == TypeHelper.ChargeoutType.Appointment);
		public virtual ObservableCollection<Template> ChargeoutTemplates => ChargeoutTemplateInfo.GetForChargeoutType(Entity?.ChargeoutType);
		public virtual Boolean IsCollapsedLayoutPaycharges { get; set; } = true;
		public virtual Boolean IsCollapsedLayoutRefcharges { get; set; } = true;
		public virtual Boolean IsVisibilityUserControl { get; set; } = false;
		public virtual bool IsHideLayoutCoordinations => (Entity?.HasNoCoverage == true);



		public virtual Boolean IsWindowMode { get; set; }
		public virtual ViewModes ViewMode { get; set; }
		public enum ViewModes { Main, ChargeoutsBuilder }
		public virtual Boolean IsChargeoutsBuilder => (ViewMode == ViewModes.ChargeoutsBuilder);


		public ChargeoutOneViewModel() : base()
		{
			MessengerHelper.RunAction(this, () =>
			{
				//MessengerHelper.Register<MsgRowChange<Patient>>(this, OnMsgRowChange);
			});
		}

		public static ChargeoutOneViewModel Create(bool isWindowMode)
		{
			var ret = ViewModelSource.Create<ChargeoutOneViewModel>();
			ret.IsWindowMode = isWindowMode;
			return ret;
		}

		async public Task LoadData(bool isNew = false, Guid? rowId = null, Chargeout newChargeout = null, Boolean readOnly = false, Guid? selectPaychargeRowId = null, Boolean flagAlwaysIsNewHasChanges = true, InteractionRequest<CloseDXWindowsActionParam> closeInteractionRequest = null)
		{
			if (!isNew)
			{
				ShowWaitIndicator.Show();
			}

			CloseInteractionRequest = closeInteractionRequest;

			if (newChargeout != null)
			{
				isNew = true;
			}
			IsNew = isNew;
			RowId = rowId;
			ReadOnly = readOnly;
			FlagAlwaysIsNewHasChanges = flagAlwaysIsNewHasChanges;

			Chargeout entity;
			if (!IsNew)
			{
				entity = await businessService.GetChargeout(RowId);
			}
			else
			{
				entity = newChargeout;
			}
			Entity = entity;

			ChargeoutItemEntities = new ObservableCollection<ChargeoutItem>(Entity.ChargeoutItems.OrderBy(q => q.ItemDate));
			BuildLastChargeoutItemEntities();
			ChargeoutItemEntities.ForEach(q => SubscribeChargeoutItemRow(q));

			ChargeoutPaychargeEntities = new ObservableCollection<ChargeoutPaycharge>(Entity.ChargeoutPaycharges);
			ChargeoutPaychargeEntities.ForEach(q => SubscribeChargeoutPaychargeRow(q));

			ChargeoutRefchargeEntities = new ObservableCollection<ChargeoutRefcharge>(Entity.ChargeoutRefcharges);
			ChargeoutRefchargeEntities.ForEach(q => SubscribeChargeoutRefchargeRow(q));

			CalcChargeoutFields();
			SetupReadOnly();
			IsCollapsedLayoutPaycharges = !ChargeoutPaychargeEntities.Any();
			IsCollapsedLayoutRefcharges = !ChargeoutRefchargeEntities.Any();

			var prow = ChargeoutPaychargeEntities.FirstOrDefault(q => q.PaychargeRowId == selectPaychargeRowId);
			if (prow != null)
			{
				ChargeoutPaychargeSelectedEntity = prow;
				BehaviorGridConrolChargeoutPaycharge.Focus();
			}

			ResetHasChange();
			ShowWaitIndicator.Hide();
			IsVisibilityUserControl = true;
		}


		void SetupReadOnly()
		{
			//if (ChargeoutPaychargeEntities.Count > 0)
			//{
			//	ReadOnly = true;
			//}
		}


		#region Region ChargeoutItem_operations

		public void ChargeoutItemNew()
		{
			DispatcherUIHelper.Run(async () =>
			{
				await ChargeoutItemNewAppointment();
			});
		}

		async Task ChargeoutItemNewAppointment()
		{
			var excludeInvoiceItems = ChargeoutItemEntities.Select(q => q.InvoiceItemRowId);
			var ret2 = await PickPaychargeAppointmentsViewModel.PickRow(
									ShowDXWindowsInteractionRequest,
									PickPaychargeAppointmentsViewModel.PickModeEnum.Main,
									chargeoutRecipientRowId: Entity.ChargeoutRecipientRowId,
									completed: true,
									forChargeout: true,
									excludeInvoiceItems: excludeInvoiceItems,
									multipleSelectionMode: true);
			if (!ret2.IsSuccess) return;

			var appointments = ret2.PickRows;
			foreach (var appointment in appointments)
			{
				var chargeoutItem = ChargeoutItem.CreateFromAppointment(appointment, appointment.InvoiceItem);
				chargeoutItem.ChargeoutRowId = Entity.RowId;
				ChargeoutItemEntities.Add(chargeoutItem);
				SubscribeChargeoutItemRow(chargeoutItem);
			}
			CalcChargeoutFields();


		}



		public void ChargeoutItemDelete()
		{
			var row = ChargeoutItemSelectedEntity;
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "row"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				ChargeoutItemEntities.Remove(row);
				Entity.IsChanged = true;
				CalcChargeoutFields();
			}
		}
		public bool CanChargeoutItemDelete() => (ChargeoutItemSelectedEntity != null);

		#endregion

		
		
		#region Region ChargeoutPaycharge_operations

		public void ChargeoutPaychargeNew()
		{
			//if (ChargeoutType == Chargeout.ChargeoutTypeEnum.Supply)
			{
				DispatcherUIHelper.Run(async () =>
				{
					var ret2 = await PickPaychargeViewModel.PickRow(
						ShowDXWindowsInteractionRequest,
						PickPaychargeViewModel.PickModeEnum.Main,
						сhargeoutRecipientRowId: Entity.ChargeoutRecipientRowId,
						excludePaycharges: ChargeoutPaychargeEntities.Select(q => q.PaychargeRowId).ToArray(),
						hasNoDistributedAmount: true);
					if (!ret2.IsSuccess) return;

					var paycharge = ret2.PickRow;//.ToPaycharge();
					var row = new ChargeoutPaycharge
					{
						RowId = Guid.NewGuid(),
						ChargeoutRowId = Entity.RowId,
						PaychargeRowId = paycharge.RowId,
						Amount = paycharge.Amount,
						Paycharge = paycharge,
						IsChanged = true,
						IsNew = true,
					};
					//row.Description = GetDescription(row);
					ChargeoutPaychargeEntities.Add(row);
					ChargeoutPaychargeSelectedEntity = row;
					SubscribeChargeoutPaychargeRow(row);
					CalcChargeoutFields();

					DispatcherUIHelper.Run(() =>
					{
						BehaviorGridConrolChargeoutPaycharge.SetCurrentColumn("Amount");
						BehaviorGridConrolChargeoutPaycharge.ShowEditor(true);
					});
				});
			}
		}
		public void ChargeoutPaychargeDelete()
		{
			var row = ChargeoutPaychargeSelectedEntity;
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "row"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				ChargeoutPaychargeEntities.Remove(row);
				Entity.IsChanged = true;
				CalcChargeoutFields();
			}
		}
		public bool CanChargeoutPaychargeDelete() => (ChargeoutPaychargeSelectedEntity != null);

		#endregion





		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));



		bool Validate()
		{
			List<string> errors = new List<string>();

			ValidateHelper.Empty(Entity.ChargeoutDate, "Chargeout Date", errors);
			//ValidateHelper.Empty(Entity.PrintTemplate, "Template", errors);

			if (ChargeoutItemEntities.Count == 0)
			{
				errors.Add("Chargeout Items list cannot be empty");
			}

			if (ShowThirdPartyServiceProvider)
			{
				ValidateHelper.Empty(Entity.ServiceProviderRowId, "Provider", errors);
			}

			ValidateHelper.PositiveEnumerable(this, ChargeoutItemEntities, (q) => q.Units, "Units", () => ChargeoutItemSelectedEntity, errors);
			ValidateHelper.PositiveEnumerable(this, ChargeoutItemEntities, (q) => q.Price, "Unit Price", () => ChargeoutItemSelectedEntity, errors);
			ValidateHelper.EmptyEnumerable(this, ChargeoutItemEntities, (q) => q.Tax, "Unit Tax", () => ChargeoutItemSelectedEntity, errors);


			ValidateHelper.EmptyEnumerable(this, ChargeoutPaychargeEntities, (q) => q.Amount, "Amount", () => ChargeoutPaychargeSelectedEntity, errors);

			if (errors.Count > 0)
			{
				var err = string.Join("\n", errors.ToArray());
				MessageBoxService.ShowMessage(err, CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
				return false;
			}

			return true;
		}

		public async Task<bool> SaveCore(bool andClose)
		{
			//validate
			if (!Validate())
			{
				return false;
			}

			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);

			var chargeoutRecipient = Entity.ChargeoutRecipient;

			//calculate fields
			Entity.BillTo = chargeoutRecipient.ContactName;
			Entity.BillToAddress1 = chargeoutRecipient.AddressLine;
			Entity.BillToCity = chargeoutRecipient.City;
			Entity.BillToProvince = chargeoutRecipient.Province;
			Entity.BillToPostCode = chargeoutRecipient.Postcode;


			//updateEntity
			var updateEntity = Entity.GetPocoClone();
			updateEntity.ChargeoutItems = ChargeoutItemEntities.Select(q => q.GetPocoClone()).ToList();
			updateEntity.ChargeoutPaycharges = ChargeoutPaychargeEntities.Select(q => q.GetPocoClone()).ToList();

			//save
			var uret = IsNew ?
				await businessService.PostChargeout(updateEntity) :
				await businessService.PutChargeout(updateEntity);
			//await Task.Delay(3000);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService))
			{
				return false;
			}

			if (IsNew)
			{
				var serverReturn = JsonConvert.DeserializeObject<ServerReturnUpdateChargeout>(uret.ResponseJson);
				var chargeoutNumber = serverReturn.ChargeoutNumber;
				updateEntity.ChargeoutNumber = chargeoutNumber;
				Entity.ChargeoutNumber = chargeoutNumber;
			}

			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			MessengerHelper.SendMsgRowChange(updateEntity, IsNew);
			ShowWaitIndicator.Hide();
			IsNew = false;
			ResetHasChange();

			//close
			if (andClose)
			{
				CloseCore(force: true);
			}

			return true;
		}










		bool forceClose;
		void CloseCore(bool force = false)
		{
			this.forceClose = force;
			CloseInteractionRequest?.Raise(null);
		}

		bool HasChange()
		{
			if (Entity == null || ChargeoutItemEntities == null)
			{
				return false;
			}

			if (Entity.IsChanged || ChargeoutItemEntities.Any(q => q.IsChanged))
			{
				System.Diagnostics.Debug.WriteLine("OneModel.HasChange()=" + true);
				return true;
			}
			if (IsNew && FlagAlwaysIsNewHasChanges)
			{
				System.Diagnostics.Debug.WriteLine("OneModel.HasChange()=" + true);
				return true;
			}
			System.Diagnostics.Debug.WriteLine("OneModel.HasChange()=" + false);
			return false;
		}

		void ResetHasChange()
		{
			Entity.IsChanged = false;
			ChargeoutItemEntities.ForEach(q => q.IsChanged = false);
		}


		public async Task<OnCloseReturn> OnClose(bool showOKCancel = false)
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
					return OnCloseReturn.Cancel;
				}
				else if (ret == MessageResult.No)
				{
					return OnCloseReturn.No;
				}
				else if (ret == MessageResult.Yes || ret == MessageResult.OK)
				{
					return await SaveCore(andClose: false) ? OnCloseReturn.Yes : OnCloseReturn.Cancel;
				}
				else throw new ArgumentException();
			}
			else
			{
				return OnCloseReturn.Yes;
			}
		}

		public void Close() => CloseCore();


		public async Task ClosingEvent(CancelEventArgs arg)
		{
			if (forceClose)
			{
				return;
			}
			if (await OnClose() == OnCloseReturn.Cancel)
			{
				arg.Cancel = true;
			}
		}

		//Guid lastNewActionBookmark;
		//public void NewOpenPatient(bool isnew)
		//{
		//	lastNewActionBookmark = (isnew ? Guid.NewGuid() : default(Guid));
		//	ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
		//	{
		//		ViewCode = ViewCodes.OnePatientView,
		//		Param = new OnePatientViewModel.OpenParams
		//		{
		//			IsNew = isnew,
		//			RowId = (isnew ? default(Guid) : Entity.PatientRowId),
		//			NewActionBookmark = lastNewActionBookmark,
		//		},
		//	});
		//}
		//public void OpenPatient() => NewOpenPatient(false);
		//public bool CanOpenPatient() => (!GuidHelper.IsNullOrEmpty(Entity?.PatientRowId));

		//void OnMsgRowChange(MsgRowChange<Patient> msg)
		//{
		//	if (msg.RowAction == RowAction.Update && Entity.PatientRowId == msg.Row.RowId)
		//	{
		//		Entity.Patient = msg.Row;
		//	}
		//	else if (msg.RowAction == RowAction.Insert && msg.Options == "NewActionBookmark=" + lastNewActionBookmark)
		//	{
		//		Entity.PatientRowId = msg.Row.RowId;
		//		Entity.Patient = msg.Row;
		//	}
		//}



		public void SubscribeChargeoutItemRow(ChargeoutItem row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				CalcChargeoutFields();
			};

			row.OnAddRowFromPopup = (column) =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneMedicalServiceView,
					Param = "IsNew;ItemType=" + TypeHelper.MedicalItemType.ThirdPartyService,
				});
			};

			row.OnOpenDetail = () =>
			{
				var param = new OneAppointmentViewModel.OpenParams
				{
					IsNew = false,
					RowId = (Guid)row.InvoiceItem.AppointmentRowId,
					IsReadOnly = true,
				};
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneAppointmentView,
					Param = param,
				});
			};
		}

		void SubscribeChargeoutPaychargeRow(ChargeoutPaycharge row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				CalcChargeoutFields();
			};
			row.OnOpenDetail = () =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.PaychargeWindowView,
					Param = new PaychargeWindowViewModel.OpenParams { IsNew = false, RowId = row.PaychargeRowId, SelectChargeoutRowId = row.ChargeoutRowId },
				});
			};
		}

		void SubscribeChargeoutRefchargeRow(ChargeoutRefcharge row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				CalcChargeoutFields();
			};
			row.OnOpenDetail = () =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.RefchargeWindowView,
					Param = new RefchargeWindowViewModel.OpenParams { IsNew = false, RowId = row.RefchargeRowId, SelectChargeoutRowId = row.ChargeoutRowId },
				});
			};
		}


		List<ChargeoutItem> lastChargeoutItemEntities;
		void BuildLastChargeoutItemEntities()
		{
			lastChargeoutItemEntities = ChargeoutItemEntities.Select(q => q.Clone()).ToList();
		}

		public void CalcChargeoutFields()
		{
			Chargeout.CalcTotalFields(Entity, ChargeoutItemEntities);
			CalcChargeoutDate();

			Entity.PaychargeTotal = ChargeoutPaychargeEntities.Sum(q => q.Amount);
			Entity.PaychargeRequest = Entity.Total - Entity.PaychargeTotal;

			BehaviorGridConrolChargeoutItem.UpdateTotalSummary();
			BehaviorGridConrolChargeoutPaycharge.UpdateTotalSummary();

			BuildLastChargeoutItemEntities();
		}


		void CalcChargeoutDate()
		{
			if (Entity.ChargeoutType != TypeHelper.ChargeoutType.Appointment)
			{
				return;
			}

			var zzz = (from a in ChargeoutItemEntities join b in lastChargeoutItemEntities on a.RowId equals b.RowId select new { a, b }).ToArray();

			var count =
				(from a in ChargeoutItemEntities join b in lastChargeoutItemEntities on a.RowId equals b.RowId select new { a, b })
				.Count(q => q.a.ItemDate == q.b.ItemDate);
			if (ChargeoutItemEntities.Count == count && lastChargeoutItemEntities.Count == count)
			{
				return; //даты не менялись
			}

			Entity.ChargeoutDate = ChargeoutItemEntities.Where(q => q.ItemDate != null).Max(q => q.ItemDate);
		}




		public void AddRowFromPopup(string column)
		{
			if (column == nameof(Entity.Status1RowId))
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneChargeoutStatusView,
					Param = "IsNew",
				});
			}

			else if (column == nameof(Entity.ThirdPartyServiceProviderRowId))
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneThirdPartyServiceProviderView,
					Param = "IsNew",
				});
			}
		}





		public void PrintChargeout()
		{
			DispatcherUIHelper.Run(async () =>
			{
				if (string.IsNullOrEmpty(Entity.PrintTemplate))
				{
					MessageBoxService.ShowError("Select Print Template before print chargeout");
					return;
				}

				var ret = await OnClose(true);
				if (ret == OnCloseReturn.Yes)
				{
					ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
					{
						ViewCode = ViewCodes.ChargeoutPrintView,
						Param = new ChargeoutPrintViewModel.OpenParams { RowId = Entity.RowId },
					});
				}
			});
		}

	}
}
