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
	public class WorkInoutsListViewModel : ViewModelBase, ILeftPanelViewModel
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		#endregion
		public virtual ObservableCollection<WorkInout> Entities { get; set; }
		public virtual WorkInout SelectedEntity { get; set; }
		public virtual DateTime FilterFrom { get; set; }
		public virtual DateTime FilterTo { get; set; }

		~WorkInoutsListViewModel()
		{
			NLog.Trace("~WorkInoutsListViewModel=" + this.GetHashCode());
		}


		public WorkInoutsListViewModel() : base()
		{
			NLog.vv(() => "WorkInoutsListViewModel.create=" + this.GetHashCode());
			var ret = GlobalSettings.Instance.InventoryList.Get();
			FilterFrom = ret.FilterFrom;
			FilterTo = ret.FilterTo;
		}

		public void OnOpen(string parm)
		{
			OpenParmQuery = parm;
			OpenParms = QueryHelper.ParseString(parm);
			MessengerHelper.Register<MsgRowChange<WorkInout>>(this, OnMsgRowChange);
			DispatcherUIHelper.Run2(LoadData());
		}
		String OpenParmQuery;
		NameValueCollection OpenParms;



		public async Task LoadData()
		{
			ShowWaitIndicator.Show();

			var query = "workInoutDateFrom" + FilterFrom.ToWebQuery() + "&" + "workInoutDateTo=" + FilterTo.ToWebQuery() + "&";
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetWorkInoutList(query));
			Entities = rows.OrderByDescending(q => q.Start).ToObservableCollection();

			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		void OnMsgRowChange(MsgRowChange<WorkInout> msg)
		{
			MessengerHelper.RunAction(this, async () =>
			{
				ShowWaitIndicator.Show();

				var workInoutRowId = msg.Row.RowId;
				var row = (await businessService.GetWorkInoutList("rowId=" + workInoutRowId)).Single();

				var n = Entities.FindIndex(q => q.RowId == row.RowId);
				if (n == -1)
				{
					Entities.Insert(0, row);
				}
				else
				{
					Entities[n] = row;
				}
				SelectedEntity = row;

				ShowWaitIndicator.Hide();
			});
		}

		public void Delete(WorkInout row)
		{
			DispatcherUIHelper.Run(async () =>
			{
				var messageBoxService = this.GetRequiredService<IMessageBoxService>();
				var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "WorkInout"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
					var uret = await businessService.DeleteWorkInout(row.RowId);
					ShowWaitIndicator.Hide();
					if (!uret.Validate(messageBoxService)) return;
					Entities.Remove(row);
				}
			});
		}
		public bool CanDelete(WorkInout row) => (row != null);


		public void Edit(WorkInout row)
		{
			AddEdit(row);
		}
		public bool CanEdit(WorkInout row) => (row != null);

		public void New()
		{
			AddEdit(null);
		}

		void AddEdit(WorkInout row)
		{
			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.OneWorkInoutView,
				Param = new OneWorkInoutViewModel.OpenParams
				{
					IsNew = (row == null),
					RowId = (row == null ? default(Guid) : row.RowId),
				},
			});
		}


		public void Filter(string mode) => FilterCore(mode);
		public void FinanceDateApply(FinanceDateClass preset) => FilterCore(null, preset);

		public void FilterCore(string arg = "", FinanceDateClass preset = null)
		{
			DispatcherUIHelper.Run(async () =>
			{
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

				GlobalSettings.Instance.InventoryList.Set(FilterFrom, FilterTo);
				await LoadData();
			});
		}

		public void Init() { }
	}
}
