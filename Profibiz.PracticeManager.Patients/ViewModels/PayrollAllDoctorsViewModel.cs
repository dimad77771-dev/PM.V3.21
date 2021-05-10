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

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class PayrollAllDoctorsViewModel : ViewModelBase
	{
		#region Service
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
		#endregion
		public virtual ObservableCollection<PayrollInfoResult> Entities { get; set; }
		public virtual PayrollInfoResult SelectedEntity { get; set; }
		public virtual DateTime FilterFrom { get; set;  }
		public virtual DateTime FilterTo { get; set;  }
		//public DateTime PeriodFrom => FilterFrom;
		//public DateTime PeriodTo => FilterTo.AddMonths(1).AddDays(-1);


		public PayrollAllDoctorsViewModel() : base()
		{
			var ret = GlobalSettings.Instance.PayrollAllDoctors.Get();
			FilterFrom = ret.FilterFrom;
			FilterTo = ret.FilterTo;
			NormalizeFilterFromTo();

			MessengerHelper.Register<MsgRowChange<PayrollPayment>>(this, OnMsgRowChange1);
			//MessengerHelper.Register<MsgRowChange<PayrollPayment>>(this, OnMsgRowChange2);
		}

		public void OnOpen(string parm)
		{
			DispatcherUIHelper.Run(async () =>
			{
				await LoadData();
			});
		}




		async Task LoadData()
		{
			ShowWaitIndicator.Show();
			var query = PeriodWebQuery();
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetPayrollInfo(query));
			Entities = rows.OrderBy(q => q.ServiceProviderFullName).ToObservableCollection();
			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}
		String PeriodWebQuery() => "PeriodStart=" + FilterFrom.ToWebQuery() + "&PeriodFinish=" + FilterTo.ToWebQuery();


		public void Filter(string mode) => FilterCore(mode);
		public void FinanceDateApply(FinanceDateClass preset) => FilterCore(null, preset);

		public void FilterCore(string mode = null, FinanceDateClass preset = null)
		{
			DispatcherUIHelper.Run(async () =>
			{
				if (preset != null)
				{
					var cret = preset.Get();
					FilterFrom = cret.From;
					FilterTo = cret.To;
				}
				else if (mode == "PreviousMonth")
				{
					FilterFrom = FilterFrom.AddMonths(-1);
					FilterTo = FilterTo.AddMonths(-1);
				}
				else if (mode == "NextMonth")
				{
					FilterFrom = FilterFrom.AddMonths(1);
					FilterTo = FilterTo.AddMonths(1);
				}

				NormalizeFilterFromTo();
				GlobalSettings.Instance.PayrollAllDoctors.Set(FilterFrom, FilterTo);
				await LoadData();
			});
		}

		void NormalizeFilterFromTo()
		{
			FilterFrom = FilterFrom.FirstDayMonth();
			FilterTo = FilterTo.LastDayMonth();
		}

		public void MouseDoubleClick(PayrollInfoResult row) => ShowDetail(row);
		public void OpenDetails() => ShowDetail(SelectedEntity);
		public bool CanOpenDetails() => (SelectedEntity != null);

		
		


		public void ShowDetail(PayrollInfoResult row)
		{
			DispatcherUIHelper.Run(() =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.PayrollDetailWindowView,
					Param = new PayrollDetailViewModel.OpenParams
					{
						PayrollInfoResult = row,
					},
				});
			});
		}


		public void OpenPayrollPayments()
		{
			DispatcherUIHelper.Run(() =>
			{
				var row = SelectedEntity;
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.PayrollPaymentListWindowView,
					Param = new PayrollPaymentListViewModel.OpenParams
					{
						ServiceProviderRowId = row.ServiceProviderRowId,
					},
				});
			});
		}
		public bool CanOpenPayrollPayments() => (SelectedEntity != null);

		public void AddPayrollPayment()
		{
			DispatcherUIHelper.Run(() =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.PayrollPaymentOneWindowView,
					Param = new PayrollPaymentOneViewModel.OpenParams
					{
						IsNew = true,
						ServiceProviderRowId = SelectedEntity.ServiceProviderRowId,
						NewRowInfo = SelectedEntity,
						ForNewPeriodStart = SelectedEntity.PeriodStart,
						ForNewPeriodFinish = SelectedEntity.PeriodFinish,
					},
				});
			});
		}
		public bool CanAddPayrollPayment() => (SelectedEntity != null);



		void OnMsgRowChange1(MsgRowChange<PayrollPayment> msg)
		{
			MessengerHelper.RunAction(this, async () =>
			{
				ShowWaitIndicator.Show();
				var serviceProviderRowId = msg.Row.ServiceProviderRowId;
				var rows = await businessService.GetPayrollInfo(PeriodWebQuery() + "&ServiceProviderRowId=" + serviceProviderRowId.ToWebQuery());
				var row = rows[0];
				var index = Entities.FindIndex(q => q.ServiceProviderRowId == serviceProviderRowId);
				if (index >= 0)
				{
					Entities[index] = row;
				}
				ShowWaitIndicator.Hide();
			});
		}

	}
}	
