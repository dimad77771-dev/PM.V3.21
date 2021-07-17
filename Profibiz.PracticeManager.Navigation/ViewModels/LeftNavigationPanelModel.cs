using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm.POCO;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PropertyChanged;
using Profibiz.PracticeManager.Infrastructure;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;
using Profibiz.PracticeManager.Navigation.Views;
using System.ComponentModel;
using System.Windows;
using Profibiz.PracticeManager.Model;

namespace Profibiz.PracticeManager.Navigation.ViewModels
{
	[ImplementPropertyChanged]
	//[POCOViewModel]
	public class LeftNavigationPanelViewModel //: ViewModelBase
	{
		public LeftNavigationPanelViewModel(Autofac.IContainer container) : base()
		{
			var allModules = new[]
			{
				new ProfibizModuleDescription
				{
					Code = "Patients",
					ModuleTitle = "Patients",
					ImageSource = new Uri("pack://application:,,,/Profibiz.PracticeManager.Infrastructure;component/Resources/Modules/icon-nav-employees-32.png"),
				},
				new ProfibizModuleDescription
				{
					Code = "Specialists",
					ModuleTitle = "Staffs/Specialists",
					ImageSource = new Uri("pack://application:,,,/Profibiz.PracticeManager.Infrastructure;component/Resources/Modules/icon-nav-customers-32.png"),
				},
				new ProfibizModuleDescription
				{
					Code = "AppointmentsScheduler",
					ModuleTitle = "Appointment Book",
					ImageSource = new Uri("pack://application:,,,/Profibiz.PracticeManager.Infrastructure;component/Resources/Modules/icon-nav-opportunities-32.png"),
				},
				new ProfibizModuleDescription
				{
					Code = "CalendarEventsScheduler",
					ModuleTitle = "Calendar Events",
					ImageSource = new Uri("pack://application:,,,/Profibiz.PracticeManager.Infrastructure;component/Resources/Modules/icon-nav-opportunities-32.png"),
				},
				new ProfibizModuleDescription
				{
					Code = "Finances",
					ModuleTitle = "Financials",
					ImageSource = new Uri("pack://application:,,,/Profibiz.PracticeManager.Infrastructure;component/Resources/Modules/icon-nav-finances-32.png"),
					NavPaneShowMode = DevExpress.Xpf.NavBar.ShowMode.Items,
					//StaticFilters = (new FilterItem[] { new FilterItem(), new FilterItem(), new FilterItem() }).ToObservableCollection()
				},
				new ProfibizModuleDescription
				{
					Code = "Inventory",
					ModuleTitle = "Inventory",
					ImageSource = new Uri("pack://application:,,,/Profibiz.PracticeManager.Infrastructure;component/Resources/Modules/icon-nav-opportunities-32.png"),
					NavPaneShowMode = DevExpress.Xpf.NavBar.ShowMode.Items,
					//StaticFilters = (new FilterItem[] { new FilterItem(), new FilterItem(), new FilterItem() }).ToObservableCollection()
				},
				new ProfibizModuleDescription
				{
					Code = "Chargeouts",
					ModuleTitle = "Outgoing Invoices",
					ImageSource = new Uri("pack://application:,,,/Profibiz.PracticeManager.Infrastructure;component/Resources/Modules/icon-nav-finances-32.png"),
					NavPaneShowMode = DevExpress.Xpf.NavBar.ShowMode.Items,
					//StaticFilters = (new FilterItem[] { new FilterItem(), new FilterItem(), new FilterItem() }).ToObservableCollection()
				},

				new ProfibizModuleDescription
				{
					Code = "Lookups",
					ModuleTitle = "Practice Setup",
					ImageSource = new Uri("pack://application:,,,/Profibiz.PracticeManager.Infrastructure;component/Resources/Modules/icon-nav-products-32.png"),
					//IsOpenModalWindow = true,
				},

				new ProfibizModuleDescription
				{
					Code = "WorkInout",
					ModuleTitle = "Staff Log",
					ImageSource = new Uri("pack://application:,,,/Profibiz.PracticeManager.Infrastructure;component/Resources/Modules/icon-nav-customers-32.png"),
				},
			};

			//if (!GlobalSettings.Instance.UserSettings.ShowChargeout)
			//{
			//	Modules = Modules.Where(q => q.Code != "Chargeouts").ToArray();
			//}

			var codes = new List<string>();
			var role = UserManager.Role;
			if (role.Main_Patients) codes.Add("Patients");
			if (role.Main_Specialists) codes.Add("Specialists");
			if (role.Main_AppointmentsScheduler) codes.Add("AppointmentsScheduler");
			if (role.Main_CalendarEventsScheduler) codes.Add("CalendarEventsScheduler");
			if (role.Main_Finances) codes.Add("Finances");
			if (role.Main_Inventory) codes.Add("Inventory");
			if (role.Main_Chargeouts) codes.Add("Chargeouts");
			if (role.Main_Lookups) codes.Add("Lookups");
			if (role.Main_WorkInout) codes.Add("WorkInout");
			Modules = allModules.Where(q => codes.Contains(q.Code)).ToArray();
			if (!Modules.Any())
			{
				Modules = new[] { allModules[0] };
			}


			if (RuntimeHelper.IsMachineD)
			{
				//SelectedModule = Modules.SingleOrDefault(q => q.Code == "Patients");
				//SelectedModule = Modules.SingleOrDefault(q => q.Code == "Specialists");
				SelectedModule = Modules.SingleOrDefault(q => q.Code == "Finances");
				//SelectedModule = Modules.SingleOrDefault(q => q.Code == "Chargeouts");
				//SelectedModule = Modules.SingleOrDefault(q => q.Code == "AppointmentsScheduler");
				//SelectedModule = Modules.SingleOrDefault(q => q.Code == "CalendarEventsScheduler");
				//SelectedModule = Modules.SingleOrDefault(q => q.Code == "Inventory");
				//SelectedModule = Modules.SingleOrDefault(q => q.Code == "Lookups");
				//SelectedModule = Modules.SingleOrDefault(q => q.Code == "WorkInout");
			}
			else if (RuntimeHelper.Release)
			{
				SelectedModule = Modules.SingleOrDefault(q => q.Code == "AppointmentsScheduler");
			}
			else
			{
				SelectedModule = Modules.SingleOrDefault(q => q.Code == "Patients");
				//SelectedModule = Modules.SingleOrDefault(q => q.Code == "Specialists");
				//SelectedModule = Modules.SingleOrDefault(q => q.Code == "Finances");
				//SelectedModule = Modules.SingleOrDefault(q => q.Code == "Lookups");
				//SelectedModule = Modules.SingleOrDefault(q => q.Code == "AppointmentsScheduler");
			}

			if (SelectedModule == null)
			{
				SelectedModule = Modules[0];
			}


			NavigationPaneViewIsExpanded = GlobalSettings.Instance.LeftNavigationPanel.NavBarIsExpanded;


			MessengerHelper.Register<MsgLeftNavigationPanelNeedClosePopUp>(this, OnMsgLeftNavigationPanelNeedClosePopUp);


			SelectionPreventResolver.OnResolve += (s, e) =>
			{
				var module = (ProfibizModuleDescription)e.NewModel;
				if (module.IsOpenModalWindow)
				{
					e.Cancel = true;
					OpenLookupsEditView();
				}
			};

			SubsribeChanges();
		}



