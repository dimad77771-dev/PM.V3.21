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
	public class RefundsListViewModel : ViewModelBase
	{
		#region Service
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
		#endregion
		public virtual ObservableCollection<Refund> Entities { get; set; }
		public virtual Refund SelectedEntity { get; set; }
		public virtual RefundOneViewModel OneModel { get; set; }
		public virtual DateTime FilterFrom { get; set; }
		public virtual DateTime FilterTo { get; set; }
		public virtual ObservableCollection<AccountAgingModel> RibbonSpAccountAgingListItems { get; set; } = AccountAgingInfo.RibbonSpAccountAgingListItems;
		public virtual Int32 RibbonSpAccountAgingListColumnCount { get; set; } = AccountAgingInfo.RibbonSpAccountAgingListColumnCount;



		public RefundsListViewModel() : base()
		{
			var ret = GlobalSettings.Instance.Finances.GetFinancesViewDateFilter();
			FilterFrom = ret.FilterFrom;
			FilterTo = ret.FilterTo;
		}

		public void OnOpen(string parm)
		{
			OpenParmQuery = parm;
			OpenParms = QueryHelper.ParseString(parm);
			MessengerHelper.Register<MsgRowChange<Refund>>(this, OnMsgRowChange);
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
			var task = BusinessServiceHelprer.GetRefundList(paymentDateFrom: FilterFrom, paymentDateTo: FilterTo);
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(task);
			Entities = rows.OrderByDescending(q => q.PaymentDate).ToObservableCollection();
			OneModel = null;
			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		void OnMsgRowChange(MsgRowChange<Refund> msg)
		{
			MessengerHelper.RunAction(this, async () =>
			{
				ShowWaitIndicator.Show();
				var RefundRowId = msg.Row.RowId;
				var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetRefundList("rowId=" + RefundRowId));
				if (rows.Count > 0)
				{
					var row = rows[0];
					var index = Entities.FindIndex(q => q.RowId == RefundRowId);
					if (index >= 0)
					{
						Entities[index] = row;
					}
				}
				ShowWaitIndicator.Hide();
			});
		}

		bool isCancelOnSelectedEntityChanged;
		public void OnSelectedEntityChanged(Refund oldrow)
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
                    OneModel = CreateOneModel();
                    await OneModel.LoadData(false, SelectedEntity.RowId);
                }
                else
                {
                    OneModel = null;
                }
			});
		}

		RefundOneViewModel CreateOneModel()
		{
			return RefundOneViewModel.Create(isWindowMode: false);
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

		public void Filter(string mode) => FilterCore(mode);
		public void FinanceDateApply(FinanceDateClass preset) => FilterCore(null, preset);

		public void FilterCore(string arg, FinanceDateClass preset = null)
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

		//public void New()
		//{
		//	DispatcherUIHelper.Run(async () =>
		//	{
		//		if (OneModel != null)
		//		{
		//			var ret = await OneModel.OnClose();
		//			if (ret.IsCancel())
		//			{
		//				return;
		//			}
		//		}

		//		var ret2 = await PickPatientViewModel.PickPatient(ShowDXWindowsInteractionRequest, PickPatientViewModel.PickModeEnum.PickPatient);
		//		if (!ret2.IsSuccess) return;
		//		var patient = ret2.PickPatient;


		//		var Refund = new Refund
		//		{
		//			RowId = Guid.NewGuid(),
		//			Patient = patient,
		//			PatientRowId = patient.RowId,
		//			PaymentDate = DateTime.Today,
		//			IsNew = true,
		//		};
		//		var nrow = new Refund
		//		{
		//			RowId = Refund.RowId,
		//			PatientFullName = patient.FullName,
		//			PatientRowId = patient.RowId,
		//			PaymentDate = Refund.PaymentDate,
		//			IsNew = true,
		//		};

		//		OneModel = CreateOneModel();
		//		await OneModel.LoadData(newRefund: Refund);

		//		Entities.Insert(0, nrow);
		//		SelectedEntity = nrow;

		//		ClearAllNewRows();
		//	});
		//}

		public async void Delete()
		{
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "Refund"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				var row = SelectedEntity;
				ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
				var uret = await businessService.DeleteRefund(row.RowId);
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

	}
}	
