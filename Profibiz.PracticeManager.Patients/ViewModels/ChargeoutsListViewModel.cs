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
	public class ChargeoutsListViewModel : ViewModelBase
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
		#endregion
		public virtual ObservableCollection<RowModel> RowModels { get; set; }
		public virtual RowModel SelectedRowModel { get; set; }
		public virtual Chargeout CurrentChargeout => SelectedRowModel?.Chargeout;
		public virtual Chargeout SelectedEntity { get; set; }
		public virtual ChargeoutOneViewModel OneModel { get; set; }
		public virtual DateTime FilterFrom { get; set; }
		public virtual DateTime FilterTo { get; set; }
		public virtual ChargeoutsFilterDateMode FilterDateMode { get; set; }
		public virtual Boolean IsShowAllowRefchargeOnly { get; set; } = false;
		public virtual ObservableCollection<AccountAgingModel> RibbonSpAccountAgingListItems { get; set; } = AccountAgingInfo.RibbonSpAccountAgingListItems;
		public virtual ObservableCollection<ChargeoutListBackgroundColorModel> RibbonChargeoutListBackgroundColorListItems { get; set; } = ChargeoutListBackgroundColorInfo.RibbonChargeoutListBackgroundColorListItems;
		public virtual Int32 RibbonSpAccountAgingListColumnCount { get; set; } = AccountAgingInfo.RibbonSpAccountAgingListColumnCount;
		public virtual Boolean IsRightPanelVisible => (ViewMode == ViewModes.Main);
		public virtual Boolean IsMainRibbonShow { get; set; }
		public virtual Boolean IsVisibleServiceProvidersList => true;
		public virtual Boolean IsVisibleMedicalServicesList => true;
		public virtual Boolean IsVisibleCategoriesList => true;
		public virtual Boolean IsFullListMode => (ViewMode == ViewModes.Main);
		public virtual Boolean IsShowIsSelectedColumn => (ViewMode == ViewModes.PickRows);
		public virtual Boolean IsEnabledItalicFormatConditionHasApprovedAmount => (ViewMode == ViewModes.PickRows);

		public virtual bool? IsSelectedAllRows { get; set; } = null;
		public virtual bool IsVisibleSelectedAllRowsCheckbox { get; set; } = true;
		public virtual ObservableCollection<Chargeout> SelectedChargeouts { get; set; } = new ObservableCollection<Chargeout>();
		public virtual bool IsMultirowSelection { get; set; } = false;

		public virtual Boolean IsShowPaidOnly { get; set; }

		public virtual List<Chargeout> PreloadedRows { get; set; }
		public virtual Guid? OnePatientRowId { get; set; }
		public virtual ViewModes ViewMode { get; set; }
		public enum ViewModes { Main, OnePatient, PickRows }

		public ChargeoutsListViewModel() : base()
		{
			var ret = GlobalSettings.Instance.Chargeouts.GetChargeoutsViewDateFilter();
			FilterFrom = ret.FilterFrom;
			FilterTo = ret.FilterTo;
			FilterDateMode = (ChargeoutsFilterDateMode)ret.ChargeoutsFilterDateMode;
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

			List<Chargeout> rows;
			if (PreloadedRows != null)
			{
				rows = PreloadedRows;
			}
			else
			{
				var query = "";
				if (FilterDateMode == ChargeoutsFilterDateMode.ChargeoutDate)
				{
					query = "chargeoutDateFrom=" + FilterFrom.ToWebQuery() + "&" + "chargeoutDateTo=" + FilterTo.ToWebQuery();
				}
				else if (FilterDateMode == ChargeoutsFilterDateMode.GeneratedDate)
				{
					query = "createdDateFrom=" + FilterFrom.ToWebQuery() + "&" + "createdDateTo=" + FilterTo.ToWebQuery();
				}
				else throw new ArgumentException();

				if (OnePatientRowId != null)
				{
					query += "&" + "patientRowId=" + OnePatientRowId + "&" + "useFamilyHead=" + true;
				}
				if (IsShowAllowRefchargeOnly)
				{
					query += "&" + "negativeBalanceOnly=" + IsShowAllowRefchargeOnly.ToWebQuery();
				}
				if (IsShowPaidOnly)
				{
					query += "&" + "isShowPaidOnly=" + IsShowPaidOnly.ToWebQuery();
				}
				rows = await GetChargeoutList(query);
			}
			RowModels = rows.OrderByDescending(q => q.ChargeoutDate).SelectMany(q => ChargeoutV2RowModels(q)).ToObservableCollection();
			OneModel = null;

			ResetFilterInfo();
			UpdateIsSelectedAllRows();
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
				MessengerHelper.Register<MsgRowChange<Chargeout>>(this, OnMsgRowChangeChargeout);
				MessengerHelper.Register<MsgRowChange<Refcharge>>(this, OnMsgRowChangeRefcharge);

				BehaviorGridConrol.OnFilterSortGroupChange(UpdateIsAlternateRow);
				SubsribeAdvancedFilters();
				SubsribeIsMultirowSelectionChanged();

				isRegisterMessenger = true;
			}
		}

		void OnMsgRowChangeCore(Guid[] chargeoutRowIds)
		{
			MessengerHelper.RunAction(this, async () =>
			{
				ShowWaitIndicator.Show();

				foreach (var chargeoutRowId in chargeoutRowIds)
				{
					var rows = await GetChargeoutList("rowId=" + chargeoutRowId);

					var n = RowModels.FindIndex(q => q.Chargeout.RowId == chargeoutRowId);
					if (n == -1)
					{
						n = 0;
					}
					RowModels.RemoveRange(q => q.Chargeout.RowId == chargeoutRowId);
					var models = rows.SelectMany(q => ChargeoutV2RowModels(q));
					RowModels.InsertRange(n, models);
					SelectedRowModel = RowModels[n];
				}

				UpdateIsAlternateRow();
				ShowWaitIndicator.Hide();
			});
		}

		void OnMsgRowChangeChargeout(MsgRowChange<Chargeout> msg)
		{
			OnMsgRowChangeCore(new[] { msg.Row.RowId });
		}

		void OnMsgRowChangeRefcharge(MsgRowChange<Refcharge> msg)
		{
			var chargeoutRowIds = msg.Row.ChargeoutRefcharges.Select(q => q.ChargeoutRowId).Distinct().ToArray();
			OnMsgRowChangeCore(chargeoutRowIds);
		}

		async Task<List<Chargeout>> GetChargeoutList(string qry)
		{
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetChargeoutList(qry + "&includeChargeoutClaims=true"));
			return rows;
		}

		async Task<List<RowModel>> GetRowModelList(string qry)
		{
			var rows = await GetChargeoutList(qry);
			var models = rows.SelectMany(q => ChargeoutV2RowModels(q)).ToList();
			return models;
		}


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


		public void OnIsShowAllowRefchargeOnlyChanged()
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

				GlobalSettings.Instance.Chargeouts.SetChargeoutsViewDateFilter(FilterFrom, FilterTo, (int)FilterDateMode);
				await LoadData();
			});
		}



		void AddEdit(Chargeout row = null)
		{
			DispatcherUIHelper.Run(async () =>
			{
				var isnew = (row == null);

				Chargeout chargeout = null;
				if (isnew)
				{
					var ret2 = await PickChargeoutRecipientsViewModel.PickRow(ShowDXWindowsInteractionRequest);
					if (!ret2.IsSuccess) return;
					var chargeoutRecipient = ret2.PickRow;

					var newChargeoutType = Chargeout.CHARGEOUT_TYPE;

					chargeout = new Chargeout
					{
						RowId = Guid.NewGuid(),
						ChargeoutRecipient = chargeoutRecipient,
						ChargeoutRecipientRowId = chargeoutRecipient.RowId,
						ChargeoutDate = DateTime.Today,
						ChargeoutType = newChargeoutType,
						PrintTemplate = Chargeout.ChargeoutType2DefaultPrintTemplate(newChargeoutType),
						ChargeoutNumber = "",
						//Rate = patient.Rate,
						//HasNoCoverage = hasNoCoverage,
						Created = DateTimeHelper.Now,
					};
				}


				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.ChargeoutWindowView,
					Param = new ChargeoutWindowViewModel.OpenParams
					{
						IsNew = isnew,
						RowId = (isnew ? default(Guid) : row.RowId),
						ReadOnly = false,
						NewChargeout = chargeout,
					},
				});
			});
		}

		public void Edit()
		{
			AddEdit(CurrentChargeout);
		}
		public bool CanEdit() => (CurrentChargeout != null);




		public void New()
		{
			AddEdit(null);
		}

		public async void Delete()
		{
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();

			Chargeout[] deleteChargeouts;
			if (IsMultirowSelection)
			{
				deleteChargeouts = RowModels.Where(q => q.Chargeout.IsSelectedRow).Select(q => q.Chargeout).Distinct().ToArray();
			}
			else
			{
				var row = SelectedRowModel;
				if (row == null) return;
				deleteChargeouts = new[] { row.Chargeout };
			}
			if (deleteChargeouts.Length == 0)
			{
				messageBoxService.ShowError("You must select at least one row");
				return;
			}

			string msg;
			if (deleteChargeouts.Length == 1)
			{
				msg = "Do you want to delete Chargeout #" + deleteChargeouts[0].ChargeoutNumber + "?";
			}
			else
			{
				msg = "Do you want to delete Chargeouts(" + deleteChargeouts.Length + ")" + ":\n" + string.Join("\n", deleteChargeouts.Select(q => "#" + q.ChargeoutNumber).ToArray()) + "\n?";
			}

			var ret = messageBoxService.ShowMessage(msg, CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
				var deleteChargeoutsRowId = deleteChargeouts.Select(q => q.RowId).ToList();
				var uret = await businessService.DeleteChargeout(deleteChargeoutsRowId);
				ShowWaitIndicator.Hide();
				if (!uret.Validate(messageBoxService)) return;

				RowModels.RemoveRange(q => deleteChargeoutsRowId.Contains(q.Chargeout.RowId));
				UpdateIsAlternateRow();
			}
		}
		public bool CanDelete() => (IsMultirowSelection ? true : SelectedRowModel != null);

		public void MouseDoubleClick(RowModel row)
		{
			if (row == null) return;
			AddEdit(row?.Chargeout);
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

				RowModels.Where(z => z.Chargeout.RowId == q.Chargeout.RowId).ForEach(z => z.IsSelected = q.IsSelected);
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
			SelectedChargeouts = RowModels.Where(q => q.IsSelected).Select(q => q.Chargeout).Distinct().ToObservableCollection();
		}

		#endregion

		public IEnumerable<RowModel> ChargeoutV2RowModels(Chargeout chargeout)
		{
			var list = new List<RowModel>();
			var totalRow = new RowModel
			{
				Chargeout = chargeout,
				Parent = this,
			};
			list.Add(totalRow);

			chargeout.UpdatePaidStatus();

			list.ForEach(q =>
			{
				q.OnOpenDetail = () => AddEdit(q.Chargeout);
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

			e.Merge = (row1.Chargeout == row2.Chargeout);
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
			int chargeoutNpp = 0;
			Guid? lastChargeoutRowId = null;
			foreach (var row in rows)
			{
				if (lastChargeoutRowId != null && lastChargeoutRowId != row.Chargeout.RowId)
				{
					chargeoutNpp++;
				}
				row.IsAlternateRow = (chargeoutNpp % 2 == 1);
				lastChargeoutRowId = row.Chargeout.RowId;
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
					.Select(q => BehaviorGridConrol.GetRow<RowModel>(q));

				if (column == "Chargeout.ChargeoutNumber")
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

		public void RefchargeNew()
		{
			//var row = CurrentChargeout;
			//var newRefcharge = new Refcharge
			//{
			//	RowId = Guid.NewGuid(),
			//	PatientRowId = row.PatientRowId,
			//	PatientFullName = row.PatientFullName,
			//	PaychargeDate = DateTime.Today,
			//	RefchargeItemsType = TypeHelper.RefchargeItemsType_Chargeout,
			//	IsNew = true,
			//};
			//var newChargeoutRefcharge = new ChargeoutRefcharge
			//{
			//	RowId = Guid.NewGuid(),
			//	Amount = -(row.Balance ?? 0),
			//	ChargeoutRowId = row.RowId,
			//	Chargeout = row,
			//	RefchargeRowId = newRefcharge.RowId,
			//	AllocationDate = DateTime.Today,
			//	IsChanged = true,
			//	IsNew = true,
			//};
			////newChargeoutRefcharge.OnAfterLoad();


			//ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			//{
			//	ViewCode = ViewCodes.RefchargeWindowView,
			//	Param = new RefchargeWindowViewModel.OpenParams
			//	{
			//		IsNew = true,
			//		ReadOnly = false,
			//		NewRefcharge = newRefcharge,
			//		NewChargeoutRefcharges = new List<ChargeoutRefcharge> { newChargeoutRefcharge },
			//	},
			//});
		}
		public bool CanRefchargeNew() => ((CurrentChargeout?.Balance ?? 0) < 0);


		void SubsribeAdvancedFilters()
		{
			(this as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				if (e.PropertyName.In( nameof(IsShowPaidOnly) ))
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
						RowModels.ForEach(q => q.Chargeout.IsSelectedRow = false);
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
				e.Visible = FilterInfo.ChargeoutRowIdsInFilter.Contains(row.Chargeout.RowId);
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

			var needAdvMode = filter.Contains("[StatusInfoText]") || filter.Contains("[ChargeoutClaim.InsuranceCoverage.InsuranceProviderCode]");
			if (needAdvMode)
			{
				GridControlForFilter.FilterString = GridControl.FilterString;
				var rows = GridControlForFilter.DataController.GetAllFilteredAndSortedRows().Cast<RowModel>();
                FilterInfo.ChargeoutRowIdsInFilter = rows
                    .Select(q => q.Chargeout.RowId)
                    .Distinct()
                    .ToHashSet();
                
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
				var excelfile = "Chargeout List " + DateTime.Now.ToString("yy-MM-dd HH-mm-ss") + ".xlsx";
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
			public HashSet<Guid> ChargeoutRowIdsInFilter;
		}


		[ImplementPropertyChanged]
		public class RowModel
		{
			public RowModel() { }

			public Chargeout Chargeout { get; set; }
			public ChargeoutsListViewModel Parent { get; set; }

			public Boolean IsAlternateRow { get; set; }

			public DelegateCommand OpenDetailCommand => new DelegateCommand(() =>
			{
				OnOpenDetail?.Invoke();
			});
			public Action OnOpenDetail;

			public bool IsSelected { get; set; }
			public bool IsVisibleSelected { get; set; } = true;
		}
	}





	public class ChargeoutsListRowItalicConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var row = (ChargeoutsListViewModel.RowModel)value;
			if (row != null && row.Parent.IsEnabledItalicFormatConditionHasApprovedAmount)
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


	public class ChargeoutsListViewModelRowBackgroundConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue) return null;
				var isAlternateRow = (Boolean)values[0];
				var paidProblem = (Chargeout.PaidProblemEnum)values[1];

				Brush brush = null;
				if (paidProblem == Chargeout.PaidProblemEnum.None)
				{
					brush = (Brush)(!isAlternateRow ? Application.Current.FindResource("GridOddRowBrush") : Application.Current.FindResource("GridEvenRowBrush"));
				}
				else if (paidProblem == Chargeout.PaidProblemEnum.NotPaid)
				{
					brush = (!isAlternateRow ? ConvertFunc.ToBrush("#FF6A00") : ConvertFunc.ToBrush("#EF5F00"));
				}


				if (brush == null) throw new ArgumentException();
				return brush;
			}
			catch (Exception ex)
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

	public enum ChargeoutsFilterDateMode
	{
		[Description("Invoice Date")]
		ChargeoutDate = 0,

		[Description("Generated Date")]
		GeneratedDate = 1,
	}
}
