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
using AutoMapper;
using Newtonsoft.Json;
using PropertyChanged;
using Profibiz.PracticeManager.InfrastructureExt.Common;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using XtraAppointment = DevExpress.XtraScheduler.Appointment;
using TimeOfDayInterval = DevExpress.XtraScheduler.TimeOfDayInterval;
using Xtra = DevExpress.XtraScheduler;
using Profibiz.PracticeManager.Model;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Markup;
using System.Drawing;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Scheduler.Drawing;
using System.Windows.Media;
using Profibiz.PracticeManager.Patients.BusinessService;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class CalendarEventsSchedulerViewModel : ViewModelBase, ILeftPanelViewModel
	{
		#region Services
		IPatientsBusinessService businessService = ServiceHelper.GetInstance<IPatientsBusinessService>();
		ILookupsBusinessService lookupsService = ServiceHelper.GetInstance<ILookupsBusinessService>();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion
		#region Constaints
		public static readonly Guid ALL_APPOINTMENTBOOKS = default(Guid);
		public static readonly String ALL_APPOINTMENTBOOKS_NAME = "ALL APPOINTMENT BOOKS";
		#endregion

		public OpenParams OpenParam { get; set; }
		public virtual ObservableCollection<HospitalCalendarEvent> CalendarEvents { get; set; }
		public virtual ObservableCollection<XtraAppointment> SelectedCalendarEvents { get; set; } = new ObservableCollection<XtraAppointment>();
		XtraAppointment SelectedCalendarEvent => (SelectedCalendarEvents != null && SelectedCalendarEvents.Count == 1) ? SelectedCalendarEvents[0] : null;

		public virtual double DayViewVerticalCellMinHeight { get; set; } = 20;
		public virtual TimeSpan TimeScale { get; set; } = new TimeSpan(0, 30, 0);
		public virtual TimeOfDayInterval VisibleTimeInterval { get; set; } = TimeOfDayInterval.Day;

		public virtual Xtra.WeekDays WeekWorkDays { get; set; } = Xtra.WeekDays.EveryDay;
		public virtual SchedulerControlManager SchedulerControlManager { get; set; } = new SchedulerControlManager();
		public virtual DevExpress.Xpf.Scheduler.SchedulerControl SchedulerControl999 { get; set; } = new DevExpress.Xpf.Scheduler.SchedulerControl();


		public enum ViewModeEnum { CalendarEventBooks, OnePatient }
		public virtual ViewModeEnum ViewMode { get; set; }
		public virtual Patient OnePatient { get; set; }
		public bool IsOnePatient => (ViewMode == ViewModeEnum.OnePatient);


		public Boolean IsEnableControl { get; set; }
		public Boolean IsMainRibbonShow { get; set; }
		public Boolean IsSmallRibbonHide { get; set; }
		public Boolean AllCalendarEventBooksComboBoxShow => (ViewMode == ViewModeEnum.OnePatient);
		public String RibbonStyle => (ViewMode == ViewModeEnum.OnePatient ? "TabletOffice" : "Office2010");
		public Boolean RibbonHideHeaderAndTabsGrid => (ViewMode == ViewModeEnum.OnePatient);
		public Boolean IsVisibleRibbonEditCalendarEvents => (ViewMode != ViewModeEnum.OnePatient);
		public Boolean IsVisibleRibbonActiveView => true;
		public Boolean IsVisibleRibbonGroupBy => (ViewMode == ViewModeEnum.CalendarEventBooks);
		public Boolean IsVisibleRibbonCalendarEventBook => (ViewMode == ViewModeEnum.CalendarEventBooks);
		public Boolean IsVisibleRibbonSpecialists => (ViewMode == ViewModeEnum.CalendarEventBooks);
		public Boolean IsVisibleRibbonInsuranceFilters => (ViewMode == ViewModeEnum.CalendarEventBooks);
		

		public virtual DateTime FilterFrom { get; set; }
		public virtual DateTime FilterTo { get; set; }




		public CalendarEventsSchedulerViewModel() : base()
		{
			var ret = GlobalSettings.Instance.CalendarEvent.GetCalendarEventsViewDateFilter();
			FilterFrom = ret.FilterFrom;
			FilterTo = ret.FilterTo;

			this.PropertyChanged += CalendarEventsSchedulerViewModel_PropertyChanged;
			CellBackgroundBrushSelector = new CellBackgroundBrushSelectorClass(this);
		}

		private void CalendarEventsSchedulerViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var b = e.PropertyName;
		}

		public async Task OnOpen(object param)
		{
			if (QueryHelper.IsStringParam(param))
			{
				if ((string)param == "ViewMode=OnePatient")
				{
					ViewMode = ViewModeEnum.OnePatient;
				}
				else if ((string)param == "ViewMode=CalendarEventBooks")
				{
					ViewMode = ViewModeEnum.CalendarEventBooks;
				}
				else throw new ArgumentException();
			}
			else
			{
				OpenParam = (param as OpenParams) ?? new OpenParams();
			}

			GroupType = Xtra.SchedulerGroupType.None;
			IsMainRibbonShow = !IsOnePatient;
			IsSmallRibbonHide = !IsOnePatient;
			await LoadData();
		}



		public async Task LoadData()
		{
			NLog.vv();

			if (ViewMode == ViewModeEnum.OnePatient && OnePatient == null)
			{
				IsEnableControl = false;
				return;
			}

			IsEnableControl = true;
			ShowWaitIndicator.Show();

			await lookupsService.UpdateAllLookups();
			await LoadCalendarEventData();
			RegisterMessengers();

			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		async Task LoadCalendarEventData()
		{
			ShowWaitIndicator.Show();

			var patientRowId = OnePatient?.RowId;
			var task2 =
				businessService.GetCalendarEventList(patientRowId: patientRowId, startFrom: FilterFrom, startTo: FilterTo);
			await task2;
			var allCalendarEvents = task2.Result;

			CalendarEvents = allCalendarEvents.Select(q => CalendarEvent2HospitalCalendarEvent(q)).ToObservableCollection();

			CustomizeSchedulerView();

			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		bool isRegisterMessenger;
		void RegisterMessengers()
		{
			if (!isRegisterMessenger)
			{
				MessengerHelper.Register<MsgRowChange<CalendarEvent>>(this, OnMsgRowChangeCalendarEvent);
				MessengerHelper.Register<MsgRowChange<Patient>>(this, OnMsgRowChangePatient);
				isRegisterMessenger = true;
			}
		}

		public void Filter()
		{
			DispatcherUIHelper.Run(async () =>
			{
				GlobalSettings.Instance.CalendarEvent.SetCalendarEventsViewDateFilter(FilterFrom, FilterTo);
				await LoadData();
				await LoadCalendarEventData();
			});
		}

		public async Task SetOnePatient(Patient newOnePatient)
		{
			OnePatient = newOnePatient;
			await LoadData();
		}


		public HospitalCalendarEvent CalendarEvent2HospitalCalendarEvent(CalendarEvent row)
		{
			var serviceProvider = LookupDataProvider.FindServiceProvider(row.ServiceProviderRowId);
			var ret = new HospitalCalendarEvent
			{
				Entity = row,
				RowId = row.RowId,
				StartDate = row.Start,
				EndDate = row.Finish,
				AllDay = row.AllDay,
				Patient = row.Patient,
				PatientName = row.Patient?.FullName,
				ServiceProvider = serviceProvider,
				ServiceProviderName = serviceProvider?.FullName,
				Notes = row.Notes,
				Description = row.Description,
				Location = row.Description,
				InsuranceNumber = row.RefNumber,
				ParentViewModel = this,
			};
			return ret;
		}

		void OnMsgRowChangeCalendarEvent(MsgRowChange<CalendarEvent> msg)
		{
			MessengerHelper.RunAction(this, () =>
			{
				var row = msg.Row;
				var action = msg.RowAction;
				if (action == RowAction.Update)
				{
					var index = CalendarEvents.FindIndex(q => q.RowId == row.RowId);
					if (index >= 0)
					{
						var nrow = CalendarEvent2HospitalCalendarEvent(row);
						CalendarEvents[index] = nrow;
						SchedulerControlManager.SetSelection(nrow.StartDate, nrow.EndDate, null);
					}
				}
				else if (action == RowAction.Insert)
				{
					var nrow = CalendarEvent2HospitalCalendarEvent(row);
					CalendarEvents.Add(nrow);
					SchedulerControlManager.SetSelection(nrow.StartDate, nrow.EndDate, null);
				}
			});
		}

		void OnMsgRowChangePatient(MsgRowChange<Patient> msg)
		{
			MessengerHelper.RunAction(this, () =>
			{
				if (msg.RowAction == RowAction.Update)
				{
					if (CalendarEvents == null) return;

					var appointments = CalendarEvents.Where(q => q.Patient?.RowId == msg.Row.RowId).ToArray();
					foreach (var appointment in appointments)
					{
						var index = CalendarEvents.IndexOf(appointment);
						appointment.Entity.Patient = msg.Row;
						var nrow = CalendarEvent2HospitalCalendarEvent(appointment.Entity);
						CalendarEvents[index] = nrow;
					}
				}
			});
		}




		public void NewEntity(string arg) => AddEditEntity(null, arg == "Patient" ? OneCalendarEventViewModel.ShowModes.Patient : OneCalendarEventViewModel.ShowModes.ServiceProvider);
		public void EditEntity()
		{
			if (SelectedCalendarEvent != null)
			{
				AddEditEntity(XtraCalendarEvent2CalendarEvent(SelectedCalendarEvent));
			}
		}
		public void MouseDoubleClick(System.Windows.Input.MouseButtonEventArgs e)
		{
			e.Handled = true;
			EditEntity();
		}
		void AddEditEntity(CalendarEvent row, OneCalendarEventViewModel.ShowModes newShowModes = OneCalendarEventViewModel.ShowModes.Patient)
		{
			var param = new OneCalendarEventViewModel.OpenParams
			{
				IsNew = (row == null),
				RowId = (row == null ? default(Guid) : row.RowId),
			};
			if (row == null)
			{
				param.NewStart = SchedulerControlManager.SelectedInterval?.Start;
				param.NewFinish = SchedulerControlManager.SelectedInterval?.End;
				param.NewShowMode = newShowModes;
				if (ViewMode == ViewModeEnum.CalendarEventBooks)
				{
				}
				if (ViewMode == ViewModeEnum.OnePatient)
				{
					param.NewPatient = OnePatient;
				}
			}
			if (ViewMode == ViewModeEnum.OnePatient)
			{
				param.IsLockPatient = true;
			}

			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.OneCalendarEventView,
				Param = param,
			});
		}
		async public void DeleteEntity()
		{
			if (SelectedCalendarEvent != null)
			{
				var row = XtraCalendarEvent2CalendarEvent(SelectedCalendarEvent);
				var ret = MessageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "CalendarEvent"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
					var uret = await businessService.DeleteCalendarEvent(row.RowId);
					ShowWaitIndicator.Hide();
					if (!uret.Validate(MessageBoxService)) return;
					var arow = CalendarEvents.Single(q => q.RowId == row.RowId);
					CalendarEvents.Remove(arow);
					SchedulerControlManager.SetSelection(row.Start, row.Start, null);
				}

			}
		}

		public void CalendarEventViewInfoCustomize(DevExpress.Xpf.Scheduler.AppointmentViewInfoCustomizingEventArgs e)
		{
			var rowId = (Guid)e.ViewInfo.Appointment.CustomFields["RowId"];
			if (CalendarEvents.Count(q => q.RowId == rowId) > 1)
			{
				var b = 100;
			}
			var entity = CalendarEvents.Single(q => q.RowId == rowId);
			e.ViewInfo.CustomViewInfo = entity;
		}




		CalendarEvent XtraCalendarEvent2CalendarEvent(XtraAppointment xtraCalendarEvent)
		{
			return (xtraCalendarEvent?.CustomFields["Entity"] as CalendarEvent);
		}



		public void CalendarEventResizing(Xtra.AppointmentResizeEventArgs e)
		{
			//var appointment = (CalendarEvent)e.EditedCalendarEvent.CustomFields["Entity"];
			//if (appointment.InInvoice)
			//{
			//	e.Allow = false;
			//	e.Handled = true;
			//}
			//var a = e;
			Debug.WriteLine(e.EditedAppointment.End);
		}

		async public void CalendarEventResized(Xtra.AppointmentResizeEventArgs e)
		{
			e.Handled = true;
			if (!await SaveCalendarEvent(e.EditedAppointment))
			{
				e.Allow = false;
			}
		}

		async public void CalendarEventDrop(DevExpress.XtraScheduler.AppointmentDragEventArgs e)
		{
			if (!await SaveCalendarEvent(e.EditedAppointment))
			{
				e.Allow = false;
			}
		}

		async Task<bool> SaveCalendarEvent(XtraAppointment item)
		{
			DateTime finishTime, startTime;
			if (item.Start < item.End)
			{
				startTime = item.Start;
				finishTime = item.End;
			}
			else if (item.End < item.Start)
			{
				startTime = item.End;
				finishTime = item.Start;
			}
			else
			{
				return false;
			}
			//var serviceProviderRowId = (Guid?)item.ResourceId;
			//if (serviceProviderRowId == ServiceProvider.NO_DOCTOR)
			//{
			//	serviceProviderRowId = null;
			//}

			//appointment
			var appointment = (CalendarEvent)item.CustomFields["Entity"];
			if (appointment.Completed)
			{
				var err = "You cannot change Completed Calendar Event";
				MessageBoxService.ShowError(err);
				return false;
			}

			//quest
			var text = "Change Calendar Event time to " + startTime.FormatHHMM() + " - " + finishTime.FormatHHMM();
			var ret = MessageBoxService.ShowMessage(text, CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret != MessageResult.Yes)
			{
				return false;
			}

			//updateEntity
			appointment.Start = startTime;
			appointment.Finish = finishTime;
			var updateEntity = appointment.GetPocoClone();

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = await businessService.PutCalendarEvent(new List<CalendarEvent>() { updateEntity });
			ShowWaitIndicator.Hide();
			//if (!OneCalendarEventViewModel.ValidateCalendarEventUpdateErrror(uret, false, MessageBoxService))
			//{
			//	return false;
			//}

			var index = CalendarEvents.FindIndex(q => q.RowId == appointment.RowId);
			var nrow = CalendarEvent2HospitalCalendarEvent(appointment);
			CalendarEvents[index] = nrow;

			return true;
		}

		public void PopupMenuShowing(DevExpress.Xpf.Scheduler.SchedulerMenuEventArgs e)
		{
			//delete
			var menuNames = e.Menu.Items.OfType<System.Windows.FrameworkContentElement>().Select(q => q.Name).Where(q => !string.IsNullOrEmpty(q)).ToArray();
			menuNames.ForEach(q =>
			{
				e.Customizations.Add(new DevExpress.Xpf.Bars.RemoveBarItemAndLinkAction()
				{
					ItemName = q
				});
			});

			if (!IsEnableControl)
			{
				return;
			}

			//add
			if (e.Menu.Name == "DefaultMenu")
			{
				var myMenuItemPatient = new DevExpress.Xpf.Bars.BarButtonItem()
				{
					Name = "UserNewCalendarEventPatient",
					Content = "New Patient Event",
					Glyph = new BitmapImage(new Uri("pack://application:,,,/Profibiz.PracticeManager.Infrastructure;component/Resources/Doctor.png")),
				};
				myMenuItemPatient.ItemClick += (a, b) => NewEntity("Patient");
				e.Customizations.Add(myMenuItemPatient);

				var myMenuItemServiceProvider = new DevExpress.Xpf.Bars.BarButtonItem()
				{
					Name = "UserNewCalendarEventServiceProvider",
					Content = "New Service Provider Event",
					Glyph = new BitmapImage(new Uri("pack://application:,,,/Profibiz.PracticeManager.Infrastructure;component/Resources/specialist-16.png")),
				};
				myMenuItemServiceProvider.ItemClick += (a, b) => NewEntity("ServiceProvider");
				e.Customizations.Add(myMenuItemServiceProvider);

				e.Customizations.Add(new DevExpress.Xpf.Bars.BarItemLinkSeparator());
			}
			else if (e.Menu.Name == "CalendarEventMenu")
			{
				var myMenuItem1 = new DevExpress.Xpf.Bars.BarButtonItem()
				{
					Name = "UserEditCalendarEvent",
					Content = "Edit Calendar Event",
					Glyph = new BitmapImage(new Uri("pack://application:,,,/Profibiz.PracticeManager.InfrastructureExt;component/Resources/icon-edit-16.png")),
				};
				myMenuItem1.ItemClick += (a, b) => EditEntity();
				e.Customizations.Add(myMenuItem1);
				e.Customizations.Add(new DevExpress.Xpf.Bars.BarItemLinkSeparator());

				var myMenuItem2 = new DevExpress.Xpf.Bars.BarButtonItem()
				{
					Name = "UserDeleteCalendarEvent",
					Content = "Delete Calendar Event",
					Glyph = new BitmapImage(new Uri("pack://application:,,,/Profibiz.PracticeManager.InfrastructureExt;component/Resources/icon-delete-16.png")),
				};
				myMenuItem2.ItemClick += (a, b) => DeleteEntity();
				e.Customizations.Add(myMenuItem2);
				e.Customizations.Add(new DevExpress.Xpf.Bars.BarItemLinkSeparator());
			}
		}


		public void ActiveViewChanged(EventArgs e)
		{
			CustomizeSchedulerView();
		}


		public void ViewZoomIn()
		{
			SchedulerControlManager.ZoomIn();
		}

		public void ViewZoomOut()
		{
			SchedulerControlManager.ZoomOut();
		}


		[ImplementPropertyChanged]
		public class Doctor
		{
			public Guid Id { get; set; }
			public String Name { get; set; }
			public String BackgroundColor { get; set; }
			public String ForegroundColor { get; set; }
			public Boolean IsSelected { get; set; }
			public ServiceProvider Entity { get; set; }

			public DelegateCommand DoctorSelectUnselectCommand => new DelegateCommand(() =>
			{
				IsSelected = !IsSelected;
			});
		}

		public class HospitalCalendarEvent
		{
			public Guid RowId { get; set; }
			public DateTime StartDate { get; set; }
			public DateTime EndDate { get; set; }
			public string PatientName { get; set; }
			public Patient Patient { get; set; }
			public string ServiceProviderName { get; set; }
			public ServiceProvider ServiceProvider { get; set; }
			public string Notes { get; set; }
			public string Description { get; set; }
			public string Location { get; set; }
			public string InsuranceNumber { get; set; }
			public bool FirstVisit { get; set; }
			public string Recurrence { get; set; }
			public int Type { get; set; }
			public bool AllDay { get; set; }
			public CalendarEvent Entity { get; set; }
			public CalendarEventsSchedulerViewModel ParentViewModel { get; set; }

			public String StartEndDateString => AllDay ? "All Day Event" : StartDate.FormatHHMM() + " - " + EndDate.FormatHHMM();
			public String DateString => StartDate.FormatShortDate();
			public String Subject => (Patient != null ? PatientName : "(*)" + ServiceProviderName);
		}


		public void ChangeActiveView(Xtra.SchedulerViewType value)
		{
			ActiveViewType = value;
			if (ActiveViewType == Xtra.SchedulerViewType.Week)
			{
				//GroupType = Xtra.SchedulerGroupType.None;
			}
			CustomizeSchedulerView();
		}
		public Xtra.SchedulerViewType ActiveViewType { get; set; } = Xtra.SchedulerViewType.Month;

		public void ChangeGroupType(Xtra.SchedulerGroupType value)
		{
			GroupType = value;
		}
		public Xtra.SchedulerGroupType GroupType { get; set; } = Xtra.SchedulerGroupType.Resource;

		public enum ShowHoursEnum { Working, All }
		public ShowHoursEnum ShowHours { get; set; } = ShowHoursEnum.Working;
		public Boolean ShowHoursIsEnabled { get; set; }
		public Boolean ShowHoursIsVisible { get; set; }
		public void OnShowHoursChanged()
		{
			CustomizeSchedulerView();
		}

		public String ShowDays { get; set; } = "Mon-Fri";
		public void OnShowDaysChanged()
		{
			CustomizeSchedulerView();
		}
		public Boolean ShowDaysIsEnabled { get; set; }
		public Boolean ShowDaysIsVisible { get; set; }

		public Int32 ShowDays2 { get; set; } = 1;
		public void OnShowDays2Changed()
		{
			CustomizeSchedulerView();
		}
		public Boolean ShowDays2IsEnabled { get; set; }
		public Boolean ShowDays2IsVisible { get; set; }

		public String StartEndTimeVisibility { get; set; } = "Auto";
		public Boolean StartEndTimeVisibilityIsEnabled { get; set; }
		public Boolean StartEndTimeVisibilityIsVisible { get; set; }

		public Boolean CompressWeekend { get; set; } = true;
		public Boolean CompressWeekendIsEnabled { get; set; } = true;
		public Boolean CompressWeekendIsVisible { get; set; } = true;


		public void CustomizeSchedulerView()
		{
			//TimeScale = new TimeSpan(0, SelectedCalendarEventBook.Interval, 0);
			//VisibleTimeInterval =
			//	(ShowHours == ShowHoursEnum.Working && SelectedCalendarEventBook.RowId != ALL_APPOINTMENTBOOKS) ?
			//	new TimeOfDayInterval(SelectedCalendarEventBook.StartAt.TimeOfDay, SelectedCalendarEventBook.FinishAt.TimeOfDay) :
			//	new TimeOfDayInterval(new TimeSpan(0, 0, 0), new TimeSpan(24, 0, 0));
			TimeScale = new TimeSpan(0, 30, 0);
			VisibleTimeInterval = new TimeOfDayInterval(new TimeSpan(0, 0, 0), new TimeSpan(24, 0, 0));

			var mon_fri = Xtra.WeekDays.Monday | Xtra.WeekDays.Tuesday | Xtra.WeekDays.Wednesday | Xtra.WeekDays.Thursday | Xtra.WeekDays.Friday;
			var mon_sat = mon_fri | Xtra.WeekDays.Saturday;
			WeekWorkDays =
					ShowDays == "Mon-Fri" ? mon_fri :
					ShowDays == "Mon-Sat" ? mon_sat :
					Xtra.WeekDays.EveryDay;


			ShowHoursIsEnabled = (ActiveViewType == Xtra.SchedulerViewType.Day || ActiveViewType == Xtra.SchedulerViewType.WorkWeek);
			ShowDaysIsEnabled = (ActiveViewType == Xtra.SchedulerViewType.WorkWeek);
			ShowDays2IsEnabled = (ActiveViewType == Xtra.SchedulerViewType.Day);
			ShowDays2IsVisible = (ActiveViewType == Xtra.SchedulerViewType.Day);
			ShowDaysIsVisible = !ShowDays2IsVisible;
			ShowHoursIsVisible = true;
			StartEndTimeVisibilityIsVisible = true;

			StartEndTimeVisibilityIsEnabled = (ActiveViewType == Xtra.SchedulerViewType.Day || ActiveViewType == Xtra.SchedulerViewType.WorkWeek);

			var isMonthView = (ActiveViewType == Xtra.SchedulerViewType.Month);
			CompressWeekendIsVisible = isMonthView;
			StartEndTimeVisibilityIsVisible = StartEndTimeVisibilityIsVisible && !isMonthView;
			ShowDays2IsVisible = ShowDays2IsVisible && !isMonthView;
			ShowDaysIsVisible = ShowDaysIsVisible && !isMonthView;
			ShowHoursIsVisible = ShowHoursIsVisible && !isMonthView;
		}


		public CellBackgroundBrushSelectorClass CellBackgroundBrushSelector { get; set; }
		public class CellBackgroundBrushSelectorClass : ICellBrushSelector, DevExpress.Xpf.Scheduler.Internal.ISchedulerDefaultCellBrushSelector
		{
			private CalendarEventsSchedulerViewModel ParentViewModel { get; set; }
			public DayViewCellBackgroundBrushSelector DefaultSelector = new DayViewCellBackgroundBrushSelector();

			public CellBackgroundBrushSelectorClass(CalendarEventsSchedulerViewModel parent)
			{
				ParentViewModel = parent;
			}

			public System.Windows.Media.Brush SelectBrush(VisualResourceCellBaseContent content)
			{
				if (ParentViewModel.IsOnePatient)
				{
					var workTimeCell = content as VisualWorkTimeCellBaseContent;
					if (workTimeCell == null) return null;

					var iStart = workTimeCell.IntervalStart;
					var iFinish = workTimeCell.IntervalEnd;
					//var ex = ParentViewModel.FindInIntervalAllCalendarEventInCalendarEventBook(iStart, iFinish).Any();
					var ex = false;

					var color = ex ? System.Windows.Media.Color.FromRgb(255, 186, 186) : System.Windows.Media.Color.FromRgb(191, 255, 191);
					return new SolidColorBrush { Color = color };
				}
				else
				{
					return DefaultSelector.SelectBrush(content);
				}




				//	return null;
				//if (workTimeCell.Brushes == null)
				//	return null;
				//if (workTimeCell.IsWorkTime)
				//	return workTimeCell.Brushes.CellLight;
				//else
				//	return workTimeCell.Brushes.Cell;
			}
		}

		void UpdateCellBackgroundBrush()
		{
			CellBackgroundBrushSelector = new CellBackgroundBrushSelectorClass(this);
		}

		public void Init() { } 

		public class OpenParams
		{
			//public Patient OnePatient { get; set; }

			//public Guid CalendarEventBookRowId { get; set; }
			//public bool IsNew { get; set; }
			//public Guid RowId { get; set; }
		}
	}






}
