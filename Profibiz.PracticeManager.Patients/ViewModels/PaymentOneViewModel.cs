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
	public class PaymentOneViewModel : ViewModelBase 
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public GridControlBehaviorManager BehaviorGridConrolInvoiceItem { get; set; } = new GridControlBehaviorManager();
		#endregion
		public Payment Entity { get; set; }
		public bool IsNew { get; set; }
		public Guid? RowId { get; set; }
		public ObservableCollection<InvoicePayment> InvoicePaymentEntities { get; set; }
		public InvoicePayment InvoicePaymentSelectedEntity { get; set; }
		public ObservableCollection<PaymentRefund> PaymentRefundEntities { get; set; }
		public PaymentRefund PaymentRefundSelectedEntity { get; set; }
		public virtual Boolean ReadOnly { get; set; }
		public virtual Boolean IsCollapsedLayoutRefunds { get; set; } = true;



		public PaymentOneViewModel() : base()
		{
			MessengerHelper.Register<MsgRowChange<Patient>>(this, OnMsgRowChange);
		}
        public static PaymentOneViewModel Create()
        {
            return ViewModelSource.Create<PaymentOneViewModel>();
        }



        async public Task LoadData(bool isNew = false, Guid? rowId = null, Payment newPayment = null, Boolean readOnly = false, Guid? selectInvoiceRowId = null)
		{
			if (newPayment != null)
			{
				isNew = true;
			}
			IsNew = isNew;
			RowId = rowId;
            ReadOnly = readOnly;
            ShowWaitIndicator.Show();

			Payment entity;
			if (!IsNew)
			{
				entity = await businessService.GetPayment(RowId);
				entity.InvoicePayments.ForEach(q => q.OnAfterLoad());
			}
			else
			{
				entity = newPayment;
			}
			Entity = entity;
			await LoadPatient();

			InvoicePaymentEntities = new ObservableCollection<InvoicePayment>(Entity.InvoicePayments);
            InvoicePaymentEntities.ForEach(q => SubscribeInvoicePaymentRow(q));
			PaymentRefundEntities = new ObservableCollection<PaymentRefund>(Entity.PaymentRefunds);
			PaymentRefundEntities.ForEach(q => SubscribePaymentRefundRow(q));
			CalcPaymentFields();
			IsCollapsedLayoutRefunds = !PaymentRefundEntities.Any();

			var srow = InvoicePaymentEntities.FirstOrDefault(q => q.InvoiceRowId == selectInvoiceRowId);
            if (srow != null)
            {
                InvoicePaymentSelectedEntity = srow;
                BehaviorGridConrolInvoiceItem.Focus();
            }


            ResetHasChange();
			ShowWaitIndicator.Hide();
		}


		async Task LoadPatient()
		{
			if (Entity.PatientRowId != null && Entity.Patient == null)
			{
				Entity.Patient = await businessService.GetPatient(Entity.PatientRowId);
			}
		}


        public void InvoicePaymentNew()
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
						ExcludeInvoices = InvoicePaymentEntities.Select(q => q.InvoiceRowId).ToArray(),
						FlagNoPaidOrNoApprovedAmount = true,
						ShowMessageIfNotExists = "There are no unpaid invoices for this patient in the system. To allocate payments please enter invoices from invoice builder",
					};
					var ret2 = await PickInvoiceViewModel.PickRow(parms);
                    if (!ret2.IsSuccess) return;

                    InvoicePayment firstAddedRow = null;
                    var invoices = ret2.PickRows;
                    foreach (var invoice in invoices)
                    {
                        var row = new InvoicePayment
                        {
                            RowId = Guid.NewGuid(),
                            InvoiceRowId = invoice.RowId,
                            PaymentRowId = Entity.RowId,
                            Amount = null,
                            AllocationDate = DateTime.Today,
                            Invoice = invoice,
                            IsChanged = true,
                            IsNew = true,
                        };
                        InvoicePaymentEntities.Add(row);
                        SubscribeInvoicePaymentRow(row);
                        if (firstAddedRow == null)
                        {
                            firstAddedRow = row;
                        }
                    }
                    CalcPaymentFields();

                    if (firstAddedRow != null)
                    {
                        InvoicePaymentSelectedEntity = firstAddedRow;
                        DispatcherUIHelper.Run(() =>
                        {
                            BehaviorGridConrolInvoiceItem.SetCurrentColumn("Amount");
                            BehaviorGridConrolInvoiceItem.ShowEditor(true);
                        });
                    }
                });
            }
        }



        public void InvoicePaymentDelete()
        {
            var row = InvoicePaymentSelectedEntity;
            var messageBoxService = this.GetRequiredService<IMessageBoxService>();
            var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "row"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
            if (ret == MessageResult.Yes)
            {
                InvoicePaymentEntities.Remove(row);
                Entity.IsChanged = true;
                CalcPaymentFields();
            }
        }
        public bool CanInvoicePaymentDelete() => (InvoicePaymentSelectedEntity != null);




		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));

		

		bool Validate()
		{
			List<string> errors = new List<string>();

			ValidateHelper.Empty(Entity.PaymentDate, "Payment Date", errors);
			ValidateHelper.Positive(Entity.Amount, "Amount", errors);
			if (Entity.PaymentBalance < 0)
            {
                errors.Add("Total Allocated Amount can't be more then Payment Amount");
            }

            ValidateHelper.PositiveEnumerable(this, InvoicePaymentEntities, (q) => q.Amount, "Allocated Amount", () => InvoicePaymentSelectedEntity, errors);
            //!!!ValidateHelper.ValidateEnumerable(this, InvoicePaymentEntities, (q) => q.NewBalanceDue >= 0, "Allocated Amount can't be more then Balance Due", () => InvoicePaymentSelectedEntity, errors);


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
			updateEntity.InvoicePayments = InvoicePaymentEntities.Select(q => q.GetPocoClone()).ToList();

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = IsNew ?
				await businessService.PostPayment(updateEntity) :
				await businessService.PutPayment(updateEntity);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService))
			{
				return false;
			}

			MessengerHelper.SendMsgRowChange(updateEntity, IsNew);
			foreach(var invoicePayment in InvoicePaymentEntities)
			{
				MessengerHelper.SendMsgRowChange(invoicePayment.Invoice, false);
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
			if (Entity == null || InvoicePaymentEntities == null)
			{
				return false;
			}

			return (IsNew || Entity.IsChanged || InvoicePaymentEntities.Any(q => q.IsChanged));
		}

		void ResetHasChange()
		{
			Entity.IsChanged = false;
			InvoicePaymentEntities.ForEach(q => q.IsChanged = false);
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



        void SubscribeInvoicePaymentRow(InvoicePayment row)
        {
            (row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
            {
                CalcPaymentFields();
            };
            row.OnOpenDetail = () =>
            {
                ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
                {
                    ViewCode = ViewCodes.InvoiceWindowView,
                    Param = new InvoiceWindowViewModel.OpenParams { IsNew = false, RowId = row.InvoiceRowId, SelectPaymentRowId = row.PaymentRowId },
                });
            };
        }

		void SubscribePaymentRefundRow(PaymentRefund row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				CalcPaymentFields();
			};
			row.OnOpenDetail = () =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.RefundWindowView,
					Param = new RefundWindowViewModel.OpenParams { IsNew = false, RowId = row.RefundRowId },
				});
			};
		}


		void CalcPaymentFields()
        {
            Entity.AmountInInvoices = InvoicePaymentEntities.Sum(q => q.Amount) ?? 0;
            BehaviorGridConrolInvoiceItem.UpdateTotalSummary();
        }

        public void AutoAllocation()
        {
            var prows = new List<InvoicePayment>();
            var paymentBalance = Entity.PaymentBalance;
            foreach (var row in InvoicePaymentEntities.Where(q => (q.Amount ?? 0) <= 0))
            {
                if (paymentBalance > 0)
                {
                    var invoiceTotal = row.Invoice.PaymentRequest ?? 0;
                    var allocateAmount = Math.Min(invoiceTotal, paymentBalance);
                    row.Amount = allocateAmount;
                    paymentBalance -= allocateAmount;
                    prows.Add(row);
                }
            }
            CalcPaymentFields();

            DispatcherUIHelper.Run(() =>
			{
				RunAnimation(prows);
			});

			var zerorows = InvoicePaymentEntities.Where(q => (q.Amount ?? 0) <= 0).ToArray();
			if (zerorows.Length > 0)
			{
				var err = "The total amount of invoices exceeds payment amount. Would you like to remove exceeded items?";
				var ret = MessageBoxService.ShowMessage(err, CommonResources.Warning_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					zerorows.ForEach(q => InvoicePaymentEntities.Remove(q));
				}
			}
		}

        void RunAnimation(IEnumerable<InvoicePayment> rows)
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
