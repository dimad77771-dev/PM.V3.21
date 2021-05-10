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
using Microsoft.Practices.ServiceLocation;
using DevExpress.Xpf.Core;
using System.Diagnostics;
using Profibiz.PracticeManager.Patients.BusinessService;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class InsuranceArticleInfoViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public GridControlBehaviorManager BehaviorGridConrolOrderItem { get; set; } = new GridControlBehaviorManager();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> OpenWindowInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion
		public OpenParams OpenParam { get; set; }
		public virtual InsuranceArticleInfo Entity { get; set; }
		public virtual ObservableCollection<InsuranceArticleInfo.Row> ItemEntities { get; set; }
		public virtual InsuranceArticleInfo.Row ItemSelectedEntity { get; set; }
		public virtual Boolean IsShowAllYears { get; set; } = false;
		


		public InsuranceArticleInfoViewModel() : base() {}

		public void OnOpen(object arg)
		{
			if (arg is OpenParams)
			{
				OpenParam = (OpenParams)arg;
			}
			DispatcherUIHelper.Run2(LoadData());
		}

		async Task LoadData()
		{
			ShowWaitIndicator.Show();

			var qry = 
				"insuranceCoverageRowId=" + OpenParam.InsuranceCoverageRowId.ToWebQuery() + "&" +
				"patientRowId=" + OpenParam.PatientRowId.ToWebQuery() + "&" +
				"categoryRowId=" + OpenParam.СategoryRowId.ToWebQuery() + "&" + 
				"isShowAllYears=" + IsShowAllYears.ToWebQuery() + "&" +
				"showProblemOnly=" + OpenParam.ShowProblemOnly.ToWebQuery();
			var entity = await businessService.GetInsuranceArticleInfo(qry);
			Entity = entity;
			ItemEntities = Entity.Rows.OrderByDescending(q => q.InvoiceClaim.ApproveDate).ToObservableCollection();

			ItemEntities.ForEach(row =>
			{
				row.OnOpenDetail = () =>
				{
					OpenInvoice(row);
				};
			});

			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		void OpenInvoice(InsuranceArticleInfo.Row row)
		{
			if (row == null) return;

			DispatcherUIHelper.Run(() =>
			{
				OpenWindowInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.InvoiceWindowView,
					Param = new InvoiceWindowViewModel.OpenParams
					{
						IsNew = false,
						RowId = row.Invoice.RowId,
						ReadOnly = true,
					},
				});
			});
		}

		public void OpenRow(InsuranceArticleInfo.Row row) => OpenInvoice(row);


		public void Close() => CloseCore();

		public void OnIsShowAllYearsChanged()
		{
			DispatcherUIHelper.Run2(LoadData());
		}


		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		bool forceClose;
		void CloseCore(bool force = false)
		{
			this.forceClose = force;
			CloseInteractionRequest.Raise(null);
		}



		public class OpenParams
		{
			public Guid InsuranceCoverageRowId { get; set; }
			public Guid PatientRowId { get; set; }
			public Guid СategoryRowId { get; set; }

			public Boolean ShowProblemOnly { get; set; }
		}
	}
}
