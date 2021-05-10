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
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using Microsoft.Practices.ServiceLocation;
using Profibiz.PracticeManager.Patients.BusinessService;

namespace Profibiz.PracticeManager.Navigation.ViewModels
{
	[POCOViewModel]
	public class InsuranceProvidersViewGroupsViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		#endregion

		public virtual InsuranceProvider MainEntity { get; set; }
		public virtual ObservableCollection<InsuranceProvidersViewGroup> InsuranceProvidersViewGroups { get; set; }
		public virtual InsuranceProvidersViewGroup SelectedInsuranceProvidersViewGroup { get; set; }
		public virtual ObservableCollection<InsuranceProvider> InsuranceProviders { get; set; }
		public virtual InsuranceProvider SelectedInsuranceProvider { get; set; }

		public virtual ObservableCollection<TreeItem> TreeItems { get; set; } = new ObservableCollection<TreeItem>();
		public virtual TreeItem SelectedTreeItem { get; set; }
		public enum RowTypeEnum { ViewGroup, InsuranceProvider }

		public bool IsTreeItemsChanged;

		//public virtual GridControl bbb { get; set; }
		public virtual GridControlBehaviorManager BehaviorGridConrolInsuranceProvidersViewGroups { get; set; } = new GridControlBehaviorManager();



		public InsuranceProvidersViewGroupsViewModel(InsuranceProvider mainEntity) : base()
		{
			businessService = ServiceHelper.GetInstance<IPatientsBusinessService>();
			lookupsBusinessService = ServiceHelper.GetInstance<ILookupsBusinessService>();
			MainEntity = mainEntity;
		}


		async public Task LoadData()
		{
			//ShowWaitIndicator.Show();

			var vgroupTask = lookupsBusinessService.GetInsuranceProvidersViewGroups();
			await lookupsBusinessService.RunTaskAndUpdateAllLookups(vgroupTask);

			InsuranceProvidersViewGroups = (await vgroupTask).ToObservableCollection();
			InsuranceProvidersViewGroups.ForEach(q => AddPropertyChangedInsuranceProvidersViewGroup(q));
			InsuranceProviders = new ObservableCollection<InsuranceProvider>(LookupDataProvider.Instance.InsuranceProviders);

			LoadTreeItems();
			CalculateAll();
			ResetHasChange();

			//ShowWaitIndicator.Hide();
		}




		void AddPropertyChangedInsuranceProvidersViewGroup(InsuranceProvidersViewGroup row)
		{
			var irow = (row as INotifyPropertyChanged);
			irow.PropertyChanged += (s, e) => OnChangeInsuranceProvidersViewGroup(s as InsuranceProvidersViewGroup, e);
		}

		void OnChangeInsuranceProvidersViewGroup(InsuranceProvidersViewGroup row, PropertyChangedEventArgs e)
		{
			CalculateAll();	
		}

		void LoadTreeItems()
		{
			var treeItems = new ObservableCollection<TreeItem>();
			InsuranceProvidersViewGroups
				.Where(q => q.InsuranceProvidersViewGroupMappings.Any(z => z.InsuranceProviderRowId == MainEntity.RowId))
				.ForEach(q => AddViewGroupToTree(q, treeItems));
			TreeItems = treeItems;
		}

		TreeItem AddViewGroupToTree(InsuranceProvidersViewGroup viewGroup, ObservableCollection<TreeItem> treeItems)
		{
			var item = new TreeItem(this)
			{
				RowType = RowTypeEnum.ViewGroup,
				InsuranceProvidersViewGroupRowId = viewGroup.RowId,
			};
			item.IsExpanded = true;
			treeItems.Add(item);

			foreach (var mapRow in viewGroup.InsuranceProvidersViewGroupMappings)
			{
				var item2 = new TreeItem(this)
				{
					RowType = RowTypeEnum.InsuranceProvider,
					InsuranceProviderRowId = mapRow.InsuranceProviderRowId,
					ParentId = item.Id,
				};
				item2.IsExpanded = true;
				treeItems.Add(item2);
			}

			return item;
		}




