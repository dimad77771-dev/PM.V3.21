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
	public class PaychargesListViewModel : ViewModelBase, IOnCloseView
	{
		#region Service
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
		#endregion
		public virtual ObservableCollection<Paycharge> Entities { get; set; }
		public virtual Paycharge SelectedEntity { get; set; }
		public virtual PaychargeOneViewModel OneModel { get; set; }
		public virtual DateTime FilterFrom { get; set; }
		public virtual DateTime FilterTo { get; set; }
		public virtual Boolean IsShowAllowRefchargeOnly { get; set; } = false;
		public virtual ObservableCollection<AccountAgingModel> RibbonSpAccountAgingListItems { get; set; } = AccountAgingInfo.RibbonSpAccountAgingListItems;
		public virtual Int32 RibbonSpAccountAgingListColumnCount { get; set; } = AccountAgingInfo.RibbonSpAccountAgingListColumnCount;



		public PaychargesListViewModel() : base()
		{
			var ret = GlobalSettings.Instance.Finances.GetFinancesViewDateFilter();
			FilterFrom = ret.FilterFrom;
			FilterTo = ret.FilterTo;
		}

		public void OnOpen(string parm)
		{
			OpenParmQuery = parm;
			OpenParms = QueryHelper.ParseString(parm);
			MessengerHelper.Register<MsgRowChange<Paycharge>>(this, OnMsgRowChangePaycharge);
			MessengerHelper.Register<MsgRowChange<Refcharge>>(this, OnMsgRowChangeRefcharge);
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
			var task = BusinessServiceHelprer.GetPaychargeList(
				paychargeDateFrom: FilterFrom,
				paychargeDateTo: FilterTo,
				hasNoDistributedAmount: IsShowAllowRefchargeOnly ? 1 : (int?)null);
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(task);
			Entities = rows.OrderByDescending(q => q.PaychargeDate).ToObservableCollection();
			OneModel = null;
			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		void OnMsgRowChangePaycharge(MsgRowChange<Paycharge> msg)
		{
			OnMsgRowChangeCore(new[] { msg.Row.RowId }, true);
		}

		void OnMsgRowChangeRefcharge(MsgRowChange<Refcharge> msg)
		{
			var paychargeRowIds = msg.Row.PaychargeRefcharges.Select(q => q.PaychargeRowId).ToArray();
			OnMsgRowChangeCore(paychargeRowIds, false);
		}

		void OnMsgRowChangeCore(Guid[] paychargeRowIds, bool isPaychargeSource)
		{
			MessengerHelper.RunAction(this, async () =>
			{
				ShowWaitIndicator.Show();
				foreach (var paychargeRowId in paychargeRowIds)
				{
					var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetPaychargeList("rowId=" + paychargeRowId));
					if (rows.Count > 0)
					{
						var row = rows[0];
						var index = Entities.FindIndex(q => q.RowId == paychargeRowId);
						if (index >= 0)
						{
							Entities[index] = row;
						}
					}


					if (!isPaychargeSource && OneModel?.Entity?.RowId == paychargeRowId)
					{
						await OneModel.LoadData(isNew: false, rowId: paychargeRowId);
					}
				}



				ShowWaitIndicator.Hide();
			});
		}



		bool isCancelOnSelectedEntityChanged;
		public void OnSelectedEntityChanged(Paycharge oldrow)
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
					OneModel = ViewModelSource.Create<PaychargeOneViewModel>();
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

		bool ignoreOnIsShowAllowRefchargeOnlyChanged;
		public void OnIsShowAllowRefchargeOnlyChanged(bool oldValue)
		{
			if (ignoreOnIsShowAllowRefchargeOnlyChanged) return;
			if (IsShowAllowRefchargeOnly == oldValue) return;

			FilterCore(triggedByIsShowAllowRefchargeOnly: true);
		}

		public void Filter(string mode) => FilterCore(mode);
		public void FinanceDateApply(FinanceDateClass preset) => FilterCore(null, preset);

		public void FilterCore(string arg = "", FinanceDateClass preset = null, bool triggedByIsShowAllowRefchargeOnly = false)
		{
			DispatcherUIHelper.Run(async () =>
			{
				if (OneModel != null)
				{
					var ret = await OneModel.OnClose();
					if (ret.IsCancel())
					{
						if (triggedByIsShowAllowRefchargeOnly)
						{
							ignoreOnIsShowAllowRefchargeOnlyChanged = true;
							IsShowAllowRefchargeOnly = !IsShowAllowRefchargeOnly;
							ignoreOnIsShowAllowRefchargeOnlyChanged = false;
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

				var ret2 = await PickChargeoutRecipientsViewModel.PickRow(ShowDXWindowsInteractionRequest);
				if (!ret2.IsSuccess) return;
				var chargeoutRecipient = ret2.PickRow;


				var paycharge = new Paycharge
				{
					RowId = Guid.NewGuid(),
					ChargeoutRecipient = chargeoutRecipient,
					ChargeoutRecipientRowId = chargeoutRecipient.RowId,
					PaychargeDate = DateTime.Today,
					IsNew = true,
				};
				var nrow = new Paycharge
				{
					RowId = paycharge.RowId,
					ChargeoutRecipientName = chargeoutRecipient.FullName,
					ChargeoutRecipientRowId = chargeoutRecipient.RowId,
					PaychargeDate = paycharge.PaychargeDate,
					IsNew = true,
				};

				OneModel = ViewModelSource.Create<PaychargeOneViewModel>();
				await OneModel.LoadData(newPaycharge: paycharge);

				Entities.Insert(0, nrow);
				SelectedEntity = nrow;

				ClearAllNewRows();
			});
		}

		public async void Delete()
		{
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "Paycharge"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				var row = SelectedEntity;
				ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
				var uret = await businessService.DeletePaycharge(row.RowId);
				ShowWaitIndicator.Hide();
				if (!uret.Validate(messageBoxService)) return;
				Entities.Remove(row);
			}
		}
		public bool CanDelete() => (SelectedEntity != null);



		public void CustomRowFilter(RowFilterEventArgs e)
		{
			var row = Entities[e.ListSourceRowIndex];
			if (row.IsNew)
			{
				e.Visible = true;
				e.Handled = true;
			}
		}



		public void RefchargeNew()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var ret = await OneModel.OnClose();
				if (ret.IsCancel())
				{
					return;
				}

				var row = SelectedEntity;
				var newRefcharge = new Refcharge
				{
					RowId = Guid.NewGuid(),
					ChargeoutRecipientRowId = row.ChargeoutRecipientRowId,
					ChargeoutRecipientName = row.ChargeoutRecipientName,
					PaychargeDate = DateTime.Today,
					RefchargeItemsType = TypeHelper.RefchargeItemsType_Paycharge,
					IsNew = true,
				};
				var newPaychargeRefcharge = new PaychargeRefcharge
				{
					RowId = Guid.NewGuid(),
					Amount = SelectedEntity.PaychargeBalance,
					Amount0 = 0,
					PaychargeRowId = row.RowId,
					Paycharge = row,
					RefchargeRowId = newRefcharge.RowId,
					AllocationDate = DateTime.Today,
					IsChanged = true,
					IsNew = true,
				};


				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.RefchargeWindowView,
					Param = new RefchargeWindowViewModel.OpenParams
					{
						IsNew = true,
						ReadOnly = false,
						NewRefcharge = newRefcharge,
						NewPaychargeRefcharges = new List<PaychargeRefcharge> { newPaychargeRefcharge },
					},
				});
			});
		}
		public bool CanRefchargeNew() => (SelectedEntity != null && SelectedEntity.PaychargeBalance > 0);

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
