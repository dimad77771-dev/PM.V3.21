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
	public class SupplierPaymentOneViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public GridControlBehaviorManager BehaviorGridConrolOrderItem { get; set; } = new GridControlBehaviorManager();
		#endregion
		public SupplierPayment Entity { get; set; }
		public bool IsNew { get; set; }
		public Guid? RowId { get; set; }
		public ObservableCollection<OrderPayment> OrderPaymentEntities { get; set; }
		public OrderPayment OrderPaymentSelectedEntity { get; set; }
		public ObservableCollection<SupplierPaymentRefund> SupplierPaymentRefundEntities { get; set; }
		public SupplierPaymentRefund SupplierPaymentRefundSelectedEntity { get; set; }
		public Boolean ReadOnly { get; set; }
		public virtual Boolean IsCollapsedLayoutSupplierRefunds { get; set; } = true;



		public SupplierPaymentOneViewModel() : base()
		{
			MessengerHelper.Register<MsgRowChange<Supplier>>(this, OnMsgRowChange);
		}
		public static SupplierPaymentOneViewModel Create()
		{
			return ViewModelSource.Create<SupplierPaymentOneViewModel>();
		}



		async public Task LoadData(bool isNew = false, Guid? rowId = null, SupplierPayment newSupplierPayment = null, Boolean readOnly = false, Guid? selectOrderRowId = null)
		{
			if (newSupplierPayment != null)
			{
				isNew = true;
			}
			IsNew = isNew;
			RowId = rowId;
			ReadOnly = readOnly;
			ShowWaitIndicator.Show();

			SupplierPayment entity;
			if (!IsNew)
			{
				entity = await businessService.GetSupplierPayment(RowId);
				entity.OrderPayments.ForEach(q => q.OnAfterLoad());
			}
			else
			{
				entity = newSupplierPayment;
			}
			Entity = entity;

			OrderPaymentEntities = new ObservableCollection<OrderPayment>(Entity.OrderPayments);
			OrderPaymentEntities.ForEach(q => SubscribeOrderPaymentRow(q));
			SupplierPaymentRefundEntities = new ObservableCollection<SupplierPaymentRefund>(Entity.SupplierPaymentRefunds);
			SupplierPaymentRefundEntities.ForEach(q => SubscribeSupplierPaymentRefundRow(q));
			CalcSupplierPaymentFields();
			IsCollapsedLayoutSupplierRefunds = !SupplierPaymentRefundEntities.Any();

			var srow = OrderPaymentEntities.FirstOrDefault(q => q.OrderRowId == selectOrderRowId);
			if (srow != null)
			{
				OrderPaymentSelectedEntity = srow;
				BehaviorGridConrolOrderItem.Focus();
			}


			ResetHasChange();
			ShowWaitIndicator.Hide();
		}





		public void OrderPaymentNew()
		{
			{
				DispatcherUIHelper.Run(async () =>
				{
					var parms = new PickOrderViewModel.OpenParams
					{
						ShowDXWindowsInteractionRequest = ShowDXWindowsInteractionRequest,
						MessageBoxService = MessageBoxService,
						ShowWaitIndicator = ShowWaitIndicator,
						PickMode = PickOrderViewModel.PickModeEnum.Main,
						SupplierRowId = Entity.SupplierRowId,
						ExcludeOrders = OrderPaymentEntities.Select(q => q.OrderRowId).ToArray(),
						NoPaidOnly = true,
						IsMultiSelect = true,
						ShowMessageIfNotExists = "There are no unpaid orders for this supplier in the system",
					};
					var ret2 = await PickOrderViewModel.PickRow(parms);
					if (!ret2.IsSuccess) return;

					OrderPayment firstAddedRow = null;
					var invoices = ret2.PickRows;
					foreach (var invoice in invoices)
					{
						var row = new OrderPayment
						{
							RowId = Guid.NewGuid(),
							OrderRowId = invoice.RowId,
							SupplierPaymentRowId = Entity.RowId,
							Amount = null,
							AllocationDate = DateTime.Today,
							Order = invoice,
							IsChanged = true,
							IsNew = true,
						};
						OrderPaymentEntities.Add(row);
						SubscribeOrderPaymentRow(row);
						if (firstAddedRow == null)
						{
							firstAddedRow = row;
						}
					}
					CalcSupplierPaymentFields();

					if (firstAddedRow != null)
					{
						OrderPaymentSelectedEntity = firstAddedRow;
						DispatcherUIHelper.Run(() =>
						{
							BehaviorGridConrolOrderItem.SetCurrentColumn("Amount");
							BehaviorGridConrolOrderItem.ShowEditor(true);
						});
					}
				});
			}
		}



		public void OrderPaymentDelete()
		{
			var row = OrderPaymentSelectedEntity;
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "row"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				OrderPaymentEntities.Remove(row);
				Entity.IsChanged = true;
				CalcSupplierPaymentFields();
			}
		}
		public bool CanOrderPaymentDelete() => (OrderPaymentSelectedEntity != null);




		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));



		bool Validate()
		{
			List<string> errors = new List<string>();

			ValidateHelper.Empty(Entity.SupplierPaymentDate, "SupplierPayment Date", errors);
			ValidateHelper.Positive(Entity.Amount, "Amount", errors);
			if (Entity.SupplierPaymentBalance < 0)
			{
				errors.Add("Total Allocated Amount can't be more then SupplierPayment Amount");
			}

			ValidateHelper.PositiveEnumerable(this, OrderPaymentEntities, (q) => q.Amount, "Allocated Amount", () => OrderPaymentSelectedEntity, errors);
			//!!!ValidateHelper.ValidateEnumerable(this, OrderPaymentEntities, (q) => q.NewBalanceDue >= 0, "Allocated Amount can't be more then Balance Due", () => OrderPaymentSelectedEntity, errors);


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
			updateEntity.OrderPayments = OrderPaymentEntities.Select(q => q.GetPocoClone()).ToList();

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = IsNew ?
				await businessService.PostSupplierPayment(updateEntity) :
				await businessService.PutSupplierPayment(updateEntity);
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

		bool HasChange()
		{
			if (Entity == null || OrderPaymentEntities == null)
			{
				return false;
			}

			return (IsNew || Entity.IsChanged || OrderPaymentEntities.Any(q => q.IsChanged));
		}

		void ResetHasChange()
		{
			Entity.IsChanged = false;
			OrderPaymentEntities.ForEach(q => q.IsChanged = false);
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



		void SubscribeOrderPaymentRow(OrderPayment row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				CalcSupplierPaymentFields();
			};
			row.OnOpenDetail = () =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneOrderView,
					Param = new OneOrderViewModel.OpenParams
					{
						IsNew = false,
						RowId = row.OrderRowId,
					},
				});
			};
		}

		void SubscribeSupplierPaymentRefundRow(SupplierPaymentRefund row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				CalcSupplierPaymentFields();
			};
			row.OnOpenDetail = () =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.SupplierRefundWindowView,
					Param = new SupplierRefundWindowViewModel.OpenParams { IsNew = false, RowId = row.SupplierRefundRowId },
				});
			};
		}


		void CalcSupplierPaymentFields()
		{
			Entity.AmountInOrders = OrderPaymentEntities.Sum(q => q.Amount) ?? 0;
			BehaviorGridConrolOrderItem.UpdateTotalSummary();
		}

		public void AutoAllocation()
		{
			var prows = new List<OrderPayment>();
			var paymentBalance = Entity.SupplierPaymentBalance;
			foreach (var row in OrderPaymentEntities.Where(q => (q.Amount ?? 0) <= 0))
			{
				if (paymentBalance > 0)
				{
					var invoiceTotal = row.Order.PaymentRequest;
					var allocateAmount = Math.Min(invoiceTotal, paymentBalance);
					row.Amount = allocateAmount;
					paymentBalance -= allocateAmount;
					prows.Add(row);
				}
			}
			CalcSupplierPaymentFields();

			DispatcherUIHelper.Run(() =>
			{
				RunAnimation(prows);
			});

			var zerorows = OrderPaymentEntities.Where(q => (q.Amount ?? 0) <= 0).ToArray();
			if (zerorows.Length > 0)
			{
				var err = "The total amount of orders exceeds payment amount. Would you like to remove exceeded items?";
				var ret = MessageBoxService.ShowMessage(err, CommonResources.Warning_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					zerorows.ForEach(q => OrderPaymentEntities.Remove(q));
				}
			}
		}

		void RunAnimation(IEnumerable<OrderPayment> rows)
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