		[ImplementPropertyChanged]
		public class TreeItem
		{
			public TreeItem(InsuranceProvidersViewGroupsViewModel parentViewModel)
			{
				ParentViewModel = parentViewModel;
				Id = Guid.NewGuid();
			}

			public Guid Id { get; set; }
			public Guid ParentId { get; set; }

			public RowTypeEnum RowType { get; set; }
			public String ObjName { get; set; }
			public String Rowtype9 { get; set; }
			public String ImageForRow => 
				RowType == RowTypeEnum.InsuranceProvider ? insuranceProviderRowtype9ToImage :
				RowType == RowTypeEnum.ViewGroup ? insuranceProvidersViewGroupRowtype9ToImage : "";

			public Guid InsuranceProvidersViewGroupRowId { get; set; }
			public Guid InsuranceProviderRowId { get; set; }

			public InsuranceProvidersViewGroupsViewModel ParentViewModel { get; set; }
			public String DragDropRowText => ObjName;
			public Boolean IsExpanded { get; set; }

			public bool IsChanged { get; set; }

			static string insuranceProvidersViewGroupRowtype9ToImage = ResourceHelper.ConvertByResource<string>("-", "insuranceProvidersViewGroupRowtype9ToImageConverter");
			static string insuranceProviderRowtype9ToImage = ResourceHelper.ConvertByResource<string>("-", "insuranceProviderRowtype9ToImageConverter");
		}


		void CalculateAll()
		{
			UpdateTreeItemCalculateFields();
		}

		void UpdateTreeItemCalculateFields()
		{
			foreach (var item in TreeItems)
			{
				if (item.RowType == RowTypeEnum.ViewGroup)
				{
					var model = InsuranceProvidersViewGroups.Single(q => q.RowId == item.InsuranceProvidersViewGroupRowId);
					item.ObjName = model.Name;
					item.Rowtype9 = model.Rowtype9;
				}
				else if (item.RowType == RowTypeEnum.InsuranceProvider)
				{
					var model = InsuranceProviders.Single(q => q.RowId == item.InsuranceProviderRowId);
					item.ObjName = model.CompanyName;
					item.Rowtype9 = model.Rowtype9;
				}
			}
		}










