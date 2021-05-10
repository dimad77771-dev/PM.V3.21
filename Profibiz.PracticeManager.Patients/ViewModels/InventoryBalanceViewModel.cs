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
using System.ComponentModel;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class InventoryBalanceViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion
		public virtual ObservableCollection<InventoryBalance> Entities { get; set; }
		public virtual InventoryBalance SelectedEntity { get; set; }
		public virtual Boolean IsAllowCorrection { get; set; }// = true;


		public InventoryBalanceViewModel() : base()
		{
		}

		public void OnOpen(string parm)
		{
			OpenParmQuery = parm;
			OpenParms = QueryHelper.ParseString(parm);
			DispatcherUIHelper.Run(async () => await LoadData());
		}
		String OpenParmQuery;
		NameValueCollection OpenParms;




		async Task LoadData()
		{
			ShowWaitIndicator.Show();
			
			var rows = await GetList("");
			Entities = rows.ToObservableCollection();
			Entities.ForEach(row =>
			{
				row.NewBalance = row.Balance;
				row.TransactionDate = null;
				row.TransactionDescription = null;
				SubscribeRow(row);
			});

			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		

		async Task<List<InventoryBalance>> GetList(string qry)
		{
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetInventoryBalanceList(qry));
			return rows;
		}


		void SetupAllowCorrection()
		{

		}

		void SubscribeRow(InventoryBalance row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(row.NewBalance))
				{
					if (row.TransactionDate == null)
					{
						row.TransactionDate = DateTime.Today;
					}
				}
			};
		}

		public void SaveCorrection()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var messageBoxService = this.GetRequiredService<IMessageBoxService>();
				var rows = Entities.Where(q => q.IsBalanceModify).ToList();
				if (!rows.Any())
				{
					messageBoxService.ShowError("The is no changes in balances");
					return;
				}

				var errorrow = rows.FirstOrDefault(q => q.TransactionDate == null);
				if (errorrow != null)
				{
					SelectedEntity = errorrow;
					messageBoxService.ShowError("Date is required");
					return;
				}

				var txt = "Generate adjustments for balances of:\n" + string.Join("\n", rows.Select(q => "\t" + q.Name).ToArray());
				var ret = messageBoxService.ShowMessage(txt, CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
					var uret = await businessService.PostInventoryBalances(rows);
					ShowWaitIndicator.Hide();
					if (!uret.Validate(messageBoxService)) return;

					IsAllowCorrection = false;
					await LoadData();
				}
			});
		}

	}
}
