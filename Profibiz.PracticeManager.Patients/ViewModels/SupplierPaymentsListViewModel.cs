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
	public class SupplierPaymentsListViewModel : ViewModelBase
	{
		#region Service
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
		#endregion
		public virtual ObservableCollection<SupplierPayment> Entities { get; set; }
		public virtual SupplierPayment SelectedEntity { get; set; }
		public virtual SupplierPaymentOneViewModel OneModel { get; set; }
		public virtual DateTime FilterFrom { get; set; }
		public virtual DateTime FilterTo { get; set; }
		public virtual Boolean IsShowAllowSupplierRefundOnly { get; set; } = false;
		public virtual ObservableCollection<AccountAgingModel> RibbonSpAccountAgingListItems { get; set; } = AccountAgingInfo.RibbonSpAccountAgingListItems;
		public virtual Int32 RibbonSpAccountAgingListColumnCount { get; set; } = AccountAgingInfo.RibbonSpAccountAgingListColumnCount;



		public SupplierPaymentsListViewModel() : base()
		{
			var ret = GlobalSettings.Instance.Finances.GetFinancesViewDateFilter();
			FilterFrom = ret.FilterFrom;
			FilterTo = ret.FilterTo;
		}

		public void OnOpen(string parm)
		{
			OpenParmQuery = parm;
			OpenParms = QueryHelper.ParseString(parm);
			MessengerHelper.Register<MsgRowChange<SupplierPayment>>(this, OnMsgRowChangeSupplierPayment);
			MessengerHelper.Register<MsgRowChange<SupplierRefund>>(this, OnMsgRowChangeSupplierRefund);
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
			var task = BusinessServiceHelprer.GetSupplierPaymentList(
				paymentDateFrom: FilterFrom,
				paymentDateTo: FilterTo,
				hasNoDistributedAmount: IsShowAllowSupplierRefundOnly ? 1 : (int?)null);
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(task);
			Entities = rows.OrderByDescending(q => q.SupplierPaymentDate).ToObservableCollection();
			OneModel = null;
			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		void OnMsgRowChangeSupplierPayment(MsgRowChange<SupplierPayment> msg)
		{
			OnMsgRowChangeCore(new[] { msg.Row.RowId }, true);
		}

		void OnMsgRowChangeSupplierRefund(MsgRowChange<SupplierRefund> msg)
		{
			var paymentRowIds = msg.Row.SupplierPaymentRefunds.Select(q => q.SupplierPaymentRowId).ToArray();
			OnMsgRowChangeCore(paymentRowIds, false);
		}

		void OnMsgRowChangeCore(Guid[] paymentRowIds, bool isSupplierPaymentSource)
		{
			MessengerHelper.RunAction(this, async () =>
			{
				ShowWaitIndicator.Show();
				foreach (var paymentRowId in paymentRowIds)
				{
					var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetSupplierPaymentList("rowId=" + paymentRowId));
					if (rows.Count > 0)
					{
						var row = rows[0];
						var index = Entities.FindIndex(q => q.RowId == paymentRowId);
						if (index >= 0)
						{
							Entities[index] = row;
						}
					}


					if (!isSupplierPaymentSource && OneModel?.Entity?.RowId == paymentRowId)
					{
						await OneModel.LoadData(isNew: false, rowId: paymentRowId);
					}
				}



				ShowWaitIndicator.Hide();
			});
		}



		bool isCancelOnSelectedEntityChanged;
		public void OnSelectedEntityChanged(SupplierPayment oldrow)
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
					OneModel = ViewModelSource.Create<SupplierPaymentOneViewModel>();
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

		bool ignoreOnIsShowAllowSupplierRefundOnlyChanged;
		public void OnIsShowAllowSupplierRefundOnlyChanged(bool oldValue)
		{
			if (ignoreOnIsShowAllowSupplierRefundOnlyChanged) return;
			if (IsShowAllowSupplierRefundOnly == oldValue) return;

			FilterCore(triggedByIsShowAllowSupplierRefundOnly: true);
		}

		public void Filter(string mode) => FilterCore(mode);
		public void FinanceDateApply(FinanceDateClass preset) => FilterCore(null, preset);

		public void FilterCore(string arg = "", FinanceDateClass preset = null, bool triggedByIsShowAllowSupplierRefundOnly = false)
		{
			DispatcherUIHelper.Run(async () =>
			{
				if (OneModel != null)
				{
					var ret = await OneModel.OnClose();
					if (ret.IsCancel())
					{
						if (triggedByIsShowAllowSupplierRefundOnly)
						{
							ignoreOnIsShowAllowSupplierRefundOnlyChanged = true;
							IsShowAllowSupplierRefundOnly = !IsShowAllowSupplierRefundOnly;
							ignoreOnIsShowAllowSupplierRefundOnlyChanged = false;
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

				var ret2 = await PickSupplierViewModel.Pick(new PickSupplierViewModel.OpenParams
				{
					ShowDXWindowsInteractionRequest = ShowDXWindowsInteractionRequest,
					PickMode = PickSupplierViewModel.PickModeEnum.SingleSelect,
				});
				if (!ret2.IsSuccess) return;
				var supplier = ret2.Row;


				var SupplierPayment = new SupplierPayment
				{
					RowId = Guid.NewGuid(),
					Supplier = supplier,
					SupplierRowId = supplier.RowId,
					SupplierPaymentDate = DateTime.Today,
					IsNew = true,
				};
				var nrow = new SupplierPayment
				{
					RowId = SupplierPayment.RowId,
					SupplierRowId = supplier.RowId,
					SupplierPaymentDate = SupplierPayment.SupplierPaymentDate,
					IsNew = true,
				};

				OneModel = ViewModelSource.Create<SupplierPaymentOneViewModel>();
				await OneModel.LoadData(newSupplierPayment: SupplierPayment);

				Entities.Insert(0, nrow);
				SelectedEntity = nrow;

				ClearAllNewRows();
			});
		}

		public async void Delete()
		{
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "SupplierPayment"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				var row = SelectedEntity;
				ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
				var uret = await businessService.DeleteSupplierPayment(row.RowId);
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
			//System.Diagnostics.Debug.WriteLine("row=" + row.SupplierPaymentDate);
			if (row.IsNew)
			{
				e.Visible = true;
				e.Handled = true;
			}
		}



		public void SupplierRefundNew()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var ret = await OneModel.OnClose();
				if (ret.IsCancel())
				{
					return;
				}

				var row = SelectedEntity;
				var newSupplierRefund = new SupplierRefund
				{
					RowId = Guid.NewGuid(),
					SupplierRowId = row.SupplierRowId,
					SupplierFullName = row.SupplierFullName,
					SupplierPaymentDate = DateTime.Today,
					SupplierRefundItemsType = TypeHelper.SupplierRefundItemsType_SupplierPayment,
					IsNew = true,
				};
				var newSupplierPaymentRefund = new SupplierPaymentRefund
				{
					RowId = Guid.NewGuid(),
					Amount = SelectedEntity.SupplierPaymentBalance,
					Amount0 = 0,
					SupplierPaymentRowId = row.RowId,
					SupplierPayment = row,
					SupplierRefundRowId = newSupplierRefund.RowId,
					AllocationDate = DateTime.Today,
					IsChanged = true,
					IsNew = true,
				};


				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.SupplierRefundWindowView,
					Param = new SupplierRefundWindowViewModel.OpenParams
					{
						IsNew = true,
						ReadOnly = false,
						NewSupplierRefund = newSupplierRefund,
						NewSupplierPaymentRefunds = new List<SupplierPaymentRefund> { newSupplierPaymentRefund },
					},
				});
			});
		}
		public bool CanSupplierRefundNew() => (SelectedEntity != null && SelectedEntity.SupplierPaymentBalance > 0);

	}
}
