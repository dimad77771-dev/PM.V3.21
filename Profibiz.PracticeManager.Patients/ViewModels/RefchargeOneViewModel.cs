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
using System.Windows;
using System.Windows.Media;
using Profibiz.PracticeManager.Patients.BusinessService;
using System.Windows.Media.Animation;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class RefchargeOneViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public GridControlBehaviorManager BehaviorGridConrolChargeoutItem { get; set; } = new GridControlBehaviorManager();
		public GridControlBehaviorManager BehaviorGridConrolPaychargeItem { get; set; } = new GridControlBehaviorManager();
		#endregion
		public Refcharge Entity { get; set; }
		public bool IsNew { get; set; }
		public Guid? RowId { get; set; }
		public String RefchargeItemsType => Entity?.RefchargeItemsType;
		public ObservableCollection<ChargeoutRefcharge> ChargeoutRefchargeEntities { get; set; }
		public ChargeoutRefcharge ChargeoutRefchargeSelectedEntity { get; set; }
		public ObservableCollection<PaychargeRefcharge> PaychargeRefchargeEntities { get; set; }
		public PaychargeRefcharge PaychargeRefchargeSelectedEntity { get; set; }
		public Boolean ReadOnly { get; set; }
		public virtual Boolean IsWindowMode { get; set; }
		public virtual Boolean IsShowCommandPanel => IsWindowMode;

		public virtual Boolean IsTypeChargeout => RefchargeItemsType == TypeHelper.RefchargeItemsType_Chargeout;
		public virtual Boolean IsTypePaycharge => RefchargeItemsType == TypeHelper.RefchargeItemsType_Paycharge;
		public virtual Boolean IsVisibleGridChargeout => IsTypeChargeout;
		public virtual Boolean IsVisibleGridControlChargeout => IsTypeChargeout && !ReadOnly;
		public virtual Boolean IsVisibleGridPaycharge => IsTypePaycharge;
		public virtual Boolean IsVisibleGridControlPaycharge => IsTypePaycharge && !ReadOnly;
		public virtual String HeightGridChargeout => IsTypeChargeout ? "*" : "0";
		public virtual String HeightGridPaycharge => IsTypePaycharge ? "*" : "0";




		public RefchargeOneViewModel() : base()
		{
			//MessengerHelper.Register<MsgRowChange<Patient>>(this, OnMsgRowChange);
		}
		public static RefchargeOneViewModel Create(bool isWindowMode)
		{
			var ret = ViewModelSource.Create<RefchargeOneViewModel>();
			ret.IsWindowMode = isWindowMode;
			return ret;
		}



		async public Task LoadData(bool isNew = false, Guid? rowId = null, Refcharge newRefcharge = null, List<ChargeoutRefcharge> newChargeoutRefcharges = null, List<PaychargeRefcharge> newPaychargeRefcharges = null, Boolean readOnly = false, Guid? selectChargeoutRowId = null, Guid? selectPaychargeRowId = null, InteractionRequest<CloseDXWindowsActionParam> closeInteractionRequest = null)
		{
			if (newRefcharge != null)
			{
				isNew = true;
			}
			IsNew = isNew;
			RowId = rowId;
			ReadOnly = readOnly;
			CloseInteractionRequest = closeInteractionRequest;
			ShowWaitIndicator.Show();

			Refcharge entity;
			if (!IsNew)
			{
				entity = await businessService.GetRefcharge(RowId);
				entity.PaychargeRefcharges.ForEach(q => q.OnAfterLoad());
				entity.ChargeoutRefcharges.ForEach(q => q.OnAfterLoad());
			}
			else
			{
				entity = newRefcharge;
			}
			Entity = entity;
			if (newChargeoutRefcharges != null)
			{
				Entity.ChargeoutRefcharges = newChargeoutRefcharges;
			}
			if (newPaychargeRefcharges != null)
			{
				Entity.PaychargeRefcharges = newPaychargeRefcharges;
			}

			ChargeoutRefchargeEntities = new ObservableCollection<ChargeoutRefcharge>(Entity.ChargeoutRefcharges);
			ChargeoutRefchargeEntities.ForEach(q => SubscribeChargeoutRefchargeRow(q));
			PaychargeRefchargeEntities = new ObservableCollection<PaychargeRefcharge>(Entity.PaychargeRefcharges);
			PaychargeRefchargeEntities.ForEach(q => SubscribePaychargeRefchargeRow(q));
			CalcRefchargeFields();

			var srow1 = ChargeoutRefchargeEntities.FirstOrDefault(q => q.ChargeoutRowId == selectChargeoutRowId);
			if (srow1 != null)
			{
				ChargeoutRefchargeSelectedEntity = srow1;
				BehaviorGridConrolChargeoutItem.Focus();
			}

			var srow2 = PaychargeRefchargeEntities.FirstOrDefault(q => q.PaychargeRowId == selectPaychargeRowId);
			if (srow2 != null)
			{
				PaychargeRefchargeSelectedEntity = srow2;
				BehaviorGridConrolPaychargeItem.Focus();
			}


			ResetHasChange();

			ShowWaitIndicator.Hide();
		}





		public void ChargeoutRefchargeNew()
		{
			{
				DispatcherUIHelper.Run(async () =>
				{
					var parms = new PickChargeoutViewModel.OpenParams
					{
						ShowDXWindowsInteractionRequest = ShowDXWindowsInteractionRequest,
						MessageBoxService = MessageBoxService,
						ShowWaitIndicator = ShowWaitIndicator,
						PickMode = PickChargeoutViewModel.PickModeEnum.Main,
						ChargeoutRecipientRowId = Entity.ChargeoutRecipientRowId,
						ExcludeChargeouts = ChargeoutRefchargeEntities.Select(q => q.ChargeoutRowId).ToArray(),
						NegativeBalanceOnly = true,
						ShowMessageIfNotExists = "There are no chargeouts that have paycharges that can be refchargeed",
					};
					var ret2 = await PickChargeoutViewModel.PickRow(parms);
					if (!ret2.IsSuccess) return;

					ChargeoutRefcharge firstAddedRow = null;
					var chargeouts = ret2.PickRows;
					foreach (var chargeout in chargeouts)
					{
						var row = new ChargeoutRefcharge
						{
							RowId = Guid.NewGuid(),
							ChargeoutRowId = chargeout.RowId,
							RefchargeRowId = Entity.RowId,
							Amount = -(chargeout.Balance ?? 0),
							AllocationDate = DateTime.Today,
							Chargeout = chargeout,
							IsChanged = true,
							IsNew = true,
						};
						//row.OnAfterLoad();
						ChargeoutRefchargeEntities.Add(row);
						SubscribeChargeoutRefchargeRow(row);
						if (firstAddedRow == null)
						{
							firstAddedRow = row;
						}
					}
					CalcRefchargeFields();

					if (firstAddedRow != null)
					{
						ChargeoutRefchargeSelectedEntity = firstAddedRow;
						DispatcherUIHelper.Run(() =>
						{
							BehaviorGridConrolChargeoutItem.SetCurrentColumn("Amount");
							BehaviorGridConrolChargeoutItem.ShowEditor(true);
						});
					}
				});
			}
		}

		public void ChargeoutRefchargeDelete()
		{
			var row = ChargeoutRefchargeSelectedEntity;
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "row"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				ChargeoutRefchargeEntities.Remove(row);
				Entity.IsChanged = true;
				CalcRefchargeFields();
			}
		}
		public bool CanChargeoutRefchargeDelete() => (ChargeoutRefchargeSelectedEntity != null);




		public void PaychargeRefchargeNew()
		{
			{
				DispatcherUIHelper.Run(async () =>
				{
					//var parms = new PickPaychargeViewModel.OpenParams
					//{
					//	//!!ShowDXWindowsInteractionRequest = ShowDXWindowsInteractionRequest,
					//	//!!MessageBoxService = MessageBoxService,
					//	//!!ShowWaitIndicator = ShowWaitIndicator,
					//	PickMode = PickPaychargeViewModel.PickModeEnum.Main,
					//	PatientRowId = Entity.PatientRowId,
					//	//!!UseFamilyHead = true,
					//	ExcludePaycharges = PaychargeRefchargeEntities.Select(q => q.PaychargeRowId).ToArray(),
					//	//!!NegativeBalanceOnly = true,
					//	//!!ShowMessageIfNotExists = "There are no paycharges that have paycharges that can be refchargeed",
					//};
					//var ret2 = await PickPaychargeViewModel.PickRow(parms);
					var ret2 = await PickPaychargeViewModel.PickRow(
						showDXWindowsInteractionRequest: ShowDXWindowsInteractionRequest,
						pickMode: PickPaychargeViewModel.PickModeEnum.Main,
						сhargeoutRecipientRowId: Entity.ChargeoutRecipientRowId,
						excludePaycharges: PaychargeRefchargeEntities.Select(q => q.PaychargeRowId).ToArray(),
						hasNoDistributedAmount: true,
						isMultiSelect: true);
					if (!ret2.IsSuccess) return;

					PaychargeRefcharge firstAddedRow = null;
					var paycharges = ret2.PickRows;
					foreach (var paycharge in paycharges)
					{
						var row = new PaychargeRefcharge
						{
							RowId = Guid.NewGuid(),
							PaychargeRowId = paycharge.RowId,
							RefchargeRowId = Entity.RowId,
							Amount = paycharge.PaychargeBalance,
							Amount0 = 0,
							AllocationDate = DateTime.Today,
							Paycharge = paycharge,
							IsChanged = true,
							IsNew = true,
						};
						PaychargeRefchargeEntities.Add(row);
						SubscribePaychargeRefchargeRow(row);
						if (firstAddedRow == null)
						{
							firstAddedRow = row;
						}
					}
					CalcRefchargeFields();

					if (firstAddedRow != null)
					{
						PaychargeRefchargeSelectedEntity = firstAddedRow;
						DispatcherUIHelper.Run(() =>
						{
							BehaviorGridConrolPaychargeItem.SetCurrentColumn("Amount");
							BehaviorGridConrolPaychargeItem.ShowEditor(true);
						});
					}
				});
			}
		}

		public void PaychargeRefchargeDelete()
		{
			var row = PaychargeRefchargeSelectedEntity;
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "row"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				PaychargeRefchargeEntities.Remove(row);
				Entity.IsChanged = true;
				CalcRefchargeFields();
			}
		}
		public bool CanPaychargeRefchargeDelete() => (PaychargeRefchargeSelectedEntity != null);




		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));


		bool Validate()
		{
			List<string> errors = new List<string>();

			ValidateHelper.Empty(Entity.PaychargeDate, "Paycharge Date", errors);
			ValidateHelper.Positive(Entity.Amount, "Amount", errors);
			//if (Entity.PaychargeBalance < 0)
			//         {
			//             errors.Add("Total Allocated Amount can't be more then Refcharge Amount");
			//         }

			ValidateHelper.PositiveEnumerable(this, ChargeoutRefchargeEntities, (q) => q.Amount, "Allocated Amount", () => ChargeoutRefchargeSelectedEntity, errors);
			//!!!ValidateHelper.ValidateEnumerable(this, ChargeoutRefchargeEntities, (q) => q.NewBalanceDue >= 0, "Allocated Amount can't be more then Balance Due", () => ChargeoutRefchargeSelectedEntity, errors);
			ValidateHelper.PositiveEnumerable(this, PaychargeRefchargeEntities, (q) => q.Amount, "Allocated Amount", () => PaychargeRefchargeSelectedEntity, errors);

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
			NLog.vv(() => andClose);
			if (!Validate())
			{
				return false;
			}

			//updateEntity
			var updateEntity = Entity.GetPocoClone();
			updateEntity.ChargeoutRefcharges = ChargeoutRefchargeEntities.Select(q => q.GetPocoClone()).ToList();
			updateEntity.PaychargeRefcharges = PaychargeRefchargeEntities.Select(q => q.GetPocoClone()).ToList();

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = IsNew ?
				await businessService.PostRefcharge(updateEntity) :
				await businessService.PutRefcharge(updateEntity);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService))
			{
				return false;
			}

			MessengerHelper.SendMsgRowChange(updateEntity, IsNew);
			IsNew = false;
			ResetHasChange();
			ChargeoutRefchargeEntities.ForEach(q => q.IsNew = false);

			//close
			if (andClose)
			{
				CloseCore(force: true);
			}

			return true;
		}
		public void Close() => CloseCore();




		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; }
		bool forceClose;
		void CloseCore(bool force = false)
		{
			this.forceClose = force;
			CloseInteractionRequest?.Raise(null);
		}

		bool HasChange()
		{
			if (Entity == null || ChargeoutRefchargeEntities == null || PaychargeRefchargeEntities == null)
			{
				return false;
			}

			return (IsNew || Entity.IsChanged || ChargeoutRefchargeEntities.Any(q => q.IsChanged) || PaychargeRefchargeEntities.Any(q => q.IsChanged));
		}

		void ResetHasChange()
		{
			Entity.IsChanged = false;
			ChargeoutRefchargeEntities.ForEach(q => q.IsChanged = false);
			PaychargeRefchargeEntities.ForEach(q => q.IsChanged = false);
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
		//	MessengerHelper.RunAction(this, () =>
		//	{
		//		if (msg.RowAction == RowAction.Update && Entity.PatientRowId == msg.Row.RowId)
		//		{
		//			Entity.Patient = msg.Row;
		//		}
		//		else if (msg.RowAction == RowAction.Insert && msg.Options == "NewActionBookmark=" + lastNewActionBookmark)
		//		{
		//			Entity.PatientRowId = msg.Row.RowId;
		//			Entity.Patient = msg.Row;
		//		}
		//	});
		//}



		void SubscribeChargeoutRefchargeRow(ChargeoutRefcharge row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				CalcRefchargeFields();
			};
			row.OnOpenDetail = () =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.ChargeoutWindowView,
					Param = new ChargeoutWindowViewModel.OpenParams
					{
						IsNew = false,
						RowId = row.ChargeoutRowId,
					},
				});
			};
		}

		void SubscribePaychargeRefchargeRow(PaychargeRefcharge row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				CalcRefchargeFields();
			};
			row.OnOpenDetail = () =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.PaychargeWindowView,
					Param = new PaychargeWindowViewModel.OpenParams
					{
						IsNew = false,
						RowId = row.PaychargeRowId,
					},
				});
			};
		}

		void CalcRefchargeFields()
		{
			Entity.Amount = ChargeoutRefchargeEntities.Sum(q => q.Amount) + PaychargeRefchargeEntities.Sum(q => q.Amount);
			BehaviorGridConrolChargeoutItem.UpdateTotalSummary();
			BehaviorGridConrolPaychargeItem.UpdateTotalSummary();
		}











	}
}
