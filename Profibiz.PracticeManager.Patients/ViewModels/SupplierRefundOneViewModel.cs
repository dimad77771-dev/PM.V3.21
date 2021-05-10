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
	public class SupplierRefundOneViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public GridControlBehaviorManager BehaviorGridConrolSupplierPaymentItem { get; set; } = new GridControlBehaviorManager();
		#endregion
		public SupplierRefund Entity { get; set; }
		public bool IsNew { get; set; }
		public Guid? RowId { get; set; }
		public String SupplierRefundItemsType => Entity?.SupplierRefundItemsType;
		public ObservableCollection<SupplierPaymentRefund> SupplierPaymentRefundEntities { get; set; }
		public SupplierPaymentRefund SupplierPaymentRefundSelectedEntity { get; set; }
		public Boolean ReadOnly { get; set; }
		public virtual Boolean IsWindowMode { get; set; }
		public virtual Boolean IsShowCommandPanel => IsWindowMode;

		public virtual Boolean IsTypeOrder => SupplierRefundItemsType == TypeHelper.SupplierRefundItemsType_Order;
		public virtual Boolean IsTypeSupplierPayment => SupplierRefundItemsType == TypeHelper.SupplierRefundItemsType_SupplierPayment;
		public virtual Boolean IsVisibleGridOrder => IsTypeOrder;
		public virtual Boolean IsVisibleGridControlOrder => IsTypeOrder && !ReadOnly;
		public virtual Boolean IsVisibleGridSupplierPayment => IsTypeSupplierPayment;
		public virtual Boolean IsVisibleGridControlSupplierPayment => IsTypeSupplierPayment && !ReadOnly;
		public virtual String HeightGridOrder => IsTypeOrder ? "*" : "0";
		public virtual String HeightGridSupplierPayment => IsTypeSupplierPayment ? "*" : "0";




		public SupplierRefundOneViewModel() : base()
		{
			MessengerHelper.Register<MsgRowChange<Supplier>>(this, OnMsgRowChange);
		}
		public static SupplierRefundOneViewModel Create(bool isWindowMode)
		{
			var ret = ViewModelSource.Create<SupplierRefundOneViewModel>();
			ret.IsWindowMode = isWindowMode;
			return ret;
		}



		async public Task LoadData(bool isNew = false, Guid? rowId = null, SupplierRefund newSupplierRefund = null, List<SupplierPaymentRefund> newSupplierPaymentRefunds = null, Boolean readOnly = false, Guid? selectOrderRowId = null, Guid? selectSupplierPaymentRowId = null, InteractionRequest<CloseDXWindowsActionParam> closeInteractionRequest = null)
		{
			if (newSupplierRefund != null)
			{
				isNew = true;
			}
			IsNew = isNew;
			RowId = rowId;
			ReadOnly = readOnly;
			CloseInteractionRequest = closeInteractionRequest;
			ShowWaitIndicator.Show();

			SupplierRefund entity;
			if (!IsNew)
			{
				entity = await businessService.GetSupplierRefund(RowId);
				entity.SupplierPaymentRefunds.ForEach(q => q.OnAfterLoad());
			}
			else
			{
				entity = newSupplierRefund;
			}
			Entity = entity;
			if (newSupplierPaymentRefunds != null)
			{
				Entity.SupplierPaymentRefunds = newSupplierPaymentRefunds;
			}

			SupplierPaymentRefundEntities = new ObservableCollection<SupplierPaymentRefund>(Entity.SupplierPaymentRefunds);
			SupplierPaymentRefundEntities.ForEach(q => SubscribeSupplierPaymentRefundRow(q));
			CalcSupplierRefundFields();


			var srow2 = SupplierPaymentRefundEntities.FirstOrDefault(q => q.SupplierPaymentRowId == selectSupplierPaymentRowId);
			if (srow2 != null)
			{
				SupplierPaymentRefundSelectedEntity = srow2;
				BehaviorGridConrolSupplierPaymentItem.Focus();
			}


			ResetHasChange();

			ShowWaitIndicator.Hide();
		}


		public void SupplierPaymentRefundNew()
		{
			{
				DispatcherUIHelper.Run(async () =>
				{
					var ret2 = await PickSupplierPaymentViewModel.PickRow(
						showDXWindowsInteractionRequest: ShowDXWindowsInteractionRequest,
						pickMode: PickSupplierPaymentViewModel.PickModeEnum.Main,
						supplierRowId: Entity.SupplierRowId,
						excludeSupplierPayments: SupplierPaymentRefundEntities.Select(q => q.SupplierPaymentRowId).ToArray(),
						hasNoDistributedAmount: true,
						isMultiSelect: true);
					if (!ret2.IsSuccess) return;

					SupplierPaymentRefund firstAddedRow = null;
					var payments = ret2.PickRows;
					foreach (var payment in payments)
					{
						var row = new SupplierPaymentRefund
						{
							RowId = Guid.NewGuid(),
							SupplierPaymentRowId = payment.RowId,
							SupplierRefundRowId = Entity.RowId,
							Amount = payment.SupplierPaymentBalance,
							Amount0 = 0,
							AllocationDate = DateTime.Today,
							SupplierPayment = payment,
							IsChanged = true,
							IsNew = true,
						};
						SupplierPaymentRefundEntities.Add(row);
						SubscribeSupplierPaymentRefundRow(row);
						if (firstAddedRow == null)
						{
							firstAddedRow = row;
						}
					}
					CalcSupplierRefundFields();

					if (firstAddedRow != null)
					{
						SupplierPaymentRefundSelectedEntity = firstAddedRow;
						DispatcherUIHelper.Run(() =>
						{
							BehaviorGridConrolSupplierPaymentItem.SetCurrentColumn("Amount");
							BehaviorGridConrolSupplierPaymentItem.ShowEditor(true);
						});
					}
				});
			}
		}

		public void SupplierPaymentRefundDelete()
		{
			var row = SupplierPaymentRefundSelectedEntity;
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "row"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				SupplierPaymentRefundEntities.Remove(row);
				Entity.IsChanged = true;
				CalcSupplierRefundFields();
			}
		}
		public bool CanSupplierPaymentRefundDelete() => (SupplierPaymentRefundSelectedEntity != null);




		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));


		bool Validate()
		{
			List<string> errors = new List<string>();

			ValidateHelper.Empty(Entity.SupplierPaymentDate, "SupplierPayment Date", errors);
			ValidateHelper.Positive(Entity.Amount, "Amount", errors);
			ValidateHelper.PositiveEnumerable(this, SupplierPaymentRefundEntities, (q) => q.Amount, "Allocated Amount", () => SupplierPaymentRefundSelectedEntity, errors);

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
			updateEntity.SupplierPaymentRefunds = SupplierPaymentRefundEntities.Select(q => q.GetPocoClone()).ToList();

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = IsNew ?
				await businessService.PostSupplierRefund(updateEntity) :
				await businessService.PutSupplierRefund(updateEntity);
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




		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; }
		bool forceClose;
		void CloseCore(bool force = false)
		{
			this.forceClose = force;
			CloseInteractionRequest?.Raise(null);
		}

		bool HasChange()
		{
			if (Entity == null || SupplierPaymentRefundEntities == null)
			{
				return false;
			}

			return (IsNew || Entity.IsChanged || SupplierPaymentRefundEntities.Any(q => q.IsChanged));
		}

		void ResetHasChange()
		{
			Entity.IsChanged = false;
			SupplierPaymentRefundEntities.ForEach(q => q.IsChanged = false);
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


		public void NewOpenSupplier(bool isnew)
		{
			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.OneSupplierView,
				Param = "OpenRowId=" + Entity.SupplierRowId,
			});
		}
		public void OpenSupplier() => NewOpenSupplier(false);
		public bool CanOpenSupplier() => (!GuidHelper.IsNullOrEmpty(Entity?.SupplierRowId));

		void OnMsgRowChange(MsgRowChange<Supplier> msg)
		{
			MessengerHelper.RunAction(this, () =>
			{
				if (msg.RowAction == RowAction.Update && Entity.SupplierRowId == msg.Row.RowId)
				{
					Entity.Supplier = msg.Row;
				}
			});
		}

		void SubscribeSupplierPaymentRefundRow(SupplierPaymentRefund row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				CalcSupplierRefundFields();
			};
			row.OnOpenDetail = () =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.SupplierPaymentWindowView,
					Param = new SupplierPaymentWindowViewModel.OpenParams
					{
						IsNew = false,
						RowId = row.SupplierPaymentRowId,
					},
				});
			};
		}

		void CalcSupplierRefundFields()
		{
			Entity.Amount = SupplierPaymentRefundEntities.Sum(q => q.Amount);
			BehaviorGridConrolSupplierPaymentItem.UpdateTotalSummary();
		}











	}
}
