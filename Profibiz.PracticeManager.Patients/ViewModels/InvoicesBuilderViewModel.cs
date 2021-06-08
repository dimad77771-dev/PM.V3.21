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
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using DevExpress.Xpf.Core;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
    [POCOViewModel]
    public class InvoicesBuilderViewModel : ViewModelBase
    {
        #region Service
        IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
        ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
        public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
        public IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
        public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
		#endregion
		public virtual ObservableCollection<Appointment> Entities { get; set; }
		public virtual Appointment SelectedEntitySet { get; set; }
        public virtual InvoiceOneViewModel OneModel { get; set; }
        public virtual DateTime FilterFrom { get; set; }
        public virtual DateTime FilterTo { get; set; }
		public virtual Boolean IsShowGenerated { get; set; } = false;
		public virtual ObservableCollection<AccountAgingModel> RibbonSpAccountAgingListItems { get; set; } = AccountAgingInfo.RibbonSpAccountAgingListItems;
		public virtual Int32 RibbonSpAccountAgingListColumnCount { get; set; } = AccountAgingInfo.RibbonSpAccountAgingListColumnCount;

		
		



		public InvoicesBuilderViewModel() : base()
        {
			var ret = GlobalSettings.Instance.Finances.GetFinancesViewDateFilter();
			FilterFrom = ret.FilterFrom;
			FilterTo = ret.FilterTo;
		}

		public void OnOpen(string parm)
        {
            OpenParmQuery = parm;
            OpenParms = QueryHelper.ParseString(parm);
            MessengerHelper.Register<MsgRowChange<Invoice>>(this, OnMsgRowChange);

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

			var currentRowId = SelectedEntitySet?.RowId;
			var rows = await GetAppointmentList();
            Entities = rows.ToObservableCollection();
            UpdateAllIsGroupSelected();
            Entities.ForEach(q =>
            {
                SubscribeAppointmentRow(q);
            });

			OneModel = null;
			var firstSelectedEntity = Entities.FirstOrDefault(q => q.RowId == currentRowId) ?? Entities.FirstOrDefault();
			await NewFocusedRow(firstSelectedEntity);

			ShowWaitIndicator.Hide();
            DXSplashScreenHelper.Hide();
		}

		async Task ReloadDataForPatient(Guid patientRowId)
		{
			ShowWaitIndicator.Show();
			ignoreUpdateCurrentNewPatientInvoice = true;

			var exRowIds = Entities.Where(q => q.PatientRowId == patientRowId).Select(q => q.RowId).ToArray();
			var rows = await GetAppointmentList(rowIds: exRowIds, ignoreInInvoice: true);

			foreach (var row in rows)
			{
				var index = Entities.FindIndex(q => q.RowId == row.RowId);
				if (index >= 0)
				{
					if (row.IsVisibleSelected)
					{
						row.IsSelected = Entities[index].IsSelected;
					}
					Entities[index] = row;
				}
			}
			UpdateAllIsGroupSelected();
			rows.ForEach(q =>
			{
				SubscribeAppointmentRow(q);
			});

			//если я остался на том же пациенте (команда "Save"), но при этом я стою не на верном OneModel, то найти верную строку и перейти к ней
			if (SelectedEntitySet?.PatientRowId == patientRowId)
			{
				var invoiceRowId = OneModel.Entity.RowId;
				if (SelectedEntitySet?.InvoiceRowId != invoiceRowId)
				{
					var frow = rows.FirstOrDefault(q => q.InvoiceRowId == invoiceRowId);
					if (frow != null)
					{
						NewFocusedRow(frow);
					}
				}
			}


			ShowWaitIndicator.Hide();
			ignoreUpdateCurrentNewPatientInvoice = false;
		}

		async Task<List<Appointment>> GetAppointmentList(Guid? patientRowId = null, Guid[] rowIds = null, bool ignoreInInvoice = false)
		{
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(
				businessService.GetAppointmentList(
					startFrom: FilterFrom, 
					startTo: FilterTo, 
					rowIds: rowIds,
					completed: true, 
					patientRowId: patientRowId,
					inInvoice: IsShowGenerated || ignoreInInvoice ? (bool?)null : false));
			rows = rows
				.OrderBy(q => q.PatientFullName)
				.ThenBy(q => q.InInvoice)
				.ThenBy(q => q.PolicyFullName)
				//.ThenBy(q => LookupDataProvider.Insurance2Code(q.InsuranceProviderRowId))
				.ThenBy(q => q.InvoiceNumber)
				.ThenBy(q => q.Start)
				.ToList();
			return rows;
		}

		bool hasMsgRowChange = false;
		void OnMsgRowChange(MsgRowChange<Invoice> msg)
        {
			hasMsgRowChange = true;
		}

		async Task<Boolean> OnSelectedEntityChanged222(Appointment lastSelectedRow, Appointment newSelectedRow)
        {
            if (newSelectedRow?.RowId == lastSelectedRow?.RowId) return true;
			if (GetInvoiceKey(newSelectedRow) == GetInvoiceKey(lastSelectedRow)) return true;


            if (OneModel != null)
            {
                var ret = await OneModel.OnClose();
                if (ret.IsCancel())
                {
					ClearIsSelected(lastSelectedRow);
					return false;
                }
            }

			ClearIsSelected(newSelectedRow);

			if (newSelectedRow != null)
            {
                OneModel = InvoiceOneViewModel.Create(isWindowMode: false);
				OneModel.ViewMode = InvoiceOneViewModel.ViewModes.InvoicesBuilder;
				OneModel.IsShowColorRowBackground = true;
				if (newSelectedRow.InvoiceRowId != null)
				{
					await OneModel.LoadData(false, newSelectedRow.InvoiceRowId);
				}
				else
				{
					var patientRowId = newSelectedRow.PatientRowId;
					var patient = new Patient
					{
						RowId = patientRowId,
						FirstName = newSelectedRow.PatientFullName,
						Rate = newSelectedRow.PatientRate,
					};
					Invoice newInvoice = new Invoice
					{
						RowId = Guid.NewGuid(),
						InvoiceType = TypeHelper.InvoiceType.Appointment,
						PrintTemplate = Invoice.InvoiceType2DefaultPrintTemplate(TypeHelper.InvoiceType.Appointment),
						InvoiceDate = DateTime.Today,
						Patient = patient,
						PatientRowId = patientRowId,
						InvoiceNumber = "",
						Rate = patient.Rate,
						Created = DateTimeHelper.Now,
					};

					await OneModel.LoadData(true, newInvoice: newInvoice, flagAlwaysIsNewHasChanges: false);

					UpdateCurrentNewPatientInvoice(patientRowId);
				}
            }
            else
            {
                OneModel = null;
            }
			return true;
        }

		void ClearIsSelected(Appointment currentSelectedRow)
		{
			ignoreChangeIsSelected = true;
			var exceptPatientRowId = (currentSelectedRow == null || currentSelectedRow.InInvoice ? (Guid?)null : currentSelectedRow.PatientRowId);
			Entities.Where(q => q.PatientRowId != exceptPatientRowId).ForEach(q => q.IsSelected = false);
			ignoreChangeIsSelected = false;
		}

		bool ignoreUpdateCurrentNewPatientInvoice = false;
		void UpdateCurrentNewPatientInvoice(Guid patientRowId)
		{
			if (ignoreUpdateCurrentNewPatientInvoice) return;
			if (OneModel == null) return;
			if (OneModel?.Entity.PatientRowId != patientRowId) return;

			var invoiceRowId = OneModel.Entity.RowId;
			var invoiceItems = OneModel.InvoiceItemEntities;

			//add
			var allSelectedRows = Entities.Where(q => q.PatientRowId == patientRowId && q.IsSelected && q.IsVisibleSelected).ToArray();
			foreach (var appointment in allSelectedRows.Where(q => !invoiceItems.Any(z => z.AppointmentRowId == q.RowId)))
			{
				var invoiceItem = InvoiceItem.CreateFromAppointment(appointment);
				invoiceItem.InvoiceRowId = invoiceRowId;
				invoiceItems.Add(invoiceItem);
				OneModel.SubscribeInvoiceItemRow(invoiceItem);
			}

			//delete
			var delrows =
				invoiceItems
				.Where(q => !Entities.Any(z => z.RowId == q.AppointmentRowId && z.PatientRowId == patientRowId && z.IsSelected && z.IsVisibleSelected))
				.ToArray();
			delrows.ForEach(q => invoiceItems.Remove(q));

			OneModel.CalcInvoiceFields();

			//это HasNoCoverage-invoice?
			var hasNoCoverage = allSelectedRows.Any() && allSelectedRows.All(q => q.HasNoCoverage);

			//SendToInsuranceProviderRowId
			var allInsuranceCoverageRowIds = allSelectedRows.Select(q => q.InsuranceCoverageRowId).Distinct().ToArray();
			if (!hasNoCoverage)
			{
				if (allInsuranceCoverageRowIds.Contains(null)) throw new LogicalException();
			}
			var sendToInsuranceProviderRowId = (allInsuranceCoverageRowIds.Length == 1 ? allInsuranceCoverageRowIds.First() : null);

			var invoiceClaimEntities = OneModel.InvoiceClaimEntities;
			invoiceClaimEntities.Clear();
			if (hasNoCoverage)
			{
				var amount = (decimal)OneModel.Entity.Total;
				invoiceClaimEntities.Add(new InvoiceClaim
				{
					RowId = Guid.NewGuid(),
					InsuranceCoverageRowId = null,
					SentDate = DateTime.Today,
					SentAmont = amount,
					ApproveDate = DateTime.Today,
					ApproveAmont = amount,
				});
			}
			else
			{
				if (sendToInsuranceProviderRowId != null)
				{
					invoiceClaimEntities.Add(new InvoiceClaim
					{
						RowId = Guid.NewGuid(),
						InsuranceCoverageRowId = (Guid)allInsuranceCoverageRowIds[0],
						SentDate = DateTime.Today,
						SentAmont = (decimal)OneModel.Entity.Amount,
					});
				}
			}
			OneModel.Entity.HasNoCoverage = hasNoCoverage;

			OneModel.CalcPrintTemplateFromAppointments(allSelectedRows);
		}

		string GetInvoiceKey(Appointment row)
		{
			if (row == null)
			{
				return "null";
			}
			else if (row.InInvoice)
			{
				return "InvoiceRowId=" + row.InvoiceRowId;
			}
			else
			{
				return "PatientRowId=" + row.PatientRowId;
			}
		}


		public void Save()
        {
            DispatcherUIHelper.Run(async () =>
            {
                if (OneModel != null)
                {
					hasMsgRowChange = false;
					await OneModel.SaveCore(false);
					if (hasMsgRowChange)
					{
						await ReloadDataForPatient(OneModel.Entity.PatientRowId);
					}
				}
            });
        }

		bool ignoreOnIsShowGeneratedChanged;
		public void OnIsShowGeneratedChanged(bool oldValue)
		{
			if (ignoreOnIsShowGeneratedChanged) return;
			if (IsShowGenerated == oldValue) return;

			FilterCore(triggedByIsShowGenerated: true);
		}


		public void Filter(string mode) => FilterCore(mode);
		public void FinanceDateApply(FinanceDateClass preset) => FilterCore(null, preset);


		public void FilterCore(string arg = "", FinanceDateClass preset = null, bool triggedByIsShowGenerated = false)
		{
            DispatcherUIHelper.Run(async () =>
            {
                if (OneModel != null)
                {
                    var ret = await OneModel.OnClose();
                    if (ret.IsCancel())
                    {
						if (triggedByIsShowGenerated)
						{
							ignoreOnIsShowGeneratedChanged = true;
							IsShowGenerated = !IsShowGenerated;
							ignoreOnIsShowGeneratedChanged = false;
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


        bool ignoreChangeIsSelected = false;
        bool ignoreChangeIsGroupSelected = false;
        void SubscribeAppointmentRow(Appointment row)
        {
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
            {
				if (e.PropertyName == nameof(row.IsGroupSelected) && !ignoreChangeIsGroupSelected)
                {
                    ChangeIsGroupSelected(row);
                }
                if (e.PropertyName == nameof(row.IsSelected) && !ignoreChangeIsSelected)
                {
                    ChangeIsSelected(row);
                }
            };

			row.OnOpenDetail = () =>
			{
				var param = new OneAppointmentViewModel.OpenParams
				{
					IsNew = false,
					RowId = row.RowId,
					IsReadOnly = true,
				};
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneAppointmentView,
					Param = param,
				});
			};
		}

		void ChangeIsGroupSelected(Appointment row)
        {
            ignoreChangeIsSelected = true;

            Entities.Where(q => q.PatientRowId == row.PatientRowId).ForEach(q => q.IsSelected = (bool)row.IsGroupSelected);
			UpdateCurrentNewPatientInvoice(row.PatientRowId);

			ignoreChangeIsSelected = false;
        }

        void UpdateAllIsGroupSelected()
        {
            //!!!Entities.ForEach(q => ChangeIsSelected(q));
        }

		bool ignoreMoveFocusOnChangeIsSelected;
		void ChangeIsSelected(Appointment row)
        {
			bool localIgnoreMoveFocusOnChangeIsSelected = ignoreMoveFocusOnChangeIsSelected;
			DispatcherUIHelper.Run(async () =>
			{
				try
				{
					ignoreChangeIsGroupSelected = true;

					if (!localIgnoreMoveFocusOnChangeIsSelected)
					{
						if (!await NewFocusedRow(row))
						{
							return;
						}
					}

					Func<Appointment, bool> wh = (q => q.PatientRowId == row.PatientRowId && q.IsVisibleSelected);
					var allCount = Entities.Count(wh);
					var selectedCount = Entities.Count(q => wh(q) && q.IsSelected);
					var notselectedCount = Entities.Count(q => wh(q) && !q.IsSelected);
					var isGroupSelected =
						allCount == selectedCount ? (bool?)true :
						allCount == notselectedCount ? (bool?)false :
						null;
					var isVisibleGroupSelected = (allCount > 0);
					Entities.Where(q => q.PatientRowId == row.PatientRowId).ForEach(q =>
					{
						q.IsGroupSelected = isGroupSelected;
						q.IsVisibleGroupSelected = isVisibleGroupSelected;
					});

					UpdateCurrentNewPatientInvoice(row.PatientRowId);
				}
				finally
				{
					ignoreChangeIsGroupSelected = false;
				}
			});
        }

        public void CustomColumnGroup(CustomColumnSortEventArgs e)
        {
            int res = Comparer.Default.Compare(e.Value1, e.Value2);
            if (res == 0)
            {
                var rowid1 = Entities[e.ListSourceRowIndex1].PatientRowId;
                var rowid2 = Entities[e.ListSourceRowIndex2].PatientRowId;
                res = Comparer.Default.Compare(rowid1, rowid2);
            }
            e.Result = res;
            e.Handled = true;
        }


		//требование перехода на другую строку - возвращаем если требование удовлетворено
		public async Task<Boolean> NewFocusedRow(Appointment newSelectedRow)
		{
			if (newSelectedRow != SelectedEntitySet)
			{
				var oldrow = SelectedEntitySet;

				hasMsgRowChange = false;
				if (!await OnSelectedEntityChanged222(SelectedEntitySet, newSelectedRow))
				{
					return false;
				}
				SetSelectedEntity(newSelectedRow);
				if (hasMsgRowChange)
				{
					await ReloadDataForPatient(oldrow.PatientRowId);
				}
			}
			return true;
		}


		public void BeforeLayoutRefresh(CancelRoutedEventArgs e)
		{
			e.Cancel = cancelBeforeLayoutRefresh;
		}
		bool cancelBeforeLayoutRefresh = true;
		void SetSelectedEntity(Appointment row)
		{
			cancelBeforeLayoutRefresh = false;
			SelectedEntitySet = row;
			cancelBeforeLayoutRefresh = true;
		}



		const string columnInsuranceCoverageInfo = "columnInsuranceCoverageInfo";
		public void MouseClick(System.Windows.Input.MouseButtonEventArgs e)
		{
			DispatcherUIHelper.Run(async () =>
			{
				var hitInfo = BehaviorGridConrol.GetCalcHitInfo(e);
				if (hitInfo == null) return;

				var row44 = BehaviorGridConrol.GetRow<Appointment>(hitInfo.RowHandle);
				await NewFocusedRow(row44);

				if (hitInfo.Column?.Name == columnInsuranceCoverageInfo && hitInfo.InRow)
				{
					var row = BehaviorGridConrol.GetRow<Appointment>(hitInfo.RowHandle);
					if (!row.InInvoice)
					{
						ignoreMoveFocusOnChangeIsSelected = true;
						foreach (var arow in Entities.Where(q => q.PatientRowId == row.PatientRowId && !q.InInvoice && q.IsVisibleSelected))
						{
							arow.IsSelected = (arow.InsuranceCoverageRowId == row.InsuranceCoverageRowId);
						}
						ignoreMoveFocusOnChangeIsSelected = false;
					}
				}
			});
		}

		public void CellMerge(CellMergeEventArgs e)
		{
			var row1 = BehaviorGridConrol.GetRow<Appointment>(e.RowHandle1);
			var row2 = BehaviorGridConrol.GetRow<Appointment>(e.RowHandle2);

			if (e.Column.Name == columnInsuranceCoverageInfo)
			{
				e.Merge = (row1.InsuranceCoverageRowId == row2.InsuranceCoverageRowId && row1.InInvoice == row2.InInvoice && row1.InvoiceRowId == row2.InvoiceRowId);
				e.Handled = true;
			}
			else if (e.Column.FieldName == "InvoiceNumber")
			{
				e.Merge = (row1.InsuranceCoverageRowId == row2.InsuranceCoverageRowId && row1.InInvoice == row2.InInvoice && row1.InvoiceRowId == row2.InvoiceRowId);
				e.Handled = true;
			}
		}




		public void TreeAction(string command)
		{
			if (command == "CollapseAll")
			{
				BehaviorGridConrol.CollapseAllGroups();
			}
			else if (command == "ExpandAll")
			{
				BehaviorGridConrol.ExpandAllGroups();
			}
		}
	}


	public enum InvoicesFilterDateMode
	{
		[Description("Invoice Date")]
		InvoiceDate = 0,

		[Description("Generated Date")]
		GeneratedDate = 1,
	}
}
