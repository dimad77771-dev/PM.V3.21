using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using Profibiz.PracticeManager.Model;
using DevExpress.DevAV.Common;
using System.Collections.ObjectModel;
using Prism.Interactivity.InteractionRequest;
using Profibiz.PracticeManager.Infrastructure;
using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using AutoMapper;
using Prism.Regions;
using Autofac;
using System.Collections.Specialized;
using Profibiz.PracticeManager.InfrastructureExt.Common;
using Profibiz.PracticeManager.Patients.BusinessService;
using DevExpress.Xpf.Grid;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class PaymentsListViewModel : ViewModelBase, IOnCloseView
	{
		#region Service
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
		#endregion
		public virtual ObservableCollection<Payment> Entities { get; set; }
		public virtual Payment SelectedEntity { get; set; }
		public virtual PaymentOneViewModel OneModel { get; set; }
		public virtual DateTime FilterFrom { get; set; }
		public virtual DateTime FilterTo { get; set; }
		public virtual Boolean IsShowAllowRefundOnly { get; set; } = false;
		public virtual ObservableCollection<AccountAgingModel> RibbonSpAccountAgingListItems { get; set; } = AccountAgingInfo.RibbonSpAccountAgingListItems;
		public virtual Int32 RibbonSpAccountAgingListColumnCount { get; set; } = AccountAgingInfo.RibbonSpAccountAgingListColumnCount;



		public PaymentsListViewModel() : base()
		{
			var ret = GlobalSettings.Instance.Finances.GetFinancesViewDateFilter();
			FilterFrom = ret.FilterFrom;
			FilterTo = ret.FilterTo;
		}

		public void OnOpen(string parm)
		{
			OpenParmQuery = parm;
			OpenParms = QueryHelper.ParseString(parm);
			MessengerHelper.Register<MsgRowChange<Payment>>(this, OnMsgRowChangePayment);
			MessengerHelper.Register<MsgRowChange<Refund>>(this, OnMsgRowChangeRefund);
			DispatcherUIHelper.Run(async () =>
			{
				await LoadData();
			});
		}
		String OpenParmQuery;
		NameValueCollection OpenParms;



		async Task LoadData()
		{
			ShowWaitIndicator.Show();
			var task = BusinessServiceHelprer.GetPaymentList(
				paymentDateFrom: FilterFrom, 
				paymentDateTo: FilterTo, 
				hasNoDistributedAmount: IsShowAllowRefundOnly ? 1 : (int?)null);
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(task);
			Entities = rows.OrderByDescending(q => q.PaymentDate).ToObservableCollection();
			OneModel = null;
			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		void OnMsgRowChangePayment(MsgRowChange<Payment> msg)
		{
			OnMsgRowChangeCore(new[] { msg.Row.RowId }, true);
		}

		void OnMsgRowChangeRefund(MsgRowChange<Refund> msg)
		{
			var paymentRowIds = msg.Row.PaymentRefunds.Select(q => q.PaymentRowId).ToArray();
			OnMsgRowChangeCore(paymentRowIds, false);
		}

		void OnMsgRowChangeCore(Guid[] paymentRowIds, bool isPaymentSource)
		{
			MessengerHelper.RunAction(this, async () =>
			{
				ShowWaitIndicator.Show();
				foreach (var paymentRowId in paymentRowIds)
				{
					var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetPaymentList("rowId=" + paymentRowId));
					if (rows.Count > 0)
					{
						var row = rows[0];
						var index = Entities.FindIndex(q => q.RowId == paymentRowId);
						if (index >= 0)
						{
							Entities[index] = row;
						}
					}


					if (!isPaymentSource && OneModel?.Entity?.RowId == paymentRowId)
					{
						await OneModel.LoadData(isNew: false, rowId: paymentRowId);
					}
				}

				

				ShowWaitIndicator.Hide();
			});
		}



		bool isCancelOnSelectedEntityChanged;
		public void OnSelectedEntityChanged(Payment oldrow)
		{
			DispatcherUIHelper.Run(async () =>
			{
				if (SelectedEntity?.RowId == oldrow?.RowId)
				{
					return;
				}

				if (isCancelOnSelectedEntityChanged)
				{
					isCancelOnSelectedEntityChanged = false;
					return;
				}
				if (SelectedEntity != null && SelectedEntity.IsNew)
				{
					return;
				}

				if (OneModel != null)
				{
					var ret = await OneModel.OnClose();
					if (ret.IsCancel())
					{
						isCancelOnSelectedEntityChanged = true;
						SelectedEntity = oldrow;
						return;
					}
				}

				ClearAllNewRows();

                if (SelectedEntity != null)
                {
                    OneModel = ViewModelSource.Create<PaymentOneViewModel>();
                    await OneModel.LoadData(false, SelectedEntity.RowId);
                }
                else
                {
                    OneModel = null;
                }
			});
		}

		void ClearAllNewRows()
		{
			Entities.RemoveRange(q => q.IsNew && q != SelectedEntity);
		}

		public void Save()
		{
			DispatcherUIHelper.Run(async () =>
			{
				await SaveCore();
			});
		}


		async Task<bool> SaveCore()
		{
			if (OneModel == null)
			{
				return true;
			}
			return await OneModel.SaveCore(false);
		}

		bool ignoreOnIsShowAllowRefundOnlyChanged;
		public void OnIsShowAllowRefundOnlyChanged(bool oldValue)
		{
			if (ignoreOnIsShowAllowRefundOnlyChanged) return;
			if (IsShowAllowRefundOnly == oldValue) return;

			FilterCore(triggedByIsShowAllowRefundOnly: true);
		}

		public void Filter(string mode) => FilterCore(mode);
		public void FinanceDateApply(FinanceDateClass preset) => FilterCore(null, preset);

		public void FilterCore(string arg = "", FinanceDateClass preset = null, bool triggedByIsShowAllowRefundOnly = false)
		{
			DispatcherUIHelper.Run(async () =>
			{
				if (OneModel != null)
				{
					var ret = await OneModel.OnClose();
					if (ret.IsCancel())
					{
						if (triggedByIsShowAllowRefundOnly)
						{
							ignoreOnIsShowAllowRefundOnlyChanged = true;
							IsShowAllowRefundOnly = !IsShowAllowRefundOnly;
							ignoreOnIsShowAllowRefundOnlyChanged = false;
						}
						return;
					}
				}

				if (preset != null)
				{
					var cret = preset.Get();
					FilterFrom = cret.From;
					FilterTo = cret.To;
				}
				else if (arg == "PreviousMonth")
				{
					var cret = DateTimeHelper.ChangeMonthFromTo(-1, FilterFrom, FilterTo);
					FilterFrom = cret.From;
					FilterTo = cret.To;
				}
				else if (arg == "NextMonth")
				{
					var cret = DateTimeHelper.ChangeMonthFromTo(1, FilterFrom, FilterTo);
					FilterFrom = cret.From;
					FilterTo = cret.To;
				}

				GlobalSettings.Instance.Finances.SetFinancesViewDateFilter(FilterFrom, FilterTo);
				await LoadData();
			});
		}

		public void New()
		{
			DispatcherUIHelper.Run(async () =>
			{
				if (OneModel != null)
				{
					var ret = await OneModel.OnClose();
					if (ret.IsCancel())
					{
						return;
					}
				}

				var ret2 = await PickPatientViewModel.PickPatient(ShowDXWindowsInteractionRequest, PickPatientViewModel.PickModeEnum.PickPatient);
				if (!ret2.IsSuccess) return;
				var patient = ret2.PickPatient;


				var Payment = new Payment
				{
					RowId = Guid.NewGuid(),
					Patient = patient,
					PatientRowId = patient.RowId,
					PaymentDate = DateTime.Today,
					IsNew = true,
				};
				var nrow = new Payment
				{
					RowId = Payment.RowId,
					PatientFullName = patient.FullName,
					PatientRowId = patient.RowId,
					PaymentDate = Payment.PaymentDate,
					IsNew = true,
				};

				OneModel = ViewModelSource.Create<PaymentOneViewModel>();
				await OneModel.LoadData(newPayment: Payment);

				Entities.Insert(0, nrow);
				SelectedEntity = nrow;

				ClearAllNewRows();
			});
		}

		public async void Delete()
		{
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "Payment"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				var row = SelectedEntity;
				ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
				var uret = await businessService.DeletePayment(row.RowId);
				ShowWaitIndicator.Hide();
				if (!uret.Validate(messageBoxService)) return;
				Entities.Remove(row);
			}
		}
		public bool CanDelete() => (SelectedEntity != null);



		public void CustomRowFilter(RowFilterEventArgs e)
		{
			var row = Entities[e.ListSourceRowIndex];
			//System.Diagnostics.Debug.WriteLine("e.ListSourceRowIndex=" + e.ListSourceRowIndex);
			//System.Diagnostics.Debug.WriteLine("row=" + row.PaymentDate);
			if (row.IsNew)
			{
				e.Visible = true;
				e.Handled = true;
			}
		}



		public void RefundNew()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var ret = await OneModel.OnClose();
				if (ret.IsCancel())
				{
					return;
				}

				var row = SelectedEntity;
				var newRefund = new Refund
				{
					RowId = Guid.NewGuid(),
					PatientRowId = row.PatientRowId,
					PatientFullName = row.PatientFullName,
					PaymentDate = DateTime.Today,
					RefundItemsType = TypeHelper.RefundItemsType_Payment,
					IsNew = true,
				};
				var newPaymentRefund = new PaymentRefund
				{
					RowId = Guid.NewGuid(),
					Amount = SelectedEntity.PaymentBalance,
					Amount0 = 0,
					PaymentRowId = row.RowId,
					Payment = row,
					RefundRowId = newRefund.RowId,
					AllocationDate = DateTime.Today,
					IsChanged = true,
					IsNew = true,
				};


				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.RefundWindowView,
					Param = new RefundWindowViewModel.OpenParams
					{
						IsNew = true,
						ReadOnly = false,
						NewRefund = newRefund,
						NewPaymentRefunds = new List<PaymentRefund> { newPaymentRefund },
					},
				});
			});
		}
		public bool CanRefundNew() => (SelectedEntity != null && SelectedEntity.PaymentBalance > 0);

		bool IOnCloseView.OnClose()
		{
			if (OneModel != null && OneModel.HasChange())
			{
				var ret = MessageBoxService.Confirmation("All changes will be discarded. Continue?");
				return (ret == MessageResult.Yes);
			}
			return true;
		}
	}
}	
