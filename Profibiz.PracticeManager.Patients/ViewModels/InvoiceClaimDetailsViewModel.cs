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
using System.Windows.Media;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class InvoiceClaimDetailsViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public GridControlBehaviorManager BehaviorGridConrolOrderItem { get; set; } = new GridControlBehaviorManager();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> OpenWindowInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion
		public virtual OpenParams OpenParam { get; set; }
		public virtual Invoice Invoice { get; set; }
		public virtual InvoiceClaim InvoiceClaim { get; set; }
		public virtual ObservableCollection<InvoiceClaimDetail> Entities { get; set; }
		public virtual InvoiceClaimDetail SelectedEntity { get; set; }
		public virtual InsurancePatientCategoryInfo InsurancePatientCategoryInfo { get; set; } = new InsurancePatientCategoryInfo();
		public virtual Decimal SumLineTotals { get; set; }
		public virtual Decimal DifferenceLineTotals { get; set; }
		public virtual Brush DifferenceLineTotalsForegroundColor => (DifferenceLineTotals == 0 ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.Red));


		public InvoiceClaimDetailsViewModel() : base() { }

		public void OnOpen(object arg)
		{
			OpenParam = (OpenParams)arg;
			DispatcherUIHelper.Run(async () => await LoadData());
		}

		async Task LoadData()
		{
			ShowWaitIndicator.Show();

			Invoice = OpenParam.Invoice;
			InvoiceClaim = OpenParam.InvoiceClaim;

			var entities = InvoiceClaim.InvoiceClaimDetails.GetPocoCloneList();
			Entities = entities.OrderBy(q => LookupDataProvider.MedicalService2Name(q.ServcieOrSupplyRowId)).ToObservableCollection();
			var isEmpty = Entities.Any();
			

			var itemInfoSet = Invoice.InvoiceItems
				.Where(q => q.ServcieOrSupplyRowId != null)
				.GroupBy(q => q.ServcieOrSupplyRowId)
				.Select(q => new
				{
					ServcieOrSupplyRowId = (Guid)q.Key,
					SumLineTotal = q.Sum(z => z.LineTotal ?? 0),
					SumUnits = q.Sum(z => z.Units ?? 0),
				})
				.ToArray();
			foreach(var itemInfo in itemInfoSet)
			{
				if (!Entities.Any(q => q.ServcieOrSupplyRowId == itemInfo.ServcieOrSupplyRowId))
				{
					var nrow = new InvoiceClaimDetail
					{
						RowId = Guid.NewGuid(),
						InvoiceClaimRowId = InvoiceClaim.RowId,
						ServcieOrSupplyRowId = itemInfo.ServcieOrSupplyRowId,
					};
					if (!isEmpty)
					{
						//nrow.Units = itemInfo.SumUnits;
						//nrow.Amount = Math.Round(itemInfo.SumLineTotal / itemInfo.SumUnits, 2);
					}
					Entities.Add(nrow);
				}
			}
			foreach(var row in Entities)
			{
				var itemInfo = itemInfoSet.SingleOrDefault(q => q.ServcieOrSupplyRowId == row.ServcieOrSupplyRowId);
				if (itemInfo != null)
				{
					row.InvoiceItemsUnits = itemInfo.SumUnits;
					row.InvoiceItemsAmount = itemInfo.SumLineTotal;
				}
				else
				{
					row.InvoiceItemsAmount = 0;
					row.InvoiceItemsUnits = 0;
				}
			}

			await LoadInsuranceInfo();
			CalcRowFields();
			Entities.ForEach(q => SubscribeRow(q));
			ResetHasChange();
			RegisterMessenges();

			ShowWaitIndicator.Hide();
		}

		async Task LoadInsuranceInfo()
		{
			var insuranceCoverageRowId = InvoiceClaim.InsuranceCoverageRowId;
			var qry = new[] { insuranceCoverageRowId }.OfType<Guid>().ToArray().ToWebQuery();
			InsurancePatientCategoryInfo = await businessService.GetInsurancePatientCategoryInfo(qry);

			foreach (var row in Entities)
			{
				var servcieOrSupplyRowId = row.ServcieOrSupplyRowId;
				var categoryRowId = LookupDataProvider.MedicalService2CategoryRowId(servcieOrSupplyRowId);
				var info = InsurancePatientCategoryInfo.Find(insuranceCoverageRowId, Invoice.PatientRowId, categoryRowId);
				row.InsuranceInfo = info;
			}
		}

		bool isRegisterMessenges;
		void RegisterMessenges()
		{
			if (!isRegisterMessenges)
			{
				MessengerHelper.Register<MsgRowChange<InsuranceCoverage>>(this, OnMsgRowChangeInsuranceCoverage);
				isRegisterMessenges = true;
			}
		}

		void OnMsgRowChangeInsuranceCoverage(MsgRowChange<InsuranceCoverage> msg)
		{
			MessengerHelper.RunAction(this, () =>
			{
				DispatcherUIHelper.Run2(LoadInsuranceInfo());
			});
		}


		public void Close() => CloseCore();
		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));

		public void Submit() => SaveAndClose();
		public void Cancel() => CloseCore(force: true);


		public void ClearItem()
		{
			SelectedEntity.Units = null;
			SelectedEntity.Amount = 0;
			CalcRowFields();
		}
		public bool CanClearItem() => (SelectedEntity != null);






		bool Validate()
		{
			var errors = new List<string>();

			foreach(var row in Entities)
			{
				if (row.Amount > 0)
				{
					if (LookupDataProvider.MedicalService2IsSupply(row.ServcieOrSupplyRowId))
					{
						if ((row.Units ?? 0) <= 0)
						{
							errors.Add("Qty is required for category type SUPPLY");
						}
					}
				}
			}
			if (errors.Count > 0)
			{
				var err = string.Join("\n", errors.ToArray());
				MessageBoxService.ShowMessage(err, CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
				return false;
			}

			var warnings = Entities
				.Where(q => q.Amount > 0 && !q.InsuranceInfo.IsFind)
				.Select(q => "No Insurance Coverage for item \"" + LookupDataProvider.MedicalService2Name(q.ServcieOrSupplyRowId) + "\"")
				.ToList();
			if (warnings.Count > 0)
			{
				var warning = string.Join("\n", warnings.ToArray());
				var ret = MessageBoxService.Confirmation(warning + "\n\t" + "Continue saving?");
				if (ret != MessageResult.Yes)
				{
					return false;
				}
			}

			return true;
		}

		async Task<bool> SaveCore(bool andClose)
		{
			//validate
			if (!Validate())
			{
				return false;
			}

			//updateEntity
			var updateEntities = Entities.Where(q => q.Amount > 0).GetPocoCloneList();
			InvoiceClaim.InvoiceClaimDetails = updateEntities;
			InvoiceClaim.ApproveAmont = InvoiceClaim.InvoiceClaimDetails.Sum(q => q.Amount);
			OpenParam.IsSuccess = true;

			ResetHasChange();

			//close
			if (andClose)
			{
				CloseCore(force: true);
			}

			return true;
		}






		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		bool forceClose;
		void CloseCore(bool force = false)
		{
			this.forceClose = force;
			CloseInteractionRequest.Raise(null);
		}

		bool HasChange()
		{
			return Entities.Any(q => q.IsChanged);
		}
		void ResetHasChange()
		{
			Entities.ForEach(q => q.IsChanged = false);
		}



		async public Task<bool> OnClose(bool showOKCancel = false)
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
					return false;
				}
				else if (ret == MessageResult.No)
				{
					return true;
				}
				else if (ret == MessageResult.Yes || ret == MessageResult.OK)
				{
					return await SaveCore(andClose: false);
				}
				else throw new ArgumentException();
			}
			else
			{
				return true;
			}
		}
		public async void ClosingEvent(CancelEventArgs arg)
		{
			if (forceClose)
			{
				return;
			}
			if (!await OnClose())
			{
				arg.Cancel = true;
			}
		}


		void SubscribeRow(InvoiceClaimDetail row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) => CalcRowFields();

			row.OnInsuranceOpen = () =>
			{
				OpenWindowInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.InsuranceCoverage2WindowView,
					Param = new InsuranceCoverage2ViewModel.OpenParams
					{
						IsNew = false,
						RowId = (Guid)InvoiceClaim.InsuranceCoverageRowId,
						FamilyMembers = new List<Patient>(),
						HighlightPatientRowId = Invoice.PatientRowId,
						HighlightCategoryRowId = LookupDataProvider.MedicalService2CategoryRowId(row.ServcieOrSupplyRowId),
					},
				});
			};
		}
		void CalcRowFields()
		{
			BehaviorGridConrolOrderItem.UpdateTotalSummary();
			SumLineTotals = Entities.Sum(q => q.Amount);
			DifferenceLineTotals = (InvoiceClaim.ApproveAmont ?? 0) - SumLineTotals;
		}

		public class OpenParams
		{
			public Invoice Invoice { get; set; }
			public InvoiceClaim InvoiceClaim { get; set; }
			public Boolean ReadOnly { get; set; }

			public Boolean IsSuccess { get; set; }
		}
	}
}
