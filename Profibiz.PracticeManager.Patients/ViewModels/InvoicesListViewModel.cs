using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
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
using PropertyChanged;
using DevExpress.Xpf.Grid;
using DevExpress.Data;
using System.Linq.Dynamic;
using System.Windows.Data;
using System.Globalization;
using System.ComponentModel;
using DevExpress.Xpf.Printing;
using System.IO;
using DevExpress.XtraPrinting;
using DevExpress.Xpf.Spreadsheet;
using DevExpress.Spreadsheet;
using System.Windows.Media;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class InvoicesListViewModel : ViewModelBase
	{
		#region Service
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
		public virtual GridControlBehaviorManager BehaviorGridConrolForFilter { get; set; } = new GridControlBehaviorManager();
		public GridControl GridControl => BehaviorGridConrol.Control;
		public GridControl GridControlForFilter => BehaviorGridConrolForFilter.Control;
		//public SpreadsheetControl aa11;
		#endregion
		public virtual ObservableCollection<RowModel> RowModels { get; set; }
		public virtual RowModel SelectedRowModel { get; set; }
		public virtual Invoice CurrentInvoice => SelectedRowModel?.Invoice;

		//public virtual ObservableCollection<Invoice> Entities { get; set; }
		public virtual Invoice SelectedEntity { get; set; }
		public virtual InvoiceOneViewModel OneModel { get; set; }
		public virtual DateTime FilterFrom { get; set; }
		public virtual DateTime FilterTo { get; set; }
		public virtual InvoicesFilterDateMode FilterDateMode { get; set; }
		public virtual Boolean IsShowAllowRefundOnly { get; set; } = false;
		public virtual ObservableCollection<AccountAgingModel> RibbonSpAccountAgingListItems { get; set; } = AccountAgingInfo.RibbonSpAccountAgingListItems;
		public virtual ObservableCollection<InvoiceListBackgroundColorModel> RibbonInvoiceListBackgroundColorListItems { get; set; } = InvoiceListBackgroundColorInfo.RibbonInvoiceListBackgroundColorListItems;
		public virtual Int32 RibbonSpAccountAgingListColumnCount { get; set; } = AccountAgingInfo.RibbonSpAccountAgingListColumnCount;
		public virtual Boolean IsRightPanelVisible => (ViewMode == ViewModes.Main);
		public virtual Boolean IsMainRibbonShow { get; set; }
		public virtual Boolean IsVisibleServiceProvidersList => true;
		public virtual Boolean IsVisibleMedicalServicesList => true;
		public virtual Boolean IsVisibleCategoriesList => true;
		public virtual Boolean IsFullListMode => (ViewMode == ViewModes.Main);
		public virtual Boolean IsVisibleInvoiceClaimForms => (ViewMode != ViewModes.PickRows);
		public virtual Boolean IsShowIsSelectedColumn => (ViewMode == ViewModes.PickRows);
		public virtual Boolean IsEnabledItalicFormatConditionHasApprovedAmount => (ViewMode == ViewModes.PickRows);

		public virtual bool? IsSelectedAllRows { get; set; } = null;
		public virtual bool IsVisibleSelectedAllRowsCheckbox { get; set; } = true;
		public virtual ObservableCollection<Invoice> SelectedInvoices { get; set; } = new ObservableCollection<Invoice>();
		public virtual bool IsMultirowSelection { get; set; } = false;
		

		public virtual ObservableCollection<Referrer> AllReferrers { get; set; } = LookupDataProvider.Instance.Referrers;
		public virtual Referrer SelectedReferrer { get; set; }
		public virtual ObservableCollection<InsuranceProvider> AllInsuranceProviders { get; set; } = LookupDataProvider.Instance.InsuranceProviders;
		public virtual InsuranceProvider SelectedInsuranceProvider { get; set; }
		public virtual Boolean IsShowSentOnly { get; set; }
		public virtual Boolean IsShowPaidOnly { get; set; }
		public virtual Boolean IsCoordinationProblemOnly { get; set; }





		public virtual List<Invoice> PreloadedRows { get; set; }
		public virtual Guid? OnePatientRowId { get; set; }
		public virtual ViewModes ViewMode { get; set; }
		public enum ViewModes { Main, OnePatient, PickRows }

		public InvoicesListViewModel() : base()
		{
			var ret = GlobalSettings.Instance.Finances.GetFinancesViewDateFilter();
			FilterFrom = ret.FilterFrom;
			FilterTo = ret.FilterTo;
			FilterDateMode = (InvoicesFilterDateMode)ret.InvoicesFilterDateMode;
		}

		public void OnOpen(string parm)
		{
			OpenParmQuery = parm;
			OpenParms = QueryHelper.ParseString(parm);
			IsMainRibbonShow = (ViewMode == ViewModes.Main);
			DispatcherUIHelper.Run(async () =>
			{
				await LoadData();
			});

		}
		String OpenParmQuery;
		NameValueCollection OpenParms;



		async public Task LoadData()
		{
			ShowWaitIndicator.IsEnabled = (ViewMode != ViewModes.OnePatient);
			ShowWaitIndicator.Show();

			List<Invoice> rows;
			if (PreloadedRows != null)
			{
				rows = PreloadedRows;
			}
			else
			{
				var query = "";
				if (FilterDateMode == InvoicesFilterDateMode.InvoiceDate)
				{
					query = "invoiceDateFrom=" + FilterFrom.ToWebQuery() + "&" + "invoiceDateTo=" + FilterTo.ToWebQuery();
				}
				else if (FilterDateMode == InvoicesFilterDateMode.GeneratedDate)
				{
					query = "createdDateFrom=" + FilterFrom.ToWebQuery() + "&" + "createdDateTo=" + FilterTo.ToWebQuery();
				}
				else throw new ArgumentException();

				if (OnePatientRowId != null)
				{
					query += "&" + "patientRowId=" + OnePatientRowId + "&" + "useFamilyHead=" + true;
				}
				if (IsShowAllowRefundOnly)
				{
					query += "&" + "negativeBalanceOnly=" + IsShowAllowRefundOnly.ToWebQuery();
				}
				if (IsShowSentOnly)
				{
					query += "&" + "isShowSentOnly=" + IsShowSentOnly.ToWebQuery();
				}
				if (IsCoordinationProblemOnly)
				{
					query += "&" + "isCoordinationProblemOnly=" + IsCoordinationProblemOnly.ToWebQuery();
				}
				if (IsShowPaidOnly)
				{
					query += "&" + "isShowPaidOnly=" + IsShowPaidOnly.ToWebQuery();
				}
				if (SelectedReferrer != null)
				{
					query += "&" + "referrerRowId=" + SelectedReferrer.RowId.ToWebQuery();
				}
				if (SelectedInsuranceProvider != null)
				{
					query += "&" + "insuranceProviderRowId=" + SelectedInsuranceProvider.RowId.ToWebQuery();
				}
				rows = await GetInvoiceList(query);
			}
			//Entities = rows.OrderByDescending(q => q.InvoiceDate).ToObservableCollection();
			//for (int i = 0; i < 6; i++)
			//{
			//	rows.AddRange(rows.ToArray());
			//}
			//ProcessAdvancedFilters(rows);
			RowModels = rows.OrderByDescending(q => q.InvoiceDate).SelectMany(q => InvoiceV2RowModels(q)).ToObservableCollection();
			OneModel = null;

			ResetFilterInfo();
			UpdateIsSelectedAllRows();
			//SelectedEntity = Entities.SingleOrDefault();
			//await Task.Delay(3000);
			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();

			RegisterMessenger();
			UpdateIsAlternateRow();
		}

		void ReloadData()
		{
			DispatcherUIHelper.Run(async () => await LoadData());
		}

		bool isRegisterMessenger;
		void RegisterMessenger()
		{
			if (!isRegisterMessenger)
			{
				MessengerHelper.Register<MsgRowChange<Invoice>>(this, OnMsgRowChangeInvoice);
				MessengerHelper.Register<MsgRowChange<Refund>>(this, OnMsgRowChangeRefund);

				BehaviorGridConrol.OnFilterSortGroupChange(UpdateIsAlternateRow);
				SubsribeAdvancedFilters();
				SubsribeIsMultirowSelectionChanged();

				isRegisterMessenger = true;
			}
		}

		void OnMsgRowChangeCore(Guid[] invoiceRowIds)
		{
			MessengerHelper.RunAction(this, async () =>
			{
				ShowWaitIndicator.Show();

				foreach (var invoiceRowId in invoiceRowIds)
				{
					var rows = await GetInvoiceList("rowId=" + invoiceRowId);

					var n = RowModels.FindIndex(q => q.Invoice.RowId == invoiceRowId);
					if (n == -1)
					{
						n = 0;
					}
					RowModels.RemoveRange(q => q.Invoice.RowId == invoiceRowId);
					var models = rows.SelectMany(q => InvoiceV2RowModels(q));
					RowModels.InsertRange(n, models);
					SelectedRowModel = RowModels[n];
				}

				UpdateIsAlternateRow();
				ShowWaitIndicator.Hide();
			});
		}

		void OnMsgRowChangeInvoice(MsgRowChange<Invoice> msg)
		{
			OnMsgRowChangeCore(new[] { msg.Row.RowId });
		}

		void OnMsgRowChangeRefund(MsgRowChange<Refund> msg)
		{
			var invoiceRowIds = msg.Row.InvoiceRefunds.Select(q => q.InvoiceRowId).Distinct().ToArray();
			OnMsgRowChangeCore(invoiceRowIds);
		}

		async Task<List<Invoice>> GetInvoiceList(string qry)
		{
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetInvoiceList(qry + "&includeInvoiceClaims=true"));
			return rows;
		}

		async Task<List<RowModel>> GetRowModelList(string qry)
		{
			var rows = await GetInvoiceList(qry);
			var models = rows.SelectMany(q => InvoiceV2RowModels(q)).ToList();
			return models;
		}



		//bool isCancelOnSelectedEntityChanged;
		//public void OnSelectedEntityChanged(Invoice oldrow)
		//{
		//	return;

		//	if (!IsRightPanelVisible)
		//	{
		//		return;
		//	}

		//	DispatcherUIHelper.Run(async () =>
		//	{
		//		if (SelectedEntity?.RowId == oldrow?.RowId)
		//		{
		//			return;
		//		}

		//		if (isCancelOnSelectedEntityChanged)
		//		{
		//			isCancelOnSelectedEntityChanged = false;
		//			return;
		//		}
		//		if (SelectedEntity != null && SelectedEntity.IsNew)
		//		{
		//			return;
		//		}

		//		if (OneModel != null)
		//		{
		//			var ret = await OneModel.OnClose();
		//			if (ret.IsCancel())
		//			{
		//				isCancelOnSelectedEntityChanged = true;
		//				SelectedEntity = oldrow;
		//				return;
		//			}
		//		}

		//		ClearAllNewRows();

		//		if (SelectedEntity != null)
		//		{
		//			OneModel = InvoiceOneViewModel.Create(isWindowMode: false);
		//			await OneModel.LoadData(false, SelectedEntity.RowId);
		//		}
		//		else
		//		{
		//			OneModel = null;
		//		}
		//	});
		//}

		//void ClearAllNewRows()
		//{
		//	Entities.RemoveRange(q => q.IsNew && q != SelectedEntity);
		//}

		public void Save()
		{
			DispatcherUIHelper.Run(async () =>
			{
				if (OneModel != null)
				{
					await SaveCore();
				}
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


		public void OnIsShowAllowRefundOnlyChanged()
		{
			FilterCore();
		}
		

		public void FilterCore(string mode = null, FinanceDateClass preset = null)
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

				GlobalSettings.Instance.Finances.SetFinancesViewDateFilter(FilterFrom, FilterTo, (int)FilterDateMode);
				await LoadData();
			});
		}



		void AddEdit(Invoice row = null, string newInvoiceType = null)
		{
			DispatcherUIHelper.Run(async () =>
			{
				var isnew = (row == null);

				Invoice invoice = null;
				if (isnew)
				{
					var ret2 = await PickPatientViewModel.PickPatient(ShowDXWindowsInteractionRequest, PickPatientViewModel.PickModeEnum.PickPatient);
					if (!ret2.IsSuccess) return;
					var patient = ret2.PickPatient;
					var hasNoCoverage = patient.HasNoCoverage;

					invoice = new Invoice
					{
						RowId = Guid.NewGuid(),
						Patient = patient,
						PatientFullName = patient.FullName,
						PatientRowId = patient.RowId,
						InvoiceDate = DateTime.Today,
						InvoiceType = newInvoiceType,
						PrintTemplate = Invoice.InvoiceType2DefaultPrintTemplate(newInvoiceType),
						InvoiceNumber = "",
						Rate = patient.Rate,
						HasNoCoverage = hasNoCoverage,
						Created = DateTimeHelper.Now,
					};
				}



				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.InvoiceWindowView,
					Param = new InvoiceWindowViewModel.OpenParams
					{
						IsNew = isnew,
						RowId = (isnew ? default(Guid) : row.RowId),
						//ReadOnly = (!IsFullListMode),
						ReadOnly = false,
						NewInvoice = invoice,
					},
				});
			});
		}

		public void Edit()
		{
			AddEdit(CurrentInvoice);
		}
		public bool CanEdit() => (CurrentInvoice != null);




		public void New(string invoiceType)
		{
			AddEdit(null, invoiceType);
		}

		public async void Delete()
		{
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();

			Invoice[] deleteInvoices;
			if (IsMultirowSelection)
			{
				deleteInvoices = RowModels.Where(q => q.Invoice.IsSelectedRow).Select(q => q.Invoice).Distinct().ToArray();
			}
			else
			{
				var row = SelectedRowModel;
				if (row == null) return;
				deleteInvoices = new[] { row.Invoice };
			}
			if (deleteInvoices.Length == 0)
			{
				messageBoxService.ShowError("You must select at least one row");
				return;
			}

			string msg;
			if (deleteInvoices.Length == 1)
			{
				msg = "Do you want to delete Invoice #" + deleteInvoices[0].InvoiceNumber + "?";
			}
			else
			{
				msg = "Do you want to delete Invoices(" + deleteInvoices.Length + ")" + ":\n" + string.Join("\n", deleteInvoices.Select(q => "#" + q.InvoiceNumber).ToArray()) + "\n?";
			}

			var ret = messageBoxService.ShowMessage(msg, CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
				var deleteInvoicesRowId = deleteInvoices.Select(q => q.RowId).ToList();
				var uret = await businessService.DeleteInvoice(deleteInvoicesRowId);
				ShowWaitIndicator.Hide();
				if (!uret.Validate(messageBoxService)) return;

				RowModels.RemoveRange(q => deleteInvoicesRowId.Contains(q.Invoice.RowId));
				UpdateIsAlternateRow();
			}
		}
		public bool CanDelete() => (IsMultirowSelection ? true : SelectedRowModel != null);

		public void MouseDoubleClick(RowModel row)
		{
			if (row == null) return;
			AddEdit(row?.Invoice);
		}

		#region IsSelected region

		bool ignoreOnIsSelectedChangedEvents;
		public void OnIsSelectedAllRowsChanged()
		{
			if (!ignoreOnIsSelectedChangedEvents)
			{
				ignoreOnIsSelectedChangedEvents = true;
				BehaviorGridConrol.BeginDataUpdate();

				RowModels.ForEach(q => q.IsSelected = (IsSelectedAllRows == true));
				UpdateIsSelectedAllRows();

				BehaviorGridConrol.EndDataUpdate();
				ignoreOnIsSelectedChangedEvents = false;
			}
		}
		void OnIsSelectedChanged(RowModel q)
		{
			if (!ignoreOnIsSelectedChangedEvents)
			{
				ignoreOnIsSelectedChangedEvents = true;
				BehaviorGridConrol.BeginDataUpdate();

				RowModels.Where(z => z.Invoice.RowId == q.Invoice.RowId).ForEach(z => z.IsSelected = q.IsSelected);
				UpdateIsSelectedAllRows();

				BehaviorGridConrol.EndDataUpdate();
				ignoreOnIsSelectedChangedEvents = false;
			}
		}
		void UpdateIsSelectedAllRows()
		{
			var ex1 = RowModels.Any(q => q.IsSelected);
			var ex2 = RowModels.Any(q => !q.IsSelected);
			IsSelectedAllRows =
				ex1 && ex2 ? (bool?)null :
				ex1 && !ex2 ? true :
				false;
			IsVisibleSelectedAllRowsCheckbox = RowModels.Any();
			SelectedInvoices = RowModels.Where(q => q.IsSelected).Select(q => q.Invoice).Distinct().ToObservableCollection();
		}

		#endregion

		public IEnumerable<RowModel> InvoiceV2RowModels(Invoice invoice)
		{
			var list = new List<RowModel>();
			var noInvoiceClaims = (invoice.InvoiceClaims.Count == 0);
			var len = noInvoiceClaims ? 1 : invoice.InvoiceClaims.Count;
			for (int i = 0; i < len; i++)
			{
				list.Add(new RowModel
				{
					Invoice = invoice,
					InvoiceClaim = noInvoiceClaims ? null : invoice.InvoiceClaims[i],
					NumInInvoice = i,
					RowType = RowType.Main,
					Parent = this,
				});
			}

			var totalInvoiceClaim = new InvoiceClaim
			{
				SentAmont = invoice.InvoiceClaims.Sum(q => q.SentAmont),
				ApproveAmont = invoice.InvoiceClaims.Sum(q => q.ApproveAmont),
				DueByPatient = invoice.InvoiceClaims.Sum(q => q.DueByPatient),
			};
			var totalRow = new RowModel
			{
				Invoice = invoice,
				InvoiceClaim = totalInvoiceClaim,
				NumInInvoice = -1,
				RowType = RowType.Total,
				Parent = this,
			};
			list.Add(totalRow);

			invoice.UpdatePaidStatus();

			list.ForEach(q =>
			{
				q.OnOpenDetail = () => AddEdit(q.Invoice);
				(q as INotifyPropertyChanged).PropertyChanged += (s, e) =>
				{
					if (e.PropertyName == nameof(q.IsSelected))
					{
						OnIsSelectedChanged(q);
					}
				};
			});

			return list;
		}
		

		public void CellMerge(CellMergeEventArgs e)
		{
			var row1 = BehaviorGridConrol.GetRow<RowModel>(e.RowHandle1);
			var row2 = BehaviorGridConrol.GetRow<RowModel>(e.RowHandle2);

			e.Merge = (row1.Invoice == row2.Invoice);
			e.Handled = true;
		}


		bool inUpdateIsAlternateRow;
		void UpdateIsAlternateRow()
		{
			if (inUpdateIsAlternateRow) return;

			DateTime dt1 = DateTimeHelper.Now;

			inUpdateIsAlternateRow = true;
			BehaviorGridConrol.BeginDataUpdate();

			var rows = BehaviorGridConrol.GetAllVisibleRowHandles().Select(q => BehaviorGridConrol.GetRow<RowModel>(q));
			int invoiceNpp = 0;
			Guid? lastInvoiceRowId = null;
			foreach (var row in rows)
			{
				if (lastInvoiceRowId != null && lastInvoiceRowId != row.Invoice.RowId)
				{
					invoiceNpp++;
				}
				row.IsAlternateRow = (invoiceNpp % 2 == 1);
				lastInvoiceRowId = row.Invoice.RowId;
			}

			BehaviorGridConrol.EndDataUpdate();
			inUpdateIsAlternateRow = false;

			DateTime dt2 = DateTimeHelper.Now;
			NLog.Info("UpdateIsAlternateRow time=" + (dt2 - dt1).TotalMilliseconds);
		}

		
		public void CustomSummary(CustomSummaryEventArgs e)
		{
			if (e.IsTotalSummary && e.SummaryProcess == CustomSummaryProcess.Finalize)
			{
				var column = ((GridSummaryItem)(e.Item)).FieldName;
				var rows = BehaviorGridConrol.GetAllVisibleRowHandles()
					.Select(q => BehaviorGridConrol.GetRow<RowModel>(q))
					.Where(q => q.RowType == RowType.Total);

				if (column == "Invoice.InvoiceNumber")
				{
					e.TotalValue = rows.Count();
					e.TotalValueReady = true;
				}
				else
				{
					var exp = "q." + column;
					var p = System.Linq.Expressions.Expression.Parameter(typeof(RowModel), "q");
					var e22 = DynamicExpression.ParseLambda(new[] { p }, null, exp);

					if (e22.ReturnType == typeof(decimal))
					{
						var func = (Func<RowModel, decimal>)e22.Compile();
						var sum = rows.Sum(func);
						e.TotalValue = sum;
						e.TotalValueReady = true;
					}
					else if (e22.ReturnType == typeof(decimal?))
					{
						var func = (Func<RowModel, decimal?>)e22.Compile();
						var sum = rows.Sum(func);
						e.TotalValue = sum;
						e.TotalValueReady = true;
					}
				}
			}
		}

		public void RefundNew()
		{
			var row = CurrentInvoice;
			var newRefund = new Refund
			{
				RowId = Guid.NewGuid(),
				PatientRowId = row.PatientRowId,
				PatientFullName = row.PatientFullName,
				PaymentDate = DateTime.Today,
				RefundItemsType = TypeHelper.RefundItemsType_Invoice,
				IsNew = true,
			};
			var newInvoiceRefund = new InvoiceRefund
			{
				RowId = Guid.NewGuid(),
				Amount = -(row.Balance ?? 0),
				InvoiceRowId = row.RowId,
				Invoice = row,
				RefundRowId = newRefund.RowId,
				AllocationDate = DateTime.Today,
				IsChanged = true,
				IsNew = true,
			};
			//newInvoiceRefund.OnAfterLoad();


			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.RefundWindowView,
				Param = new RefundWindowViewModel.OpenParams
				{
					IsNew = true,
					ReadOnly = false,
					NewRefund = newRefund,
					NewInvoiceRefunds = new List<InvoiceRefund>{ newInvoiceRefund },
				},
			});
		}
		public bool CanRefundNew() => ((CurrentInvoice?.Balance ?? 0) < 0);

		public void PaymentNew()
		{
			var row = CurrentInvoice;
			//var newPayment = new Payment
			//{
			//	RowId = Guid.NewGuid(),
			//	PatientRowId = row.PatientRowId,
			//	PatientFullName = row.PatientFullName,
			//	PaymentDate = DateTime.Today,
			//	//PaymentItemsType = TypeHelper.PaymentItemsType_Invoice,
			//	IsNew = true,
			//};

			var amount = (row.Balance ?? 0);
			var newPayment = new Payment
			{
				RowId = Guid.NewGuid(),
				//Patient = CurrentInvoice.Patient,
				PatientRowId = CurrentInvoice.PatientRowId,
				PaymentDate = DateTime.Today,
				Amount = amount,
				IsNew = true,
			};
			var newInvoicePayment = new InvoicePayment
			{
				RowId = Guid.NewGuid(),
				Amount = amount,
				InvoiceRowId = row.RowId,
				Invoice = row,
				PaymentRowId = newPayment.RowId,
				AllocationDate = DateTime.Today,
				IsChanged = true,
				IsNew = true,
			};
			newPayment.InvoicePayments = new[] { newInvoicePayment }.ToList();

			////newInvoicePayment.OnAfterLoad();





			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.PaymentWindowView,
				Param = new PaymentWindowViewModel.OpenParams
				{
					IsNew = true,
					NewPayment = newPayment,
					ReadOnly = false,
					IsVariant2 = true,
				},
			});
		}
		public bool CanPaymentNew() => (CurrentInvoice != null) && (CurrentInvoice?.Balance ?? 0) > 0 && (!CurrentInvoice.IsEstimation);

		void SubsribeAdvancedFilters()
		{
			(this as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				if (e.PropertyName.In(nameof(SelectedReferrer), nameof(SelectedInsuranceProvider), nameof(IsShowSentOnly), nameof(IsShowPaidOnly), nameof(IsCoordinationProblemOnly)))
				{
					ReloadData();
				}
			};
		}

		void SubsribeIsMultirowSelectionChanged()
		{
			(this as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(IsMultirowSelection))
				{
					if (!IsMultirowSelection)
					{
						RowModels.ForEach(q => q.Invoice.IsSelectedRow = false);
					}
				}
			};
		}

		public void CustomRowFilter(RowFilterEventArgs e)
		{
			//NLog.Trace("CustomRowFilter=" + e.ListSourceRowIndex);
			if (e.ListSourceRowIndex == 0)
			{
				BuildFilterInfo();
			}

			if (FilterInfo.UseAdvMode)
			{
				var row = RowModels[e.ListSourceRowIndex];
				e.Visible = FilterInfo.InvoiceRowIdsInFilter.Contains(row.Invoice.RowId);
				e.Handled = true;
			}
		}

		void BuildFilterInfo()
		{
			BuildGridControlForFilter();

			var filter = GridControl.FilterString;
			if (filter == FilterInfo.FilterString) return;  //если фильтр не поменялся, то ничего не надо

			FilterInfo = new FilterInfoClass
			{
				FilterString = filter,
			};

			var needAdvMode = filter.Contains("[StatusInfoText]") || filter.Contains("[InvoiceClaim.InsuranceCoverage.InsuranceProviderCode]");
			if (needAdvMode)
			{
				GridControlForFilter.FilterString = GridControl.FilterString;
				var rows = GridControlForFilter.DataController.GetAllFilteredAndSortedRows().Cast<RowModel>();
				FilterInfo.InvoiceRowIdsInFilter = rows.Select(q => q.Invoice.RowId).Distinct().ToHashSet();
				FilterInfo.UseAdvMode = true;
			}
		}

		void ResetFilterInfo()
		{
			FilterInfo = new FilterInfoClass();
		}

		void BuildGridControlForFilter()
		{
			if (!GridControlForFilter.Columns.Any())
			{
				foreach (var column in GridControl.Columns)
				{
					var ncolumn = new GridColumn
					{
						FieldName = column.FieldName,
					};

					if (column.Binding != null)
					{
						var binding = (Binding)column.Binding;
						var prefix = "RowData.Row.";
						var path = binding.Path.Path;
						if (!path.StartsWith(prefix)) throw new ArgumentException();

						path = path.Substring(prefix.Length);
						var nBinding = new Binding(path);
						nBinding.Converter = binding.Converter;
						ncolumn.Binding = nBinding;
					}
					GridControlForFilter.Columns.Add(ncolumn);
				}
			}
		}

		public void Print()
		{
			var printGridControl = new PrintGridControl
			{
				GridControl = GridControl,
				PageOrientation = PageOrientation.Landscape,
			};
			printGridControl.Run();

			//var tableview = (TableView)GridControl.View;
			////tableview.Clon
			//List<TemplatedLink> links = new List<TemplatedLink>();
			//links.Add(new PrintableControlLink(tableview));

			//var compositeLink = new CompositeLink(links);
			//compositeLink.Landscape = true;
			//tableview.PrintAutoWidth = true;
			//PrintHelper.ShowRibbonPrintPreview(null, compositeLink);

			//GridControl.View.ShowPrintPreview(null);
		}

		public class PrintGridControl
		{
			public GridControl GridControl { get; set; }
			public PageOrientation PageOrientation { get; set; }
			//public SpreadsheetControl spreadsheetControl { get; set; }

			public void Run()
			{
				var mouseWaitCursor = new MouseWaitCursor();
				var tableview = (TableView)GridControl.View;
				var stream = new MemoryStream();
				var xlsxExportOptions = new XlsxExportOptions
				{

				};
				tableview.ExportToXlsx(stream, xlsxExportOptions);
				stream.Seek(0, SeekOrigin.Begin);

				var spreadsheetControl = new SpreadsheetControl();
				spreadsheetControl.LoadDocument(stream, DocumentFormat.Xlsx);
				var doc = spreadsheetControl.Document;
				var worksheets = doc.Worksheets[0];
				var activeView = worksheets.ActiveView;
				var pageMargins = worksheets.ActiveView.Margins;
				activeView.Orientation = PageOrientation;
				worksheets.PrintOptions.FitToWidth = 1;
				worksheets.PrintOptions.FitToHeight = 0;
				worksheets.PrintOptions.FitToPage = true;
				//!!worksheets.PrintOptions.PrintTitles.SetRows(0, 0);

				var range = worksheets.Range;
				var lastRow = worksheets.Rows.LastUsedIndex;
				var lastColumn = worksheets.Columns.LastUsedIndex;

				var range1 = worksheets.Range.FromLTRB(0, 0, lastColumn, 0);
				var format1 = range1.BeginUpdateFormatting();
				format1.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
				format1.Alignment.WrapText = true;
				range1.EndUpdateFormatting(format1);

				var range2 = worksheets.Range.FromLTRB(0, 1, lastColumn, lastRow);
				range2.AutoFitColumns();

				//__spreadsheetControl = spreadsheetControl;
				//spreadsheetControl.ShowPrintPreview();
				//spreadsheetControl.Document.Sho
				//spreadsheetControl.Print();
				//spreadsheetControl.SaveDocument(@"E:\PROJECTS\Profibiz.PracticeManager\aaa.xlsx");
				
				//var excelfile = Guid.NewGuid().ToString().Replace("{", "").Replace("}","").Replace("-","") + ".xlsx";
				var excelfile = "Invoice List " + DateTime.Now.ToString("yy-MM-dd HH-mm-ss") + ".xlsx";
				excelfile = Path.Combine(GetExcelDirectory(), excelfile);
				spreadsheetControl.SaveDocument(excelfile);
				System.Diagnostics.Process.Start(excelfile);
				mouseWaitCursor.Dispose();

				//doc.SaveDocument(@"E:\PROJECTS\Profibiz.PracticeManager\aaa.xlsx");
				//doc.ExportToPdf(@"E:\PROJECTS\Profibiz.PracticeManager\1112.pdf");
			}

			string GetExcelDirectory()
			{
				var excelDirectory = Path.Combine(AssemblyHelper.GetMainPath(), "ExcelOutput");
				if (!File.Exists(excelDirectory))
				{
					Directory.CreateDirectory(excelDirectory);
				}
				return excelDirectory;
			}
		}

		FilterInfoClass FilterInfo = new FilterInfoClass();
		class FilterInfoClass
		{
			public Boolean UseAdvMode = false;
			public String FilterString = "";
			public HashSet<Guid> InvoiceRowIdsInFilter;
		}


		[ImplementPropertyChanged]
		public class RowModel
		{
			public RowModel() { }

			public Invoice Invoice { get; set; }
			public InvoiceClaim InvoiceClaim { get; set; }
			public Int32 NumInInvoice { get; set; }
			public RowType RowType { get; set; }
			public InvoicesListViewModel Parent { get; set; }

			public Boolean IsAlternateRow { get; set; }

			public String StatusInfoText => 
				RowType == RowType.Total ? "Totals" :
				InvoiceClaim == null ? "-" :
				InvoiceClaim.StatusInfo;

			//public Boolean? HasApprovedAmount => Invoice?.HasApprovedAmount;

			public DelegateCommand OpenDetailCommand => new DelegateCommand(() =>
			{
				OnOpenDetail?.Invoke();
			});
			public Action OnOpenDetail;

			public bool IsSelected { get; set; }
			public bool IsVisibleSelected { get; set; } = true;
		}
		public enum RowType { Main, Total }
	}


	public class InvoiceClaimFormsConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var row = (InvoicesListViewModel.RowModel)value;
			if (parameter.Equals("Visibility"))
			{
				var visible = (row != null && row.RowType == InvoicesListViewModel.RowType.Main && row.InvoiceClaim != null);
				return visible ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
			}
			else if (parameter.Equals("Background"))
			{
                //return (row != null && row.RowType == InvoicesListViewModel.RowType.Main && row.InvoiceClaim?.Forms == true ? "#FFAAAA" : "Transparent");
                return (row != null && row.RowType == InvoicesListViewModel.RowType.Main && row.InvoiceClaim?.Forms != null && row.InvoiceClaim?.Forms != TypeHelper.FormsStatus.OK ? "#FFAAAA" : "Transparent");
            }
            else throw new ArgumentException();
		}

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}


	
	public class InvoicesListRowItalicConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var row = (InvoicesListViewModel.RowModel)value;
			if (row != null && !row.Invoice.HasApprovedAmount && row.Parent.IsEnabledItalicFormatConditionHasApprovedAmount)
			{
				return "Italic";
			}
			return "Normal";
		}

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	
	public class InvoicesListViewModelRowBackgroundConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue) return null;
				var isAlternateRow = (Boolean)values[0];
				var paidProblem = (Invoice.PaidProblemEnum)values[1];

				Brush brush = null;
				if (paidProblem == Invoice.PaidProblemEnum.None)
				{
					brush = (Brush)(!isAlternateRow ? Application.Current.FindResource("GridOddRowBrush") : Application.Current.FindResource("GridEvenRowBrush"));
				}
				else if (paidProblem == Invoice.PaidProblemEnum.SentButNotApproved)
				{
					brush = (!isAlternateRow ? ConvertFunc.ToBrush("#FF0000") : ConvertFunc.ToBrush("#EF0000"));
				}
				else if (paidProblem == Invoice.PaidProblemEnum.ApprovedButNotPaidToUs)
				{
					brush = (!isAlternateRow ? ConvertFunc.ToBrush("#FF6A00") : ConvertFunc.ToBrush("#EF5F00"));
				}


				if (brush == null) throw new ArgumentException();
				return brush;
			}
			catch(Exception ex)
			{
				var a = 10;
				throw new AggregateException(ex);
			}
		}

		public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new System.NotImplementedException();
		}
	}

	public class InvoicesListViewModelRowForegroundConvertor : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var invoiceClaim = values[0] as InvoiceClaim;
			var invoice = values[1] as Invoice;
			if (invoiceClaim == null || invoice == null) return null;

			var color = "";
			if (invoiceClaim.StatusInfo == "Sent")
			{
				color = "Blue";
			}
			else if (invoiceClaim.StatusInfo == "Rejected")
			{
				color = "Red";
			}
			else if (invoiceClaim.StatusInfo == "Partially")
			{
				color = "#FF00DC";
			}
			else if (invoiceClaim.StatusInfo == "Approved")
			{
				color = "Green";
			}
			else throw new ArgumentException();

			var mcolor = (Color)ColorConverter.ConvertFromString(color);
			return new SolidColorBrush(mcolor);
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

}
