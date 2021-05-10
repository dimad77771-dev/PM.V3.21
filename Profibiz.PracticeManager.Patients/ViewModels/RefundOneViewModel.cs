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
	public class RefundOneViewModel : ViewModelBase 
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public GridControlBehaviorManager BehaviorGridConrolInvoiceItem { get; set; } = new GridControlBehaviorManager();
		public GridControlBehaviorManager BehaviorGridConrolPaymentItem { get; set; } = new GridControlBehaviorManager();
		#endregion
		public Refund Entity { get; set; }
		public bool IsNew { get; set; }
		public Guid? RowId { get; set; }
		public String RefundItemsType => Entity?.RefundItemsType;
		public ObservableCollection<InvoiceRefund> InvoiceRefundEntities { get; set; }
		public InvoiceRefund InvoiceRefundSelectedEntity { get; set; }
		public ObservableCollection<PaymentRefund> PaymentRefundEntities { get; set; }
		public PaymentRefund PaymentRefundSelectedEntity { get; set; }
		public Boolean ReadOnly { get; set; }
		public virtual Boolean IsWindowMode { get; set; }
		public virtual Boolean IsShowCommandPanel => IsWindowMode;

		public virtual Boolean IsTypeInvoice => RefundItemsType == TypeHelper.RefundItemsType_Invoice;
		public virtual Boolean IsTypePayment => RefundItemsType == TypeHelper.RefundItemsType_Payment;
		public virtual Boolean IsVisibleGridInvoice => IsTypeInvoice;
		public virtual Boolean IsVisibleGridControlInvoice => IsTypeInvoice && !ReadOnly;
		public virtual Boolean IsVisibleGridPayment => IsTypePayment;
		public virtual Boolean IsVisibleGridControlPayment => IsTypePayment && !ReadOnly;
		public virtual String HeightGridInvoice => IsTypeInvoice ? "*" : "0";
		public virtual String HeightGridPayment => IsTypePayment ? "*" : "0";




		public RefundOneViewModel() : base()
		{
			MessengerHelper.Register<MsgRowChange<Patient>>(this, OnMsgRowChange);
		}
        public static RefundOneViewModel Create(bool isWindowMode)
        {
			var ret = ViewModelSource.Create<RefundOneViewModel>();
			ret.IsWindowMode = isWindowMode;
			return ret;
		}



		async public Task LoadData(bool isNew = false, Guid? rowId = null, Refund newRefund = null, List<InvoiceRefund> newInvoiceRefunds = null, List<PaymentRefund> newPaymentRefunds = null, Boolean readOnly = false, Guid? selectInvoiceRowId = null, Guid? selectPaymentRowId = null, InteractionRequest<CloseDXWindowsActionParam> closeInteractionRequest = null)
		{
			if (newRefund != null)
			{
				isNew = true;
			}
			IsNew = isNew;
			RowId = rowId;
            ReadOnly = readOnly;
			CloseInteractionRequest = closeInteractionRequest;
			ShowWaitIndicator.Show();

			Refund entity;
			if (!IsNew)
			{
				entity = await businessService.GetRefund(RowId);
				entity.PaymentRefunds.ForEach(q => q.OnAfterLoad());
				entity.InvoiceRefunds.ForEach(q => q.OnAfterLoad());
			}
			else
			{
				entity = newRefund;
			}
			Entity = entity;
			if (newInvoiceRefunds != null)
			{
				Entity.InvoiceRefunds = newInvoiceRefunds;
			}
			if (newPaymentRefunds != null)
			{
				Entity.PaymentRefunds = newPaymentRefunds;
			}

			InvoiceRefundEntities = new ObservableCollection<InvoiceRefund>(Entity.InvoiceRefunds);
            InvoiceRefundEntities.ForEach(q => SubscribeInvoiceRefundRow(q));
			PaymentRefundEntities = new ObservableCollection<PaymentRefund>(Entity.PaymentRefunds);
			PaymentRefundEntities.ForEach(q => SubscribePaymentRefundRow(q));
			CalcRefundFields();

            var srow1 = InvoiceRefundEntities.FirstOrDefault(q => q.InvoiceRowId == selectInvoiceRowId);
            if (srow1 != null)
            {
                InvoiceRefundSelectedEntity = srow1;
                BehaviorGridConrolInvoiceItem.Focus();
            }

			var srow2 = PaymentRefundEntities.FirstOrDefault(q => q.PaymentRowId == selectPaymentRowId);
			if (srow2 != null)
			{
				PaymentRefundSelectedEntity = srow2;
				BehaviorGridConrolPaymentItem.Focus();
			}


			ResetHasChange();
			
			ShowWaitIndicator.Hide();
		}





        public void InvoiceRefundNew()
        {
            {
                DispatcherUIHelper.Run(async () =>
                {
					var parms = new PickInvoiceViewModel.OpenParams
					{
						ShowDXWindowsInteractionRequest = ShowDXWindowsInteractionRequest,
						MessageBoxService = MessageBoxService,
						ShowWaitIndicator = ShowWaitIndicator,
						PickMode = PickInvoiceViewModel.PickModeEnum.Main,
						PatientRowId = Entity.PatientRowId,
						UseFamilyHead = true,
						ExcludeInvoices = InvoiceRefundEntities.Select(q => q.InvoiceRowId).ToArray(),
						NegativeBalanceOnly = true,
						ShowMessageIfNotExists = "There are no invoices that have payments that can be refunded",
					};
					var ret2 = await PickInvoiceViewModel.PickRow(parms);
                    if (!ret2.IsSuccess) return;

                    InvoiceRefund firstAddedRow = null;
                    var invoices = ret2.PickRows;
                    foreach (var invoice in invoices)
                    {
                        var row = new InvoiceRefund
                        {
                            RowId = Guid.NewGuid(),
                            InvoiceRowId = invoice.RowId,
                            RefundRowId = Entity.RowId,
                            Amount = -(invoice.Balance ?? 0),
                            AllocationDate = DateTime.Today,
                            Invoice = invoice,
                            IsChanged = true,
                            IsNew = true,
                        };
						//row.OnAfterLoad();
                        InvoiceRefundEntities.Add(row);
                        SubscribeInvoiceRefundRow(row);
                        if (firstAddedRow == null)
                        {
                            firstAddedRow = row;
                        }
                    }
                    CalcRefundFields();

                    if (firstAddedRow != null)
                    {
                        InvoiceRefundSelectedEntity = firstAddedRow;
                        DispatcherUIHelper.Run(() =>
                        {
                            BehaviorGridConrolInvoiceItem.SetCurrentColumn("Amount");
                            BehaviorGridConrolInvoiceItem.ShowEditor(true);
                        });
                    }
                });
            }
        }

        public void InvoiceRefundDelete()
        {
            var row = InvoiceRefundSelectedEntity;
            var messageBoxService = this.GetRequiredService<IMessageBoxService>();
            var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "row"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
            if (ret == MessageResult.Yes)
            {
                InvoiceRefundEntities.Remove(row);
                Entity.IsChanged = true;
                CalcRefundFields();
            }
        }
        public bool CanInvoiceRefundDelete() => (InvoiceRefundSelectedEntity != null);



		
		public void PaymentRefundNew()
		{
			{
				DispatcherUIHelper.Run(async () =>
				{
					//var parms = new PickPaymentViewModel.OpenParams
					//{
					//	//!!ShowDXWindowsInteractionRequest = ShowDXWindowsInteractionRequest,
					//	//!!MessageBoxService = MessageBoxService,
					//	//!!ShowWaitIndicator = ShowWaitIndicator,
					//	PickMode = PickPaymentViewModel.PickModeEnum.Main,
					//	PatientRowId = Entity.PatientRowId,
					//	//!!UseFamilyHead = true,
					//	ExcludePayments = PaymentRefundEntities.Select(q => q.PaymentRowId).ToArray(),
					//	//!!NegativeBalanceOnly = true,
					//	//!!ShowMessageIfNotExists = "There are no payments that have payments that can be refunded",
					//};
					//var ret2 = await PickPaymentViewModel.PickRow(parms);
					var ret2 = await PickPaymentViewModel.PickRow(
						showDXWindowsInteractionRequest: ShowDXWindowsInteractionRequest,
						pickMode: PickPaymentViewModel.PickModeEnum.Main,
						patientRowId: Entity.PatientRowId,
						excludePayments: PaymentRefundEntities.Select(q => q.PaymentRowId).ToArray(),
						hasNoDistributedAmount: true,
						isMultiSelect: true);
					if (!ret2.IsSuccess) return;

					PaymentRefund firstAddedRow = null;
					var payments = ret2.PickRows;
					foreach (var payment in payments)
					{
						var row = new PaymentRefund
						{
							RowId = Guid.NewGuid(),
							PaymentRowId = payment.RowId,
							RefundRowId = Entity.RowId,
							Amount = payment.PaymentBalance,
							Amount0 = 0,
							AllocationDate = DateTime.Today,
							Payment = payment,
							IsChanged = true,
							IsNew = true,
						};
						PaymentRefundEntities.Add(row);
						SubscribePaymentRefundRow(row);
						if (firstAddedRow == null)
						{
							firstAddedRow = row;
						}
					}
					CalcRefundFields();

					if (firstAddedRow != null)
					{
						PaymentRefundSelectedEntity = firstAddedRow;
						DispatcherUIHelper.Run(() =>
						{
							BehaviorGridConrolPaymentItem.SetCurrentColumn("Amount");
							BehaviorGridConrolPaymentItem.ShowEditor(true);
						});
					}
				});
			}
		}

		public void PaymentRefundDelete()
		{
			var row = PaymentRefundSelectedEntity;
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "row"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				PaymentRefundEntities.Remove(row);
				Entity.IsChanged = true;
				CalcRefundFields();
			}
		}
		public bool CanPaymentRefundDelete() => (PaymentRefundSelectedEntity != null);




		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));
				

		bool Validate()
		{
			List<string> errors = new List<string>();

			ValidateHelper.Empty(Entity.PaymentDate, "Payment Date", errors);
			ValidateHelper.Positive(Entity.Amount, "Amount", errors);
			//if (Entity.PaymentBalance < 0)
			//         {
			//             errors.Add("Total Allocated Amount can't be more then Refund Amount");
			//         }

			ValidateHelper.PositiveEnumerable(this, InvoiceRefundEntities, (q) => q.Amount, "Allocated Amount", () => InvoiceRefundSelectedEntity, errors);
			//!!!ValidateHelper.ValidateEnumerable(this, InvoiceRefundEntities, (q) => q.NewBalanceDue >= 0, "Allocated Amount can't be more then Balance Due", () => InvoiceRefundSelectedEntity, errors);
			ValidateHelper.PositiveEnumerable(this, PaymentRefundEntities, (q) => q.Amount, "Allocated Amount", () => PaymentRefundSelectedEntity, errors);

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
			updateEntity.InvoiceRefunds = InvoiceRefundEntities.Select(q => q.GetPocoClone()).ToList();
			updateEntity.PaymentRefunds = PaymentRefundEntities.Select(q => q.GetPocoClone()).ToList();

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = IsNew ?
				await businessService.PostRefund(updateEntity) :
				await businessService.PutRefund(updateEntity);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService))
			{
				return false;
			}

			MessengerHelper.SendMsgRowChange(updateEntity, IsNew);
			IsNew = false;
			ResetHasChange();
			InvoiceRefundEntities.ForEach(q => q.IsNew = false);

			//close
			if (andClose)
			{
				CloseCore(force:true);
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
			if (Entity == null || InvoiceRefundEntities == null || PaymentRefundEntities == null)
			{
				return false;
			}

			return (IsNew || Entity.IsChanged || InvoiceRefundEntities.Any(q => q.IsChanged) || PaymentRefundEntities.Any(q => q.IsChanged));
		}

		void ResetHasChange()
		{
			Entity.IsChanged = false;
			InvoiceRefundEntities.ForEach(q => q.IsChanged = false);
			PaymentRefundEntities.ForEach(q => q.IsChanged = false);
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
		public void OpenPatient() => NewOpenPatient(false);
		public bool CanOpenPatient() => (!GuidHelper.IsNullOrEmpty(Entity?.PatientRowId));

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



        void SubscribeInvoiceRefundRow(InvoiceRefund row)
        {
            (row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
            {
                CalcRefundFields();
            };
            row.OnOpenDetail = () =>
            {
                ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
                {
                    ViewCode = ViewCodes.InvoiceWindowView,
                    Param = new InvoiceWindowViewModel.OpenParams
					{
						IsNew = false,
						RowId = row.InvoiceRowId,
					},
                });
            };
        }

		void SubscribePaymentRefundRow(PaymentRefund row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				CalcRefundFields();
			};
			row.OnOpenDetail = () =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.PaymentWindowView,
					Param = new PaymentWindowViewModel.OpenParams
					{
						IsNew = false,
						RowId = row.PaymentRowId,
					},
				});
			};
		}

		void CalcRefundFields()
        {
            Entity.Amount = InvoiceRefundEntities.Sum(q => q.Amount) + PaymentRefundEntities.Sum(q => q.Amount);
            BehaviorGridConrolInvoiceItem.UpdateTotalSummary();
			BehaviorGridConrolPaymentItem.UpdateTotalSummary();
		}











    }
}