		public virtual TreeListDragDropManagerEx TreeDragDropManager { get; set; }
		public virtual void OnTreeDragDropManagerChanged(TreeListDragDropManagerEx old)
		{
			if (old == null)
			{
				TreeDragDropManager.DragOver += TreeDragDropManager_DragOver;
				TreeDragDropManager.Drop += TreeDragDropManager_Drop;
			}
		}
		void TreeDragDropManager_DragOver(object sender, TreeListDragOverEventArgs e)
		{
			var dragRow = DragDropHelper.GetDraggedRow(e);

			var allowDrop = false;
			var targetRow = e.TargetNode?.Content as TreeItem;
			if (dragRow is InsuranceProvidersViewGroup)
			{
				if (e.DropTargetType == DropTargetType.InsertRowsIntoNode || e.DropTargetType == DropTargetType.None)
				{
					allowDrop = true;
				}
			}
			else if (dragRow is InsuranceProvider)
			{
				if (e.DropTargetType == DropTargetType.InsertRowsIntoNode && targetRow != null && targetRow.RowType == RowTypeEnum.ViewGroup)
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

			if (dragRow is InsuranceProvidersViewGroup)
			{
				var drow = dragRow as InsuranceProvidersViewGroup;
				if (TreeItems.Any(q => q.RowType == RowTypeEnum.ViewGroup && q.InsuranceProvidersViewGroupRowId == drow.RowId))
				{
					var ret = MessageBoxService.ShowMessage("View group \"" + drow.Name + "\" already in list", CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
					return;
				}
				var item = AddViewGroupToTree(drow, TreeItems);
				CalculateAll();
				SelectedTreeItem = item;
				IsTreeItemsChanged = true;
			}
			else if (dragRow is InsuranceProvider)
			{
				var drow = dragRow as InsuranceProvider;
				if (TreeItems.Any(q => q.ParentId == targetRow.Id && q.InsuranceProviderRowId == drow.RowId))
				{
					var ret = MessageBoxService.ShowMessage("\"" + drow.CompanyName + "\" already in view group", CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
					return;
				}

				var insuranceProvidersViewGroupRowId = targetRow.InsuranceProvidersViewGroupRowId;
				var mrow = new InsuranceProvidersViewGroupMapping
				{
					RowId = Guid.NewGuid(),
					InsuranceProviderRowId = drow.RowId,
					InsuranceProvidersViewGroupRowId = targetRow.InsuranceProvidersViewGroupRowId,
				};
				InsuranceProvidersViewGroups.Single(q => q.RowId == insuranceProvidersViewGroupRowId).InsuranceProvidersViewGroupMappings.Add(mrow);


				var item2 = new TreeItem(this)
				{
					RowType = RowTypeEnum.InsuranceProvider,
					InsuranceProviderRowId = drow.RowId,
					ParentId = targetRow.Id,
				};
				TreeItems.Add(item2);
				targetRow.IsExpanded = false;
				targetRow.IsExpanded = true;
				CalculateAll();
				IsTreeItemsChanged = true;
			}
		}


		public virtual GridDragDropManagerEx DragDropManagerViewGroups { get; set; }
		public virtual void OnDragDropManagerViewGroupsChanged(GridDragDropManagerEx old)
		{
			if (old == null)
			{
				DragDropManagerViewGroups.DragOver += DragDropManagerViewGroups_DragOver;
				DragDropManagerViewGroups.Drop += DragDropManagerViewGroups_Drop;
			}
		}
		void DragDropManagerViewGroups_DragOver(object sender, GridDragOverEventArgs e)
		{
			var dragRow = DragDropHelper.GetDraggedTreeRow(e) as TreeItem;

			var allowDrop = false;
			var targetRow = e.TargetRow as InsuranceProvidersViewGroup;
			if (dragRow != null && dragRow.RowType == RowTypeEnum.ViewGroup)
			{
				allowDrop = true;
			}

			if (!allowDrop)
			{
				e.AllowDrop = false;
				e.Handled = true;
			}
		}
		void DragDropManagerViewGroups_Drop(object sender, GridDropEventArgs e)
		{
			e.Handled = true;
			var item = DragDropHelper.GetDraggedTreeRow(e) as TreeItem;
			var item2 = TreeItems.Where(q => q.ParentId == item.Id);

			TreeItems.Remove(item);
			TreeItems.RemoveRange(item2);

			CalculateAll();
			IsTreeItemsChanged = true;
		}



		public virtual GridDragDropManagerEx DragDropManagerInsuranceProviders { get; set; }
		public virtual void OnDragDropManagerInsuranceProvidersChanged(GridDragDropManagerEx old)
		{
			if (old == null)
			{
				DragDropManagerInsuranceProviders.DragOver += DragDropManagerInsuranceProviders_DragOver;
				DragDropManagerInsuranceProviders.Drop += DragDropManagerInsuranceProviders_Drop;
			}
		}
		private void DragDropManagerInsuranceProviders_DragOver(object sender, GridDragOverEventArgs e)
		{
			var dragRow = DragDropHelper.GetDraggedTreeRow(e) as TreeItem;

			var allowDrop = false;
			var targetRow = e.TargetRow as InsuranceProvidersViewGroup;
			if (dragRow != null && dragRow.RowType == RowTypeEnum.InsuranceProvider)
			{
				allowDrop = true;
			}

			if (!allowDrop)
			{
				e.AllowDrop = false;
				e.Handled = true;
			}
		}
		private void DragDropManagerInsuranceProviders_Drop(object sender, GridDropEventArgs e)
		{
			e.Handled = true;
			var item2 = DragDropHelper.GetDraggedTreeRow(e) as TreeItem;
			var item = TreeItems.Single(q => q.Id == item2.ParentId);

			var vrow = InsuranceProvidersViewGroups.Single(q => q.RowId == item.InsuranceProvidersViewGroupRowId);
			var mrow = vrow.InsuranceProvidersViewGroupMappings.Single(q => q.InsuranceProviderRowId == item2.InsuranceProviderRowId);
			vrow.InsuranceProvidersViewGroupMappings.Remove(mrow);

			TreeItems.Remove(item2);
			CalculateAll();
			IsTreeItemsChanged = true;
		}



		public void Close() => CloseCore();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		bool forceClose;
		void CloseCore(bool force = false)
		{
			this.forceClose = force;
			CloseInteractionRequest.Raise(null);
		}

		public bool HasChange()
		{
			return (IsTreeItemsChanged || InsuranceProvidersViewGroups.Any(q => q.IsChanged));
		}
		public void ResetHasChange()
		{
			IsTreeItemsChanged = false;
			InsuranceProvidersViewGroups.ForEach(q => q.IsChanged = false);
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


			if (errors.Count > 0)
			{
				var err = string.Join("\n", errors.ToArray());
				MessageBoxService.ShowMessage(err, CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
				return false;
			}

			return true;
		}

		public async Task<bool> SaveCore(bool andClose)
		{
			//validate
			if (!Validate())
			{
				return false;
			}

			//updateEntity
			var updateEntity = InsuranceProvidersViewGroups.Select(q =>
			{
				var row = q.GetPocoClone();
				row.InsuranceProvidersViewGroupMappings = q.InsuranceProvidersViewGroupMappings.Select(z => z.GetPocoClone()).ToObservableCollection();
				return row;
			}).ToList();


			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = await lookupsBusinessService.PutInsuranceProvidersViewGroups(updateEntity);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService))
			{
				return false;
			}


			//updateEntity.PolicyOwner = InsuranceProviders.SingleOrDefault(q => q.Entity.RowId == Entity.PolicyOwnerRowId)?.Entity;
			//MessengerHelper.SendMsgRowChange(updateEntity, IsNew);
			ResetHasChange();

			return true;
		}



		public bool CanDeleteViewGroup()
		{
			return (SelectedInsuranceProvidersViewGroup != null);
		}

		public void DeleteViewGroup()
		{
			var row = SelectedInsuranceProvidersViewGroup;
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var ret = messageBoxService.ShowMessage("Do you want to delete view group \"" + row.Name + "\"?", CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				InsuranceProvidersViewGroups.Remove(row);
				var item = TreeItems.SingleOrDefault(q => q.InsuranceProvidersViewGroupRowId == row.RowId);
				var items = TreeItems.Where(q => q.ParentId == item?.Id);
				TreeItems.Remove(item);
				TreeItems.RemoveRange(items);
				IsTreeItemsChanged = true;
			}
		}

		public void AddViewGroup()
		{
			var row = new InsuranceProvidersViewGroup
			{
				RowId = Guid.NewGuid(),
				Name = "<New View Group>",
			};
			AddPropertyChangedInsuranceProvidersViewGroup(row);
			//var mrow = new InsuranceProvidersViewGroupMapping
			//{
			//	RowId = Guid.NewGuid(),
			//	InsuranceProvidersViewGroupRowId = row.RowId,
			//	InsuranceProviderRowId = MainEntity.RowId, 
			//};
			//row.InsuranceProvidersViewGroupMappings.Add(mrow);
			InsuranceProvidersViewGroups.Add(row);
			SelectedInsuranceProvidersViewGroup = row;
			AddViewGroupToTree(row, TreeItems);
			CalculateAll();
			IsTreeItemsChanged = true;

			BehaviorGridConrolInsuranceProvidersViewGroups.SetCurrentColumn("Name");
			BehaviorGridConrolInsuranceProvidersViewGroups.FocuseRow(InsuranceProvidersViewGroups.IndexOf(row));
			BehaviorGridConrolInsuranceProvidersViewGroups.ShowEditor(true);
		}
	}
}




