using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Profibiz.PracticeManager.Infrastructure;
using Prism.Interactivity.InteractionRequest;
using System.Collections.ObjectModel;
using DevExpress.DevAV.Common;
using DevExpress.Mvvm.POCO;
using System.ComponentModel;
using Newtonsoft.Json;
using PropertyChanged;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.DragDrop;
using System.Diagnostics;
using Profibiz.PracticeManager.Model;
using Profibiz.PracticeManager.InfrastructureExt.Common;
using Profibiz.PracticeManager.Patients.BusinessService;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
using System.Globalization;
using Profibiz.PracticeManager.SharedCode;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class InsuranceCoverage2ViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
		#endregion
		public enum ViewModeEnum { Full, Short }
		public ViewModeEnum ViewMode { get; set; } = ViewModeEnum.Full;
		public bool IsFullMode => (ViewMode == ViewModeEnum.Full);
		public bool IsReadOnly => (ViewMode != ViewModeEnum.Full);
		public bool IsShowColumnAll { get; set; }

		public bool IsNew { get; set; }
		public virtual InsuranceCoverage Entity { get; set; }
		public virtual ObservableCollection<Row> AllRows { get; set; } = new ObservableCollection<Row>();
		public virtual Row SelectedRow { get; set; }
		
		public virtual ObservableCollection<Category> Categories { get; set; }
		public virtual ObservableCollection<Holder> Holders { get; set; }

		public ColumnBase CurrentColumn { get; set; }




		public InsuranceCoverage2ViewModel() : base()
		{
		}


		public OpenParams OpenParam { get; set; }
		public void OnOpen(OpenParams param)
		{
			OpenParam = param;
			DispatcherUIHelper.Run(async () =>
			{
				await LoadData();
			});
		}
		public class OpenParams
		{
			public bool IsNew { get; set; }
			public Guid RowId { get; set; }
			public IEnumerable<Patient> FamilyMembers { get; set; } = new List<Patient>();

			public Guid? HighlightPatientRowId { get; set; }
			public Guid? HighlightCategoryRowId { get; set; }

			//public InsurancePatientCategoryInfo.FindResult InsuranceCoverageHighlight { get; set; }
		}

		public async Task LoadData()
		{
			ShowWaitIndicator.Show();

			IsNew = OpenParam.IsNew;
			if (IsNew)
			{
				Entity = new InsuranceCoverage
				{
					RowId = Guid.NewGuid(),
				};
			}
			else
			{
				Entity = await businessService.GetInsuranceCoverage(OpenParam.RowId);
				await LoadInsurancePatientCategoryInfo();
			}
			await LoadCategories();
			LoadPatients();
			BuildGridControlColumns();
			LoadAllRows();
			CalculateAll();
			SubsribeInsuranceCoverageYearTypeChange();

			DispatcherUIHelper.Run(ResetHasChange); //DispatcherUIHelper.Run - нужно из-за dxe:MemoEdit - 

			ShowWaitIndicator.Hide();
		}

		

		void BuildGridControlColumns()
		{
			Columns.Add(new Column(this));
			Holders.ForEach(q => Columns.Add(new Column(this, q)));

			var gridConrol = BehaviorGridConrol.Control;
			foreach (var column in Columns)
			{
				var gridColumn = new GridColumn();
				var binding = new Binding(column.IsAllColumn ? "CellAll" : "CellsHolders[" + column.NumInList + "]");
				gridColumn.Binding = binding;

				var res1 = gridConrol.FindResource("cellModelTemplate");
				gridColumn.CellTemplate = (DataTemplate)res1;

				var res2 = gridConrol.FindResource("cellModelStyle");
				gridColumn.CellStyle = (Style)res2;

				if (column.IsAllColumn)
				{
					var bindingIsShowColumnAll = new Binding(nameof(IsShowColumnAll));
					gridColumn.SetBinding(GridColumn.VisibleProperty, bindingIsShowColumnAll);
				}

				//gridColumn.HeaderStyle = (Style)gridConrol.FindResource("localGridHeaderStyleBoldItalic2222"); 
				//var styleHeader = new Style(typeof(GridColumnHeader));
				//styleHeader.Setters.Add(new Setter(GridColumnHeader.FontWeightProperty, FontWeights.Bold));
				////styleHeader.Setters.Add(new Setter(GridColumnHeader.BackgroundProperty, new SolidColorBrush(Colors.Transparent)));
				//styleHeader.Setters.Add(new Setter(GridColumnHeader.BackgroundProperty, new SolidColorBrush(Color.FromArgb((column.IsAllColumn ? (byte)0 : (byte)196), 255, 255, 255))));
				//gridColumn.HeaderStyle = styleHeader;

				//var headerTemplateName =
				//	column.IsAllColumn ? "HeaderAllColumnTemplate" :
				//	column.Holder.Entity.IsFamilyHead ? "HeaderHeaderColumnTemplate" :
				//	"HeaderMemberColumnTemplate";
				var headerTemplate = (DataTemplate)gridConrol.FindResource("HeaderHeaderColumnTemplate");
				gridColumn.HeaderTemplate = headerTemplate;

				//if (column.IsAllColumn)
				//if (column.NumInList.In(2,3))
				//{
				//	//var res3 = gridConrol.FindResource("localGridHeaderStyleItalic");
				//	//var res3 = gridConrol.FindResource("localGridHeaderStyleBold");
				//	var res3 = gridConrol.FindResource("localGridHeaderStyleBoldItalic");
				//	gridColumn.HeaderStyle = (Style)res3;
				//}

				gridColumn.Header = (column.IsAllColumn ? "For All" : column.Holder.Entity.FullName).ToUpper();
				gridColumn.Width = 280;
				gridColumn.Tag = column;

				gridConrol.Columns.Add(gridColumn);
			}
		}

		void UpdateIsShowColumnAll()
		{
			IsShowColumnAll = (AllRows.Any(q => q.IsAll));
		}



		void LoadPatients()
		{
			var holders = new ObservableCollection<Holder>(Entity.InsuranceCoverageHolders.Select(q => new Holder(this) { InsuranceCoverageHolder = q, Entity = q.Patient } ));
			var addFamilyMembers = OpenParam.FamilyMembers.Where(q => !holders.Any(z => z.Entity.RowId == q.RowId));
			holders.AddRange(addFamilyMembers.Select(q => new Holder(this) { Entity = q }));
			Holders = holders.OrderByDescending(q => q.Entity.IsFamilyHead).ThenBy(q => q.Entity.FullName).ToObservableCollection();
		}

		async Task LoadCategories()
		{
			await lookupsBusinessService.UpdateAllLookups();
			Categories = new ObservableCollection<Category>(
				LookupDataProvider.Instance.Categories
				.Where(q => q.IsServiceOrSuppy));
		}

		public virtual InsurancePatientCategoryInfo InsurancePatientCategoryInfo { get; set; } = new InsurancePatientCategoryInfo();
		async Task LoadInsurancePatientCategoryInfo()
		{
			var insuranceCoverageRowIds = new[] { Entity.RowId };
			InsurancePatientCategoryInfo = await businessService.GetInsurancePatientCategoryInfo(insuranceCoverageRowIds.ToWebQuery());
		}

		void UpdateFromInsurancePatientCategoryInfo(IEnumerable<Row> rows)
		{
			var highlightUse = (OpenParam.HighlightCategoryRowId != null && OpenParam.HighlightPatientRowId != null);
			var highlightCategoryRowId = (highlightUse ? (Guid)OpenParam.HighlightCategoryRowId : default(Guid));
			var highlightPatientRowId = (highlightUse ? (Guid)OpenParam.HighlightPatientRowId : default(Guid));
			var highlightRow = default(Row);
			var highlightColumn = default(Column);

			foreach (var row in rows)
			{
				var categoryRowIds = row.CategoryRowIds;
				if (row.IsAll)
				{
					var patientRowIds = row.CellsHolders.Where(q => q.IncludeInAll).Select(q => q.Holder.Entity.RowId).ToArray();
					var trows = InsurancePatientCategoryInfo.Find(Entity.RowId, patientRowIds.FirstOrDefault(), categoryRowIds.FirstOrDefault());
					var cell = row.CellAll;
					cell.UsedApproveAmount = trows.ApproveAmount;
					cell.UsedApproveUnits = trows.ApproveUnits;

					if (highlightUse)
					{
						if (categoryRowIds.Contains(highlightCategoryRowId) && patientRowIds.Contains(highlightPatientRowId))
						{
							cell.IsSelectedArticle = true;
							highlightRow = row;
						}
					}
				}
				else
				{
					foreach (var cell in row.CellsHolders)
					{
						var patientRowId = cell.Holder.Entity.RowId;
						var trows2 = InsurancePatientCategoryInfo.Find(Entity.RowId, patientRowId, categoryRowIds.FirstOrDefault());
						cell.UsedApproveAmount = trows2.ApproveAmount;
						cell.UsedApproveUnits = trows2.ApproveUnits;

						if (highlightUse)
						{
							if (categoryRowIds.Contains(highlightCategoryRowId) && patientRowId == highlightPatientRowId)
							{
								cell.IsSelectedArticle = true;
								highlightRow = row;
								highlightColumn = Columns.Single(q => q.Holder == cell.Holder);
							}
						}
					}
				}
			}

			if (highlightRow != null)
			{
				SelectedRow = highlightRow;
			}
			if (highlightColumn != null)
			{
				var gridColumn = BehaviorGridConrol.Control.Columns.Single(q => q.Tag == highlightColumn);
				BehaviorGridConrol.Control.CurrentColumn = gridColumn;
			}
		}


		void LoadAllRows()
		{
			var treeItems = new ObservableCollection<Cell>();

			var rows = new List<Row>();
			foreach (var insuranceCoverageItem in Entity.InsuranceCoverageItems)
			{
				var insuranceCoverageItemRowId = insuranceCoverageItem.RowId;
				var row = new Row(this)
				{
					CategoryRowIds = Entity.InsuranceCoverageItemCategories.Where(q => q.InsuranceCoverageItemRowId == insuranceCoverageItemRowId).Select(q => q.CategoryRowId).ToList(),
				};
				row.CellAll = new Cell(this, row, null);
				row.IsAll = insuranceCoverageItem.CoversAllHolders;
				if (row.IsAll)
				{
					row.CellAll.UpdateFrom(insuranceCoverageItem);
				}
				

				var insuranceCoverageItemHolders = Entity.InsuranceCoverageItemHolders.Where(q => q.InsuranceCoverageItemRowId == insuranceCoverageItemRowId).ToArray();
				foreach (var holder in Holders)
				{
					var cell = new Cell(this, row, holder);
					if (row.IsAll)
					{
						cell.IncludeInAll = false;
					}
					var insuranceCoverageItemHolder = insuranceCoverageItemHolders.SingleOrDefault(q => q.InsuranceCoverageHolderRowId == holder.InsuranceCoverageHolder?.RowId);
					if (insuranceCoverageItemHolder != null)
					{
						if (row.IsAll)
						{
							cell.IncludeInAll = true;
						}
						else
						{
							cell.UpdateFrom(insuranceCoverageItemHolder);
						}
					}
					row.CellsHolders.Add(cell);
				}

				rows.Add(row);
			}
			UpdateFromInsurancePatientCategoryInfo(rows);

			AllRows = rows.OrderBy(q => q.CategoriesNames).ToObservableCollection();
		}


		[ImplementPropertyChanged]
		public class Holder
		{
			public InsuranceCoverage2ViewModel ParentViewModel { get; set; }
			public Holder(InsuranceCoverage2ViewModel parentViewModel)
			{
				ParentViewModel = parentViewModel;
			}

			public Patient Entity { get; set; }
			public InsuranceCoverageHolder InsuranceCoverageHolder { get; set; }

			public Boolean IsAllEmpty { get; set; }

			public void Calculate()
			{
				bool isAllEmpty = true;
				foreach (var row in ParentViewModel.AllRows)
				{
					if (!row.AllCells.Single(q => q.Holder == this).IsEmpty)
					{
						isAllEmpty = false;
						break;
					}
				}
				IsAllEmpty = isAllEmpty;
			}
		}

		[ImplementPropertyChanged]
		public class Column
		{
			public InsuranceCoverage2ViewModel ParentViewModel { get; set; }
			public Column(InsuranceCoverage2ViewModel parentViewModel, Holder holder = null)
			{
				ParentViewModel = parentViewModel;
				Holder = holder;
			}

			public Holder Holder { get; set; }

			public Boolean IsAllColumn => (Holder == null);
			public Int32 NumInList => ParentViewModel.Holders.IndexOf(Holder);
		}
		public ObservableCollection<Column> Columns { get; set; } = new ObservableCollection<Column>();

		[ImplementPropertyChanged]
		public class Row
		{
			public InsuranceCoverage2ViewModel ParentViewModel { get; set; }
			public Row(InsuranceCoverage2ViewModel parentViewModel)
			{
				ParentViewModel = parentViewModel;
			}

			
			public Boolean IsAll { get; set; }

			public Cell CellAll { get; set; }
			public List<Cell> CellsHolders { get; set; } = new List<Cell>();
			public IEnumerable<Cell> AllCells => (new[] { CellAll }.Union(CellsHolders));

			public List<Guid> CategoryRowIds { get; set; }

			

			public string CategoriesNames => string.Join("; ", 
				ParentViewModel.Categories.Where(q => CategoryRowIds.Contains(q.RowId)).Select(q => q.FullName).OrderBy(q => q));

			public bool IsSupplyRow => CategoryRowIds.Any(q => LookupDataProvider.FindCategory(q).CategoryType == TypeHelper.CategoryType.Supply);


			public bool IsChanged { get; set; }

			public void OnIsAllChanged()
			{
				ParentViewModel.CalculateAll();
			}

			public void Calculate()
			{
				AllCells.ForEach(q => q.Calculate());
			}

			public List<String> Validate()
			{
				var errs = new List<String>();
				if (IsAll)
				{
					if (CellAll.IsEmpty)
					{
						errs.Add("Data <For All> for row \"" + CategoriesNames + "\" is empty");
					}
				}
				else
				{
					if (CellsHolders.All(q => q.IsEmpty))
					{
						errs.Add("Data for Treatment(s) \"" + CategoriesNames + "\" is empty");
					}
				}
				return errs;
			}
		}


		[ImplementPropertyChanged]
		public class Cell
		{
			public InsuranceCoverage2ViewModel ParentViewModel { get; set; }
			public Row Row { get; set; }
			public Holder Holder { get; set; }
			public Cell(InsuranceCoverage2ViewModel parentViewModel, Row row, Holder holder)
			{
				ParentViewModel = parentViewModel;
				Row = row;
				Holder = holder;
			}
			public DateTime? StartDate { get; set; }
			public DateTime? EndDate { get; set; }
			public Decimal? AnnualAmountCovered { get; set; }
			public Decimal? PercentageCovered { get; set; }
			public Decimal? HourlyRateCap { get; set; }
			public Boolean IsPrescriptionRequired { get; set; }
			public Decimal? PerVisitCost { get; set; }
			public Int32? MaximumVisits { get; set; }
			public Decimal? MaximumQuantity { get; set; }
			public Boolean IsSelectedArticle { get; set; }

			public Boolean IncludeInAll { get; set; } = true;

			public Boolean IsCellForAll => (Holder == null);
			public Boolean IsShowMainGrid { get; set; } = true;
			public Boolean IsShowSecondGrid { get; set; } = true;

			public Boolean VisibilityMaximumVisits => !Row.IsSupplyRow;
			public Boolean VisibilityMaximumQuantity => Row.IsSupplyRow;

			public String AdditionalInformation { get; set; }
			public Boolean HasAdditionalInformation => !String.IsNullOrEmpty(AdditionalInformation);


			public Decimal UsedApproveAmount { get; set; }
			public Decimal UsedApproveUnits { get; set; }
			public Decimal? UsedApproveUnitsFormated => (UsedApproveUnits == 0 && MaximumQuantity == null ? (decimal?)null : UsedApproveUnits);
			public Decimal? RemainedApproveAmount => (AnnualAmountCovered == null ? null : AnnualAmountCovered - UsedApproveAmount);
			public String RemainedApproveAmountForegroundColor => 
				RemainedApproveAmount == null ? "?" :
				RemainedApproveAmount >= 0 ? "Green" :
				"Red";


			public Boolean FieldsReadOnly { get; set; }

			public bool IsChanged
			{
				//get; set;
				set
				{
					var a = 100;
					if (value && Row != null)
					{
						Row.IsChanged = true;
					}
				}
			}


			public void Calculate()
			{
				IsShowMainGrid = (Row.IsAll == IsCellForAll);
				IsShowSecondGrid = (!IsCellForAll && Row.IsAll);
			}

			public bool IsEmptyCore =>
				StartDate == null && EndDate == null && AnnualAmountCovered == null && PercentageCovered == null && HourlyRateCap == null && !IsPrescriptionRequired && PerVisitCost == null && MaximumQuantity == null && MaximumVisits == null && AdditionalInformation == null;

			public bool IsEmpty =>
				IsCellForAll ? IsEmptyCore :
				Row.IsAll ? !IncludeInAll :
				IsEmptyCore;

			public void OnIsEmptyChanged()
			{
				ParentViewModel.CalculateAll();
			}

			public DelegateCommand OpenInsuranceArticleInfoCommand => new DelegateCommand(() =>
			{
				ParentViewModel.OpenInsuranceArticleInfo(this);
			});
		}

		void OpenInsuranceArticleInfo(Cell cell)
		{
			var categoryRowId = cell.Row.CategoryRowIds[0];
			var patientRowId = cell.IsCellForAll ?
				cell.Row.CellsHolders.FirstOrDefault(q => q.IncludeInAll)?.Holder?.Entity?.RowId :
				cell.Holder.Entity.RowId;
			if (patientRowId == null) return;

			var param = new InsuranceArticleInfoViewModel.OpenParams
			{
				InsuranceCoverageRowId = Entity.RowId,
				PatientRowId = (Guid)patientRowId,
				СategoryRowId = categoryRowId,
			};
			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.InsuranceArticleInfoView,
				Param = param,
			});
		}


		void CalculateAll()
		{
			UpdateIsShowColumnAll();
			AllRows.ForEach(q => q.Calculate());
			Holders.ForEach(q => q.Calculate());
		}



		void SubsribeInsuranceCoverageYearTypeChange()
		{
			(Entity as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(Entity.InsuranceCoverageYearType))
				{
					if (Entity.InsuranceCoverageYearType == TypeHelper.InsuranceCoverageYearType.CalendarYear)
					{
						if (Entity.CoverageStartDate == null && Entity.CoverageValidUntil == null)
						{
							Entity.CoverageStartDate = DateTimeHelper.FirstDayCurrentYear();
							Entity.CoverageValidUntil = DateTimeHelper.LastDayCurrentYear();
						}
					}
				}
			};
		}




		public void MouseClick(System.Windows.Input.MouseButtonEventArgs e)
		{
			DispatcherUIHelper.Run(async () =>
			{
				var hitInfo = BehaviorGridConrol.GetCalcHitInfo(e);
				if (hitInfo == null) return;

				if (hitInfo.Column?.Name == "categoryCellGridColumn")
				{
					var row = BehaviorGridConrol.GetRow<Row>(hitInfo.RowHandle);

					var ret2 = await PickCategoryViewModel.Pick(new PickCategoryViewModel.OpenParams
					{
						ShowDXWindowsInteractionRequest = ShowDXWindowsInteractionRequest,
						PickMode = PickCategoryViewModel.PickModeEnum.Main,
						Categories = Categories,
						SelectedCategoryRowIds = row.CategoryRowIds,
					});
					if (!ret2.IsSuccess) return;

					row.CategoryRowIds = ret2.Rows.Select(q => q.RowId).ToList();
				}
			});
		}


		public void CategoriesDelete()
		{
			var row = SelectedRow;
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var txt = "Do you want to delete \"" + row.CategoriesNames + "\"?";
			var ret = messageBoxService.ShowMessage(txt, CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				AllRows.Remove(row);
				Entity.IsChanged = true;
			}
		}
		public bool CanCategoriesDelete() => (SelectedRow != null);

		public void CategoriesNew()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var ret2 = await PickCategoryViewModel.Pick(new PickCategoryViewModel.OpenParams
				{
					ShowDXWindowsInteractionRequest = ShowDXWindowsInteractionRequest,
					PickMode = PickCategoryViewModel.PickModeEnum.Main,
					Categories = Categories,
					SelectedCategoryRowIds = new List<Guid>(),
				});
				if (!ret2.IsSuccess) return;

				var row = new Row(this)
				{
					CategoryRowIds = ret2.Rows.Select(q => q.RowId).ToList(),
				};
				row.CellAll = new Cell(this, row, null);
				row.CellsHolders = Holders.Select(q => new Cell(this, row, q)).ToList();
				AllRows.Add(row);
				SelectedRow = row;
				CalculateAll();
				Entity.IsChanged = true;
			});
		}

		public void MenuCellClear(GridCellMenuInfo info)
		{
			var column = info.Column.Tag as Column;
			var row = info.Row.Row as Row;

			var cell = row.AllCells.Single(q => q.IsCellForAll == column.IsAllColumn && q.Holder == column.Holder);

			cell.StartDate = null;
			cell.EndDate = null;
			cell.AnnualAmountCovered = null;
			cell.PercentageCovered = null;
			cell.HourlyRateCap = null;
			cell.IsPrescriptionRequired = false;
			cell.PerVisitCost = null;
			cell.MaximumVisits = null;
			cell.MaximumQuantity = null;
			cell.AdditionalInformation = null;
		}
		public bool CanMenuCellClear(GridCellMenuInfo info)
		{
			var column = info?.Column?.Tag as InsuranceCoverage2ViewModel.Column;
			var row = info?.Row?.Row as Row;
			return (column != null && row != null);
		}


		public void MenuCellCopyToAll(GridCellMenuInfo info)
		{
			var column = info.Column.Tag as Column;
			var row = info.Row.Row as Row;

			var copyCell = row.AllCells.Single(q => q.IsCellForAll == column.IsAllColumn && q.Holder == column.Holder);
			var otherCells = row.AllCells.Where(q => q != copyCell);

			foreach (var cell in otherCells)
			{
				cell.StartDate = copyCell.StartDate;
				cell.EndDate = copyCell.EndDate;
				cell.AnnualAmountCovered = copyCell.AnnualAmountCovered;
				cell.PercentageCovered = copyCell.PercentageCovered;
				cell.HourlyRateCap = copyCell.HourlyRateCap;
				cell.IsPrescriptionRequired = copyCell.IsPrescriptionRequired;
				cell.PerVisitCost = copyCell.PerVisitCost;
				cell.MaximumVisits = copyCell.MaximumVisits;
				cell.MaximumQuantity = copyCell.MaximumQuantity;
				cell.AdditionalInformation = copyCell.AdditionalInformation;
			}
		}
		public bool CanMenuCellCopyToAll(GridCellMenuInfo info)
		{
			var column = info?.Column?.Tag as InsuranceCoverage2ViewModel.Column;
			var row = info?.Row?.Row as Row;
			return (column != null && row != null && !column.IsAllColumn);
		}

		


		public void Close() => CloseCore();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		bool forceClose;
		void CloseCore(bool force = false)
		{
			this.forceClose = force;
			CloseInteractionRequest.Raise(null);
		}

		bool HasChange()
		{
			return (IsNew || Entity.IsChanged || AllRows.Any(q => q.IsChanged));
		}
		void ResetHasChange()
		{
			Entity.IsChanged = false;
			AllRows.ForEach(q => q.IsChanged = false);
		}


		async Task<bool> OnClose(bool showOKCancel = false)
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

		bool Validate()
		{
			var errors = new List<string>();


			if (!AllRows.Any())
			{
				errors.Add("Please complete treatment / equipment coverage details");
			}
			ValidateHelper.Empty(Entity.CoverageStartDate, "Start Date", errors);
			ValidateHelper.Empty(Entity.CoverageValidUntil, "End Date", errors);
			ValidateHelper.Empty(Entity.InsuranceProviderRowId, "Insurance Provider", errors);
			ValidateHelper.Empty(Entity.PolicyNumber, "Policy Number", errors);
			ValidateHelper.Empty(Entity.InsuranceProviderRowId, "Policy Owner", errors);

			foreach (var row in AllRows)
			{
				var errs = row.Validate();
				if (errs.Any())
				{
					errors.AddRange(errs);
					SelectedRow = row;
				}
			}


			if (errors.Count > 0)
			{
				var err = string.Join("\n", errors.ToArray());
				MessageBoxService.ShowMessage(err, CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
				return false;
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
			var updateEntity = Entity.GetPocoClone();

			//InsuranceCoverageHolders
			foreach (var holder in Holders.Where(q => !q.IsAllEmpty))
			{
				var patientRowId = holder.Entity.RowId;
				var insuranceCoverageHolder = new InsuranceCoverageHolder
				{
					RowId = Guid.NewGuid(),
					InsuranceCoverageRowId = Entity.RowId,
					PolicyHolderRowId = holder.Entity.RowId,
					PolicyHolderType = (Entity.PolicyOwnerRowId == patientRowId ? TypeHelper.PolicyHolderType.Owner : TypeHelper.PolicyHolderType.Beneficiary),
				};
				updateEntity.InsuranceCoverageHolders.Add(insuranceCoverageHolder);
			}
			var insuranceCoverageHolders = updateEntity.InsuranceCoverageHolders;

			//InsuranceCoverageItems
			foreach (var row in AllRows)
			{
				//InsuranceCoverageItems
				var insuranceCoverageItem = new InsuranceCoverageItem
				{
					RowId = Guid.NewGuid(),
					InsuranceCoverageRowId = Entity.RowId,
					CoversAllHolders = row.IsAll,
				};
				if (row.IsAll)
				{
					insuranceCoverageItem.UpdateFrom(row.CellAll);
				}
				updateEntity.InsuranceCoverageItems.Add(insuranceCoverageItem);

				//InsuranceCoverageItemHolders
				var cellsHolders = row.CellsHolders;
				foreach (var cellHolder in cellsHolders.Where(q => !q.IsEmpty))
				{
					var patientRowId = cellHolder.Holder.Entity.RowId;
					var insuranceCoverageItemHolder = new InsuranceCoverageItemHolder
					{
						RowId = Guid.NewGuid(),
						InsuranceCoverageItemRowId = insuranceCoverageItem.RowId,
						InsuranceCoverageHolderRowId = insuranceCoverageHolders.Single(q => q.PolicyHolderRowId == patientRowId).RowId,
					};
					if (!row.IsAll)
					{
						insuranceCoverageItemHolder.UpdateFrom(cellHolder);
					}
					updateEntity.InsuranceCoverageItemHolders.Add(insuranceCoverageItemHolder);
				}

				//InsuranceCoverageItemCategories
				foreach (var categoryRowId in row.CategoryRowIds)
				{
					var insuranceCoverageItemCategory = new InsuranceCoverageItemCategory
					{
						RowId = Guid.NewGuid(),
						InsuranceCoverageItemRowId = insuranceCoverageItem.RowId,
						CategoryRowId = categoryRowId,
					};
					updateEntity.InsuranceCoverageItemCategories.Add(insuranceCoverageItemCategory);
				}
			}


			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = IsNew ?
				await businessService.PostInsuranceCoverage(updateEntity) :
				await businessService.PutInsuranceCoverage(updateEntity);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService))
			{
				return false;
			}


			updateEntity.PolicyOwner = Holders.SingleOrDefault(q => q.Entity.RowId == Entity.PolicyOwnerRowId)?.Entity;
			MessengerHelper.SendMsgRowChange(updateEntity, IsNew);
			IsNew = false;
			ResetHasChange();

			//close
			if (andClose)
			{
				CloseCore(force: true);
			}

			return true;
		}


		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));



		public void CopyInsuranceCoverage()
		{
			DispatcherUIHelper.Run(async() =>
			{
				var txt = "Do you want to copy this Insurance Coverage?";
				var ret = MessageBoxService.ShowMessage(txt, CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret != MessageResult.Yes)
				{
					return;
				}

				if (!await OnClose(showOKCancel: true))
				{
					return;
				}


				ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Custom, "Copying...");
				var updateEntity = Entity.GetPocoClone();
				var uret = await businessService.CloneInsuranceCoverage(updateEntity);
				ShowWaitIndicator.Hide();
				if (!uret.Validate(MessageBoxService))
				{
					return;
				}

				var serverReturn = JsonConvert.DeserializeObject<ServerReturnCloneInsuranceCoverage>(uret.ResponseJson);
				var cloneRowId = serverReturn.CloneRowId;

				var newEntity = await businessService.GetInsuranceCoverage(cloneRowId);
				MessengerHelper.SendMsgRowChange(newEntity, true);

				CloseCore(force: true);

				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.InsuranceCoverage2WindowView,
					Param = new InsuranceCoverage2ViewModel.OpenParams
					{
						IsNew = false,
						RowId = cloneRowId,
					},
				});
			});
		}
	}

	static class InsuranceCoverage2ViewModelExtension
	{
		public static void UpdateFrom(this InsuranceCoverage2ViewModel.Cell dst, InsuranceCoverageItem src) => UpdateFromCore(dst, src);
		public static void UpdateFrom(this InsuranceCoverage2ViewModel.Cell dst, InsuranceCoverageItemHolder src) => UpdateFromCore(dst, src);
		public static void UpdateFrom(this InsuranceCoverageItem dst, InsuranceCoverage2ViewModel.Cell src) => UpdateFromCore(dst, src);
		public static void UpdateFrom(this InsuranceCoverageItemHolder dst, InsuranceCoverage2ViewModel.Cell src) => UpdateFromCore(dst, src);

		static void UpdateFromCore(object dst, object src)
		{
			InsuranceCoverage2ViewModel.Cell obj = null;
			foreach (var prop in new[] { nameof(obj.StartDate), nameof(obj.EndDate), nameof(obj.PercentageCovered), nameof(obj.HourlyRateCap),
										nameof(obj.IsPrescriptionRequired), nameof(obj.PerVisitCost), nameof(obj.AnnualAmountCovered),
										nameof(obj.MaximumVisits), nameof(obj.MaximumQuantity), nameof(obj.AdditionalInformation) })
			{
				var propDst = dst.GetType().GetProperty(prop);
				var propSrc = src.GetType().GetProperty(prop);
				var val = propSrc.GetValue(src);
				propDst.SetValue(dst, val);
			}
		}
	}

	public class InsuranceCoverage2ViewModelHeaderInfoConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter.Equals("ImageSource"))
			{
				var column = value as InsuranceCoverage2ViewModel.Column;
				if (column == null) return null;

				var image =
					column.IsAllColumn ? BitmapFunc.DXImage("Summary_16x16.png") :
					column.Holder.Entity.IsFamilyHead ? "pack://application:,,,/Profibiz.PracticeManager.Infrastructure;component/Resources/Header.png" :
					"pack://application:,,,/Profibiz.PracticeManager.Infrastructure;component/Resources/Member.png";
				return image;
			}
			else if (parameter.Equals("FontStyle"))
			{
				var fontStyle = (bool)value ? FontStyles.Italic : FontStyles.Normal;
				return fontStyle;
			}
			else if (parameter.Equals("FontWeight"))
			{
				var fontStyle = (bool)value ? FontWeights.Normal : FontWeights.Bold;
				return fontStyle;
			}
			else throw new ArgumentException();
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}


	//public class InsuranceCoverage2ViewModelMenuVisibleConverter : IValueConverter
	//{
	//	object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
	//	{
	//		if (parameter.Equals("Clear"))
	//		{
	//			var column = value as InsuranceCoverage2ViewModel.Column;
	//			if (column == null) return false;

	//			return true;
	//		}
	//		else if (parameter.Equals("Clear22"))
	//		{
	//			var column = value as InsuranceCoverage2ViewModel.Column;
	//			if (column == null) return false;

	//			return column.IsAllColumn;
	//		}
	//		else throw new ArgumentException();
	//	}
	//	object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	//	{
	//		return value;
	//	}
	//}

}





