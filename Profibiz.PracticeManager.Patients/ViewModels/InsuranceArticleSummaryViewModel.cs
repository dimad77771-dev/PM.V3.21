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
using System.IO;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class InsuranceArticleSummaryViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		#endregion
		public virtual InsuranceArticleSummary Entity { get; set; }
		public virtual ObservableCollection<InsuranceArticleV> Entities { get; set; }
		public virtual InsuranceArticleV SelectedEntity { get; set; }
		public virtual ObservableCollection<Category> FilterCategoriesAll { get; set; } = new ObservableCollection<Category>();
		public virtual List<Object> FilterCategoriesSelected { get; set; } = new List<Object>();
		public List<Object> LastFilterCategoriesSelected;


		public InsuranceArticleSummaryViewModel() : base()
		{
		}

		public void OnOpen(string parm)
		{
			OpenParmQuery = parm;
			OpenParms = QueryHelper.ParseString(parm);
			DispatcherUIHelper.Run2(LoadData());
		}
		String OpenParmQuery;
		NameValueCollection OpenParms;



		public async Task LoadData()
		{
			ShowWaitIndicator.Show();

			var categoriesRowIds = FilterCategoriesSelected.Cast<Category>().Select(q => q.RowId).OrderBy(q => q).ToArray();
			if (categoriesRowIds.SequenceEqual(FilterCategoriesAll.Select(q => q.RowId).OrderBy(q => q)))
			{
				categoriesRowIds = new Guid[0];
			}
			var query = "categoriesRowIds=" + categoriesRowIds.ToWebQuery();
			var entity = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetInsuranceArticleSummary(query));
			entity.Rows.ForEach(q => q.CalculateColumns());
			if (categoriesRowIds.Any())
			{
				entity.Rows = entity.Rows.Where(q => categoriesRowIds.Intersect(q.CategoryRowIds).Any()).ToArray();
			}


			Entity = entity;
			Entity.Rows.ForEach(q => SubscribeListRow(q));
			Entities = Entity.Rows.OrderBy(q => q.PatientsName).ToObservableCollection();

			if (!FilterCategoriesAll.Any())
			{
				FilterCategoriesAll = LookupDataProvider.Instance.Categories.OrderBy(q => q.CategoryType).ThenBy(q => q.FullName).ToObservableCollection();
			}

			LastFilterCategoriesSelected = FilterCategoriesSelected;


			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		void SubscribeListRow(InsuranceArticleV row)
		{
			row.OnOpenDetail = () =>
			{
				DispatcherUIHelper.Run(() =>
				{
					var param = new InsuranceArticleInfoViewModel.OpenParams
					{
						InsuranceCoverageRowId = row.InsuranceCoverageRowId,
						PatientRowId = row.PatientRowId0,
						СategoryRowId = row.CategoryRowId0,
					};
					ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
					{
						ViewCode = ViewCodes.InsuranceArticleInfoView,
						Param = param,
					});
				});
			};

			row.OnOpenDetail2 = () =>
			{
				DispatcherUIHelper.Run(() =>
				{
					ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
					{
						ViewCode = ViewCodes.InsuranceCoverage2WindowView,
						Param = new InsuranceCoverage2ViewModel.OpenParams
						{
							IsNew = false,
							RowId = row.InsuranceCoverageRowId,
							HighlightPatientRowId = row.PatientRowId0,
							HighlightCategoryRowId = row.CategoryRowId0,
						},
					});
				});
			};
		}

		public void OnFilterCategoriesSelectedChanged()
		{
			if (FilterCategoriesSelected == null) FilterCategoriesSelected = new List<object>();
			if (FilterCategoriesSelected.SequenceEqual(LastFilterCategoriesSelected))
			{
				return;
			}
			LastFilterCategoriesSelected = FilterCategoriesSelected;

			DispatcherUIHelper.Run2(LoadData());
		}


		public void ShowProblem()
		{
			DispatcherUIHelper.Run(() =>
			{
				var param = new InsuranceArticleInfoViewModel.OpenParams
				{
					ShowProblemOnly = true,
				};
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.InsuranceArticleInfoView,
					Param = param,
				});
			});
		}
	}
}