		public virtual ProfibizModuleDescription[] Modules { get; set; }
		public virtual ProfibizModuleDescription SelectedModule { get; set; }
		public virtual BehaviorSelectionPreventResolver SelectionPreventResolver { get; set; } = new BehaviorSelectionPreventResolver();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual Boolean NavigationPaneViewIsExpanded { get; set; } = true;
		public virtual Boolean NavigationPaneViewIsPopupOpen { get; set; } = true;


		void SubsribeChanges()
		{
			(this as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(NavigationPaneViewIsExpanded))
				{
					GlobalSettings.Update((q) => q.LeftNavigationPanel.NavBarIsExpanded = NavigationPaneViewIsExpanded);
				}
			};
		}


		public void OnSelectedModuleChanged()
		{
			DispatcherUIHelper.Run(() =>
			{
				FrameworkElement view;
				if (SelectedModule.Code == "AppointmentsScheduler")
				{
					view = RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.AppointmentsSchedulerPanelView, null);
				}
				else if (SelectedModule.Code == "CalendarEventsScheduler")
				{
					view = RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.CalendarEventsSchedulerView, null);
				}
				else if (SelectedModule.Code == "WorkInout")
				{
					view = RegionHelper.OpenOrActivateViewInMainRegion(ViewCodes.WorkInoutsListView, null);
				}
				else
				{
					view = RegionHelper.OpenViewInRegion(SelectedModule.RegionName, SelectedModule.LeftFilterView, null);
				}
				var viewmodel = SelectedModule.ViewModel = view.DataContext;
				((ILeftPanelViewModel)viewmodel).Init();


				ShellViewModel.Instance.ShowLeftNavigationPanelRegion = (SelectedModule.Code != "AppointmentsScheduler" && SelectedModule.Code != "CalendarEventsScheduler" && SelectedModule.Code != "WorkInout");
			});
		}


		public void OpenLookupsEditView()
		{
			DispatcherUIHelper.Run(() =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.LookupsEditView,
					Param = new LookupsEditViewModel.OpenParams {  },
				});
			});
		}

		public void OnMsgLeftNavigationPanelNeedClosePopUp(MsgLeftNavigationPanelNeedClosePopUp arg)
		{
			NavigationPaneViewIsPopupOpen = false;
		}
	}

	[ImplementPropertyChanged]
	public class ProfibizModuleDescription
	{
		public String Code { get; set; }
		public String ModuleTitle { get; set; }
		public Uri ImageSource { get; set; }
		public String RegionName
		{
			get
			{
				return "Region-" + Code;
			}
		}
		public String LeftFilterView
		{
			get
			{
				return "LeftFilter" + Code + "View";
			}
		}
		public object ViewModel { get; set; }
		public DevExpress.Xpf.NavBar.ShowMode NavPaneShowMode { get; set; } = DevExpress.Xpf.NavBar.ShowMode.MaximizedDefaultItem;
		//public string Header => ModuleTitle + "-111";

		public virtual ObservableCollection<IFilterItem> StaticFilters { get; set; }
		//public virtual ObservableCollection<IFilterItem> StaticFilters { get; set; }

		public ProfibizModuleDescription()
		{
		}

		public bool IsOpenModalWindow { get; set; }
	}
}
