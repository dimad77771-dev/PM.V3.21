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
using Microsoft.Practices.ServiceLocation;
using System.ComponentModel;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class PickPatientViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		#endregion
		public virtual ObservableCollection<Patient> Entities { get; set; }
		public virtual Patient SelectedEntity { get; set; }
		public virtual OpenParams OpenParam { get; set; }
		public virtual String WindowTitle { get; set; }
		public virtual Boolean AutoExpandAllNodes { get; set; }
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();

		public virtual PickPatientInsuranceCoveragesDetailViewModel InsuranceCoveragesDetailViewModel { get; set; } = new PickPatientInsuranceCoveragesDetailViewModel();
		public virtual FilterMode[] FilterModes { get; set; } = new[]
		{
			new FilterMode(FilterModeEnum.All, "All"),
			new FilterMode(FilterModeEnum.NoInsurance, "No Insurance"),
			new FilterMode(FilterModeEnum.WithInsurance, "With Insurance"),
		};
		public virtual FilterMode CurrentFilterMode { get; set; }
		public virtual Boolean ShowFilterMode => (OpenParam.InsuranceProvidersViewGroupRowId != null);
		public virtual Boolean ShowInsuranceDetails => (OpenParam.InsuranceProvidersViewGroupRowId != null);

		public enum FilterModeEnum { All, NoInsurance, WithInsurance }
		public class FilterMode
		{
			public FilterModeEnum Code { get; set; }
			public string Name { get; set; }
			public FilterMode(FilterModeEnum code, string name)
			{
				Code = code;
				Name = name;
			}
		}

		public void OnCurrentFilterModeChanged(FilterMode oldFilterMode)
		{
			if (OpenParam == null) return;
			if (oldFilterMode.Code != CurrentFilterMode.Code)
			{
				ReloadData();
			}
		}

		public void OnSelectedEntityChanged(Patient oldSelectedEntity)
		{
			if (!ShowInsuranceDetails) return;
			if (SelectedEntity == null || SelectedEntity?.RowId == oldSelectedEntity?.RowId) return;

			DispatcherUIHelper.Run(async () =>
			{
				await InsuranceCoveragesDetailViewModel.LoadData(SelectedEntity.RowId);
			});
		}
		


		public PickPatientViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
			CurrentFilterMode = FilterModes.First(q => q.Code == FilterModeEnum.WithInsurance);
			MessengerHelper.Register<MsgRowChange<Patient>>(this, OnMsgRowChange);
		}

		
		public void OnOpen(OpenParams param)
		{
			OpenParam = param;
			ReloadData();
		}

		public void Pick(Patient entity)
		{
			SelectedEntity = entity;
			Submit();
		}

		public void Submit()
		{
			if (SelectedEntity == null) return;

			var ret = new ReturnParams { IsSuccess = true, PickPatient = SelectedEntity };
			OpenParam.TaskSource.SetResult(ret);
			CloseInteractionRequest.Raise(null);
		}
		public bool CanSubmit() => (SelectedEntity != null);

		public void Cancel()
		{
			CloseInteractionRequest.Raise(null);
		}
		public void ClosingEvent(CancelEventArgs arg)
		{
			if (OpenParam.TaskSource.Task.Status != TaskStatus.RanToCompletion)
			{
				var ret = new ReturnParams { IsSuccess = false };
				OpenParam.TaskSource.SetResult(ret);
			}
		}



		async Task LoadData()
		{
			ShowWaitIndicator.Show();
			if (OpenParam.PickMode == PickModeEnum.PickFamily)
			{
				WindowTitle = "Choose Family";
				AutoExpandAllNodes = false;
			}
			else if (OpenParam.PickMode == PickModeEnum.PickPatient)
			{
				WindowTitle = "Choose Patient";
				AutoExpandAllNodes = true;
			}
			else throw new ArgumentException();

			var qry = "";
			if (OpenParam.InsuranceProvidersViewGroupRowId != null)
			{
				if (CurrentFilterMode.Code == FilterModeEnum.WithInsurance)
				{
					qry = QueryHelper.BuildQuery("insuranceProvidersViewGroupRowId", OpenParam.InsuranceProvidersViewGroupRowId, "includeAllFamilyMember", true);
				}
				else if (CurrentFilterMode.Code == FilterModeEnum.NoInsurance)
				{
					qry = QueryHelper.BuildQuery("hasNoInsuranceProvider", true, "includeAllFamilyMember", true);
				}
			}
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetPatientList(qry));
			Entities = Mapper.Map<ObservableCollection<Patient>>(rows);
			BehaviorGridConrol.FocuseSearchControl();

			ShowWaitIndicator.Hide();
		}
		void ReloadData()
		{
			DispatcherUIHelper.Run(async () => await LoadData());
		}

		Guid lastNewActionBookmark;
		public void NewRow()
		{
			DispatcherUIHelper.Run(() =>
			{
				lastNewActionBookmark = Guid.NewGuid();
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OnePatientView,
					Param = new OnePatientViewModel.OpenParams
					{
						IsNew = true,
						RowId = default(Guid),
						NewActionBookmark = lastNewActionBookmark,
					},
				});
			});
		}


		public void ImportBodyrevivalsalonspa()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var result = await PickBodyrevivalsalonspaPatientViewModel.Pick(new PickBodyrevivalsalonspaPatientViewModel.OpenParams
				{
					ShowDXWindowsInteractionRequest = ShowDXWindowsInteractionRequest,
					MessageBoxService = MessageBoxService,
					BusinessService = businessService,
				});
				if (!result.IsSuccess)
				{
					return;
				}

				ReloadData();
			});
		}


		void OnMsgRowChange(MsgRowChange<Patient> msg)
		{
			MessengerHelper.RunAction(this, () =>
			{
				if (msg.RowAction == RowAction.Insert && msg.Options == "NewActionBookmark=" + lastNewActionBookmark)
				{
					DispatcherUIHelper.Run(async () =>
					{
						await LoadData();
						var row = Entities.FirstOrDefault(q => q.RowId == msg.Row.RowId);
						if (row != null)
						{
							SelectedEntity = row;
						}
					});
				}
			});
		}


		public class OpenParams
		{
			public PickModeEnum PickMode { get; set; }
			public Guid? InsuranceProvidersViewGroupRowId { get; set; }
			public TaskCompletionSource<ReturnParams> TaskSource { get; set; }
		}
		public enum PickModeEnum { PickPatient, PickFamily }


		public class ReturnParams
		{
			public Boolean IsSuccess { get; set; }
			public Patient PickPatient { get; set; }
		}

		public static Task<ReturnParams> PickPatient(
			InteractionRequest<ShowDXWindowsActionParam> showDXWindowsInteractionRequest,
			PickPatientViewModel.PickModeEnum pickMode,
			Guid? insuranceProvidersViewGroupRowId = null)
		{
			var tcs = new TaskCompletionSource<ReturnParams>();

			showDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.PickPatientView,
				Param = new PickPatientViewModel.OpenParams
				{
					PickMode = pickMode,
					InsuranceProvidersViewGroupRowId = insuranceProvidersViewGroupRowId,
					TaskSource = tcs,
				},
			});


			return tcs.Task;
		}

	}
}	
