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
	public class InventoryListViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion
		public virtual ObservableCollection<Inventory> Entities { get; set; }
		public virtual Inventory SelectedEntity { get; set; }
		public virtual DateTime FilterFrom { get; set; }
		public virtual DateTime FilterTo { get; set; }


		public InventoryListViewModel() : base()
		{
			var ret = GlobalSettings.Instance.InventoryList.Get();
			FilterFrom = ret.FilterFrom;
			FilterTo = ret.FilterTo;
		}

		public void OnOpen(string parm)
		{
			OpenParmQuery = parm;
			OpenParms = QueryHelper.ParseString(parm);
			DispatcherUIHelper.Run(async () => await LoadData());
			MessengerHelper.Register<MsgRowChange<Inventory>>(this, OnMsgRowChange);
		}
		String OpenParmQuery;
		NameValueCollection OpenParms;




		async Task LoadData()
		{
			ShowWaitIndicator.Show();
			
			var rows = await GetInventoryList("");
			Entities = rows.ToObservableCollection();

			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();

			RegisterMessenger();
		}

		void OnMsgRowChange(MsgRowChange<Inventory> msg)
		{
			MessengerHelper.RunAction(this, () =>
			{
				MessengerHelper.UpdateEntities(this, Entities, msg.Row, msg.RowAction, (a, b) => a.RowId == b.RowId, () => SelectedEntity);
			});
		}


		public async void Delete()
		{
			var row = SelectedEntity;
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			if (row.Rowtype == Inventory.TYPE_ORDER)
			{
				var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "Order"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
					var orderRowId = (Guid)row.OrderRowId;
					var uret = await businessService.DeleteOrder(orderRowId);
					ShowWaitIndicator.Hide();
					if (!uret.Validate(messageBoxService)) return;

					OnMsgRowChangeCore(new[] { orderRowId });
				}
			}
			else if (row.Rowtype == Inventory.TYPE_CORRECTION)
			{
				var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "Correction Data"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
					var inventoryRowId = row.RowId;
					var uret = await businessService.DeleteInventory(inventoryRowId);
					ShowWaitIndicator.Hide();
					if (!uret.Validate(messageBoxService)) return;

					Entities.Remove(row);
				}
			}
		}
		public bool CanDelete() => (SelectedEntity != null && SelectedEntity.Rowtype.In(Inventory.TYPE_ORDER, Inventory.TYPE_CORRECTION));


		
		public void Edit()
		{
			AddEdit(SelectedEntity);
		}
		public bool CanEdit() => (SelectedEntity != null && SelectedEntity.Rowtype.In(Inventory.TYPE_ORDER, Inventory.TYPE_INVOICE));

		public void New()
		{
			AddEdit(null);
		}

		void AddEdit(Inventory row)
		{
			DispatcherUIHelper.Run(() =>
			{
				if (row == null || row.Rowtype == Inventory.TYPE_ORDER)
				{
					var isnew = (row == null);
					var param = new OneOrderViewModel.OpenParams
					{
						IsNew = isnew,
						RowId = (isnew ? default(Guid) : (Guid)row.OrderRowId),
					};
					ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
					{
						ViewCode = ViewCodes.OneOrderView,
						Param = param,
					});
				}
				else if (row.Rowtype == Inventory.TYPE_INVOICE)
				{
					ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
					{
						ViewCode = ViewCodes.InvoiceWindowView,
						Param = new InvoiceWindowViewModel.OpenParams
						{
							IsNew = false,
							RowId = (Guid)row.InvoiceRowId,
							ReadOnly = true,
						},
					});
				}
			});
		}



		bool isRegisterMessenger;
		void RegisterMessenger()
		{
			if (!isRegisterMessenger)
			{
				MessengerHelper.Register<MsgRowChange<Order>>(this, OnMsgRowChangeOrder);
				

				//BehaviorGridConrol.OnFilterSortGroupChange(UpdateIsAlternateRow);
				//SubsribeAdvancedFilters();
				//SubsribeIsMultirowSelectionChanged();

				isRegisterMessenger = true;
			}
		}

		void OnMsgRowChangeOrder(MsgRowChange<Order> msg)
		{
			OnMsgRowChangeCore(new[] { msg.Row.RowId });
		}

		void OnMsgRowChangeCore(Guid[] orderRowIds)
		{
			MessengerHelper.RunAction(this, async () =>
			{
				ShowWaitIndicator.Show();

				foreach (var orderRowId in orderRowIds)
				{
					var rows = await GetInventoryList("orderRowId=" + orderRowId);

					var n = Entities.FindIndex(q => q.OrderRowId == orderRowId);
					if (n == -1)
					{
						n = 0;
					}
					Entities.RemoveRange(q => q.OrderRowId == orderRowId);
					Entities.InsertRange(n, rows);
					SelectedEntity = Entities[n];
				}

				ShowWaitIndicator.Hide();
			});
		}

		async Task<List<Inventory>> GetInventoryList(string qry)
		{
			var query = "transactionDateFrom=" + FilterFrom.ToWebQuery() + "&" + "transactionDateTo=" + FilterTo.ToWebQuery();
			if (!string.IsNullOrEmpty(qry))
			{
				query += "&" + qry;
			}
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetInventoryList(query));
			return rows;
		}

		public void Filter(string mode) => FilterCore(mode);
		public void FinanceDateApply(FinanceDateClass preset) => FilterCore(null, preset);

		void FilterCore(string mode = null, FinanceDateClass preset = null)
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
					var cret = DateTimeHelper.ChangeMonthFromTo(-1, FilterFrom, FilterTo);
					FilterFrom = cret.From;
					FilterTo = cret.To;
				}
				else if (mode == "NextMonth")
				{
					var cret = DateTimeHelper.ChangeMonthFromTo(1, FilterFrom, FilterTo);
					FilterFrom = cret.From;
					FilterTo = cret.To;
				}

				GlobalSettings.Instance.InventoryList.Set(FilterFrom, FilterTo);
				await LoadData();
			});
		}

	}
}
