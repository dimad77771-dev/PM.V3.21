using DevExpress.DevAV.ViewModels;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Profibiz.PracticeManager.Infrastructure;
using Profibiz.PracticeManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm.POCO;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PropertyChanged;
using System.ComponentModel;
using System.Web;
using Profibiz.PracticeManager.Patients.BusinessService;


namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[ImplementPropertyChanged]
	public class LeftFilterSpecialistsViewModel : ILeftPanelViewModel
	{
		public LeftFilterSpecialistsViewModel()
		{
			MessengerHelper.RunAction(this, () =>
			{
				MessengerHelper.Register<MsgRowLookupUpdate>(this, OnLookupUpdate);
				BuildAllFilters();
			});
		}

		public void Init()
		{
			if (SelectedItem != null)
			{
				SelectedItem = InsuranceProviderFilters.SingleOrDefault(q => FilterItem.IsEqual(q, SelectedItem));
			}
			if (SelectedItem == null)
			{
				SelectedItem = AllItemsFilter;
			}
		}



		async void BuildAllFilters()
		{
			await BusinessServiceHelper.UpdateAllLookups();
			BuildInsuranceProviderFilters();
		}

		void OnLookupUpdate(MsgRowLookupUpdate arg)
		{
			if (arg.Table == MsgRowLookupUpdate.TableEnum.ProfessionalAssociations)
			{
				BuildAllFilters();
				Init();
			}
		}


		void BuildInsuranceProviderFilters()
		{
			var items = new List<FilterItem>();
			var professionalAssociations = LookupDataProvider.Instance.ProfessionalAssociations;
			items.AddRange(professionalAssociations.OrderBy(q => q.Name).Select(q => new FilterItem(q.Name, FilterItem.FilterColumnEnum.ProfessionalAssociation, q.RowId)));
			InsuranceProviderFilters = new ObservableCollection<FilterItem>(items);
			StaticFilterItems = InsuranceProviderFilters.ToObservableCollection();
		}


		public FilterItem AllItemsFilter { get; set; } = new FilterItem { Name = "All", IsAllItems = true };
		public string InsuranceProviderFilterName { get; set; } = "Professional Associations";
		public ObservableCollection<FilterItem> InsuranceProviderFilters { get; set; } = new ObservableCollection<FilterItem>();
		public FilterItem SelectedItem { get; set; }
		public ObservableCollection<FilterItem> StaticFilterItems { get; set; }


		FilterItem CurrentFilterItem;
		public void OnSelectedItemChanged()
		{
			DispatcherUIHelper.Run(() =>
			{
				if (SelectedItem != null && !FilterItem.IsEqual(CurrentFilterItem, SelectedItem))
				{
					RegionHelper.CloseLeftNavigationPopUp();

					CurrentFilterItem = SelectedItem;
					var currentViewModel = RegionHelper.GetMainRegionView()?.DataContext as SpecialistsViewModel;
					var parm = new Dictionary<string, object>() { { "ProfessionalAssociationRowId", CurrentFilterItem.FilterRowId } };
					if (currentViewModel != null)
					{
						var qry = QueryHelper.BuildQuery(parm);
						currentViewModel.OnOpen(qry);
					}
					else
					{
						RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.SpecialistsView, parm);
					}
				}
			});
		}


		[ImplementPropertyChanged]
		public class FilterItem : IFilterItem
		{
			public FilterItem() { }

			public FilterItem(String name, FilterColumnEnum filterColumn, Guid? filterRowId)
			{
				Name = name;
				FilterColumn = filterColumn;
				FilterRowId = filterRowId;
			}

			public virtual string Name { get; set; }
			public virtual bool IsAllItems { get; set; }
			public virtual int EntitiesCount { get; set; } = -1;
			public virtual string ImageUri { get; set; }
			public FilterColumnEnum FilterColumn;
			public Guid? FilterRowId;


			public virtual string DisplayText
			{
				get
				{
					return Name + (EntitiesCount == -1 ? "" : "(" + EntitiesCount + ")");
				}
			}

			public static bool IsEqual(FilterItem item1, FilterItem item2)
			{
				return (item1 != null && item2 != null && item1.FilterRowId == item2.FilterRowId && item1.FilterColumn == item2.FilterColumn);
			}

			public enum FilterColumnEnum { ProfessionalAssociation }
		}
	}
}
