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
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.TreeList;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class PatientsViewModel : ViewModelBase
	{
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		public GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();

		public virtual ObservableCollection<Patient> Entities { get; set; }
		public virtual Patient SelectedEntity { get; set; }
		//public virtual ObservableCollection<Patient> SelectedEntities { get; set; } = new ObservableCollection<Patient>();
		public virtual string CurrentView { get; set; } = "TreeListView";
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();

		public virtual Boolean IsMultiSelectMode { get; set; } = true;
		public string GridSelectionMode => (IsMultiSelectMode ? "MultipleRow" : "None");

		public virtual ObservableCollection<Patient> RibbonPatientsWithCheckBoxItems { get; set; }
		public virtual Int32 RibbonPatientsWithCheckBoxColumnCount { get; set; }
		public virtual String RibbonPatientsWithCheckBoxText { get; set; }

		public virtual AppointmentsSchedulerViewModel AppointmentsSchedulerModel { get; set; }
		public virtual Boolean IsShowAppointments { get; set; } = GlobalSettings.Instance.PatientsList.IsShowAppointments;
		public virtual Boolean IsVisibleAppointmentsScheduler => IsShowAppointments;

		public virtual User Role { get; set; } = UserManager.Role;



		//Test0001 ttt = new Test0001();

		public PatientsViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			NLog.vv();

			//ttt.Run();

			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
		}

		public void OnOpen(string parm)
		{
			NLog.vv(() => parm);

			OpenParmQuery = parm;
			OpenParms = QueryHelper.ParseString(parm);
			OpenParmInsuranceProviderRowId = QueryHelper.ParseGuid(OpenParms["InsuranceProviderRowId"]);
			MessengerHelper.Register<MsgRowChange<Patient>>(this, OnMsgRowChange);
			DispatcherUIHelper.Run(async () =>
			{
				await AppointmentsSchedulerModel.OnOpen("ViewMode=OnePatient");
				await LoadData();
			});
		}
		String OpenParmQuery;
		NameValueCollection OpenParms;
		Guid? OpenParmInsuranceProviderRowId;



		async Task LoadData()
		{
			ShowWaitIndicator.Show();
			var qry = OpenParmQuery;
			if (Role.Patient_RestrictPatientList)
			{
				qry += "&restrictPatientList=" + UserManager.UserRowId;
			}
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetPatientList(qry));
			Entities = Mapper.Map<ObservableCollection<Patient>>(rows.OrderBy(q => q.LastName).ThenBy(q => q.FirstName));
			UpdateIsCheckBoxVisibility();
			UpdateRibbonPatientsWithCheckBoxItems();
			Entities.ForEach(q => SubscribeRowItemChanged(q));
			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		public void RefreshData()
		{
			DispatcherUIHelper.Run(async () => await LoadData());
		}

		void SubscribeRowItemChanged(Patient row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(row.IsCheckBox))
				{
					UpdateRibbonPatientsWithCheckBoxItems();
				}
			};
		}

		public void OnIsShowAppointmentsChanged()
		{
			if (IsShowAppointments)
			{
				OnSelectedEntityChanged();
			}
			GlobalSettings.Update((q) => q.PatientsList.IsShowAppointments = IsShowAppointments);
		}

		void UpdateRibbonPatientsWithCheckBoxItems()
		{
			RibbonPatientsWithCheckBoxItems = Entities.Where(q => q.IsCheckBox).ToObservableCollection();
			RibbonPatientsWithCheckBoxColumnCount = 3;// Math.Max((RibbonPatientsWithCheckBoxItems.Count + 1) / 2, 1);
			RibbonPatientsWithCheckBoxText = "Selected: " + RibbonPatientsWithCheckBoxItems.Count;
		}

		void OnMsgRowChange(MsgRowChange<Patient> msg)
		{
			MessengerHelper.RunAction(this, () =>
			{
				var row = Entities.FirstOrDefault(q => q.RowId == msg.Row.RowId);
				var oldIsExpanded = row?.IsExpanded;

				MessengerHelper.UpdateEntities(this, Entities, msg.Row, msg.RowAction, (a, b) => a.RowId == b.RowId, () => SelectedEntity);

				row = Entities.FirstOrDefault(q => q.RowId == msg.Row.RowId);
				if (row != null && oldIsExpanded != null)
				{
					row.IsExpanded = oldIsExpanded.Value;
				}
				SubscribeRowItemChanged(row);

				UpdateIsCheckBoxVisibility();
			});
		}

		public void OnSelectedEntityChanged()
		{
			DispatcherUIHelper.Run(async () =>
			{
				if (!IsShowAppointments)
				{
					return;
				}

				await Task.Delay(200);
				var viewmodel = AppointmentsSchedulerModel;
				await viewmodel.SetOnePatient(SelectedEntity);
			});
		}

		public void Delete(Patient row)
		{
			DispatcherUIHelper.Run(async () =>
			{
				var messageBoxService = this.GetRequiredService<IMessageBoxService>();
				var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "Patient"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
					var uret = await businessService.DeletePatient(row.RowId);
					ShowWaitIndicator.Hide();
					if (!uret.Validate(messageBoxService)) return;
					Entities.Remove(row);
				}
			});
		}
		public bool CanDelete(Patient row) => (row != null && !Role.Patient_DataReadOnly);

		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public void Edit(Patient row)
		{
			AddEdit(row);
		}
		public bool CanEdit(Patient row) => (row != null);

		public void New()
		{
			//ttt.Test(); return;
			//BehaviorGridConrol.zzzzzzzzzz(); return;

			AddEdit(null);
		}
		public bool CanNew() => (!Role.Patient_DataReadOnly);

		void AddEdit(Patient row)
		{
			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.OnePatientView,
				Param = new OnePatientViewModel.OpenParams
				{
					IsNew = (row == null),
					RowId = (row == null ? default(Guid) : row.RowId)
				},
			});
		}


		public void ChangeView(string newCurrentView)
		{
			CurrentView = newCurrentView;
		}
		public bool CanChangeView(string newCurrentView)
		{
			return (CurrentView != newCurrentView);
		}


		public void TreeAction(string command)
		{
			if (command == "CollapseAll")
			{
				TreeListViewActions.CollapseAllNodes();
			}
			else if (command == "ExpandAll")
			{
				TreeListViewActions.ExpandAllNodes();
			}
		}
		public bool CanTreeAction(string command)
		{
			return (CurrentView == "TreeListView");
		}

		public virtual TreeListViewActions TreeListViewActions { get; set; } = new TreeListViewActions();

		public void CustomRowFilter(RowFilterEventArgs e)
		{
			//var row = Entities[e.ListSourceRowIndex];
			//System.Diagnostics.Debug.WriteLine("e.ListSourceRowIndex=" + e.ListSourceRowIndex);
			//System.Diagnostics.Debug.WriteLine("row=" + row.FullName);
			////if (row.IsNew)
			////{
			////	e.Visible = true;
			////	e.Handled = true;
			////}
		}
		public void CustomNodeFilter(TreeListNodeFilterEventArgs e)
		{
			//var rh = e.Node.RowHandle;
			//var row = BehaviorGridConrol.GetRow<Patient>(rh);
			//System.Diagnostics.Debug.WriteLine("row=" + row.FullName);
			////if (row.FullName == "Akmaev, Aleksander")
			//{
			//	BehaviorGridConrol.zzzzzzzzzz();
			//}
			////if (row.IsNew)
			////{
			////	e.Visible = true;
			////	e.Handled = true;
			////}
		}



		void UpdateIsCheckBoxVisibility()
		{
			BehaviorGridConrol.BeginDataUpdate();

			foreach (var row in Entities)
			{
				var isCheckBoxVisibility = true;
				if (row.FamilyMemberType != TypeHelper.FamilyMemberType.Head)
				{
					isCheckBoxVisibility = false;
				}
				else if (row.RowId != row.FamilyHeadRowId)
				{
					isCheckBoxVisibility = false;
				}
				else if (Entities.Any(q => q.FamilyHeadRowId == row.RowId && q.RowId != row.RowId))
				{
					isCheckBoxVisibility = false;
				}
				row.IsCheckBoxVisibility = isCheckBoxVisibility;
			}

			BehaviorGridConrol.EndDataUpdate();
		}

		public void ClearAllCheckBox()
		{
			//if (RuntimeHelper.IsMachineD)
			//{
			//	DispatcherUIHelper.Run(async () =>
			//	{
			//		var tmp = new PrintdocProccessing();
			//		await tmp.Run();
			//		return;
			//	});
			//}
			Entities.Where(q => q.IsCheckBox).ForEach(q => q.IsCheckBox = false);
			UpdateRibbonPatientsWithCheckBoxItems();
		}

		public void CheckingCheckout()
		{
			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.OneWorkInoutView,
				Param = new OneWorkInoutViewModel.OpenParams
				{
					IsNew = true,
					RowId = default(Guid),
					IsSimpleMode = true,
				},
			});
		}


		public void FamilyBuild()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var messageBoxService = this.GetRequiredService<IMessageBoxService>();

				var patients = Entities.Where(q => q.IsCheckBox).ToList();
				if (patients.Count < 2)
				{
					messageBoxService.ShowError("You must select more than one row");
					return;
				}
				patients.ForEach(q =>
				{
					q.IsSelectHeadFamily = false;
					q.IsSelectUseHeadAddress = false;
				});

				var parm = new PatientBuildFamilyViewModel.OpenParams
				{
					ShowDXWindowsInteractionRequest = ShowDXWindowsInteractionRequest,
					ListRows = patients,
				};
				var ret = await PatientBuildFamilyViewModel.Run(parm);
				if (!ret.IsSuccess)
				{
					return;
				}

				ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
				var uret = await businessService.PatientBuildFamily(patients);
				ShowWaitIndicator.Hide();
				if (!uret.Validate(messageBoxService))
				{
					return;
				}

				var familyHeadRowId = patients.Single(q => q.IsSelectHeadFamily).RowId;
				patients.Where(q => !q.IsSelectHeadFamily).ForEach(q =>
				{
					q.FamilyHeadRowId = familyHeadRowId;
					q.FamilyMemberType = TypeHelper.FamilyMemberType.Member;
				});
				patients.ForEach(q => q.IsCheckBox = false);
				patients.ForEach(q => OnMsgRowChange(new MsgRowChange<Patient> { RowAction = RowAction.Update, Row = q } ));
			});
		}
	}
}	
