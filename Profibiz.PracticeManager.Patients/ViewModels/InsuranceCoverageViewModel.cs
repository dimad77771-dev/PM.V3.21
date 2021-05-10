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

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class InsuranceCoverageViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		#endregion
		public enum ViewModeEnum { Full, Short }
		public ViewModeEnum ViewMode { get; set; } = ViewModeEnum.Full;
		public bool IsFullMode => (ViewMode == ViewModeEnum.Full);
		public bool IsReadOnly => (ViewMode != ViewModeEnum.Full);


		public bool IsNew { get; set; }
		public virtual InsuranceCoverage Entity { get; set; }
		public virtual ObservableCollection<TreeItem> TreeItems { get; set; } = new ObservableCollection<TreeItem>();
		public virtual TreeItem SelectedTreeItem { get; set; }
		public virtual TreeListViewShowingEditorResponse TreeShowingEditor { get; set; } = new TreeListViewShowingEditorResponse();
		public virtual ObservableCollection<CategoryRow> Categories { get; set; }
		public virtual CategoryRow SelectedCategory { get; set; }
		public virtual ObservableCollection<Holder> Holders { get; set; }
		public virtual Holder SelectedHolder { get; set; }

		public enum Mode2Enum { None, All, Individual }
		public enum RowTypeEnum { Service, Pathient }

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
			}
			Mode2List = EnumFunc.GetValues<Mode2Enum>().Where(q => q != Mode2Enum.None).Select(q => new Mode2ListItem { Value = q }).ToObservableCollection();
			TreeShowingEditor.OnShowingEditor += TreeShowingEditor_OnShowingEditor;
			await LoadCategories();
			LoadPatients();
			LoadTreeItems();
			CalculateAll();
			SubsribeInsuranceCoverageYearTypeChange();
			ResetHasChange();

			ShowWaitIndicator.Hide();
		}


		public InsuranceCoverageViewModel() : base()
		{
			var b = 100;
		}

		[ImplementPropertyChanged]
		public class Mode2ListItem
		{
			public Mode2Enum Value { get; set; }
			public String Name => (Value == Mode2Enum.None ? "" : Value.ToString());
		}
		public virtual ObservableCollection<Mode2ListItem> Mode2List { get; set; }


		[ImplementPropertyChanged]
		public class Holder
		{
			public Patient Entity { get; set; }
			public Boolean InInsuranceCoverage { get; set; }
			public String DragDropRowText => Entity.FullName;

			public static Holder FromEntity(Patient entity) => new Holder { Entity = entity };
		}

		[ImplementPropertyChanged]
		public class CategoryRow
		{
			public Category Entity { get; set; }
			public String DragDropRowText => Entity.FullName;
		}


		void LoadPatients()
		{
			var holders = new ObservableCollection<Holder>(Entity.InsuranceCoverageHolders.Select(q => q.Patient).Select(q => Holder.FromEntity(q)));
			var addFamilyMembers = OpenParam.FamilyMembers.Where(q => !holders.Any(z => z.Entity.RowId == q.RowId));
			holders.AddRange(addFamilyMembers.Select(q => Holder.FromEntity(q)));

			Holders = holders;//.OrderByDescending(q => q.InInsuranceCoverage).ThenBy(q => q.Entity.FamilyMemberType).ThenBy(q => q.Entity.FullName).ToObservableCollection();
		}

		void LoadTreeItems()
		{
			var treeItems = new ObservableCollection<TreeItem>();

			foreach (var coverageService in Entity.InsuranceCoverageServices)
			{
				var item = new TreeItem(this)
				{
					RowType = RowTypeEnum.Service,
					CategoryRowId = coverageService.CategoryRowId,
					Mode2 = (coverageService.CoversAllHolders ? Mode2Enum.All : Mode2Enum.Individual),
				};
				item.IsExpanded = true;
				if (coverageService.CoversAllHolders)
				{
					item.CoverageStartDate = coverageService.StartDate;
					item.CoverageValidUntil = coverageService.EndDate;
					item.PercentageCovered = coverageService.PercentageCovered;
					item.HourlyRateCap = coverageService.HourlyRateCap;
					item.IsPrescriptionRequired = coverageService.IsPrescriptionRequired;
					item.PerVisitCost = coverageService.PerVisitCost;
					item.AnnualAmountCovered = coverageService.AnnualAmountCovered;
				}
				treeItems.Add(item);

				foreach (var mapRow in Entity.InsuranceCoverageHolderServices.Where(q => q.InsuranceCoverageServiceRowId == coverageService.RowId))
				{
					var holderRowId = Entity.InsuranceCoverageHolders.Single(q => q.RowId == mapRow.InsuranceCoverageHolderRowId).PolicyHolderRowId;
					var item2 = new TreeItem(this)
					{
						RowType = RowTypeEnum.Pathient,
						HolderRowId = holderRowId,
						ParentId = item.Id,
					};
					if (!coverageService.CoversAllHolders)
					{
						item2.CoverageStartDate = mapRow.StartDate;
						item2.CoverageValidUntil = mapRow.EndDate;
						item2.PercentageCovered = mapRow.PercentageCovered;
						item2.HourlyRateCap = mapRow.HourlyRateCap;
						item2.IsPrescriptionRequired = mapRow.IsPrescriptionRequired;
						item2.PerVisitCost = mapRow.PerVisitCost;
						item2.AnnualAmountCovered = mapRow.AnnualAmountCovered;
					}
					treeItems.Add(item2);
				}
			}

			TreeItems = treeItems;
		}

		async Task LoadCategories()
		{
			await lookupsBusinessService.UpdateAllLookups();
			Categories = new ObservableCollection<CategoryRow>(
				LookupDataProvider.Instance.Categories
				.Where(q => q.IsServiceOrSuppy)
				.Select(q => new CategoryRow { Entity = q }));
		}



		[ImplementPropertyChanged]
		public class TreeItem
		{
			public TreeItem(InsuranceCoverageViewModel parentViewModel)
			{
				ParentViewModel = parentViewModel;
				Id = Guid.NewGuid();
			}

			public Guid Id { get; set; }
			public Guid ParentId { get; set; }
			public RowTypeEnum RowType { get; set; }
			public String ObjName { get; set; }
			public String Rowtype9 { get; set; }
			public Mode2Enum Mode2 { get; set; }
			public Boolean FieldsReadOnly { get; set; }

			public Guid HolderRowId { get; set; }
			public Guid CategoryRowId { get; set; }

			public DateTime? CoverageStartDate { get; set; }
			public DateTime? CoverageValidUntil { get; set; }
			public Decimal? PercentageCovered { get; set; }
			public Decimal? HourlyRateCap { get; set; }
			public Boolean IsPrescriptionRequired { get; set; }
			public Decimal? PerVisitCost { get; set; }
			public Decimal? AnnualAmountCovered { get; set; }

			public InsuranceCoverageViewModel ParentViewModel { get; set; }
			public String DragDropRowText => (RowType == RowTypeEnum.Service ? ObjName : ObjName);
			public Boolean IsExpanded { get; set; }

			public bool IsChanged { get; set; }

			public void OnMode2Changed()
			{
				ParentViewModel.UpdateTreeItemCalculateFields();
			}
		}

		void CalculateAll()
		{
			UpdateTreeItemCalculateFields();
			UpdateHoldersCalculateFields();
		}

		void UpdateHoldersCalculateFields()
		{
			foreach (var row in Holders)
			{
				row.InInsuranceCoverage = TreeItems.Any(q => q.HolderRowId == row.Entity.RowId);
			}
		}

		void UpdateTreeItemCalculateFields()
		{
			foreach (var item in TreeItems)
			{
				//ObjName, Rowtype9
				if (item.RowType == RowTypeEnum.Service)
				{
					var model = Categories.Single(q => q.Entity.RowId == item.CategoryRowId).Entity;
					item.ObjName = model.FullName;
					item.Rowtype9 = model.Rowtype9;
				}
				else if (item.RowType == RowTypeEnum.Pathient)
				{
					var model = Holders.Single(q => q.Entity.RowId == item.HolderRowId).Entity;
					item.ObjName = model.FullName;
					item.Rowtype9 = model.Rowtype9;
				}


				//FieldsReadOnly
				var fieldsReadOnly = true;
				if (item.RowType == RowTypeEnum.Service)
				{
					if (item.Mode2 == Mode2Enum.All)
					{
						fieldsReadOnly = false;
					}
				}
				else if (item.RowType == RowTypeEnum.Pathient)
				{
					var pitem = TreeItems.Single(q => q.Id == item.ParentId);
					if (pitem.Mode2 == Mode2Enum.Individual)
					{
						fieldsReadOnly = false;
					}
				}
				item.FieldsReadOnly = fieldsReadOnly;
				if (fieldsReadOnly)
				{
					item.CoverageStartDate = null;
					item.CoverageValidUntil = null;
					item.PercentageCovered = null;
					item.HourlyRateCap = null;
					item.IsPrescriptionRequired = false;
					item.PerVisitCost = null;
					item.AnnualAmountCovered = null;
				}

				//if (!fieldsReadOnly)
				//{
				//	if (Entity.InsuranceCoverageYearType == TypeHelper.InsuranceCoverageYearType.CalendarYear)
				//	{
				//		if (item.CoverageStartDate == null && item.CoverageValidUntil == null)
				//		{
				//			item.CoverageStartDate = DateTimeHelper.FirstDayCurrentYear();
				//			item.CoverageValidUntil = DateTimeHelper.LastDayCurrentYear();
				//		}
				//	}
				//}
			}
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
					//CalculateAll();
				}
			};
		}

		//void Calculate


		private void TreeShowingEditor_OnShowingEditor(object sender, TreeListViewShowingEditorResponseEventArgs e)
		{
			var row = (TreeItem)e.RowModel;
			var columns = new[] {
				"AnnualAmountCovered", "PercentageCovered", "HourlyRateCap", "IsPrescriptionRequired", "PerVisitCost",
				"CoverageStartDate", "CoverageValidUntil" };
			if (columns.Contains(e.FieldName) && row.FieldsReadOnly)
			{
				e.Cancel = true;
			}
			else if (e.FieldName == "Mode2" && row.RowType == RowTypeEnum.Pathient)
			{
				e.Cancel = true;
			}
		}






		public virtual TreeListDragDropManagerEx TreeDragDropManager { get; set; }
		public virtual void OnTreeDragDropManagerChanged(TreeListDragDropManagerEx old)
		{
			if (TreeDragDropManager != old)
			{
				TreeDragDropManager.DragOver += TreeDragDropManager_DragOver;
				TreeDragDropManager.Drop += TreeDragDropManager_Drop;
			}
		}
		void TreeDragDropManager_DragOver(object sender, TreeListDragOverEventArgs e)
		{
			if (IsReadOnly)
			{
				e.AllowDrop = false;
				e.Handled = true;
				return;
			}

			var dragRow = DragDropHelper.GetDraggedRow(e);

			var allowDrop = false;
			var targetRow = e.TargetNode?.Content as TreeItem;
			if (dragRow is CategoryRow)
			{
				if (e.DropTargetType == DropTargetType.InsertRowsIntoNode || e.DropTargetType == DropTargetType.None)
				{
					allowDrop = true;
				}
			}
			else if (dragRow is Holder)
			{
				if (e.DropTargetType == DropTargetType.InsertRowsIntoNode && targetRow != null && targetRow.RowType == RowTypeEnum.Service)
				{
					allowDrop = true;
				}
			}

			if (!allowDrop)
			{
				e.AllowDrop = false;
				e.Handled = true;
			}
		}
		void TreeDragDropManager_Drop(object sender, TreeListDropEventArgs e)
		{
			e.Handled = true;
			var dragRow = DragDropHelper.GetDraggedRow(e);
			var targetRow = e.TargetNode?.Content as TreeItem;

			if (dragRow is CategoryRow)
			{
				var drow = dragRow as CategoryRow;
				var item = new TreeItem(this)
				{
					RowType = RowTypeEnum.Service,
					CategoryRowId = drow.Entity.RowId,
					Mode2 = Mode2Enum.Individual,
				};
				item.IsExpanded = true;
				TreeItems.Add(item);

				foreach (var holder in Holders)//.Where(q => q.InInsuranceCoverage))
				{
					var item2 = new TreeItem(this)
					{
						RowType = RowTypeEnum.Pathient,
						HolderRowId = holder.Entity.RowId,
						ParentId = item.Id,
					};
					TreeItems.Add(item2);
				}
				CalculateAll();
				Entity.IsChanged = true;
				SelectedTreeItem = item;
			}
			else if (dragRow is Holder)
			{
				var drow = dragRow as Holder;
				if (TreeItems.Any(q => q.ParentId == targetRow.Id && q.HolderRowId == drow.Entity.RowId))
				{
					var ret = MessageBoxService.ShowMessage("\"" + drow.Entity.FullName + "\" already in service", CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
					return;
				}
				var item2 = new TreeItem(this)
				{
					RowType = RowTypeEnum.Pathient,
					HolderRowId = drow.Entity.RowId,
					ParentId = targetRow.Id,
				};
				TreeItems.Add(item2);
				CalculateAll();
			}
		}


		public virtual GridDragDropManagerEx DragDropManagerHolders { get; set; }
		public virtual void OnDragDropManagerHoldersChanged(GridDragDropManagerEx old)
		{
			if (old == null)
			{
				DragDropManagerHolders.DragOver += DragDropManagerHolders_DragOver;
				DragDropManagerHolders.Drop += DragDropManagerHolders_Drop;
			}
		}
		void DragDropManagerHolders_DragOver(object sender, GridDragOverEventArgs e)
		{
			var dragRow = DragDropHelper.GetDraggedTreeRow(e) as TreeItem;

			var allowDrop = false;
			var targetRow = e.TargetRow as Holder;
			if (dragRow != null && dragRow.RowType == RowTypeEnum.Pathient)
			{
				allowDrop = true;
			}

			if (!allowDrop)
			{
				e.AllowDrop = false;
				e.Handled = true;
			}
		}
		void DragDropManagerHolders_Drop(object sender, GridDropEventArgs e)
		{
			e.Handled = true;
			var dragRow = DragDropHelper.GetDraggedTreeRow(e) as TreeItem;
			TreeItems.Remove(dragRow);
			CalculateAll();
			Entity.IsChanged = true;
		}



		public virtual GridDragDropManagerEx DragDropManagerCategories { get; set; }
		public virtual void OnDragDropManagerCategoriesChanged(GridDragDropManagerEx old)
		{
			if (old == null)
			{
				DragDropManagerCategories.DragOver += DragDropManagerCategories_DragOver;
				DragDropManagerCategories.Drop += DragDropManagerCategories_Drop;
			}
		}
		private void DragDropManagerCategories_DragOver(object sender, GridDragOverEventArgs e)
		{
			var dragRow = DragDropHelper.GetDraggedTreeRow(e) as TreeItem;

			var allowDrop = false;
			var targetRow = e.TargetRow as CategoryRow;
			if (dragRow != null && dragRow.RowType == RowTypeEnum.Service)
			{
				allowDrop = true;
			}

			if (!allowDrop)
			{
				e.AllowDrop = false;
				e.Handled = true;
			}
		}
		private void DragDropManagerCategories_Drop(object sender, GridDropEventArgs e)
		{
			e.Handled = true;
			var dragRow = DragDropHelper.GetDraggedTreeRow(e) as TreeItem;
			foreach (var crow in TreeItems.Where(q => q.ParentId == dragRow.Id).ToArray())
			{
				TreeItems.Remove(crow);
			}
			TreeItems.Remove(dragRow);
			CalculateAll();
			Entity.IsChanged = true;
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
			return (IsNew || Entity.IsChanged || TreeItems.Any(q => q.IsChanged));
		}
		void ResetHasChange()
		{
			//MainModel.IsErrorInfoWork = false;
			Entity.IsChanged = false;
			TreeItems.ForEach(q => q.IsChanged = false);
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


			//if (!TreeItems.Any())
			//{
			//	errors.Add("Rows is empty");//???
			//}
			ValidateHelper.Empty(Entity.InsuranceProviderRowId, "Insurance Provider", errors);
			ValidateHelper.Empty(Entity.PolicyNumber, "Policy Number", errors);
			ValidateHelper.Empty(Entity.InsuranceProviderRowId, "Policy Owner", errors);

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
			foreach (var holder in Holders.Where(q => TreeItems.Select(z => z.HolderRowId).Contains(q.Entity.RowId)))
			{
				var holderRowId = holder.Entity.RowId;
				var insuranceCoverageHolder = new InsuranceCoverageHolder
				{
					RowId = Guid.NewGuid(),
					InsuranceCoverageRowId = Entity.RowId,
					PolicyHolderRowId = holder.Entity.RowId,
					PolicyHolderType = (Entity.PolicyOwnerRowId == holderRowId ? TypeHelper.PolicyHolderType.Owner : TypeHelper.PolicyHolderType.Beneficiary),
				};
				updateEntity.InsuranceCoverageHolders.Add(insuranceCoverageHolder);
			}

			//InsuranceCoverageServices, InsuranceCoverageHolderServices
			foreach (var serviceRow in TreeItems.Where(q => q.RowType == RowTypeEnum.Service))
			{
				var insuranceCoverageService = new InsuranceCoverageService
				{
					RowId = Guid.NewGuid(),
					InsuranceCoverageRowId = Entity.RowId,
					CategoryRowId = serviceRow.CategoryRowId,
					CoversAllHolders = (serviceRow.Mode2 == Mode2Enum.All ? true : false),
					StartDate = serviceRow.CoverageStartDate,
					EndDate = serviceRow.CoverageValidUntil,
					AnnualAmountCovered = serviceRow.AnnualAmountCovered,
					PercentageCovered = serviceRow.PercentageCovered,
					HourlyRateCap = serviceRow.HourlyRateCap,
					IsPrescriptionRequired = serviceRow.IsPrescriptionRequired,
					PerVisitCost = serviceRow.PerVisitCost,
				};
				updateEntity.InsuranceCoverageServices.Add(insuranceCoverageService);

				foreach (var pathientRow in TreeItems.Where(q => q.RowType == RowTypeEnum.Pathient && q.ParentId == serviceRow.Id))
				{
					var insuranceCoverageHolderService = new InsuranceCoverageHolderService
					{
						RowId = Guid.NewGuid(),
						InsuranceCoverageServiceRowId = insuranceCoverageService.RowId,
						InsuranceCoverageHolderRowId = updateEntity.InsuranceCoverageHolders.Single(q => q.PolicyHolderRowId == pathientRow.HolderRowId).RowId,
						StartDate = pathientRow.CoverageStartDate,
						EndDate = pathientRow.CoverageValidUntil,
						AnnualAmountCovered = pathientRow.AnnualAmountCovered,
						PercentageCovered = pathientRow.PercentageCovered,
						HourlyRateCap = pathientRow.HourlyRateCap,
						IsPrescriptionRequired = pathientRow.IsPrescriptionRequired,
						PerVisitCost = pathientRow.PerVisitCost,
					};
					updateEntity.InsuranceCoverageHolderServices.Add(insuranceCoverageHolderService);
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
	}
}




