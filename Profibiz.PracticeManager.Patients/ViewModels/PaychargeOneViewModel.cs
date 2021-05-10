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
	public class PaychargeOneViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public GridControlBehaviorManager BehaviorGridConrolChargeoutItem { get; set; } = new GridControlBehaviorManager();
		#endregion
		public Paycharge Entity { get; set; }
		public bool IsNew { get; set; }
		public Guid? RowId { get; set; }
		public ObservableCollection<ChargeoutPaycharge> ChargeoutPaychargeEntities { get; set; }
		public ChargeoutPaycharge ChargeoutPaychargeSelectedEntity { get; set; }
		public ObservableCollection<PaychargeRefcharge> PaychargeRefchargeEntities { get; set; }
		public PaychargeRefcharge PaychargeRefchargeSelectedEntity { get; set; }
		public Boolean ReadOnly { get; set; }
		public virtual Boolean IsCollapsedLayoutRefcharges { get; set; } = true;



		public PaychargeOneViewModel() : base()
		{
		}
		public static PaychargeOneViewModel Create()
		{
			return ViewModelSource.Create<PaychargeOneViewModel>();
		}



		async public Task LoadData(bool isNew = false, Guid? rowId = null, Paycharge newPaycharge = null, Boolean readOnly = false, Guid? selectChargeoutRowId = null)
		{
			if (newPaycharge != null)
			{
				isNew = true;
			}
			IsNew = isNew;
			RowId = rowId;
			ReadOnly = readOnly;
			ShowWaitIndicator.Show();

			Paycharge entity;
			if (!IsNew)
			{
				entity = await businessService.GetPaycharge(RowId);
				entity.ChargeoutPaycharges.ForEach(q => q.OnAfterLoad());
			}
			else
			{
				entity = newPaycharge;
			}
			Entity = entity;

			ChargeoutPaychargeEntities = new ObservableCollection<ChargeoutPaycharge>(Entity.ChargeoutPaycharges);
			ChargeoutPaychargeEntities.ForEach(q => SubscribeChargeoutPaychargeRow(q));
			PaychargeRefchargeEntities = new ObservableCollection<PaychargeRefcharge>(Entity.PaychargeRefcharges);
			PaychargeRefchargeEntities.ForEach(q => SubscribePaychargeRefchargeRow(q));
			CalcPaychargeFields();
			IsCollapsedLayoutRefcharges = !PaychargeRefchargeEntities.Any();

			var srow = ChargeoutPaychargeEntities.FirstOrDefault(q => q.ChargeoutRowId == selectChargeoutRowId);
			if (srow != null)
			{
				ChargeoutPaychargeSelectedEntity = srow;
				BehaviorGridConrolChargeoutItem.Focus();
			}


			ResetHasChange();
			ShowWaitIndicator.Hide();
		}





		public void ChargeoutPaychargeNew()
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
						ExcludeChargeouts = ChargeoutPaychargeEntities.Select(q => q.ChargeoutRowId).ToArray(),
						FlagNoPaidOrNoApprovedAmount = true,
						ShowMessageIfNotExists = "There are no unpaid Outgoing Invoices for this Recipient in the system",
					};
					var ret2 = await PickChargeoutViewModel.PickRow(parms);
					if (!ret2.IsSuccess) return;

					ChargeoutPaycharge firstAddedRow = null;
					var chargeouts = ret2.PickRows;
					foreach (var chargeout in chargeouts)
					{
						var row = new ChargeoutPaycharge
						{
							RowId = Guid.NewGuid(),
							ChargeoutRowId = chargeout.RowId,
							PaychargeRowId = Entity.RowId,
							Amount = null,
							AllocationDate = DateTime.Today,
							Chargeout = chargeout,
							IsChanged = true,
							IsNew = true,
						};
						ChargeoutPaychargeEntities.Add(row);
						SubscribeChargeoutPaychargeRow(row);
						if (firstAddedRow == null)
						{
							firstAddedRow = row;
						}
					}
					CalcPaychargeFields();

					if (firstAddedRow != null)
					{
						ChargeoutPaychargeSelectedEntity = firstAddedRow;
						DispatcherUIHelper.Run(() =>
						{
							BehaviorGridConrolChargeoutItem.SetCurrentColumn("Amount");
							BehaviorGridConrolChargeoutItem.ShowEditor(true);
						});
					}
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
				CalcPaychargeFields();
			}
		}
		public bool CanChargeoutPaychargeDelete() => (ChargeoutPaychargeSelectedEntity != null);




		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));



		bool Validate()
		{
			List<string> errors = new List<string>();

			ValidateHelper.Empty(Entity.PaychargeDate, "Paycharge Date", errors);
			ValidateHelper.Positive(Entity.Amount, "Amount", errors);
			if (Entity.PaychargeBalance < 0)
			{
				errors.Add("Total Allocated Amount can't be more then Paycharge Amount");
			}

			ValidateHelper.PositiveEnumerable(this, ChargeoutPaychargeEntities, (q) => q.Amount, "Allocated Amount", () => ChargeoutPaychargeSelectedEntity, errors);
			//!!!ValidateHelper.ValidateEnumerable(this, ChargeoutPaychargeEntities, (q) => q.NewBalanceDue >= 0, "Allocated Amount can't be more then Balance Due", () => ChargeoutPaychargeSelectedEntity, errors);


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
			updateEntity.ChargeoutPaycharges = ChargeoutPaychargeEntities.Select(q => q.GetPocoClone()).ToList();

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = IsNew ?
				await businessService.PostPaycharge(updateEntity) :
				await businessService.PutPaycharge(updateEntity);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService))
			{
				return false;
			}

			MessengerHelper.SendMsgRowChange(updateEntity, IsNew);
			IsNew = false;
			ResetHasChange();

			//close
			if (andClose)
			{
				CloseCore(force: true);
			}

			return true;
		}
		public void Close() => CloseCore();




		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		bool forceClose;
		void CloseCore(bool force = false)
		{
			this.forceClose = force;
			CloseInteractionRequest.Raise(null);
		}

		public bool HasChange()
		{
			if (Entity == null || ChargeoutPaychargeEntities == null)
			{
				return false;
			}

			return (IsNew || Entity.IsChanged || ChargeoutPaychargeEntities.Any(q => q.IsChanged));
		}

		void ResetHasChange()
		{
			Entity.IsChanged = false;
			ChargeoutPaychargeEntities.ForEach(q => q.IsChanged = false);
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
					RowId = (isnew ? default(Guid) : Entity.ChargeoutRecipientRowId),
					NewActionBookmark = lastNewActionBookmark,
				},
			});
		}
		public void OpenPatient() => NewOpenPatient(false);
		public bool CanOpenPatient() => (!GuidHelper.IsNullOrEmpty(Entity?.ChargeoutRecipientRowId));



		void SubscribeChargeoutPaychargeRow(ChargeoutPaycharge row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				CalcPaychargeFields();
			};
			row.OnOpenDetail = () =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.ChargeoutWindowView,
					Param = new ChargeoutWindowViewModel.OpenParams { IsNew = false, RowId = row.ChargeoutRowId, SelectPaychargeRowId = row.PaychargeRowId },
				});
			};
		}

		void SubscribePaychargeRefchargeRow(PaychargeRefcharge row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				CalcPaychargeFields();
			};
			row.OnOpenDetail = () =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.RefchargeWindowView,
					Param = new RefchargeWindowViewModel.OpenParams { IsNew = false, RowId = row.RefchargeRowId },
				});
			};
		}


		void CalcPaychargeFields()
		{
			Entity.AmountInChargeouts = ChargeoutPaychargeEntities.Sum(q => q.Amount) ?? 0;
			BehaviorGridConrolChargeoutItem.UpdateTotalSummary();
		}

		public void AutoAllocation()
		{
			var prows = new List<ChargeoutPaycharge>();
			var paychargeBalance = Entity.PaychargeBalance;
			foreach (var row in ChargeoutPaychargeEntities.Where(q => (q.Amount ?? 0) <= 0))
			{
				if (paychargeBalance > 0)
				{
					var chargeoutTotal = row.Chargeout.PaychargeRequest ?? 0;
					var allocateAmount = Math.Min(chargeoutTotal, paychargeBalance);
					row.Amount = allocateAmount;
					paychargeBalance -= allocateAmount;
					prows.Add(row);
				}
			}
			CalcPaychargeFields();

			DispatcherUIHelper.Run(() =>
			{
				RunAnimation(prows);
			});

			var zerorows = ChargeoutPaychargeEntities.Where(q => (q.Amount ?? 0) <= 0).ToArray();
			if (zerorows.Length > 0)
			{
				var err = "The total amount of chargeouts exceeds paycharge amount. Would you like to remove exceeded items?";
				var ret = MessageBoxService.ShowMessage(err, CommonResources.Warning_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					zerorows.ForEach(q => ChargeoutPaychargeEntities.Remove(q));
				}
			}
		}

		void RunAnimation(IEnumerable<ChargeoutPaycharge> rows)
		{
			foreach (var row in rows)
			{
				var animation = new ColorAnimation(
					(Color)ColorConverter.ConvertFromString("#DEF8CB"),
					(Color)ColorConverter.ConvertFromString("#93F747"),
					new Duration(TimeSpan.FromSeconds(0.5)));
				animation.AutoReverse = true;

				animation.Completed += (s, e) =>
				{
					row.CellBackgroundAmount.IsActive = false;
				};
				row.CellBackgroundAmount.IsActive = true;
				row.CellBackgroundAmount.BeginAnimation(BackgroundAnimationElement.ColorProperty, animation, HandoffBehavior.SnapshotAndReplace);
			}
		}









	}
}
